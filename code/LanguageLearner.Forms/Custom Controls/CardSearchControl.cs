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
    public partial class CardSearchControl : UserControl
    {
        string answer = "Answer";
        string question = "Question";
        string both = "Both";

        public CardSearchControl()
        {
            InitializeComponent();

            cmbSearchBy.Items.AddRange(new string[] {both, answer, question});         
            cmbSearchBy.SelectedIndex = 0;
        }                                

        #region Search + Search Events

        void search()
        {
            if (txtSearch.Text.Trim().Length < 1)
            {
                MessageBox.Show("Please enter text to search for.", "Search for what?", MessageBoxButtons.OK);
                return;
            }

            string precision = "%";
            if (chkBoxExact.Checked)
                precision = string.Empty;

            string whereClause = string.Format("Where {0} like '{2}{1}{2}'", cmbSearchBy.SelectedItem.ToString(), txtSearch.Text, precision);

            if (cmbSearchBy.SelectedItem.ToString() == both)
            {
                whereClause = string.Format("Where Answer like '{1}{0}{1}' OR Question like '{1}{0}{1}'", txtSearch.Text, precision);
            }

            LanguageData dataLayer = new LanguageData();
            loadResults(dataLayer.GetCardsWhere(whereClause));
        }

        void loadResults(dsLanguageData.CardDataTable cards)
        {
            listViewResults.Items.Clear();

            if (cards.Rows.Count < 1)
                listViewResults.Items.Add("No matches found");
            foreach (dsLanguageData.CardRow card in cards)
            {
                listViewResults.Items.Add(new IDListViewItem(card.ID, string.Format("{0} -- {1}", card.Answer, card.Question)));
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            search();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {            
            if (e.KeyChar == (char)Keys.Return)
            {                
                btnSearch.PerformClick();
            }
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim().Length > 0)
                search();
        }
        #endregion

        private void listViewResults_ItemDrag(object sender, ItemDragEventArgs e)
        {
            List<int> cardIDs = new List<int>();

            foreach (ListViewItem item in listViewResults.SelectedItems)
            {
                if(item is IDListViewItem)
                    cardIDs.Add(((IDListViewItem)item).ID);
            }

            if (cardIDs.Count > 0)
            {
                LanguageData dataLayer = new LanguageData();
                string description = string.Empty;

                foreach (dsLanguageData.CardRow card in dataLayer.GetCardsByIDs(cardIDs))
                {
                    description += ", " + card.Question;
                }
                description = description.Remove(0, 2);

                DoDragDrop(new CardDragDropHolder(this, cardIDs, description, CardDragDropAction.Add), DragDropEffects.All);
            }
        }                
    }    
}
