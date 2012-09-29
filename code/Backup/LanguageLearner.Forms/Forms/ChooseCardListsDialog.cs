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
    public partial class ChooseCardListsDialog : Form
    {
        List<int> _cardListIDs = new List<int>();
        List<int> _selectedCardListIDs = new List<int>();
        public string Description;

        #region Properties

        public List<int> CardListIDs
        {
            get { return _cardListIDs; }
            set
            {
                _cardListIDs = value;
                setListView(_cardListIDs, listViewCardLists);
            }
        }
        
        public List<int> SelectedCardListIDs
        {
            get { return _cardListIDs; }
            set
            {
                _selectedCardListIDs = value;
                setListView(_selectedCardListIDs, listViewSelectedCardLists);
            }
        }

        public bool RememberSelection
        {
            get { return chkBoxRemember.Checked; }
            set { chkBoxRemember.Checked = value; }
        }
        #endregion                

        public ChooseCardListsDialog(List<int> cardListIDs, string description)
        {
            InitializeComponent();

            CardListIDs = cardListIDs;
            SelectedCardListIDs = cardListIDs;
            Description = description;
        }

        void setListView(List<int> cardListIDs, ListView listView)
        {
            listView.Items.Clear();
            LanguageData dataLayer = new LanguageData();

            foreach (int id in cardListIDs)
            {
                dsLanguageData.CardListRow row = dataLayer.daCardList.GetDataByID(id)[0];
                listView.Items.Add(new IDListViewItem(row.ID, row.Name));
            }
            
        }

        #region Events

        private void btnAddAll_Click(object sender, EventArgs e)
        {
            SelectedCardListIDs = CardListIDs;
        }

        private void btnAddSelected_Click(object sender, EventArgs e)
        {
            foreach (IDListViewItem item in listViewCardLists.SelectedItems)
            {
                bool add = true;
                foreach (IDListViewItem selItem in listViewSelectedCardLists.Items)
                {
                    if (selItem.ID == item.ID)
                        add = false;
                }

                if (add)
                    listViewSelectedCardLists.Items.Add(item.Clone() as IDListViewItem);
            }
        }

        

        private void btnRemoveSelected_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewSelectedCardLists.SelectedItems)
            {
                item.Remove();
            }
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            SelectedCardListIDs = new List<int>();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

    }
}