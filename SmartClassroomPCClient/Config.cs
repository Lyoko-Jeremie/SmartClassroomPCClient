using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartClassroomPCClient
{
    // 配置文件读写
    public static class Config
    {
        public static IPAddress ServerIp;
        public static int ServerPort;
        public static EndPoint ServerEndPoint;

        public static void WriteConfig()
        {
            IniFile file = new IniFile();
            file.Write("Ip", ServerIp.ToString(), "Server");
            file.Write("Port", ServerPort.ToString(), "Server");
        }
        public static bool ReadConfig()
        {
            bool ok = true;

            IniFile file = new IniFile();

            if (!file.KeyExists("Ip", "Server"))
            {
                ok = false;
                Program.MainForm.InformationTextLineError("Config File No ServerIP.");
            }
            else
            {
                var ip = file.Read("Ip", "Server");
                Program.MainForm.InformationTextLineInfo("Configed ServerIP:" + ip);
                ServerIp = IPAddress.Parse(ip);
                Program.MainForm.InformationTextLineInfo("Configed ServerIP Version:" + ServerIp.AddressFamily);
            }

            if (!file.KeyExists("Port", "Server"))
            {
                ok = false;
                Program.MainForm.InformationTextLineError("Config File No ServerPort.");
            }
            else
            {
                var port = file.Read("Port", "Server");
                Program.MainForm.InformationTextLineInfo("Configed ServerPort:" + port);
                ServerPort = Convert.ToInt32(port);
            }

            ServerEndPoint = new IPEndPoint(ServerIp, ServerPort);

            return ok;
        }
    }
}
