using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LanguageLearner.UI
{

    public partial class GetStringDialog : Form
    {
        public string TextValue = string.Empty;
        
        public GetStringDialog(string text, string caption)
        {
            InitializeComponent();

            txtEdit.Text = text;
            this.Text = caption;
            
        }        

        private void btnOK_Click(object sender, EventArgs e)
        {            
            TextValue = txtEdit.Text;
            if (TextValue.Trim().Length < 1)
            {
                MessageBox.Show("Text cannot be empty");
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}