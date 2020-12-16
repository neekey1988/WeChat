using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;
using WeChat.Component.Middlewares;

namespace WeChat.Component.Extensions
{
    public static class MiddlewaresWeChatExtensions
    {
        public static IApplicationBuilder UseWeChat(this IApplicationBuilder app)=> app.UseMiddleware<MiddlewaresWeChat>();
    }
}
