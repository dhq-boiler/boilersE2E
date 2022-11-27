using WindowsFormsApp.Controls;

namespace WindowsFormsApp
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.display = new System.Windows.Forms.TextBox();
            this.plusMinus = new System.Windows.Forms.Button();
            this.zero = new System.Windows.Forms.Button();
            this.dot = new System.Windows.Forms.Button();
            this.equal = new System.Windows.Forms.Button();
            this.plus = new System.Windows.Forms.Button();
            this.three = new System.Windows.Forms.Button();
            this.two = new System.Windows.Forms.Button();
            this.one = new System.Windows.Forms.Button();
            this.minus = new System.Windows.Forms.Button();
            this.six = new System.Windows.Forms.Button();
            this.five = new System.Windows.Forms.Button();
            this.four = new System.Windows.Forms.Button();
            this.multiple = new System.Windows.Forms.Button();
            this.nine = new System.Windows.Forms.Button();
            this.eight = new System.Windows.Forms.Button();
            this.seven = new System.Windows.Forms.Button();
            this.divide = new System.Windows.Forms.Button();
            this.Clear = new System.Windows.Forms.Button();
            this.ClearEntry = new System.Windows.Forms.Button();
            this.backspace = new System.Windows.Forms.Button();
            this.fomula = new WindowsFormsApp.Controls.UnfocusableTextbox();
            this.SuspendLayout();
            // 
            // display
            // 
            this.display.AccessibleName = "display";
            this.display.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.display.Location = new System.Drawing.Point(29, 22);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(505, 55);
            this.display.TabIndex = 0;
            this.display.Text = "0";
            this.display.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.display.TextChanged += new System.EventHandler(this.display_TextChanged);
            this.display.KeyDown += new System.Windows.Forms.KeyEventHandler(this.display_KeyDown);
            // 
            // plusMinus
            // 
            this.plusMinus.AccessibleName = "plusMinus";
            this.plusMinus.Font = new System.Drawing.Font("MS UI Gothic", 36F);
            this.plusMinus.Location = new System.Drawing.Point(29, 390);
            this.plusMinus.Name = "plusMinus";
            this.plusMinus.Size = new System.Drawing.Size(115, 68);
            this.plusMinus.TabIndex = 1;
            this.plusMinus.Text = "+/-";
            this.plusMinus.UseVisualStyleBackColor = true;
            this.plusMinus.Click += new System.EventHandler(this.plusMinus_Click);
            // 
            // zero
            // 
            this.zero.AccessibleName = "zero";
            this.zero.Font = new System.Drawing.Font("MS UI Gothic", 36F);
            this.zero.Location = new System.Drawing.Point(159, 390);
            this.zero.Name = "zero";
            this.zero.Size = new System.Drawing.Size(115, 68);
            this.zero.TabIndex = 2;
            this.zero.Text = "0";
            this.zero.UseVisualStyleBackColor = true;
            this.zero.Click += new System.EventHandler(this.zero_Click);
            // 
            // dot
            // 
            this.dot.AccessibleName = "dot";
            this.dot.Font = new System.Drawing.Font("MS UI Gothic", 36F);
            this.dot.Location = new System.Drawing.Point(289, 390);
            this.dot.Name = "dot";
            this.dot.Size = new System.Drawing.Size(115, 68);
            this.dot.TabIndex = 3;
            this.dot.Text = ".";
            this.dot.UseVisualStyleBackColor = true;
            this.dot.Click += new System.EventHandler(this.dot_Click);
            // 
            // equal
            // 
            this.equal.AccessibleName = "equal";
            this.equal.Font = new System.Drawing.Font("MS UI Gothic", 36F);
            this.equal.Location = new System.Drawing.Point(419, 390);
            this.equal.Name = "equal";
            this.equal.Size = new System.Drawing.Size(115, 68);
            this.equal.TabIndex = 4;
            this.equal.Text = "=";
            this.equal.UseVisualStyleBackColor = true;
            this.equal.Click += new System.EventHandler(this.equal_Click);
            // 
            // plus
            // 
            this.plus.AccessibleName = "plus";
            this.plus.Font = new System.Drawing.Font("MS UI Gothic", 36F);
            this.plus.Location = new System.Drawing.Point(419, 316);
            this.plus.Name = "plus";
            this.plus.Size = new System.Drawing.Size(115, 68);
            this.plus.TabIndex = 8;
            this.plus.Text = "+";
            this.plus.UseVisualStyleBackColor = true;
            this.plus.Click += new System.EventHandler(this.plus_Click);
            // 
            // three
            // 
            this.three.AccessibleName = "three";
            this.three.Font = new System.Drawing.Font("MS UI Gothic", 36F);
            this.three.Location = new System.Drawing.Point(289, 316);
            this.three.Name = "three";
            this.three.Size = new System.Drawing.Size(115, 68);
            this.three.TabIndex = 7;
            this.three.Text = "3";
            this.three.UseVisualStyleBackColor = true;
            this.three.Click += new System.EventHandler(this.three_Click);
            // 
            // two
            // 
            this.two.AccessibleName = "two";
            this.two.Font = new System.Drawing.Font("MS UI Gothic", 36F);
            this.two.Location = new System.Drawing.Point(159, 316);
            this.two.Name = "two";
            this.two.Size = new System.Drawing.Size(115, 68);
            this.two.TabIndex = 6;
            this.two.Text = "2";
            this.two.UseVisualStyleBackColor = true;
            this.two.Click += new System.EventHandler(this.two_Click);
            // 
            // one
            // 
            this.one.AccessibleName = "one";
            this.one.Font = new System.Drawing.Font("MS UI Gothic", 36F);
            this.one.Location = new System.Drawing.Point(29, 316);
            this.one.Name = "one";
            this.one.Size = new System.Drawing.Size(115, 68);
            this.one.TabIndex = 5;
            this.one.Text = "1";
            this.one.UseVisualStyleBackColor = true;
            this.one.Click += new System.EventHandler(this.one_Click);
            // 
            // minus
            // 
            this.minus.AccessibleName = "minus";
            this.minus.Font = new System.Drawing.Font("MS UI Gothic", 36F);
            this.minus.Location = new System.Drawing.Point(419, 242);
            this.minus.Name = "minus";
            this.minus.Size = new System.Drawing.Size(115, 68);
            this.minus.TabIndex = 12;
            this.minus.Text = "-";
            this.minus.UseVisualStyleBackColor = true;
            this.minus.Click += new System.EventHandler(this.minus_Click);
            // 
            // six
            // 
            this.six.AccessibleName = "six";
            this.six.Font = new System.Drawing.Font("MS UI Gothic", 36F);
            this.six.Location = new System.Drawing.Point(289, 242);
            this.six.Name = "six";
            this.six.Size = new System.Drawing.Size(115, 68);
            this.six.TabIndex = 11;
            this.six.Text = "6";
            this.six.UseVisualStyleBackColor = true;
            this.six.Click += new System.EventHandler(this.six_Click);
            // 
            // five
            // 
            this.five.AccessibleName = "five";
            this.five.Font = new System.Drawing.Font("MS UI Gothic", 36F);
            this.five.Location = new System.Drawing.Point(159, 242);
            this.five.Name = "five";
            this.five.Size = new System.Drawing.Size(115, 68);
            this.five.TabIndex = 10;
            this.five.Text = "5";
            this.five.UseVisualStyleBackColor = true;
            this.five.Click += new System.EventHandler(this.five_Click);
            // 
            // four
            // 
            this.four.AccessibleName = "four";
            this.four.Font = new System.Drawing.Font("MS UI Gothic", 36F);
            this.four.Location = new System.Drawing.Point(29, 242);
            this.four.Name = "four";
            this.four.Size = new System.Drawing.Size(115, 68);
            this.four.TabIndex = 9;
            this.four.Text = "4";
            this.four.UseVisualStyleBackColor = true;
            this.four.Click += new System.EventHandler(this.four_Click);
            // 
            // multiple
            // 
            this.multiple.AccessibleName = "multiple";
            this.multiple.Font = new System.Drawing.Font("MS UI Gothic", 36F);
            this.multiple.Location = new System.Drawing.Point(419, 168);
            this.multiple.Name = "multiple";
            this.multiple.Size = new System.Drawing.Size(115, 68);
            this.multiple.TabIndex = 16;
            this.multiple.Text = "×";
            this.multiple.UseVisualStyleBackColor = true;
            this.multiple.Click += new System.EventHandler(this.multiple_Click);
            // 
            // nine
            // 
            this.nine.AccessibleName = "nine";
            this.nine.Font = new System.Drawing.Font("MS UI Gothic", 36F);
            this.nine.Location = new System.Drawing.Point(289, 168);
            this.nine.Name = "nine";
            this.nine.Size = new System.Drawing.Size(115, 68);
            this.nine.TabIndex = 15;
            this.nine.Text = "9";
            this.nine.UseVisualStyleBackColor = true;
            this.nine.Click += new System.EventHandler(this.nine_Click);
            // 
            // eight
            // 
            this.eight.AccessibleName = "eight";
            this.eight.Font = new System.Drawing.Font("MS UI Gothic", 36F);
            this.eight.Location = new System.Drawing.Point(159, 168);
            this.eight.Name = "eight";
            this.eight.Size = new System.Drawing.Size(115, 68);
            this.eight.TabIndex = 14;
            this.eight.Text = "8";
            this.eight.UseVisualStyleBackColor = true;
            this.eight.Click += new System.EventHandler(this.eight_Click);
            // 
            // seven
            // 
            this.seven.AccessibleName = "seven";
            this.seven.Font = new System.Drawing.Font("MS UI Gothic", 36F);
            this.seven.Location = new System.Drawing.Point(29, 168);
            this.seven.Name = "seven";
            this.seven.Size = new System.Drawing.Size(115, 68);
            this.seven.TabIndex = 13;
            this.seven.Text = "7";
            this.seven.UseVisualStyleBackColor = true;
            this.seven.Click += new System.EventHandler(this.seven_Click);
            // 
            // divide
            // 
            this.divide.AccessibleName = "divide";
            this.divide.Font = new System.Drawing.Font("MS UI Gothic", 36F);
            this.divide.Location = new System.Drawing.Point(419, 94);
            this.divide.Name = "divide";
            this.divide.Size = new System.Drawing.Size(115, 68);
            this.divide.TabIndex = 20;
            this.divide.Text = "÷";
            this.divide.UseVisualStyleBackColor = true;
            this.divide.Click += new System.EventHandler(this.divide_Click);
            // 
            // Clear
            // 
            this.Clear.AccessibleName = "clear";
            this.Clear.Font = new System.Drawing.Font("MS UI Gothic", 36F);
            this.Clear.Location = new System.Drawing.Point(289, 94);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(115, 68);
            this.Clear.TabIndex = 19;
            this.Clear.Text = "C";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // ClearEntry
            // 
            this.ClearEntry.AccessibleName = "clearEntry";
            this.ClearEntry.Font = new System.Drawing.Font("MS UI Gothic", 36F);
            this.ClearEntry.Location = new System.Drawing.Point(159, 94);
            this.ClearEntry.Name = "ClearEntry";
            this.ClearEntry.Size = new System.Drawing.Size(115, 68);
            this.ClearEntry.TabIndex = 18;
            this.ClearEntry.Text = "CE";
            this.ClearEntry.UseVisualStyleBackColor = true;
            this.ClearEntry.Click += new System.EventHandler(this.ClearEntry_Click);
            // 
            // backspace
            // 
            this.backspace.AccessibleName = "backspace";
            this.backspace.Font = new System.Drawing.Font("MS UI Gothic", 36F);
            this.backspace.Location = new System.Drawing.Point(29, 94);
            this.backspace.Name = "backspace";
            this.backspace.Size = new System.Drawing.Size(115, 68);
            this.backspace.TabIndex = 17;
            this.backspace.Text = "⌫";
            this.backspace.UseVisualStyleBackColor = true;
            this.backspace.Click += new System.EventHandler(this.backspace_Click);
            // 
            // fomula
            // 
            this.fomula.AccessibleName = "fomula";
            this.fomula.CausesValidation = false;
            this.fomula.Location = new System.Drawing.Point(29, 7);
            this.fomula.Name = "fomula";
            this.fomula.ReadOnly = true;
            this.fomula.Size = new System.Drawing.Size(505, 19);
            this.fomula.TabIndex = 21;
            this.fomula.TabStop = false;
            this.fomula.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 478);
            this.Controls.Add(this.fomula);
            this.Controls.Add(this.divide);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.ClearEntry);
            this.Controls.Add(this.backspace);
            this.Controls.Add(this.multiple);
            this.Controls.Add(this.nine);
            this.Controls.Add(this.eight);
            this.Controls.Add(this.seven);
            this.Controls.Add(this.minus);
            this.Controls.Add(this.six);
            this.Controls.Add(this.five);
            this.Controls.Add(this.four);
            this.Controls.Add(this.plus);
            this.Controls.Add(this.three);
            this.Controls.Add(this.two);
            this.Controls.Add(this.one);
            this.Controls.Add(this.equal);
            this.Controls.Add(this.dot);
            this.Controls.Add(this.zero);
            this.Controls.Add(this.plusMinus);
            this.Controls.Add(this.display);
            this.Name = "Form1";
            this.Text = "WinForms 電卓";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox display;
        private System.Windows.Forms.Button plusMinus;
        private System.Windows.Forms.Button zero;
        private System.Windows.Forms.Button dot;
        private System.Windows.Forms.Button equal;
        private System.Windows.Forms.Button plus;
        private System.Windows.Forms.Button three;
        private System.Windows.Forms.Button two;
        private System.Windows.Forms.Button one;
        private System.Windows.Forms.Button minus;
        private System.Windows.Forms.Button six;
        private System.Windows.Forms.Button five;
        private System.Windows.Forms.Button four;
        private System.Windows.Forms.Button multiple;
        private System.Windows.Forms.Button nine;
        private System.Windows.Forms.Button eight;
        private System.Windows.Forms.Button seven;
        private System.Windows.Forms.Button divide;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.Button ClearEntry;
        private System.Windows.Forms.Button backspace;
        private UnfocusableTextbox fomula;
    }
}

