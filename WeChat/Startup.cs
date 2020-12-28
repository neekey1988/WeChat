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
            services.Configure<List<OptionsWeChat>>(Configuration.GetSection("OptionsWeChat"));
            services.AddSingleton<WeChatLink.Transformer.WeChatLinkTranslationTransformer>();
            services.AddWeChatLink();
            services.AddControllers()
                .AddControllersAsServices();//����ע�����������
        }

        /// <summary>
        /// 3.1�汾���Զ�����������Ҫ����
        /// </summary>
        /// <param name="containerBuilder"></param>
        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            Log4NetHelper log = new Log4NetHelper();

            //��ȡ���п��������Ͳ�ʹ������ע��
            var controllerBaseType = typeof(ControllerBase);
            containerBuilder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType)
                .PropertiesAutowired();
            //logע��
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

            app.UseRouting();
            app.UseWeChat();
            //WeChatLink.Transformer.WeChatLinkTranslationTransformer a = new WeChatLink.Transformer.WeChatLinkTranslationTransformer();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapDynamicControllerRoute<WeChatLink.Transformer.WeChatLinkTranslationTransformer>("/wechat"); 
            });
        }
    }
}
