
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using System.Net;

namespace boilersE2E.Core
{
    public abstract class E2ETestFixtureBase
    {
        protected const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        private static readonly Logger s_logger = LogManager.GetCurrentClassLogger();
        public static Process WinAppDriverProcess { get; set; }

        public static WindowsDriver<WindowsElement> Session { get; protected set; }

        /// <summary>
        /// WinAppDriver.exe を自動起動するかどうかの環境変数名です。
        /// 環境変数の値がtrueまたは1の場合は、WinAppDriver.exeを自動起動・自動終了します。
        /// 環境変数の値がそれ以外の値の場合は、WinAppDriver.exeを自動起動しません。このオプションはCIサーバーで別途WinAppDriverを実行している時に使用します。
        /// </summary>
        public static string EnvironmentVariableNameWhereWinAppDriverRunAutomatically { get; set; } = "BOILERS_E2ETEST_WINAPPDRIVER_AUTORUN";

        /// <summary>
        /// ウィンドウサイズを手動でセットするかどうかの環境変数名です。
        /// 環境変数の値がtrueまたは1の場合は、WindowSizeプロパティのサイズで設定します。
        /// 環境変数の値がそれ以外の値の場合は、ウィンドウサイズを最大化します。
        /// </summary>
        public static string EnvironmentVariableNameWhereSetWindowSizeManually { get; set; } = "BOILERS_E2ETEST_SET_WINDOWSIZE_MANUAL";

        /// <summary>
        /// テストするアプリケーションのパスを指定します。
        /// </summary>
        public abstract string AppPath { get; }

        /// <summary>
        /// テストするアプリケーションのウィンドウサイズを指定します。
        /// </summary>
        public abstract Size WindowSize { get; }

        /// <summary>
        /// 最前面にあるウィンドウを最大化します。
        /// </summary>
        public static void MaximizeWindow()
        {
            if (Session is null)
                return;
            Session.SwitchTo().Window(Session.WindowHandles.First()).Manage().Window.Maximize();
        }

        /// <summary>
        /// 指定した Func が完了するかタイムアウト時間が経過するまで待機し、Funcから得られる要素を返します。
        /// </summary>
        /// <param name="function">Funcオブジェクト</param>
        /// <param name="timeOutSeconds">タイムアウト秒数</param>
        /// <returns>functionから返される WindowsElement オブジェクト</returns>
        public static WindowsElement WaitForObject(Func<WindowsElement> function, int timeOutSeconds = 10)
        {

            WindowsElement waitElement = null;

            try
            {
                var wait = new DefaultWait<WindowsDriver<WindowsElement>>(Session)
                {
                    Timeout = TimeSpan.FromSeconds(timeOutSeconds),
                    PollingInterval = TimeSpan.FromSeconds(1)
                };

                wait.IgnoreExceptionTypes(typeof(WebDriverException));
                wait.IgnoreExceptionTypes(typeof(InvalidOperationException));
                wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.IgnoreExceptionTypes(typeof(NotFoundException));
                wait.IgnoreExceptionTypes(typeof(WebException));
                wait.IgnoreExceptionTypes(typeof(WebDriverTimeoutException));

                wait.Until(driver =>
                {
                    waitElement = function();

                    return waitElement != null && waitElement.Enabled && waitElement.Displayed;
                });

                return waitElement;

            }
            catch (Exception e)
            {
                s_logger.Error(e);
                return waitElement;
            }
        }

        /// <summary>
        /// 要素に文字列を入力します。
        /// </summary>
        /// <param name="text">入力する文字列</param>
        public static void InputText(string text)
        {
            Util.SetTextToClipboard(text);
            Session.Keyboard.PressKey(OpenQA.Selenium.Keys.Control + "v" + OpenQA.Selenium.Keys.Control);
        }

        /// <summary>
        /// AutomationIDで要素を検索し、取得します。
        /// </summary>
        /// <param name="automationId">AutomationID</param>
        /// <param name="timeOutSeconds">タイムアウト秒数</param>
        /// <returns>取得したWindowsElementオブジェクト</returns>
        public static WindowsElement GetElementByAutomationID(string automationId, int timeOutSeconds = 10)
        {
            WindowsElement element = null;

            var wait = new DefaultWait<WindowsDriver<WindowsElement>>(Session)
            {
                Timeout = TimeSpan.FromSeconds(timeOutSeconds),
                Message = $"Element with automationId \"{automationId}\" not found."
            };

            wait.IgnoreExceptionTypes(typeof(WebDriverException));

            try
            {
                wait.Until(Driver =>
                {
                    element = Driver.FindElementByAccessibilityId(automationId);
                    return element != null;
                });
            }
            catch (WebDriverTimeoutException ex)
            {
                s_logger.Error(ex);
                s_logger.Error($"automationId:{automationId}");
            }

            return element;
        }

        /// <summary>
        /// 名前で要素を検索し、取得します。
        /// </summary>
        /// <param name="name">要素の名前</param>
        /// <param name="timeOutSeconds">タイムアウト秒数</param>
        /// <returns>取得したWindowsElementオブジェクト</returns>
        public static WindowsElement GetElementByName(string name, int timeOutSeconds = 10)
        {
            WindowsElement element = null;

            var wait = new DefaultWait<WindowsDriver<WindowsElement>>(Session)
            {
                Timeout = TimeSpan.FromSeconds(timeOutSeconds),
                Message = $"Element with Name \"{name}\" not found."
            };

            wait.IgnoreExceptionTypes(typeof(WebDriverException));

            try
            {
                wait.Until(Driver =>
                {
                    element = Driver.FindElementByName(name);
                    return element != null;
                });
            }
            catch (WebDriverTimeoutException ex)
            {
                s_logger.Error(ex);
                s_logger.Error($"Name:{name}");
            }

            return element;
        }

        /// <summary>
        /// 要素を取得します。
        /// </summary>
        /// <param name="by">OpenQA.Selenium.Byオブジェクト</param>
        /// <param name="timeOutSeconds">タイムアウト秒数</param>
        /// <returns>取得したWindowsElementオブジェクト</returns>
        public static WindowsElement GetElementBy(By by, int timeOutSeconds = 10)
        {
            WindowsElement element = null;

            var wait = new DefaultWait<WindowsDriver<WindowsElement>>(Session)
            {
                Timeout = TimeSpan.FromSeconds(timeOutSeconds),
                Message = $"Element with By {by.ToString()} not found."
            };

            wait.IgnoreExceptionTypes(typeof(WebDriverException));

            try
            {
                wait.Until(Driver =>
                {
                    element = Driver.FindElement(by);
                    return element != null;
                });
            }
            catch (WebDriverTimeoutException ex)
            {
                s_logger.Error(ex);
            }

            return element;
        }

        /// <summary>
        /// AutomationIDで要素を検索し、存在するか検証します。
        /// </summary>
        /// <param name="automationId">AutomationID</param>
        /// <param name="timeOutSeconds">タイムアウト秒数</param>
        /// <returns>存在する場合は true、存在しない場合は false を返します。</returns>
        public static bool ExistsElementByAutomationID(string automationId, int timeOutSeconds = 10)
        {
            WindowsElement element = null;

            var wait = new DefaultWait<WindowsDriver<WindowsElement>>(Session)
            {
                Timeout = TimeSpan.FromSeconds(timeOutSeconds),
                Message = $"Element with automationId \"{automationId}\" not found."
            };

            wait.IgnoreExceptionTypes(typeof(WebDriverException));

            try
            {
                wait.Until(Driver =>
                {
                    element = Driver.FindElementByAccessibilityId(automationId);
                    return element != null;
                });
            }
            catch (WebDriverTimeoutException ex)
            {
            }
            catch (WebDriverException ex)
            {
            }

            return element != null;
        }

        /// <summary>
        /// 要素が存在するか検証します。
        /// </summary>
        /// <param name="by">OpenQA.Selenium.Byオブジェクト</param>
        /// <returns>存在する場合は true、存在しない場合は false を返します。</returns>
        public static bool IsElementPresent(By by)
        {
            try
            {
                Session.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (WebDriverException)
            {
                return false;
            }
        }

#if false
        /// <summary>
        /// xUnit v3 になるまで使用不可。
        /// スクリーンショットを撮影します。
        /// スクリーンショットの保存先ディレクトリは $(TargetDir) になります。
        /// </summary>
        /// <param name="filename">撮影したスクリーンショットの保存ファイル名</param>
        public static void TakeScreenShot(string filename)
        {
            Session.GetScreenshot().SaveAsFile($"{AppDomain.CurrentDomain.BaseDirectory}\\{filename}");
            TestContext.AddTestAttachment($"{AppDomain.CurrentDomain.BaseDirectory}\\{filename}");
        }
#endif

        /// <summary>
        /// WindowsDriverインスタンスを作成します。
        /// </summary>
        public abstract void CreateSession();
        
        protected static void QuitTargetApp()
        {
            s_logger.Debug($"Begin QuitTargetApp().");
            s_logger.Trace($"Session is null: {Session is null}");
            if (Session is null)
            {
                s_logger.Warn($"Exit QuitTargetApp() because Session is null.");
                return;
            }

            var exceptionCount = 0;
            while (Session.WindowHandles.Any())
            {
                try
                {
                    Session.SwitchTo().Window(Session.CurrentWindowHandle);
                    //Alt+F4によるアプリ終了
                    var actions = new Actions(Session);
                    actions.SendKeys(OpenQA.Selenium.Keys.Alt + OpenQA.Selenium.Keys.F4 + OpenQA.Selenium.Keys.Alt);
                    actions.Perform();
                    s_logger.Trace($"Send Alt+F4.");
                }
                catch (WebDriverException e)
                {
                    s_logger.Warn(e);
                    exceptionCount++;
                    if (exceptionCount == 10)
                    {
                        s_logger.Trace($"Go to kill mode.");
                        break;
                    }
                }
                s_logger.Trace($"Session.WindowHandles.Any(): {Session.WindowHandles.Any()}");
            }
            Session.WindowHandles.Select(x => Session.SwitchTo().Window(x)).ToList().ForEach(x => x.Dispose());
            s_logger.Trace($"Being Session.Quit().");
            Session.Quit();
            s_logger.Trace($"End Session.Quit().");
            Session = null;
            s_logger.Debug($"End QuitTargetApp().");
        }

        [Conditional("LOCALPC")]
        protected abstract void RebootWinAppDriver();
    }
}
