using Microsoft.Extensions.Logging;
using System;
using System.Net.NetworkInformation;

namespace Pinger.Protocols
{
    public class IcmpPingEngine
    {
        private readonly ILogger<PingEngine> _logger;
        private IcmpPingSettings _pingSettings;

        private string TargetHost { get; set; }

        public IcmpPingEngine(ILogger<PingEngine> logger)
        {
            _logger = logger;
        }


        public bool Ping(IcmpPingSettings pingerSettings)
        {
            _pingSettings = pingerSettings;
            TargetHost = pingerSettings.Host;
            var pinger = new Ping();
            try
            {

                var reply = pinger.Send(TargetHost, pingerSettings.Timeout);
                if(reply != null && reply.Status == IPStatus.TimedOut)
                {
                    _logger.LogInformation("{DateTime}  {protocol}: TimedOut", DateTime.Now, _pingSettings.Protocol);
                }
                var response = (reply != null && reply.Status == IPStatus.Success);

                pinger.Dispose();
                LogInfo(response);
                return response ;
            }
            catch (UriFormatException uriEx)
            {
                _logger.LogInformation(uriEx.ToString());
                return false;
            }
            catch (NullReferenceException ex)
            {
                _logger.LogInformation(ex.ToString());
                return false;
            }
            
        }
        private void LogInfo(bool response)
        {
            _logger.LogInformation("{DateTime}  {protocol}: {response}", DateTime.Now, _pingSettings.Protocol, response);
        }

    }
}
