using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            fomula.Text = string.Empty;
        }

        private bool clearFlag = false;

        /// <summary>
        /// 引用：@mizu-kazu [C#] eval風 文字列式を演算する
        /// https://qiita.com/mizu-kazu/items/e75e5cd8c91dbf34d44c
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private static T StrCalc<T>(string s, params object[] args)
        {
            using (DataTable dt = new DataTable())
            {
                s = string.Format(s, args);
                object result = dt.Compute(s, "");
                var converter = TypeDescriptor.GetConverter(typeof(T));
                return (T)converter.ConvertFromString(result.ToString());
            }
        }

        private void ClearDisplayIfFlagIsTrue()
        {
            if (clearFlag)
            {
                display.Text = string.Empty;
                clearFlag = false;
            }
        }

        private void RemovePreviousAddedOperatorIfPreviousCharIsOperator()
        {
            if (IfPreviousCharIsOperator())
            {
                fomula.Text = fomula.Text.Substring(0, fomula.Text.Length - 1);
            }
        }

        private bool IfPreviousCharIsOperator()
        {
            return fomula.Text.Last() == '+'
                || fomula.Text.Last() == '-'
                || fomula.Text.Last() == '×'
                || fomula.Text.Last() == '÷';
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            clearFlag = true;
        }

        private void zero_Click(object sender, EventArgs e)
        {
            fomula.Text += "0";
            ClearDisplayIfFlagIsTrue();
            display.Text += "0";
        }

        private void one_Click(object sender, EventArgs e)
        {
            fomula.Text += "1";
            ClearDisplayIfFlagIsTrue();
            display.Text += "1";
        }

        private void two_Click(object sender, EventArgs e)
        {
            fomula.Text += "2";
            ClearDisplayIfFlagIsTrue();
            display.Text += "2";
        }

        private void three_Click(object sender, EventArgs e)
        {
            fomula.Text += "3";
            ClearDisplayIfFlagIsTrue();
            display.Text += "3";
        }

        private void four_Click(object sender, EventArgs e)
        {
            fomula.Text += "4";
            ClearDisplayIfFlagIsTrue();
            display.Text += "4";
        }

        private void five_Click(object sender, EventArgs e)
        {
            fomula.Text += "5";
            ClearDisplayIfFlagIsTrue();
            display.Text += "5";
        }

        private void six_Click(object sender, EventArgs e)
        {
            fomula.Text += "6";
            ClearDisplayIfFlagIsTrue();
            display.Text += "6";
        }

        private void seven_Click(object sender, EventArgs e)
        {
            fomula.Text += "7";
            ClearDisplayIfFlagIsTrue();
            display.Text += "7";
        }

        private void eight_Click(object sender, EventArgs e)
        {
            fomula.Text += "8";
            ClearDisplayIfFlagIsTrue();
            display.Text += "8";
        }

        private void nine_Click(object sender, EventArgs e)
        {
            fomula.Text += "9";
            ClearDisplayIfFlagIsTrue();
            display.Text += "9";
        }

        private void dot_Click(object sender, EventArgs e)
        {
            if (int.TryParse(display.Text, out int i))
            {
                fomula.Text += ".";
                display.Text += ".";
                clearFlag = false;
            }
        }

        private void plus_Click(object sender, EventArgs e)
        {
            RemovePreviousAddedOperatorIfPreviousCharIsOperator();
            fomula.Text += "+";
            clearFlag = true;
        }

        private void minus_Click(object sender, EventArgs e)
        {
            RemovePreviousAddedOperatorIfPreviousCharIsOperator();
            fomula.Text += "-";
            clearFlag = true;
        }

        private void multiple_Click(object sender, EventArgs e)
        {
            RemovePreviousAddedOperatorIfPreviousCharIsOperator();
            fomula.Text += "×";
            clearFlag = true;
        }

        private void divide_Click(object sender, EventArgs e)
        {
            RemovePreviousAddedOperatorIfPreviousCharIsOperator();
            fomula.Text += "÷";
            clearFlag = true;
        }

        private void ClearEntry_Click(object sender, EventArgs e)
        {
            fomula.Text = string.Empty;
            display.Text = "0";
            clearFlag = true;
        }

        private void equal_Click(object sender, EventArgs e)
        {
            display.Text = StrCalc<double>(fomula.Text.Replace("×", "*").Replace("÷", "/")).ToString();
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            if (fomula.Text.Length == 0)
                return;
            fomula.Text = fomula.Text.Substring(0, fomula.Text.LastIndexOf(display.Text));
            display.Text = "0";
            clearFlag = true;
        }

        private void plusMinus_Click(object sender, EventArgs e)
        {
            var temp = display.Text;
            fomula.Text = fomula.Text.Substring(0, fomula.Text.LastIndexOf(display.Text));
            display.Text = "0";
            if (temp.StartsWith("-"))
            {
                display.Text = temp.Substring(1);
            }
            else if (temp.StartsWith("(-"))
            {
                display.Text = temp.Substring(2, temp.Substring(2).Length - 1);
            }
            else
            {
                if (IfPreviousCharIsOperator())
                {
                    display.Text = $"(-{temp})";
                }
                else
                {
                    display.Text = $"-{temp}";
                }
            }
            fomula.Text += display.Text;
        }

        private void backspace_Click(object sender, EventArgs e)
        {
            if (display.Text == "0")
                return;
            display.Text = display.Text.Substring(0, display.Text.Length- 1);
            if (display.Text.Length == 0)
            {
                display.Text = "0";
                clearFlag = true;
            }
            fomula.Text = fomula.Text.Substring(0, fomula.Text.Length - 1);
        }
    }
}
