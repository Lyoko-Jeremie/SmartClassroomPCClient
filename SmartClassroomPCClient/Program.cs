using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

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
        private static System.Threading.Timer _timer_KeepAlive;

        public static Form1 MainForm => _mainForm;

        [STAThread]
        public static void StartBackgroundThread()
        {
            if (_isBackgroundThreadRun) return;
            BackgroundThread.SetApartmentState(ApartmentState.STA);
            BackgroundThread.Start();
            _isBackgroundThreadRun = true;
        }

        private static void KeepAliveTimerThread(object state)
        {
            _mainForm.InformationTextLineInfo("KeepAliveTimerThread");
            ClientSocket cs = new ClientSocket();
            var r = cs.KeepAliveSocket(Config.ServerEndPoint);
            _mainForm.InformationTextLineInfo("KeepAliveTimerThreadEnd");
            if (r == SocketTaskFlag.ConnectFail)
            {
                _mainForm.InformationTextLineError("KeepAliveSocket:" + r);
                _timer_KeepAlive.Change(5 * 1000, 5 * 1000);
            }
            else
            {
                _timer_KeepAlive.Change(30 * 1000, 30 * 1000);
            }
        }

        private delegate void Any();

        private static void StartBackgroundThreadMain()
        {
            if (Config.ReadConfig())
            {
                _timer_KeepAlive = new System.Threading.Timer(KeepAliveTimerThread, null, 0, 30 * 1000);
            }
            else
            {
                //Config.ServerIp = IPAddress.Parse("127::1");
                //Config.ServerPort = 7864;
                //Config.WriteConfig();
                _mainForm.InformationTextLineError("ReadConfig Error...Exit.....");
                Thread.Sleep(5000);
                System.Environment.Exit(-1);
            }

            //while (true)
            //{
            //    _mainForm.InformationTextLine("SmartClassroom");
            //    Thread.Sleep(10);
            //}
            //Thread.Sleep(5000);
            //while (true)
            //{
            //    if (_mainForm.InvokeRequired)
            //    {
            //        _mainForm.Invoke(new Any(_mainForm.Hide));
            //    }
            //    _mainForm.InformationTextLineInfo("Hide");
            //    Thread.Sleep(1000);
            //    if (_mainForm.InvokeRequired)
            //    {
            //        _mainForm.Invoke(new Any(_mainForm.Show));
            //    }
            //    _mainForm.InformationTextLineInfo("Show");
            //    Thread.Sleep(1000);
            //}
        }
    }
}
