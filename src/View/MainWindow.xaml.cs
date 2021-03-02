using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;
using GenshinPlayerQuery.Model;

namespace GenshinPlayerQuery.View
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MessageBus.MainWindow = this;
            WebBrowserZoomInvoker.AddZoomInvoker(WebBrowserMain);

            foreach (string uid in MessageBus.QueryHistory)
            {
                ComboBoxUserId.Items.Add(uid);
            }

            if (ComboBoxUserId.Items.Count > 0)
            {
                ComboBoxUserId.Text = ComboBoxUserId.Items[ComboBoxUserId.Items.Count - 1].ToString();
            }

            if (GenshinApi.GetLoginStatus())
            {
                Visibility = Visibility.Visible;
            }
            else
            {
                MessageBus.Login();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string uid = ComboBoxUserId.Text;
            string server = ComboBoxServer.Text;
            PlayerQueryResult playerQueryResult = GenshinApi.GetPlayerData(uid, server);
            if (!playerQueryResult.Success)
            {
                MessageBox.Show(playerQueryResult.Message, "查询失败", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            WebBrowserMain.NavigateToString(Render.RenderHtml(playerQueryResult));
            MessageBus.AddQueryHistory(uid);
        }

        private void WebBrowserMain_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.Uri != null)
            {
                if (e.Uri.Host == "rainng")
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    string[] args = e.Uri.Query.Substring(1).Split('&');
                    foreach (string arg in args)
                    {
                        string[] kv = arg.Split('=');
                        dic[kv[0]] = kv[1];
                    }
                    MessageBus.ShowRoleDetails(dic["uid"], dic["server"], dic["role"]);
                    e.Cancel = true;
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MessageBus.Exit();
        }

        private void HyperlinkProjectAddress_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start("https://github.com/Azure99/GenshinPlayerQuery");
        }

        private void HyperlinkByAzure99_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start("https://github.com/Azure99");
        }
    }
}
