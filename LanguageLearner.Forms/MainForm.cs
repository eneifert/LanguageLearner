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
        public MainForm()
        {
            InitializeComponent();

            collectionTreeControl.DataSelected += new CollectionTreeControl.DataSelectedEventHandler(collectionTreeControl_DataSelected);
            collectionTreeControl.LoadNodes();
        }

        void collectionTreeControl_DataSelected(CollectionTreeControl.DataSelectedEventArgs e)
        {
            cardListPlayList.LoadCardLists(e.CardListIDs);
        }

        private void cardListPlayList_CardSelected(CardListPlayList.CardSelectedEventArgs e)
        {
            if (!cardControl.LoadCard(e.Card))
                e.Success = false;
        }

        #region Add, Save, Delete Card

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            cardControl.SaveCard();
        }

        private void btnDeleteCard_Click(object sender, EventArgs e)
        {
            //Clear the card, Delete it and any references to it, Refresh the playlist
            cardControl.DeleteCard();
            cardListPlayList.RefreshCards();
        }

        private void btnAddNewCard_Click(object sender, EventArgs e)
        {
            //If list are selected see which lists they want to add the card to
            //Add the card to the dictionary
            //TODO what to do about selecting/showing the card?
        }
        #endregion

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (!cardControl.InformationWasShown)
                cardControl.ShowAnswer();
            else
                cardListPlayList.GetNextCard();
        }

    }
}