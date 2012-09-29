namespace LanguageLearner.Data {
    using System.Data;
    using System.Collections.Generic;

    public partial class dsLanguageData
    {        
        public partial class CardDataTable
        {     
            public static dsLanguageData.CardRow CloneRow(dsLanguageData.CardRow card, CardDataTable dtCard)
            {
                dsLanguageData.CardRow newRow = dtCard.MakeNewCardRow();
                newRow.ItemArray = card.ItemArray;
                return newRow;
            }

            /// <summary>
            /// Makes a new CardRow and sets everything to its defualt values
            /// </summary>
            /// <returns></returns>
            public dsLanguageData.CardRow MakeNewCardRow()
            {
                dsLanguageData.CardRow card = this.NewCardRow();
                card.ID = -1;
                card.Answer = string.Empty;
                card.Count = 0;
                card.DateCreated = System.DateTime.Now;
                card.Difficulty = 1;
                card.LastModified = System.DateTime.Now;
                card.MarkForReview = false;
                card.Notes = string.Empty;
                card.Question = string.Empty;

                return card;
            }

            public void CloneAndAddRow(dsLanguageData.CardRow card)
            {                
                this.Rows.Add(CardDataTable.CloneRow(card, this));
            }

            public List<int> GetIDList()
            {
                List<int> ids = new List<int>();

                foreach (dsLanguageData.CardRow row in this)
                {
                    ids.Add(row.ID);
                }

                return ids;
            }
        }

        public partial class CardRow
        {
            public void ClearMarks()
            {
                this.Count = 0;
                this.Difficulty = 1;
                this.MarkForReview = false;                
            }
        }

        public partial class SoundClipDataTable
        {
            public static dsLanguageData.SoundClipRow CloneRow(dsLanguageData.SoundClipRow row)
            {
                dsLanguageData.SoundClipRow newRow = new SoundClipDataTable().NewSoundClipRow();                
                newRow.ItemArray = row.ItemArray;
                return newRow;
            }
        }

        public partial class PictureDataTable
        {
            public static dsLanguageData.PictureRow CloneRow(dsLanguageData.PictureRow row)
            {
                dsLanguageData.PictureRow newRow = new PictureDataTable().NewPictureRow();
                newRow.ItemArray = row.ItemArray;
                return newRow;
            }
        }

        public partial class CollectionDataTable
        {
            public static dsLanguageData.CollectionRow CloneRow(dsLanguageData.CollectionRow row)
            {
                dsLanguageData.CollectionRow newRow = new CollectionDataTable().NewCollectionRow();
                newRow.ItemArray = row.ItemArray;
                return newRow;
            }
        }

        public partial class CardListDataTable
        {
            public static dsLanguageData.CardListRow CloneRow(dsLanguageData.CardListRow row)
            {
                dsLanguageData.CardListRow newRow = new CardListDataTable().NewCardListRow();
                newRow.ItemArray = row.ItemArray;
                return newRow;
            }
        }

        public partial class DictionaryDataTable
        {
            public static dsLanguageData.DictionaryRow CloneRow(dsLanguageData.DictionaryRow row)
            {
                dsLanguageData.DictionaryRow newRow = new DictionaryDataTable().NewDictionaryRow();
                newRow.ItemArray = row.ItemArray;
                return newRow;
            }
        }
    }

    
   
}
