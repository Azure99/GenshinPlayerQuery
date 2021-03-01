using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
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
            string uid = ComboBoxUserId.Text;
            string server = ComboBoxServer.Text;
            PlayerData playerData = GenshinApi.GetPlayerData(uid, server);
            WebBrowserMain.NavigateToString(Render.RenderHtml(playerData));
            Debug.WriteLine(Render.RenderHtml(playerData));
        }
    }
}
