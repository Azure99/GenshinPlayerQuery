using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
