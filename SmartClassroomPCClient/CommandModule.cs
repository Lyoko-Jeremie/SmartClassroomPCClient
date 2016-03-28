using System;
using System.Collections.Generic;
using System.Net;

namespace SmartClassroomPCClient
{
    internal class CommandModule
    {

        public CommandModule()
        {

            // 初始化状态转换表
            _stateMoveTable = new Dictionary<ModeState, Dictionary<ChangeState, ModeState>>();
            foreach (var e in Enum.GetValues(typeof(ModeState)))
            {
                _stateMoveTable[(ModeState)e] = new Dictionary<ChangeState, ModeState>();
            }
            _stateMoveTable[ModeState.Guest][ChangeState.Enable] = ModeState.Enable;
            _stateMoveTable[ModeState.Enable][ChangeState.Admin] = ModeState.Admin;
            _stateMoveTable[ModeState.Enable][ChangeState.Exit] = ModeState.Guest;
            _stateMoveTable[ModeState.Admin][ChangeState.Exit] = ModeState.Enable;

            PrintLine("CommandModule Init End.");
        }

        // 分隔字符
        private static readonly char[] SplitChar = new char[] { ' ' };

        // 当前状态
        private ModeState _nowmode = ModeState.Guest;

        // 有限状态机 状态转换表
        private readonly Dictionary<ModeState, Dictionary<ChangeState, ModeState>> _stateMoveTable;

        private enum ModeState
        {
            Guest,
            Enable,
            Admin
        }

        private enum ChangeState
        {
            Exit,
            Enable,
            Admin
        }

        private ModeState ChangeModeState(ChangeState cs)
        {
            try
            {
                _nowmode = _stateMoveTable[_nowmode][cs];
                PrintLine("Now User Mode Change To : " + _nowmode);
                return _nowmode;
            }
            catch (Exception)
            {
                return _nowmode;
            }
        }

        public void CommandString(string cm)
        {
            if (cm.Length < 3) return;
            var data = cm.Split(SplitChar);
            switch (_nowmode)
            {
                case ModeState.Guest:
                    UserGuest(cm, data);
                    break;
                case ModeState.Enable:
                    UserEnable(cm, data);
                    break;
                case ModeState.Admin:
                    UserAdmin(cm, data);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void PrintLine(string s)
        {
            switch (_nowmode)
            {
                case ModeState.Guest:
                    Program.MainForm.InformationTextLineCmd(s);
                    break;
                case ModeState.Enable:
                    Program.MainForm.InformationTextLineCmd("[Enable]>" + s);
                    break;
                case ModeState.Admin:
                    Program.MainForm.InformationTextLineCmd("[Admin]#" + s);
                    break;
                default:
                    Program.MainForm.InformationTextLineCmd(s);
                    break;
            }
        }

        private void Clear()
        {
            Program.MainForm.ClearInformationText();
        }

        private void UserGuest(string cm, IReadOnlyList<string> data)
        {
            switch (data[0])
            {
                case "clear":
                    Clear();
                    break;
                case "enable":
                    ChangeModeState(ChangeState.Enable);
                    break;
                case "get":
                    Get(cm, data);
                    break;
                case "off":
                    OffTing(cm, data);
                    break;
                case "on":
                    OnTing(cm, data);
                    break;
                default:
                    PrintLine("Unknow Command:" + cm);
                    break;
            }
        }

        private void UserEnable(string cm, IReadOnlyList<string> data)
        {
            switch (data[0])
            {
                case "clear":
                    Clear();
                    break;
                case "exit":
                    ChangeModeState(ChangeState.Exit);
                    break;
                case "admin":
                    ChangeModeState(ChangeState.Admin);
                    break;
                case "set":
                    Set(cm, data);
                    break;
                case "get":
                    Get(cm, data);
                    break;
                default:
                    PrintLine("Unknow Command:" + cm);
                    break;
            }
        }

        private void UserAdmin(string cm, IReadOnlyList<string> data)
        {
            switch (data[0])
            {
                case "clear":
                    Clear();
                    break;
                case "exit":
                    ChangeModeState(ChangeState.Exit);
                    break;
                case "set":
                    Set(cm, data);
                    break;
                case "get":
                    Get(cm, data);
                    break;
                case "saveconfig":
                    Config.WriteConfig();
                    if (!Config.ReadConfig())
                    {
                        PrintLine("Reload Config Failed.");
                        break;
                    }
                    PrintLine("Write Config OK.");
                    break;
                case "reloadconfig":
                    if (!Config.ReadConfig())
                    {
                        PrintLine("Reload Config Failed.");
                        break;
                    }
                    PrintLine("Reload Config OK.");
                    break;
                default:
                    PrintLine("Unknow Command:" + cm);
                    break;
            }
        }

        private void OffTing(string cm, IReadOnlyList<string> data)
        {
            switch (data[1])
            {
                case "KAT":
                    Program.KeepAliveTimerThreadOutput = false;
                    PrintLine("KeepAliveTimerThreadOutput are Off");
                    break;
                default:
                    PrintLine("Unknow Command:" + cm);
                    break;
            }
        }
        private void OnTing(string cm, IReadOnlyList<string> data)
        {
            switch (data[1])
            {
                case "KAT":
                    Program.KeepAliveTimerThreadOutput = true;
                    PrintLine("KeepAliveTimerThreadOutput are On");
                    break;
                default:
                    PrintLine("Unknow Command:" + cm);
                    break;
            }
        }

        private void Set(string cm, IReadOnlyList<string> data)
        {
            switch (_nowmode)
            {
                case ModeState.Enable:
                    PrintLine("Unknow Command:" + cm);
                    return;
                case ModeState.Admin:
                    switch (data[1])
                    {
                        case "ip":
                            try
                            {
                                Config.ServerIp = IPAddress.Parse(data[2]);
                                PrintLine("IP is set to:" + Config.ServerIp);
                            }
                            catch (Exception)
                            {
                                PrintLine("Wrong IP:" + data[2]);
                            }
                            break;
                        case "port":
                            try
                            {
                                Config.ServerPort = int.Parse(data[2]);
                                PrintLine("Port is set to:" + Config.ServerPort);
                            }
                            catch (Exception)
                            {
                                PrintLine("Wrong Port:" + data[2]);
                            }
                            break;
                        default:
                            PrintLine("Unknow Command:" + cm);
                            break;
                    }
                    return;
                case ModeState.Guest:
                default:
                    PrintLine("Unknow Command:" + cm);
                    return;
            }
        }

        private void Get(string cm, IReadOnlyList<string> data)
        {
            switch (data[1])
            {
                default:
                    PrintLine("Unknow Command:" + cm);
                    break;
            }
        }
    }
}
