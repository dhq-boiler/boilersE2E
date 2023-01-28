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
        private bool equalFlag = false;
        private bool ctrlV = false;

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
                if (string.IsNullOrEmpty(result.ToString()))
                {
                    return default(T);
                }
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
            if (fomula.Text.Length == 0)
            {
                return false;
            }
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
            ClearDisplayIfFlagIsTrue();
            ctrlV = false;
            equalFlag = false;
            display.Text += "0";
        }

        private void one_Click(object sender, EventArgs e)
        {
            ClearDisplayIfFlagIsTrue();
            ctrlV = false;
            equalFlag = false;
            display.Text += "1";
        }

        private void two_Click(object sender, EventArgs e)
        {
            ClearDisplayIfFlagIsTrue();
            ctrlV = false;
            equalFlag = false;
            display.Text += "2";
        }

        private void three_Click(object sender, EventArgs e)
        {
            ClearDisplayIfFlagIsTrue();
            ctrlV = false;
            equalFlag = false;
            display.Text += "3";
        }

        private void four_Click(object sender, EventArgs e)
        {
            ClearDisplayIfFlagIsTrue();
            ctrlV = false;
            equalFlag = false;
            display.Text += "4";
        }

        private void five_Click(object sender, EventArgs e)
        {
            ClearDisplayIfFlagIsTrue();
            ctrlV = false;
            equalFlag = false;
            display.Text += "5";
        }

        private void six_Click(object sender, EventArgs e)
        {
            ClearDisplayIfFlagIsTrue();
            ctrlV = false;
            equalFlag = false;
            display.Text += "6";
        }

        private void seven_Click(object sender, EventArgs e)
        {
            ClearDisplayIfFlagIsTrue();
            ctrlV = false;
            equalFlag = false;
            display.Text += "7";
        }

        private void eight_Click(object sender, EventArgs e)
        {
            ClearDisplayIfFlagIsTrue();
            ctrlV = false;
            equalFlag = false;
            display.Text += "8";
        }

        private void nine_Click(object sender, EventArgs e)
        {
            ClearDisplayIfFlagIsTrue();
            ctrlV = false;
            equalFlag = false;
            display.Text += "9";
        }

        private void dot_Click(object sender, EventArgs e)
        {
            if (int.TryParse(display.Text, out int i))
            {
                display.Text += ".";
                ctrlV = false;
                clearFlag = false;
            }
        }

        private void plus_Click(object sender, EventArgs e)
        {
            RemovePreviousAddedOperatorIfPreviousCharIsOperator();
            fomula.Text += "+";
            ctrlV = false;
            clearFlag = true;
            equalFlag = false;
        }

        private void minus_Click(object sender, EventArgs e)
        {
            RemovePreviousAddedOperatorIfPreviousCharIsOperator();
            fomula.Text += "-";
            ctrlV = false;
            clearFlag = true;
            equalFlag = false;
        }

        private void multiple_Click(object sender, EventArgs e)
        {
            RemovePreviousAddedOperatorIfPreviousCharIsOperator();
            fomula.Text += "×";
            ctrlV = false;
            clearFlag = true;
            equalFlag = false;
        }

        private void divide_Click(object sender, EventArgs e)
        {
            RemovePreviousAddedOperatorIfPreviousCharIsOperator();
            fomula.Text += "÷";
            ctrlV = false;
            clearFlag = true;
            equalFlag = false;
        }

        private void ClearEntry_Click(object sender, EventArgs e)
        {
            fomula.Text = string.Empty;
            ctrlV = false;
            equalFlag = true;
            display.Text = "0";
            clearFlag = true;
            equalFlag = false;
        }

        private void equal_Click(object sender, EventArgs e)
        {
            ctrlV = false;
            equalFlag = true;
            display.Text = StrCalc<double>(fomula.Text.Replace("×", "*").Replace("÷", "/")).ToString();
            equalFlag = false;
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            if (fomula.Text.Length == 0)
                return;
            fomula.Text = fomula.Text.Substring(0, fomula.Text.LastIndexOf(display.Text));
            ctrlV = false;
            display.Text = "0";
            clearFlag = true;
            equalFlag = false;
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

        private void display_TextChanged(object sender, EventArgs e)
        {
            if (equalFlag)
                return;
            var textbox = sender as TextBox;
            if (ctrlV)
            {
                fomula.Text += textbox.Text;
            }
            else
            {
                if (textbox.Text.Length - 1 >= 0)
                {
                    fomula.Text += display.Text.Substring(display.Text.IndexOf(textbox.Text) + textbox.Text.Length - 1);
                }
            }
        }

        private void display_KeyDown(object sender, KeyEventArgs e)
        {
            ctrlV = e.Modifiers == Keys.Control && e.KeyCode == Keys.V;
        }
    }
}
