using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeChat.Common.Options
{
    public class OptionsWeChat : IOptions<OptionsWeChat>
    {

        public string token { get; set; }
        public string appid { get; set; }
        public string appsecret { get; set; }
        public string timestamp { get; set; }
        public string nonce { get; set; }
        public string signature { get; set; }
        public string Router { get; set; }
        public string AssemblyName { get; set; }
        public string ClassName { get; set; }
        public OptionsWeChat Value => this;
    }
}
