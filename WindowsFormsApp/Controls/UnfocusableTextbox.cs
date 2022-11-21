using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp.Controls
{
    public class UnfocusableTextbox : TextBox
    {
        public UnfocusableTextbox()
        {
            this.ReadOnly = true;
        }

        protected override void WndProc(ref Message m)
        {
            // Ignore all messages that try to set the focus.
            if (m.Msg != 0x7)
            {
                base.WndProc(ref m);
            }
        }
    }
}
