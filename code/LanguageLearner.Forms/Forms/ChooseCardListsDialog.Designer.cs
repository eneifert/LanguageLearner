namespace LanguageLearner.UI
{
    partial class ChooseCardListsDialog
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
            this.listViewCardLists = new System.Windows.Forms.ListView();
            this.listViewSelectedCardLists = new System.Windows.Forms.ListView();
            this.btnAddAll = new System.Windows.Forms.Button();
            this.btnAddSelected = new System.Windows.Forms.Button();
            this.btnRemoveAll = new System.Windows.Forms.Button();
            this.btnRemoveSelected = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkBoxRemember = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // listViewCardLists
            // 
            this.listViewCardLists.FullRowSelect = true;
            this.listViewCardLists.Location = new System.Drawing.Point(12, 29);
            this.listViewCardLists.Name = "listViewCardLists";
            this.listViewCardLists.Size = new System.Drawing.Size(167, 141);
            this.listViewCardLists.TabIndex = 0;
            this.listViewCardLists.UseCompatibleStateImageBehavior = false;
            this.listViewCardLists.View = System.Windows.Forms.View.List;
            // 
            // listViewSelectedCardLists
            // 
            this.listViewSelectedCardLists.FullRowSelect = true;
            this.listViewSelectedCardLists.Location = new System.Drawing.Point(245, 29);
            this.listViewSelectedCardLists.Name = "listViewSelectedCardLists";
            this.listViewSelectedCardLists.Size = new System.Drawing.Size(167, 141);
            this.listViewSelectedCardLists.TabIndex = 1;
            this.listViewSelectedCardLists.UseCompatibleStateImageBehavior = false;
            this.listViewSelectedCardLists.View = System.Windows.Forms.View.List;
            // 
            // btnAddAll
            // 
            this.btnAddAll.Location = new System.Drawing.Point(190, 41);
            this.btnAddAll.Name = "btnAddAll";
            this.btnAddAll.Size = new System.Drawing.Size(41, 25);
            this.btnAddAll.TabIndex = 2;
            this.btnAddAll.Text = ">>";
            this.btnAddAll.UseVisualStyleBackColor = true;
            this.btnAddAll.Click += new System.EventHandler(this.btnAddAll_Click);
            // 
            // btnAddSelected
            // 
            this.btnAddSelected.Location = new System.Drawing.Point(190, 72);
            this.btnAddSelected.Name = "btnAddSelected";
            this.btnAddSelected.Size = new System.Drawing.Size(41, 25);
            this.btnAddSelected.TabIndex = 3;
            this.btnAddSelected.Text = "->";
            this.btnAddSelected.UseVisualStyleBackColor = true;
            this.btnAddSelected.Click += new System.EventHandler(this.btnAddSelected_Click);
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.Location = new System.Drawing.Point(190, 134);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new System.Drawing.Size(41, 25);
            this.btnRemoveAll.TabIndex = 5;
            this.btnRemoveAll.Text = "<<";
            this.btnRemoveAll.UseVisualStyleBackColor = true;
            this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
            // 
            // btnRemoveSelected
            // 
            this.btnRemoveSelected.Location = new System.Drawing.Point(190, 103);
            this.btnRemoveSelected.Name = "btnRemoveSelected";
            this.btnRemoveSelected.Size = new System.Drawing.Size(41, 25);
            this.btnRemoveSelected.TabIndex = 4;
            this.btnRemoveSelected.Text = "<-";
            this.btnRemoveSelected.UseVisualStyleBackColor = true;
            this.btnRemoveSelected.Click += new System.EventHandler(this.btnRemoveSelected_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Card Lists:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(242, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Add Card To:";
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(337, 185);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 29);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(245, 185);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 29);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkBoxRemember
            // 
            this.chkBoxRemember.AutoSize = true;
            this.chkBoxRemember.Checked = true;
            this.chkBoxRemember.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBoxRemember.Location = new System.Drawing.Point(12, 192);
            this.chkBoxRemember.Name = "chkBoxRemember";
            this.chkBoxRemember.Size = new System.Drawing.Size(138, 17);
            this.chkBoxRemember.TabIndex = 10;
            this.chkBoxRemember.Text = "Remember my selection";
            this.chkBoxRemember.UseVisualStyleBackColor = true;
            // 
            // ChooseCardListsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(424, 224);
            this.Controls.Add(this.chkBoxRemember);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRemoveAll);
            this.Controls.Add(this.btnRemoveSelected);
            this.Controls.Add(this.btnAddSelected);
            this.Controls.Add(this.btnAddAll);
            this.Controls.Add(this.listViewSelectedCardLists);
            this.Controls.Add(this.listViewCardLists);
            this.Name = "ChooseCardListsDialog";
            this.Text = "Add Card To Which Card Lists";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewCardLists;
        private System.Windows.Forms.ListView listViewSelectedCardLists;
        private System.Windows.Forms.Button btnAddAll;
        private System.Windows.Forms.Button btnAddSelected;
        private System.Windows.Forms.Button btnRemoveAll;
        private System.Windows.Forms.Button btnRemoveSelected;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkBoxRemember;
    }
}