using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using LanguageLearner.Data;
using System.IO;


namespace LanguageLearner.UI
{
    /// <summary>
    /// List that shows a group of Cards to the user and allows them to select a specific card.
    /// </summary>
    public partial class CardListPlayList : UserControl
    {
        dsLanguageData.CardDataTable _cards = new dsLanguageData.CardDataTable();
        List<int> _cardListIDs = new List<int>();

        string _playlistDescription = string.Empty;
        int _lastSelectedID = -1;

        bool _switchQuestionAndAnswerText = false;
        bool hideFilter = false;
        string headingText = "PlayList";
        
        #region Properties

        public string Description
        {
            get { return _playlistDescription; }
            set { _playlistDescription = value; }
        }

        [Browsable(true)]
        public bool HideFilter
        {
            get { return hideFilter; }
            set
            {
                hideFilter = value;
                if (hideFilter)
                    tableLayoutPanel.RowStyles[1].Height = 0;
                else
                    tableLayoutPanel.RowStyles[1].Height = 27;

            }
        }

        [Browsable(true)]
        public string HeadingText
        {
            get { return headingText; }
            set
            {
                headingText = value;
                btnHeading.Text = value;
            }
        }

        /// <summary>
        /// Gets or Sets whether the question and answer text is switched and refreshes the card list.
        /// </summary>
        public bool SwitchQuestionAndAnswerText
        {
            get { return _switchQuestionAndAnswerText; }
            set
            {
                if (value != _switchQuestionAndAnswerText)
                {
                    _switchQuestionAndAnswerText = value;
                    RefreshCards(false);
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the cards that are displayed
        /// </summary>
        public dsLanguageData.CardDataTable Cards
        {
            get 
            {
                if (_cards == null)
                    _cards = new dsLanguageData.CardDataTable();
                return _cards; 
            }
            set
            {                
                _cards = value;

                if (_cards != null && _cards.Rows.Count > 0)
                {
                    onPlayListChanged(PlaylistAction.Loaded);
                }
                else
                {
                    onPlayListChanged(PlaylistAction.Cleared);
                }

                RefreshCards();
            }
        }

        public List<int> CardListIDs
        {
            get { return _cardListIDs; }
            set { _cardListIDs = value; }
        }
    
        #endregion

        public CardListPlayList()
        {
            InitializeComponent();
        }

        #region Public Methods

        /// <summary>
        /// Clears the cards in the list
        /// </summary>
        public void ClearCards()
        {
            bool wasEmpty = Cards.Rows.Count < 1;

            Description = string.Empty;
            _cards = new dsLanguageData.CardDataTable();
            _cardListIDs = new List<int>();

            btnHeading.Text = headingText;

            listView.Items.Clear();
            _lastSelectedID = -1;

            if(!wasEmpty)
                onPlayListChanged(PlaylistAction.Cleared);
        }                 
     
        /// <summary>
        /// Selects a card at random and then fires the CardSelected event
        /// </summary>
        public void SelectRandomCard()
        {
            if (listView.Items.Count < 1)
                return;

            Random r = new Random();
            int randIndex = r.Next(listView.Items.Count - 1);

            if (listView.Items.Count > 1)
            {
                while (_lastSelectedID > -1 && ((IDListViewItem)listView.Items[randIndex]).ID == _lastSelectedID)
                    randIndex = r.Next(listView.Items.Count - 1);
            }

            foreach (ListViewItem item in listView.SelectedItems)
                item.Selected = false;

            listView.Items[randIndex].Selected = true;
            onCardSelected();            
        }

        /// <summary>
        /// Selects the next card and will wrap around if it comes to the end
        /// </summary>
        public void SelectNextCard()
        {
            moveSelectedIndex(1);
        }

        /// <summary>
        /// Selects the previous card and will wrap around if it comes to the end
        /// </summary>
        public void SelectPreviousCard()
        {
            moveSelectedIndex(-1);
        }          
      
        /// <summary>
        /// Refreshes all the cards from the DB
        /// </summary>
        public void RefreshCards()
        {
            RefreshCards(true);
        }

        /// <summary>
        /// Refreshes all the loaded cards without getting the from the DB
        /// </summary>
        /// <param name="fromDB"></param>
        public void RefreshCards(bool fromDB)
        {
            listView.Items.Clear();

            if (fromDB && Cards.Rows.Count > 0)
            {
                try
                {
                    LanguageData dataLayer = new LanguageData();
                    _cards = dataLayer.GetCardsByIDs(_cards.GetIDList());
                }
                catch (FileNotFoundException e)
                {
                    //TODO send a message here that the main form will pick up and display on the bottom
                }
            }

            foreach (dsLanguageData.CardRow row in Cards)
            {
                listView.Items.Add(new IDListViewItem(row.ID, getDisplayText(row)));
            }

            listView.Refresh();
        }

        /// <summary>
        /// Updates a card that has been changed
        /// </summary>
        /// <param name="card"></param>
        public void UpdateCard(dsLanguageData.CardRow card)
        {
            for (int i = 0; i < Cards.Count; i++)
            {
                if (Cards[i].ID == card.ID)
                    ((dsLanguageData.CardRow)Cards.Rows[i]).ItemArray = card.ItemArray;
            }

            RefreshCards(false);
        }

        /// <summary>
        /// Removes a card from the list of loaded cards
        /// </summary>
        /// <param name="cardID"></param>
        public void RemoveCardFromTheList(int cardID)
        {
            bool changed = false;

            if (Cards != null)
            {
                for (int i = 0; i < Cards.Count; i++)
                {
                    if (Cards[i].ID == cardID)
                    {
                       Cards.Rows.Remove(Cards[i]);
                       changed = true;
                    }
                }
                
                if (changed)
                {
                    if (Cards != null && Cards.Rows.Count > 0)                    
                        onPlayListChanged(PlaylistAction.Changed);                    
                    else                    
                        onPlayListChanged(PlaylistAction.Deleted);                                      
                }

                RefreshCards(false);
            }
        }

        /// <summary>
        /// Removes cards with the specified answerText.
        /// </summary>
        /// <param name="answerText"></param>
        public void RemoveCardsWhereAnswerIs(string answerText )
        {
            bool changed = false;

            if (Cards != null)
            {
                for (int i = 0; i < Cards.Count; i++)
                {
                    if (Cards[i].Answer == answerText)
                    {
                        Cards.Rows.Remove(Cards[i]);
                        changed = true;
                    }
                }

                if (changed)
                {
                    if (Cards != null && Cards.Rows.Count > 0)
                        onPlayListChanged(PlaylistAction.Changed);
                    else
                        onPlayListChanged(PlaylistAction.Deleted);
                }

                RefreshCards(false);
            }
        }

        /// <summary>
        /// Adds a card to the current list. If no list is selected then a new custom list is created.
        /// </summary>
        /// <param name="card"></param>
        public void AddCard(dsLanguageData.CardRow card)
        {
            Cards.CloneAndAddRow(card);
            
            if (Cards.Rows.Count < 2)
            {
                Description = "Custom Playlist";
                onPlayListChanged(PlaylistAction.Loaded);
            }
            else
            {
                onPlayListChanged(PlaylistAction.Changed);
            }
        }

        #endregion

        #region Private Methods

        string getDisplayText(dsLanguageData.CardRow row)
        {
            string text = string.Empty;

            if (_switchQuestionAndAnswerText)
            {
                text = row.Answer;
                if (text.Trim().Length < 1)
                    text = row.Question;
            }
            else
            {
                text = row.Question;
                if (text.Trim().Length < 1)
                    text = row.Answer;
            }
            return text;
        }

        private void moveSelectedIndex(int difference)
        {
            foreach (ListViewItem item in listView.SelectedItems)
                item.Selected = false;

            int index = getListIndex(_lastSelectedID);

            if (listView.Items.Count > 0)
            {
                if (index < 0 && listView.Items.Count > 0)
                    listView.Items[0].Selected = true;
                else
                {
                    index += difference;

                    if (index < 0)
                        listView.Items[listView.Items.Count - 1].Selected = true;
                    else if (index >= listView.Items.Count)
                        listView.Items[0].Selected = true;
                    else
                        listView.Items[index].Selected = true;
                }

                onCardSelected();
            }
        }

        int getListIndex(int cardID)
        {
            foreach (IDListViewItem item in listView.Items)
            {
                if (item.ID == cardID)
                    return item.Index;
            }

            return -1;
        }
        #endregion

        /// <summary>
        /// Public Events that other controls can subcribed to
        /// </summary>
        /// <param name="e"></param>
        #region CardListPlayList Selected/Changed Events
        
        public delegate void PlayListChangedEventHandler(PlayListChangedEventArgs e);
        /// <summary>
        /// Event that is triggered when a Playlist has been loaded, changed, or cleared
        /// </summary>
        public event PlayListChangedEventHandler PlayListChanged;

        public class PlayListChangedEventArgs : EventArgs
        {
            public List<int> CardIDs;
            public string Description;
            public PlaylistAction Action;
        
            public PlayListChangedEventArgs(List<int> cardIDs, string description, PlaylistAction action)
            {
                CardIDs = cardIDs;
                Description = description;
                Action = action;
            }
        }

        void onPlayListChanged(PlaylistAction action)
        {
            if (PlayListChanged != null)
                PlayListChanged(new PlayListChangedEventArgs(Cards.GetIDList(), Description, action));

        }

        public delegate void CardSelectedEventHandler(CardSelectedEventArgs e);
        /// <summary>
        /// Event that is triggered when a new card has been selected.
        /// </summary>
        public event CardSelectedEventHandler CardSelected;

        private void listView_DoubleClick(object sender, EventArgs e)
        {
            onCardSelected();
        }
 
        private void listView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                onCardSelected();
        }

        void onCardSelected()
        {
            if (listView.SelectedItems.Count != 1)
                return;

            dsLanguageData.CardRow c = Cards.Select(string.Format("ID = {0}", ((IDListViewItem)listView.SelectedItems[0]).ID))[0] as dsLanguageData.CardRow;
            CardSelectedEventArgs e = new CardSelectedEventArgs(c);

            if (CardSelected != null)
                CardSelected(e);

            if (e.CardWasSelected)
                _lastSelectedID = c.ID;
        }

        public class CardSelectedEventArgs : EventArgs
        {
            public dsLanguageData.CardRow Card;
            public bool CardWasSelected;

            public CardSelectedEventArgs(dsLanguageData.CardRow card)
            {
                Card = card;
                CardWasSelected = true;
            }
        }
        
        #endregion        
        
        #region Events

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            ClearCards();
        }

        private void btnRemoveSelected_Click(object sender, EventArgs e)
        {
            //if the selected card is being removed
            bool wasSelected = false;
            List<int> removedCardIDs = new List<int>();

            foreach (IDListViewItem item in listView.SelectedItems)
            {
                if (item.ID == _lastSelectedID)
                    wasSelected = true;

                _cards.Rows.Remove(_cards.FindByID(item.ID));

                removedCardIDs.Add(item.ID);
            }

            RefreshCards(false);

            if (removedCardIDs.Count > 0)
            {
                if (Cards != null && Cards.Rows.Count > 0)
                    onPlayListChanged(PlaylistAction.Changed);
                else
                    onPlayListChanged(PlaylistAction.Deleted);
            }

            if (wasSelected)
                SelectRandomCard();
        }     

        #endregion

        #region Drag/Drop

        private void CardListPlayList_DragDrop(object sender, DragEventArgs e)
        {
            string[] formats = e.Data.GetFormats();
            bool wasEmpty = Cards.Count < 1;

            if (e.Data.GetData(formats[0]) is CardDragDropHolder)
            {
                //Add the card to the list
                LanguageData dataLayer = new LanguageData();
                CardDragDropHolder tmp = e.Data.GetData(formats[0]) as CardDragDropHolder;             

                dsLanguageData.CardDataTable dtNewCards = dataLayer.GetCardsByIDs(tmp.CardIDs);
                if (tmp.Action == CardDragDropAction.Add)
                    AddOrLoadCards(dtNewCards, tmp.Description, tmp.CardListIDs,  AddOrLoad.Add);
                else
                    AddOrLoadCards(dtNewCards, tmp.Description, tmp.CardListIDs, AddOrLoad.Load);                                              
            }
        }
        
        public void AddOrLoadCards(dsLanguageData.CardDataTable cards, string description, List<int> cardListIDs, AddOrLoad action)
        {
            if (action == AddOrLoad.Load)
            {
                ClearCards();
            }

            if (cards.Rows.Count < 1 && description.Trim().Length < 1)
                return;

            btnHeading.Text = string.Format("Playlist: {0}", description);

            CardListIDs = cardListIDs;
            bool wasEmpty = Cards.Count < 1;
            Cards.Merge(cards);
            RefreshCards(false);

            //deciede whether or not to make a new list or just add to the old one
            if (action == AddOrLoad.Load || wasEmpty )
            {
                Description = description;
                onPlayListChanged(PlaylistAction.Loaded);
                SelectRandomCard();
            }
            else if (action == AddOrLoad.Add)
            {
                onPlayListChanged(PlaylistAction.Changed);
            }    
        }

        private void CardListPlayList_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }
        #endregion        
                           
    }

    public class IDListViewItem : ListViewItem
    {
        public int ID;

        public IDListViewItem(int id, string item)
            : base(item)
        {
            ID = id;
        }
    }

    public enum PlaylistAction
    {
        Loaded,
        Changed,
        Deleted,
        Cleared
    }

    /// <summary>
    /// Enum that specifies whether something should be added or loaded
    /// </summary>
    public enum AddOrLoad
    {
        Add,
        Load
    }
}
