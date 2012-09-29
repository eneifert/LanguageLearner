using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using LanguageLearner.Data;

namespace Import
{
    /// <summary>
    /// Handles importing data from the old LangLearner program.
    /// </summary>
    public partial class ImportMainForm : Form
    {
        Thread importThread;
        LanguageData ldata = new LanguageData();         

        public ImportMainForm()
        {
            InitializeComponent();
        
            ldata.DataImported += new LanguageData.DataImportedEventHandler(ldata_DataImported);        

            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog2.InitialDirectory = Application.StartupPath;
            
        }

        private void btnOldDB_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            txtOldDB.Text = openFileDialog1.FileName;
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (importThread != null && importThread.ThreadState == ThreadState.Running)
                return;

            progressBar1.Value = 0;
            
            importThread = new Thread(new ThreadStart(runImportThread));
            importThread.Start();            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (importThread != null)
                importThread.Abort();
        }

        void runImportThread()
        {
            //TODO what if db is empty?
            //ldata.ImportFromOldDb(txtOldDB.Text, txtNewDB.Text, chkBoxImportMarks.Checked);
        }

        void ldata_DataImported(LanguageData.DataImportedEventArgs e)
        {
            if (this.lvInfo.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                LanguageData.DataImportedEventHandler d = new LanguageData.DataImportedEventHandler(updateInfo);                
                this.Invoke(d, new object[] { e });
            }
            else
            {
                updateInfo(e);
            }
        }

        void updateInfo(LanguageData.DataImportedEventArgs e)
        {
            ListViewItem item = new ListViewItem(e.Message);
            if (!e.Success)
            {
                item.ForeColor = Color.Red;
            }

            lvInfo.Items.Insert(0, item);

            progressBar1.Maximum = e.Total;
            progressBar1.Value = e.Index;

            if (e.Total == e.Index)
            {
                progressBar1.Value = 0;
                MessageBox.Show("Finished Importing Data", "All Done");
            }

            this.Refresh();
        }

        private void btnNewDB_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
            txtNewDB.Text = openFileDialog2.FileName;
        }

        
    }
}