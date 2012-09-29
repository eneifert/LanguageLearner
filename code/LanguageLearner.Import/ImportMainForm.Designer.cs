namespace Import
{
    partial class ImportMainForm
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
            this.btnOldDB = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.txtOldDB = new System.Windows.Forms.TextBox();
            this.txtNewDB = new System.Windows.Forms.TextBox();
            this.btnNewDB = new System.Windows.Forms.Button();
            this.btnGo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.chkBoxImportMarks = new System.Windows.Forms.CheckBox();
            this.lvInfo = new System.Windows.Forms.ListView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOldDB
            // 
            this.btnOldDB.Location = new System.Drawing.Point(210, 18);
            this.btnOldDB.Name = "btnOldDB";
            this.btnOldDB.Size = new System.Drawing.Size(146, 23);
            this.btnOldDB.TabIndex = 0;
            this.btnOldDB.Text = "Select Old Database";
            this.btnOldDB.UseVisualStyleBackColor = true;
            this.btnOldDB.Click += new System.EventHandler(this.btnOldDB_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Old Lang DBs|*.sqlite";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            this.openFileDialog2.Filter = "New Lang DB| *.s3db";
            // 
            // txtOldDB
            // 
            this.txtOldDB.Location = new System.Drawing.Point(12, 21);
            this.txtOldDB.Name = "txtOldDB";
            this.txtOldDB.Size = new System.Drawing.Size(177, 20);
            this.txtOldDB.TabIndex = 1;
            // 
            // txtNewDB
            // 
            this.txtNewDB.Location = new System.Drawing.Point(12, 70);
            this.txtNewDB.Name = "txtNewDB";
            this.txtNewDB.Size = new System.Drawing.Size(177, 20);
            this.txtNewDB.TabIndex = 3;
            // 
            // btnNewDB
            // 
            this.btnNewDB.Location = new System.Drawing.Point(210, 67);
            this.btnNewDB.Name = "btnNewDB";
            this.btnNewDB.Size = new System.Drawing.Size(146, 23);
            this.btnNewDB.TabIndex = 2;
            this.btnNewDB.Text = "Select New Database";
            this.btnNewDB.UseVisualStyleBackColor = true;
            this.btnNewDB.Click += new System.EventHandler(this.btnNewDB_Click);
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(485, 18);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(76, 33);
            this.btnGo.TabIndex = 4;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "*Leave blank to create new database";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 287);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(553, 23);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 7;
            // 
            // chkBoxImportMarks
            // 
            this.chkBoxImportMarks.AutoSize = true;
            this.chkBoxImportMarks.Checked = true;
            this.chkBoxImportMarks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBoxImportMarks.Location = new System.Drawing.Point(485, 98);
            this.chkBoxImportMarks.Name = "chkBoxImportMarks";
            this.chkBoxImportMarks.Size = new System.Drawing.Size(87, 17);
            this.chkBoxImportMarks.TabIndex = 8;
            this.chkBoxImportMarks.Text = "Import Marks";
            this.chkBoxImportMarks.UseVisualStyleBackColor = true;
            // 
            // lvInfo
            // 
            this.lvInfo.Location = new System.Drawing.Point(12, 121);
            this.lvInfo.Name = "lvInfo";
            this.lvInfo.Size = new System.Drawing.Size(553, 160);
            this.lvInfo.TabIndex = 9;
            this.lvInfo.UseCompatibleStateImageBehavior = false;
            this.lvInfo.View = System.Windows.Forms.View.List;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(485, 57);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(76, 33);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ImportMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 322);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lvInfo);
            this.Controls.Add(this.chkBoxImportMarks);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.txtNewDB);
            this.Controls.Add(this.btnNewDB);
            this.Controls.Add(this.txtOldDB);
            this.Controls.Add(this.btnOldDB);
            this.Name = "ImportMainForm";
            this.Text = "DBImportTool";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOldDB;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.TextBox txtOldDB;
        private System.Windows.Forms.TextBox txtNewDB;
        private System.Windows.Forms.Button btnNewDB;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.CheckBox chkBoxImportMarks;
        private System.Windows.Forms.ListView lvInfo;
        private System.Windows.Forms.Button btnCancel;
    }
}