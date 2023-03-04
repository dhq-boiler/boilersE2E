using boilersE2E.Core;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;

namespace boilersE2E
{
    /// <summary>
    /// 拡張メソッドを管理するExtensionsクラスです。
    /// </summary>
    public static class Extensions
    {
        private static readonly Logger s_logger = LogManager.GetCurrentClassLogger();


        public static void ClearRepeatedlyUntilTimeout(this WindowsElement element, int timeoutSeconds = 10)
        {
            var timespan = TimeSpan.FromSeconds(timeoutSeconds);
            var beginDateTime = DateTime.Now;
            while ((DateTime.Now - beginDateTime) < timespan)
            {
                try
                {
                    element.Clear();
                    break;
                }
                catch (WebDriverException e)
                {
                    s_logger.Warn(e);
                }
            }
        }

        public static string ToStringEx(this TimeSpan ts, string format)
        {
            string hh = ts.ToString("hh");
            if (ts < TimeSpan.Zero) { hh = "-" + hh; }
            string mm = ts.ToString("mm");
            string ss = ts.ToString("ss");

            format = format.Replace("hh", hh);
            format = format.Replace("mm", mm);
            format = format.Replace("ss", ss);

            return format;
        }
    }
}
