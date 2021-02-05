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
                     services.AddTransient<Pinger>();
                     services.AddTransient<FileLogger>();
                     services.AddHostedService<Start>();
                 });

        
    }
}