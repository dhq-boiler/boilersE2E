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
            EnvironmentVariableNameWhereSetWindowSizeManually = "BOILERS_E2ETEST_SET_WINDOWSIZE_MANUAL";
        }

        [Test]
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

            Assert.That(GetElementByAutomationID("display").Text, Is.EqualTo("579"));
        }

        [Test]
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

            Assert.That(GetElementByAutomationID("display").Text, Is.EqualTo("-333"));
        }

        [Test]
        public void Calc_369Multiple3()
        {
            GetElementByAutomationID("three").Click();
            GetElementByAutomationID("six").Click();
            GetElementByAutomationID("nine").Click();
            GetElementByAutomationID("multiple").Click();
            GetElementByAutomationID("three").Click();
            GetElementByAutomationID("equal").Click();

            Assert.That(GetElementByAutomationID("display").Text, Is.EqualTo("1107"));
        }

        [Test]
        public void Calc_369Divide3()
        {
            GetElementByAutomationID("three").Click();
            GetElementByAutomationID("six").Click();
            GetElementByAutomationID("nine").Click();
            GetElementByAutomationID("divide").Click();
            GetElementByAutomationID("three").Click();
            GetElementByAutomationID("equal").Click();

            Assert.That(GetElementByAutomationID("display").Text, Is.EqualTo("123"));
        }

        [Test]
        public void Calc_33_3Divide3()
        {
            GetElementByAutomationID("three").Click();
            GetElementByAutomationID("three").Click();
            GetElementByAutomationID("dot").Click();
            GetElementByAutomationID("three").Click();
            GetElementByAutomationID("divide").Click();
            GetElementByAutomationID("three").Click();
            GetElementByAutomationID("equal").Click();

            Assert.That(GetElementByAutomationID("display").Text, Is.EqualTo("11.1"));
        }

        [Test]
        public void C()
        {
            Assert.That(GetElementByAutomationID("display").Text, Is.EqualTo("0"));

            GetElementByAutomationID("Clear").Click();

            Assert.That(GetElementByAutomationID("display").Text, Is.EqualTo("0"));

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

            Assert.That(GetElementByAutomationID("display").Text, Is.EqualTo("1234567890"));

            GetElementByAutomationID("Clear").Click();

            Assert.That(GetElementByAutomationID("display").Text, Is.EqualTo("0"));
        }

        [Test]
        public void CE()
        {
            Assert.That(GetElementByAutomationID("fomula").Text, Is.EqualTo(string.Empty));
            Assert.That(GetElementByAutomationID("display").Text, Is.EqualTo("0"));

            GetElementByAutomationID("ClearEntry").Click();

            Assert.That(GetElementByAutomationID("display").Text, Is.EqualTo("0"));

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

            Assert.That(GetElementByAutomationID("fomula").Text, Is.EqualTo("1234567890+1234567890"));
            Assert.That(GetElementByAutomationID("display").Text, Is.EqualTo("1234567890"));

            GetElementByAutomationID("ClearEntry").Click();

            Assert.That(GetElementByAutomationID("fomula").Text, Is.EqualTo(string.Empty));
            Assert.That(GetElementByAutomationID("display").Text, Is.EqualTo("0"));
        }

        [Test, Apartment(ApartmentState.STA), Retry(10)]
        public void Paste()
        {
            Assert.That(GetElementByAutomationID("display").Text, Is.EqualTo("0"));

            GetElementByAutomationID("display").ClearRepeatedlyUntilTimeout();
            InputText("123456789");
            GetElementByAutomationID("plus").Click();
            GetElementByAutomationID("display").ClearRepeatedlyUntilTimeout();
            InputText("987654321");
            GetElementByAutomationID("equal").Click();

            Assert.That(GetElementByAutomationID("fomula").Text, Is.EqualTo("123456789+987654321"));
            Assert.That(GetElementByAutomationID("display").Text, Is.EqualTo("1111111110"));
        }
    }
}