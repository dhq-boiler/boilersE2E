using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
            Session.FindElementByAccessibilityId("one").Click();
            Session.FindElementByAccessibilityId("two").Click();
            Session.FindElementByAccessibilityId("three").Click();
            Session.FindElementByAccessibilityId("plus").Click();
            Session.FindElementByAccessibilityId("four").Click();
            Session.FindElementByAccessibilityId("five").Click();
            Session.FindElementByAccessibilityId("six").Click();
            Session.FindElementByAccessibilityId("equal").Click();

            Assert.Equal("579", Session.FindElementByAccessibilityId("display").Text);
        }

        [Fact]
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

            Assert.Equal("-333", Session.FindElementByAccessibilityId("display").Text);
        }

        [Fact]
        public void Calc_369Multiple3()
        {
            Session.FindElementByAccessibilityId("three").Click();
            Session.FindElementByAccessibilityId("six").Click();
            Session.FindElementByAccessibilityId("nine").Click();
            Session.FindElementByAccessibilityId("multiple").Click();
            Session.FindElementByAccessibilityId("three").Click();
            Session.FindElementByAccessibilityId("equal").Click();

            Assert.Equal("1107", Session.FindElementByAccessibilityId("display").Text);
        }

        [Fact]
        public void Calc_369Divide3()
        {
            Session.FindElementByAccessibilityId("three").Click();
            Session.FindElementByAccessibilityId("six").Click();
            Session.FindElementByAccessibilityId("nine").Click();
            Session.FindElementByAccessibilityId("divide").Click();
            Session.FindElementByAccessibilityId("three").Click();
            Session.FindElementByAccessibilityId("equal").Click();

            Assert.Equal("123", Session.FindElementByAccessibilityId("display").Text);
        }

        [Fact]
        public void Calc_33_3Divide3()
        {
            Session.FindElementByAccessibilityId("three").Click();
            Session.FindElementByAccessibilityId("three").Click();
            Session.FindElementByAccessibilityId("dot").Click();
            Session.FindElementByAccessibilityId("three").Click();
            Session.FindElementByAccessibilityId("divide").Click();
            Session.FindElementByAccessibilityId("three").Click();
            Session.FindElementByAccessibilityId("equal").Click();

            Assert.Equal("11.1", Session.FindElementByAccessibilityId("display").Text);
        }

        [Fact]
        public void C()
        {
            Assert.Equal("0", Session.FindElementByAccessibilityId("display").Text);

            Session.FindElementByAccessibilityId("Clear").Click();

            Assert.Equal("0", Session.FindElementByAccessibilityId("display").Text);

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

            Assert.Equal("1234567890", Session.FindElementByAccessibilityId("display").Text);

            Session.FindElementByAccessibilityId("Clear").Click();

            Assert.Equal("0", Session.FindElementByAccessibilityId("display").Text);
        }

        [Fact]
        public void CE()
        {
            Assert.Equal(string.Empty, Session.FindElementByAccessibilityId("fomula").Text);
            Assert.Equal("0", Session.FindElementByAccessibilityId("display").Text);

            Session.FindElementByAccessibilityId("ClearEntry").Click();

            Assert.Equal("0", Session.FindElementByAccessibilityId("display").Text);

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

            Assert.Equal("1234567890+1234567890", Session.FindElementByAccessibilityId("fomula").Text);
            Assert.Equal("1234567890", Session.FindElementByAccessibilityId("display").Text);

            Session.FindElementByAccessibilityId("ClearEntry").Click();

            Assert.Equal(string.Empty, Session.FindElementByAccessibilityId("fomula").Text);
            Assert.Equal("0", Session.FindElementByAccessibilityId("display").Text);
        }
    }
}
