namespace ForumBrowser
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            btnSetAsDefault = new Button();
            btnDeregister = new Button();
            label1 = new Label();
            chkAllowSSO = new CheckBox();
            chkAllowCORS = new CheckBox();
            chkUnlimitedStorage = new CheckBox();
            sep1 = new Label();
            label3 = new Label();
            linkLabel1 = new LinkLabel();
            SuspendLayout();
            // 
            // btnSetAsDefault
            // 
            btnSetAsDefault.Location = new Point(12, 13);
            btnSetAsDefault.Name = "btnSetAsDefault";
            btnSetAsDefault.Size = new Size(199, 23);
            btnSetAsDefault.TabIndex = 0;
            btnSetAsDefault.Text = "Register ProgID and set Default";
            btnSetAsDefault.UseVisualStyleBackColor = true;
            btnSetAsDefault.Click += btnSetAsDefault_Click;
            // 
            // btnDeregister
            // 
            btnDeregister.Location = new Point(12, 42);
            btnDeregister.Name = "btnDeregister";
            btnDeregister.Size = new Size(199, 23);
            btnDeregister.TabIndex = 1;
            btnDeregister.Text = "Deregister ProgID";
            btnDeregister.UseVisualStyleBackColor = true;
            btnDeregister.Click += btnDeregister_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(319, 30);
            label1.Name = "label1";
            label1.Size = new Size(166, 135);
            label1.TabIndex = 3;
            label1.Text = resources.GetString("label1.Text");
            // 
            // chkAllowSSO
            // 
            chkAllowSSO.AutoSize = true;
            chkAllowSSO.Location = new Point(12, 106);
            chkAllowSSO.Name = "chkAllowSSO";
            chkAllowSSO.Size = new Size(278, 19);
            chkAllowSSO.TabIndex = 4;
            chkAllowSSO.Text = "Allow Single Sign On using OS Primary Account";
            chkAllowSSO.UseVisualStyleBackColor = true;
            chkAllowSSO.CheckedChanged += chkAllowSSO_CheckedChanged;
            // 
            // chkAllowCORS
            // 
            chkAllowCORS.AutoSize = true;
            chkAllowCORS.Location = new Point(12, 131);
            chkAllowCORS.Name = "chkAllowCORS";
            chkAllowCORS.Size = new Size(226, 19);
            chkAllowCORS.TabIndex = 5;
            chkAllowCORS.Text = "Allow CORS (disable browser security)";
            chkAllowCORS.UseVisualStyleBackColor = true;
            chkAllowCORS.CheckedChanged += chkAllowCORS_CheckedChanged;
            // 
            // chkUnlimitedStorage
            // 
            chkUnlimitedStorage.AutoSize = true;
            chkUnlimitedStorage.Location = new Point(12, 156);
            chkUnlimitedStorage.Name = "chkUnlimitedStorage";
            chkUnlimitedStorage.Size = new Size(184, 19);
            chkUnlimitedStorage.TabIndex = 6;
            chkUnlimitedStorage.Text = "Allow unlimited backing store";
            chkUnlimitedStorage.UseVisualStyleBackColor = true;
            chkUnlimitedStorage.CheckedChanged += chkUnlimitedStorage_CheckedChanged;
            // 
            // sep1
            // 
            sep1.BorderStyle = BorderStyle.Fixed3D;
            sep1.Location = new Point(9, 231);
            sep1.Name = "sep1";
            sep1.Size = new Size(504, 2);
            sep1.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 254);
            label3.Name = "label3";
            label3.Size = new Size(108, 15);
            label3.TabIndex = 8;
            label3.Text = "ForumBrowser 1.0c";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(140, 254);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(45, 15);
            linkLabel1.TabIndex = 9;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "GitHub";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(525, 289);
            Controls.Add(linkLabel1);
            Controls.Add(label3);
            Controls.Add(sep1);
            Controls.Add(chkUnlimitedStorage);
            Controls.Add(chkAllowCORS);
            Controls.Add(chkAllowSSO);
            Controls.Add(label1);
            Controls.Add(btnDeregister);
            Controls.Add(btnSetAsDefault);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Settings";
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Settings";
            TopMost = true;
            Load += Settings_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSetAsDefault;
        private Button btnDeregister;
        private Label label1;
        private CheckBox chkAllowSSO;
        private CheckBox chkAllowCORS;
        private CheckBox chkUnlimitedStorage;
        private Label sep1;
        private Label label3;
        private LinkLabel linkLabel1;
    }
}