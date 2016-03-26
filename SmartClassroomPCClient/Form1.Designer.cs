namespace SmartClassroomPCClient
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.buttonHide = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.informationTextBox = new System.Windows.Forms.TextBox();
            this.inputTextBox = new System.Windows.Forms.TextBox();
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.InputCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 32F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(588, 57);
            this.label1.TabIndex = 1;
            this.label1.Text = "SmartClassroom PC Client";
            // 
            // buttonHide
            // 
            this.buttonHide.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonHide.BackColor = System.Drawing.Color.Black;
            this.buttonHide.ForeColor = System.Drawing.Color.White;
            this.buttonHide.Location = new System.Drawing.Point(12, 283);
            this.buttonHide.Name = "buttonHide";
            this.buttonHide.Size = new System.Drawing.Size(244, 31);
            this.buttonHide.TabIndex = 1;
            this.buttonHide.Text = "Hide 隐藏";
            this.buttonHide.UseVisualStyleBackColor = false;
            // 
            // buttonClear
            // 
            this.buttonClear.BackColor = System.Drawing.Color.Black;
            this.buttonClear.ForeColor = System.Drawing.Color.White;
            this.buttonClear.Location = new System.Drawing.Point(500, 283);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(100, 23);
            this.buttonClear.TabIndex = 3;
            this.buttonClear.TabStop = false;
            this.buttonClear.Text = "Clear 清空";
            this.buttonClear.UseVisualStyleBackColor = false;
            // 
            // informationTextBox
            // 
            this.informationTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.informationTextBox.BackColor = System.Drawing.Color.Black;
            this.informationTextBox.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.informationTextBox.ForeColor = System.Drawing.SystemColors.Info;
            this.informationTextBox.Location = new System.Drawing.Point(12, 71);
            this.informationTextBox.Multiline = true;
            this.informationTextBox.Name = "informationTextBox";
            this.informationTextBox.ReadOnly = true;
            this.informationTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.informationTextBox.ShortcutsEnabled = false;
            this.informationTextBox.Size = new System.Drawing.Size(588, 177);
            this.informationTextBox.TabIndex = 4;
            this.informationTextBox.TabStop = false;
            this.informationTextBox.Text = "信息界面\r\n系统信息\r\n进程LOG\r\n\r\n信息界面\r\n系统信息\r\n进程LOG\r\n\r\n信息界面\r\n系统信息\r\n进程LOG\r\n\r\n信息界面\r\n系统信息\r\n进程LOG\r" +
    "\n\r\n信息界面\r\n系统信息\r\n进程LOG\r\n\r\n信息界面\r\n系统信息\r\n进程LOG\r\n\r\n";
            // 
            // inputTextBox
            // 
            this.inputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputTextBox.BackColor = System.Drawing.Color.Black;
            this.inputTextBox.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.inputTextBox.ForeColor = System.Drawing.SystemColors.Info;
            this.inputTextBox.Location = new System.Drawing.Point(12, 256);
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Size = new System.Drawing.Size(482, 21);
            this.inputTextBox.TabIndex = 2;
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSubmit.BackColor = System.Drawing.Color.Black;
            this.buttonSubmit.ForeColor = System.Drawing.Color.White;
            this.buttonSubmit.Location = new System.Drawing.Point(500, 254);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.Size = new System.Drawing.Size(100, 23);
            this.buttonSubmit.TabIndex = 3;
            this.buttonSubmit.Text = "Submit 提交";
            this.buttonSubmit.UseVisualStyleBackColor = false;
            // 
            // InputCheckBox
            // 
            this.InputCheckBox.AutoSize = true;
            this.InputCheckBox.ForeColor = System.Drawing.Color.White;
            this.InputCheckBox.Location = new System.Drawing.Point(274, 289);
            this.InputCheckBox.Name = "InputCheckBox";
            this.InputCheckBox.Size = new System.Drawing.Size(84, 16);
            this.InputCheckBox.TabIndex = 5;
            this.InputCheckBox.Text = "Input 输入";
            this.InputCheckBox.UseVisualStyleBackColor = true;
            this.InputCheckBox.CheckedChanged += new System.EventHandler(this.InputCheckBox_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(613, 326);
            this.ControlBox = false;
            this.Controls.Add(this.InputCheckBox);
            this.Controls.Add(this.buttonSubmit);
            this.Controls.Add(this.inputTextBox);
            this.Controls.Add(this.informationTextBox);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonHide);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonHide;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.TextBox informationTextBox;
        private System.Windows.Forms.TextBox inputTextBox;
        private System.Windows.Forms.Button buttonSubmit;
        private System.Windows.Forms.CheckBox InputCheckBox;
    }
}

