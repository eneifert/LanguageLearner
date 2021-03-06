using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Alvas.Forms;
using LangLearner.Data;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;

namespace LangLearner.Main
{
    public partial class MainForm : Form
    {

        delegate void DictaphoneCallaback();
        event DictaphoneCallaback _dictaphoneCallBack;
        
        LLSqlite _dataProcessor = new LLSqlite();
        dsLanguageDB.CardDataTable _dtCurCards = new dsLanguageDB.CardDataTable();
        dsLanguageDB.CardRow _curCardRow = null;
        dsLanguageDB.SoundFileRow _curSoundFileRow = null;
        string _dataPath = Application.StartupPath + "\\Data";

        /// <summary>
        /// Since the answer is not initially shown in the flash card this variable keeps track of when it was shown.
        /// This is useful for things like fast editing text and loading the next card. 
        /// </summary>
        bool _answerWasShown = false;
        
        Dictaphone _dictaphone = new Dictaphone();
        private const string _timeFormat = "Time in ms: {0}";

        Control _lastInputBoxWithFocus = null;
        Color _colorEdit = Color.LightYellow;

        public MainForm()
        {
            InitializeComponent();

            openLangFileDialog.InitialDirectory = Application.StartupPath;
            this.openSqliteFileDialog.InitialDirectory = Application.StartupPath + "\\Data";

            _dictaphone.ChangeState += new Dictaphone.ChangeStateEventHandler(dict_ChangeState);
            _dictaphone.ChangePosition += new Dictaphone.ChangePositionEventHandler(dict_ChangePosition);                                 
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new MainForm());
        }

        #region Events

        void checkDictaphoneCallBack()
        {
            if (_dictaphoneCallBack != null)
            {
                _dictaphoneCallBack();
                _dictaphoneCallBack = null;
            }
        }
        void dict_ChangeState(object sender, DictaphoneStateEventArgs e)
        {          
            switch (e.State)
            {
                case DictaphoneState.Initial:
                    lblTime.Text = string.Format(_timeFormat, 0);
                    setEnableForEverythingThatCanAffectSound(true);
                    checkDictaphoneCallBack();
                    break;
                case DictaphoneState.PausePlay:
                    throw new Exception("Please implement the dict_ChangeState handler");
                    break;
                case DictaphoneState.Play:
                    setEnableForEverythingThatCanAffectSound(false);
                    btnStop.Enabled = true;
                    btnPlayQuestion.Enabled = true;
                    btnShowAnswer.Enabled = true;
                    break;
                case DictaphoneState.PauseRecord:
                    throw new Exception("Please implement the dict_ChangeState handler");                    
                    break;
                case DictaphoneState.Record:
                    setEnableForEverythingThatCanAffectSound(false);                    
                    btnStopRec.Enabled = true;
                    break;
                default:
                    break;
            }
        }       
        void setEnableForEverythingThatCanAffectSound(bool enabled)
        {
            btnAddNewCard.Enabled = enabled;
            btnClearSoundFile.Enabled = enabled;
            btnImportSoundFile.Enabled = enabled;
            btnNewLngFile.Enabled = enabled;
            btnPlayQuestion.Enabled = enabled;
            btnRec.Enabled = enabled;
            btnSaveChanges.Enabled = enabled;
            btnStopRec.Enabled = enabled;
            btnShowAnswer.Enabled = enabled;
            btnStop.Enabled = enabled;
            btnDeleteCard.Enabled = enabled;
            btnDeleteCardList.Enabled = enabled;
            btnEditCardListName.Enabled = enabled;
        }
        void dict_ChangePosition(object sender, PositionEventArgs e)
        {
            lblTime.Text = string.Format(_timeFormat, e.Position);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _dictaphone.SoundLevelMeter = this.vum;
            
            loadCardLists();
        }
        void loadCardLists()
        {
            listFiles.Items.Clear();

            dsLanguageDB.CardListDataTable dtCardList = _dataProcessor.GetCardLists();
            foreach (dsLanguageDB.CardListRow clrow in dtCardList.Rows)
            {
                listFiles.Items.Add(clrow.Name, clrow.Guid.ToString());
            }
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {   
            _dictaphone.StartRecord(true, "LaguageExample.wav");
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            _dictaphone.StartPlay();
        }
        private void ctnStop_Click(object sender, EventArgs e)
        {
            _dictaphone.ClosePlayer();
        } 

        private void btnStopRecording_Click(object sender, EventArgs e)
        {
            _dictaphone.StopRecord();
        }

        private void btnClearSoundFile_Click(object sender, EventArgs e)
        {
            if (_curSoundFileRow != null && _curSoundFileRow.Guid != Guid.Empty.ToString())
            {
                if (automaticallySaveChangesToolStripMenuItem.Checked || MessageBox.Show("Delete current sound? Note you will note be able to undo this.", "Are you sure?", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                {
                    _dataProcessor.daSoundFile.DeleteByQuid(_curSoundFileRow.Guid);
                    _curSoundFileRow = null;
                }
            }
            _dictaphone.ClosePlayer();
            _dictaphone.WavStream = null;
        }

        private void btnSelectSoundFile_Click(object sender, EventArgs e)
        {
            if (openLangFileDialog.ShowDialog() == DialogResult.OK)
            {
                _dictaphone.WavStream = new FileStream(openLangFileDialog.FileName, FileMode.Open);
            }
        }

        void PlaySoundWithCallBack(DictaphoneCallaback callback)
        {
            _dictaphoneCallBack = callback;
            _dictaphone.StartPlay();
        }
        private void btnShowAnswer_Click(object sender, EventArgs e)
        {
            if (_curCardRow != null)
            {                
                if (_answerWasShown)
                {
                    if (playSoundBeforeShowingNextCardToolStripMenuItem.Checked)
                    {
                        PlaySoundWithCallBack(delegate(){ loadRandomCard(); });
                    }
                    else
                    {
                        _dictaphone.ClosePlayer();
                        loadRandomCard();
                    }
                }
                else
                {
                    if (playSoundBeforeAnswerToolStripMenuItem.Checked)
                    {
                        PlaySoundWithCallBack(showAnswer);
                    }
                    else
                    {
                        showAnswer();                        
                    }
                }
            }
        }

        private void listFiles_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            loadCards(getSelectedCards(false), true);
        }
           
        void loadCards(dsLanguageDB.CardDataTable dtCards, bool resetRange)
        {
            _dtCurCards = dtCards;

            if(resetRange)
                setRange(0, _dtCurCards.Count);

            if (_dtCurCards.Count > 0)
            {
                grpBoxPracticeCards.Enabled = true;
            }
            else
            {
                grpBoxPracticeCards.Enabled = false;
            }

            tryToHideDesiredCards();
            playList.LoadPlaylist(_dtCurCards);
            loadRandomCard();    
        }

        void tryToHideDesiredCards()
        {
            DoesCardMeetCondition hasQuestionAndSound = delegate(dsLanguageDB.CardRow card) { return card.HasQuestionAndSound(); };            

            //if they don't want the completed cards
            if (this.hideCompletedCardsToolStripMenuItem.Checked)
            {
                //Remove the completed cards
                removeRowsWhere(hasQuestionAndSound, false);
            }

            //if they don't want the unfinished cards           
            if (this.hideUnfinishedCardsToolStripMenuItem.Checked)
            {
                //Remove the unfinished cards
                removeRowsWhere(hasQuestionAndSound, true);
            }           
            
            showOnlyRowsInRange();            
        }      

        delegate bool DoesCardMeetCondition(dsLanguageDB.CardRow card);    
        private void removeRowsWhere(DoesCardMeetCondition cardCondition, bool negateCondition)
        {
            for (int i = 0; i < _dtCurCards.Count; i++)
            {
                dsLanguageDB.CardRow card = _dtCurCards[i] as dsLanguageDB.CardRow;
                
                bool remove = cardCondition(card);
                if (negateCondition)
                {
                    remove = !remove;
                }

                if (remove)
                {
                    _dtCurCards.RemoveCardRow(card);
                    i--;
                }
            }
        }

        /// <summary>
        /// Shows the rows in the the selected range. If there is problem all rows should be shown.
        /// </summary>
        private void showOnlyRowsInRange()
        {
            int low, high;
            if (int.TryParse(txtStart.Text, out low) && int.TryParse(txtEnd.Text, out high))
            {
                if (low >= 0 && low < _dtCurCards.Count && high > 0 && high > low && high <= _dtCurCards.Count)
                {
                    for (int i = 0; i < _dtCurCards.Rows.Count; i++)
                    {
                        if (i < low || i >= high)
                        {
                            _dtCurCards.Rows.RemoveAt(i);
                            i--;
                            low--;
                            high--;
                        }
                    }
                    return;
                }
            }
            setRange(0, _dtCurCards.Count);
        }
        void setRange(int start, int end)
        {
            txtStart.Text = start.ToString();
            txtEnd.Text = end.ToString();
        }

        private dsLanguageDB.CardDataTable getSelectedCards(bool onlyGetUnfinished)
        {
            dsLanguageDB.CardDataTable dtCards = new dsLanguageDB.CardDataTable();

            List<string> cardGuids = new List<string>();
            if (onlyGetUnfinished)
            {
                dtCards.Merge(_dataProcessor.daCard.GetUnfinishedCards());
            }
            else
            {
                //get a list of selected guids that need to be loaded
                foreach (ListViewItem item in listFiles.SelectedItems)
                {
                    if (this.hideEasyCardsToolStripMenuItem.Checked)
                    {
                        dtCards.Merge(_dataProcessor.daCard.GetDifficultCardsByListGuids(item.ImageKey, true));
                    }
                    else
                    {
                        dtCards.Merge(_dataProcessor.daCard.GetDataByListGuids(item.ImageKey));
                    }
                }
            }

            return dtCards;
        }      

        private void chkBoxShowQuestion_CheckedChanged(object sender, EventArgs e)
        {
            switchQuestionAndAnswer();
        }
        void switchQuestionAndAnswer()
        {
            txtQuestion.Text = getQuestionString();
            txtAnswer.Text = getAnswerString();
        }

        private bool isSingleCardListIsSelected()
        {
            if (listFiles.SelectedItems.Count != 1)
            {
                MessageBox.Show("Please select one list to add the card to");
                return false;
            }
            return true;
        }

        private void btnAddNewCard_Click(object sender, EventArgs e)
        {
            tryAddNewCard();
        }
        void tryAddNewCard()
        {
            //check to make sure the current card is saved
            if (!isSingleCardListIsSelected())
            {
                return;
            }
            if (changesHaveBeenSavedOrIgnored(MessageBoxButtons.YesNoCancel))
            {
                dsLanguageDB.CardRow newCard = _dtCurCards.MakeNewCardRow();
                newCard.ListGuid = listFiles.SelectedItems[0].ImageKey;
                newCard.Guid = Guid.NewGuid().ToString();

                _dtCurCards.AddCardRow(newCard);
                _curSoundFileRow = null;

                _answerWasShown = true;
                setCardEdit(_colorEdit);
            }
        }

        private bool changesHaveBeenSavedOrIgnored(MessageBoxButtons buttons)
        {                
            if (_curCardRow == null)
            {
                return true;
            }

            if (curCardHasChanges())
            {
                DialogResult proceed = DialogResult.None;
                if (automaticallySaveChangesToolStripMenuItem.Checked)
                {
                    proceed = DialogResult.Yes;
                }
                else
                {
                    proceed = MessageBox.Show("Save current changes?", "Save Changes?", buttons);
                }
                
                if (proceed == DialogResult.Yes)
                {
                    saveCurCard();
                    return true;
                }
                else if (proceed == DialogResult.No)
                {
                    dsLanguageDB.CardRow oldRow = _curCardRow;
                    setCardEdit(Color.White);
                    clearCard();

                    if (oldRow.RowState == DataRowState.Added)
                    {
                        _dtCurCards.RemoveCardRow(oldRow);
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            //if the row is a new row and has no changes just remove it
            else if (_curCardRow.RowState == DataRowState.Added)
            {
                if(_dtCurCards.Rows.Contains(_curCardRow))
                    _dtCurCards.RemoveCardRow(_curCardRow);
            }
            
            return true;              
        }
        private bool curCardHasChanges()
        {
            if (_curCardRow == null || _curCardRow.RowState == DataRowState.Detached)
            {
                return false;
            }

            if (!switchQuestionAndAnswerToolStripMenuItem.Checked)
            {
                if (_curCardRow.Answer != getAnswerText() && _answerWasShown)
                    return true;
                if (_curCardRow.Question != getQuestionText())
                    return true;
            }
            else
            {
                if (_curCardRow.Answer != getAnswerText())
                    return true;
                if (_curCardRow.Question != getQuestionText() && _answerWasShown)
                    return true;
            }
            if (_curCardRow.Example != txtExample.Text)
                return true;
            if (_curCardRow.IsEasy != chkBoxCardIsEasy.Checked)
                return true;
            if (_curCardRow.IsMarkedForReview != chkBoxMarkForReview.Checked)
                return true;
            if (_curCardRow.IsEasy != chkBoxCardIsEasy.Checked)
                return true;
            if (curSoundFileHasChanges())
                return true;

            return false;
        }
        bool curSoundFileHasChanges()
        {
            if (_curSoundFileRow != null)
            {
                if (_curCardRow.SoundFileGuid != _curSoundFileRow.Guid)
                    return true;
                if (_curSoundFileRow.SoundFile.Length != streamToString(_dictaphone.WavStream).Length)
                    return true;
            }
            return false;
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            saveCurCard();
        }

        private void btnEditAnswer_Click(object sender, EventArgs e)
        {           
        }

        private void btnEditQuestion_Click(object sender, EventArgs e)
        {
            txtQuestion.BackColor = _colorEdit;
        }
        #endregion
 
        private void showAnswer()
        {
            txtAnswer.Text = getAnswerString();
            _answerWasShown = true;

            if (playSoundWhenAnswerIsShownToolStripMenuItem.Checked)
            {
                _dictaphone.StartPlay();
            }
        }

        void setCardEdit(Color c)
        {           
            txtAnswer.BackColor = c;
            txtQuestion.BackColor = c;
            txtExample.BackColor = c;        
        }

        bool loadRandomCard()
        {
            //make sure any callbacks have been called
            checkDictaphoneCallBack();

            if (changesHaveBeenSavedOrIgnored(MessageBoxButtons.YesNoCancel))
            {
                dsLanguageDB.CardRow lastCard = _curCardRow;
                clearCard();

                if (_dtCurCards.Rows.Count > 0)
                {
                    Random rand = new Random();
                    dsLanguageDB.CardRow cardToLoad = null;

                    //if you can help it don't show the same row twice in a row                     
                    if (_dtCurCards.Rows.Count > 1)
                    {
                        while (cardToLoad == null || lastCard == cardToLoad)
                        {
                            cardToLoad = (dsLanguageDB.CardRow)_dtCurCards.Rows[rand.Next(_dtCurCards.Rows.Count)];
                        }
                    }
                    else
                    {
                        cardToLoad = _dtCurCards[0];
                    }

                    playList.SelectCard(cardToLoad);
                }
                else
                {
                    grpBoxPracticeCards.Enabled = false;
                }
                return true;
            }
            return false;
        }

        private void loadCard(dsLanguageDB.CardRow card)
        {
            if (!grpBoxPracticeCards.Enabled)
            {
                grpBoxPracticeCards.Enabled = true;
            }

            txtAnswer.Text = string.Empty;
            _answerWasShown = false;

            //if the current card is not already loaded
            if (_curCardRow != card)
            {
                _curCardRow = card;                

                loadCurSoundFile();

                if (playSoundOnLoadToolStripMenuItem.Checked)
                {
                    _dictaphone.StartPlay();
                }
                else if (playSoundBeforeCardLoadsToolStripMenuItem.Checked)
                {
                    PlaySoundWithCallBack(displayCard);
                    return;
                }

                displayCard();             
            }
        }
        void displayCard()
        {
            txtQuestion.Text = getQuestionString();

            //if the user doesn't want to hide the answer or their is no answer to show
            if (showAnswerOnLoadToolStripMenuItem.Checked || getAnswerString().Trim().Length < 1)
            {
                txtAnswer.Text = getAnswerString();
                _answerWasShown = true;
            }

            txtExample.Text = _curCardRow.Example;
            chkBoxCardIsEasy.Checked = _curCardRow.IsEasy;
            chkBoxMarkForReview.Checked = _curCardRow.IsMarkedForReview;
        }
       

        void clearCard()
        {
            _curCardRow = null;
            _curSoundFileRow = null;
            txtAnswer.Text = string.Empty;
            txtExample.Text = string.Empty;
            txtQuestion.Text = string.Empty;
            chkBoxCardIsEasy.Checked = false;
            _dictaphone.WavStream = null;

            setCardEdit(Color.White);
        }

        private void loadCurSoundFile()
        {
            _curSoundFileRow = _dataProcessor.GetSoundFile(_curCardRow.SoundFileGuid);                        

            if (_curSoundFileRow != null && _curSoundFileRow.SoundFile.Length > 1)
            {
                _dictaphone.WavStream = streamFromString(_curSoundFileRow.SoundFile);                
            }
        }

        private void saveCurCard()
        {
            string reason = string.Empty;
            if (_curCardRow != null)
            {
                _curCardRow.BeginEdit();

                if (curCardHasChanges())
                {
                    _curCardRow.LastModified = DateTime.Now;
                }

                if (_answerWasShown)
                    _curCardRow.Answer = getAnswerText();
                
                _curCardRow.Question = getQuestionText();
                _curCardRow.Example = txtExample.Text;
                _curCardRow.IsEasy = chkBoxCardIsEasy.Checked;
                _curCardRow.IsMarkedForReview = chkBoxMarkForReview.Checked;

                _curCardRow.EndEdit();

                saveCurSoundFile();

                if (_curCardRow.RowState == DataRowState.Added)
                {
                    _dataProcessor.InsertCard(_curCardRow);
                    _curCardRow.AcceptChanges();
                }
                else if (_curCardRow.RowState == DataRowState.Modified)
                {
                    int i = _dataProcessor.UpdateCard(_curCardRow);
                    _curCardRow.AcceptChanges();
                }

                setCardEdit(Color.White);
            }           
        }

        void saveCurSoundFile()
        {
            //TODO can you save a cleared wav file?               
            if (_dictaphone.WavStream != null)
            {
                if (_curSoundFileRow == null)
                {
                    _curSoundFileRow = new dsLanguageDB.SoundFileDataTable().MakeNewSoundFileRow();
                    _curSoundFileRow.Guid = Guid.NewGuid().ToString();
                }

                if (curSoundFileHasChanges())
                {
                    _curSoundFileRow.DateCreated = DateTime.Now;
                    _curSoundFileRow.SoundFile = streamToString(_dictaphone.WavStream);
                }

                if (_curSoundFileRow.RowState == DataRowState.Detached)
                {
                    _dataProcessor.InsertSoundFile(_curSoundFileRow);
                    _curCardRow.SoundFileGuid = _curSoundFileRow.Guid;
                    _curSoundFileRow = _dataProcessor.GetSoundFile(_curCardRow.SoundFileGuid); 
                }
                else if (_curSoundFileRow.RowState == DataRowState.Modified)
                {
                    _dataProcessor.daSoundFile.Update(_curSoundFileRow);
                }
            }
        }

        private MemoryStream streamFromString(string s)
        {
            MemoryStream ms = new MemoryStream();
            string tmp = string.Empty;
            int i = 0;
            while(i < s.Length)
            {
                tmp = s.Substring(i, 3);
                ms.WriteByte(byte.Parse(tmp));
                i += 3;
            }

            return ms;
        }

        private string streamToString(Stream s)
        {
            StringBuilder sb = new StringBuilder();
            s.Seek(0, SeekOrigin.Begin);
            int tmpb = s.ReadByte();
            while (tmpb != -1)
            {
                if(tmpb < 10)
                {
                    sb.Append("00");
                }
                else if (tmpb < 100)
                {
                    sb.Append("0");
                }
                
                sb.Append(tmpb.ToString());
                tmpb = s.ReadByte();
            }

            return sb.ToString();
        }
        
        private string getQuestionString()
        {            
            if (!switchQuestionAndAnswerToolStripMenuItem.Checked)
                return _curCardRow.Question;
            return _curCardRow.Answer;
        }
        private string getAnswerString()
        {
            if (!switchQuestionAndAnswerToolStripMenuItem.Checked)
                return _curCardRow.Answer;
            return _curCardRow.Question;
        }
        private string getQuestionText()
        {
            if (!switchQuestionAndAnswerToolStripMenuItem.Checked)
                return txtQuestion.Text;
            return txtAnswer.Text;
        }

        private string getAnswerText()
        {
            if (!switchQuestionAndAnswerToolStripMenuItem.Checked)
                return txtAnswer.Text;
            return txtQuestion.Text;
        }

        private void listFiles_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            string tmp = string.Empty;
        }

        private void btnEditCardListName_Click(object sender, EventArgs e)
        {
            if (isSingleCardListIsSelected())
            {
                ListViewItem selected = listFiles.SelectedItems[0];
                GetStringDialog getStringDialog = new GetStringDialog(selected.Text, "Add");
                getStringDialog.ShowDialog();
                if (getStringDialog.DialogResult == DialogResult.OK)
                {
                    if (getStringDialog.TextValue != selected.Text)
                    {
                        int i = _dataProcessor.daCardList.UpdateName(getStringDialog.TextValue, DateTime.Now, selected.ImageKey);
                        if (i > 0)
                            selected.Text = getStringDialog.TextValue;
                    }
                }
            }
        }

        private void btnNewLngFile_Click(object sender, EventArgs e)
        {
            GetStringDialog getStringDialog = new GetStringDialog(string.Empty, "Add");
            getStringDialog.ShowDialog();
            if (getStringDialog.DialogResult == DialogResult.OK)
            {
                string g = Guid.NewGuid().ToString();
                if (_dataProcessor.InsertCardList(g, getStringDialog.TextValue) > 0)
                {
                    listFiles.Items.Add(getStringDialog.TextValue, g);
                }

            }
        }

        private void btnDeleteCardList_Click(object sender, EventArgs e)
        {
            if (listFiles.SelectedItems.Count == 1)
            {
                DialogResult d = MessageBox.Show("Delete this list and all its associated cards?", "Delete Card List?", MessageBoxButtons.YesNoCancel);
                if (d == DialogResult.Yes)
                {
                    string guid = listFiles.SelectedItems[0].ImageKey;
                    _dataProcessor.DeleteCardList(guid);
                    loadCardLists();
                    clearCard();
                    playList.Items.Clear();
                }
            }
        }

        private void btnDeleteCard_Click(object sender, EventArgs e)
        {
            DeleteCard();
        }
        private void DeleteCard()
        {
            if (_curCardRow != null && _curCardRow.Guid != Guid.Empty.ToString())
            {

                DialogResult d = DialogResult.None;
                if (automaticallySaveChangesToolStripMenuItem.Checked)
                    d = DialogResult.Yes;
                else
                    d = MessageBox.Show("Delete this card?", "Delete Card?", MessageBoxButtons.YesNoCancel);
                
                if (d == DialogResult.Yes)
                {
                    _dataProcessor.DeleteCard(_curCardRow.Guid);
                    _dtCurCards.RemoveCardRow(_curCardRow);
                    clearCard();
                    loadRandomCard();
                }
            }
        }

        private void txtColorBox_Enter(object sender, EventArgs e)
        {
            TextBox box = (TextBox)sender;

            if (_curCardRow != null)
            {
                box.BackColor = _colorEdit;

                if (box == txtAnswer && !_answerWasShown)
                {
                    showAnswer();
                }                
            }
        }

        private void btnExportDB_Click(object sender, EventArgs e)
        {

            string name = "Data " + getCurTimeString();
            openSqliteFileDialog.InitialDirectory = Application.StartupPath + "\\Backup\\";
            openSqliteFileDialog.FileName = name;

            if (openSqliteFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.Copy(Application.StartupPath + "\\LanguageDB.sqlite", openSqliteFileDialog.FileName, true);
            }
        }

        string getCurTimeString()
        {
            string d = DateTime.Now.ToString();
            d = d.Replace('/', '-');
            return d.Replace(':', '_');          
        }

        private void btnImportDB_Click(object sender, EventArgs e)
        {
            openSqliteFileDialog.FileName = string.Empty;
            openSqliteFileDialog.InitialDirectory = Application.StartupPath + "\\Data\\";
            if (openSqliteFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.Copy(Application.StartupPath + "\\LanguageDB.sqlite", Application.StartupPath + "\\Backup\\Data " + getCurTimeString() + ".sqlite", true);                
            
                _dataProcessor.MergeWith(openSqliteFileDialog.FileName);
                loadCardLists();
            }
        }

        private void hideEasyCardsToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            clearCard();
            loadCards(getSelectedCards(false), true);
        }

        private void txtBox_Leave(object sender, EventArgs e)
        {
            _lastInputBoxWithFocus = (Control)sender;
        }

        private void btnKeyboard_Click(object sender, EventArgs e)
        {
            if (_lastInputBoxWithFocus != null)
            {
                Keys keys = MainForm.ModifierKeys;
                
                string text = ((Button)sender).Text;
                if (keys == Keys.Shift || keys == Keys.CapsLock)
                {
                    text = ((Button)sender).Tag.ToString();
                }

                _lastInputBoxWithFocus.Focus();
                SendKeys.Send(text);
            }
        }        

        private void txtFunction_KeyDown(object sender, KeyEventArgs e)
        {
            _lastInputBoxWithFocus = (Control)sender;

            int key = e.KeyValue;
            int f1 = 112;
            int f12 = 123;

            if (key >= f1 && key <= f12)
            {
                int index = key - f1;
                if (index < panelKeyboard.Controls.Count)
                {
                    Button b = getKeyboardButton(index);
                    if (b != null)
                    {
                        btnKeyboard_Click(b, new EventArgs());
                    }
                }
                else
                {
                    e.Handled = true;
                }
            }
            if (!e.Handled)
            {
                checkQuickKeyPressed(sender, e);
            }
        }
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            checkQuickKeyPressed(sender, e);
        }

        private void checkQuickKeyPressed(object sender, KeyEventArgs e)
        {            
            if (e.KeyCode == Keys.Enter)
            {
                if (e.Shift)
                {
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    saveCurCard();
                }

            }
            else if (e.KeyCode == Keys.OemPeriod)
            {
                if (e.Shift)
                {
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    tryAddNewCard();                                      
                }
            }
        }
        Button getKeyboardButton(int index)
        {
            foreach (Button b in panelKeyboard.Controls)
            {
                if (b.Name.EndsWith(index.ToString()))
                {
                    return b;
                }
            }
            return null;
        }

        private void playList_NowPlayingIsChanging(object sender, NowPlayingEventArgs e)
        {
            if (!changesHaveBeenSavedOrIgnored(MessageBoxButtons.YesNo))
                e.AllowChange = false;
        }

        private void playList_NowPlayingChanged(object sender, EventArgs e)
        {
            //Clear the current card and load the new one if possible
            clearCard();
            if (playList.NowPlaying != null)
            {
                dsLanguageDB.CardRow card = _dtCurCards.FindByGuid(playList.NowPlaying.ImageKey);
                if (card != null)
                {
                    loadCard(card);
                }
            }
            
        }
        private void btnRefreshPlaylist_Click(object sender, EventArgs e)
        {
            loadCards(getSelectedCards(false), false);
            //playList.RefreshPlaylist();
        }

        #region Tool Strip Items      

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void readMeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO Read Me
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO About
        }

        private void playList_DeleteItemRequest(object sender, StringEventArgs e)
        {
            DeleteCard();
        }                

        private void chkBoxSwithQuestionAndAnser_CheckedChanged(object sender, EventArgs e)
        {
            switchQuestionAndAnswerToolStripMenuItem.Checked = this.chkBoxSwitchQuestionAndAnser.Checked;
        }

        private void switchQuestionAndAnswerToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            txtQuestion.Text = getQuestionString();
            txtAnswer.Text = getAnswerString();
            _answerWasShown = true;
            playList.UseQuestionText = !switchQuestionAndAnswerToolStripMenuItem.Checked;
        }  

        private void filterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadCards(getSelectedCards(false), false);
        }
        private void loadAllCardsWithoutQuestionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadCards(_dataProcessor.daCard.GetUnfinishedCards(), true);
        }
        private void getAllCardsToReviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadCards(_dataProcessor.daCard.GetByMarkedForReview(true), true);           
        }
        #endregion                                      
    }

    [Serializable]
    public class WAVFile 
    {
        public Stream WAVcontents;

        public WAVFile(Stream wav)
        {
            WAVcontents = wav;            
        }
    }

}
