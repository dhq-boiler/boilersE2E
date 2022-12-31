using System.Drawing;
using System.Reflection;

namespace boilersE2E.NUnit.Test
{
    public class E2ETestForWinForms : E2ETestFixture
    {
        public override string AppPath => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "WindowsFormsApp.exe");

        public override Size WindowSize => new Size(571, 517);
        static E2ETestForWinForms()
        {
            EnvironmentVariableNameWhereWinAppDriverRunAutomatically = "BOILERS_E2ETEST_IS_VALID";
        }

        [Test]
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

            Assert.That(Session.FindElementByAccessibilityId("display").Text, Is.EqualTo("579"));
        }

        [Test]
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

            Assert.That(Session.FindElementByAccessibilityId("display").Text, Is.EqualTo("-333"));
        }

        [Test]
        public void Calc_369Multiple3()
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
        public void Calc_369Divide3()
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
        public void Calc_33_3Divide3()
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

        [Test, Apartment(ApartmentState.STA), Retry(3)]
        public void Paste()
        {
            Assert.That(Session.FindElementByAccessibilityId("display").Text, Is.EqualTo("0"));

            InputText(Session.FindElementByAccessibilityId("display"), "123456789");
            Session.FindElementByAccessibilityId("plus").Click();
            Session.FindElementByAccessibilityId("display").ClearRepeatedlyUntilTimeout();
            InputText(Session.FindElementByAccessibilityId("display"), "987654321");
            Session.FindElementByAccessibilityId("equal").Click();

            Assert.That(Session.FindElementByAccessibilityId("fomula").Text, Is.EqualTo("123456789+987654321"));
            Assert.That(Session.FindElementByAccessibilityId("display").Text, Is.EqualTo("1111111110"));
        }
    }
}