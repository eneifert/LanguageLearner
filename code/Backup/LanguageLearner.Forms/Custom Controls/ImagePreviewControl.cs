using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace LanguageLearner.UI
{
    public partial class ImagePreviewControl : UserControl
    {
        string _title;
        Image _image;
        bool _showText = true;
        bool _showButtons = true;

        public event EventHandler ImageDoubleClick;
        public event EventHandler KeepImage;
        public event EventHandler ReplaceImage;

        #region Properties

        [Browsable(true)]
        public bool ShowText
        {
            get { return _showText; }
            set
            {
                _showText = value;
                if (_showText)
                {
                    tableLayoutPanel1.RowStyles[0].Height = 14;
                    
                }
                else
                {
                    tableLayoutPanel1.RowStyles[0].Height = 0;                   
                }
            }
        }

        [Browsable(true)]
        public bool ShowButtons
        {
            get { return _showButtons; }
            set
            {
                _showButtons = value;
                if (_showButtons)
                {
                    tableLayoutPanel1.RowStyles[2].Height = 30;

                }
                else
                {
                    tableLayoutPanel1.RowStyles[2].Height = 0;
                }
            }
        }

        /// <summary>
        /// Gets or sets the text for the image
        /// </summary>
        public string Title
        {
            set
            {
                _title = value;
                lblTitle.Text = _title;
            }
            get { return _title; }
        }

        public Image Image
        {
            set 
            { 
                _image = value;
                pictureBox.Image = _image;
            }
            get { return _image; }
        }
        #endregion

        public ImagePreviewControl()
        {
            InitializeComponent();
        }

        public ImagePreviewControl(Image image, string title, bool showText, bool showButtons)
        {
            InitializeComponent();

            Image = image;
            Title = title;
            ShowText = showText;
            ShowButtons = showButtons;
        }

        #region Events

        private void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            pictureBox.BorderStyle = BorderStyle.Fixed3D;
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pictureBox_DoubleClick(object sender, EventArgs e)
        {
            if (ImageDoubleClick != null)
            {
                ImageDoubleClick(this, new EventArgs());
            }
        }

        #endregion

        private void btnKeep_Click(object sender, EventArgs e)
        {
            if (KeepImage != null)
                KeepImage(this, new EventArgs());
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (ReplaceImage != null)
                ReplaceImage(this, new EventArgs());
        }

    }
}
