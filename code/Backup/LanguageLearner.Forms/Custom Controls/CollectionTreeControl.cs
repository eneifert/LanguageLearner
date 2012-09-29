using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using LanguageLearner.Data;
using System.IO;
using System.Xml;

namespace LanguageLearner.UI
{
    /// <summary>
    /// Class that loads the Collections and CardLists and allows the user to select what they want to practice.
    /// Also allows the user to create/delete Collections and CardGroups.
    /// </summary>
    public partial class CollectionTreeControl : UserControl
    {
        bool hideButtons = false;
        bool disableModificationButtons = false;
        CardDragDropAction dragDropAction = CardDragDropAction.Load;

        [Browsable(true)]
        public CardDragDropAction DragDropAction
        {
            get { return dragDropAction; }
            set { dragDropAction = value; }
        }

        [Browsable(true)]
        public bool HideButtons
        {
            get { return hideButtons; }
            set
            {
                hideButtons = value;
                if (hideButtons)
                    tableLayoutPanel.RowStyles[1].Height = 0;
                else
                    tableLayoutPanel.RowStyles[1].Height = 41;
                
            }
        }

        [Browsable(true)]
        public bool DisableModificationButtons
        {
            get { return disableModificationButtons; }
            set
            {
                disableModificationButtons = value;
                btnAdd.Enabled = !value;
                btnEdit.Enabled = !value;
                btnDelete.Enabled = !value;
            }
        }

        public CollectionTreeControl()
        {
            InitializeComponent();

        }

        #region Public Methods

        /// <summary>
        /// Returns a CardDataTable with the selected cards
        /// </summary>
        public dsLanguageData.CardDataTable GetSelectedCards()
        {
            return getSelectedCardsEventArgs().Cards;
        }

        /// <summary>
        /// Loads the Collections and CardLists.
        /// </summary>
        public void ReLoadNodes()
        {            
            ReLoadDictionary();
            ReLoadCardLists();            
        }

        public void ReLoadDictionary()
        {           
            dictionaryTreeView.Nodes.Clear();
            LanguageData dataLayer = new LanguageData();
            dsLanguageData.DictionaryDataTable dictionary = dataLayer.daDictionary.GetData();

            foreach (dsLanguageData.DictionaryRow dict in dictionary)
            {
                MyTreeNode tmpDict = new MyTreeNode(dict.Name, dict.ID, MyTreeNodeType.Dictionary);

                foreach (char letter in dict.Alphabet)
                {
                    MyTreeNode tmpLet = new MyTreeNode(letter.ToString(), -1, MyTreeNodeType.Letter);

                    dsLanguageData.CardDataTable cards = dataLayer.GetCardsBeginningWith(letter.ToString(), dict.Column);
                    foreach (dsLanguageData.CardRow card in cards)
                    {
                        tmpLet.Nodes.Add(new MyTreeNode((string)card[dict.Column], card.ID, MyTreeNodeType.Card));
                    }

                    tmpDict.Nodes.Add(tmpLet);
                }

                dictionaryTreeView.Nodes.Add(tmpDict);
            }
        }

        public void ReLoadCardLists()
        {
            cardListsTreeView.Nodes.Clear();

            try
            {
                LanguageData dataLayer = new LanguageData();

                foreach (dsLanguageData.CollectionRow collection in dataLayer.daCollection.GetData())
                {
                    MyTreeNode collectionNode = new MyTreeNode(collection.Name, collection.ID, MyTreeNodeType.Collection);

                    foreach (dsLanguageData.CardListRow cardList in dataLayer.daCardList.GetDataByCollectionID(collection.ID))
                    {
                        collectionNode.Nodes.Add(new MyTreeNode(cardList.Name, cardList.ID, MyTreeNodeType.CardList));
                    }
                    cardListsTreeView.Nodes.Add(collectionNode);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        #endregion
           
        #region DataSelected Event
        
        private void btnSendAllCards_Click(object sender, EventArgs e)
        {
            LanguageData dataLayer = new LanguageData();
            CardsSelectedEventArgs eventArgs = new CardsSelectedEventArgs(dataLayer.daCard.GetData(), "All Cards in Database");
            onCardsSelected(eventArgs);
        }

        private void btnSendSelectedCards_Click(object sender, EventArgs e)
        {
            CardsSelectedEventArgs eventArgs = getSelectedCardsEventArgs();
            onCardsSelected(eventArgs);
        }
     
        void onCardsSelected(CardsSelectedEventArgs eventArgs)
        {
            if (CardsSelected != null && eventArgs != null && eventArgs.Cards != null && (eventArgs.Cards.Rows.Count > 0 || eventArgs.CardListIDs.Count > 0))
                CardsSelected(eventArgs);
        }

        CardsSelectedEventArgs getSelectedCardsEventArgs()
        {
            List<int> cardListIDs = new List<int>();
            dsLanguageData.CardDataTable cards = new dsLanguageData.CardDataTable();
            
            if (cardsTabControl.SelectedTab == tpDictionary)
            {

                MyTreeNodeType selNodeType = getSelectedNodeType();
                switch (selNodeType)
                {
                    case MyTreeNodeType.Letter:
                        cards = getSelectedCardsBySelectedLetters();
                        //TODO Get Selected Letters and change the Event Args to hold them
                        break;
                    case MyTreeNodeType.Card:
                        cards = getSelectedCardsBySelectedCardsInDictionary();
                        break;
                    case MyTreeNodeType.Other:
                        return null;
                }                
            }
            else if (cardsTabControl.SelectedTab == tpCardLists)
            {
                cards = getSelectedCardsBySelectedCardLists();

                foreach (MWCommon.MWTreeNodeWrapper node in cardListsTreeView.SelNodes.Values)
                {
                    MyTreeNode tmp = node.Node as MyTreeNode;
                    if (tmp.Type == MyTreeNodeType.CardList)
                        cardListIDs.Add(tmp.ID);
                }
            }

            return new CardsSelectedEventArgs(cards, cardListIDs, getSelectedItemDescription());
        }        
        
        dsLanguageData.CardDataTable getSelectedCardsBySelectedCardsInDictionary()
        {
            string whereClause = "Where ID in (";
            bool empty = true;            

            LanguageData dataLayer = new LanguageData();
            dsLanguageData.DictionaryDataTable dictionary = dataLayer.daDictionary.GetData();
            string column = string.Empty;

            foreach (MWCommon.MWTreeNodeWrapper node in dictionaryTreeView.SelNodes.Values)
            {
                MyTreeNode tmp = node.Node as MyTreeNode;
                if (tmp.Type == MyTreeNodeType.Card)
                {
                    DataRow[] rows = dictionary.Select(string.Format("Name = '{0}'", tmp.Parent.Parent.Text));
                    column = rows[0]["Column"].ToString();                    

                    whereClause += tmp.ID.ToString() + ", ";
                    empty = false;
                }
            }

            if(empty)
                return new dsLanguageData.CardDataTable();

            whereClause = whereClause.Remove(whereClause.Length - 2,2);
            whereClause += ") Order By " + column;

            return dataLayer.GetCardsWhere(whereClause);
        }

        dsLanguageData.CardDataTable getSelectedCardsBySelectedLetters()
        {
            string whereClause = string.Empty;
            bool empty = true;
            string column = string.Empty;

            LanguageData dataLayer = new LanguageData();
            dsLanguageData.DictionaryDataTable dictionary = dataLayer.daDictionary.GetData();              

            foreach (MWCommon.MWTreeNodeWrapper node in dictionaryTreeView.SelNodes.Values)
            {
                MyTreeNode tmp = node.Node as MyTreeNode;
                if (tmp.Type == MyTreeNodeType.Letter)
                {
                    empty = false;
                    DataRow[] rows = dictionary.Select(string.Format("Name = '{0}'", tmp.Parent.Text));
                    column = rows[0]["Column"].ToString();
                    whereClause += string.Format("({0} LIKE '{1}%') OR", column, tmp.Text);
                }                
            }

            if (!empty)
            {
                whereClause = whereClause.Remove(whereClause.Length - 2, 2);
                whereClause += string.Format(" Order By {0}", column);
            }

            return dataLayer.GetCardsWhere(whereClause);            
        }

        dsLanguageData.CardDataTable getSelectedCardsBySelectedCardLists()
        {
            dsLanguageData.CardDataTable cards = new dsLanguageData.CardDataTable();
            List<int> cardListIDs = new List<int>();

            foreach (MWCommon.MWTreeNodeWrapper node in cardListsTreeView.SelNodes.Values)
            {
                MyTreeNode tmp = node.Node as MyTreeNode;
                if (tmp.Type == MyTreeNodeType.CardList)
                    cardListIDs.Add(tmp.ID);
            }

            if (cardListIDs.Count > 0)
            {
                LanguageData dataLayer = new LanguageData();
                cards = dataLayer.GetCardsInCardLists(cardListIDs);
            }

            return cards;
        }

        string getSelectedItemDescription()
        {            
            string description = string.Empty;
            MWControls.MWTreeView treeView;
            if (tpCardLists == cardsTabControl.SelectedTab)
                treeView = cardListsTreeView;
            else
                treeView = dictionaryTreeView;

            if (treeView.SelNodes.Count < 1)
                return string.Empty;

            foreach (MWCommon.MWTreeNodeWrapper node in treeView.SelNodes.Values)
            {
                MyTreeNode tmp = node.Node as MyTreeNode;
                description += ", " + tmp.Text;
            }
            description = description.Remove(0, 2);

            return description;
        }

        private MyTreeNodeType getSelectedNodeType()
        {
            MyTreeNodeType type = MyTreeNodeType.Other;
            bool wasSet = false;

            foreach (MWCommon.MWTreeNodeWrapper node in dictionaryTreeView.SelNodes.Values)
            {
                MyTreeNode tmp = node.Node as MyTreeNode;

                if (type != tmp.Type && wasSet)
                {
                    return MyTreeNodeType.Other;
                }
                else
                {
                    type = tmp.Type;
                    wasSet = true;
                }                
            }

            return type;
        }  

        public class CardsSelectedEventArgs : EventArgs
        {
            public dsLanguageData.CardDataTable Cards;
            public string SelectionDescription;
            public List<int> CardListIDs = new List<int>();

            public CardsSelectedEventArgs(dsLanguageData.CardDataTable cards, string selectionDescription)
                : this(cards, new List<int>(), selectionDescription)
            { }

            public CardsSelectedEventArgs(dsLanguageData.CardDataTable cards, List<int> cardListIDs, string selectionDescription)
            {
                CardListIDs = cardListIDs;
                Cards = cards;
                SelectionDescription = selectionDescription;
            }

            
        }        

        public delegate void CardsSelectedEventHandler(CardsSelectedEventArgs e);
        public event CardsSelectedEventHandler CardsSelected;
        #endregion

        #region Add, Edit, Delete Dictionary or CardLists

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cardsTabControl.SelectedTab == tpDictionary)
                addEditDictionary();
            else if (cardsTabControl.SelectedTab == tpCardLists)
                addCardList();
            
        }

        void addEditDictionary()
        {
            AddEditDictionary addEditForm = new AddEditDictionary();
            addEditForm.DataChanged += new EventHandler(addEditForm_DataChanged);
            addEditForm.ShowDialog();
        }

        void addEditForm_DataChanged(object sender, EventArgs e)
        {
            ReLoadDictionary();
        }

        void addCardList()
        {
            CreateNewGroupDialog addDiag = new CreateNewGroupDialog();

            if (cardListsTreeView.SelNode != null && ((MyTreeNode)cardListsTreeView.SelNode).Type == MyTreeNodeType.Collection)
                addDiag.SelectedCollectionID = ((MyTreeNode)cardListsTreeView.SelNode).ID;

            if (addDiag.ShowDialog() == DialogResult.OK)
            {
                LanguageData dataLayer = new LanguageData();

                if (addDiag.GroupType == MyTreeNodeType.Collection)
                {
                    //Add the collection, refresh the nodes, and select it
                    dsLanguageData.CollectionRow collection = new dsLanguageData.CollectionDataTable().NewCollectionRow();
                    collection.Name = addDiag.GroupName;
                    collection.CanEdit = true;
                    dataLayer.InsertCollection(collection);

                    ReLoadNodes();
                    foreach (MyTreeNode node in cardListsTreeView.Nodes)
                    {
                        if (node.ID == collection.ID)
                        {
                            cardListsTreeView.SelectNode(node, true);
                        }
                    }
                }
                else if (addDiag.GroupType == MyTreeNodeType.CardList)
                {
                    //add the CardList, refresh the nodes, and select it
                    dsLanguageData.CardListRow cardList = new dsLanguageData.CardListDataTable().NewCardListRow();
                    cardList.Name = addDiag.GroupName;
                    cardList.CollectionID = addDiag.SelectedCollectionID;
                    dataLayer.InsertCardList(cardList);

                    ReLoadNodes();

                    foreach (MyTreeNode node in cardListsTreeView.Nodes)
                    {
                        if (node.ID == cardList.CollectionID)
                        {
                            node.Expand();
                            foreach (MyTreeNode cardListNode in node.Nodes)
                            {
                                if (cardListNode.ID == cardList.ID)
                                {
                                    cardListsTreeView.SelectNode(cardListNode, true);
                                }
                            }
                        }
                    }

                    //onDataSelected(); Removed 3/10/09 seems misplaced

                }//end elseif                                               
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (cardsTabControl.SelectedTab == tpDictionary)
                addEditDictionary();
            else if (cardsTabControl.SelectedTab == tpCardLists)
                editCardLists();                
        }

        void editCardLists()
        {
            if (cardListsTreeView.SelNodes.Count < 1)
                return;
            if (cardListsTreeView.SelNodes.Count > 1)
            {
                MessageBox.Show("You can only edit one group at a time.", "Select Only One Group", MessageBoxButtons.OK);
                return;
            }

            LanguageData dataLayer = new LanguageData();
            MyTreeNode node = cardListsTreeView.SelNode as MyTreeNode;
            bool canEdit = true;

            switch (node.Type)
            {
                case MyTreeNodeType.Collection:
                    dsLanguageData.CollectionRow col = dataLayer.daCollection.GetDataByID(node.ID)[0];
                    if (!col.CanEdit)
                    {
                        canEdit = false;
                    }
                    break;
                case MyTreeNodeType.Other:
                    canEdit = false;
                    break;
            }

            if (!canEdit)
            {
                MessageBox.Show("Nope, you can't change this one. But don't worry there are plenty of other groups ready and willing for a change!", "Can't do it", MessageBoxButtons.OK);
                return;
            }

            GetStringDialog diag = new GetStringDialog(node.Text, "Edit");
            diag.ShowDialog();
            if (diag.DialogResult == DialogResult.OK)
            {
                node.Text = diag.TextValue;

                switch (node.Type)
                {
                    case MyTreeNodeType.CardList:
                        dataLayer.daCardList.UpdateName(node.Text, node.ID);
                        break;
                    case MyTreeNodeType.Collection:
                        dataLayer.daCollection.UpdateName(node.Text, node.ID);
                        break;
                }
            }        
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(cardListsTreeView.SelNodes.Count < 1)
                return;

            if (MessageBox.Show("Are you sure you want to delete the selected group?", "Delete Selection?", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }

            LanguageData dataLayer = new LanguageData();
            foreach (MWCommon.MWTreeNodeWrapper wrapper in cardListsTreeView.SelNodes.Values)
            {
                MyTreeNode node = wrapper.Node as MyTreeNode;
                switch (node.Type)
                {
                    case MyTreeNodeType.CardList:
                        dataLayer.DeleteCardList(node.ID);
                        break;
                    case MyTreeNodeType.Collection:
                        dataLayer.DeleteCollection(node.ID);
                        break;
                }
            }

            ReLoadNodes();
        }
        #endregion

        #region Drag/Drop
            
        private void dictionaryTreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            onDragSelectedCards();                       
        }

        private void cardListsTreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            onDragSelectedCards();
        }

        void onDragSelectedCards()
        {
            CardsSelectedEventArgs cardEventArgs = getSelectedCardsEventArgs();
            if(cardEventArgs != null && (cardEventArgs.Cards.Rows.Count > 0 || cardEventArgs.CardListIDs.Count > 0))
                DoDragDrop(new CardDragDropHolder(this, cardEventArgs.Cards.GetIDList(), cardEventArgs.SelectionDescription, cardEventArgs.CardListIDs, DragDropAction), DragDropEffects.All);            
        }
        #endregion

        

    }

    /// <summary>
    /// Tree Node used in the CollectionTreeControl
    /// </summary>
    class MyTreeNode : TreeNode
    {
        public int ID;
        public MyTreeNodeType Type;

        public MyTreeNode(string name, int id, MyTreeNodeType type)
            : base(name)
        {
            ID = id;
            Type = type;
        }
    }

    public enum MyTreeNodeType
    {
        Collection,
        CardList,
        Dictionary,
        Letter,
        Card,
        Other
    }
}
