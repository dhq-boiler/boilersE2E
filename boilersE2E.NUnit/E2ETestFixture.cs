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
                WinAppDriverProcess = Process.Start(new ProcessStartInfo(@"C:\Program Files\Windows Application Driver\WinAppDriver.exe"));
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
            if (Session == null)
            {
                CreateSession();
                Assert.That(Session, Is.Not.Null);

                DoAfterBoot();

                var environmentVariable = Environment.GetEnvironmentVariable(EnvironmentVariableNameWhereSetWindowSizeManually);
                if (environmentVariable == "true" || environmentVariable == 1.ToString())
                {
                    Session.Manage().Window.Size = new Size(WindowSize.Width, WindowSize.Height);
                }
                else
                {
                    MaximizeWindow();
                }

                DoAfterSettingWindowSize();
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
                    //スクリーンショットを撮影する
                    TakeScreenShot($"{TestContext.CurrentContext.Test.FullName}.png");
                }

                QuitTargetApp();
            }
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
            Assert.That(Session, Is.Not.Null);
        }
    }
}
