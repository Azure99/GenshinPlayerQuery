using System;
using System.ComponentModel;
using System.Windows;
using GenshinPlayerQuery.Core;
using Microsoft.Web.WebView2.Core;

namespace GenshinPlayerQuery.View
{
    /// <summary>
    ///     LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        private bool _loginSuccessful;

        public LoginWindow()
        {
            InitializeComponent();
            MessageBus.LoginWindow = this;
        }

        private void WebViewLogin_SourceChanged(object sender, CoreWebView2SourceChangedEventArgs e)
        {
            if (WebViewLogin.Source.ToString() == "https://user.mihoyo.com/#/account/home")
            {
                _loginSuccessful = true;
                MessageBus.AfterLoginSuccessful();
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!_loginSuccessful)
            {
                MessageBus.AfterLoginFailed();
            }
        }
    }
}