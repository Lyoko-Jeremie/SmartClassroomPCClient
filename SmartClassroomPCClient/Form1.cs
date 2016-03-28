using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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
            BigTitile.MouseDown += MouseDownDrop;
            this.MouseDown += MouseDownDrop;
            inputTextBox.KeyDown += InputTextBoxEnterEvent;

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

        public void InformationTextLineError(String line)
        {
            InformationTextLine("[Error] " + line);
        }
        public void InformationTextLineInfo(String line)
        {
            InformationTextLine("[Info] " + line);
        }
        public void InformationTextLineCmd(String line)
        {
            InformationTextLine("[CMD] " + line);
        }

        public void InformationTextLine(String line)
        {
            //lock (_informationText)
            {
                _informationText += "\r\n" + line;
                if (_informationText.Length > 8192)
                    _informationText = _informationText.Substring(_informationText.Length - 8192);
            }
        }

        public void ClearInformationText()
        {
            _informationText = "";
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

        // 窗口拖动
        [DllImport("user32")]
        private static extern bool ReleaseCapture();
        [DllImport("user32")]
        private static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        const int WM_SYSCOMMAND = 0x0112;
        const int SC_MOVE = 0Xf010;
        const int HTCAPTION = 0x0002;
        private void MouseDownDrop(object sender, MouseEventArgs e)
        {
            MouseDownDrop();
        }
        private void MouseDownDrop()
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void buttonHide_Click(object sender, EventArgs e)
        {
            InformationTextLineInfo("Hide_Click");
            this.Hide();
            notifyIcon.ShowBalloonTip(1000);
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            InformationTextLineInfo("Icon_MouseDoubleClick");
            this.Show();
        }

        private void ToolStripMenuItemOpen_Click(object sender, EventArgs e)
        {
            InformationTextLineInfo("ToolStripMenuItemOpen_Click");
            this.Show();
        }

        private void ToolStripMenuItemHide_Click(object sender, EventArgs e)
        {
            InformationTextLineInfo("ToolStripMenuItemHide_Click");
            this.Hide();
            notifyIcon.ShowBalloonTip(1000);
        }

        private void ToolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            Program.Exit();
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            Program.CM.CommandString(inputTextBox.Text);
            inputTextBox.Text = "";
        }

        private void InputTextBoxEnterEvent(object sender, KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.KeyCode != Keys.Enter) return;
            Program.CM.CommandString(inputTextBox.Text);
            inputTextBox.Text = "";
        }

    }
}
