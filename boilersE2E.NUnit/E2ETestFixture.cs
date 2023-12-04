using boilersE2E.Core;
using NLog;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System.Diagnostics;
using System.IO;

namespace boilersE2E.NUnit
{
    public abstract class E2ETestFixture : E2ETestFixtureBase
    {
        private static readonly Logger s_logger = LogManager.GetCurrentClassLogger();
        private Stopwatch stopwatch = new Stopwatch();

        /// <summary>
        /// SetUpメソッド内でWindowsDriverオブジェクトを生成し、テストセッションを開始した後に任意の処理を実行します。
        /// </summary>
        public virtual void DoAfterBoot()
        {
        }

        /// <summary>
        /// SetUpメソッド内で、ウィンドウサイズを調整した後に任意の処理を実行します。
        /// </summary>
        public virtual void DoAfterSettingWindowSize()
        {
        }

        /// <summary>
        /// OneTimeSetUpメソッド
        /// boilersE2ETestEnvironmentVariableNameで指定した環境変数の値が"1"または"true"の時、WinAppDriver.exeのプロセスを起動します。
        /// </summary>
        [OneTimeSetUp]
        public static void OneTimeSetUp()
        {
            var environmentVariable = Environment.GetEnvironmentVariable(EnvironmentVariableNameWhereWinAppDriverRunAutomatically);
            if (environmentVariable == "true" || environmentVariable == 1.ToString())
            {
                Assert.That(File.Exists(WinAppDriverPath), Is.True, "WinAppDriver doesn't installed");
                WinAppDriverProcess = Process.Start(new ProcessStartInfo(WinAppDriverPath));
            }
        }

        /// <summary>
        /// OneTimeTearDownメソッド
        /// boilersE2ETestEnvironmentVariableNameで指定した環境変数の値が"1"または"true"の時、WinAppDriver.exeのプロセスをキルします。
        /// </summary>
        [OneTimeTearDown]
        public static void OneTimeTearDown()
        {
            var environmentVariable = Environment.GetEnvironmentVariable(EnvironmentVariableNameWhereWinAppDriverRunAutomatically);
            if (environmentVariable == "true" || environmentVariable == 1.ToString())
            {
                WinAppDriverProcess.Kill();
            }
        }

        /// <summary>
        /// SetUpメソッド
        /// WindowsDriverオブジェクトを生成し、テストセッションを開始します。
        /// また、boilersE2ETestEnvironmentVariableNameで指定した環境変数の値により、ウィンドウのサイズを変更します。
        /// </summary>
        [SetUp]
        public void Setup()
        {
            stopwatch.Start();
            s_logger.Info($"[{TestContext.CurrentContext.Test.Name}]Begin Unit Test.");
            var environmentVariable = Environment.GetEnvironmentVariable(EnvironmentVariableNameWhereWinAppDriverRunAutomatically);
            if (WinAppDriverProcess is null && (environmentVariable == "true" || environmentVariable == 1.ToString()))
            {
                Assert.That(File.Exists(WinAppDriverPath), Is.True, "WinAppDriver doesn't installed");
                WinAppDriverProcess = Process.Start(new ProcessStartInfo(WinAppDriverPath));
                s_logger.Debug($"[{TestContext.CurrentContext.Test.Name}]Started WinAppDriver.exe process.");
            }

            if (Session == null)
            {
                s_logger.Debug($"[{TestContext.CurrentContext.Test.Name}]Begin CreateSession().");
                CreateSession();
                s_logger.Debug($"[{TestContext.CurrentContext.Test.Name}]End CreateSession().");
                Assert.That(Session, Is.Not.Null);

                s_logger.Debug($"[{TestContext.CurrentContext.Test.Name}]Begin DoAfterBoot().");
                DoAfterBoot();
                s_logger.Debug($"[{TestContext.CurrentContext.Test.Name}]End DoAfterBoot().");

                s_logger.Debug($"[{TestContext.CurrentContext.Test.Name}]Begin to set window size.");
                environmentVariable = Environment.GetEnvironmentVariable(EnvironmentVariableNameWhereSetWindowSizeManually);
                if (environmentVariable == "true" || environmentVariable == 1.ToString())
                {
                    Session.Manage().Window.Size = new Size(WindowSize.Width, WindowSize.Height);
                }

                environmentVariable = Environment.GetEnvironmentVariable(EnvironmentVariableNameWhereMaximizeWindow);
                if (environmentVariable == "true" || environmentVariable == 1.ToString())
                {
                    MaximizeWindow();
                }
                s_logger.Debug($"[{TestContext.CurrentContext.Test.Name}]End to set window size.");

                s_logger.Debug($"[{TestContext.CurrentContext.Test.Name}]Begin DoAfterSettingWindowSize().");
                DoAfterSettingWindowSize();
                s_logger.Debug($"[{TestContext.CurrentContext.Test.Name}]End DoAfterSettingWindowSize().");
            }
        }

        /// <summary>
        /// TearDownメソッド
        /// 実行中のすべてのウィンドウに対し、Alt+F4を送信してウィンドウを閉じます。
        /// その後、WindowsDriverオブジェクトのQuitメソッドを呼び出して、テストセッションを終了します。
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            if (Session != null)
            {
                //テストが失敗した時
                if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
                {
                    s_logger.Debug($"[{TestContext.CurrentContext.Test.Name}]Begin TakeScreenShot().");
                    //スクリーンショットを撮影する
                    TakeScreenShot($"{TestContext.CurrentContext.Test.FullName}.png");
                    s_logger.Debug($"[{TestContext.CurrentContext.Test.Name}]End TakeScreenShot().");
                }

                s_logger.Debug($"[{TestContext.CurrentContext.Test.Name}]Begin QuitTargetApp().");
                QuitTargetApp();
                s_logger.Debug($"[{TestContext.CurrentContext.Test.Name}]End QuitTargetApp().");
            }
            stopwatch.Stop();
            s_logger.Info($"[{TestContext.CurrentContext.Test.Name}]End Unit Test. Elapsed:{TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds).ToStringEx("hhhmmmsss")}");
        }
        
        /// <summary>
        /// スクリーンショットを撮影します。
        /// スクリーンショットの保存先ディレクトリは $(TargetDir) になります。
        /// </summary>
        /// <param name="filename">撮影したスクリーンショットの保存ファイル名</param>
        public static void TakeScreenShot(string filename)
        {
            try
            {
                Session.GetScreenshot().SaveAsFile($"{AppDomain.CurrentDomain.BaseDirectory}\\{filename}");
                TestContext.AddTestAttachment($"{AppDomain.CurrentDomain.BaseDirectory}\\{filename}");
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
            Assert.That(File.Exists(WinAppDriverPath), Is.True, "WinAppDriver doesn't installed");
            try
            {
                Session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), options);
            }
            catch (WebDriverException)
            {
                QuitTargetApp();
                RebootWinAppDriver();
                CreateSession();
            }
            Assert.That(Session, Is.Not.Null);
        }
    }
}
