using System.Drawing;
using System.Reflection;

namespace boilersE2E.xUnit.Test
{
    public class ConcreteE2ETestFixtureForWPF : E2ETestFixture, IClassFixture<ConcreteE2ETestClass>
    {
        public override string AppPath => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "WPFApp.exe");

        public override Size WindowSize => new Size(571, 517);

        public ConcreteE2ETestFixtureForWPF() : base() { }

        [Fact]
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

            Assert.Equal("579", GetElementByAutomationID("display").Text);
        }

        [Fact]
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

            Assert.Equal("-333", GetElementByAutomationID("display").Text);
        }

        [Fact]
        public void Calc_369Multiple3()
        {
            GetElementByAutomationID("three").Click();
            GetElementByAutomationID("six").Click();
            GetElementByAutomationID("nine").Click();
            GetElementByAutomationID("multiple").Click();
            GetElementByAutomationID("three").Click();
            GetElementByAutomationID("equal").Click();

            Assert.Equal("1107", GetElementByAutomationID("display").Text);
        }

        [Fact]
        public void Calc_369Divide3()
        {
            GetElementByAutomationID("three").Click();
            GetElementByAutomationID("six").Click();
            GetElementByAutomationID("nine").Click();
            GetElementByAutomationID("divide").Click();
            GetElementByAutomationID("three").Click();
            GetElementByAutomationID("equal").Click();

            Assert.Equal("123", GetElementByAutomationID("display").Text);
        }

        [Fact]
        public void Calc_33_3Divide3()
        {
            GetElementByAutomationID("three").Click();
            GetElementByAutomationID("three").Click();
            GetElementByAutomationID("dot").Click();
            GetElementByAutomationID("three").Click();
            GetElementByAutomationID("divide").Click();
            GetElementByAutomationID("three").Click();
            GetElementByAutomationID("equal").Click();

            Assert.Equal("11.1", GetElementByAutomationID("display").Text);
        }

        [Fact]
        public void C()
        {
            Assert.Equal("0", GetElementByAutomationID("display").Text);

            GetElementByAutomationID("Clear").Click();

            Assert.Equal("0", GetElementByAutomationID("display").Text);

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

            Assert.Equal("1234567890", GetElementByAutomationID("display").Text);

            GetElementByAutomationID("Clear").Click();

            Assert.Equal("0", GetElementByAutomationID("display").Text);
        }

        [Fact]
        public void CE()
        {
            Assert.Equal(string.Empty, GetElementByAutomationID("fomula").Text);
            Assert.Equal("0", GetElementByAutomationID("display").Text);

            GetElementByAutomationID("ClearEntry").Click();

            Assert.Equal("0", GetElementByAutomationID("display").Text);

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

            Assert.Equal("1234567890+1234567890", GetElementByAutomationID("fomula").Text);
            Assert.Equal("1234567890", GetElementByAutomationID("display").Text);

            GetElementByAutomationID("ClearEntry").Click();

            Assert.Equal(string.Empty, GetElementByAutomationID("fomula").Text);
            Assert.Equal("0", GetElementByAutomationID("display").Text);
        }
    }
}
