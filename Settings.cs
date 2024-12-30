using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForumBrowser
{
    public partial class Settings : Form
    {
        Form1 parentForm;
        //export some public vars
        public bool AllowSSO
        {
            get { return chkAllowSSO.Checked; }
            set { chkAllowSSO.Checked = value; }
        }
        public bool AllowCORS
        {
            get { return chkAllowCORS.Checked; }
            set { chkAllowCORS.Checked = value; }
        }
        public bool UnlimitedStorage
        {
            get { return chkUnlimitedStorage.Checked; }
            set { chkUnlimitedStorage.Checked = value; }
        }

        public Settings(Form1 parentForm)
        {
            this.parentForm = parentForm;
            InitializeComponent();
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey("ForumBrowser"))
            {
                chkAllowSSO.Checked = Convert.ToBoolean(key.GetValue("AllowSSO", false));
                chkAllowCORS.Checked = Convert.ToBoolean(key.GetValue("AllowCORS", false));
                chkUnlimitedStorage.Checked = Convert.ToBoolean(key.GetValue("UnlimitedStorage", true));
            }

        }

        private void btnSetAsDefault_Click(object sender, EventArgs e)
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey("ForumBrowser"))
            {
                key.SetValue("", "URL:Forum Browser Protocol");
                key.SetValue("URL Protocol", "");
                using (RegistryKey shell = key.CreateSubKey("shell"))
                {
                    using (RegistryKey open = shell.CreateSubKey("open"))
                    {
                        using (RegistryKey command = open.CreateSubKey("command"))
                        {
                            command.SetValue("", "\"" + Application.ExecutablePath + "\" \"%1\"");
                        }
                    }
                }
            }

        }

        private void btnDeregister_Click(object sender, EventArgs e)
        {
            // Remove the registry key
            Registry.CurrentUser.DeleteSubKeyTree("ForumBrowser");

        }

        private void Settings_Load(object sender, EventArgs e)
        {
            //load the settings

        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            string helpMessage = "";
            MessageBox.Show(helpMessage, "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void chkAllowSSO_CheckedChanged(object sender, EventArgs e)
        {
            // Save the setting
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey("ForumBrowser"))
            {
                key.SetValue("AllowSSO", chkAllowSSO.Checked);
            }
        }

        private void chkAllowCORS_CheckedChanged(object sender, EventArgs e)
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey("ForumBrowser"))
            {
                key.SetValue("AllowCORS", chkAllowCORS.Checked);
            }
        }

        private void chkUnlimitedStorage_CheckedChanged(object sender, EventArgs e)
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey("ForumBrowser"))
            {
                key.SetValue("UnlimitedStorage", chkUnlimitedStorage.Checked);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = "https://github.com/abbychau/ForumBrowser";
            this.Close();
            parentForm.NewTab(url);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
