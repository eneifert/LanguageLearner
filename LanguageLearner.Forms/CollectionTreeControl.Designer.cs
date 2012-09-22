namespace LanguageLearner.UI
{
    partial class CollectionTreeControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CollectionTreeControl));
            this.button5 = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.collectionTreeView = new MWControls.MWTreeView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnShow = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(0, 3);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(227, 26);
            this.button5.TabIndex = 23;
            this.button5.Text = "Card Collections";
            this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImage = global::LanguageLearner.UI.Properties.Resources.no;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.Location = new System.Drawing.Point(84, 30);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(36, 30);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            this.btnEdit.BackgroundImage = global::LanguageLearner.UI.Properties.Resources.settings;
            this.btnEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEdit.Location = new System.Drawing.Point(42, 30);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(36, 30);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // collectionTreeView
            // 
            this.collectionTreeView.CheckedNodes = ((System.Collections.Hashtable)(resources.GetObject("collectionTreeView.CheckedNodes")));
            this.collectionTreeView.Location = new System.Drawing.Point(0, 66);
            this.collectionTreeView.MultiSelect = MWCommon.TreeViewMultiSelect.MultiSameBranchAndLevel;
            this.collectionTreeView.Name = "collectionTreeView";
            this.collectionTreeView.SelNodes = ((System.Collections.Hashtable)(resources.GetObject("collectionTreeView.SelNodes")));
            this.collectionTreeView.Size = new System.Drawing.Size(224, 552);
            this.collectionTreeView.TabIndex = 24;
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImage = global::LanguageLearner.UI.Properties.Resources.add;
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.Location = new System.Drawing.Point(0, 30);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(36, 30);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnShow
            // 
            this.btnShow.BackgroundImage = global::LanguageLearner.UI.Properties.Resources.redo;
            this.btnShow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnShow.Location = new System.Drawing.Point(191, 30);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(33, 30);
            this.btnShow.TabIndex = 26;
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // CollectionTreeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.collectionTreeView);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Name = "CollectionTreeControl";
            this.Size = new System.Drawing.Size(227, 630);
            this.Load += new System.EventHandler(this.CollectionTreeControl_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private MWControls.MWTreeView collectionTreeView;
        private System.Windows.Forms.Button btnShow;
    }
}
