using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;

namespace boilersE2E
{
    /// <summary>
    /// 拡張メソッドを管理するExtensionsクラスです。
    /// </summary>
    public static class Extensions
    {
        private static readonly Logger s_logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// WindowsElementオブジェクトに文字列を送信するActionsオブジェクトを構成します。
        /// </summary>
        /// <param name="actions">構成するActionsオブジェクト</param>
        /// <param name="element">文字列を送信するWindowsElement</param>
        /// <param name="text">送信する文字列</param>
        public static void InputText(this Actions actions, WindowsElement element, string text)
        {
            Util.SetTextToClipboard(text);
            actions.SendKeys(element, OpenQA.Selenium.Keys.Control + "v" + OpenQA.Selenium.Keys.Control);
        }

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
                    s_logger.Error(e);
                }
            }
        }
    }
}
