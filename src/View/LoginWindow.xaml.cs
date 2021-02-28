using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;

namespace GenshinPlayerQuery.View
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        private const int COOKIE_HTTP_ONLY = 0x00002000;
        private bool _loginSuccessful;

        public LoginWindow()
        {
            InitializeComponent();
            MessageBus.LoginWindow = this;
        }

        private void LoginBrowser_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if (e.Uri.OriginalString == "https://user.mihoyo.com/#/account/home")
            {
                string url = "https://user.mihoyo.com/";
                StringBuilder loginTicket = new StringBuilder();
                uint size = 256;
                InternetGetCookieEx(url, "login_ticket", loginTicket, ref size, COOKIE_HTTP_ONLY, IntPtr.Zero);
                _loginSuccessful = true;
                MessageBus.AfterLoginSuccessful(loginTicket.ToString());
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!_loginSuccessful)
            {
                MessageBus.AfterLoginFailed();
            }
        }

        [DllImport("wininet.dll", SetLastError = true)]
        private static extern bool InternetGetCookieEx(
            string url, string cookieName, StringBuilder cookieData, ref uint cookieSize, int flags,
            IntPtr reversed);
    }
}