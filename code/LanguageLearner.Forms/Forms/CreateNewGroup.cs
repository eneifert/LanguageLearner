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
    public partial class CreateNewGroupDialog : Form
    {
        public string GroupName
        {
            get { return txtName.Text; }
        }
        public MyTreeNodeType GroupType
        {
            get
            {
                if (rbtnCardList.Checked)
                    return MyTreeNodeType.CardList;
                return MyTreeNodeType.Collection;
            }
        }
        public int SelectedCollectionID
        {
            get
            {
                if (cmbBoxCollection.Items.Count > 0)
                    return (int)cmbBoxCollection.SelectedValue;
                return -1;
            }
            set
            {
                rbtnCardList.Checked = true;
                cmbBoxCollection.SelectedValue = value;
            }
        }
        
        public CreateNewGroupDialog()
        {
            InitializeComponent();

            LanguageData dataLayer = new LanguageData();
            cmbBoxCollection.DataSource = dataLayer.daCollection.GetData();
            cmbBoxCollection.DisplayMember = "Name";
            cmbBoxCollection.ValueMember = "ID";            
        }

        private void rbtnCardList_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnCardList.Checked)
                panel.Enabled = true;
            else
                panel.Enabled = false;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim().Length < 1)
            {
                showMsg("*Name cannot be empty");
                return;
            }
            if (rbtnCardList.Checked && SelectedCollectionID < 0)
            {
                showMsg("*You must select a Collection");
                return;
            }

            Close();
        }

        void showMsg(string message)
        {
            errorMsg.Text = message;
            errorMsg.Visible = true;
        }       
    }
}