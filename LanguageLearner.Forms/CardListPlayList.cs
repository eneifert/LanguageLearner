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
    /// List that shows a group of Cards to the user and allows them to select a specific card.
    /// </summary>
    public partial class CardListPlayList : UserControl
    {
        dsLanguageData.CardDataTable _cards;
        public bool SwitchQuestionAndAnswer = false;
        public List<int> SelectedCardListIDs;
        int _selectedCardID = 0;

        #region Properties

        /// <summary>
        /// Gets or sets the cards that are displayed
        /// </summary>
        public dsLanguageData.CardDataTable Cards
        {
            get { return _cards; }
            set
            {
                _cards = value;

                RefreshCards();
            }
        }
        #endregion

        public CardListPlayList()
        {
            InitializeComponent();
        }

        #region Public Methods
        /// <summary>
        /// Selects a card randomly and then fires the CardSelected event.
        /// </summary>
        public void GetNextCard()
        {

        }

        /// <summary>
        /// Loads the cards from a list of CardIDs
        /// </summary>
        /// <param name="cardListIDs"></param>
        public void LoadCardLists(List<int> cardListIDs)
        {
            SelectedCardListIDs = cardListIDs;

            RefreshCards();
        }

        public void RefreshCards()
        {
            listView.Items.Clear();
            _selectedCardID = 0;

            if (SelectedCardListIDs == null)
                return;

            LanguageData dataLayer = new LanguageData();
            _cards = dataLayer.GetCardsInCardLists(SelectedCardListIDs);

            if (_cards != null)
            {
                foreach (dsLanguageData.CardRow row in Cards)
                {
                    listView.Items.Add(new ListViewItem(getDisplayText(row), row.ID));
                }
            }
            listView.Refresh();
        }
        #endregion

        #region Private Methods

        string getDisplayText(dsLanguageData.CardRow row)
        {
            if (SwitchQuestionAndAnswer)
                return row.Answer;
            return row.Question;
        }
        #endregion

        /// <summary>
        /// Handles how a card is selected, and fires the CardSelected Event
        /// </summary>
        /// <param name="e"></param>
        #region Card Select Event

        public delegate void CardSelectedEventHandler(CardSelectedEventArgs e);

        /// <summary>
        /// Event that is fired when a card is selected. If the CardSelectedEventArgs.Success field is not marked false then t
        /// </summary>
        public event CardSelectedEventHandler CardSelected;

        private void listView_DoubleClick(object sender, EventArgs e)
        {
            onCardSelected();
        }
        private void listView_KeyPress(object sender, KeyPressEventArgs e)
        {
            onCardSelected();
        } 

        void onCardSelected()
        {
            if (listView.SelectedItems.Count != 1)
                return;

            dsLanguageData.CardRow c = Cards.Select(string.Format("ID = {0}", listView.SelectedItems[0].ImageIndex))[0] as dsLanguageData.CardRow;
            CardSelectedEventArgs e = new CardSelectedEventArgs(c, SelectedCardListIDs);

            if (CardSelected != null)
                CardSelected(e);

            if (e.Success)
                _selectedCardID = c.ID;
        }

        public class CardSelectedEventArgs : EventArgs
        {
            public dsLanguageData.CardRow Card;
            public List<int> CardListIDs;
            public bool Success = true;

            public CardSelectedEventArgs(dsLanguageData.CardRow card, List<int> cardListIDs)
            {
                Card = card;
                CardListIDs = cardListIDs;
            }
        }        
        #endregion        
    }
}