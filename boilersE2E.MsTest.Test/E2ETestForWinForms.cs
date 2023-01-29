using boilersE2E.MsTest;
using MSTest.TestFramework.Extensions.AttributeEx;
using System.Drawing;
using System.Reflection;

namespace boilersE2E.MsTest.Test
{
    [TestClass]
    public class E2ETestForWinForms : E2ETestFixture
    {
        public override string AppPath => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "WindowsFormsApp.exe");

        public override Size WindowSize => new Size(571, 517);
        static E2ETestForWinForms()
        {
            EnvironmentVariableNameWhereWinAppDriverRunAutomatically = "BOILERS_E2ETEST_IS_VALID";
            EnvironmentVariableNameWhereSetWindowSizeManually = "BOILERS_E2ETEST_SET_WINDOWSIZE_MANUAL";
        }

        [TestMethod]
        public void Calc_123Plus456()
        {
            GetElementByAutomationID("one").Click();
            GetElementByAutomationID("two").Click();
            GetElementByAutomationID("three").Click();
            GetElementByAutomationID("plus").Click();
            GetElementByAutomationID("four").Click();
            GetElementByAutomationID("five").Click();
            GetElementByAutomationID("six").Click();
            GetElementByAutomationID("equal").Click();

            Assert.AreEqual("579", GetElementByAutomationID("display").Text);
        }

        [TestMethod]
        public void Calc_123Minus456()
        {
            GetElementByAutomationID("one").Click();
            GetElementByAutomationID("two").Click();
            GetElementByAutomationID("three").Click();
            GetElementByAutomationID("minus").Click();
            GetElementByAutomationID("four").Click();
            GetElementByAutomationID("five").Click();
            GetElementByAutomationID("six").Click();
            GetElementByAutomationID("equal").Click();

            Assert.AreEqual("-333", GetElementByAutomationID("display").Text);
        }

        [TestMethod]
        public void Calc_369Multiple3()
        {
            GetElementByAutomationID("three").Click();
            GetElementByAutomationID("six").Click();
            GetElementByAutomationID("nine").Click();
            GetElementByAutomationID("multiple").Click();
            GetElementByAutomationID("three").Click();
            GetElementByAutomationID("equal").Click();

            Assert.AreEqual("1107", GetElementByAutomationID("display").Text);
        }

        [TestMethod]
        public void Calc_369Divide3()
        {
            GetElementByAutomationID("three").Click();
            GetElementByAutomationID("six").Click();
            GetElementByAutomationID("nine").Click();
            GetElementByAutomationID("divide").Click();
            GetElementByAutomationID("three").Click();
            GetElementByAutomationID("equal").Click();

            Assert.AreEqual("123", GetElementByAutomationID("display").Text);
        }

        [TestMethod]
        public void Calc_33_3Divide3()
        {
            GetElementByAutomationID("three").Click();
            GetElementByAutomationID("three").Click();
            GetElementByAutomationID("dot").Click();
            GetElementByAutomationID("three").Click();
            GetElementByAutomationID("divide").Click();
            GetElementByAutomationID("three").Click();
            GetElementByAutomationID("equal").Click();

            Assert.AreEqual("11.1", GetElementByAutomationID("display").Text);
        }

        [TestMethod]
        public void C()
        {
            Assert.AreEqual("0", GetElementByAutomationID("display").Text);

            GetElementByAutomationID("Clear").Click();

            Assert.AreEqual("0", GetElementByAutomationID("display").Text);

            GetElementByAutomationID("one").Click();
            GetElementByAutomationID("two").Click();
            GetElementByAutomationID("three").Click();
            GetElementByAutomationID("four").Click();
            GetElementByAutomationID("five").Click();
            GetElementByAutomationID("six").Click();
            GetElementByAutomationID("seven").Click();
            GetElementByAutomationID("eight").Click();
            GetElementByAutomationID("nine").Click();
            GetElementByAutomationID("zero").Click();

            Assert.AreEqual("1234567890", GetElementByAutomationID("display").Text);

            GetElementByAutomationID("Clear").Click();

            Assert.AreEqual("0", GetElementByAutomationID("display").Text);
        }

        [TestMethod]
        public void CE()
        {
            Assert.AreEqual(string.Empty, GetElementByAutomationID("fomula").Text);
            Assert.AreEqual("0", GetElementByAutomationID("display").Text);

            GetElementByAutomationID("ClearEntry").Click();

            Assert.AreEqual("0", GetElementByAutomationID("display").Text);

            GetElementByAutomationID("one").Click();
            GetElementByAutomationID("two").Click();
            GetElementByAutomationID("three").Click();
            GetElementByAutomationID("four").Click();
            GetElementByAutomationID("five").Click();
            GetElementByAutomationID("six").Click();
            GetElementByAutomationID("seven").Click();
            GetElementByAutomationID("eight").Click();
            GetElementByAutomationID("nine").Click();
            GetElementByAutomationID("zero").Click();

            GetElementByAutomationID("plus").Click();

            GetElementByAutomationID("one").Click();
            GetElementByAutomationID("two").Click();
            GetElementByAutomationID("three").Click();
            GetElementByAutomationID("four").Click();
            GetElementByAutomationID("five").Click();
            GetElementByAutomationID("six").Click();
            GetElementByAutomationID("seven").Click();
            GetElementByAutomationID("eight").Click();
            GetElementByAutomationID("nine").Click();
            GetElementByAutomationID("zero").Click();

            Assert.AreEqual("1234567890+1234567890", GetElementByAutomationID("fomula").Text);
            Assert.AreEqual("1234567890", GetElementByAutomationID("display").Text);

            GetElementByAutomationID("ClearEntry").Click();

            Assert.AreEqual(string.Empty, GetElementByAutomationID("fomula").Text);
            Assert.AreEqual("0", GetElementByAutomationID("display").Text);
        }

        [STATestMethod, Retry(10)]
        public void Paste()
        {
            Assert.AreEqual("0", GetElementByAutomationID("display").Text);

            GetElementByAutomationID("display").ClearRepeatedlyUntilTimeout();
            InputText(GetElementByAutomationID("display"), "123456789");
            GetElementByAutomationID("plus").Click();
            GetElementByAutomationID("display").ClearRepeatedlyUntilTimeout();
            InputText(GetElementByAutomationID("display"), "987654321");
            GetElementByAutomationID("equal").Click();

            Assert.AreEqual("123456789+987654321", GetElementByAutomationID("fomula").Text);
            Assert.AreEqual("1111111110", GetElementByAutomationID("display").Text);
        }
    }
}