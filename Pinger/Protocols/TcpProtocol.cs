using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Pinger.Protocols
{
    public class TCPProtocol : Protocol
    {
        private string TargetHost { get; set; }
        private int TargetPort { get; set; }

        public TCPProtocol(string targetHost)
        {
            this.TargetHost = targetHost.Split(':')[0];
            TargetPort = int.Parse(targetHost.Split(':')[1]);
        }
        public override bool PingHost()
        {
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
