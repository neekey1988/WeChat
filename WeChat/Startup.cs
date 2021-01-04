using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WeChat.Common.Logging;
using WeChatLink.Options;
using WeChatLink.Extensions;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;

namespace WeChat
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //1.添加配置项映射，添加所需的注入
            services.Configure<OptionsWeChat>(Configuration.GetSection("OptionsWeChat"));
            services.AddWeChatLink();
            services.AddControllers()
                .AddControllersAsServices();//属性注入必须加上这个
        }

        /// <summary>
        /// 3.1版本会自动进来，不需要引用
        /// </summary>
        /// <param name="containerBuilder"></param>
        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            Log4NetHelper log = new Log4NetHelper();

            //获取所有控制器类型并使用属性注入
            var controllerBaseType = typeof(ControllerBase);
            containerBuilder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType)
                .PropertiesAutowired();
            //log注入
            containerBuilder.Register<ILogHelper>((x)=> {
                return new Log4NetHelper();
            }).PropertiesAutowired().InstancePerLifetimeScope();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseHttpsRedirection();

            //2.要放到UseRouting之前，core3.0中端点路由优先级最高，一旦命中端点路由，其他中间件不会触发，其他中间件指的是在UseRouting和UseEndpoints之间的中间件
            app.UseWeChat();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //3.动态路由，映射地址要和appsettings中获取微信配置接口的路由地址一致
                endpoints.MapDynamicControllerRoute<WeChatLink.Transformer.WeChatLinkTranslationTransformer>("wechat"); 
            });
        }
    }
}
