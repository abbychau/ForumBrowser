namespace ForumBrowser
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Register application exit event to ensure proper cleanup
            Application.ApplicationExit += (s, e) => WebView2Manager.Cleanup();

            Application.Run(new Form1());
        }
    }
}