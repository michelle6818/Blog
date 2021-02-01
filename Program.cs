using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Utilites;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Blog
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

                //Seed data  I am going to write a class and a set of methods that seed info
                //DataService - the name of a Class (aka - A Type)
                //ManageDataAsync - a method inside the class that does a unit of work
                //All ManageData is going to do is call a few other methods ("wrapper method")

                await DataUtility.ManageDataAsync(host);              
                host.Run();         
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
