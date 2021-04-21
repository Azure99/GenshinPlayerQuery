using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using me.cqp.luohuaming.GenshinQuery.PublicInfos;

namespace me.cqp.luohuaming.GenshinQuery.Code
{
    public static class Helper
    {
        [DllImport("user32.dll")]
        private static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, uint nFlags);
        /// <summary>
        /// 保存WebBrowser页面为截图
        /// </summary>
        /// <param name="wb"></param>
        /// <returns></returns>
        public static Bitmap SnapWeb(WebBrowser wb)
        {
            HtmlDocument hd = wb.Document;
            int height = 2300;
            int width = 850;
            wb.Height = height;
            wb.Width = width;
            Bitmap bmp = new Bitmap(width, height);
            Rectangle rec = new Rectangle(0, 0, width, height);
            wb.DrawToBitmap(bmp, rec);
            return bmp;
        }
        public static string GetCookie()
        {
            string filePath = Path.Combine(MainSave.AppDirectory, "Cookie.txt");
            if (File.Exists(filePath) is false)
                return string.Empty;
            else
                return File.ReadAllText(filePath);
        }
    }
}
