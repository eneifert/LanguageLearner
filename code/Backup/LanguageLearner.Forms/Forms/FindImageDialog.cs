using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LanguageLearner.UI
{
    public partial class FindImageDialog : Form
    {
        public Image SelectedImage;

        public string SearchText
        {
            get { return getImageControl1.SearchText; }
            set { getImageControl1.SearchText = value; }
        }

        public FindImageDialog()
        {
            InitializeComponent();
        }

        public FindImageDialog(string searchText, int searchResults)
        {
            InitializeComponent();

            SearchText = searchText;
            getImageControl1.MaxSearchResults = searchResults;
        }

        public event EventHandler ImagePreviewSelected;       

        private void getImageControl1_ImagePreviewSelected(object sender, EventArgs e)
        {
            SelectedImage = ((ImagePreviewControl)sender).Image;

            if (ImagePreviewSelected != null)
                ImagePreviewSelected(sender, new EventArgs());

            this.Close();
        }
    }
}