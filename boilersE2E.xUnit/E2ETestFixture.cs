using boilersE2E.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System.IO;
using Xunit;

namespace boilersE2E.xUnit
{
    [Collection("Parallel execution not possible")]
    public abstract class E2ETestFixture : E2ETestFixtureBase, IDisposable
    {
        /// <summary>
        /// コンストラクタ内でWindowsDriverオブジェクトを生成し、テストセッションを開始した後に任意の処理を実行します。
        /// </summary>
        public virtual void DoAfterBoot()
        {
        }

        /// <summary>
        /// コンストラクタで、ウィンドウサイズを調整した後に任意の処理を実行します。
        /// </summary>
        public virtual void DoAfterSettingWindowSize()
        {
        }

        /// <summary>
        /// コンストラクタ
        /// WindowsDriverオブジェクトを生成し、テストセッションを開始します。
        /// また、boilersE2ETestEnvironmentVariableNameで指定した環境変数の値により、ウィンドウのサイズを変更します。
        /// </summary>
        public E2ETestFixture()
        {
            if (Session == null)
            {
                CreateSession();
                Assert.NotNull(Session);

                DoAfterBoot();

                var environmentVariable = Environment.GetEnvironmentVariable(EnvironmentVariableNameWhereSetWindowSizeManually);
                if (environmentVariable == "true" || environmentVariable == 1.ToString())
                {
                    Session.Manage().Window.Size = new Size(WindowSize.Width, WindowSize.Height);
                }

                environmentVariable = Environment.GetEnvironmentVariable(EnvironmentVariableNameWhereMaximizeWindow);
                if (environmentVariable == "true" || environmentVariable == 1.ToString())
                {
                    MaximizeWindow();
                }

                DoAfterSettingWindowSize();
            }
        }

        /// <summary>
        /// Disposeメソッド
        /// 実行中のすべてのウィンドウに対し、Alt+F4を送信してウィンドウを閉じます。
        /// その後、WindowsDriverオブジェクトのQuitメソッドを呼び出して、テストセッションを終了します。
        /// </summary>
        public void Dispose()
        {
            if (Session != null)
            {
                QuitTargetApp();
            }
        }
        
        public override void CreateSession()
        {
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", AppPath);
            options.AddAdditionalCapability("appWorkingDir", Path.GetDirectoryName(AppPath));

            var environmentVariable = Environment.GetEnvironmentVariable(EnvironmentVariableNameWhereWinAppDriverRunAutomatically);
            if (environmentVariable == "true" || environmentVariable == 1.ToString())
            {
                Assert.True(File.Exists(WinAppDriverPath), "WinAppDriver doesn't installed");
            }

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
            Assert.NotNull(Session);
        }
    }
}
