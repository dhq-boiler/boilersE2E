using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;

namespace boilersE2E
{
    /// <summary>
    /// 拡張メソッドを管理するExtensionsクラスです。
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// WindowsElementオブジェクトに文字列を送信するActionsオブジェクトを構成します。
        /// </summary>
        /// <param name="actions">構成するActionsオブジェクト</param>
        /// <param name="element">文字列を送信するWindowsElement</param>
        /// <param name="text">送信する文字列</param>
        public static void InputText(this Actions actions, WindowsElement element, string text)
        {
            Util.SetTextToClipboard(text);
            actions.SendKeys(element, Keys.Control + "v" + Keys.Control);
        }
    }
}
