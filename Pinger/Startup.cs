using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Pinger
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup()
        {
            Configuration = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("Settings.json")
                       .Build(); 
        }
       
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();               
            });
            loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(),
                                                        "logger.txt"));
           
        }

            public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<FileLogger>(options => Configuration.GetSection("LoggerSettings").Bind(options));
            
        }
    }
}
