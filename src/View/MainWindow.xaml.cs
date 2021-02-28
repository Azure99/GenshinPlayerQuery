using System;
using System.Windows;
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
            PlayerInfo playerInfo = GenshinApi.GetPlayerInfo(uid, server);
            DataContext = playerInfo;
            if (playerInfo != null)
            {
                WebBrowserAvatars.Source = new Uri("http://www.rainng.com/");
                WebBrowserAvatars.Source =
                    new Uri(
                        $"https://webstatic.mihoyo.com/app/community-game-records/index.html?bbs_presentation_style=fullscreen#/ys/role/all?role_id={uid}&server={server}");
            }
        }

        private void WebBrowserAvatars_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if (e.Uri.OriginalString.StartsWith("https://webstatic.mihoyo.com/app/community-game-records/index.html"))
            {
                WebBrowserAvatars.InvokeScript("eval", @"setInterval(function () {
                    var roles = document.getElementsByClassName('role-item');
                    for (var i = 0; i < roles.length; i++) {
                        roles[i].style.marginLeft = '5px';
                    }
                }, 100);");
            }

        }
    }
}
