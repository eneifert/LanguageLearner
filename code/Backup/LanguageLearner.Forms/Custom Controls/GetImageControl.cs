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
    public partial class GetImageControl : UserControl
    {
        WebImageThreadManager _WTIM_SearchFor;
        WebImageThreadManager _WTIM_GetForCards;
        List<ImageRecievedCallbackItem> errors;

        #region Properties 

        /// <summary>
        /// Gets or sets the text to search for
        /// </summary>
        public string SearchText
        {
            get { return txtSearchFor.Text; }
            set
            {
                txtSearchFor.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the max number of search results
        /// </summary>
        public int MaxSearchResults
        {
            get { return int.Parse(txtMaxResults.Text); }
            set 
            {
                if (value > 0)
                {
                    txtMaxResults.Text = value.ToString();
                }
                else
                {
                    txtMaxResults.Text = "1";
                }
            }
        }

        /// <summary>
        /// Gets or sets whether the LoadingPanel is visible.       
        /// </summary>
        public bool LoadingPanelIsVisible
        {
            get { return !resultsFlowLayoutPanel.Visible; }
            set 
            {
                if (resultsFlowLayoutPanel.Visible && value)
                    resultsFlowLayoutPanel.Controls.Clear();

                resultsFlowLayoutPanel.Visible = !value;                 
            }
        }
        #endregion

        public GetImageControl()
        {
            InitializeComponent();           
        }

        ~GetImageControl()
        {
            StopDownloading();
        }

        #region Public Methods

        /// <summary>
        /// Adds an image preview to the results panel.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="title"></param>
        /// <param name="showTitle"></param>
        /// <param name="showButtons"></param>
        public void AddImagePreview(Image image, string title, bool showTitle, bool showButtons)
        {
            ImagePreviewControl pr = new ImagePreviewControl(image, title, showTitle, showButtons);
            pr.ImageDoubleClick += new EventHandler(imagePreview_DoubleClick);
            pr.KeepImage += new EventHandler(this.imagePreview_KeepImage);
            pr.ReplaceImage += new EventHandler(this.imagePreview_ReplaceImage);

            resultsFlowLayoutPanel.Controls.Add(pr);
        }

        /// <summary>
        /// Loads all of the cards images in the results panel
        /// </summary>
        /// <param name="cards"></param>
        public void LoadImagesForCards(dsLanguageData.CardDataTable cards)
        {
            resultsFlowLayoutPanel.Controls.Clear();

            LanguageData dataLayer = new LanguageData();

            resultsFlowLayoutPanel.SuspendLayout();

            foreach (dsLanguageData.CardRow card in cards)
            {
                Image img = dataLayer.GetPictureForCard(card.ID);
                AddImagePreview(img, card.Answer, true, true);
            }

            resultsFlowLayoutPanel.ResumeLayout();
        }

        /// <summary>
        /// Starts searching for the cards.
        /// </summary>
        public void StartSearching()
        {            
            if (txtSearchFor.Text.Trim().Length < 1)
            {
                MessageBox.Show("Enter what you want to search for.");
                return;
            }

            int maxResults;
            if (!int.TryParse(txtMaxResults.Text, out maxResults))
            {
                MessageBox.Show("# of Results can not be blank");
                return;
            }

            List<WebImageSearchItem> items = new List<WebImageSearchItem>();
            string[] values = txtSearchFor.Text.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in values)
            {
                if (s.Trim().Length > 0)
                    items.Add(new WebImageSearchItem(s));
            }

            progressBar.Maximum = items.Count;
            progressBar.Step = 1;
            progressBar.Value = 0;

            if (items.Count > 0)
            {
                lblStatus.Text = "Downloading... Please Wait...";
            }
            else
            {
                lblStatus.Text = "Nothing to search for.";
                return;
            }

            LoadingPanelIsVisible = true;
            loadingSmallPictureBox.Visible = true;

            errors = new List<ImageRecievedCallbackItem>();

            //Get the images
            _WTIM_SearchFor = WebImage.ProcessImageBatchWithThreadPool(items, maxResults, wTIM_SearchForCallback, 5);          
        }

        /// <summary>
        /// Download and save images for a group of cards by matching images with the answer text.         
        /// </summary>
        /// <param name="cards"></param>
        public void DownloadAndSaveImagesForCards(dsLanguageData.CardDataTable cards)
        {            
            //If it is already working.
            if (_WTIM_GetForCards != null && _WTIM_GetForCards.IsWorking)
            {
                return;
            }

            List<WebImageSearchItem> searchItems = new List<WebImageSearchItem>();
            LanguageData dataLayer = new LanguageData();
            foreach (dsLanguageData.CardRow card in cards)
            {
                if (!dataLayer.CardHasPicture(card.ID))
                    searchItems.Add(new WebImageSearchItem(card.ID, card.Answer));
            }

            progressBar.Maximum = searchItems.Count;
            progressBar.Step = 1;
            progressBar.Value = 0;

            if (searchItems.Count > 0)
            {
                lblStatus.Text = "Downloading... Please Wait...";                
            }
            else
            {
                lblStatus.Text = "All done. No need to get any pictures";                

                LoadImagesForCards(cards);

                return;
            }

            LoadingPanelIsVisible = true;
            loadingSmallPictureBox.Visible = true;

            errors = new List<ImageRecievedCallbackItem>();

            _WTIM_GetForCards = WebImage.ProcessImageBatchWithThreadPool(searchItems, wTIM_GetForSelectedCardsCallback, 5);
        }


        /// <summary>
        /// Stops any running threads downloading images
        /// </summary>
        public void StopDownloading()
        {
            if (_WTIM_SearchFor != null && _WTIM_SearchFor.IsWorking)
            {
                _WTIM_SearchFor.StopThreads();
            }

            if (_WTIM_GetForCards != null && _WTIM_GetForCards.IsWorking)
            {
                _WTIM_GetForCards.StopThreads();
            }

            resultsFlowLayoutPanel.Visible = true;
            loadingSmallPictureBox.Visible = false;
            progressBar.Value = 0;
            lblStatus.Text = "Status:";
        }
        
        #endregion                

        #region Thread callback and processing methods

        void wTIM_SearchForCallback(ImageRecievedCallbackItem imageItem)
        {            
            if (!imageItem.Success)
            {
                //Handle the error
                errors.Add(imageItem);            
            }

            this.Invoke(new UpdateAfterImagesWereFoundCallback(updateAfterImagesWereFound), new object[] { imageItem });
        }

        void addImageToResults(Image image, string searchText)
        {
            AddImagePreview(image, searchText, true, false);
        }

        delegate void UpdateAfterImagesWereFoundCallback(ImageRecievedCallbackItem imageItem);

        void updateAfterImagesWereFound(ImageRecievedCallbackItem imageItem)
        {
            updateStatusAndProgressBar(imageItem);

            if (imageItem.Success)
            {
                foreach (Image image in imageItem.Images)
                {
                    addImageToResults(image, imageItem.SearchItem.SearchText);
                }                
            }            
        }

        void wTIM_GetForSelectedCardsCallback(ImageRecievedCallbackItem imageItem)
        {
            if (imageItem.Success)
            {
                //Save the image
                LanguageData dataLayer = new LanguageData();
                dataLayer.InsertUpdatePicture(imageItem.Images[0], imageItem.SearchItem.ID, imageItem.SearchItem.SearchText);
            }
            else
            {
                //Handle the error
                errors.Add(imageItem);
            }

            //update the screen
            this.Invoke(new UpdateAfterImageDownloadedAndSavedCallback(updateAfterImageDownloadedAndSaved), new object[] { imageItem });
        }        

        delegate void UpdateAfterImageDownloadedAndSavedCallback(ImageRecievedCallbackItem imageItem);

        void updateAfterImageDownloadedAndSaved(ImageRecievedCallbackItem imageItem)
        {
            updateStatusAndProgressBar(imageItem);

            Image downloadedImage = null;
            if (imageItem.Success)
                downloadedImage = imageItem.Images[0];

            AddImagePreview(downloadedImage, imageItem.SearchItem.SearchText, true, true);
        }

        void updateStatusAndProgressBar(ImageRecievedCallbackItem imageItem)
        {            
            if (LoadingPanelIsVisible)
                LoadingPanelIsVisible = false;

            if (imageItem.Success)
            {
                progressBar.Value = imageItem.ProcessedCount;
                lblStatus.Text = string.Format("Downloaded: {0} of {1}", imageItem.ProcessedCount, imageItem.Total);
            }
            else
            {
                progressBar.Value = imageItem.ProcessedCount;
                lblStatus.Text = imageItem.ErrorMsg;
            }
           
            if (imageItem.ProcessedCount == imageItem.Total)
            {
                //update the screen                
                if(lblStatus.Text != WebImage.InternetConnectionErrorMessage)
                    lblStatus.Text = string.Format("Finished with {0} errors.", errors.Count);
                
                progressBar.Value = 0;                
                loadingSmallPictureBox.Visible = false;
            }
        }
        
        #endregion
        
        #region Button Events

        private void btnSearchFor_Click(object sender, EventArgs e)
        {
            StartSearching();
        }
        
        private void btnCancelSearch_Click(object sender, EventArgs e)
        {
            StopDownloading();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearchFor.Text = string.Empty;
            resultsFlowLayoutPanel.Controls.Clear();
        }

        #endregion

        #region ImagePreview methods

        public event EventHandler ImagePreviewWasHidden;
        public event EventHandler ImagePreviewSelected;       

        void imagePreview_DoubleClick(object sender, EventArgs e)
        {
            if (ImagePreviewSelected != null)
                ImagePreviewSelected(sender, new EventArgs());
        }

        void imagePreview_ReplaceImage(object sender, EventArgs e)
        {
            //Remove it from the results
            removeImagePreview((ImagePreviewControl)sender);

            //Add the text to the search bar
            if (txtSearchFor.Text.Trim().Length > 1)
                txtSearchFor.Text += " || ";
            txtSearchFor.Text += ((ImagePreviewControl)sender).Title;
        }

        void imagePreview_KeepImage(object sender, EventArgs e)
        {
            //Remove it from the results
            removeImagePreview((ImagePreviewControl)sender);

            if (ImagePreviewWasHidden != null)
                ImagePreviewWasHidden(sender, new EventArgs());
        }        

        void removeImagePreview(ImagePreviewControl imagePreview)
        {
            resultsFlowLayoutPanel.Controls.Remove(imagePreview);
        }
        #endregion                
    }
}
