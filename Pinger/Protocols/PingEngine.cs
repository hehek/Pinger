using AutoMapper.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pinger.Protocols
{
    public class PingEngine
    {
        private ILogger<PingEngine> _logger;

        public PingEngine(ILogger<PingEngine> logger)
        {
            _logger = logger;
        }

        internal bool Ping<T>(BasePingSettings pingSettings)
        {
            if (pingSettings is HttpPingSettings httpPS)
            {
                HttpPingEngine httpPingEngine = new HttpPingEngine(_logger);
                return httpPingEngine.Ping(httpPS);
            }
            else if(pingSettings is TcpPingSettings tcpPS)
            {
                TcpPingEngine tcpPingEngine = new TcpPingEngine(_logger);
                return tcpPingEngine.Ping(tcpPS);
            }
            else if(pingSettings is IcmpPingSettings icmpPS)
            {
                IcmpPingEngine icmpPingEngine = new IcmpPingEngine(_logger);
                return icmpPingEngine.Ping(icmpPS);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}