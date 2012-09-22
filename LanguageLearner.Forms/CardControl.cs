using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using LanguageLearner.Data;

namespace LanguageLearner.UI
{
    /// <summary>
    /// This class handles presenting the card to the user and also handles any changes made to the card.
    /// </summary>
    public partial class CardControl : UserControl
    {
        dsLanguageData.CardRow _curCard;
        dsLanguageData.SoundClipRow _curSoundClip;
        bool _answerWasShown;

        #region Properties 

        /// <summary>
        /// Gets whether or not the card has finished displaying its information.
        /// </summary>
        public bool InformationWasShown
        {
            get { return _answerWasShown; }
        }

        /// <summary>
        /// Gets or sets the current selected Difficulty of the card.
        /// </summary>
        int Difficulty
        {
            get
            {
                if (radioBtnEasy.Checked)
                    return 0;
                if (radioBtnMedium.Checked)
                    return 1;
                if (radioBtnMedium.Checked)
                    return 2;
                return 1;
            }

            set
            {
                switch (value)
                {
                    case 0:
                        radioBtnEasy.Checked = true;
                        break;
                    case 1:
                        radioBtnMedium.Checked = true;
                        break;
                    case 2:
                        radioBtnHard.Checked = true;
                        break;
                    default:
                        radioBtnMedium.Checked = true;
                        break;
                }
            }
        }
        #endregion

        public CardControl()
        {
            InitializeComponent();
        }

        #region Public Methods
        /// <summary>
        /// Loads the card and also checks to make sure the user is finished with the previous card.
        /// </summary>
        /// <param name="card"></param>  
        /// <returns>True if the card was loaded</returns>    
        public bool LoadCard(dsLanguageData.CardRow card)
        {
            bool result = false;

            if (ChangesHaveBeenSavedOrIgnored())
            {
                ClearCard();

                _curCard = card;

                LanguageData dataLayer = new LanguageData();
                dsLanguageData.SoundClipDataTable dtSound = dataLayer.daSoundClip.GetDataByCardID(_curCard.ID);
                if (dtSound.Rows.Count > 0)
                    _curSoundClip = dtSound[0];
                
                txtQuestion.Text = getTextForQuestion(_curCard);

                if (chkBoxShowAnswer.Checked)
                    showAnswer();

                txtNotes.Text = _curCard.Notes;

                Difficulty = _curCard.Difficulty;
                result = true;
            }            

            return result;
        }

        /// <summary>
        /// Saves the card.
        /// </summary>
        /// <returns></returns>
        public bool SaveCard()
        {
            _curCard.LastModified = DateTime.Now;
            _curCard.Notes = txtNotes.Text;
            _curCard.Difficulty = Difficulty;

            showAnswer();
            if (chkBoxSwitchQuestionAnswer.Checked)
            {
                _curCard.Question = txtAnswer.Text;
                _curCard.Answer = txtQuestion.Text;
            }
            else
            {
                _curCard.Answer = txtAnswer.Text;
                _curCard.Question = txtQuestion.Text;
            }

            LanguageData dataLayer = new LanguageData();
            int i = dataLayer.InsertOrUpdateCard(_curCard);

            if(i > 0)
                return true;
            return false;
        }

        /// <summary>
        /// Switches which gets shown first: the question or answer
        /// </summary>
        public void SwitchQuestionAndAnswer()
        {
            chkBoxSwitchQuestionAnswer.Checked = !chkBoxSwitchQuestionAnswer.Checked;
        }

        /// <summary>
        /// Unloads the current card and clears any data on the screen.
        /// </summary>
        public void ClearCard()
        {
            _curCard = null;
            _curSoundClip = null;

            _answerWasShown = false;
            txtAnswer.Text = string.Empty;
            txtNotes.Text = string.Empty;
            txtQuestion.Text = string.Empty;
            radioBtnMedium.Checked = true;
        }

        /// <summary>
        /// Deletes the current card. And clears the data from the screen.
        /// </summary>
        public void DeleteCard()
        {
            if (_curCard != null && _curCard.ID > 0)
            {
                LanguageData dataLayer = new LanguageData();
                dataLayer.DeleteCard(_curCard.ID);
            }
            ClearCard();
        }

        /// <summary>
        /// Displays the answer if it has not already been shown.
        /// </summary>
        public void ShowAnswer()
        {
            if (!_answerWasShown && _curCard != null)
            {
                txtAnswer.Text = getTextForAnswer(_curCard);
                _answerWasShown = true;
            }
        }
        #endregion

        #region Private Methods        

        /// <summary>
        /// Gets the text from a card that should be shown in the question box.
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        string getTextForQuestion(dsLanguageData.CardRow card)
        {
            if (chkBoxSwitchQuestionAnswer.Checked)
                return card.Answer;
            return card.Question;
        }

        /// <summary>
        /// Gets the text from a card that should be shown in the answer box.
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        string getTextForAnswer(dsLanguageData.CardRow card)
        {
            if (chkBoxSwitchQuestionAnswer.Checked)
                return card.Question;
            return card.Answer;
        }

        /// <summary>
        /// Checks to see if any changes have been handled.
        /// </summary>
        /// <returns></returns>
        private bool ChangesHaveBeenSavedOrIgnored()
        {
            bool ok = true;

            if (hasChanges())
            {
                ok = false;
                DialogResult result = MessageBox.Show("Do you want to save the changes?", "Save Changes?", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                    ok = SaveCard();                
            }
            return ok;
        }

        bool hasChanges()
        {
            if(_curCard == null)
                return false;

            LanguageData dataLayer = new LanguageData();
            dsLanguageData.CardRow tmpCard = dataLayer.daCard.GetDataByCardID(_curCard.ID).Rows[0] as dsLanguageData.CardRow;
            dsLanguageData.SoundClipDataTable tmpDTSound = dataLayer.daSoundClip.GetDataByCardID(_curCard.ID);
            dsLanguageData.SoundClipRow tmpSoundClip = null;
            if(tmpDTSound.Rows.Count > 0)
                tmpSoundClip = tmpDTSound[0];

            bool hasChanges = false;

            if (tmpCard.Question.Trim() != txtQuestion.Text.Trim())
                hasChanges = true;
            if (!_answerWasShown)
                showAnswer();
            if (tmpCard.Answer.Trim() != txtAnswer.Text.Trim())
                hasChanges = true;
            if (tmpCard.Notes.Trim() != txtNotes.Text.Trim())
                hasChanges = true;
            if (tmpCard.Difficulty != Difficulty)
                hasChanges = true;

            //if the sound clips are not null check to see if the match
            if (tmpSoundClip != null && _curSoundClip != null)
            {
                if (tmpSoundClip.SoundClip.Length != _curSoundClip.SoundClip.Length)
                    hasChanges = true;
            }
            else if (tmpSoundClip != _curSoundClip)
                hasChanges = true;

            return hasChanges;
        }
        #endregion

        private void chkBoxSwitchQuestionAnswer_CheckedChanged(object sender, EventArgs e)
        {
            if (_curCard != null)
            {
                showAnswer();

                string tmp = txtQuestion.Text;
                txtQuestion.Text = txtAnswer.Text;
                txtAnswer.Text = tmp;
            }
        }       
    }
}
