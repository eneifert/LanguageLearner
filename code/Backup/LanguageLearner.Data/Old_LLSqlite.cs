using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data.Common;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace LangLearner.Data
{
    /// <summary>
    /// This is the old DB interface from version 1. This is here for reference purposes only and should be skipped by the compiler.
    /// </summary>
    public class LLSqlite
    {
        string conString;
        public dsLanguageDBTableAdapters.CardListTableAdapter daCardList = new LangLearner.Data.dsLanguageDBTableAdapters.CardListTableAdapter();
        public dsLanguageDBTableAdapters.CardTableAdapter daCard = new LangLearner.Data.dsLanguageDBTableAdapters.CardTableAdapter();
        public dsLanguageDBTableAdapters.SoundFileTableAdapter daSoundFile = new LangLearner.Data.dsLanguageDBTableAdapters.SoundFileTableAdapter();
        
        string ConnectionString
        {
            get { return conString; }
            set 
            {
                conString = "data source=" + value;
                daCard.Connection.ConnectionString = conString;
                daCardList.Connection.ConnectionString = conString;
                daSoundFile.Connection.ConnectionString = conString;
            }
        }
        
        
        public LLSqlite()
        {
            ConnectionString = string.Format("{0}\\LanguageDB.sqlite", Application.StartupPath);
        }

        public LLSqlite(string dbPath)
        {
            this.ConnectionString = dbPath;                   
        }

        public void CreateDB()
        {
            DbConnection con = new SQLiteConnection(conString);
            DbCommand dbcom = con.CreateCommand();
            
            con.Open();            
            con.Close();
            
        }

        public int InsertCardList(string guid, string name)
        {            
            return daCardList.Insert(guid, name, DateTime.Now, DateTime.Now);            
        }       

        public int InsertCard(dsLanguageDB.CardRow card)
        {
            int i = this.daCard.Insert(card.Guid, card.ListGuid, card.Question, card.Answer, card.Example, card.IsEasy, card.Count, card.SoundFileGuid, card.PictureGuid, card.DateCreated, card.LastModified, card.IsMarkedForReview);
            if (card.SoundFileRow != null && card.SoundFileRow.Guid != Guid.Empty.ToString())
            {
                i += InsertSoundFile(card.SoundFileRow);
            }
            return i;
        }

        public int InsertSoundFile(dsLanguageDB.SoundFileRow sound)
        {
            return this.daSoundFile.Insert(sound.Guid, sound.Name, sound.Name2, sound.SoundFile, sound.DateCreated);                
        }       

        public LangLearner.Data.dsLanguageDB.CardListDataTable GetCardLists()
        {            
            return this.daCardList.GetData();
        }

        public int UpdateCard(dsLanguageDB.CardRow card)
        {
            return daCard.UpdateCard(card.Guid, card.ListGuid, card.Question, card.Answer, card.Example, card.IsEasy, card.Count, card.SoundFileGuid, card.PictureGuid, card.DateCreated, card.LastModified, card.IsMarkedForReview);
        }

        public dsLanguageDB.CardDataTable GetCardsInLists(List<string> cardListGuids)
        {
            return this.daCard.GetDataByListGuids(ToCommaString<string>(cardListGuids));
        }

        public dsLanguageDB.SoundFileRow GetSoundFile(string guid)
        {
            dsLanguageDB.SoundFileDataTable tbl = this.daSoundFile.GetDataByGuid(guid);
            if (tbl.Rows.Count < 1)
                return null;
            return (dsLanguageDB.SoundFileRow)tbl.Rows[0];
        }

        public int DeleteCardList(string guid)
        {            
            int i = daSoundFile.DeleteByListGuid(guid);
            i += daCard.DeleteByListGuid(guid);
            i += daCardList.DeleteListCard(guid);
            return i;
        }

        public int DeleteCard(string guid)
        {
            int i = daSoundFile.DeleteByCardGuid(guid);
            i += daCard.DeleteByCardGuid(guid);
            return i;
        }

        public string ToCommaString<T>(List<T> list)
        {           
            return ToCommaString<T>(list, string.Empty);
        }

        public string ToCommaString<T>(List<T> list, string surroundEachWith)
        {
            string s = string.Empty;

            foreach (T item in list)
            {               
                s += String.Format("{0}{1}{0}, ", surroundEachWith, item.ToString());
            }
            s = s.TrimEnd(new char[] { ',', ' ' });
            return s;
        }

        public void MergeWith(string db)
        {           
            dsLanguageDB ds = GetDataSet(this);
            dsLanguageDB ds2 = GetDataSet(new LLSqlite(db));               

            int i = 0;

            //Insert or Update any changed rows
            foreach (dsLanguageDB.CardListRow row in ds2.CardList)
            {
                 dsLanguageDB.CardListRow r = ds.CardList.FindByGuid(row.Guid);
                 if (r == null)
                 {
                     i += daCardList.Insert(row.Guid, row.Name, row.DateCreated, row.LastModified);
                 }
                 else if(r.LastModified.CompareTo(row.LastModified) < 0)
                 {
                     r.ItemArray = row.ItemArray;
                     i += daCardList.Update(r);
                 }
            }
            foreach (dsLanguageDB.SoundFileRow row in ds2.SoundFile)
            {
                dsLanguageDB.SoundFileRow r = ds.SoundFile.FindByGuid(row.Guid);
                if (r == null)
                {
                    i += daSoundFile.Insert(row.Guid, row.Name, row.Name2, row.SoundFile, row.DateCreated);
                }
                else if (r.DateCreated.CompareTo(row.DateCreated) < 0)
                {
                    r.ItemArray = row.ItemArray;
                    i += daSoundFile.Update(r);
                }
            }
            foreach (dsLanguageDB.CardRow row in ds2.Card)
            {
                dsLanguageDB.CardRow r = ds.Card.FindByGuid(row.Guid);
                if (r == null)
                {
                    i += this.daCard.Insert(row.Guid, row.ListGuid, row.Question, row.Answer, row.Example, row.IsEasy, row.Count, row.SoundFileGuid, row.PictureGuid, row.DateCreated, row.LastModified, row.IsMarkedForReview);
                }
                else if (r.LastModified.CompareTo(row.LastModified) < 0)
                {
                    r.ItemArray = row.ItemArray;
                    i += this.daCard.Update(r);
                }                
            }                  
        }
        dsLanguageDB GetDataSet(LLSqlite lls)
        {
            dsLanguageDB ds = new dsLanguageDB();
            lls.daCardList.Fill(ds.CardList);
            lls.daSoundFile.Fill(ds.SoundFile);
            lls.daCard.Fill(ds.Card);

            return ds;
        }        
        
    }
}
