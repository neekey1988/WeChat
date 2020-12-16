using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeChat.Common.Options;
using WeChat.Component.Providers.WeChat;
using WeChat.Component.Builders;

namespace WeChat.Component.Middlewares
{
    public class MiddlewaresWeChat
    {
        private RequestDelegate next;
        private IHostingEnvironment hostingEnv;

        public MiddlewaresWeChat(RequestDelegate _next, IHostingEnvironment _hostingEnv)
        {
            this.next = _next;
            this.hostingEnv = _hostingEnv;
        }

        public async Task Invoke(HttpContext context, IOptionsSnapshot<List<OptionsWeChat>> options)
        {
            IWeChatProvider wechatProvider = new WeChatBuilder(options).Build(context.Request.Path.Value);
            if (wechatProvider == null)
            {
                await this.next(context);
                return;
            }
            if (context.Request.Query["signature"].Count == 0 || context.Request.Query["timestamp"].Count == 0 || context.Request.Query["nonce"].Count == 0)
            {
                context.Response.StatusCode = 404;
                return;
            }
            wechatProvider.options.signature = context.Request.Query["signature"][0];
            wechatProvider.options.timestamp = context.Request.Query["timestamp"][0];
            wechatProvider.options.nonce = context.Request.Query["nonce"][0];
            wechatProvider.CheckSignature(hostingEnv, out dynamic result);

            return;
        }
    }
}
