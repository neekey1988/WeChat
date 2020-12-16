using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WeChat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Common.Logging.Log4NetHelper.ConfigureAndWatch("Log4net.config");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureLogging((hostContext, logging) =>
            {
                //aspnetcore的log组件是在build中添加的，添加之后会输出很多无用信息，所以在build之前把这些信息过滤掉
                //logging.AddFilter("System", LogLevel.Warning);
                //logging.AddFilter("Microsoft", LogLevel.Warning);
                //logging.ClearProviders();
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .UseServiceProviderFactory(new AutofacServiceProviderFactory());
}
}
