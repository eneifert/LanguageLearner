namespace LanguageLearner.UI
{
    partial class GetImages
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GetImages));
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.cardPanel = new System.Windows.Forms.Panel();
            this.btnRemoveCardFromList = new System.Windows.Forms.Button();
            this.btnSavePicture = new System.Windows.Forms.Button();
            this.btnDeletePicture = new System.Windows.Forms.Button();
            this.txtAnswer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cardPreviewPictureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtQuestion = new System.Windows.Forms.TextBox();
            this.getImageControl1 = new LanguageLearner.UI.GetImageControl();
            this.cardListPlayList = new LanguageLearner.UI.CardListPlayList();
            this.collectionTreeControl = new LanguageLearner.UI.CollectionTreeControl();
            this.btnViewAllImages = new System.Windows.Forms.Button();
            this.cardPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cardPreviewPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(863, 696);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(109, 28);
            this.btnStart.TabIndex = 9;
            this.btnStart.Text = "Download All";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(743, 696);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(115, 28);
            this.btnStop.TabIndex = 25;
            this.btnStop.Text = "Stop Dowloading";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // cardPanel
            // 
            this.cardPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardPanel.Controls.Add(this.btnRemoveCardFromList);
            this.cardPanel.Controls.Add(this.btnSavePicture);
            this.cardPanel.Controls.Add(this.btnDeletePicture);
            this.cardPanel.Controls.Add(this.txtAnswer);
            this.cardPanel.Controls.Add(this.label3);
            this.cardPanel.Controls.Add(this.cardPreviewPictureBox);
            this.cardPanel.Controls.Add(this.label1);
            this.cardPanel.Controls.Add(this.txtQuestion);
            this.cardPanel.Location = new System.Drawing.Point(247, 12);
            this.cardPanel.Name = "cardPanel";
            this.cardPanel.Size = new System.Drawing.Size(478, 141);
            this.cardPanel.TabIndex = 28;
            // 
            // btnRemoveCardFromList
            // 
            this.btnRemoveCardFromList.Location = new System.Drawing.Point(229, 108);
            this.btnRemoveCardFromList.Name = "btnRemoveCardFromList";
            this.btnRemoveCardFromList.Size = new System.Drawing.Size(89, 23);
            this.btnRemoveCardFromList.TabIndex = 9;
            this.btnRemoveCardFromList.Text = "Remove Card";
            this.btnRemoveCardFromList.UseVisualStyleBackColor = true;
            this.btnRemoveCardFromList.Click += new System.EventHandler(this.btnRemoveCardFromList_Click);
            // 
            // btnSavePicture
            // 
            this.btnSavePicture.Location = new System.Drawing.Point(359, 110);
            this.btnSavePicture.Name = "btnSavePicture";
            this.btnSavePicture.Size = new System.Drawing.Size(46, 23);
            this.btnSavePicture.TabIndex = 8;
            this.btnSavePicture.Text = "Save";
            this.btnSavePicture.UseVisualStyleBackColor = true;
            this.btnSavePicture.Click += new System.EventHandler(this.btnSavePicture_Click);
            // 
            // btnDeletePicture
            // 
            this.btnDeletePicture.Location = new System.Drawing.Point(413, 110);
            this.btnDeletePicture.Name = "btnDeletePicture";
            this.btnDeletePicture.Size = new System.Drawing.Size(46, 23);
            this.btnDeletePicture.TabIndex = 7;
            this.btnDeletePicture.Text = "Delete";
            this.btnDeletePicture.UseVisualStyleBackColor = true;
            this.btnDeletePicture.Click += new System.EventHandler(this.btnDeletePicture_Click);
            // 
            // txtAnswer
            // 
            this.txtAnswer.Location = new System.Drawing.Point(9, 24);
            this.txtAnswer.Multiline = true;
            this.txtAnswer.Name = "txtAnswer";
            this.txtAnswer.ReadOnly = true;
            this.txtAnswer.Size = new System.Drawing.Size(309, 26);
            this.txtAnswer.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Answer";
            // 
            // cardPreviewPictureBox
            // 
            this.cardPreviewPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardPreviewPictureBox.Location = new System.Drawing.Point(359, 4);
            this.cardPreviewPictureBox.Name = "cardPreviewPictureBox";
            this.cardPreviewPictureBox.Size = new System.Drawing.Size(100, 100);
            this.cardPreviewPictureBox.TabIndex = 4;
            this.cardPreviewPictureBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Question";
            // 
            // txtQuestion
            // 
            this.txtQuestion.Location = new System.Drawing.Point(9, 76);
            this.txtQuestion.Multiline = true;
            this.txtQuestion.Name = "txtQuestion";
            this.txtQuestion.ReadOnly = true;
            this.txtQuestion.Size = new System.Drawing.Size(309, 26);
            this.txtQuestion.TabIndex = 2;
            // 
            // getImageControl1
            // 
            this.getImageControl1.BackColor = System.Drawing.Color.Transparent;
            this.getImageControl1.LoadingPanelIsVisible = false;
            this.getImageControl1.Location = new System.Drawing.Point(247, 169);
            this.getImageControl1.MaxSearchResults = 6;
            this.getImageControl1.Name = "getImageControl1";
            this.getImageControl1.SearchText = "";
            this.getImageControl1.Size = new System.Drawing.Size(478, 558);
            this.getImageControl1.TabIndex = 29;
            this.getImageControl1.ImagePreviewSelected += new System.EventHandler(this.getImageControl1_ImagePreviewSelected);
            this.getImageControl1.ImagePreviewWasHidden += new System.EventHandler(this.getImageControl1_ImagePreviewWasHidden);
            // 
            // cardListPlayList
            // 
            this.cardListPlayList.AllowDrop = true;
            this.cardListPlayList.HeadingText = "Selected Cards";
            this.cardListPlayList.HideFilter = true;
            this.cardListPlayList.Location = new System.Drawing.Point(740, 12);
            this.cardListPlayList.Name = "cardListPlayList";
            this.cardListPlayList.Size = new System.Drawing.Size(232, 644);
            this.cardListPlayList.SwitchQuestionAndAnswerText = true;
            this.cardListPlayList.TabIndex = 26;
            this.cardListPlayList.PlayListChanged += new LanguageLearner.UI.CardListPlayList.PlayListChangedEventHandler(this.cardListPlayList_PlayListChanged);
            this.cardListPlayList.CardSelected += new LanguageLearner.UI.CardListPlayList.CardSelectedEventHandler(this.cardListPlayList_CardSelected);
            // 
            // collectionTreeControl
            // 
            this.collectionTreeControl.DisableModificationButtons = true;
            this.collectionTreeControl.DragDropAction = LanguageLearner.UI.CardDragDropAction.Add;
            this.collectionTreeControl.HideButtons = false;
            this.collectionTreeControl.Location = new System.Drawing.Point(12, 12);
            this.collectionTreeControl.Name = "collectionTreeControl";
            this.collectionTreeControl.Size = new System.Drawing.Size(229, 715);
            this.collectionTreeControl.TabIndex = 0;
            this.collectionTreeControl.CardsSelected += new LanguageLearner.UI.CollectionTreeControl.CardsSelectedEventHandler(this.collectionTreeControl_CardsSelected);
            // 
            // btnViewAllImages
            // 
            this.btnViewAllImages.Location = new System.Drawing.Point(743, 662);
            this.btnViewAllImages.Name = "btnViewAllImages";
            this.btnViewAllImages.Size = new System.Drawing.Size(229, 28);
            this.btnViewAllImages.TabIndex = 30;
            this.btnViewAllImages.Text = "View Selected Cards";
            this.btnViewAllImages.UseVisualStyleBackColor = true;
            this.btnViewAllImages.Click += new System.EventHandler(this.btnViewAllImages_Click);
            // 
            // GetImages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(981, 730);
            this.Controls.Add(this.btnViewAllImages);
            this.Controls.Add(this.getImageControl1);
            this.Controls.Add(this.cardPanel);
            this.Controls.Add(this.cardListPlayList);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.collectionTreeControl);
            this.Name = "GetImages";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Download Images From The Web!";
            this.cardPanel.ResumeLayout(false);
            this.cardPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cardPreviewPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CollectionTreeControl collectionTreeControl;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private CardListPlayList cardListPlayList;
        private System.Windows.Forms.Panel cardPanel;
        private System.Windows.Forms.Button btnSavePicture;
        private System.Windows.Forms.Button btnDeletePicture;
        private System.Windows.Forms.TextBox txtAnswer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox cardPreviewPictureBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtQuestion;
        private GetImageControl getImageControl1;
        private System.Windows.Forms.Button btnRemoveCardFromList;
        private System.Windows.Forms.Button btnViewAllImages;
    }
}