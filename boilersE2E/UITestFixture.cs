using NLog;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace boilersE2E
{
    public abstract class UITestFixture
    {
        private const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        private static readonly Logger s_logger = LogManager.GetCurrentClassLogger();
        private static Process wad;

        protected static WindowsDriver<WindowsElement> Session { get; private set; }

        public static string boilersE2ETestEnvironmentVariableName { get; set; }

        public abstract string AppPath { get; }

        public abstract Size WindowSize { get; }

        public virtual void DoAfterBoot()
        {
        }

        [OneTimeSetUp]
        public static void OneTimeSetUp()
        {
            var environmentVariable = Environment.GetEnvironmentVariable(boilersE2ETestEnvironmentVariableName);
            if (environmentVariable == "true" || environmentVariable == 1.ToString())
            {
                wad = Process.Start(new ProcessStartInfo(@"C:\Program Files\Windows Application Driver\WinAppDriver.exe"));
            }
        }

        [OneTimeTearDown]
        public static void OneTimeTearDown()
        {
            var environmentVariable = Environment.GetEnvironmentVariable(boilersE2ETestEnvironmentVariableName);
            if (environmentVariable == "true" || environmentVariable == 1.ToString())
            {
                wad.Kill();
            }
        }

        [SetUp]
        public void Setup()
        {
            if (Session == null)
            {
                var options = new AppiumOptions();
                options.AddAdditionalCapability("app", AppPath);
                options.AddAdditionalCapability("appWorkingDir", Path.GetDirectoryName(AppPath));
                Session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), options);
                Assert.That(Session, Is.Not.Null);

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

        [TearDown]
        public void TearDown()
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

        public static void InputText(WindowsElement element, string text)
        {
            Util.SetTextToClipboard(text);
            element.SendKeys(OpenQA.Selenium.Keys.Control + "v" + OpenQA.Selenium.Keys.Control);
        }

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

        public static void TakeScreenShot(string filename)
        {
            Session.GetScreenshot().SaveAsFile($"{AppDomain.CurrentDomain.BaseDirectory}\\{filename}");
            TestContext.AddTestAttachment($"{AppDomain.CurrentDomain.BaseDirectory}\\{filename}");
        }
    }
}
