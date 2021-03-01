using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using GenshinPlayerQuery.View;

namespace GenshinPlayerQuery
{
    static class MessageBus
    {
        private const string LOGIN_TICKET_FILE = "ticket.txt";
        private const string QUERY_HISTORY_FILE = "history.txt";

        public static string LoginTicket { get; set; } = "";
        public static List<string> QueryHistory { get; set; } = new List<string>();
        public static LoginWindow LoginWindow { get; set; }
        public static MainWindow MainWindow { get; set; }

        static MessageBus()
        {
            if (File.Exists(LOGIN_TICKET_FILE))
            {
                LoginTicket = File.ReadAllText(LOGIN_TICKET_FILE);
            }

            if (File.Exists(QUERY_HISTORY_FILE))
            {
                try
                {
                    string queryHistory = File.ReadAllText(QUERY_HISTORY_FILE);
                    QueryHistory.AddRange(Regex.Split(queryHistory, "\r\n|\r|\n"));
                }
                catch
                {
                    MessageBox.Show("查询历史记录解析失败");
                }
            }
        }

        public static void ShowRoleDetails(string uid, string server, string roleId)
        {
            new RoleWindow(uid, server, roleId).Show();
        }

        public static void Login()
        {
            MainWindow.Visibility = Visibility.Hidden;
            new LoginWindow().Show();
        }

        public static void AfterLoginFailed()
        {
            MessageBox.Show("工具需要您的米游社Cookie来调用查询接口\r\n此操作不会泄露您的账号信息", "提示", MessageBoxButton.OK);
            Environment.Exit(0);
        }

        public static void AfterLoginSuccessful(string loginTicket)
        {
            LoginTicket = loginTicket;
            File.WriteAllText(LOGIN_TICKET_FILE, loginTicket);
            LoginWindow.Close();
            MainWindow.Visibility = Visibility.Visible;
        }
    }
}
