using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Security.Policy;
using System;

namespace ForumBrowser
{
    public partial class Form1 : Form
    {
        private readonly Settings settings;
        private Dictionary<WebView2, List<(string title, string url)>> histories = new();
        public Form1()
        {
            InitializeComponent();
            settings = new(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "";
            toolStrip1.Renderer = new ToolStripProfessionalRenderer()
            {
                RoundedEdges = false
            };
            void resizeCallback(object? s, EventArgs e)
            {
                int _padding = 5;
                tabControl.Left = _padding;
                tabControl.Width = this.Width - (20 + _padding);
                tabControl.Height = this.Height - 95;
                int countOfToolStripItems = toolStrip1.Items.Count;
                int toolStripItemWidth = toolStrip1.Items[0].Width;
                //txtUrl.Left = countOfToolStripItems * 30;
                txtUrl.Width = this.Width - (countOfToolStripItems * toolStripItemWidth + 35);
            }

            //form1 resize callback
            this.Resize += resizeCallback;
            resizeCallback(this, new EventArgs());



            // when double click on tabcontrol empty space, add a new tab
            // workaround: use form1 double click event and check if the mouse is on the tabcontrol
            this.DoubleClick += (s, e) =>
            {


                if (tabControl.Bounds.Contains(((MouseEventArgs)e).Location))
                {
                    NewTab("about:blank");
                    txtUrl.Text = "about:blank";
                }
            };
            

            // on tab middle click, close the tab
            tabControl.MouseClick += (s, e) =>
            {
                if (e.Button == MouseButtons.Middle)
                {
                    for (int i = 0; i < tabControl.TabPages.Count; i++)
                    {
                        if (tabControl.GetTabRect(i).Contains(e.Location))
                        {
                            tabControl.TabPages.RemoveAt(i);
                            break;
                        }
                    }
                }

                // on right click, show context menu
                if (e.Button == MouseButtons.Right)
                {
                    contextMenuStrip1.Show(tabControl, e.Location);
                }
            };

            //on tab close, clear histories[webview2]
            tabControl.ControlRemoved += (s, e) =>
            {
                if (e.Control is WebView2 webview2)
                {
                    histories.Remove(webview2);
                }
            };

            // on tab change event, change the txtUrl.Text
            tabControl.SelectedIndexChanged += (s, e) =>
            {
                if (tabControl.SelectedTab != null && tabControl.SelectedTab.Controls.Count != 0)
                {
                    WebView2 webview2 = (WebView2)tabControl.SelectedTab.Controls[0];
                    txtUrl.Text = webview2.Source.ToString();
                    SetFormTitle(webview2.CoreWebView2.DocumentTitle);
                }
            };
            bool boolTxtFirstFocus = false;
            txtUrl.LostFocus += (s, e) =>
            {
                boolTxtFirstFocus = true;
                if (txtUrl.Text == "")
                {
                    var currentTab = tabControl.SelectedTab;
                    if (currentTab != null && currentTab.Controls.Count != 0)
                    {
                        WebView2 webview2 = (WebView2)currentTab.Controls[0];
                        txtUrl.Text = webview2.Source.ToString();
                    }
                }
            };
            txtUrl.Click += (s, e) =>
            {
                if (boolTxtFirstFocus)
                {
                    txtUrl.SelectAll();
                    boolTxtFirstFocus = false;
                }
            };

            //TxtUrl_KeyDown
            txtUrl.KeyDown += (s, e) =>
            {
                //if ctrl+enter is pressed, add a new tab
                if (e.Control && e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;

                    EnteringUrl(true);

                    e.Handled = true;
                }

                //if enter is pressed, load the URL
                if (e.KeyCode == Keys.Enter)
                {

                    e.SuppressKeyPress = true;
                    // check if no tab is selected
                    bool isNewTab = tabControl.SelectedTab == null;
                    EnteringUrl(isNewTab);
                    e.Handled = true;
                }
            };

            //tsback right click, show context menu
            tsBack.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    //set contextMenuStripHistory items
                    contextMenuStripHistory.Items.Clear();
                    if (tabControl.SelectedTab != null && tabControl.SelectedTab.Controls.Count != 0)
                    {
                        WebView2 webview2 = (WebView2)tabControl.SelectedTab.Controls[0];
                        if (histories.ContainsKey(webview2))
                        {
                            foreach (var item in histories[webview2])
                            {
                                ToolStripMenuItem menuItem = new()
                                {
                                    Text = item.title ?? item.url,
                                    Tag = item
                                };
                                menuItem.Click += (s, e) =>
                                {
                                    webview2.CoreWebView2.Navigate(item.url);
                                };
                                contextMenuStripHistory.Items.Add(menuItem);
                            }
                        }
                    }


                    contextMenuStripHistory.Show(this, e.Location);
                }
            };


            txtUrl.Text = "https://www.google.com";
            NavigateToUrl(txtUrl.Text);
        }

        private void TxtUrl_LostFocus(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SetFormTitle(string title)
        {
            this.Text = title + " - ForumBrowser";
            if (title == "")
            {
                this.Text = "ForumBrowser";
            }
        }
        public async void NewTab(string url)
        {
            // this will add a new tab to tabControl and add a new WebView2 control to it
            // after that, it will load URL to txtUrl.Text

            TabPage tab = new()
            {
                Text = "New Tab"
            };
            tabControl.TabPages.Add(tab);

            WebView2 webview2 = new()
            {
                Dock = DockStyle.Fill
            };
            /* disabled features:
             * msWebOOUI,msPdfOOUI: ¸T¤î°g§A¿ï³æ
             * 
             */
            string browserOptions = "--disable-features=msWebOOUI,msPdfOOUI,msSmartScreenProtection,ElasticOverscroll";
            browserOptions += " --disable-background-networking "; //--user-agent=\"ForumBrowser 1.0\"
            if (settings.AllowCORS)
            {
                browserOptions += " --disable-web-security";
            }
            if (settings.UnlimitedStorage)
            {
                browserOptions += " --disable-features=ElasticOverscroll";
            }
            string cacheDir = Environment.CurrentDirectory + @"\Cache\";
            Directory.CreateDirectory(cacheDir);
            var options = new CoreWebView2EnvironmentOptions(
                browserOptions,
                null,
                null,
                settings.AllowSSO,
                null
            );
            var env = await CoreWebView2Environment.CreateAsync(null, cacheDir, options);

            await webview2.EnsureCoreWebView2Async(
                env
            );
            histories[webview2] = [];

            //pass hotkey to form1
            webview2.KeyDown += (s, e) =>
            {
                //log to toolstrip

                toolStripStatusLabel1.Text = e.KeyCode.ToString();
                toolStripStatusLabel1.Text += " " + e.Control.ToString();

                if (e.KeyCode == Keys.T)
                {
                    NewTab("about:blank");
                    txtUrl.Text = "about:blank";

                }

                if (e.KeyCode == Keys.W)
                {

                    if (tabControl.SelectedTab != null)
                        tabControl.TabPages.Remove(tabControl.SelectedTab);

                }

                //ctrl+1, ctrl+2, ctrl+3, ctrl+4, ctrl+5, ctrl+6, ctrl+7, ctrl+8, ctrl+9
                if (e.KeyCode >= Keys.D1 && e.KeyCode <= Keys.D9)
                {
                    int index = e.KeyCode - Keys.D1;
                    if (index < tabControl.TabPages.Count)
                    {
                        tabControl.SelectedIndex = index;
                    }
                }

                //alt+d / ctrl+l
                if (e.KeyCode == Keys.D || e.KeyCode == Keys.L)
                {
                    txtUrl.Focus();
                    txtUrl.SelectAll();
                }

                contextMenuStripHistory.Hide();
            };
            //keypress
            webview2.KeyPress += (s, e) =>
            {

                this.OnKeyPress(e);
            };
            tab.Controls.Add(webview2);
            url = ConvertToValidUrl(url);
            webview2.Source = new Uri(url);
            
            //one webview2 get focus, hide history context menu
            webview2.GotFocus += (s, e) =>
            {
                contextMenuStripHistory.Hide();
            };

            //WebView2.NavigationCompleted ¡÷ is raised when the WebView has completely loaded(body.onload has been raised) or loading stopped with error.
            //WebView2.CoreWebView2.DOMContentLoaded ¡÷ is raised when the initial html document has been parsed.This aligns with the the document's DOMContentLoaded event in html. (This one is available starting from 1.0.705.50.)
            // update progress bar
            webview2.CoreWebView2.NavigationStarting += (s, e) =>
            {
                toolStripProgressBar1.Value = 10;
            };
            webview2.CoreWebView2.NavigationCompleted += (s, e) =>
            {
                histories[webview2].Add(
                    (
                        webview2.CoreWebView2.DocumentTitle == "" ? webview2.Source.ToString() : webview2.CoreWebView2.DocumentTitle,
                        webview2.Source.ToString()
                    )
                );
                toolStripProgressBar1.Value += 45;
            };

            webview2.CoreWebView2.DOMContentLoaded += (s, e) =>
            {
                toolStripProgressBar1.Value += 45;
            };
            webview2.CoreWebView2.ContentLoading += (s, e) =>
            {
                toolStripStatusLabel2.Text = "ContentLoaded";
            };
            //ContextMenuRequested 
            webview2.CoreWebView2.ContextMenuRequested += (s, args) =>
            {
                    var newItem = webview2.CoreWebView2.Environment.CreateContextMenuItem(
                        "Open in Default Browser", null, CoreWebView2ContextMenuItemKind.Command);

                    newItem.CustomItemSelected += (send, ex) =>
                    {
                        System.Diagnostics.Process.Start("explorer.exe", args.ContextMenuTarget.PageUri);
                    };

                    args.MenuItems.Insert(args.MenuItems.Count, newItem);
            };

            webview2.CoreWebView2.DocumentTitleChanged += (s, e) =>
            {
                tab.Text = webview2.CoreWebView2.DocumentTitle;
                //if tab is selected, change the form title
                if (tabControl.SelectedTab == tab)
                {
                    SetFormTitle(webview2.CoreWebView2.DocumentTitle);
                }
            };
            webview2.CoreWebView2.FaviconChanged += async (s, e) =>
            {
                //corewebview obj
                if (s is not CoreWebView2 coreWebView2)
                {
                    return;
                }
                Stream iconStream = await coreWebView2.GetFaviconAsync(CoreWebView2FaviconImageFormat.Png);
                string iconUrl = coreWebView2.FaviconUri;
                imageList1.Images.Add(iconUrl, Image.FromStream(iconStream));

                tab.ImageIndex = imageList1.Images.IndexOfKey(iconUrl);
            };

            //on url change, change the txtUrl.Text
            webview2.CoreWebView2.NavigationStarting += (s, e) =>
            {
                contextMenuStripHistory.Hide();
                // only if tab is selected
                if (tabControl.SelectedTab == tab)
                    txtUrl.Text = e.Uri;
            };


            //on hover, change the status bar text
            webview2.CoreWebView2.Settings.IsStatusBarEnabled = false;
            webview2.CoreWebView2.StatusBarTextChanged += (s, e) =>
            {
                string? txt = (s as CoreWebView2)?.StatusBarText;
                txt = txt == null ? "" : txt;
                toolStripStatusLabel1.Text = txt;
            };
            //WebResourceRequested 
            webview2.CoreWebView2.WebResourceRequested += (s, e) =>
            {
                //log to toolstrip
                toolStripStatusLabel1.Text = "Requesting " + e.Request.Uri;
            };
            //WebResourceResponseReceived 
            webview2.CoreWebView2.WebResourceResponseReceived += (s, e) =>
            {
                //log to toolstrip
                toolStripStatusLabel2.Text = "Response " + e.Response.StatusCode + ": " + e.Request.Uri;
            };
            webview2.CoreWebView2.WebMessageReceived += (s, e) =>
            {
                //log to toolstrip
                toolStripStatusLabel1.Text = "WebMessageReceived " + e.WebMessageAsJson;
            };

            tabControl.SelectedTab = tab;

            await webview2.EnsureCoreWebView2Async(null);
            // add to webview2.CoreWebView2.NewWindowRequested
            webview2.CoreWebView2.NewWindowRequested += (s, e) =>
            {
                e.Handled = true;
                NewTab(e.Uri);
            };
        }
        private void NavigateToUrl(string url)
        {
            // this will navigate to the URL in txtUrl.Text
            WebView2 webview2;
            if (tabControl.SelectedTab != null && tabControl.SelectedTab.Controls.Count != 0)
            {
                webview2 = (WebView2)tabControl.SelectedTab.Controls[0];
                url = ConvertToValidUrl(url);
                webview2.Source = new Uri(url);


            }
            else
            {
                NewTab(url);
            }
        }
        private void BtnAddTab_Click(object sender, EventArgs e)
        {
            NewTab(txtUrl.Text);
        }

        //convert url into a valid url
        static string ConvertToValidUrl(string url)
        {
            // skip about:blank
            if (url == "about:blank")
            {
                return url;
            }
            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
            {
                url = "https://" + url;
            }
            return url;
        }

        //fixable url (check if the url is url-like but just without the protocol)
        // usage: if the url is not fixable, we do a google search instead of navigating to the url
        static bool IsFixableUrl(string url)
        {
            if (url.Contains(' ') || url.Contains('\n') || url.Contains('\t'))
            {
                return false;
            }
            if (url.Contains('.'))
            {
                return true;
            }
            return false;
        }

        private void EnteringUrl(bool isNewTab)
        {
            string url = txtUrl.Text;
            if (IsFixableUrl(url))
            {
                url = ConvertToValidUrl(url);
                txtUrl.Text = url;
            }
            else
            {
                txtUrl.Text = "https://www.google.com/search?q=" + url;
            }
            if (!isNewTab)
            {
                NavigateToUrl(txtUrl.Text);
            }
            else
            {
                NewTab(txtUrl.Text);
            }
            //focus the webview2
            if (tabControl.SelectedTab != null && tabControl.SelectedTab.Controls.Count != 0)
            {
                WebView2 webview2 = (WebView2)tabControl.SelectedTab.Controls[0];
                webview2.Focus();
            }
        }


        private void tsBack_Click(object sender, EventArgs e)
        {
            // this will navigate back
            WebView2 webview2;
            if (tabControl.SelectedTab != null && tabControl.SelectedTab.Controls.Count != 0)
            {
                webview2 = (WebView2)tabControl.SelectedTab.Controls[0];
                if (webview2.CoreWebView2.CanGoBack)
                {
                    webview2.CoreWebView2.GoBack();
                }
            }
        }

        private void TsRefresh_Click(object sender, EventArgs e)
        {
            // this will refresh the page
            WebView2 webview2;
            if (tabControl.SelectedTab != null && tabControl.SelectedTab.Controls.Count != 0)
            {
                webview2 = (WebView2)tabControl.SelectedTab.Controls[0];
                webview2.CoreWebView2.Reload();
            }
        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            string helpMessage = "Ctrl+T: New Tab\nCtrl+W: Close Tab\nCtrl+1-9: Switch Tab\nAlt+D/Ctrl+L: Focus URL Bar\nCtrl+Enter/Enter: Load URL\nMiddle Click Tab: Close Tab\nDouble Click Tab: New Tab\nCtrl+R: Refresh\nCtrl+Left/Right: Back/Forward";
            MessageBox.Show(helpMessage, "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {
            settings.ShowDialog();
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void closeTabsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // this will close all tabs except the below the menu
            int index = tabControl.SelectedIndex;
            for (int i = tabControl.TabPages.Count - 1; i > index; i--)
            {
                tabControl.TabPages.RemoveAt(i);
            }
            for (int i = index - 1; i >= 0; i--)
            {
                tabControl.TabPages.RemoveAt(i);
            }
        }
    }
}
