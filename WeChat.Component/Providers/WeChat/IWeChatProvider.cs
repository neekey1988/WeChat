using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using WeChat.Common.Options;

namespace WeChat.Component.Providers.WeChat
{
    public interface IWeChatProvider
    {
        OptionsWeChat options { get; set; }
        bool CheckSignature(IHostingEnvironment hostingEnv, out dynamic result);
    }
}
