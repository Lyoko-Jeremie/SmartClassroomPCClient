using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartClassroomPCClient
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]     // 所以说STAThread模型直接免除线程同步操作  C#实现方法级线程同步
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            _mainForm = new Form1();
            Application.Run(_mainForm);
        }

        private static readonly Thread BackgroundThread = new Thread(new ThreadStart(StartBackgroundThreadMain));
        private static bool _isBackgroundThreadRun = false;
        private static Form1 _mainForm;

        [STAThread]
        public static void StartBackgroundThread()
        {
            if (!_isBackgroundThreadRun)
            {
                BackgroundThread.SetApartmentState(ApartmentState.STA);
                BackgroundThread.Start();
                _isBackgroundThreadRun = true;
            }
        }

        private static void StartBackgroundThreadMain()
        {
            while (true)
            {
                _mainForm.InformationTextLine("SmartClassroom");
                Thread.Sleep(10);
            }
        }

    }
}
