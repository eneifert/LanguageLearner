namespace LanguageLearner.UI
{
    partial class GetImageControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GetImageControl));
            this.lblSearchFor = new System.Windows.Forms.Label();
            this.txtSearchFor = new System.Windows.Forms.TextBox();
            this.panelSearchResults = new System.Windows.Forms.Panel();
            this.resultsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.loadingPictureBox = new System.Windows.Forms.PictureBox();
            this.mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.topTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panelTopLeft = new System.Windows.Forms.Panel();
            this.panelTopRight = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblNumberOfResults = new System.Windows.Forms.Label();
            this.txtMaxResults = new System.Windows.Forms.TextBox();
            this.btnCancelSearch = new System.Windows.Forms.Button();
            this.btnSearchFor = new System.Windows.Forms.Button();
            this.infoPanel = new System.Windows.Forms.Panel();
            this.loadingSmallPictureBox = new System.Windows.Forms.PictureBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panelSearchResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadingPictureBox)).BeginInit();
            this.mainTableLayoutPanel.SuspendLayout();
            this.topTableLayoutPanel.SuspendLayout();
            this.panelTopLeft.SuspendLayout();
            this.panelTopRight.SuspendLayout();
            this.infoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadingSmallPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSearchFor
            // 
            this.lblSearchFor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSearchFor.AutoSize = true;
            this.lblSearchFor.Location = new System.Drawing.Point(3, 5);
            this.lblSearchFor.Name = "lblSearchFor";
            this.lblSearchFor.Size = new System.Drawing.Size(62, 13);
            this.lblSearchFor.TabIndex = 35;
            this.lblSearchFor.Text = "Search For:";
            // 
            // txtSearchFor
            // 
            this.txtSearchFor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchFor.Location = new System.Drawing.Point(3, 21);
            this.txtSearchFor.Multiline = true;
            this.txtSearchFor.Name = "txtSearchFor";
            this.txtSearchFor.Size = new System.Drawing.Size(357, 48);
            this.txtSearchFor.TabIndex = 34;
            // 
            // panelSearchResults
            // 
            this.panelSearchResults.BackColor = System.Drawing.Color.White;
            this.panelSearchResults.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSearchResults.Controls.Add(this.resultsFlowLayoutPanel);
            this.panelSearchResults.Controls.Add(this.loadingPictureBox);
            this.panelSearchResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSearchResults.Location = new System.Drawing.Point(3, 88);
            this.panelSearchResults.Name = "panelSearchResults";
            this.panelSearchResults.Size = new System.Drawing.Size(523, 202);
            this.panelSearchResults.TabIndex = 32;
            // 
            // resultsFlowLayoutPanel
            // 
            this.resultsFlowLayoutPanel.AutoScroll = true;
            this.resultsFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.resultsFlowLayoutPanel.Name = "resultsFlowLayoutPanel";
            this.resultsFlowLayoutPanel.Size = new System.Drawing.Size(521, 200);
            this.resultsFlowLayoutPanel.TabIndex = 0;
            // 
            // loadingPictureBox
            // 
            this.loadingPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loadingPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("loadingPictureBox.Image")));
            this.loadingPictureBox.Location = new System.Drawing.Point(0, 0);
            this.loadingPictureBox.Name = "loadingPictureBox";
            this.loadingPictureBox.Size = new System.Drawing.Size(521, 200);
            this.loadingPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.loadingPictureBox.TabIndex = 0;
            this.loadingPictureBox.TabStop = false;
            // 
            // mainTableLayoutPanel
            // 
            this.mainTableLayoutPanel.ColumnCount = 1;
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayoutPanel.Controls.Add(this.panelSearchResults, 0, 1);
            this.mainTableLayoutPanel.Controls.Add(this.topTableLayoutPanel, 0, 0);
            this.mainTableLayoutPanel.Controls.Add(this.infoPanel, 0, 2);
            this.mainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            this.mainTableLayoutPanel.RowCount = 3;
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.mainTableLayoutPanel.Size = new System.Drawing.Size(529, 339);
            this.mainTableLayoutPanel.TabIndex = 39;
            // 
            // topTableLayoutPanel
            // 
            this.topTableLayoutPanel.ColumnCount = 2;
            this.topTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.topTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 154F));
            this.topTableLayoutPanel.Controls.Add(this.panelTopLeft, 0, 0);
            this.topTableLayoutPanel.Controls.Add(this.panelTopRight, 1, 0);
            this.topTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.topTableLayoutPanel.Name = "topTableLayoutPanel";
            this.topTableLayoutPanel.RowCount = 1;
            this.topTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.topTableLayoutPanel.Size = new System.Drawing.Size(523, 79);
            this.topTableLayoutPanel.TabIndex = 33;
            // 
            // panelTopLeft
            // 
            this.panelTopLeft.Controls.Add(this.lblSearchFor);
            this.panelTopLeft.Controls.Add(this.txtSearchFor);
            this.panelTopLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTopLeft.Location = new System.Drawing.Point(3, 3);
            this.panelTopLeft.Name = "panelTopLeft";
            this.panelTopLeft.Size = new System.Drawing.Size(363, 73);
            this.panelTopLeft.TabIndex = 0;
            // 
            // panelTopRight
            // 
            this.panelTopRight.Controls.Add(this.btnClear);
            this.panelTopRight.Controls.Add(this.lblNumberOfResults);
            this.panelTopRight.Controls.Add(this.txtMaxResults);
            this.panelTopRight.Controls.Add(this.btnCancelSearch);
            this.panelTopRight.Controls.Add(this.btnSearchFor);
            this.panelTopRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTopRight.Location = new System.Drawing.Point(372, 3);
            this.panelTopRight.Name = "panelTopRight";
            this.panelTopRight.Size = new System.Drawing.Size(148, 73);
            this.panelTopRight.TabIndex = 1;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(2, 22);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(64, 21);
            this.btnClear.TabIndex = 36;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblNumberOfResults
            // 
            this.lblNumberOfResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNumberOfResults.AutoSize = true;
            this.lblNumberOfResults.Location = new System.Drawing.Point(3, 5);
            this.lblNumberOfResults.Name = "lblNumberOfResults";
            this.lblNumberOfResults.Size = new System.Drawing.Size(94, 13);
            this.lblNumberOfResults.TabIndex = 35;
            this.lblNumberOfResults.Text = "Number of Results";
            // 
            // txtMaxResults
            // 
            this.txtMaxResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaxResults.Location = new System.Drawing.Point(106, 2);
            this.txtMaxResults.Name = "txtMaxResults";
            this.txtMaxResults.Size = new System.Drawing.Size(39, 20);
            this.txtMaxResults.TabIndex = 34;
            this.txtMaxResults.Text = "6";
            // 
            // btnCancelSearch
            // 
            this.btnCancelSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelSearch.Location = new System.Drawing.Point(2, 48);
            this.btnCancelSearch.Name = "btnCancelSearch";
            this.btnCancelSearch.Size = new System.Drawing.Size(64, 21);
            this.btnCancelSearch.TabIndex = 33;
            this.btnCancelSearch.Text = "Cancel";
            this.btnCancelSearch.UseVisualStyleBackColor = true;
            this.btnCancelSearch.Click += new System.EventHandler(this.btnCancelSearch_Click);
            // 
            // btnSearchFor
            // 
            this.btnSearchFor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchFor.Location = new System.Drawing.Point(72, 22);
            this.btnSearchFor.Name = "btnSearchFor";
            this.btnSearchFor.Size = new System.Drawing.Size(73, 47);
            this.btnSearchFor.TabIndex = 32;
            this.btnSearchFor.Text = "Find Images";
            this.btnSearchFor.UseVisualStyleBackColor = true;
            this.btnSearchFor.Click += new System.EventHandler(this.btnSearchFor_Click);
            // 
            // infoPanel
            // 
            this.infoPanel.Controls.Add(this.loadingSmallPictureBox);
            this.infoPanel.Controls.Add(this.progressBar);
            this.infoPanel.Controls.Add(this.lblStatus);
            this.infoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoPanel.Location = new System.Drawing.Point(3, 296);
            this.infoPanel.Name = "infoPanel";
            this.infoPanel.Size = new System.Drawing.Size(523, 40);
            this.infoPanel.TabIndex = 34;
            // 
            // loadingSmallPictureBox
            // 
            this.loadingSmallPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loadingSmallPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("loadingSmallPictureBox.Image")));
            this.loadingSmallPictureBox.Location = new System.Drawing.Point(504, -1);
            this.loadingSmallPictureBox.Name = "loadingSmallPictureBox";
            this.loadingSmallPictureBox.Size = new System.Drawing.Size(19, 21);
            this.loadingSmallPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.loadingSmallPictureBox.TabIndex = 38;
            this.loadingSmallPictureBox.TabStop = false;
            this.loadingSmallPictureBox.Visible = false;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(0, 21);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(522, 16);
            this.progressBar.TabIndex = 37;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(0, 5);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(37, 13);
            this.lblStatus.TabIndex = 36;
            this.lblStatus.Text = "Status";
            // 
            // GetImageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.mainTableLayoutPanel);
            this.Name = "GetImageControl";
            this.Size = new System.Drawing.Size(529, 339);
            this.panelSearchResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.loadingPictureBox)).EndInit();
            this.mainTableLayoutPanel.ResumeLayout(false);
            this.topTableLayoutPanel.ResumeLayout(false);
            this.panelTopLeft.ResumeLayout(false);
            this.panelTopLeft.PerformLayout();
            this.panelTopRight.ResumeLayout(false);
            this.panelTopRight.PerformLayout();
            this.infoPanel.ResumeLayout(false);
            this.infoPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadingSmallPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblSearchFor;
        private System.Windows.Forms.TextBox txtSearchFor;
        private System.Windows.Forms.Panel panelSearchResults;
        private System.Windows.Forms.FlowLayoutPanel resultsFlowLayoutPanel;
        private System.Windows.Forms.PictureBox loadingPictureBox;
        private System.Windows.Forms.TableLayoutPanel mainTableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel topTableLayoutPanel;
        private System.Windows.Forms.Panel panelTopLeft;
        private System.Windows.Forms.Panel panelTopRight;
        private System.Windows.Forms.Label lblNumberOfResults;
        private System.Windows.Forms.TextBox txtMaxResults;
        private System.Windows.Forms.Button btnCancelSearch;
        private System.Windows.Forms.Button btnSearchFor;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Panel infoPanel;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.PictureBox loadingSmallPictureBox;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}
