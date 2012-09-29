using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LanguageLearner.Data;
using System.Threading;
using System.Net;

namespace LanguageLearner.UI
{
    public partial class GetImages : Form
    {       
        public GetImages()
        {
            InitializeComponent();
            collectionTreeControl.ReLoadNodes();
        }       

        #region CollectionTree and CardListPlayList events

        private void collectionTreeControl_CardsSelected(CollectionTreeControl.CardsSelectedEventArgs e)
        {            
            cardListPlayList.AddOrLoadCards(e.Cards, e.SelectionDescription, e.CardListIDs, AddOrLoad.Add);
        }

        private void cardListPlayList_CardSelected(CardListPlayList.CardSelectedEventArgs e)
        {
            loadCard(e.Card);
        }

        private void cardListPlayList_PlayListChanged(CardListPlayList.PlayListChangedEventArgs e)
        {
            if (e.Action == PlaylistAction.Loaded)
                getImageControl1.LoadImagesForCards(cardListPlayList.Cards);
            if (e.Action == PlaylistAction.Deleted)
                clearCard();
        }

        #endregion

        #region Card Preview

        dsLanguageData.CardRow _curCard;

        void loadCard(dsLanguageData.CardRow card)
        {
            clearCard();

            _curCard = card;

            if (_curCard != null)
            {
                txtAnswer.Text = _curCard.Answer;
                txtQuestion.Text = _curCard.Question;

                LanguageData dataLayer = new LanguageData();
                dsLanguageData.PictureDataTable dtPic = dataLayer.daPicture.GetDataByCardID(_curCard.ID);
                if (dtPic != null && dtPic.Rows.Count > 0 && dtPic[0].Image.Length > 0)
                {
                    cardPreviewPictureBox.Image = dataLayer.ByteArrayToImage(dtPic[0].Image);
                }
            }
        }

        void clearCard()
        {
            _curCard = null;
            txtQuestion.Text = string.Empty;
            txtAnswer.Text = string.Empty;
            cardPreviewPictureBox.Image = null;
        }

        private void btnDeletePicture_Click(object sender, EventArgs e)
        {
            if (_curCard != null)
            {
                LanguageData.DataLayer.daPicture.DeleteByCardID(_curCard.ID);
                cardPreviewPictureBox.Image = null;
            }
        }

        private void btnSavePicture_Click(object sender, EventArgs e)
        {
            LanguageData dataLayer = new LanguageData();
            if (_curCard != null && cardPreviewPictureBox.Image != null && dataLayer.ImageToByteArray(cardPreviewPictureBox.Image).Length > 0)
                dataLayer.InsertUpdatePicture(cardPreviewPictureBox.Image, _curCard.ID, _curCard.Answer);

        }

        private void btnRemoveCardFromList_Click(object sender, EventArgs e)
        {
            if (_curCard != null && _curCard.ID > 0)
            {
                cardListPlayList.RemoveCardFromTheList(_curCard.ID);
                clearCard();
            }
        } 
        #endregion               

        #region ImagePreview events
  
        private void getImageControl1_ImagePreviewWasHidden(object sender, EventArgs e)
        {
            //Remove it from the Selected Cards list
            cardListPlayList.RemoveCardsWhereAnswerIs(((ImagePreviewControl)sender).Title);
        }

        private void getImageControl1_ImagePreviewSelected(object sender, EventArgs e)
        {
            //Select the picture if a card is shown
            if (_curCard != null && _curCard.ID > 0)
            {
                cardPreviewPictureBox.Image = ((ImagePreviewControl)sender).Image;
            }
        }
        #endregion
        
        #region Button Events

        private void btnViewAllImages_Click(object sender, EventArgs e)
        {
            getImageControl1.LoadImagesForCards(cardListPlayList.Cards);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            getImageControl1.DownloadAndSaveImagesForCards(cardListPlayList.Cards);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            getImageControl1.StopDownloading();
        }
        #endregion
        
    }
}