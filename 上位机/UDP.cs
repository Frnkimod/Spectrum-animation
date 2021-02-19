using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace ArduinoAudioPlayer
{
    public class UDP
    {
        private static UdpClient sendUdpClient;
        private static string ip = "127.0.0.1";//目标ip
        public static void SendUdp(double data)
        {
            sendUdpClient = new UdpClient(0);
            byte[] sendbytes = Encoding.Default.GetBytes(data.ToString());
            IPAddress remoteIp = IPAddress.Parse(ip);
            IPEndPoint remoteIPEndPoint = new IPEndPoint(remoteIp, int.Parse("2333"));
            sendUdpClient.Send(sendbytes, sendbytes.Length, remoteIPEndPoint);
            sendUdpClient.Close();
        }
        public static void SendUdpString(String data)
        {
            sendUdpClient = new UdpClient(0);
            byte[] sendbytes = Encoding.Default.GetBytes(data);
            IPAddress remoteIp = IPAddress.Parse(ip);
            IPEndPoint remoteIPEndPoint = new IPEndPoint(remoteIp, int.Parse("2333"));
            sendUdpClient.Send(sendbytes, sendbytes.Length, remoteIPEndPoint);
            sendUdpClient.Close();
        }
    }
}
