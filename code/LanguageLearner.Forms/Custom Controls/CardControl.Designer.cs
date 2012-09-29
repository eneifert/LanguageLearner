namespace LanguageLearner.UI
{
    partial class CardControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardControl));
            this.label1 = new System.Windows.Forms.Label();
            this.panelDifficulty = new System.Windows.Forms.Panel();
            this.radioBtnHard = new CloudToolkitN6.CloudRadioButton();
            this.radioBtnEasy = new CloudToolkitN6.CloudRadioButton();
            this.radioBtnMedium = new CloudToolkitN6.CloudRadioButton();
            this.txtQuestion = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAnswer = new System.Windows.Forms.TextBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.chkBoxShowAnswer = new System.Windows.Forms.CheckBox();
            this.chkBoxSwitchQuestionAnswer = new System.Windows.Forms.CheckBox();
            this.soundImageList = new System.Windows.Forms.ImageList(this.components);
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDeleteCard = new CloudToolkitN6.CloudButton();
            this.btnSaveChanges = new CloudToolkitN6.CloudButton();
            this.btnAddNewCard = new CloudToolkitN6.CloudButton();
            this.vum = new Alvas.Audio.SoundLevelMeter();
            this.lblTime = new System.Windows.Forms.Label();
            this.btnClearImage = new System.Windows.Forms.Button();
            this.btnGetImage = new System.Windows.Forms.Button();
            this.btnSound = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.panelDifficulty.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(379, 291);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 17);
            this.label1.TabIndex = 98;
            this.label1.Text = "Difficulty";
            // 
            // panelDifficulty
            // 
            this.panelDifficulty.BackColor = System.Drawing.Color.White;
            this.panelDifficulty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelDifficulty.Controls.Add(this.radioBtnHard);
            this.panelDifficulty.Controls.Add(this.radioBtnEasy);
            this.panelDifficulty.Controls.Add(this.radioBtnMedium);
            this.panelDifficulty.Location = new System.Drawing.Point(356, 311);
            this.panelDifficulty.Name = "panelDifficulty";
            this.panelDifficulty.Size = new System.Drawing.Size(113, 75);
            this.panelDifficulty.TabIndex = 97;
            // 
            // radioBtnHard
            // 
            this.radioBtnHard.AutoSize = true;
            this.radioBtnHard.BackColor = System.Drawing.Color.Transparent;
            this.radioBtnHard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(79)))), ((int)(((byte)(118)))));
            this.radioBtnHard.Location = new System.Drawing.Point(10, 54);
            this.radioBtnHard.Name = "radioBtnHard";
            this.radioBtnHard.Size = new System.Drawing.Size(48, 17);
            this.radioBtnHard.TabIndex = 85;
            this.radioBtnHard.TabStop = true;
            this.radioBtnHard.Text = "Hard";
            this.radioBtnHard.UseVisualStyleBackColor = true;
            // 
            // radioBtnEasy
            // 
            this.radioBtnEasy.AutoSize = true;
            this.radioBtnEasy.BackColor = System.Drawing.Color.Transparent;
            this.radioBtnEasy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(79)))), ((int)(((byte)(118)))));
            this.radioBtnEasy.Location = new System.Drawing.Point(10, 8);
            this.radioBtnEasy.Name = "radioBtnEasy";
            this.radioBtnEasy.Size = new System.Drawing.Size(48, 17);
            this.radioBtnEasy.TabIndex = 83;
            this.radioBtnEasy.TabStop = true;
            this.radioBtnEasy.Text = "Easy";
            this.radioBtnEasy.UseVisualStyleBackColor = true;
            // 
            // radioBtnMedium
            // 
            this.radioBtnMedium.AutoSize = true;
            this.radioBtnMedium.BackColor = System.Drawing.Color.Transparent;
            this.radioBtnMedium.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(79)))), ((int)(((byte)(118)))));
            this.radioBtnMedium.Location = new System.Drawing.Point(10, 31);
            this.radioBtnMedium.Name = "radioBtnMedium";
            this.radioBtnMedium.Size = new System.Drawing.Size(62, 17);
            this.radioBtnMedium.TabIndex = 84;
            this.radioBtnMedium.TabStop = true;
            this.radioBtnMedium.Text = "Medium";
            this.radioBtnMedium.UseVisualStyleBackColor = true;
            // 
            // txtQuestion
            // 
            this.txtQuestion.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuestion.Location = new System.Drawing.Point(14, 21);
            this.txtQuestion.Multiline = true;
            this.txtQuestion.Name = "txtQuestion";
            this.txtQuestion.Size = new System.Drawing.Size(294, 92);
            this.txtQuestion.TabIndex = 89;
            this.txtQuestion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(10, 1);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(69, 17);
            this.label16.TabIndex = 91;
            this.label16.Text = "Question:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(10, 122);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(58, 17);
            this.label15.TabIndex = 93;
            this.label15.Text = "Answer:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 291);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 17);
            this.label2.TabIndex = 95;
            this.label2.Text = "Notes";
            // 
            // txtAnswer
            // 
            this.txtAnswer.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtAnswer.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAnswer.Location = new System.Drawing.Point(13, 142);
            this.txtAnswer.Multiline = true;
            this.txtAnswer.Name = "txtAnswer";
            this.txtAnswer.Size = new System.Drawing.Size(295, 86);
            this.txtAnswer.TabIndex = 94;
            this.txtAnswer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAnswer.Enter += new System.EventHandler(this.txtAnswer_Enter);
            // 
            // txtNotes
            // 
            this.txtNotes.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes.Location = new System.Drawing.Point(13, 311);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(337, 75);
            this.txtNotes.TabIndex = 96;
            this.txtNotes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chkBoxShowAnswer
            // 
            this.chkBoxShowAnswer.AutoSize = true;
            this.chkBoxShowAnswer.Location = new System.Drawing.Point(145, 123);
            this.chkBoxShowAnswer.Name = "chkBoxShowAnswer";
            this.chkBoxShowAnswer.Size = new System.Drawing.Size(135, 17);
            this.chkBoxShowAnswer.TabIndex = 103;
            this.chkBoxShowAnswer.Text = "Show Answer On Load";
            this.chkBoxShowAnswer.UseVisualStyleBackColor = true;
            // 
            // chkBoxSwitchQuestionAnswer
            // 
            this.chkBoxSwitchQuestionAnswer.AutoSize = true;
            this.chkBoxSwitchQuestionAnswer.Location = new System.Drawing.Point(145, 2);
            this.chkBoxSwitchQuestionAnswer.Name = "chkBoxSwitchQuestionAnswer";
            this.chkBoxSwitchQuestionAnswer.Size = new System.Drawing.Size(163, 17);
            this.chkBoxSwitchQuestionAnswer.TabIndex = 104;
            this.chkBoxSwitchQuestionAnswer.Text = "Switch Question And Answer";
            this.chkBoxSwitchQuestionAnswer.UseVisualStyleBackColor = true;
            this.chkBoxSwitchQuestionAnswer.CheckedChanged += new System.EventHandler(this.chkBoxSwitchQuestionAnswer_CheckedChanged);
            // 
            // soundImageList
            // 
            this.soundImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("soundImageList.ImageStream")));
            this.soundImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.soundImageList.Images.SetKeyName(0, "sound_off_sm.jpg");
            this.soundImageList.Images.SetKeyName(1, "sound_on_sm.jpg");
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.White;
            this.btnClear.Enabled = false;
            this.btnClear.Location = new System.Drawing.Point(422, 255);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(47, 31);
            this.btnClear.TabIndex = 107;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDeleteCard
            // 
            this.btnDeleteCard.BackColor = System.Drawing.Color.Transparent;
            this.btnDeleteCard.ButtonText = "Delete (D)";
            this.btnDeleteCard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteCard.DisabledColors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(212))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))))};
            this.btnDeleteCard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(105)))), ((int)(((byte)(152)))));
            this.btnDeleteCard.Icon = null;
            this.btnDeleteCard.IconAlign = CloudToolkitN6.CloudButton.IconBitmapAlign.Left;
            this.btnDeleteCard.IconSpacingX = 5;
            this.btnDeleteCard.IconSpacingY = 5;
            this.btnDeleteCard.IconTransparency = 0F;
            this.btnDeleteCard.Location = new System.Drawing.Point(131, 392);
            this.btnDeleteCard.MouseDown_Colors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(193)))), ((int)(((byte)(135))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(208)))), ((int)(((byte)(171))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(220)))), ((int)(((byte)(167))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(239)))), ((int)(((byte)(205))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(215)))), ((int)(((byte)(112))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(200)))), ((int)(((byte)(49))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(221)))), ((int)(((byte)(132)))))};
            this.btnDeleteCard.MouseOn_Colors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(168)))), ((int)(((byte)(113))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(185)))), ((int)(((byte)(129))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(231)))), ((int)(((byte)(182))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(217))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(226)))), ((int)(((byte)(133))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(77))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(232)))), ((int)(((byte)(151)))))};
            this.btnDeleteCard.Name = "btnDeleteCard";
            this.btnDeleteCard.Normal_Colors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(189)))), ((int)(((byte)(207))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(214)))), ((int)(((byte)(212))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(208)))), ((int)(((byte)(221))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(249)))), ((int)(((byte)(253))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(238)))), ((int)(((byte)(249))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(225)))), ((int)(((byte)(244))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))))};
            this.btnDeleteCard.Size = new System.Drawing.Size(114, 38);
            this.btnDeleteCard.TabIndex = 108;
            this.btnDeleteCard.TextImageRelation = CloudToolkitN6.CloudButton.ButtonTextImageRelation.TextAboveImage;
            this.btnDeleteCard.TextSpacingX = 5;
            this.btnDeleteCard.TextSpacingY = 5;
            this.btnDeleteCard.Click += new System.EventHandler(this.btnDeleteCard_Click);
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.BackColor = System.Drawing.Color.Transparent;
            this.btnSaveChanges.ButtonText = "Save Changes (S)";
            this.btnSaveChanges.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveChanges.DisabledColors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(212))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))))};
            this.btnSaveChanges.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(105)))), ((int)(((byte)(152)))));
            this.btnSaveChanges.Icon = null;
            this.btnSaveChanges.IconAlign = CloudToolkitN6.CloudButton.IconBitmapAlign.Left;
            this.btnSaveChanges.IconSpacingX = 5;
            this.btnSaveChanges.IconSpacingY = 5;
            this.btnSaveChanges.IconTransparency = 0F;
            this.btnSaveChanges.Location = new System.Drawing.Point(356, 392);
            this.btnSaveChanges.MouseDown_Colors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(193)))), ((int)(((byte)(135))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(208)))), ((int)(((byte)(171))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(220)))), ((int)(((byte)(167))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(239)))), ((int)(((byte)(205))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(215)))), ((int)(((byte)(112))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(200)))), ((int)(((byte)(49))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(221)))), ((int)(((byte)(132)))))};
            this.btnSaveChanges.MouseOn_Colors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(168)))), ((int)(((byte)(113))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(185)))), ((int)(((byte)(129))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(231)))), ((int)(((byte)(182))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(217))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(226)))), ((int)(((byte)(133))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(77))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(232)))), ((int)(((byte)(151)))))};
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Normal_Colors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(189)))), ((int)(((byte)(207))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(214)))), ((int)(((byte)(212))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(208)))), ((int)(((byte)(221))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(249)))), ((int)(((byte)(253))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(238)))), ((int)(((byte)(249))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(225)))), ((int)(((byte)(244))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))))};
            this.btnSaveChanges.Size = new System.Drawing.Size(113, 38);
            this.btnSaveChanges.TabIndex = 109;
            this.btnSaveChanges.TextImageRelation = CloudToolkitN6.CloudButton.ButtonTextImageRelation.TextAboveImage;
            this.btnSaveChanges.TextSpacingX = 5;
            this.btnSaveChanges.TextSpacingY = 5;
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // btnAddNewCard
            // 
            this.btnAddNewCard.BackColor = System.Drawing.Color.Transparent;
            this.btnAddNewCard.ButtonText = "Add New Card (A)";
            this.btnAddNewCard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddNewCard.DisabledColors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(212))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))))};
            this.btnAddNewCard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(105)))), ((int)(((byte)(152)))));
            this.btnAddNewCard.Icon = null;
            this.btnAddNewCard.IconAlign = CloudToolkitN6.CloudButton.IconBitmapAlign.Left;
            this.btnAddNewCard.IconSpacingX = 5;
            this.btnAddNewCard.IconSpacingY = 5;
            this.btnAddNewCard.IconTransparency = 0F;
            this.btnAddNewCard.Location = new System.Drawing.Point(14, 392);
            this.btnAddNewCard.MouseDown_Colors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(193)))), ((int)(((byte)(135))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(208)))), ((int)(((byte)(171))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(220)))), ((int)(((byte)(167))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(239)))), ((int)(((byte)(205))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(215)))), ((int)(((byte)(112))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(200)))), ((int)(((byte)(49))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(221)))), ((int)(((byte)(132)))))};
            this.btnAddNewCard.MouseOn_Colors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(168)))), ((int)(((byte)(113))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(185)))), ((int)(((byte)(129))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(231)))), ((int)(((byte)(182))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(217))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(226)))), ((int)(((byte)(133))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(77))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(232)))), ((int)(((byte)(151)))))};
            this.btnAddNewCard.Name = "btnAddNewCard";
            this.btnAddNewCard.Normal_Colors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(189)))), ((int)(((byte)(207))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(214)))), ((int)(((byte)(212))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(208)))), ((int)(((byte)(221))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(249)))), ((int)(((byte)(253))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(238)))), ((int)(((byte)(249))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(225)))), ((int)(((byte)(244))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))))};
            this.btnAddNewCard.Size = new System.Drawing.Size(111, 38);
            this.btnAddNewCard.TabIndex = 110;
            this.btnAddNewCard.TextImageRelation = CloudToolkitN6.CloudButton.ButtonTextImageRelation.TextAboveImage;
            this.btnAddNewCard.TextSpacingX = 5;
            this.btnAddNewCard.TextSpacingY = 5;
            this.btnAddNewCard.Click += new System.EventHandler(this.btnAddNewCard_Click);
            // 
            // vum
            // 
            this.vum.BackColor = System.Drawing.Color.White;
            this.vum.ForeColor = System.Drawing.Color.Black;
            this.vum.Location = new System.Drawing.Point(14, 255);
            this.vum.Name = "vum";
            this.vum.OwnerDraw = false;
            this.vum.Size = new System.Drawing.Size(294, 31);
            this.vum.TabIndex = 112;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(11, 239);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(69, 13);
            this.lblTime.TabIndex = 111;
            this.lblTime.Text = "Time in ms: 0";
            // 
            // btnClearImage
            // 
            this.btnClearImage.BackColor = System.Drawing.Color.White;
            this.btnClearImage.Enabled = false;
            this.btnClearImage.Location = new System.Drawing.Point(422, 197);
            this.btnClearImage.Name = "btnClearImage";
            this.btnClearImage.Size = new System.Drawing.Size(47, 31);
            this.btnClearImage.TabIndex = 113;
            this.btnClearImage.Text = "Clear";
            this.btnClearImage.UseVisualStyleBackColor = false;
            this.btnClearImage.Click += new System.EventHandler(this.btnClearImage_Click);
            // 
            // btnGetImage
            // 
            this.btnGetImage.BackColor = System.Drawing.Color.White;
            this.btnGetImage.Enabled = false;
            this.btnGetImage.Location = new System.Drawing.Point(319, 197);
            this.btnGetImage.Name = "btnGetImage";
            this.btnGetImage.Size = new System.Drawing.Size(92, 31);
            this.btnGetImage.TabIndex = 114;
            this.btnGetImage.Text = "Get Image";
            this.btnGetImage.UseVisualStyleBackColor = false;
            this.btnGetImage.Click += new System.EventHandler(this.btnGetImage_Click);
            // 
            // btnSound
            // 
            this.btnSound.BackColor = System.Drawing.Color.White;
            this.btnSound.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSound.Enabled = false;
            this.btnSound.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSound.ImageIndex = 0;
            this.btnSound.ImageList = this.soundImageList;
            this.btnSound.Location = new System.Drawing.Point(319, 255);
            this.btnSound.Name = "btnSound";
            this.btnSound.Size = new System.Drawing.Size(92, 31);
            this.btnSound.TabIndex = 106;
            this.btnSound.Text = "No Sound";
            this.btnSound.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSound.UseVisualStyleBackColor = false;
            this.btnSound.Click += new System.EventHandler(this.btnSound_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Location = new System.Drawing.Point(319, 21);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(150, 170);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 105;
            this.pictureBox.TabStop = false;
            // 
            // CardControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.btnGetImage);
            this.Controls.Add(this.btnClearImage);
            this.Controls.Add(this.panelDifficulty);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.txtAnswer);
            this.Controls.Add(this.txtQuestion);
            this.Controls.Add(this.btnAddNewCard);
            this.Controls.Add(this.btnDeleteCard);
            this.Controls.Add(this.btnSaveChanges);
            this.Controls.Add(this.vum);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSound);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.chkBoxSwitchQuestionAnswer);
            this.Controls.Add(this.chkBoxShowAnswer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label2);
            this.Name = "CardControl";
            this.Size = new System.Drawing.Size(480, 436);
            this.panelDifficulty.ResumeLayout(false);
            this.panelDifficulty.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelDifficulty;
        private CloudToolkitN6.CloudRadioButton radioBtnHard;
        private CloudToolkitN6.CloudRadioButton radioBtnMedium;
        private CloudToolkitN6.CloudRadioButton radioBtnEasy;
        private System.Windows.Forms.TextBox txtQuestion;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAnswer;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.CheckBox chkBoxShowAnswer;
        private System.Windows.Forms.CheckBox chkBoxSwitchQuestionAnswer;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button btnSound;
        private System.Windows.Forms.ImageList soundImageList;
        private System.Windows.Forms.Button btnClear;
        private CloudToolkitN6.CloudButton btnDeleteCard;
        private CloudToolkitN6.CloudButton btnSaveChanges;
        private CloudToolkitN6.CloudButton btnAddNewCard;
        private Alvas.Audio.SoundLevelMeter vum;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Button btnClearImage;
        private System.Windows.Forms.Button btnGetImage;
    }
}
