namespace LanguageLearner.UI
{
    partial class CreateNewGroupDialog
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
            this.btnOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.rbtnCardList = new System.Windows.Forms.RadioButton();
            this.rbtnCollection = new System.Windows.Forms.RadioButton();
            this.cmbBoxCollection = new System.Windows.Forms.ComboBox();
            this.panel = new System.Windows.Forms.Panel();
            this.errorMsg = new System.Windows.Forms.Label();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(153, 187);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Add To Collection:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(15, 24);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(206, 20);
            this.txtName.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(61, 187);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Name:";
            // 
            // rbtnCardList
            // 
            this.rbtnCardList.AutoSize = true;
            this.rbtnCardList.Location = new System.Drawing.Point(103, 60);
            this.rbtnCardList.Name = "rbtnCardList";
            this.rbtnCardList.Size = new System.Drawing.Size(63, 17);
            this.rbtnCardList.TabIndex = 5;
            this.rbtnCardList.Text = "CardList";
            this.rbtnCardList.UseVisualStyleBackColor = true;
            this.rbtnCardList.CheckedChanged += new System.EventHandler(this.rbtnCardList_CheckedChanged);
            // 
            // rbtnCollection
            // 
            this.rbtnCollection.AutoSize = true;
            this.rbtnCollection.Checked = true;
            this.rbtnCollection.Location = new System.Drawing.Point(15, 60);
            this.rbtnCollection.Name = "rbtnCollection";
            this.rbtnCollection.Size = new System.Drawing.Size(71, 17);
            this.rbtnCollection.TabIndex = 6;
            this.rbtnCollection.TabStop = true;
            this.rbtnCollection.Text = "Collection";
            this.rbtnCollection.UseVisualStyleBackColor = true;
            // 
            // cmbBoxCollection
            // 
            this.cmbBoxCollection.FormattingEnabled = true;
            this.cmbBoxCollection.Location = new System.Drawing.Point(6, 24);
            this.cmbBoxCollection.Name = "cmbBoxCollection";
            this.cmbBoxCollection.Size = new System.Drawing.Size(199, 21);
            this.cmbBoxCollection.TabIndex = 7;
            // 
            // panel
            // 
            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel.Controls.Add(this.cmbBoxCollection);
            this.panel.Controls.Add(this.label1);
            this.panel.Enabled = false;
            this.panel.Location = new System.Drawing.Point(15, 83);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(213, 61);
            this.panel.TabIndex = 8;
            // 
            // errorMsg
            // 
            this.errorMsg.AutoSize = true;
            this.errorMsg.ForeColor = System.Drawing.Color.Red;
            this.errorMsg.Location = new System.Drawing.Point(12, 161);
            this.errorMsg.Name = "errorMsg";
            this.errorMsg.Size = new System.Drawing.Size(124, 13);
            this.errorMsg.TabIndex = 9;
            this.errorMsg.Text = "* Name cannot be empty";
            this.errorMsg.Visible = false;
            // 
            // CreateNewGroupDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(243, 241);
            this.Controls.Add(this.errorMsg);
            this.Controls.Add(this.rbtnCollection);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.rbtnCardList);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnOk);
            this.Text = "Create New Group";
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbtnCardList;
        private System.Windows.Forms.RadioButton rbtnCollection;
        private System.Windows.Forms.ComboBox cmbBoxCollection;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Label errorMsg;
    }
}