using Microsoft.Extensions.Configuration;
using System;

namespace Pinger
{
    class Program
    {
        static void Main(string[] args)
        {

            IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .Build();          

            var hostList = config.GetSection("Hosts").Get<HostList>();
           
            Console.ReadLine();
        }
    }
}
