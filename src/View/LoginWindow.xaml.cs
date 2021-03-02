using System;
using System.Windows;
using GenshinPlayerQuery.Core;

namespace GenshinPlayerQuery.View
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        private bool _loginSuccessful;

        public LoginWindow()
        {
            InitializeComponent();
            MessageBus.LoginWindow = this;
        }

        private void WebBrowserLogin_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if (e.Uri != null && e.Uri.OriginalString == "https://user.mihoyo.com/#/account/home")
            {
                _loginSuccessful = true;
                MessageBus.AfterLoginSuccessful();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!_loginSuccessful)
            {
                MessageBus.AfterLoginFailed();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            WebBrowserLogin.Dispose();
        }
    }
}