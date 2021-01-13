﻿using Microsoft.Extensions.Configuration;
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

            var configuration = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("Settings.json")
                       .Build();
            var hostList = configuration.GetSection("Hosts").Get<List<PingerSettings>>();

            foreach (var hl in hostList)
            {
                logger.LogInformation(hl.Host + "\n"
                                           + hl.Protocol + "\n"
                                           + hl.Status + "\n"
                                           + hl.Timeout, "arg");

            }
            Console.ReadLine();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args);
                }
    }
}