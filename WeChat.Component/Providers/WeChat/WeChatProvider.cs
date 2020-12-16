using Microsoft.AspNetCore.Hosting;
using NETCore.Encrypt;
using System;
using System.Collections.Generic;
using System.Text;
using WeChat.Common.Options;

namespace WeChat.Component.Providers.WeChat
{
    class WeChatProvider : WeChatBaseProvider, IWeChatProvider
    {
        public OptionsWeChat options { get; set; }

        public WeChatProvider(OptionsWeChat _options)
        {
            options = _options;
        }
        public bool CheckSignature(IHostingEnvironment hostingEnv, out dynamic result)
        {
            result = "";
            string[] ArrTmp = { options.token, options.timestamp, options.nonce };
            Array.Sort(ArrTmp);     //字典排序
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = EncryptProvider.Sha1(tmpStr);
            tmpStr = tmpStr.ToLower();
            if (tmpStr == options.signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
