using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pinger
{
    internal class Start : IHostedService
    {
        private readonly ILogger<Start> _logger;
        private readonly IConfiguration _configuration;

        public Start(ILogger<Start> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var hostList = _configuration.GetSection("Hosts").Get<List<PingerSettings>>();

            foreach (var hl in hostList)
            {               
                _logger.LogInformation(hl.Host + "\n"
                                           + hl.Protocol + "\n"
                                           + hl.Status + "\n"
                                           + hl.Timeout);

            }
            Console.ReadLine();
            return Task.CompletedTask;
        }
      
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.Log(LogLevel.Information, "Service stopped.");
            return Task.CompletedTask;
        }

    }
}
