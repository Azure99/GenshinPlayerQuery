using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using GenshinPlayerQuery.Model;
using GenshinPlayerQuery.View;
using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json.Linq;

namespace GenshinPlayerQuery.Core
{
    internal static class MessageBus
    {
        private const string MINIMUM_WEBVIEW2_VERSION = "94.0.992.31";

        private const string COOKIE_URL = "https://user.mihoyo.com/";

        private const string LOGIN_TICKET_FILE = "ticket.txt";
        private const string QUERY_HISTORY_FILE = "history.txt";
        private const int MAX_QUERY_HISTORY_COUNT = 10;

        public static PlayerQueryResult PlayerQueryResult { get; set; }

        static MessageBus()
        {
            new Thread(UpdateChecker.CheckUpdate).Start();

            if (File.Exists(LOGIN_TICKET_FILE))
            {
                LoginTicket = File.ReadAllText(LOGIN_TICKET_FILE);
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

        public static string LoginTicket { get; set; }
        public static List<string> QueryHistory { get; set; } = new List<string>();
        public static LoginWindow LoginWindow { get; set; }
        public static MainWindow MainWindow { get; set; }
        public static CaptchaWindow CaptchaWindow { get; set; }

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

        public static bool CheckWebView2Runtime()
        {
            Debug.WriteLine("version");
            try
            {
                string localVersion = CoreWebView2Environment.GetAvailableBrowserVersionString();
                if (CoreWebView2Environment.CompareBrowserVersions(localVersion, MINIMUM_WEBVIEW2_VERSION) >= 0)
                {
                    return true;
                }
            }
            catch
            { }
            return false;
        }

        public static void Login()
        {
            if (!CheckWebView2Runtime())
            {
                if (MessageBox.Show("系统需要安装WebView2, 是否立刻安装?", "WebView2缺失", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Process.Start(new ProcessStartInfo("https://go.microsoft.com/fwlink/p/?LinkId=2124703")
                    {
                        UseShellExecute = true
                    });
                }
                Exit();
            }
            MainWindow.Visibility = Visibility.Hidden;
            new LoginWindow().Show();
        }

        public static async void AfterLoginSuccessful()
        {
            LoginTicket = await GetBrowserLoginTicket();
            File.WriteAllText(LOGIN_TICKET_FILE, LoginTicket);
            LoginWindow.Close();
            if (GenshinApi.GetNeedCaptcha())
            {
                CaptchaChallenge();
            }
            else
            {
                MainWindow.Visibility = Visibility.Visible;
            }
        }

        public static void AfterLoginFailed()
        {
            MessageBox.Show("工具需要您的米游社Cookie来调用查询接口\r\n此操作不会泄露您的账号信息", "提示", MessageBoxButton.OK);
            Exit();
        }

        public static void CaptchaChallenge()
        {
            ServerResponse<CaptchaChallenge> challenge = GenshinApi.CreateCaptchaChallenge();
            new CaptchaWindow(challenge.Data.Challenge, challenge.Data.GT).Show();
        }

        public static void AfterCaptchaChallengeSuccessful(string challenge, string validate, string secCode)
        {
            GenshinApi.VerifyCaptchaChallenge(new CaptchaVerification
            {
                Challenge = challenge,
                Validate = validate,
                SecCode = secCode
            });
            CaptchaWindow.Close();
            MainWindow.Visibility = Visibility.Visible;
        }

        public static void AfterCaptchaChallengeFailed()
        {
            MessageBox.Show("请完成验证码认证", "提示", MessageBoxButton.OK);
            Exit();
        }

        public static void ShowRoleDetails(string roleId)
        {
            string role = JObject.Parse(PlayerQueryResult.Roles)["avatars"].First(x => x["id"].ToString() == roleId).ToString();
            new RoleWindow(PageRender.RenderRolePage(role)).Show();
        }

        public static async Task<string> GetBrowserLoginTicket()
        {
            List<CoreWebView2Cookie> cookies = await LoginWindow.WebViewLogin.CoreWebView2.CookieManager.GetCookiesAsync(COOKIE_URL);
            return string.Join("; ", cookies.Select(cookie => $"{cookie.Name}={cookie.Value}"));
        }
    }
}