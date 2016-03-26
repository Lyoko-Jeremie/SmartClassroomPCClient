using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartClassroomPCClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CommandToolHide();
        }

        private void InputCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.InputCheckBox.Checked)
            {
                CommandToolShow();
            }
            else
            {
                CommandToolHide();
            }
        }


        private void CommandToolHide()
        {
            this.buttonClear.Hide();
            this.buttonSubmit.Hide();
            this.inputTextBox.Hide();
        }
        private void CommandToolShow()
        {
            this.buttonClear.Show();
            this.buttonSubmit.Show();
            this.inputTextBox.Show();
        }
    }
}
