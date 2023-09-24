using GenshinPlayerQuery.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GenshinPlayerQuery.View
{
    /// <summary>
    /// CaptchaWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CaptchaWindow : Window
    {
        private string _challenge;
        private string _gt;
        private bool _verifySuccessful;

        public CaptchaWindow(string challenge, string gt)
        {
            InitializeComponent();
            MessageBus.CaptchaWindow = this;
            WebViewCaptcha.EnsureCoreWebView2Async();
            _challenge = challenge;
            _gt = gt;
        }

        private void WebViewCaptcha_CoreWebView2InitializationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            WebViewCaptcha.CoreWebView2.Navigate($"https://www.rainng.com/ser/soft/genshin-player-query/static/geetest/v1/?challenge={_challenge}&gt={_gt}");
        }

        private void WebViewCaptcha_NavigationStarting(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs e)
        {
            Uri uri = new Uri(e.Uri);
            if (uri.Host == "rainng")
            {
                _verifySuccessful = true;
                NameValueCollection result = HttpUtility.ParseQueryString(uri.Query);
                MessageBus.AfterCaptchaChallengeSuccessful(result["geetest_challenge"], result["geetest_validate"], result["geetest_seccode"]);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!_verifySuccessful)
            {
                MessageBus.AfterCaptchaChallengeFailed();
            }
        }
    }
}
