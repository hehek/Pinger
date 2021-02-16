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

        internal void Ping<TPingSettings>(TPingSettings pingSettings) where TPingSettings : BasePingSettings
        {
            throw new NotImplementedException();
        }

        public bool Ping(HttpPingSettings pingerSettings)
        {
            HttpPingEngine httpPingEngine = new HttpPingEngine();
            return httpPingEngine.Ping(pingerSettings);

        }
        public bool Ping(TcpPingSettings pingerSettings)
        {
            TcpPingEngine tcpPingEngine = new TcpPingEngine();
            return tcpPingEngine.Ping(pingerSettings);

        }
        public bool Ping(IcmpPingSettings pingerSettings)
        {
            IcmpPingEngine icmpPingEngine = new IcmpPingEngine();
            return icmpPingEngine.Ping(pingerSettings);

        }

        
    }
}
