using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public static class UDP 
{
    static Socket udpserver = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    public static bool udp_Send(string da, string ip, int port)
    {
        try
        {
            //设置服务IP，设置端口号
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ip), port);
            //发送数据

            byte[] b = Utility.strToToHexByte(da);
            udpserver.SendTo(b, b.Length, SocketFlags.None, ipep);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
