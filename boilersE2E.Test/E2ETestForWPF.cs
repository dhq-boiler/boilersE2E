using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace boilersE2E.Test
{
    public class E2ETestForWPF : E2ETestFixture
    {
        public override string AppPath => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "WPFApp.exe");

        public override Size WindowSize => new Size(571, 517);
        static E2ETestForWPF()
        {
            boilersE2ETestEnvironmentVariableName = "BOILERS_E2ETEST_IS_VALID";
        }

        [Test]
        public void 計算_123足す456()
        {
            Session.FindElementByAccessibilityId("one").Click();
            Session.FindElementByAccessibilityId("two").Click();
            Session.FindElementByAccessibilityId("three").Click();
            Session.FindElementByAccessibilityId("plus").Click();
            Session.FindElementByAccessibilityId("four").Click();
            Session.FindElementByAccessibilityId("five").Click();
            Session.FindElementByAccessibilityId("six").Click();
            Session.FindElementByAccessibilityId("equal").Click();

            Assert.That(Session.FindElementByAccessibilityId("display").Text, Is.EqualTo("579"));
        }

        [Test]
        public void 計算_123引く456()
        {
            Session.FindElementByAccessibilityId("one").Click();
            Session.FindElementByAccessibilityId("two").Click();
            Session.FindElementByAccessibilityId("three").Click();
            Session.FindElementByAccessibilityId("minus").Click();
            Session.FindElementByAccessibilityId("four").Click();
            Session.FindElementByAccessibilityId("five").Click();
            Session.FindElementByAccessibilityId("six").Click();
            Session.FindElementByAccessibilityId("equal").Click();

            Assert.That(Session.FindElementByAccessibilityId("display").Text, Is.EqualTo("-333"));
        }

        [Test]
        public void 計算_369掛ける3()
        {
            Session.FindElementByAccessibilityId("three").Click();
            Session.FindElementByAccessibilityId("six").Click();
            Session.FindElementByAccessibilityId("nine").Click();
            Session.FindElementByAccessibilityId("multiple").Click();
            Session.FindElementByAccessibilityId("three").Click();
            Session.FindElementByAccessibilityId("equal").Click();

            Assert.That(Session.FindElementByAccessibilityId("display").Text, Is.EqualTo("1107"));
        }

        [Test]
        public void 計算_369割る3()
        {
            Session.FindElementByAccessibilityId("three").Click();
            Session.FindElementByAccessibilityId("six").Click();
            Session.FindElementByAccessibilityId("nine").Click();
            Session.FindElementByAccessibilityId("divide").Click();
            Session.FindElementByAccessibilityId("three").Click();
            Session.FindElementByAccessibilityId("equal").Click();

            Assert.That(Session.FindElementByAccessibilityId("display").Text, Is.EqualTo("123"));
        }

        [Test]
        public void 計算_33_3割る3()
        {
            Session.FindElementByAccessibilityId("three").Click();
            Session.FindElementByAccessibilityId("three").Click();
            Session.FindElementByAccessibilityId("dot").Click();
            Session.FindElementByAccessibilityId("three").Click();
            Session.FindElementByAccessibilityId("divide").Click();
            Session.FindElementByAccessibilityId("three").Click();
            Session.FindElementByAccessibilityId("equal").Click();

            Assert.That(Session.FindElementByAccessibilityId("display").Text, Is.EqualTo("11.1"));
        }

        [Test]
        public void C()
        {
            Assert.That(Session.FindElementByAccessibilityId("display").Text, Is.EqualTo("0"));

            Session.FindElementByAccessibilityId("Clear").Click();

            Assert.That(Session.FindElementByAccessibilityId("display").Text, Is.EqualTo("0"));

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

            Assert.That(Session.FindElementByAccessibilityId("display").Text, Is.EqualTo("1234567890"));

            Session.FindElementByAccessibilityId("Clear").Click();

            Assert.That(Session.FindElementByAccessibilityId("display").Text, Is.EqualTo("0"));
        }

        [Test]
        public void CE()
        {
            Assert.That(Session.FindElementByAccessibilityId("fomula").Text, Is.EqualTo(string.Empty));
            Assert.That(Session.FindElementByAccessibilityId("display").Text, Is.EqualTo("0"));

            Session.FindElementByAccessibilityId("ClearEntry").Click();

            Assert.That(Session.FindElementByAccessibilityId("display").Text, Is.EqualTo("0"));

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

            Assert.That(Session.FindElementByAccessibilityId("fomula").Text, Is.EqualTo("1234567890+1234567890"));
            Assert.That(Session.FindElementByAccessibilityId("display").Text, Is.EqualTo("1234567890"));

            Session.FindElementByAccessibilityId("ClearEntry").Click();

            Assert.That(Session.FindElementByAccessibilityId("fomula").Text, Is.EqualTo(string.Empty));
            Assert.That(Session.FindElementByAccessibilityId("display").Text, Is.EqualTo("0"));
        }
    }
}
