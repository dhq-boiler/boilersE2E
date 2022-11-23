using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace boilersE2E.NUnit
{
    public abstract class E2ETestFixture
    {
        private const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        private static readonly Logger s_logger = LogManager.GetCurrentClassLogger();
        private static Process wad;

        private static TestContext s_testContext;

        protected static WindowsDriver<WindowsElement> Session { get; private set; }

        /// <summary>
        /// ClassInitialize, ClassCleanup時に取得する環境変数の変数名を指定します。
        /// これは必須です。
        /// </summary>
        public static string boilersE2ETestEnvironmentVariableName { get; set; }

        /// <summary>
        /// テストするアプリケーションのパスを指定します。
        /// </summary>
        public abstract string AppPath { get; }

        /// <summary>
        /// テストするアプリケーションのウィンドウサイズを指定します。
        /// </summary>
        public abstract Size WindowSize { get; }

        /// <summary>
        /// TestInitializeメソッド内でWindowsDriverオブジェクトを生成し、テストセッションを開始した後に任意の処理を実行します。
        /// </summary>
        public virtual void DoAfterBoot()
        {
        }

        /// <summary>
        /// ClassInitializeメソッド
        /// boilersE2ETestEnvironmentVariableNameで指定した環境変数の値が"1"または"true"の時、WinAppDriver.exeのプロセスを起動します。
        /// </summary>
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            E2ETestFixture.s_testContext = testContext;
            var environmentVariable = Environment.GetEnvironmentVariable(boilersE2ETestEnvironmentVariableName);
            if (environmentVariable == "true" || environmentVariable == 1.ToString())
            {
                wad = Process.Start(new ProcessStartInfo(@"C:\Program Files\Windows Application Driver\WinAppDriver.exe"));
            }
        }

        /// <summary>
        /// ClassCleanupメソッド
        /// boilersE2ETestEnvironmentVariableNameで指定した環境変数の値が"1"または"true"の時、WinAppDriver.exeのプロセスをキルします。
        /// </summary>
        [ClassCleanup]
        public static void ClassCleanup()
        {
            var environmentVariable = Environment.GetEnvironmentVariable(boilersE2ETestEnvironmentVariableName);
            if (environmentVariable == "true" || environmentVariable == 1.ToString())
            {
                wad.Kill();
            }
        }

        /// <summary>
        /// TestInitializeメソッド
        /// WindowsDriverオブジェクトを生成し、テストセッションを開始します。
        /// また、boilersE2ETestEnvironmentVariableNameで指定した環境変数の値により、ウィンドウのサイズを変更します。
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            if (Session == null)
            {
                var options = new AppiumOptions();
                options.AddAdditionalCapability("app", AppPath);
                options.AddAdditionalCapability("appWorkingDir", Path.GetDirectoryName(AppPath));
                Session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), options);
                Assert.IsNotNull(Session);

                DoAfterBoot();

                var environmentVariable = Environment.GetEnvironmentVariable(boilersE2ETestEnvironmentVariableName);
                if (environmentVariable == "true" || environmentVariable == 1.ToString())
                {
                    Session.Manage().Window.Size = new Size(WindowSize.Width, WindowSize.Height);
                }
                else
                {
                    Session.SwitchTo().Window(Session.WindowHandles.First()).Manage().Window.Maximize();
                }
            }
        }

        /// <summary>
        /// TestCleanupメソッド
        /// 実行中のすべてのウィンドウに対し、Alt+F4を送信してウィンドウを閉じます。
        /// その後、WindowsDriverオブジェクトのQuitメソッドを呼び出して、テストセッションを終了します。
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            if (Session != null)
            {
                while (Session.WindowHandles.Count() > 0)
                {
                    //Alt+F4によるアプリ終了
                    var actions = new Actions(Session);
                    actions.SendKeys(OpenQA.Selenium.Keys.Alt + OpenQA.Selenium.Keys.F4 + OpenQA.Selenium.Keys.Alt);
                    actions.Perform();
                }
                Session.WindowHandles.Select(x => Session.SwitchTo().Window(x)).ToList().ForEach(x => x.Dispose());
                Session.Quit();
                Session = null;
            }
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
                Assert.Fail("Failed to WaitForObject.. Check screenshots");
                return waitElement;
            }
        }

        /// <summary>
        /// 要素に文字列を入力します。
        /// </summary>
        /// <param name="element">文字列を入力する対象の WindowsElement オブジェクト</param>
        /// <param name="text">入力する文字列</param>
        public static void InputText(WindowsElement element, string text)
        {
            Util.SetTextToClipboard(text);
            element.SendKeys(OpenQA.Selenium.Keys.Control + "v" + OpenQA.Selenium.Keys.Control);
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
                Assert.Fail(ex.Message);
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
                Assert.Fail(ex.Message);
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
                Assert.Fail(ex.Message);
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

        /// <summary>
        /// スクリーンショットを撮影します。
        /// スクリーンショットの保存先ディレクトリは $(TargetDir) になります。
        /// </summary>
        /// <param name="filename">撮影したスクリーンショットの保存ファイル名</param>
        public static void TakeScreenShot(string filename)
        {
            Session.GetScreenshot().SaveAsFile($"{AppDomain.CurrentDomain.BaseDirectory}\\{filename}");
            s_testContext.AddResultFile($"{AppDomain.CurrentDomain.BaseDirectory}\\{filename}");
        }
    }
}
