using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Generic;
using System.IO;

namespace Pinger
{
    class Program
    {
        static void Main(string[] args)
        {
            ILoggerFactory loggerFactory = new LoggerFactory()
                                               .AddFile(Path.Combine(Directory.GetCurrentDirectory(),
                                                        "logger.txt"));                           
            ILogger logger = loggerFactory.CreateLogger<Program>();
            var configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("Settings.json")
                        .Build();          
            var hostList = configuration.GetSection("Hosts").Get<List<PingerSettings>>();

            foreach(var hl in hostList)
            {
                logger.LogInformation(hl.Host+"\n"
                                           +hl.Protocol+"\n"
                                           +hl.Status+"\n"
                                           +hl.Timeout,"arg");                
            }            
            Console.ReadLine();
        }

    }
}
