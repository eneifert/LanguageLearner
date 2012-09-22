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
        public CollectionTreeControl()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Loads the Collections and CardLists.
        /// </summary>
        public void LoadNodes()
        {
            //XmlDocument doc = new XmlDocument();
            //doc.Load(Application.StartupPath + "\\settings.xml");

            //TreeNode dictionary = new TreeNode("Dictionary");
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
                    collectionTreeView.Nodes.Add(collectionNode);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.InnerException.Message);
            }

            //TODO Display the dictionary by two Groups Local and Foriegn Alphabet
        }

        private void CollectionTreeControl_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Region that handles when and how data is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region DataSelected Event
        private void btnShow_Click(object sender, EventArgs e)
        {
            onDataSelected();
        }

        void onDataSelected()
        {
            List<int> ids = new List<int>();
            foreach (MWCommon.MWTreeNodeWrapper node in collectionTreeView.SelNodes.Values)
            {
                MyTreeNode tmp = node.Node as MyTreeNode;
                if(tmp.Type == MyTreeNodeType.CardList)
                    ids.Add(tmp.ID);
            }

            if(DataSelected != null)
                DataSelected(new DataSelectedEventArgs(ids));
        }

        public class DataSelectedEventArgs : EventArgs
        {
            public List<int> CardListIDs;

            public DataSelectedEventArgs(List<int> cardListIDs)
            {
                CardListIDs = cardListIDs; 
            }
        }
        public delegate void DataSelectedEventHandler(DataSelectedEventArgs e);
        public event DataSelectedEventHandler DataSelected;
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
        Other
    }
}
