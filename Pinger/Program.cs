using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pinger.Protocols;
using System;
using System.Collections.Generic;
using System.IO;


namespace Pinger
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureHostConfiguration(configHost =>
            {
                configHost.SetBasePath(Directory.GetCurrentDirectory());
                configHost.AddJsonFile("Settings.json");
            })
                .ConfigureLogging(builder =>
                    builder.ClearProviders()
                        .AddFileLogger(new FileLoggerConfiguration
                        {
                            LogLevel = LogLevel.None,
                            Path = "log.txt"
                        }).AddConsole())
                .ConfigureServices((hostContext, services) =>
                {
                    var config = new AutoMapperConfiguration().Configure();
                    var mapper = config.CreateMapper();

                    var list = new List<BasePingSettings>();
                    var hostList = hostContext.Configuration.GetSection("Hosts").Get<List<PingerSettings>>();
                    foreach (var hl in hostList)
                    {
                        if (hl.Protocol == "TCP")
                        {
                            list.Add(mapper.Map<PingerSettings, TcpPingSettings>(hl));
                        }
                        else if (hl.Protocol == "ICMP")
                        {
                            list.Add(mapper.Map<PingerSettings, IcmpPingSettings>(hl));
                        }
                        else if (hl.Protocol == "HTTP")
                        {
                            list.Add(mapper.Map<PingerSettings, HttpPingSettings>(hl));
                        }
                    }

                    services.AddSingleton(list);
                    services.AddSingleton(mapper);
                    services.AddScoped<Pinger<HttpPingSettings>>();
                    services.AddScoped<Pinger<TcpPingSettings>>();
                    services.AddScoped<Pinger<IcmpPingSettings>>();
                    services.AddScoped<PingEngine>();
                    services.AddScoped<HttpPingEngine>();
                    services.AddScoped<TcpPingEngine>();
                    services.AddScoped<IcmpPingEngine>();
                    services.AddTransient<FileLogger>();
                    services.AddHostedService<Start>();
                });
    }
}