namespace ForumBrowser
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            statusStrip1 = new StatusStrip();
            toolStripProgressBar1 = new ToolStripProgressBar();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripDropDownButton1 = new ToolStripDropDownButton();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            tabControl = new TabControl();
            imageList1 = new ImageList(components);
            tsBack = new ToolStripButton();
            toolStrip1 = new ToolStrip();
            TsRefresh = new ToolStripButton();
            txtUrl = new ToolStripSpringTextBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            closeTabsToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStripHistory = new ContextMenuStrip(components);
            statusStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripProgressBar1, toolStripStatusLabel1, toolStripDropDownButton1, toolStripStatusLabel2 });
            statusStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            statusStrip1.Location = new Point(0, 390);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(676, 24);
            statusStrip1.TabIndex = 4;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            toolStripProgressBar1.Name = "toolStripProgressBar1";
            toolStripProgressBar1.Size = new Size(100, 18);
            toolStripProgressBar1.Style = ProgressBarStyle.Continuous;
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.BorderSides = ToolStripStatusLabelBorderSides.Right;
            toolStripStatusLabel1.BorderStyle = Border3DStyle.Etched;
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(122, 19);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripDropDownButton1
            // 
            toolStripDropDownButton1.Alignment = ToolStripItemAlignment.Right;
            toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripDropDownButton1.Image = (Image)resources.GetObject("toolStripDropDownButton1.Image");
            toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            toolStripDropDownButton1.ShowDropDownArrow = false;
            toolStripDropDownButton1.Size = new Size(20, 22);
            toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            toolStripDropDownButton1.Click += toolStripDropDownButton1_Click;
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.BorderSides = ToolStripStatusLabelBorderSides.Right;
            toolStripStatusLabel2.BorderStyle = Border3DStyle.Etched;
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(122, 19);
            toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // tabControl
            // 
            tabControl.ImageList = imageList1;
            tabControl.Location = new Point(0, 30);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(382, 223);
            tabControl.TabIndex = 1;
            tabControl.SelectedIndexChanged += tabControl_SelectedIndexChanged;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageSize = new Size(16, 16);
            imageList1.TransparentColor = Color.Transparent;
            // 
            // tsBack
            // 
            tsBack.AutoSize = false;
            tsBack.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsBack.Image = Properties.Resources.Backwards;
            tsBack.ImageTransparentColor = Color.Magenta;
            tsBack.Name = "tsBack";
            tsBack.Size = new Size(23, 27);
            tsBack.Text = "Back";
            tsBack.Click += tsBack_Click;
            // 
            // toolStrip1
            // 
            toolStrip1.AutoSize = false;
            toolStrip1.BackColor = SystemColors.Control;
            toolStrip1.Items.AddRange(new ToolStripItem[] { tsBack, TsRefresh, txtUrl });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.RenderMode = ToolStripRenderMode.Professional;
            toolStrip1.Size = new Size(676, 30);
            toolStrip1.TabIndex = 5;
            toolStrip1.Text = "toolStrip1";
            // 
            // TsRefresh
            // 
            TsRefresh.DisplayStyle = ToolStripItemDisplayStyle.Image;
            TsRefresh.Image = (Image)resources.GetObject("TsRefresh.Image");
            TsRefresh.ImageTransparentColor = Color.Magenta;
            TsRefresh.Name = "TsRefresh";
            TsRefresh.Size = new Size(23, 27);
            TsRefresh.Text = "Refresh";
            TsRefresh.Click += TsRefresh_Click;
            // 
            // txtUrl
            // 
            txtUrl.BorderStyle = BorderStyle.FixedSingle;
            txtUrl.Name = "txtUrl";
            txtUrl.Size = new Size(587, 30);
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { closeTabsToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(163, 26);
            // 
            // closeTabsToolStripMenuItem
            // 
            closeTabsToolStripMenuItem.Name = "closeTabsToolStripMenuItem";
            closeTabsToolStripMenuItem.Size = new Size(162, 22);
            closeTabsToolStripMenuItem.Text = "Close Other Tabs";
            closeTabsToolStripMenuItem.Click += closeTabsToolStripMenuItem_Click;
            // 
            // contextMenuStripHistory
            // 
            contextMenuStripHistory.Name = "contextMenuStripHistory";
            contextMenuStripHistory.Size = new Size(61, 4);
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(676, 414);
            Controls.Add(statusStrip1);
            Controls.Add(tabControl);
            Controls.Add(toolStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Name = "Form1";
            Text = "ForumBrowser";
            Load += Form1_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private StatusStrip statusStrip1;
        private TabControl tabControl;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripButton tsBack;
        private ToolStrip toolStrip1;
        private ToolStripButton TsRefresh;
        private ImageList imageList1;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem closeTabsToolStripMenuItem;
        private ToolStripSpringTextBox txtUrl;
        private ToolStripProgressBar toolStripProgressBar1;
        private ContextMenuStrip contextMenuStripHistory;
    }
}
