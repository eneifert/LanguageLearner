namespace LanguageLearner.UI
{
    partial class CardListPlayList
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
            this.button6 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.cloudCheckBox3 = new CloudToolkitN6.CloudCheckBox();
            this.cloudCheckBox2 = new CloudToolkitN6.CloudCheckBox();
            this.listView = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Location = new System.Drawing.Point(0, 3);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(232, 26);
            this.button6.TabIndex = 81;
            this.button6.Text = "Playlist";
            this.button6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Show All",
            "Easy",
            "Medium",
            "Hard"});
            this.comboBox1.Location = new System.Drawing.Point(0, 35);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(87, 21);
            this.comboBox1.TabIndex = 80;
            this.comboBox1.Text = "Show All";
            // 
            // cloudCheckBox3
            // 
            this.cloudCheckBox3.Checked = false;
            this.cloudCheckBox3.Location = new System.Drawing.Point(93, 35);
            this.cloudCheckBox3.Name = "cloudCheckBox3";
            this.cloudCheckBox3.Size = new System.Drawing.Size(69, 20);
            this.cloudCheckBox3.TabIndex = 79;
            this.cloudCheckBox3.TextFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.cloudCheckBox3.Value = "Play All";
            // 
            // cloudCheckBox2
            // 
            this.cloudCheckBox2.Checked = false;
            this.cloudCheckBox2.Location = new System.Drawing.Point(164, 35);
            this.cloudCheckBox2.Name = "cloudCheckBox2";
            this.cloudCheckBox2.Size = new System.Drawing.Size(68, 20);
            this.cloudCheckBox2.TabIndex = 78;
            this.cloudCheckBox2.TextFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.cloudCheckBox2.Value = "Repeat";
            // 
            // listView
            // 
            this.listView.GridLines = true;
            this.listView.Location = new System.Drawing.Point(0, 61);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(230, 503);
            this.listView.TabIndex = 23;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.List;
            this.listView.DoubleClick += new System.EventHandler(this.listView_DoubleClick);
            this.listView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listView_KeyPress);
            // 
            // CardListPlayList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.cloudCheckBox3);
            this.Controls.Add(this.cloudCheckBox2);
            this.Name = "CardListPlayList";
            this.Size = new System.Drawing.Size(232, 565);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.ComboBox comboBox1;
        private CloudToolkitN6.CloudCheckBox cloudCheckBox3;
        private CloudToolkitN6.CloudCheckBox cloudCheckBox2;
        private System.Windows.Forms.ListView listView;
    }
}
