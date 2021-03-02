using System;
using System.Windows;

namespace GenshinPlayerQuery.View
{
    /// <summary>
    /// RoleWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RoleWindow : Window
    {
        public RoleWindow(string uid, string server, string roleId)
        {
            InitializeComponent();
            WebBrowserRole.Source =
                new Uri(
                    $"https://webstatic.mihoyo.com/app/community-game-records/index.html?bbs_presentation_style=fullscreen#/ys/role?role_id={uid}&server={server}&id={roleId}");
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            WebBrowserRole.Dispose();
        }
    }
}
