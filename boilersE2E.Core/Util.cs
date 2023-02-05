using Kolibri;

namespace boilersE2E
{
    public static class Util
    {
        public static void SetTextToClipboard(string text)
        {
            Clippy.Result result;
            var tryCount = 0;
            do
            {
                result = Clippy.PushStringToClipboard(text);
                tryCount++;
                Thread.Sleep(100);
            } while (!result.OK && tryCount <= 10);

            if (!result.OK)
            {
                throw new Exception("result.OK == false");
            }
        }

        /// <summary>
        /// https://stackoverflow.com/questions/60987163/sendkeys-not-working-in-winappdriver-using-appium
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ReplaceKeyCode(string text)
        {
            text = text.Trim('+');
            //Action characters like tab, arrow down and etc
            text = text.Replace(OpenQA.Selenium.Keys.Backspace, "{BACKSPACE}");
            text = text.Replace(OpenQA.Selenium.Keys.Delete, "{DELETE}");
            text = text.Replace(OpenQA.Selenium.Keys.ArrowDown, "{DOWN}");
            text = text.Replace(OpenQA.Selenium.Keys.End, "{END}");
            text = text.Replace(OpenQA.Selenium.Keys.Enter, "{ENTER}");
            text = text.Replace(OpenQA.Selenium.Keys.Escape, "{ESC}");
            text = text.Replace(OpenQA.Selenium.Keys.Help, "{HELP}");
            text = text.Replace(OpenQA.Selenium.Keys.Home, "{HOME}");
            text = text.Replace(OpenQA.Selenium.Keys.Insert, "{INSERT}");
            text = text.Replace(OpenQA.Selenium.Keys.ArrowLeft, "{LEFT}");
            text = text.Replace(OpenQA.Selenium.Keys.PageDown, "{PGDN}");
            text = text.Replace(OpenQA.Selenium.Keys.PageUp, "{PGUP}");
            text = text.Replace(OpenQA.Selenium.Keys.ArrowRight, "{RIGHT}");
            text = text.Replace(OpenQA.Selenium.Keys.Tab, "{TAB}");
            text = text.Replace(OpenQA.Selenium.Keys.ArrowUp, "{UP}");
            text = text.Replace(OpenQA.Selenium.Keys.F1, "{F1}");
            text = text.Replace(OpenQA.Selenium.Keys.F2, "{F2}");
            text = text.Replace(OpenQA.Selenium.Keys.F3, "{F3}");
            text = text.Replace(OpenQA.Selenium.Keys.F4, "{F4}");
            text = text.Replace(OpenQA.Selenium.Keys.F5, "{F5}");
            text = text.Replace(OpenQA.Selenium.Keys.F6, "{F6}");
            text = text.Replace(OpenQA.Selenium.Keys.F7, "{F7}");
            text = text.Replace(OpenQA.Selenium.Keys.F8, "{F8}");
            text = text.Replace(OpenQA.Selenium.Keys.F9, "{F9}");
            text = text.Replace(OpenQA.Selenium.Keys.F10, "{F10}");
            text = text.Replace(OpenQA.Selenium.Keys.F11, "{F11}");
            text = text.Replace(OpenQA.Selenium.Keys.F12, "{F12}");
            text = text.Replace(OpenQA.Selenium.Keys.Add, "{ADD}");
            text = text.Replace(OpenQA.Selenium.Keys.Subtract, "{SUBTRACT}");
            text = text.Replace(OpenQA.Selenium.Keys.Multiply, "{MULTIPLY}");
            text = text.Replace(OpenQA.Selenium.Keys.Divide, "{DIVIDE}");

            //Special Keys like control, shift and alt
            text = text.Replace(OpenQA.Selenium.Keys.Control, "^");
            text = text.Replace(OpenQA.Selenium.Keys.LeftControl, "^");
            text = text.Replace(OpenQA.Selenium.Keys.Shift, "+");
            text = text.Replace(OpenQA.Selenium.Keys.LeftShift, "+");
            text = text.Replace(OpenQA.Selenium.Keys.Alt, "%");
            text = text.Replace(OpenQA.Selenium.Keys.LeftAlt, "%");
            return text;
        }
    }
}
