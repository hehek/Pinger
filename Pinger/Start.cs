using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Pinger.Protocols;

namespace Pinger
{
    internal class Start : IHostedService
    {
        private readonly ILogger<Start> _logger;
        private List<BasePingSettings> _pingSettingsList;
        private readonly IServiceProvider _serviceProvider;

        public Start(ILogger<Start> logger, List<BasePingSettings> pingSettingsList, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _pingSettingsList = pingSettingsList;
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            foreach (var hl in _pingSettingsList)
            {


                if (hl is HttpPingSettings httpPS)
                {
                    _logger.LogInformation(httpPS.Host + "\n"
                                                   + httpPS.Protocol + "\n"
                                                   + httpPS.Status + "\n"
                                                   + httpPS.Timeout);
                    var pinger = _serviceProvider.GetService<Pinger<HttpPingSettings>>();
                    pinger.Start(httpPS);

                }
                else if (hl is TcpPingSettings tcpPS)
                {
                    _logger.LogInformation(tcpPS.Host + "\n"
                                                   + tcpPS.Protocol + "\n"
                                                   + tcpPS.Port + "\n"
                                                   + tcpPS.Timeout);
                    var pinger = _serviceProvider.GetService<Pinger<TcpPingSettings>>();
                    pinger.Start(tcpPS);
                }
                else if (hl is IcmpPingSettings icmpPS)
                {
                    _logger.LogInformation(icmpPS.Host + "\n"
                                                   + icmpPS.Protocol + "\n"
                                                   + icmpPS.Timeout);
                    var pinger = _serviceProvider.GetService<Pinger<IcmpPingSettings>>();
                    pinger.Start(icmpPS);
                }
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.Log(LogLevel.Information, "Service stopped.");
            return Task.CompletedTask;
        }
    }

}
