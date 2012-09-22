using System.IO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using Microsoft.Win32;
using System.Data;

namespace LanguageLearner.Data
{
    /// <summary>
    /// This class handles all of the interaction between the program and the DB. 
    /// </summary>
    public class LanguageData
    {
        #region Variables

        string conString;        
        public dsLanguageDataTableAdapters.CardListDataTableAdapter daCardListData = new dsLanguageDataTableAdapters.CardListDataTableAdapter();
        public dsLanguageDataTableAdapters.CardListTableAdapter daCardList = new dsLanguageDataTableAdapters.CardListTableAdapter();
        public dsLanguageDataTableAdapters.CardTableAdapter daCard = new dsLanguageDataTableAdapters.CardTableAdapter();
        public dsLanguageDataTableAdapters.CollectionTableAdapter daCollection = new dsLanguageDataTableAdapters.CollectionTableAdapter();
        public dsLanguageDataTableAdapters.SoundClipTableAdapter daSoundClip = new dsLanguageDataTableAdapters.SoundClipTableAdapter();
        
        #endregion

        #region Properties

        string ConnectionString
        {
            get { return conString; }
            set 
            {
                conString = "data source=" + value;
                daCard.Connection.ConnectionString = conString;
                daCardList.Connection.ConnectionString = conString;
                daCardListData.Connection.ConnectionString = conString;
                daCollection.Connection.ConnectionString = conString;
                daSoundClip.Connection.ConnectionString = conString;
            }
        }
        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public LanguageData()
        {
            string location = (string) Registry.LocalMachine.GetValue("\\Software\\Cragsoft\\LanguageLearner", Application.StartupPath);
            
            if (!File.Exists(string.Format("{0}\\Data.s3db", location)))                
                throw new FileNotFoundException(string.Format("Could not find Data.s3db in {0}", location));
            
            ConnectionString = string.Format("{0}\\Data.s3db", location);
        }

        /// <summary>
        /// Constructor that allows takes a string which specifies where the DB is.
        /// </summary>
        /// <param name="dbPath"></param>
        public LanguageData(string dbPath)
        {
            this.ConnectionString = dbPath;
        }
        #endregion

        #region Public Methods

        public int InsertOrUpdateCard(dsLanguageData.CardRow card)
        {
            int i = 0;

            if (daCard.GetDataByCardID(card.ID).Rows.Count < 1)
            {
                if (card.ID < 1)
                    card.ID = getNewIdForTable(new dsLanguageData.CardDataTable().TableName);

                i = daCard.Insert(card.ID, card.Question, card.Answer, card.Notes, card.Count, DateTime.Now, DateTime.Now, card.MarkForReview, card.Difficulty);
            }
            else
            {
                card.LastModified = DateTime.Now;
                i = daCard.Update(card);
            }

            return i;
        }

        public int DeleteCard(int cardID)
        {
            int i = daCardListData.DeleteByCardID(cardID);
            i += daCard.DeleteCard(cardID);
            return i;
        }

        /// <summary>
        /// Gets all the cards in the desired CardLists
        /// </summary>
        /// <param name="cardListIDs"></param>
        /// <returns></returns>
        public dsLanguageData.CardDataTable GetCardsInCardLists(List<int> cardListIDs)
        {
            string ids = string.Empty;
            foreach (int id in cardListIDs)
            {
                ids += ", " + id.ToString();
            }
            ids = ids.Remove(0, 1);
            string sql = string.Format(@"SELECT Card.ID, Card.Question, Card.Answer, Card.Notes, Card.[Count], Card.DateCreated, Card.LastModified, Card.MarkForReview, Card.Difficulty
                            FROM Card INNER JOIN CardListData ON Card.ID = CardListData.ID
                            WHERE (CardListData.CardListID IN ({0}))", ids);
            SQLiteCommand cmd = new SQLiteCommand(sql, new SQLiteConnection(conString));

            dsLanguageData.CardDataTable cards = new dsLanguageData.CardDataTable();

            cmd.Connection.Open();
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dsLanguageData.CardRow row = cards.NewCardRow();

                foreach (DataColumn col in cards.Columns)
                {
                    row[col.ColumnName] = reader[col.ColumnName];
                }
                cards.Rows.Add(row);
            }

            cmd.Connection.Close();

            return cards;

        }
        #endregion

        #region Private Methods
        int getOrCreateDefaultImportCollectionID()
        {
            dsLanguageData.CollectionDataTable col = daCollection.GetData();
            if (col.Rows.Count > 0)
                return col[0].ID;

            daCollection.Insert(1, "Imported", DateTime.Now, DateTime.Now);
            return 1;            
        }

        int getNewIdForTable(string tableName)
        {
            int id = 0;
            string sql = string.Format("SELECT ID FROM {0} ORDER BY ID DESC", tableName);
            SQLiteCommand cmd = new SQLiteCommand(sql, new SQLiteConnection(conString));

            try
            {
                cmd.Connection.Open();
                id = ((int)cmd.ExecuteScalar()) + 1;
                cmd.Connection.Close();
            }
            catch (NullReferenceException)
            {
                id = 1;
            }

            if (id < 1)
                id = 1;

            return id;
        }

        int getNewCardListDataIndex(int cardListID)
        {
            dsLanguageData.CardListDataDataTable dtCardListData = daCardListData.GetDataByCardListID(cardListID);

            if (dtCardListData.Rows.Count > 0)
                return dtCardListData[dtCardListData.Rows.Count - 1].Index + 1;

            return 1;
        }

        private MemoryStream streamFromString(string s)
        {
            MemoryStream ms = new MemoryStream();
            string tmp = string.Empty;
            int i = 0;
            while (i < s.Length)
            {
                tmp = s.Substring(i, 3);
                ms.WriteByte(byte.Parse(tmp));
                i += 3;
            }

            return ms;
        }

        #endregion            

        #region Import

        public class DataImportedEventArgs : EventArgs
        {
            public bool Success;
            public string Message = string.Empty;
            public int Index;
            public int Total;

            public DataImportedEventArgs(bool success, string message, int i, int total)
            {
                Success = success;
                Message = message;
                Index = i;
                Total = total;
            }
        }
        public delegate void DataImportedEventHandler(DataImportedEventArgs e);
        public event DataImportedEventHandler DataImported;       

        public int ImportFromOldDb(string oldDBConString, string newDBConString, bool importMarks)
        {
            ConnectionString = newDBConString;
            LangLearner.Data.LLSqlite oldSqlite = new LangLearner.Data.LLSqlite(oldDBConString);
            LangLearner.Data.dsLanguageDB.CardListDataTable dtOldCardList = oldSqlite.GetCardLists();            
            dsLanguageData dsNewData = new dsLanguageData();
            int defaultCollectionID = getOrCreateDefaultImportCollectionID();

            //Go through each old card list and add any missing cards
            int progressIndex = 0;
            foreach (LangLearner.Data.dsLanguageDB.CardListRow oldCardListRow in dtOldCardList)
            {
                progressIndex++;

                if (daCardList.GetDataByName(oldCardListRow.Name.Trim()).Rows.Count < 1)
                {
                    int i = daCardList.Insert(getNewIdForTable(dsNewData.CardList.TableName), defaultCollectionID, oldCardListRow.Name, DateTime.Now, DateTime.Now);
                    if (i < 1)
                    {
                        DataImported(new DataImportedEventArgs(false, string.Format("Could not create CardList: {0}", oldCardListRow.Name), progressIndex, dtOldCardList.Rows.Count));
                        continue;
                    }
                    else
                    {
                        DataImported(new DataImportedEventArgs(true, string.Format("Added CardList: {0}", oldCardListRow.Name), progressIndex, dtOldCardList.Rows.Count));
                    }
                }
                else
                {
                    DataImported(new DataImportedEventArgs(true, string.Format("CardList already exists: {0}. Checking for new cards...", oldCardListRow.Name), progressIndex, dtOldCardList.Rows.Count));
                }
                               
                int cardListID = (int)daCardList.ScalarQueryGetIDByName(oldCardListRow.Name.Trim());

                //Get the old cards that were in this playlist
                List<string> oldCardListGuid = new List<string>();
                oldCardListGuid.Add(oldCardListRow.Guid);
                LangLearner.Data.dsLanguageDB.CardDataTable dtOldCards = oldSqlite.GetCardsInLists(oldCardListGuid);

                //Now import the cards
                foreach (LangLearner.Data.dsLanguageDB.CardRow oldCardRow in dtOldCards)
                {
                    //Insert new cards if they don't exists                                        
                    dsLanguageData.CardDataTable dtNewCard = daCard.GetDataByCardInfo(oldCardRow.Question, oldCardRow.Answer);                    
                    if (dtNewCard.Rows.Count < 1)
                    {
                        bool markForReview = false;
                        int difficulty = 1;
                        
                        if (importMarks)
                        {
                            markForReview = oldCardRow.IsMarkedForReview;
                            if (oldCardRow.IsEasy)
                                difficulty = 0;
                        }

                        daCard.Insert(getNewIdForTable(new dsLanguageData.CardDataTable().TableName), oldCardRow.Question, oldCardRow.Answer, oldCardRow.Example, oldCardRow.Count, oldCardRow.DateCreated, DateTime.Now, markForReview, difficulty);
                        //TODO error handling
                    }

                    int cardID = daCard.GetDataByCardInfo(oldCardRow.Question, oldCardRow.Answer)[0].ID;                                                         

                    //Check for and import the associated SoundClip
                    if (oldCardRow.SoundFileGuid != null && oldCardRow.SoundFileGuid != Guid.Empty.ToString())
                    {
                        dsLanguageData.SoundClipDataTable dtSoundClip = daSoundClip.GetDataByCardID(cardID);
                        if (dtSoundClip.Rows.Count < 1)
                        {
                            //Convert the old sound row in put in the new one
                            LangLearner.Data.dsLanguageDB.SoundFileRow oldSoundRow = oldSqlite.GetSoundFile(oldCardRow.SoundFileGuid);
                            MemoryStream clipStream = streamFromString(oldSoundRow.SoundFile);

                            daSoundClip.Insert(getNewIdForTable(new dsLanguageData.SoundClipDataTable().TableName), oldSoundRow.Name, clipStream.ToArray(), oldSoundRow.DateCreated, DateTime.Now, cardID);
                            //TODO error handling
                        }
                    }                    

                    //Make sure the card is added to the current playlist
                    dsLanguageData.CardListDataDataTable dtCardListData = daCardListData.GetDataByIDs(cardListID, cardID);
                    if (dtCardListData.Rows.Count < 1)
                    {
                        //TODO make CardListData insert, edit position methods
                        daCardListData.Insert(getNewIdForTable(new dsLanguageData.CardListDataDataTable().TableName), cardListID, cardID, getNewCardListDataIndex(cardListID));
                        //TODO error handling
                    }
                }
            }
                       
            return 0;
        }
        #endregion

        

    }
}
