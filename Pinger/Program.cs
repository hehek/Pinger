using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Pinger
{
    class Program
    {
        static void Main(string[] args)
        {

            var configuration = new ConfigurationBuilder().AddJsonFile("Settings.json").Build();
            var hostList = configuration.GetSection("Hosts").Get<HostList>();
            
            Console.ReadLine();
        }

    }
}
