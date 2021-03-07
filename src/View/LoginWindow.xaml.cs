using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Navigation;
using GenshinPlayerQuery.Core;

namespace GenshinPlayerQuery.View
{
    /// <summary>
    ///     LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            MessageBus.LoginWindow = this;
        }

        private void WebBrowserLogin_Navigated(object sender, NavigationEventArgs e)
        {
            if (e.Uri != null && e.Uri.OriginalString == "https://user.mihoyo.com/#/account/home")
            {
                DialogResult = true;
                Close();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            WebBrowserLogin.Dispose();
        }
    }
}