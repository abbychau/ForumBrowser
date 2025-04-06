using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System;
using System.Windows.Forms;

namespace ForumBrowser
{
    /// <summary>
    /// Manages WebView2 environment and process lifecycle
    /// </summary>
    public static class WebView2Manager
    {
        private static CoreWebView2Environment? _sharedEnvironment;
        private static WebView2? _backgroundWebView;
        private static bool _isInitialized = false;

        /// <summary>
        /// Initializes the WebView2 environment that will be shared across all WebView2 controls
        /// </summary>
        public static async Task<CoreWebView2Environment> InitializeEnvironmentAsync(string? browserOptions = null, string? cacheDir = null)
        {
            if (_sharedEnvironment == null)
            {
                if (cacheDir == null)
                {
                    cacheDir = Environment.CurrentDirectory + @"\Cache\";
                    Directory.CreateDirectory(cacheDir);
                }

                var options = new CoreWebView2EnvironmentOptions(
                    browserOptions,
                    null,
                    null,
                    false,
                    null
                );

                _sharedEnvironment = await CoreWebView2Environment.CreateAsync(null, cacheDir, options);
                
                // Create a background WebView2 to keep processes alive
                EnsureBackgroundWebViewIsActive();
            }

            return _sharedEnvironment;
        }

        /// <summary>
        /// Gets the shared WebView2 environment
        /// </summary>
        public static CoreWebView2Environment? SharedEnvironment => _sharedEnvironment;

        /// <summary>
        /// Creates and initializes a background WebView2 to keep processes alive
        /// </summary>
        private static void EnsureBackgroundWebViewIsActive()
        {
            if (_backgroundWebView != null)
            {
                return;
            }

            // Create a hidden form to host the background WebView2
            Form backgroundForm = new Form
            {
                Width = 1,
                Height = 1,
                ShowInTaskbar = false,
                WindowState = FormWindowState.Minimized,
                FormBorderStyle = FormBorderStyle.None
            };

            _backgroundWebView = new WebView2();
            backgroundForm.Controls.Add(_backgroundWebView);
            
            // Handle process failures to restart the background WebView if needed
            _backgroundWebView.CoreWebView2InitializationCompleted += (s, e) =>
            {
                if (e.IsSuccess && _backgroundWebView?.CoreWebView2 != null)
                {
                    _backgroundWebView.CoreWebView2.ProcessFailed += (sender, args) =>
                    {
                        // Recreate the background WebView if a process fails
                        _backgroundWebView?.Dispose();
                        _backgroundWebView = null;
                        EnsureBackgroundWebViewIsActive();
                    };

                    // Navigate to a blank page
                    _backgroundWebView.Source = new Uri("about:blank");
                    _isInitialized = true;
                }
            };

            // Initialize the background WebView with the shared environment
            if (_sharedEnvironment != null)
            {
                _backgroundWebView.EnsureCoreWebView2Async(_sharedEnvironment);
            }

            // Show the form but keep it hidden
            backgroundForm.Show();
            backgroundForm.Hide();
        }

        /// <summary>
        /// Initializes a WebView2 control with the shared environment
        /// </summary>
        public static async Task InitializeWebView2Async(WebView2 webView)
        {
            if (_sharedEnvironment == null)
            {
                await InitializeEnvironmentAsync();
            }

            await webView.EnsureCoreWebView2Async(_sharedEnvironment);
        }

        /// <summary>
        /// Cleans up resources when the application is shutting down
        /// </summary>
        public static void Cleanup()
        {
            _backgroundWebView?.Dispose();
            _backgroundWebView = null;
            _sharedEnvironment = null;
            _isInitialized = false;
        }
    }
}
