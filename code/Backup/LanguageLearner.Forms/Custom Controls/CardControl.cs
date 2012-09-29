using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using LanguageLearner.Data;
using Alvas.Forms;
using System.IO;

namespace LanguageLearner.UI
{
    /// <summary>
    /// This class handles presenting the card to the user and also handles any changes made to the card.
    /// </summary>
    public partial class CardControl : UserControl
    {
        dsLanguageData.CardRow _curCard;
        dsLanguageData.SoundClipRow _curSoundClip;
        bool _informationWasShown;
        Dictaphone _dictaphone = new Dictaphone();
        DictaphoneState _dictaphone_state = DictaphoneState.Initial;
        private const string timeFormat = "Time in ms: {0}";

        #region Properties 
        public dsLanguageData.CardRow CurrentCard
        {
            get { return _curCard; }
            set
            {
                LoadCard(value);
            }
        }
        
        /// <summary>
        /// Gets whether or not all the cards information has been displayed
        /// </summary>
        public bool InformationHasBeenDisplayed
        {
            get 
            {
                if (_curCard == null)
                    return true;
                return _informationWasShown; 
            }           
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

            _dictaphone.SoundLevelMeter = vum;
            _dictaphone.ChangePosition += new Dictaphone.ChangePositionEventHandler(_dictaphone_ChangePosition);
            _dictaphone.ChangeState += new Dictaphone.ChangeStateEventHandler(_dictaphone_ChangeState);
        }

        void _dictaphone_ChangePosition(object sender, PositionEventArgs e)
        {
            lblTime.Text = string.Format(timeFormat, e.Position);
        }

        void _dictaphone_ChangeState(object sender, DictaphoneStateEventArgs e)
        {
            _dictaphone_state = e.State;
            if (e.State == DictaphoneState.Initial)
                lblTime.Text = string.Format(timeFormat, "0");
        }

        #region Public Methods

        /// <summary>
        /// Displays the answer if it has not already been shown.
        /// </summary>
        public void ShowInformation()
        {
            if (!_informationWasShown && _curCard != null)
            {
                txtAnswer.Text = getTextForAnswer(_curCard);
                _informationWasShown = true;
            }
        }

        /// <summary>
        /// Loads the card and also checks to make sure the user is finished with the previous card.
        /// </summary>
        /// <param name="card"></param>
        /// <returns>Whether or not the card was loaded</returns>
        public bool LoadCard(dsLanguageData.CardRow card)
        {
            bool result = false;

            if (ChangesHaveBeenSavedOrIgnored())
            {
                ClearCard();
               
                if (card == null)
                    return true;

                SuspendLayout();

                _curCard = card;

                LanguageData dataLayer = new LanguageData();
                
                dsLanguageData.SoundClipDataTable dtSound = dataLayer.daSoundClip.GetDataByCardID(_curCard.ID);
                if (dtSound.Rows.Count > 0 && dtSound[0].SoundClip != null)
                {
                    _curSoundClip = dtSound[0];
                    _dictaphone.WavStream = new MemoryStream(_curSoundClip.SoundClip);
                    _dictaphone.ClosePlayer();
                    setSoundButton(true);
                }

                dsLanguageData.PictureDataTable dtPic = dataLayer.daPicture.GetDataByCardID(_curCard.ID);
                if (dtPic.Rows.Count > 0 && dtPic[0].Image != null)
                {
                    pictureBox.Image = dataLayer.ByteArrayToImage(dtPic[0].Image);                    
                }

                txtQuestion.Text = getTextForQuestion(_curCard);

                if (chkBoxShowAnswer.Checked)
                    ShowInformation();

                txtNotes.Text = _curCard.Notes;
                Difficulty = _curCard.Difficulty;

                result = true;

                ResumeLayout();
            }

            if (result)
            {
                SetEnable(true);
            }

            return result;
        }

        /// <summary>
        /// Saves the card.
        /// </summary>
        /// <returns></returns>
        public bool SaveCard()
        {
            if (_curCard == null)
                return false;

            writeDataToCard(_curCard);

            LanguageData dataLayer = new LanguageData();            
            int i = dataLayer.InsertOrUpdateCard(_curCard);

            writeSoundDataToSoundClip(_curSoundClip);

            i += dataLayer.InsertUpdateOrDeleteSoundClip(_curSoundClip);
            
            i += dataLayer.InsertUpdateOrDeleteImage(pictureBox.Image, _curCard.ID, _curCard.Answer);

            if(CardDataChanged != null)
                CardDataChanged(this, new CardChangedEventArgs(_curCard, _curSoundClip));

            if(i > 0)
                return true;
            return false;
        }

        dsLanguageData.SoundClipRow writeSoundDataToSoundClip(dsLanguageData.SoundClipRow sound)
        {
            if (sound != null)
            {
                sound.CardID = _curCard.ID;
                sound.Name = getAnswerText();
            }

            return sound;
        }

        dsLanguageData.CardRow writeDataToCard(dsLanguageData.CardRow card)
        {
            if (card != null)
            {
                card.LastModified = DateTime.Now;
                card.Notes = txtNotes.Text;
                card.Difficulty = Difficulty;

                ShowInformation();
                if (chkBoxSwitchQuestionAnswer.Checked)
                {
                    card.Question = txtAnswer.Text;
                    card.Answer = txtQuestion.Text;
                }
                else
                {
                    card.Answer = txtAnswer.Text;
                    card.Question = txtQuestion.Text;
                }
            }

            return card;
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
            SuspendLayout();

            _curCard = null;
            _curSoundClip = null;
            _dictaphone.WavStream = null;
            pictureBox.Image = null;

            setSoundButton(false);

            _informationWasShown = false;
            txtAnswer.Text = string.Empty;
            txtNotes.Text = string.Empty;
            txtQuestion.Text = string.Empty;
            radioBtnMedium.Checked = true;

            SetEnable(false);

            ResumeLayout();
        }

        void SetEnable(bool enabled)
        {
            SuspendLayout();

            foreach (Control c in Controls)
                c.Enabled = enabled;

            btnAddNewCard.Enabled = true;

            ResumeLayout();
        }

        /// <summary>
        /// Deletes the current card. And clears the data from the screen.
        /// </summary>
        public void DeleteCard()
        {
            if (_curCard != null && _curCard.ID > 0)
            {
                int id = _curCard.ID;
                LanguageData dataLayer = new LanguageData();
                if (dataLayer.DeleteCard(_curCard.ID) > 0)
                {
                    if (CardWasDeleted != null)
                        CardWasDeleted(this, id);
                }
            }
            ClearCard();
        }

        /// <summary>
        /// Checks to see if any changes have been handled.
        /// </summary>
        /// <returns></returns>        
        public bool ChangesHaveBeenSavedOrIgnored()
        {
            bool ok = true;

            if (_curCard == null)
                return ok;

            LanguageData dataLayer = new LanguageData();

            if (hasChanges())
            {
                ok = false;
                DialogResult result = MessageBox.Show("Do you want to save the changes?", "Save Changes?", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    //if the card has no data just get rid of it            
                    if (_curCard != null && isCardEmpty(this.writeDataToCard(_curCard), this.writeSoundDataToSoundClip(_curSoundClip)))
                    {
                        deleteCard(_curCard);
                        ok = true;
                    }
                    else
                    {
                        ok = SaveCard();
                    }
                }
                else if (result == DialogResult.No)
                {
                    //check to see if the old card is empty and delete if it is
                    dsLanguageData.CardRow cardBeforeChanges = dataLayer.daCard.GetDataByCardID(_curCard.ID)[0];
                    dsLanguageData.SoundClipRow soundClipBeforeChanges;
                    try
                    {
                        soundClipBeforeChanges = dataLayer.daSoundClip.GetDataByCardID(cardBeforeChanges.ID)[0];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        soundClipBeforeChanges = null;
                    }
                    if (isCardEmpty(cardBeforeChanges, soundClipBeforeChanges))
                    {
                        deleteCard(cardBeforeChanges);
                    }

                    ok = true;
                }
            }
            else
            {
                if (isCardEmpty(_curCard, _curSoundClip) && _curCard != null)
                {
                    deleteCard(_curCard);
                }
            }
            return ok;
        }

        void deleteCard(dsLanguageData.CardRow card)
        {
            LanguageData dataLayer = new LanguageData();
            dataLayer.DeleteCard(card.ID);
            ClearCard();
        }

        public void PlaySound()
        {
            _dictaphone.StartPlay();
        }
        public void StopSound()
        {
            _dictaphone.ClosePlayer();

            if (_dictaphone_state == DictaphoneState.Record)
                StopRecording();
        }

        public void StartRecording()
        {
            _dictaphone.StartRecord(true, "last_recorded.wav");
        }

        public void StopRecording()
        {
            _dictaphone.StopRecord();

            MemoryStream tmp = _dictaphone.WavStream as MemoryStream;
            byte[] buffer = tmp.GetBuffer();
           
            if (_curSoundClip == null)            
                _curSoundClip = new dsLanguageData.SoundClipDataTable().NewRow() as dsLanguageData.SoundClipRow;                            

            _curSoundClip.SoundClip = buffer;

            setSoundButton(true);
        }
        #endregion

        #region Private Methods        

        bool isCardEmpty(dsLanguageData.CardRow card, dsLanguageData.SoundClipRow soundClip)
        {
            bool empty = true;
            
            if (card == null)
                return empty;

            if (card.Answer.Trim().Length > 0)
                empty = false;
            if (card.Question.Trim().Length > 0)
                empty = false;
            if (card.Notes.Trim().Length > 0)
                empty = false;
            if (soundClip != null && soundClip.SoundClip.Length > 0)
                empty = false;

            return empty;
        }

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

        
        string getQuestionText()
        {
            if (chkBoxSwitchQuestionAnswer.Checked)
                return txtAnswer.Text.Trim();
            return txtQuestion.Text.Trim();
        }

        string getAnswerText()
        {
            if (chkBoxSwitchQuestionAnswer.Checked)
                return txtQuestion.Text.Trim();
            return txtAnswer.Text.Trim();
        } 

        bool hasChanges()
        {
            if(_curCard == null)
                return false;

            bool hasChanges = false;

            try
            {
                LanguageData dataLayer = new LanguageData();
                dsLanguageData.CardRow tmpCard = dataLayer.daCard.GetDataByCardID(_curCard.ID).Rows[0] as dsLanguageData.CardRow;
                dsLanguageData.SoundClipDataTable tmpDTSound = dataLayer.daSoundClip.GetDataByCardID(_curCard.ID);
                dsLanguageData.SoundClipRow tmpSoundClip = null;
                if (tmpDTSound.Rows.Count > 0)
                    tmpSoundClip = tmpDTSound[0];

                if (tmpCard.Question.Trim() != getQuestionText())
                    hasChanges = true;
                if (!_informationWasShown)
                    ShowInformation();
                if (tmpCard.Answer.Trim() != getAnswerText())
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

            }
            catch (IndexOutOfRangeException)
            {
                hasChanges = true;
            }
            catch (RowNotInTableException)
            {
                ClearCard();
                hasChanges = false;
            }

            return hasChanges;
        }

        void setSoundButton(bool canPlay)
        {
            if (canPlay)
            {
                btnSound.Enabled = true;
                btnSound.ImageIndex = 1;
                btnSound.Text = "Play Clip";

                btnClear.Enabled = true;
            }
            else
            {
                btnSound.Enabled = false;
                btnSound.ImageIndex = 0;
                btnSound.Text = "No Sound";

                btnClear.Enabled = false;
            }
        }
        #endregion

        #region Events

        public delegate void CardDataChangedEventHandler(object sender, CardChangedEventArgs e);
        public event CardDataChangedEventHandler CardDataChanged;

        public delegate void CardWasDeletedEventHandler(object sender, int cardID);
        public event CardWasDeletedEventHandler CardWasDeleted;

        public delegate void SwitchQuestionAnswerChangedEventHandler(object sender, SwitchQuestionAndAnswerChangedEventArgs e);
        public event SwitchQuestionAnswerChangedEventHandler SwitchQuestionAndAnswerChanged;

        public event EventHandler AddNewCard;

        private void chkBoxSwitchQuestionAnswer_CheckedChanged(object sender, EventArgs e)
        {
            if (_curCard != null)
            {
                if (_informationWasShown)
                {
                    string tmp = txtQuestion.Text;
                    txtQuestion.Text = txtAnswer.Text;
                    txtAnswer.Text = tmp;
                }
                else
                {
                    txtAnswer.Text = txtQuestion.Text;
                    txtQuestion.Text = getTextForQuestion(_curCard);
                    _informationWasShown = true;
                }

                ShowInformation();                
            }

            if (SwitchQuestionAndAnswerChanged != null)
                SwitchQuestionAndAnswerChanged(this, new SwitchQuestionAndAnswerChangedEventArgs(chkBoxSwitchQuestionAnswer.Checked));

        }

        public class SwitchQuestionAndAnswerChangedEventArgs : EventArgs
        {
            public bool SwitchQuestionAndAnswer;

            public SwitchQuestionAndAnswerChangedEventArgs(bool switchQuestionAndAnswer)
            {
                SwitchQuestionAndAnswer = switchQuestionAndAnswer;
            }
        }

        public class CardChangedEventArgs : EventArgs
        {
            public dsLanguageData.CardRow Card;
            public dsLanguageData.SoundClipRow SoundClip;

            public CardChangedEventArgs(dsLanguageData.CardRow card, dsLanguageData.SoundClipRow sound)
            {
                Card = card;
                SoundClip = sound;
            }
        }

        private void txtAnswer_Enter(object sender, EventArgs e)
        {
            ShowInformation();
        }

        private void btnSound_Click(object sender, EventArgs e)
        {
            PlaySound();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (_curSoundClip != null)
                _curSoundClip.SoundClip = new byte[0];

            setSoundButton(false);
        }

        private void btnAddNewCard_Click(object sender, EventArgs e)
        {
            if (AddNewCard != null)
                AddNewCard(this, new EventArgs());
        }

        private void btnDeleteCard_Click(object sender, EventArgs e)
        {
            DeleteCard();
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            SaveCard();
        }
        #endregion

        private void btnGetImage_Click(object sender, EventArgs e)
        {
            if (_curCard != null)
            {
                FindImageDialog f = new FindImageDialog(_curCard.Answer, 8);
                f.ImagePreviewSelected += new EventHandler(findImageDialog_ImagePreviewSelected);
                f.ShowDialog();
            }
        }

        void findImageDialog_ImagePreviewSelected(object sender, EventArgs e)
        {
            pictureBox.Image = ((ImagePreviewControl)sender).Image;
        }

        private void btnClearImage_Click(object sender, EventArgs e)
        {
            pictureBox.Image = null;
        }
        
    }
}
