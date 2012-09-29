using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace LanguageLearner.UI
{
    public class CardDragDropHolder
    {
        public List<int> CardIDs;
        public string Description;
        public object Sender;
        public CardDragDropAction Action;
        public List<int> CardListIDs;

        public CardDragDropHolder(object sender, List<int> cardIDs, string description, CardDragDropAction action) 
            :this(sender, cardIDs, description, new List<int>(), action)
        { }

        public CardDragDropHolder(object sender, List<int> cardIDs, string description, List<int> cardListIDs, CardDragDropAction action)
        {
            Sender = sender;
            CardIDs = cardIDs;
            Description = description;
            Action = action;
            CardListIDs = cardListIDs;
        }
    }

    public enum CardDragDropAction
    {
        Add,
        Load
    }
}
