using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;


namespace Pinger.Protocols
{
    public class PingEngine
    {
        private readonly IServiceProvider _serviceProvider;

        public PingEngine(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        internal bool Ping<T>(BasePingSettings pingSettings)
        {
            switch (pingSettings)
            {
                case HttpPingSettings httpPs:
                {
                    var httpPingEngine = _serviceProvider.GetService<HttpPingEngine>();
                    return httpPingEngine != null && httpPingEngine.Ping(httpPs);
                }
                case TcpPingSettings tcpPs:
                {
                    var tcpPingEngine = _serviceProvider.GetService<TcpPingEngine>();
                    return tcpPingEngine != null && tcpPingEngine.Ping(tcpPs);
                }
                case IcmpPingSettings icmpPs:
                {
                    var icmpPingEngine = _serviceProvider.GetService<IcmpPingEngine>();
                    return icmpPingEngine != null && icmpPingEngine.Ping(icmpPs);
                }
                default:
                    throw new NotImplementedException();
            }
        }
    }
}