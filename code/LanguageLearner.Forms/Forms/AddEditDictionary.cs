using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LanguageLearner.Data;

namespace LanguageLearner.UI
{
    public partial class AddEditDictionary : Form
    {
        public AddEditDictionary()
        {
            InitializeComponent();

            dsDictionary.Dictionary.RowChanged += new DataRowChangeEventHandler(Dictionary_RowChanged);            
        }

        public event EventHandler DataChanged;

        private void AddEditDictionary_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dsDictionary.Dictionary' table. You can move, or remove it, as needed.
            this.dictionaryTableAdapter.Fill(this.dsDictionary.Dictionary);
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region Add, Edit, Delete Rows
        void Dictionary_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            switch (e.Action)
            {
                case DataRowAction.Add:
                case DataRowAction.Change:
                    LanguageData dataLayer = new LanguageData();
                    dsLanguageData.DictionaryRow dict = toDictionaryRow((dsDictionary.DictionaryRow) e.Row);

                    if (isValidRow(dict))
                    {
                        int i = dataLayer.InsertOrUpdateDictionary(dict);
                        if (i > 0)
                        {
                            if (DataChanged != null)
                                DataChanged(this, new EventArgs());
                        }
                        dsDictionary.AcceptChanges();
                    }

                    break;            
            }
        }

        dsLanguageData.DictionaryRow toDictionaryRow(dsDictionary.DictionaryRow row)
        {
            dsLanguageData.DictionaryRow dict = new dsLanguageData.DictionaryDataTable().NewDictionaryRow();
            dict.ID = row.ID;
            dict.Name = row.Name;
            dict.Alphabet = row.Alphabet;
            dict.Column = row.Column;

            return dict;
        }

        bool isValidRow(dsLanguageData.DictionaryRow row)
        {
            bool isValid = true;

            if (row.ID < 0)
                isValid = false;
            if (row.Name == null || row.Name.Trim().Length < 1)
                isValid = false;
            if (row.Alphabet == null || row.Alphabet.Trim().Length < 1)
                isValid = false;
            if (row.Column == null || row.Column.Trim().Length < 1)
                isValid = false;

            return isValid;
        }
            
        private void dictionaryDataGrid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete to selected row?", "Delete Row?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //Todo delete the row and update the DataSet + DataGrid
                LanguageData dataLayer = new LanguageData();
                int i = dataLayer.daDictionary.DeleteByID((int)e.Row.Cells[0].Value);

                if (i > 0)
                {
                    if (DataChanged != null)
                        DataChanged(this, new EventArgs());
                }
            }
            else
            {
                e.Cancel = true;
            }
        }        

        private void dictionaryDataGrid_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridViewRow row = dictionaryDataGrid.Rows[e.RowIndex];

            if (e.RowIndex < dsDictionary.Dictionary.Rows.Count)
            {
                if (row.Cells[e.ColumnIndex].Value == null || row.Cells[e.ColumnIndex].Value.ToString().Length < 1)
                {
                    MessageBox.Show("Sorry you left some items in the row blank", "Error", MessageBoxButtons.OK);
                    dsDictionary.Dictionary[e.RowIndex].RejectChanges();
                    e.Cancel = true;
                }
            }
        }
        #endregion

    }
}