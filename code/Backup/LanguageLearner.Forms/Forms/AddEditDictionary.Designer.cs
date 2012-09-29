namespace LanguageLearner.UI
{
    partial class AddEditDictionary
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnDone = new System.Windows.Forms.Button();
            this.dictionaryDataGrid = new System.Windows.Forms.DataGridView();
            this.dsDictionary = new LanguageLearner.UI.dsDictionary();
            this.dictionaryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dictionaryTableAdapter = new LanguageLearner.UI.dsDictionaryTableAdapters.DictionaryTableAdapter();
            this.label1 = new System.Windows.Forms.Label();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alphabetDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dictionaryDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDictionary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dictionaryBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDone
            // 
            this.btnDone.Location = new System.Drawing.Point(489, 244);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(75, 23);
            this.btnDone.TabIndex = 2;
            this.btnDone.Text = "Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // dictionaryDataGrid
            // 
            this.dictionaryDataGrid.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dictionaryDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dictionaryDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dictionaryDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.alphabetDataGridViewTextBoxColumn,
            this.columnDataGridViewTextBoxColumn});
            this.dictionaryDataGrid.DataSource = this.dictionaryBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dictionaryDataGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.dictionaryDataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dictionaryDataGrid.Location = new System.Drawing.Point(12, 36);
            this.dictionaryDataGrid.Name = "dictionaryDataGrid";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dictionaryDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dictionaryDataGrid.Size = new System.Drawing.Size(552, 150);
            this.dictionaryDataGrid.TabIndex = 3;
            this.dictionaryDataGrid.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dictionaryDataGrid_UserDeletingRow);
            this.dictionaryDataGrid.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dictionaryDataGrid_RowValidating);
            // 
            // dsDictionary
            // 
            this.dsDictionary.DataSetName = "dsDictionary";
            this.dsDictionary.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dictionaryBindingSource
            // 
            this.dictionaryBindingSource.DataMember = "Dictionary";
            this.dictionaryBindingSource.DataSource = this.dsDictionary;
            // 
            // dictionaryTableAdapter
            // 
            this.dictionaryTableAdapter.ClearBeforeFill = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(538, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Change how the cards show up in the dictionary (ex: 1, English -> Foriegn, abcdef" +
                "ghijklmnopqrstuvwxyz, Answer)";
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.iDDataGridViewTextBoxColumn.Width = 25;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.Width = 150;
            // 
            // alphabetDataGridViewTextBoxColumn
            // 
            this.alphabetDataGridViewTextBoxColumn.DataPropertyName = "Alphabet";
            this.alphabetDataGridViewTextBoxColumn.HeaderText = "Alphabet";
            this.alphabetDataGridViewTextBoxColumn.Name = "alphabetDataGridViewTextBoxColumn";
            this.alphabetDataGridViewTextBoxColumn.Width = 150;
            // 
            // columnDataGridViewTextBoxColumn
            // 
            this.columnDataGridViewTextBoxColumn.DataPropertyName = "Column";
            this.columnDataGridViewTextBoxColumn.HeaderText = "Sort By";
            this.columnDataGridViewTextBoxColumn.Items.AddRange(new object[] {
            "Answer",
            "Question"});
            this.columnDataGridViewTextBoxColumn.Name = "columnDataGridViewTextBoxColumn";
            this.columnDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.columnDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.columnDataGridViewTextBoxColumn.Width = 150;
            // 
            // AddEditDictionary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(576, 279);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dictionaryDataGrid);
            this.Controls.Add(this.btnDone);
            this.Name = "AddEditDictionary";
            this.Text = "Add Or Edit The Dictionary";
            this.Load += new System.EventHandler(this.AddEditDictionary_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dictionaryDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDictionary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dictionaryBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.DataGridView dictionaryDataGrid;
        private dsDictionary dsDictionary;
        private System.Windows.Forms.BindingSource dictionaryBindingSource;
        private LanguageLearner.UI.dsDictionaryTableAdapters.DictionaryTableAdapter dictionaryTableAdapter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn alphabetDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn columnDataGridViewTextBoxColumn;
    }
}