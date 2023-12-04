using OpenQA.Selenium.Appium.Windows;
using System.Diagnostics;
using System.IO;
using boilersE2E.Core;
using Xunit;

namespace boilersE2E.xUnit
{
    public abstract class E2ETestClass : IDisposable
    {
        public WindowsDriver<WindowsElement> Session => E2ETestFixtureBase.Session;

        /// <summary>
        /// コンストラクター
        /// boilersE2ETestEnvironmentVariableNameで指定した環境変数の値が"1"または"true"の時、WinAppDriver.exeのプロセスを起動します。
        /// </summary>
        public E2ETestClass()
        {
            var environmentVariable = Environment.GetEnvironmentVariable(E2ETestFixtureBase.EnvironmentVariableNameWhereWinAppDriverRunAutomatically);
            if (environmentVariable == "true" || environmentVariable == 1.ToString())
            {
                Assert.True(File.Exists(E2ETestFixtureBase.WinAppDriverPath), "WinAppDriver doesn't installed");
                E2ETestFixtureBase.WinAppDriverProcess =
                    Process.Start(
                        new ProcessStartInfo(E2ETestFixtureBase.WinAppDriverPath));
            }
        }

        /// <summary>
        /// Disposeメソッド
        /// boilersE2ETestEnvironmentVariableNameで指定した環境変数の値が"1"または"true"の時、WinAppDriver.exeのプロセスをキルします。
        /// </summary>
        public void Dispose()
        {
            var environmentVariable = Environment.GetEnvironmentVariable(E2ETestFixtureBase.EnvironmentVariableNameWhereWinAppDriverRunAutomatically);
            if (environmentVariable == "true" || environmentVariable == 1.ToString())
            {
                E2ETestFixtureBase.WinAppDriverProcess.Kill();
            }
        }
    }
}
