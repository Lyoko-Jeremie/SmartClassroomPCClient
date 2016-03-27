using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartClassroomPCClient
{

    class ClientSocket
    {

        // 做心跳连接 并从服务器获取指令  
        // 短连接模式  30s一次连接
        public SocketTaskFlag KeepAliveSocket(IPAddress addr, int port)
        {
            Socket socket = new Socket(addr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            socket.ReceiveTimeout = 5000;

            try
            {
                socket.Connect(new IPEndPoint(addr, port));
            }
            catch (Exception)
            {
                return SocketTaskFlag.ConnectFail;
            }



            try
            {
                socket.Send(SocketFlag.KeepAliveBegin);

                // TODO 接收任务
                ReciveFormat(socket, SocketFlag.KeepAliveOk);

                socket.Send(SocketFlag.KeepAliveEnd);
            }
            catch (Exception)
            {
                // Empty  压制所有异常
            }
            finally
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }

            return SocketTaskFlag.TaskNoThing;
        }

        // 接受一个预期的值
        private void ReciveFormat(Socket socket, byte[] flag)
        {
            byte[] buff = { 0x00, 0x00, 0x00, 0x00 };
            int ir = socket.Receive(buff, 4, SocketFlags.None);
            if (ir != 4 || !CompareSocketFlag(buff, flag))
            {
                throw new SocketTaskFlagException(SocketTaskFlag.FormatError);
            }
        }

        private bool CompareSocketFlag(byte[] a, byte[] b)
        {
            for (int i = 0; i != 4; ++i)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }
            return true;
        }

    }

    [Serializable]
    public class SocketTaskFlagException : Exception
    {
        private readonly SocketTaskFlag _stf;

        public SocketTaskFlagException()
        {
        }

        public SocketTaskFlagException(SocketTaskFlag stf)
        {
            _stf = stf;
        }

        public SocketTaskFlagException(string message) : base(message)
        {
        }

        public SocketTaskFlagException(string message, Exception inner) : base(message, inner)
        {
        }

        protected SocketTaskFlagException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

        public override string ToString()
        {
            return base.ToString() + "\r\n" + "SocketTaskFlag:" + _stf;
        }

        public SocketTaskFlag SocketTaskFlag => _stf;
    }



    // 通信标志数组
    public static class SocketFlag
    {
        public static readonly byte[] KeepAliveBegin = { 0x00, 0x00, 0x00, 0x01 };
        public static readonly byte[] KeepAliveOk = { 0x00, 0x00, 0x00, 0x02 };
        public static readonly byte[] KeepAliveEnd = { 0x00, 0x00, 0x00, 0x03 };
        public static readonly byte[] TaskBegin = { 0x00, 0x00, 0x01, 0x01 };
        public static readonly byte[] TaskEnd = { 0x00, 0x00, 0x01, 0x02 };
        public static readonly byte[] TaskNoThing = { 0x00, 0x01, 0x00, 0x00 };
        public static readonly byte[] TaskShutdown = { 0x00, 0x01, 0x00, 0x01 };
        public static readonly byte[] TaskRestart = { 0x00, 0x01, 0x00, 0x02 };
        public static readonly byte[] TaskLogout = { 0x00, 0x01, 0x00, 0x03 };
    }

    // Socket结果
    public enum SocketTaskFlag
    {
        // 连接失败
        ConnectFail,
        // KeepAlive失败
        KeepAliveFail,
        // 连接被中断
        ConnectAboart,
        // 协议格式错误
        FormatError,
        // 连接且得到的任务
        TaskShutdown,
        TaskRestart,
        TaskLogout,
        TaskNoThing
    }


}
