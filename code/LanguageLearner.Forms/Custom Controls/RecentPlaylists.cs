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
    public partial class RecentPlaylists : UserControl
    {
        dsLanguageData.RecentPlaylistDataTable _dtRecentPlaylist = new dsLanguageData.RecentPlaylistDataTable();

        public RecentPlaylists()
        {
            InitializeComponent();
        }

        public void AddPlaylist(dsLanguageData.CardDataTable cards, string description)
        {
            List<int> cardIDs = new List<int>();
            foreach (dsLanguageData.CardRow card in cards)
            {
                cardIDs.Add(card.ID);
            }

            AddPlaylist(cardIDs, description);
        }

        public void AddPlaylist(List<int> cardIDs, string description)
        {            
            LanguageData dataLayer = new LanguageData();
            dataLayer.InsertRecentPlaylist(cardIDs, description, 10);

            RefreshRecentPlaylists();
        }

        public void UpdateCurrentPlaylist(List<int> cardIDs, string description)
        {
            dsLanguageData.RecentPlaylistRow row = _dtRecentPlaylist[0];

            LanguageData dataLayer = new LanguageData();
            dataLayer.daRecentPlaylist.UpdateByID(row.ID, description, dataLayer.IntListToString(cardIDs), row.Index);

            RefreshRecentPlaylists();
        }

        public void RemoveCurrentPlaylist()
        {
            LanguageData dataLayer = new LanguageData();
            dataLayer.DeleteRecentPlaylistByIndex(0);

            RefreshRecentPlaylists();
        }

        public void RefreshRecentPlaylists()
        {
            listView1.Items.Clear();

            try
            {
                LanguageData dataLayer = new LanguageData();
                _dtRecentPlaylist = dataLayer.daRecentPlaylist.GetData();

                foreach (dsLanguageData.RecentPlaylistRow row in _dtRecentPlaylist)
                {
                    listView1.Items.Add(new IDListViewItem(row.ID, row.Name));
                }
            }
            catch (FileNotFoundException e)
            {
                //TODO send a message here that the main form will pick up and display on the bottom 
            }
        }

        private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            //Get the ids

            List<int> recentPlaylistIDs = new List<int>();
            string description = string.Empty;

            foreach (IDListViewItem item in listView1.SelectedItems)
            {
                recentPlaylistIDs.Add(item.ID);
                description += ", " + item.Text;
            }

            if (description.StartsWith(", "))
                description = description.Remove(0, 2);

            List<int> cardIDs = new List<int>();
            foreach (dsLanguageData.RecentPlaylistRow row in _dtRecentPlaylist)
            {
                if (recentPlaylistIDs.IndexOf(row.ID) > -1)
                {
                    cardIDs.AddRange(stringToIntList(row.CardIDs));
                }
            }
           
            if (cardIDs.Count > 0)
            {                
                DoDragDrop(new CardDragDropHolder(this, cardIDs, description, CardDragDropAction.Load), DragDropEffects.All);
            }
        }

        private List<int> stringToIntList(string p)
        {
            List<int> intList = new List<int>();

            string[] ids = p.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries);

            foreach (string id in ids)
            {
                intList.Add(int.Parse(id));
            }

            return intList;
        }

        
    }
}
