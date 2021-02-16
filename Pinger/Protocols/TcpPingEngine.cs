using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Pinger.Protocols
{
    public class TcpPingEngine
    {
        private string TargetHost { get; set; }
        private int TargetPort { get; set; }

        public TcpPingEngine() { }
        public bool Ping(TcpPingSettings pingerSettings)
        {
            TargetHost = pingerSettings.Host;
            TargetPort = pingerSettings.Port;
            bool conStatus;
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            {
                Blocking = true
            };
            try
            {
                socket.Connect(TargetHost, TargetPort);
            }
            catch (SocketException)
            {
            }
            catch (ArgumentNullException)
            {
            }
            finally
            {
                if (socket.Connected)
                {
                    socket.Close();
                    conStatus = true;
                }
                else
                {
                    conStatus = false;
                }
            }
            return conStatus;

        }


    }
}
