
using CefSharp;
using CefSharp.WinForms;
using System.Windows.Forms;

namespace CefsharpRunner
{
    public partial class UserInterface : Form
    {

        public UserInterface()
        {
            InitializeComponent();

            Hide(); /// hide ourselves and delegate to the persistence helper so we're in the correct orientation to begin with
            InitializeChromium();
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            
            CefSharp.Cef.Shutdown(); // allow chromium to release memory gracefully

            base.OnFormClosing(e);
        }

        private void InitializeChromium()
        {
            // we can hook into a lot of chromium goodness... investigate what we may want/need https://github.com/cefsharp/CefSharp/wiki
            CefSettings settings = new CefSettings();
            

            settings.BackgroundColor = Cef.ColorSetARGB(0, 2, 90, 154); // attempt to prevent the white flash between page loads

            // Initialize
            Cef.Initialize(settings);

            ChromiumWebBrowser Browser = new ChromiumWebBrowser("https://php.net");
            Browser.Dock = DockStyle.Fill;

            //Browser.FrameLoadEnd += Browser_FrameLoadEnd;

        
            // prevent the ugly resizing until DockStyle.Fill takes effect
            Browser.Visible = false;
            this.Load += (o, a) =>
            {
                Browser.Visible = true;
            };

            this.Controls.Add(Browser);

        }
    }
}
