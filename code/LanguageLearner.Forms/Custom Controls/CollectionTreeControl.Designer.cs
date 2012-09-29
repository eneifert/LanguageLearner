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
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cardListsTreeView = new MWControls.MWTreeView();
            this.cardsTabControl = new System.Windows.Forms.TabControl();
            this.tpDictionary = new System.Windows.Forms.TabPage();
            this.dictionaryTreeView = new MWControls.MWTreeView();
            this.tpCardLists = new System.Windows.Forms.TabPage();
            this.btnSendSelectCards = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btnPanel = new System.Windows.Forms.Panel();
            this.btnSendAllCards = new System.Windows.Forms.Button();
            this.cardsTabControl.SuspendLayout();
            this.tpDictionary.SuspendLayout();
            this.tpCardLists.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.btnPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(3, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(224, 26);
            this.btnRefresh.TabIndex = 23;
            this.btnRefresh.Text = "Card Collections";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // cardListsTreeView
            // 
            this.cardListsTreeView.CheckedNodes = ((System.Collections.Hashtable)(resources.GetObject("cardListsTreeView.CheckedNodes")));
            this.cardListsTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardListsTreeView.FullRowSelect = true;
            this.cardListsTreeView.ItemHeight = 14;
            this.cardListsTreeView.Location = new System.Drawing.Point(3, 3);
            this.cardListsTreeView.MultiSelect = MWCommon.TreeViewMultiSelect.MultiSameBranchAndLevel;
            this.cardListsTreeView.Name = "cardListsTreeView";
            this.cardListsTreeView.SelNodes = ((System.Collections.Hashtable)(resources.GetObject("cardListsTreeView.SelNodes")));
            this.cardListsTreeView.Size = new System.Drawing.Size(210, 357);
            this.cardListsTreeView.TabIndex = 24;
            this.cardListsTreeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.cardListsTreeView_ItemDrag);
            // 
            // cardsTabControl
            // 
            this.cardsTabControl.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.cardsTabControl.Controls.Add(this.tpDictionary);
            this.cardsTabControl.Controls.Add(this.tpCardLists);
            this.cardsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardsTabControl.ItemSize = new System.Drawing.Size(108, 30);
            this.cardsTabControl.Location = new System.Drawing.Point(3, 76);
            this.cardsTabControl.Name = "cardsTabControl";
            this.cardsTabControl.SelectedIndex = 0;
            this.cardsTabControl.Size = new System.Drawing.Size(224, 401);
            this.cardsTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.cardsTabControl.TabIndex = 27;
            // 
            // tpDictionary
            // 
            this.tpDictionary.Controls.Add(this.dictionaryTreeView);
            this.tpDictionary.Location = new System.Drawing.Point(4, 34);
            this.tpDictionary.Name = "tpDictionary";
            this.tpDictionary.Padding = new System.Windows.Forms.Padding(3);
            this.tpDictionary.Size = new System.Drawing.Size(216, 363);
            this.tpDictionary.TabIndex = 0;
            this.tpDictionary.Text = "Dictionary";
            this.tpDictionary.UseVisualStyleBackColor = true;
            // 
            // dictionaryTreeView
            // 
            this.dictionaryTreeView.CheckedNodes = ((System.Collections.Hashtable)(resources.GetObject("dictionaryTreeView.CheckedNodes")));
            this.dictionaryTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dictionaryTreeView.FullRowSelect = true;
            this.dictionaryTreeView.ItemHeight = 14;
            this.dictionaryTreeView.Location = new System.Drawing.Point(3, 3);
            this.dictionaryTreeView.MultiSelect = MWCommon.TreeViewMultiSelect.MultiSameBranchAndLevel;
            this.dictionaryTreeView.Name = "dictionaryTreeView";
            this.dictionaryTreeView.SelNodes = ((System.Collections.Hashtable)(resources.GetObject("dictionaryTreeView.SelNodes")));
            this.dictionaryTreeView.Size = new System.Drawing.Size(210, 357);
            this.dictionaryTreeView.TabIndex = 25;
            this.dictionaryTreeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.dictionaryTreeView_ItemDrag);
            // 
            // tpCardLists
            // 
            this.tpCardLists.Controls.Add(this.cardListsTreeView);
            this.tpCardLists.Location = new System.Drawing.Point(4, 34);
            this.tpCardLists.Name = "tpCardLists";
            this.tpCardLists.Padding = new System.Windows.Forms.Padding(3);
            this.tpCardLists.Size = new System.Drawing.Size(216, 363);
            this.tpCardLists.TabIndex = 1;
            this.tpCardLists.Text = "Card Lists";
            this.tpCardLists.UseVisualStyleBackColor = true;
            // 
            // btnSendSelectCards
            // 
            this.btnSendSelectCards.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendSelectCards.BackgroundImage = global::LanguageLearner.UI.Properties.Resources.redo;
            this.btnSendSelectCards.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSendSelectCards.Location = new System.Drawing.Point(184, 2);
            this.btnSendSelectCards.Name = "btnSendSelectCards";
            this.btnSendSelectCards.Size = new System.Drawing.Size(36, 30);
            this.btnSendSelectCards.TabIndex = 26;
            this.btnSendSelectCards.UseVisualStyleBackColor = true;
            this.btnSendSelectCards.Click += new System.EventHandler(this.btnSendSelectedCards_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImage = global::LanguageLearner.UI.Properties.Resources.no;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.Location = new System.Drawing.Point(84, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(36, 30);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackgroundImage = global::LanguageLearner.UI.Properties.Resources.settings;
            this.btnEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEdit.Location = new System.Drawing.Point(42, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(36, 30);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImage = global::LanguageLearner.UI.Properties.Resources.add;
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.Location = new System.Drawing.Point(0, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(36, 30);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.AllowDrop = true;
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.btnRefresh, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.cardsTabControl, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.btnPanel, 0, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(230, 480);
            this.tableLayoutPanel.TabIndex = 28;
            // 
            // btnPanel
            // 
            this.btnPanel.Controls.Add(this.btnSendAllCards);
            this.btnPanel.Controls.Add(this.btnDelete);
            this.btnPanel.Controls.Add(this.btnEdit);
            this.btnPanel.Controls.Add(this.btnSendSelectCards);
            this.btnPanel.Controls.Add(this.btnAdd);
            this.btnPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPanel.Location = new System.Drawing.Point(3, 35);
            this.btnPanel.Name = "btnPanel";
            this.btnPanel.Size = new System.Drawing.Size(224, 35);
            this.btnPanel.TabIndex = 24;
            // 
            // btnSendAllCards
            // 
            this.btnSendAllCards.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendAllCards.BackgroundImage = global::LanguageLearner.UI.Properties.Resources.select_all;
            this.btnSendAllCards.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSendAllCards.Location = new System.Drawing.Point(142, 2);
            this.btnSendAllCards.Name = "btnSendAllCards";
            this.btnSendAllCards.Size = new System.Drawing.Size(36, 30);
            this.btnSendAllCards.TabIndex = 27;
            this.btnSendAllCards.UseVisualStyleBackColor = true;
            this.btnSendAllCards.Click += new System.EventHandler(this.btnSendAllCards_Click);
            // 
            // CollectionTreeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "CollectionTreeControl";
            this.Size = new System.Drawing.Size(230, 480);
            this.cardsTabControl.ResumeLayout(false);
            this.tpDictionary.ResumeLayout(false);
            this.tpCardLists.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            this.btnPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private MWControls.MWTreeView cardListsTreeView;
        private System.Windows.Forms.Button btnSendSelectCards;
        private System.Windows.Forms.TabControl cardsTabControl;
        private System.Windows.Forms.TabPage tpDictionary;
        private System.Windows.Forms.TabPage tpCardLists;
        private MWControls.MWTreeView dictionaryTreeView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Panel btnPanel;
        private System.Windows.Forms.Button btnSendAllCards;
    }
}
