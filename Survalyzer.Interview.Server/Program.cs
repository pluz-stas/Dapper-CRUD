using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Survalyzer.Interview.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("survalyzer.settings.json", optional: false, reloadOnChange: true)
                .Build();

            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(serverOptions =>
                        {
                            serverOptions.AddServerHeader = false; 
                            serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2);
                            serverOptions.Limits.MaxRequestBodySize = long.MaxValue;
                        })
                        .UseConfiguration(config)
                        .UseStartup<Startup>();

                })
                .Build().Run();
        }
    }
}
