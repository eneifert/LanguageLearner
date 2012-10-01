using System.IO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using Microsoft.Win32;
using System.Data;
using System.Drawing;

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
        public dsLanguageDataTableAdapters.DictionaryTableAdapter daDictionary = new LanguageLearner.Data.dsLanguageDataTableAdapters.DictionaryTableAdapter();
        public dsLanguageDataTableAdapters.RecentPlaylistTableAdapter daRecentPlaylist = new LanguageLearner.Data.dsLanguageDataTableAdapters.RecentPlaylistTableAdapter();
        public dsLanguageDataTableAdapters.PictureTableAdapter daPicture = new LanguageLearner.Data.dsLanguageDataTableAdapters.PictureTableAdapter();
        #endregion

        #region Properties

        public static LanguageData DataLayer
        {
            get
            {
                return new LanguageData();               
            }
        }

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
                daDictionary.Connection.ConnectionString = conString;
                daRecentPlaylist.Connection.ConnectionString = conString;
                daPicture.Connection.ConnectionString = conString;
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

        public int InsertCollection(dsLanguageData.CollectionRow collection)
        {            
            collection.ID = getNewIdForTable(new dsLanguageData.CollectionDataTable().TableName);
            int i = daCollection.Insert(collection.ID, collection.Name, DateTime.Now, DateTime.Now, collection.CanEdit);

            return i;
        }

        public int InsertCardList(dsLanguageData.CardListRow cardList)
        {
            cardList.ID = getNewIdForTable(new dsLanguageData.CardListDataTable().TableName);
            int i = daCardList.Insert(cardList.ID, cardList.CollectionID, cardList.Name, DateTime.Now, DateTime.Now);

            return i;
        }      

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
                i = daCard.UpdateCard(card.ID, card.Question, card.Answer, card.Notes, card.Count, card.DateCreated, DateTime.Now, card.MarkForReview, card.Difficulty, card.ID);                
            }

            return i;
        }

        public int InsertOrUpdateDictionary(dsLanguageData.DictionaryRow dictionary)
        {
            int i = 0;

            dsLanguageData.DictionaryDataTable dtDict = daDictionary.GetDataByID(dictionary.ID);
            if (dtDict.Rows.Count < 1 || dictionary.ID < 0)
            {
                dictionary.ID = getNewIdForTable(new dsLanguageData.DictionaryDataTable().TableName);
                i = daDictionary.Insert(dictionary.ID, dictionary.Name, dictionary.Alphabet, dictionary.Column);
            }
            else
            {
                dsLanguageData.DictionaryRow d = dtDict[0];
                if (d.ID != dictionary.ID || d.Name != dictionary.Name || d.Alphabet != dictionary.Alphabet || d.Column != dictionary.Column)
                {
                    i = daDictionary.UpdateDictionary(dictionary.ID, dictionary.Name, dictionary.Alphabet, dictionary.Column);
                }
            }

            return i;
        }

        public int InsertUpdateOrDeleteSoundClip(dsLanguageData.SoundClipRow sound)
        {
            int i = 0;

            if (sound != null)
            {
                //if its being deleted                
                if (sound.SoundClip.Length == 0 || sound.RowState == DataRowState.Deleted)
                {
                    i = daSoundClip.DeleteSoundClip(sound.ID);
                }
                else
                {
                    if (sound.CardID < 1)
                    {
                        throw new MissingFieldException("SoundClip is missing its corresponding CardID");
                    }

                    if (daSoundClip.GetDataByID(sound.ID).Rows.Count < 1)
                    {
                        if (sound.ID < 1)
                            sound.ID = getNewIdForTable(new dsLanguageData.SoundClipDataTable().TableName);

                        i = daSoundClip.Insert(sound.ID, sound.Name, sound.SoundClip, DateTime.Now, DateTime.Now, sound.CardID);
                    }
                    else
                    {
                        sound.LastModified = DateTime.Now;
                        i = daSoundClip.Update(sound);
                    }
                }
            }

            return i;
        }

        public int InsertUpdateOrDeleteImage(Image image, int cardID, string searchText)
        {
            bool isEmpty = isEmptyImage(image);

            //delete empty images
            if (isEmpty && CardHasPicture(cardID))
            {
                return daPicture.DeleteByCardID(cardID);
            }
            //insert or Update new images
            else if (!isEmpty )
            {
                return InsertUpdatePicture(image, cardID, searchText);
            }

            return 0;
        }

        bool isEmptyImage(Image image)
        {
            if (image == null || ImageToByteArray(image).Length == 0)
                return true;
            return false;
        }

        public int InsertUpdatePicture(Image picture, int cardID, string searchText)
        {
            dsLanguageData.PictureDataTable dtPic = daPicture.GetDataByCardID(cardID);

            //Insert if no Pic exists
            if (dtPic.Rows.Count < 1)
                return daPicture.Insert(getNewIdForTable(new dsLanguageData.PictureDataTable().TableName), searchText, ImageToByteArray(picture), ".bmp", DateTime.Now, cardID);
            //Update the pic
            else
            {
                dsLanguageData.PictureRow pic = dtPic.Rows[0] as dsLanguageData.PictureRow;
                pic.SearchText = searchText;
                pic.Image = ImageToByteArray(picture);               
                return daPicture.Update(pic);
            }

        }

        public byte[] ImageToByteArray(Image image)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public Image ByteArrayToImage(byte[] imageArray)
        {
            MemoryStream ms = new MemoryStream(imageArray);
            return Image.FromStream(ms);
        }

        public int InsertCardListDataItem(int cardListID, int cardID)
        {
            int id = getNewIdForTable(new dsLanguageData.CardListDataDataTable().TableName);
            dsLanguageData.CardListDataDataTable dt = daCardListData.GetDataByCardListID(cardListID);
            int index = 0;

            if (dt.Rows.Count > 0)
                index = dt[dt.Rows.Count - 1].Index + 1;

            int i = 0;

            //Check to see if the item is already in there                          
            if (dt.Select(string.Format("CardListID={0} and CardID={1}", cardListID.ToString(), cardID.ToString())).Length < 1)
            {
                daCardListData.Connection.Open();
                i = daCardListData.Insert(id, cardListID, cardID, index);
                daCardListData.Connection.Close();
            }
            return i;
        }

        /// <summary>
        /// Inserts a new RecentPlaylist record at Index 0 and increments all other records. 
        /// Any records above the limit are deleted.
        /// </summary>
        /// <param name="cardIDs"></param>
        /// <param name="name"></param>
        /// <param name="playlistLimit"></param>
        /// <returns></returns>
        public int InsertRecentPlaylist(List<int> cardIDs, string name, int playlistLimit)
        {
            int i = 0;

            //First check to see if the playlist is already there, if so move it to the front
            string idString = IntListToString(cardIDs);           
            dsLanguageData.RecentPlaylistDataTable dtRecentPlaylists = daRecentPlaylist.GetDataByCardIDs(idString);

            if (dtRecentPlaylists.Rows.Count > 0)
            {
                dsLanguageData.RecentPlaylistRow row = dtRecentPlaylists[0];
                i += daRecentPlaylist.IncrementIndexesLessThan(row.Index);
                i += daRecentPlaylist.UpdateIndex(0, row.ID);
            }
            else
            {
                int id = getNewIdForTable(new dsLanguageData.RecentPlaylistDataTable().TableName);

                i += daRecentPlaylist.IncrementIndexesLessThan(playlistLimit);
                i += daRecentPlaylist.Insert(id, name, idString, 0);
                i += daRecentPlaylist.DeleteRecentPlaylistsAboveLimit(playlistLimit - 2);                
            }

            return i;            
        }

        public string IntListToString(List<int> list)
        {
            string res = string.Empty;
            foreach (int i in list)
            {
                res += ", " + i.ToString();
            }
            if(res.StartsWith(", "))
                res = res.Remove(0, 2);

            return res;
        }

        public int DeleteCard(int cardID)
        {
            int i = daCardListData.DeleteByCardID(cardID);
            i += daCard.DeleteCard(cardID);
            return i;
        }

        public int DeleteCardList(int cardListID)
        {
            int i = daCardListData.DeleteByCardListID(cardListID);
            i += daCardList.DeleteByID(cardListID);
            return i;
        }

        public int DeleteCollection(int collectionID)
        {
            int i = daCardListData.DeleteByCollectionID(collectionID);
            i += daCardList.DeleteByCollectionID(collectionID);
            i += daCollection.DeleteByID(collectionID);

            return i;
        }

        public int DeleteRecentPlaylistByIndex(int index)
        {
            int i = daRecentPlaylist.DeleteAtIndex(index);
            i += daRecentPlaylist.DecrementIndexesGreaterThan(index);
            return i;
        }

        /// <summary>
        /// Gets all the cards with the IDs
        /// </summary>
        /// <param name="cardIDs"></param>
        /// <returns></returns>
        public dsLanguageData.CardDataTable GetCardsByIDs(List<int> cardIDs)
        {
            dsLanguageData.CardDataTable cards = new dsLanguageData.CardDataTable();

            if (cardIDs == null || cardIDs.Count < 1)
                return cards;

            string ids = string.Empty;
            foreach (int id in cardIDs)
            {
                ids += ", " + id.ToString();
            }

            ids = ids.Remove(0, 1);

            string sql = string.Format(@"SELECT DISTINCT Card.ID, Card.Question, Card.Answer, Card.Notes, Card.[Count], Card.DateCreated, Card.LastModified, Card.MarkForReview, Card.Difficulty
                            FROM Card WHERE (ID IN ({0}))", ids);
            SQLiteCommand cmd = new SQLiteCommand(sql, new SQLiteConnection(conString));
            System.Data.SQLite.SQLiteDataAdapter cardAdapter = new SQLiteDataAdapter(cmd);

            cardAdapter.Fill(cards);
            return cards;
        }

        /// <summary>
        /// Gets all the cards with the IDs
        /// </summary>
        /// <param name="cardIDs"></param>
        /// <returns></returns>
        public List<int> GetCardIDsInLists(List<int> cardListIDs)
        {
            //NOTE! Using joins in SQLite is very slow so this method has been written for performance reasons

            List<int> cardIDs = new List<int>();
            dsLanguageData.CardListDataDataTable dtCardListData = new dsLanguageData.CardListDataDataTable();

            if (cardListIDs.Count < 1)
                return cardIDs;

            string ids = string.Empty;
            foreach (int id in cardListIDs)
            {
                ids += ", " + id.ToString();
            }

            ids = ids.Remove(0, 1);

            string sql = string.Format(@"SELECT DISTINCT * FROM CardListData 
                        WHERE (CardListData.CardListID IN ({0}))", ids);
            SQLiteCommand cmd = new SQLiteCommand(sql, new SQLiteConnection(conString));
            System.Data.SQLite.SQLiteDataAdapter cardListDataAdapter = new SQLiteDataAdapter(cmd);
            cardListDataAdapter.Fill(dtCardListData);

            foreach (dsLanguageData.CardListDataRow row in dtCardListData)
            {
                cardIDs.Add(row.CardID);
            }

            return cardIDs;
        }

        /// <summary>
        /// Gets all the cards in the desired CardLists
        /// </summary>
        /// <param name="cardListIDs"></param>
        /// <returns></returns>
        public dsLanguageData.CardDataTable GetCardsInCardLists(List<int> cardListIDs)
        {
            //NOTE! Using joins in SQLite is very slow so this method has been written for performance reasons
            
            dsLanguageData.CardDataTable dtCards = new dsLanguageData.CardDataTable();

            List<int> cardIDs = GetCardIDsInLists(cardListIDs);
            return GetCardsByIDs(cardIDs);                                                                      
        }

        /// <summary>
        /// Gets all the cards in the desired CardLists
        /// </summary>
        /// <param name="cardListIDs"></param>
        /// <returns></returns>
        public dsLanguageData.CardDataTable GetCardsWhere(string whereClause)
        {
            dsLanguageData.CardDataTable cards = new dsLanguageData.CardDataTable();
            
            whereClause = whereClause.Trim();
            if (!whereClause.StartsWith("WHERE", StringComparison.CurrentCultureIgnoreCase))
                whereClause = "WHERE " + whereClause;

            string sql = string.Format(@"SELECT Card.ID, Card.Question, Card.Answer, Card.Notes, Card.[Count], Card.DateCreated, Card.LastModified, Card.MarkForReview, Card.Difficulty
                            FROM Card {0}", whereClause);
            SQLiteCommand cmd = new SQLiteCommand(sql, new SQLiteConnection(conString));
            System.Data.SQLite.SQLiteDataAdapter cardAdapter = new SQLiteDataAdapter(cmd);

            cardAdapter.Fill(cards);
            return cards;
        }

        /// <summary>
        /// Gets all the cards in the desired CardLists
        /// </summary>
        /// <param name="cardListIDs"></param>
        /// <returns></returns>
        public dsLanguageData.CardDataTable GetCardsBeginningWith(string letter, string column)
        {
            dsLanguageData.CardDataTable cards = new dsLanguageData.CardDataTable();                        

            string sql = string.Format(@"SELECT ID, Question, Answer, Notes, [Count], DateCreated, LastModified, MarkForReview, Difficulty
                            FROM Card WHERE {0} like '{1}%' Order By {0}", column, letter);

            SQLiteCommand cmd = new SQLiteCommand(sql, new SQLiteConnection(conString));
            System.Data.SQLite.SQLiteDataAdapter cardAdapter = new SQLiteDataAdapter(cmd);

            cardAdapter.Fill(cards);
            return cards;
        }


        /// <summary>
        /// Returns true if the Card already has an image associated with it
        /// </summary>
        /// <param name="cardID"></param>
        /// <returns></returns>
        public bool CardHasPicture(int cardID)
        {
            dsLanguageData.PictureDataTable dtPic = daPicture.GetDataByCardID(cardID);

            if (dtPic.Rows.Count > 0 && dtPic[0].Image.Length > 0)
                return true;
            
            return false;            
        }

        /// <summary>
        /// Gets the picture associated with the card. Returns null if no pic is found.
        /// </summary>
        /// <param name="cardID"></param>
        /// <returns></returns>
        public Image GetPictureForCard(int cardID)
        {
            dsLanguageData.PictureDataTable dtPic = daPicture.GetDataByCardID(cardID);

            if (dtPic.Rows.Count > 0 && dtPic[0].Image.Length > 0)
                return ByteArrayToImage(dtPic[0].Image);

            return null;
        }

        /// <summary>
        /// Returns true if the Card already has an image associated with it
        /// </summary>
        /// <param name="cardID"></param>
        /// <returns></returns>
        public bool CardHasSound(int cardID)
        {
            dsLanguageData.SoundClipDataTable dtSound = daSoundClip.GetDataByCardID(cardID);

            if (dtSound.Rows.Count > 0 && dtSound[0].SoundClip.Length > 0)
                return true;

            return false;
        }

        
        #endregion

        #region Private Methods
        int getOrCreateDefaultImportCollectionID()
        {
            dsLanguageData.CollectionDataTable col = daCollection.GetData();
            if (col.Rows.Count > 0)
                return col[0].ID;

            daCollection.Insert(1, "Imported", DateTime.Now, DateTime.Now, false);
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

        /// <summary>
        /// Imports data from the DB whether it is in the old format or the new
        /// </summary>
        /// <param name="conString"></param>
        /// <param name="importMarks"></param>
        /// <returns></returns>
        public int SmartImportFromDB(string conString, bool importMarks)
        {
            if (conString.EndsWith("sqlite"))
                return ImportFromOldDB(conString, importMarks);
            else if (conString.EndsWith("s3db"))
                return ImportFromNewDB(conString, importMarks);

            return 0;
        }

        public int ImportFromNewDB(string conString, bool importMarks)
        {
            //Open the other DB
            if (conString == ConnectionString || !conString.EndsWith("s3db"))
                throw new InvalidDataException("Invalid Database");
            
            int affectedRows = 0;
            LanguageData otherDBLayer = new LanguageData(conString);
            Dictionary<int, int> cardIDHashTable = new Dictionary<int, int>();

            int totalRows = int.Parse(otherDBLayer.daCard.Count().ToString());
            totalRows += int.Parse(otherDBLayer.daCardList.Count().ToString());
            totalRows += int.Parse(otherDBLayer.daCollection.Count().ToString());
            totalRows += int.Parse(otherDBLayer.daDictionary.Count().ToString());

            int progress = 0;

            //Import the Cards                       
            foreach (dsLanguageData.CardRow otherCard in otherDBLayer.daCard.GetData())
            {
                progress++;

                //Check for dups 
                dsLanguageData.CardDataTable dtCard = daCard.GetDataByCardInfo(otherCard.Question, otherCard.Answer);
                if (dtCard.Rows.Count > 0)
                {
                    //Don't insert it, just add the ID to the Dictionary
                    cardIDHashTable.Add(otherCard.ID, dtCard[0].ID);
                    DataImported(new DataImportedEventArgs(true, string.Format("Skipped Card: {0}", otherCard.Answer), progress, totalRows));                    
                }
                else
                {
                    //insert the card and add the ID to the Dictionary
                    dsLanguageData.CardRow tmpCard = dsLanguageData.CardDataTable.CloneRow(otherCard, new dsLanguageData.CardDataTable());                    
                    tmpCard.ID = -1;

                    if (!importMarks)
                    {
                        tmpCard.ClearMarks();
                    }

                    affectedRows += InsertOrUpdateCard(tmpCard);
                    cardIDHashTable.Add(otherCard.ID, tmpCard.ID);
                    DataImported(new DataImportedEventArgs(true, string.Format("Added Card: {0}", tmpCard.Answer), progress, totalRows));                    

                    //insert the soundfile
                    if (otherDBLayer.CardHasSound(otherCard.ID))
                    {
                        dsLanguageData.SoundClipRow otherSoundRow = otherDBLayer.daSoundClip.GetDataByCardID(otherCard.ID)[0];
                        dsLanguageData.SoundClipRow tmpSound = dsLanguageData.SoundClipDataTable.CloneRow(otherSoundRow);
                        tmpSound.ID = -1;

                        affectedRows += InsertUpdateOrDeleteSoundClip(tmpSound);
                    }
                    
                    //insert the picture
                    if (otherDBLayer.CardHasPicture(otherCard.ID))
                    {
                        dsLanguageData.PictureRow otherPicRow = otherDBLayer.daPicture.GetDataByCardID(otherCard.ID)[0];
                        affectedRows += InsertUpdatePicture(ByteArrayToImage(otherPicRow.Image), tmpCard.ID, otherPicRow.SearchText);
                    }
                }                
            }//end foreach
            
            //Collection
            Dictionary<int, int> collectionHashTable = new Dictionary<int, int>();
            foreach (dsLanguageData.CollectionRow otherCollection in otherDBLayer.daCollection.GetData())
            {
                progress++;

                //check for dups
                if (daCollection.GetDataByName(otherCollection.Name).Rows.Count > 0)
                {
                    collectionHashTable.Add(otherCollection.ID, daCollection.GetDataByName(otherCollection.Name)[0].ID);
                    DataImported(new DataImportedEventArgs(true, string.Format("Skipped Collection: {0}", otherCollection.Name), progress, totalRows));                
                }
                else
                {
                    //Insert the new collection
                    dsLanguageData.CollectionRow tmpCollection = dsLanguageData.CollectionDataTable.CloneRow(otherCollection);
                    tmpCollection.ID = -1;

                    affectedRows += InsertCollection(tmpCollection);
                    collectionHashTable.Add(otherCollection.ID, tmpCollection.ID);
                    DataImported(new DataImportedEventArgs(true, string.Format("Added Collection: {0}", tmpCollection.Name), progress, totalRows));
                }                
            }

            //CardList
            Dictionary<int, int> cardListHashTable = new Dictionary<int, int>();
            foreach (dsLanguageData.CardListRow otherCardList in otherDBLayer.daCardList.GetData())
            {
                progress++;

                //Check for dups
                if (daCardList.GetDataByName(otherCardList.Name).Rows.Count > 0)
                {
                    cardListHashTable.Add(otherCardList.ID, daCardList.GetDataByName(otherCardList.Name)[0].ID);
                    DataImported(new DataImportedEventArgs(true, string.Format("Skipped CardList: {0}", otherCardList.Name), progress, totalRows));                
                }
                else
                {
                    //insert the new list
                    dsLanguageData.CardListRow tmpCardList = dsLanguageData.CardListDataTable.CloneRow(otherCardList);
                    tmpCardList.ID = -1;
                    tmpCardList.CollectionID = collectionHashTable[otherCardList.CollectionID];

                    affectedRows += InsertCardList(tmpCardList);
                    cardListHashTable.Add(otherCardList.ID, tmpCardList.ID);
                    DataImported(new DataImportedEventArgs(true, string.Format("Added CardList: {0}", tmpCardList.Name), progress, totalRows));
                }
                
                //CardListData
                foreach (dsLanguageData.CardListDataRow otherCardListData in otherDBLayer.daCardListData.GetDataByCardListID(otherCardList.ID))
                {
                    //If there if it is not already there
                    if (daCardListData.GetDataByIDs(cardListHashTable[otherCardList.ID], cardIDHashTable[otherCardListData.CardID]).Rows.Count < 1)
                    {
                        //add the cardID to the list
                        affectedRows += InsertCardListDataItem(cardListHashTable[otherCardList.ID], cardIDHashTable[otherCardListData.CardID]);
                    }
                }//end foreach
            
            }//end foreach

            
            //Dictionry
            foreach (dsLanguageData.DictionaryRow otherDictionary in otherDBLayer.daDictionary.GetData())
            {
                progress++;

                //if it is not a duplicate
                if (daDictionary.GetDataByName(otherDictionary.Name).Rows.Count < 1)
                {
                    //insert it
                    dsLanguageData.DictionaryRow tmpDictionary = dsLanguageData.DictionaryDataTable.CloneRow(otherDictionary);
                    tmpDictionary.ID = -1;

                    affectedRows += InsertOrUpdateDictionary(tmpDictionary);
                    DataImported(new DataImportedEventArgs(true, string.Format("Added Dictionary: {0}", tmpDictionary.Name), progress, totalRows));
                }
                else
                {
                    DataImported(new DataImportedEventArgs(true, string.Format("Skipped Dictionary: {0}", otherDictionary.Name), progress, totalRows));                
                }
            }
            
            return affectedRows;
        }

        public int ImportFromOldDB(string oldDBConString, bool importMarks)
        {
            int affectedRows = 0;
            //LangLearner.Data.LLSqlite oldSqlite = new LangLearner.Data.LLSqlite(oldDBConString);
            //LangLearner.Data.dsLanguageDB.CardListDataTable dtOldCardList = oldSqlite.GetCardLists();            
            //dsLanguageData dsNewData = new dsLanguageData();
            //int defaultCollectionID = getOrCreateDefaultImportCollectionID();

            ////Go through each old card list and add any missing cards
            //int progressIndex = 0;
            //foreach (LangLearner.Data.dsLanguageDB.CardListRow oldCardListRow in dtOldCardList)
            //{
            //    progressIndex++;

            //    //First if the Card list is not in the new DB. Add it.
            //    if (daCardList.GetDataByName(oldCardListRow.Name.Trim()).Rows.Count < 1)
            //    {
            //        affectedRows = daCardList.Insert(getNewIdForTable(dsNewData.CardList.TableName), defaultCollectionID, oldCardListRow.Name, DateTime.Now, DateTime.Now);

            //        if (affectedRows < 1)
            //        {
            //            DataImported(new DataImportedEventArgs(false, string.Format("Could not create CardList: {0}", oldCardListRow.Name), progressIndex, dtOldCardList.Rows.Count));
            //            continue;
            //        }
            //        else
            //        {
            //            DataImported(new DataImportedEventArgs(true, string.Format("Added CardList: {0}", oldCardListRow.Name), progressIndex, dtOldCardList.Rows.Count));
            //        }
            //    }
            //    else
            //    {
            //        DataImported(new DataImportedEventArgs(true, string.Format("CardList already exists: {0}. Checking for new cards...", oldCardListRow.Name), progressIndex, dtOldCardList.Rows.Count));
            //    }
                               
            //    int cardListID = (int)daCardList.ScalarQueryGetIDByName(oldCardListRow.Name.Trim());

            //    //Get the old cards that were in this playlist
            //    List<string> oldCardListGuid = new List<string>();
            //    oldCardListGuid.Add(oldCardListRow.Guid);
            //    LangLearner.Data.dsLanguageDB.CardDataTable dtOldCards = oldSqlite.GetCardsInLists(oldCardListGuid);

            //    //Now import the cards
            //    foreach (LangLearner.Data.dsLanguageDB.CardRow oldCardRow in dtOldCards)
            //    {
            //        //Insert new cards if they don't exists                                        
            //        dsLanguageData.CardDataTable dtNewCard = daCard.GetDataByCardInfo(oldCardRow.Question, oldCardRow.Answer);                    
            //        if (dtNewCard.Rows.Count < 1)
            //        {
            //            bool markForReview = false;
            //            int difficulty = 1;
                        
            //            if (importMarks)
            //            {
            //                markForReview = oldCardRow.IsMarkedForReview;
            //                if (oldCardRow.IsEasy)
            //                    difficulty = 0;
            //            }

            //            affectedRows += daCard.Insert(getNewIdForTable(new dsLanguageData.CardDataTable().TableName), oldCardRow.Question, oldCardRow.Answer, oldCardRow.Example, oldCardRow.Count, oldCardRow.DateCreated, DateTime.Now, markForReview, difficulty);
            //            //TODO error handling
            //        }

            //        int cardID = daCard.GetDataByCardInfo(oldCardRow.Question, oldCardRow.Answer)[0].ID;                                                         

            //        //Check for and import the associated SoundClip
            //        if (oldCardRow.SoundFileGuid != null && oldCardRow.SoundFileGuid != Guid.Empty.ToString())
            //        {
            //            dsLanguageData.SoundClipDataTable dtSoundClip = daSoundClip.GetDataByCardID(cardID);
            //            if (dtSoundClip.Rows.Count < 1)
            //            {
            //                //Convert the old sound row in put in the new one
            //                LangLearner.Data.dsLanguageDB.SoundFileRow oldSoundRow = oldSqlite.GetSoundFile(oldCardRow.SoundFileGuid);
            //                MemoryStream clipStream = streamFromString(oldSoundRow.SoundFile);

            //                affectedRows += daSoundClip.Insert(getNewIdForTable(new dsLanguageData.SoundClipDataTable().TableName), oldSoundRow.Name, clipStream.ToArray(), oldSoundRow.DateCreated, DateTime.Now, cardID);
            //                //TODO error handling
            //            }
            //        }                    

            //        //Make sure the card is added to the current playlist
            //        dsLanguageData.CardListDataDataTable dtCardListData = daCardListData.GetDataByIDs(cardListID, cardID);
            //        if (dtCardListData.Rows.Count < 1)
            //        {
            //            //TODO make CardListData insert, edit position methods
            //            affectedRows += daCardListData.Insert(getNewIdForTable(new dsLanguageData.CardListDataDataTable().TableName), cardListID, cardID, getNewCardListDataIndex(cardListID));
            //            //TODO error handling
            //        }
            //    }
            //}

            return affectedRows;
        }
        #endregion        
    }
}
