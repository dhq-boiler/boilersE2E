using boilersE2E.MsTest;
using System.Drawing;
using System.Reflection;

namespace boilersE2E.MsTest.Test
{
    [TestClass]
    public class E2ETestForWPF : E2ETestFixture
    {
        public override string AppPath => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "WPFApp.exe");

        public override Size WindowSize => new Size(571, 517);
        static E2ETestForWPF()
        {
            EnvironmentVariableNameWhereWinAppDriverRunAutomatically = "BOILERS_E2ETEST_IS_VALID";
            EnvironmentVariableNameWhereSetWindowSizeManually = "BOILERS_E2ETEST_SET_WINDOWSIZE_MANUAL";
        }

        [TestMethod]
        public void Calc_123Plus456()
        {
            Session.FindElementByAccessibilityId("one").Click();
            Session.FindElementByAccessibilityId("two").Click();
            Session.FindElementByAccessibilityId("three").Click();
            Session.FindElementByAccessibilityId("plus").Click();
            Session.FindElementByAccessibilityId("four").Click();
            Session.FindElementByAccessibilityId("five").Click();
            Session.FindElementByAccessibilityId("six").Click();
            Session.FindElementByAccessibilityId("equal").Click();

            Assert.AreEqual("579", Session.FindElementByAccessibilityId("display").Text);
        }

        [TestMethod]
        public void Calc_123Minus456()
        {
            Session.FindElementByAccessibilityId("one").Click();
            Session.FindElementByAccessibilityId("two").Click();
            Session.FindElementByAccessibilityId("three").Click();
            Session.FindElementByAccessibilityId("minus").Click();
            Session.FindElementByAccessibilityId("four").Click();
            Session.FindElementByAccessibilityId("five").Click();
            Session.FindElementByAccessibilityId("six").Click();
            Session.FindElementByAccessibilityId("equal").Click();

            Assert.AreEqual("-333", Session.FindElementByAccessibilityId("display").Text);
        }

        [TestMethod]
        public void Calc_369Multiple3()
        {
            Session.FindElementByAccessibilityId("three").Click();
            Session.FindElementByAccessibilityId("six").Click();
            Session.FindElementByAccessibilityId("nine").Click();
            Session.FindElementByAccessibilityId("multiple").Click();
            Session.FindElementByAccessibilityId("three").Click();
            Session.FindElementByAccessibilityId("equal").Click();

            Assert.AreEqual("1107", Session.FindElementByAccessibilityId("display").Text);
        }

        [TestMethod]
        public void Calc_369Divide3()
        {
            Session.FindElementByAccessibilityId("three").Click();
            Session.FindElementByAccessibilityId("six").Click();
            Session.FindElementByAccessibilityId("nine").Click();
            Session.FindElementByAccessibilityId("divide").Click();
            Session.FindElementByAccessibilityId("three").Click();
            Session.FindElementByAccessibilityId("equal").Click();

            Assert.AreEqual("123", Session.FindElementByAccessibilityId("display").Text);
        }

        [TestMethod]
        public void Calc_33_3Divide3()
        {
            Session.FindElementByAccessibilityId("three").Click();
            Session.FindElementByAccessibilityId("three").Click();
            Session.FindElementByAccessibilityId("dot").Click();
            Session.FindElementByAccessibilityId("three").Click();
            Session.FindElementByAccessibilityId("divide").Click();
            Session.FindElementByAccessibilityId("three").Click();
            Session.FindElementByAccessibilityId("equal").Click();

            Assert.AreEqual("11.1", Session.FindElementByAccessibilityId("display").Text);
        }

        [TestMethod]
        public void C()
        {
            Assert.AreEqual("0", Session.FindElementByAccessibilityId("display").Text);

            Session.FindElementByAccessibilityId("Clear").Click();

            Assert.AreEqual("0", Session.FindElementByAccessibilityId("display").Text);

            Session.FindElementByAccessibilityId("one").Click();
            Session.FindElementByAccessibilityId("two").Click();
            Session.FindElementByAccessibilityId("three").Click();
            Session.FindElementByAccessibilityId("four").Click();
            Session.FindElementByAccessibilityId("five").Click();
            Session.FindElementByAccessibilityId("six").Click();
            Session.FindElementByAccessibilityId("seven").Click();
            Session.FindElementByAccessibilityId("eight").Click();
            Session.FindElementByAccessibilityId("nine").Click();
            Session.FindElementByAccessibilityId("zero").Click();

            Assert.AreEqual("1234567890", Session.FindElementByAccessibilityId("display").Text);

            Session.FindElementByAccessibilityId("Clear").Click();

            Assert.AreEqual("0", Session.FindElementByAccessibilityId("display").Text);
        }

        [TestMethod]
        public void CE()
        {
            Assert.AreEqual(string.Empty, Session.FindElementByAccessibilityId("fomula").Text);
            Assert.AreEqual("0", Session.FindElementByAccessibilityId("display").Text);

            Session.FindElementByAccessibilityId("ClearEntry").Click();

            Assert.AreEqual("0", Session.FindElementByAccessibilityId("display").Text);

            Session.FindElementByAccessibilityId("one").Click();
            Session.FindElementByAccessibilityId("two").Click();
            Session.FindElementByAccessibilityId("three").Click();
            Session.FindElementByAccessibilityId("four").Click();
            Session.FindElementByAccessibilityId("five").Click();
            Session.FindElementByAccessibilityId("six").Click();
            Session.FindElementByAccessibilityId("seven").Click();
            Session.FindElementByAccessibilityId("eight").Click();
            Session.FindElementByAccessibilityId("nine").Click();
            Session.FindElementByAccessibilityId("zero").Click();

            Session.FindElementByAccessibilityId("plus").Click();

            Session.FindElementByAccessibilityId("one").Click();
            Session.FindElementByAccessibilityId("two").Click();
            Session.FindElementByAccessibilityId("three").Click();
            Session.FindElementByAccessibilityId("four").Click();
            Session.FindElementByAccessibilityId("five").Click();
            Session.FindElementByAccessibilityId("six").Click();
            Session.FindElementByAccessibilityId("seven").Click();
            Session.FindElementByAccessibilityId("eight").Click();
            Session.FindElementByAccessibilityId("nine").Click();
            Session.FindElementByAccessibilityId("zero").Click();

            Assert.AreEqual("1234567890+1234567890", Session.FindElementByAccessibilityId("fomula").Text);
            Assert.AreEqual("1234567890", Session.FindElementByAccessibilityId("display").Text);

            Session.FindElementByAccessibilityId("ClearEntry").Click();

            Assert.AreEqual(string.Empty, Session.FindElementByAccessibilityId("fomula").Text);
            Assert.AreEqual("0", Session.FindElementByAccessibilityId("display").Text);
        }
    }
}