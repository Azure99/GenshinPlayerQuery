using System.Diagnostics;
using System.Net;
using System.Windows;

namespace GenshinPlayerQuery.Core
{
    internal static class UpdateChecker
    {
        private const int NOW_VERSION = 13;
        private const string VERSION_URL = "https://www.rainng.com/ser/soft/genshin-player-query/version";
        private const string RELEASE_PAGE_URL = "https://github.com/Azure99/GenshinPlayerQuery/releases/";

        public static void CheckUpdate()
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    string versionStr = client.DownloadString(VERSION_URL);
                    int version = int.Parse(versionStr);
                    if (NOW_VERSION < version)
                    {
                        if (MessageBox.Show("检测到版本更新, 是否前往发布页面?", "版本更新", MessageBoxButton.YesNo,
                            MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            Process.Start(RELEASE_PAGE_URL);
                            MessageBus.Exit();
                        }
                    }
                }
            }
            catch
            {
                // ignored
            }
        }
    }
}