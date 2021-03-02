using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using GenshinPlayerQuery.View;

namespace GenshinPlayerQuery
{
    static class MessageBus
    {

        private const int COOKIE_HTTP_ONLY = 0x00002000;

        private const string LOGIN_TICKET_FILE = "ticket.txt";
        private const string QUERY_HISTORY_FILE = "history.txt";
        private const int MAX_QUERY_HISTORY_COUNT = 10;

        public static string LoginTicket { get; set; } = "";
        public static List<string> QueryHistory { get; set; } = new List<string>();
        public static LoginWindow LoginWindow { get; set; }
        public static MainWindow MainWindow { get; set; }

        static MessageBus()
        {
            if (File.Exists(LOGIN_TICKET_FILE))
            {
                LoginTicket = File.ReadAllText(LOGIN_TICKET_FILE);
                SetBrowserLoginTicket(LoginTicket);
            }
            if (File.Exists(QUERY_HISTORY_FILE))
            {
                try
                {
                    string queryHistory = File.ReadAllText(QUERY_HISTORY_FILE);
                    QueryHistory.AddRange(Regex.Split(queryHistory.Trim(), "\r\n|\r|\n"));
                }
                catch
                {
                    MessageBox.Show("查询历史记录解析失败");
                }
            }
        }

        public static void AddQueryHistory(string uid)
        {
            if (!QueryHistory.Contains(uid))
            {
                MainWindow.ComboBoxUserId.Items.Add(uid);
                QueryHistory.Add(uid);
                if (QueryHistory.Count > MAX_QUERY_HISTORY_COUNT)
                {
                    MainWindow.ComboBoxUserId.Items.RemoveAt(0);
                    QueryHistory.RemoveAt(0);
                }
            }
        }

        public static void Exit()
        {
            StringBuilder queryHistory = new StringBuilder();
            foreach (string uid in QueryHistory)
            {
                queryHistory.AppendLine(uid);
            }
            File.WriteAllText(QUERY_HISTORY_FILE, queryHistory.ToString());
            Environment.Exit(0);
        }

        public static void Login()
        {
            MainWindow.Visibility = Visibility.Hidden;
            new LoginWindow().Show();
        }

        public static void AfterLoginSuccessful()
        {
            LoginTicket = GetBrowserLoginTicket();
            File.WriteAllText(LOGIN_TICKET_FILE, LoginTicket);
            LoginWindow.Close();
            MainWindow.Visibility = Visibility.Visible;
        }

        public static void AfterLoginFailed()
        {
            MessageBox.Show("工具需要您的米游社Cookie来调用查询接口\r\n此操作不会泄露您的账号信息", "提示", MessageBoxButton.OK);
            Exit();
        }

        public static void ShowRoleDetails(string uid, string server, string roleId)
        {
            new RoleWindow(uid, server, roleId).Show();
        }

        public static string GetBrowserLoginTicket()
        {
            string url = "https://user.mihoyo.com/";
            StringBuilder loginTicket = new StringBuilder();
            uint size = 256;
            InternetGetCookieEx(url, "login_ticket", loginTicket, ref size, COOKIE_HTTP_ONLY, IntPtr.Zero);
            return loginTicket.ToString();
        }

        public static void SetBrowserLoginTicket(string loginTicket)
        {
            if (loginTicket.StartsWith("login_ticket="))
            {
                loginTicket = loginTicket.Split('=')[1];
            }
            string url = "https://user.mihoyo.com/";
            InternetSetCookieEx(url, "login_ticket", loginTicket, COOKIE_HTTP_ONLY, IntPtr.Zero);
        }

        [DllImport("wininet.dll", SetLastError = true)]
        private static extern bool InternetGetCookieEx(
            string url, string cookieName, StringBuilder cookieData, 
            ref uint cookieSize, int flags, IntPtr reversed);

        [DllImport("wininet.dll", SetLastError = true)]
        private static extern int InternetSetCookieEx(
            string url, string cookieName, string cookieData,
            int flags, IntPtr reversed);
    }
}
