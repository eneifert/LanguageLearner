using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LanguageLearner.Data;

namespace LanguageLearner.UI
{
    /// <summary>
    /// Main form that displays the custom controls, passes off events, and handles other miscellaneous operations.
    /// </summary>
    public partial class MainForm : Form
    {
        ChooseCardListsDialog _chooseCardListsDialog;

        public MainForm()
        {            

            InitializeComponent();

            collectionTreeControl.CardsSelected += new CollectionTreeControl.CardsSelectedEventHandler(collectionTreeControl_CardsSelected);
            collectionTreeControl.ReLoadNodes();

            recentPlaylists1.RefreshRecentPlaylists();
            cardListPlayList.PlayListChanged += new CardListPlayList.PlayListChangedEventHandler(cardListPlayList_PlayListChanged);
        }     

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!cardControl.ChangesHaveBeenSavedOrIgnored())
                e.Cancel = true;
        }

        #region Dictionary/CardLists Tree Events

        void collectionTreeControl_CardsSelected(CollectionTreeControl.CardsSelectedEventArgs e)
        {
            if (cardControl.ChangesHaveBeenSavedOrIgnored())
            {                
                cardControl.ClearCard();
                cardListPlayList.AddOrLoadCards(e.Cards, e.SelectionDescription, e.CardListIDs, AddOrLoad.Load);               
            }
        }       
        #endregion

        #region CardListPlaylist Events

        private void cardListPlayList_CardSelected(CardListPlayList.CardSelectedEventArgs e)
        {
            e.CardWasSelected = cardControl.LoadCard(e.Card);                
        }

        void cardListPlayList_PlayListChanged(CardListPlayList.PlayListChangedEventArgs e)
        {
            switch(e.Action)
            {
                case PlaylistAction.Loaded:
                    recentPlaylists1.AddPlaylist(e.CardIDs, e.Description);
                    break;
                case PlaylistAction.Changed:
                    recentPlaylists1.UpdateCurrentPlaylist(e.CardIDs, e.Description);
                    break;
                case PlaylistAction.Deleted:
                    cardControl.ClearCard();
                    recentPlaylists1.RemoveCurrentPlaylist();
                    break;
                case PlaylistAction.Cleared:
                    cardControl.ClearCard();
                    break;
            }                                            
        }
        
        #endregion

        #region Play, Show/Next, Rec, Stop, Prev, Next buttons

        private void btnPlay_Click(object sender, EventArgs e)
        {
            cardControl.PlaySound();
        }

        private void btnShowOrGetNext_Click(object sender, EventArgs e)
        {
            showOrGetNext();
        }

        void showOrGetNext()
        {
            if (!cardControl.InformationHasBeenDisplayed)
                cardControl.ShowInformation();
            else if (cardControl.ChangesHaveBeenSavedOrIgnored())
                cardListPlayList.SelectRandomCard();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            cardControl.StopSound();
        }

        private void btnRec_Click(object sender, EventArgs e)
        {
            cardControl.StartRecording();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            cardListPlayList.SelectPreviousCard();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            cardListPlayList.SelectNextCard();
        }
        #endregion                               

        #region CardControl events

        private void cardControl_SwitchQuestionAndAnswerChanged(object sender, CardControl.SwitchQuestionAndAnswerChangedEventArgs e)
        {
            cardListPlayList.SwitchQuestionAndAnswerText = e.SwitchQuestionAndAnswer;
        }

        private void cardControl_CardDataChanged(object sender, CardControl.CardChangedEventArgs e)
        {
            if (cardListPlayList != null)
                cardListPlayList.UpdateCard(e.Card);
        }

        private void cardControl_CardWasDeleted(object sender, int cardID)
        {
            if (cardListPlayList != null)
                cardListPlayList.RemoveCardFromTheList(cardID);
        }

        private void cardControl_AddNewCard(object sender, EventArgs e)
        {
            addNewCard();
        }

        void addNewCard()
        {
            //check to see if the card should maybe be added to a CardList                
            if (cardListPlayList.CardListIDs.Count > 0)
            {
                //If the user needs to select which cardLists to add it to
                if (_chooseCardListsDialog == null || _chooseCardListsDialog.Description != cardListPlayList.Description || !_chooseCardListsDialog.RememberSelection)
                {
                    _chooseCardListsDialog = new ChooseCardListsDialog(cardListPlayList.CardListIDs, cardListPlayList.Description);
                    _chooseCardListsDialog.ShowDialog();

                    if (_chooseCardListsDialog.DialogResult != DialogResult.OK)
                    {
                        _chooseCardListsDialog = null;
                        return;
                    }                    
                }              
            }

            //Create the card
            LanguageData dataLayer = new LanguageData();
            dsLanguageData.CardRow newCard = new dsLanguageData.CardDataTable().MakeNewCardRow();
            int tmp = dataLayer.InsertOrUpdateCard(newCard);

            //Add the card to the selected CardLists
            if (_chooseCardListsDialog != null)
            {
                foreach (int id in _chooseCardListsDialog.SelectedCardListIDs)
                {
                    int i = dataLayer.InsertCardListDataItem(id, newCard.ID);
                }
            }

            //Load the card and refresh the lists
            cardListPlayList.RefreshCards();
            cardControl.LoadCard(newCard);

            cardListPlayList.AddCard(newCard);
        }

        #endregion

        #region ToolStripeMenuItem Events

        private void getImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetImages gI = new GetImages();
            gI.ShowDialog(this);
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportFromDBForm import = new ImportFromDBForm();
            import.ShowDialog();
        }
        #endregion

        #region Shortcut Keys

        /// <summary>
        /// Handles all the shorcut keys for the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.A:
                        addNewCard();
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.D:
                        if (cardControl != null)
                            cardControl.DeleteCard();
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.S:
                        if (cardControl != null)
                            cardControl.SaveCard();
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.P:
                        if (cardControl != null)
                            cardControl.PlaySound();
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.N:
                        showOrGetNext();
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.R:
                        if (cardControl != null)
                            cardControl.StartRecording();
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.T:
                        if (cardControl != null)
                            cardControl.StopSound();
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.V:
                        cardListPlayList.SelectPreviousCard();
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.X:
                        cardListPlayList.SelectNextCard();
                        e.SuppressKeyPress = true;
                        break;
                }                
                
            }
        }

        #endregion
    }
}