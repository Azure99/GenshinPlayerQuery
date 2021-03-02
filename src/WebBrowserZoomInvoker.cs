using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;

namespace GenshinPlayerQuery
{
    static class WebBrowserZoomInvoker
    {
        private const string EMPTY_HTML = @"<html><head></head><body></body></html>";

        private const int LOG_PIXELS_X = 88;
        private const int LOG_PIXELS_Y = 90;

        private const int OLE_CMD_EXEC_OPT_DO_DEFAULT = 0;
        private const int OLE_CMD_ID_OPTICAL_ZOOM = 63;

        public static void AddZoomInvoker(WebBrowser browser)
        {
            Point scaleUi = GetCurrentDipScale();
            if ((int) (scaleUi.X * 100) != 100)
            {
                browser.LoadCompleted += WebBrowser_LoadCompleted;
                browser.NavigateToString(EMPTY_HTML);
            }
        }

        private static void WebBrowser_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if (sender is WebBrowser browser)
            {
                browser.LoadCompleted -= WebBrowser_LoadCompleted;
                Point scaleUi = GetCurrentDipScale();
                if (100 != (int) (scaleUi.X * 100))
                {
                    SetZoom(browser, (int) (scaleUi.X * scaleUi.Y * 100));
                }
            }
        }

        private static Point GetCurrentDipScale()
        {
            Point scaleUi = new Point(1.0f, 1.0f);
            try
            {
                SetProcessDPIAware();
                IntPtr screenDc = GetDC(IntPtr.Zero);
                int dpiX = GetDeviceCaps(screenDc, LOG_PIXELS_X);
                int dpiY = GetDeviceCaps(screenDc, LOG_PIXELS_Y);

                scaleUi.X = (float) dpiX / 96.0f;
                scaleUi.Y = (float) dpiY / 96.0f;
                ReleaseDC(IntPtr.Zero, screenDc);
                return scaleUi;
            }
            catch
            {
                // ignored
            }

            return scaleUi;
        }


        static void SetZoom(WebBrowser webBrowser, int zoom)
        {
            try
            {
                if (null == webBrowser)
                {
                    return;
                }

                FieldInfo fiComWebBrowser = webBrowser.GetType().GetField(
                    "_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
                if (null != fiComWebBrowser)
                {
                    object objComWebBrowser = fiComWebBrowser.GetValue(webBrowser);

                    if (null != objComWebBrowser)
                    {
                        object[] args = {OLE_CMD_ID_OPTICAL_ZOOM, OLE_CMD_EXEC_OPT_DO_DEFAULT, zoom, IntPtr.Zero};
                        objComWebBrowser.GetType().InvokeMember("ExecWB", BindingFlags.InvokeMethod, null,
                            objComWebBrowser, args);
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr ptr);

        [DllImport("user32.dll")]
        private static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);

        [DllImport("gdi32.dll")]
        private static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}