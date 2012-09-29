namespace LanguageLearner.UI
{
    partial class FindImageDialog
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
            this.getImageControl1 = new LanguageLearner.UI.GetImageControl();
            this.SuspendLayout();
            // 
            // getImageControl1
            // 
            this.getImageControl1.BackColor = System.Drawing.Color.Transparent;
            this.getImageControl1.LoadingPanelIsVisible = false;
            this.getImageControl1.Location = new System.Drawing.Point(3, 4);
            this.getImageControl1.Name = "getImageControl1";
            this.getImageControl1.SearchText = "";
            this.getImageControl1.Size = new System.Drawing.Size(583, 412);
            this.getImageControl1.TabIndex = 0;
            this.getImageControl1.ImagePreviewSelected += new System.EventHandler(this.getImageControl1_ImagePreviewSelected);
            // 
            // FindImageDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(591, 428);
            this.Controls.Add(this.getImageControl1);
            this.Name = "FindImageDialog";
            this.Text = "Find Pictures From The Web";
            this.ResumeLayout(false);

        }

        #endregion

        private GetImageControl getImageControl1;
    }
}