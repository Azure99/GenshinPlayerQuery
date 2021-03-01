using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            GC.Collect();
            string uid = ComboBoxUserId.Text;
            string server = ComboBoxServer.Text;
            PlayerData playerData = GenshinApi.GetPlayerData(uid, server);
            WebBrowserMain.NavigateToString(Render.RenderHtml(playerData));
        }

        private void WebBrowserMain_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            if (e.Uri != null)
            {
                if (e.Uri.Scheme == "rainng")
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
            Environment.Exit(0);
        }
    }
}
