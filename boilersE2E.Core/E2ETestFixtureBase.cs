
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using System.Net;
using WindowsInput;
using WindowsInput.Native;

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

        protected void FocusFrontWindow()
        {
            try
            {
                var allWindowHandles = Session.WindowHandles; //hold focus on the active window
                var frontWindow = allWindowHandles.LastOrDefault();
                if (frontWindow is null)
                {
                    s_logger.Warn($"empty target.");
                    return;
                }
                Session.SwitchTo().Window(frontWindow);
                Thread.Sleep(100);
            }
            catch (WebDriverException e)
            {
                s_logger.Error(e);
            }
        }
        
        /// <summary>
        /// 要素に文字列を入力します。
        /// </summary>
        /// <param name="text">入力する文字列</param>
        public void InputText(WindowsElement elm, string text)
        {
            int count = 0;
            while (true)
            {
                try
                {
                    InputSimulator sim = new InputSimulator();

                    ActionWithLog(() => Session.SwitchTo().Window(Session.CurrentWindowHandle), "A");

                    FocusFrontWindow();

                    //フォーカスを外す
                    ActionWithLog(() => ExistsElementByAutomationID("DUMMY-ELEMENT", 100), "B");

                    //フォーカスする
                    ActionWithLog(() => elm.Click(), "C");

                    if (elm.Displayed)
                    {
                        ActionWithLog(() => sim.Keyboard.KeyDown(VirtualKeyCode.DELETE), "D");
                        ActionWithLog(() => sim.Keyboard.Sleep(1000), "E");
                        ActionWithLog(() => sim.Keyboard.KeyUp(VirtualKeyCode.DELETE), "F");
                        ActionWithLog(() => sim.Keyboard.TextEntry(text), "G");
                        ActionWithLog(() => sim.Keyboard.Sleep(100), "H");

                        if (FuncWithLog(() => elm.Text.Equals(text), $"I: \"{elm.Text}\".Equals(\"{text}\")"))
                        {
                            s_logger.Info($"input test=[{text}] is copied.");
                            return;
                        }
                    }
                }
                catch (WebDriverException e)
                {
                }

                count++;
                
                if (count >= 10)
                {
                    throw new Exception($"Failed to input text=[{text}].");
                }

                Thread.Sleep(100);

                s_logger.Info($"continue to next loop...");
            }
        }

        /// <summary>
        /// AutomationIDで要素を検索し、取得します。
        /// </summary>
        /// <param name="automationId">AutomationID</param>
        /// <param name="timeOutSeconds">タイムアウト秒数</param>
        /// <returns>取得したWindowsElementオブジェクト</returns>
        public WindowsElement GetElementByAutomationID(string automationId, int timeOutSeconds = 10)
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
                    try
                    {
                        element = Driver.FindElementByAccessibilityId(automationId);
                        return element != null;
                    }
                    catch (WebDriverException)
                    {
                        FocusFrontWindow();
                        return false;
                    }
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
        public WindowsElement GetElementByName(string name, int timeOutSeconds = 10)
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
                    try
                    {
                        element = Driver.FindElementByName(name);
                        return element != null;
                    }
                    catch (WebDriverException)
                    {
                        FocusFrontWindow();
                        return false;
                    }
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
        public WindowsElement GetElementBy(By by, int timeOutSeconds = 10)
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
                    try
                    {
                        element = Driver.FindElement(by);
                        return element != null;
                    }
                    catch (WebDriverException)
                    {
                        FocusFrontWindow();
                        return false;
                    }
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
        /// <param name="timeOutMilliseconds">タイムアウトミリ秒数</param>
        /// <returns>存在する場合は true、存在しない場合は false を返します。</returns>
        public bool ExistsElementByAutomationID(string automationId, int timeOutMilliseconds = 10000)
        {
            WindowsElement element = null;

            var wait = new DefaultWait<WindowsDriver<WindowsElement>>(Session)
            {
                Timeout = TimeSpan.FromMilliseconds(timeOutMilliseconds),
                Message = $"Element with automationId \"{automationId}\" not found."
            };

            wait.IgnoreExceptionTypes(typeof(WebDriverException));

            try
            {
                wait.Until(Driver =>
                {
                    try
                    {
                        element = Driver.FindElementByAccessibilityId(automationId);
                        return element != null;
                    }
                    catch (WebDriverException)
                    {
                        FocusFrontWindow();
                        return false;
                    }
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
        public bool IsElementPresent(By by)
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
                try
                {
                    FocusFrontWindow();
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
            try
            {
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
            }
            catch (WebDriverException e)
            {
                s_logger.Warn(e);
                //このメソッドを実行するときは大抵Sessionが壊れているときが多いので、終了処理も失敗するときが多い
                //例外が出たら握りつぶす
            }
            finally
            {
                s_logger.Trace($"Being Session.Quit().");
                Session.Quit();
                s_logger.Trace($"End Session.Quit().");
                Session = null;
            }
            s_logger.Debug($"End QuitTargetApp().");
        }

        [Conditional("LOCALPC")]
        protected void RebootWinAppDriver()
        {
            s_logger.Debug($"Being RebootWinAppDriver().");
            s_logger.Trace($"WinAppDriverProcess is not null: {WinAppDriverProcess is not null}");
            if (WinAppDriverProcess is not null)
            {
                s_logger.Trace($"!WinAppDriverProcess.HasExited: {!WinAppDriverProcess.HasExited}");
                if (!WinAppDriverProcess.HasExited)
                {
                    s_logger.Trace($"Being WinAppDriverProcess.Kill().");
                    WinAppDriverProcess.Kill();
                    s_logger.Trace($"End WinAppDriverProcess.Kill().");
                }
                WinAppDriverProcess = null;
            }
            s_logger.Trace($"Being Process.Start().");
            WinAppDriverProcess = Process.Start(new ProcessStartInfo(@"C:\Program Files\Windows Application Driver\WinAppDriver.exe"));
            s_logger.Trace($"End Process.Start().");
            s_logger.Debug($"End RebootWinAppDriver().");
        }

        protected void ActionWithLog(Action action, string logmessage)
        {
            try
            {
                action();
            }
            catch (WebDriverException)
            {

            }
            finally
            {
                s_logger.Info(logmessage);
            }
        }

        protected bool FuncWithLog(Func<bool> func, string logmessage)
        {
            try
            {
                return func();
            }
            catch (WebDriverException)
            {
                return false;
            }
            finally
            {
                s_logger.Info(logmessage);
            }
        }
    }
}
