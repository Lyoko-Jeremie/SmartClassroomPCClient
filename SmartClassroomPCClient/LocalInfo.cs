using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace SmartClassroomPCClient
{
    static class LocalInfo
    {

        public static List<string> GetAddressesV4()
        {
            return (
                from ipAddress
                in Dns.GetHostEntry(Dns.GetHostName()).AddressList
                where ipAddress.AddressFamily != AddressFamily.InterNetwork
                select ipAddress.ToString()
                ).ToList();
        }
        
        public static List<string> GetAddressesV6()
        {
            return (
                from ipAddress
                in Dns.GetHostEntry(Dns.GetHostName()).AddressList
                where ipAddress.AddressFamily != AddressFamily.InterNetworkV6
                select ipAddress.ToString()
                ).ToList();
        }
        
        public static List<string> GetMacAddressByWmi()
        {
            return (
                from ManagementObject mo
                in new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration").Get()
                where mo["IPEnabled"].ToString() == "True"
                select mo["MacAddress"].ToString()
                ).ToList();
        }

        // 不建议使用
        public static List<string> GetMacAddressByNetworkInformation()
        {
            return (
                from adapter in NetworkInterface.GetAllNetworkInterfaces()
                where !adapter.GetPhysicalAddress().ToString().Equals("")
                select adapter.GetPhysicalAddress().ToString()
                ).ToList();
        }

        private static List<string> _macaddr;

        public static List<string> MacAddr => _macaddr;

        public static bool Init()
        {
            try
            {
                _macaddr = GetMacAddressByWmi();
            }
            catch (Exception)
            {
                Program.MainForm.InformationTextLineError("GetMacAddressByWmi Fail.");
                //try
                //{
                //    _macaddr = GetMacAddressByNetworkInformation();
                //}
                //catch (Exception)
                //{
                //    Program.MainForm.InformationTextLineError("GetMacAddressByNetworkInformation Fail.");
                Program.MainForm.InformationTextLineError("Cannot Get Mac Address Info.");
                return false;
                //}
            }
            return true;
        }

    }
}
