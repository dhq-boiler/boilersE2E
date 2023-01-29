using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace boilersE2E.xUnit
{
    public abstract class E2ETestClass : IDisposable
    {
        public WindowsDriver<WindowsElement> Session => E2ETestFixture.Session;

        /// <summary>
        /// コンストラクター
        /// boilersE2ETestEnvironmentVariableNameで指定した環境変数の値が"1"または"true"の時、WinAppDriver.exeのプロセスを起動します。
        /// </summary>
        public E2ETestClass()
        {
            var environmentVariable = Environment.GetEnvironmentVariable(E2ETestFixture.EnvironmentVariableNameWhereWinAppDriverRunAutomatically);
            if (environmentVariable == "true" || environmentVariable == 1.ToString())
            {
                E2ETestFixture.WinAppDriverProcess =
                    Process.Start(
                        new ProcessStartInfo(@"C:\Program Files\Windows Application Driver\WinAppDriver.exe"));
            }
        }

        /// <summary>
        /// Disposeメソッド
        /// boilersE2ETestEnvironmentVariableNameで指定した環境変数の値が"1"または"true"の時、WinAppDriver.exeのプロセスをキルします。
        /// </summary>
        public void Dispose()
        {
            var environmentVariable = Environment.GetEnvironmentVariable(E2ETestFixture.EnvironmentVariableNameWhereWinAppDriverRunAutomatically);
            if (environmentVariable == "true" || environmentVariable == 1.ToString())
            {
                E2ETestFixture.WinAppDriverProcess.Kill();
            }
        }
    }
}
