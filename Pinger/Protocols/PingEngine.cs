using AutoMapper.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pinger.Protocols
{
    public class PingEngine
    {

        public PingEngine()
        {

        }

        internal bool Ping<T>(BasePingSettings pingSettings)
        {
            if (pingSettings is HttpPingSettings httpPS)
            {
                HttpPingEngine httpPingEngine = new HttpPingEngine();
                return httpPingEngine.Ping(httpPS);
            }
            else if(pingSettings is TcpPingSettings tcpPS)
            {
                TcpPingEngine tcpPingEngine = new TcpPingEngine();
                return tcpPingEngine.Ping(tcpPS);
            }
            else if(pingSettings is IcmpPingSettings icmpPS)
            {
                IcmpPingEngine icmpPingEngine = new IcmpPingEngine();
                return icmpPingEngine.Ping(icmpPS);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}