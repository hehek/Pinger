using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Pinger.Protocols
{
    public class TcpPingEngine
    {
        private readonly ILogger<PingEngine> _logger;
        private TcpPingSettings _pingSettings;

        private string TargetHost { get; set; }
        private int TargetPort { get; set; }

        public TcpPingEngine(ILogger<PingEngine> logger) {
            _logger = logger;
        }
        public bool Ping(TcpPingSettings pingerSettings)
        {
            _pingSettings = pingerSettings;
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
            catch (SocketException socketEx)
            {
                _logger.LogError(socketEx.ToString());
            }
            catch (ArgumentNullException)
            {

            }
            catch (NullReferenceException)
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
            LogInfo(conStatus);
            return conStatus;

        }
        private void LogInfo(bool response)
        {
            _logger.LogInformation("{DateTime}  {protocol}: {response}", DateTime.Now, _pingSettings.Protocol, response);
        }

    }
}
