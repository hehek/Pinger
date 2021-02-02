using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
            ILoggerFactory loggerFactory = new LoggerFactory();
            ILogger logger = loggerFactory.CreateLogger<Program>();

            ILogger fileLogger = new FileLogger(new FileLoggerConfiguration {
                LogLevel = LogLevel.Error,
                Path = Path.Combine(Directory.GetCurrentDirectory(),
                                                        "logger.txt")
            });
                       

            var configuration = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("Settings.json")
                       .Build();

            var hostList = configuration.GetSection("Hosts").Get<List<PingerSettings>>();

            foreach (var hl in hostList)
            {
                fileLogger.LogInformation(hl.Host + "\n"
                                           + hl.Protocol + "\n"
                                           + hl.Status + "\n"
                                           + hl.Timeout, "arg"
                                           + DateTime.Today);
                logger.LogInformation(hl.Host + "\n"
                                           + hl.Protocol + "\n"
                                           + hl.Status + "\n"
                                           + hl.Timeout, "arg");

            }
            Console.ReadLine();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
            .ConfigureLogging(builder =>
                builder.ClearProviders()
                .AddFileLogger().AddConsole())            
                .ConfigureServices((hostContext, services) =>
                 {
                     services.AddTransient<Pinger>();
                 });
    }
}