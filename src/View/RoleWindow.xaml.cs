using System;
using System.Windows;
using GenshinPlayerQuery.Core;

namespace GenshinPlayerQuery.View
{
    /// <summary>
    ///     RoleWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RoleWindow : Window
    {
        public RoleWindow(string html)
        {
            InitializeComponent();
            WebBrowserZoomInvoker.AddZoomInvoker(WebBrowserRole);
            bool loaded = false;
            WebBrowserRole.LoadCompleted += (sender, args) =>
            {
                if (!loaded)
                {
                    WebBrowserRole.NavigateToString(html);
                    loaded = true;
                }
            };
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            WebBrowserRole.Dispose();
        }
    }
}