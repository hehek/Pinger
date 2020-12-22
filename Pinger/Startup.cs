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
using Microsoft.Extensions.Logging.Console;
namespace Pinger
{
    class Startup
    {
        public IConfiguration Configuration { get; set; }
      

        public Startup(IConfiguration configuration)
        {            
            Configuration = new ConfigurationBuilder()
                     .AddJsonFile("appsettings.json")
                     .Build();
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
                                                            ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(),
                                                        "logger.txt"));
        }



            public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<FileLogger>(options => Configuration.GetSection("LoggerSettings").Bind(options));
            
        }
    }
}
