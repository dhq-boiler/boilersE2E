using boilersE2E.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System.Diagnostics;
using System.IO;

namespace boilersE2E.MsTest
{
    [TestClass]
    public abstract class E2ETestFixture : E2ETestFixtureBase
    {
        private static readonly Logger s_logger = LogManager.GetCurrentClassLogger();
        private Stopwatch stopwatch = new Stopwatch();

        public TestContext TestContext { get; internal set; }

        /// <summary>
        /// TestInitializeメソッド内でWindowsDriverオブジェクトを生成し、テストセッションを開始した後に任意の処理を実行します。
        /// </summary>
        public virtual void DoAfterBoot()
        {
        }

        /// <summary>
        /// TestInitializeメソッド内で、ウィンドウサイズを調整した後に任意の処理を実行します。
        /// </summary>
        public virtual void DoAfterSettingWindowSize()
        {
        }

        /// <summary>
        /// ClassInitializeメソッド
        /// boilersE2ETestEnvironmentVariableNameで指定した環境変数の値が"1"または"true"の時、WinAppDriver.exeのプロセスを起動します。
        /// </summary>
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            var environmentVariable = Environment.GetEnvironmentVariable(EnvironmentVariableNameWhereWinAppDriverRunAutomatically);
            if (WinAppDriverProcess is null && (environmentVariable == "true" || environmentVariable == 1.ToString()))
            {
                WinAppDriverProcess = Process.Start(new ProcessStartInfo(@"C:\Program Files\Windows Application Driver\WinAppDriver.exe"));
            }
        }

        /// <summary>
        /// ClassCleanupメソッド
        /// boilersE2ETestEnvironmentVariableNameで指定した環境変数の値が"1"または"true"の時、WinAppDriver.exeのプロセスをキルします。
        /// </summary>
        [ClassCleanup]
        public static void ClassCleanup()
        {
            var environmentVariable = Environment.GetEnvironmentVariable(EnvironmentVariableNameWhereWinAppDriverRunAutomatically);
            if (environmentVariable == "true" || environmentVariable == 1.ToString())
            {
                WinAppDriverProcess.Kill();
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
            stopwatch.Start();
            s_logger.Info($"[{TestContext.TestName}]Begin Unit Test.");
            var environmentVariable = Environment.GetEnvironmentVariable(EnvironmentVariableNameWhereWinAppDriverRunAutomatically);
            if (WinAppDriverProcess is null && (environmentVariable == "true" || environmentVariable == 1.ToString()))
            {
                WinAppDriverProcess = Process.Start(new ProcessStartInfo(@"C:\Program Files\Windows Application Driver\WinAppDriver.exe"));
                s_logger.Debug($"[{TestContext.TestName}]Started WinAppDriver.exe process.");
            }

            if (Session == null)
            {
                s_logger.Debug($"[{TestContext.TestName}]Begin CreateSession().");
                CreateSession();
                s_logger.Debug($"[{TestContext.TestName}]End CreateSession().");
                Assert.IsNotNull(Session);

                s_logger.Debug($"[{TestContext.TestName}]Begin DoAfterBoot().");
                DoAfterBoot();
                s_logger.Debug($"[{TestContext.TestName}]End DoAfterBoot().");

                s_logger.Debug($"[{TestContext.TestName}]Begin to set window size.");
                environmentVariable = Environment.GetEnvironmentVariable(EnvironmentVariableNameWhereSetWindowSizeManually);
                if (environmentVariable == "true" || environmentVariable == 1.ToString())
                {
                    Session.Manage().Window.Size = new Size(WindowSize.Width, WindowSize.Height);
                }
                else
                {
                    MaximizeWindow();
                }
                s_logger.Debug($"[{TestContext.TestName}]End to set window size.");

                s_logger.Debug($"[{TestContext.TestName}]Begin DoAfterSettingWindowSize().");
                DoAfterSettingWindowSize();
                s_logger.Debug($"[{TestContext.TestName}]End DoAfterSettingWindowSize().");
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
                //テストが失敗した時
                if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
                {
                    s_logger.Debug($"[{TestContext.TestName}]Begin TakeScreenShot().");
                    //スクリーンショットを撮影する
                    TakeScreenShot($"{TestContext.FullyQualifiedTestClassName}.{TestContext.TestName}.png");
                    s_logger.Debug($"[{TestContext.TestName}]End TakeScreenShot().");
                }

                s_logger.Debug($"[{TestContext.TestName}]Begin QuitTargetApp().");
                QuitTargetApp();
                s_logger.Debug($"[{TestContext.TestName}]End QuitTargetApp().");
            }
            stopwatch.Stop();
            s_logger.Info($"[{TestContext.TestName}]End Unit Test. Elapsed:{TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds).ToStringEx("hhhmmmsss")}");
        }

        /// <summary>
        /// スクリーンショットを撮影します。
        /// スクリーンショットの保存先ディレクトリは $(TargetDir) になります。
        /// </summary>
        /// <param name="filename">撮影したスクリーンショットの保存ファイル名</param>
        public void TakeScreenShot(string filename)
        {
            try
            {
                Session.GetScreenshot().SaveAsFile($"{AppDomain.CurrentDomain.BaseDirectory}\\{filename}");
                TestContext.AddResultFile($"{AppDomain.CurrentDomain.BaseDirectory}\\{filename}");
            }
            catch (WebDriverException e)
            {
                s_logger.Warn("Failed to TakeScreenShot method.");
                s_logger.Warn(e);
            }
        }

        public override void CreateSession()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", AppPath);
            options.AddAdditionalCapability("appWorkingDir", Path.GetDirectoryName(AppPath));
            try
            {
                Session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), options);
            }
            catch (WebDriverException e)
            {
                QuitTargetApp();
                RebootWinAppDriver();
                CreateSession();
            }
            Assert.IsNotNull(Session);
        }
    }
}
