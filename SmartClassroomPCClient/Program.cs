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

        public static bool StopProgram = false;
        public static readonly Object StopProgramLock = new object();

        public static CommandModule CM;

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
        private static Timer _timerKeepAlive;

        public static Form1 MainForm => _mainForm;


        [STAThread]
        public static void StartBackgroundThread()
        {
            if (_isBackgroundThreadRun) return;
            BackgroundThread.SetApartmentState(ApartmentState.STA);
            BackgroundThread.Start();
            _isBackgroundThreadRun = true;
        }

        public static bool KeepAliveTimerThreadOutput { get; set; } = true;

        private static void KeepAliveTimerThread(object state)
        {
            InformationTextLineKeepAliveTimerThreadInfo("KeepAliveTimerThread");
            ClientSocket cs = new ClientSocket();
            var r = cs.KeepAliveSocket(Config.ServerEndPoint);
            InformationTextLineKeepAliveTimerThreadInfo("KeepAliveTimerThreadEnd");
            if (r == SocketTaskFlag.ConnectFail)
            {
                InformationTextLineKeepAliveTimerThreadError("KeepAliveSocket:" + r);
                _timerKeepAlive.Change(5 * 1000, 5 * 1000);
            }
            else
            {
                switch (r)
                {
                    case SocketTaskFlag.KeepAliveFail:
                    case SocketTaskFlag.ConnectAboart:
                        _timerKeepAlive.Change(30 * 1000, 30 * 1000);
                        break;
                    case SocketTaskFlag.ReciveTimeOut:
                        _timerKeepAlive.Change(30 * 1000, 30 * 1000);
                        break;
                    case SocketTaskFlag.TaskShutdown:
                        LocalCommand.Shutdown();
                        _timerKeepAlive.Change(15 * 1000, 15 * 1000);
                        break;
                    case SocketTaskFlag.TaskRestart:
                        LocalCommand.Restart();
                        _timerKeepAlive.Change(15 * 1000, 15 * 1000);
                        break;
                    case SocketTaskFlag.TaskLogout:
                        LocalCommand.Logout();
                        _timerKeepAlive.Change(15 * 1000, 15 * 1000);
                        break;
                    case SocketTaskFlag.TaskNoThing:
                    case SocketTaskFlag.TaskUnknow:
                    case SocketTaskFlag.FormatError:
                        _timerKeepAlive.Change(30 * 1000, 30 * 1000);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private static void InformationTextLineKeepAliveTimerThreadInfo(string line)
        {
            if (!KeepAliveTimerThreadOutput) return;
            _mainForm.InformationTextLineInfo(line);
        }

        private static void InformationTextLineKeepAliveTimerThreadError(string line)
        {
            if (!KeepAliveTimerThreadOutput) return;
            _mainForm.InformationTextLineError(line);
        }

        public delegate void Any();

        private static void StartBackgroundThreadMain()
        {
            if (!Config.ReadConfig())
            {
                //Config.ServerIp = IPAddress.Parse("127::1");
                //Config.ServerPort = 7864;
                //Config.WriteConfig();
                _mainForm.InformationTextLineError("ReadConfig Error...Exit.....");
                Thread.Sleep(5000);
                Program.Exit();
            }
            if (!LocalInfo.Init())
            {
                _mainForm.InformationTextLineError("Load LocalInfo Error...Exit.....");
                Thread.Sleep(5000);
                Program.Exit();
            }

            _timerKeepAlive = new System.Threading.Timer(KeepAliveTimerThread, null, 0, 30 * 1000);

            Program.CM = new CommandModule();


            _mainForm.InformationTextLineInfo("Mac Address:");
            foreach (var s in LocalInfo.MacAddr)
            {
                _mainForm.InformationTextLineInfo(s);
            }
            _mainForm.InformationTextLineInfo("Mac Address:");
            foreach (var s in LocalInfo.GetMacAddressByNetworkInformation())
            {
                _mainForm.InformationTextLineInfo(s);
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

        public static void Exit()
        {
            lock (StopProgramLock)
            {
                StopProgram = true;
            }
            Application.Exit();
        }
    }
}
