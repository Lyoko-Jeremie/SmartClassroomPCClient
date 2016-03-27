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
        private String _informationText = "SmartClassroom PC Client 正在启动......";


        public Form1()
        {
            InitializeComponent();
            CommandToolHide();
            timer1.Start();
            Program.StartBackgroundThread();
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

        public void InformationTextLine(String line)
        {
            //lock (_informationText)
            {
                _informationText += "\r\n" + line;
                if (_informationText.Length > 8192)
                    _informationText = _informationText.Substring(_informationText.Length-8192);
            }
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            //lock (_informationText)
            {
                this.informationTextBox.Text = _informationText;
            }
            this.informationTextBox.Select(this.informationTextBox.TextLength, 0);
            this.informationTextBox.ScrollToCaret();
        }
    }
}
