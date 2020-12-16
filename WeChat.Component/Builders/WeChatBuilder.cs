using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WeChat.Common.Options;
using WeChat.Component.Providers.WeChat;
using WeChat.Common.Share;

namespace WeChat.Component.Builders
{
    public interface IWeChatBuilder
    {
        IWeChatProvider Build(string router);
    }

    public class WeChatBuilder : IWeChatBuilder
    {
        private IOptions<List<OptionsWeChat>> options { get; set; }
        public WeChatBuilder(IOptions<List<OptionsWeChat>> _options)
        {
            this.options = _options;
        }


        public IWeChatProvider Build(string router)
        {
            var opts = options.Value.SingleOrDefault(s => s.Router.ToLower().TrimEnd(new char[] { '/', '\\' }) == router.ToLower().TrimEnd(new char[] { '/', '\\' }));
            if (opts == null)
                return null;
            AssemblyHelper ass = new AssemblyHelper();
            var provider = (IWeChatProvider)ass.CreateInstance(opts.AssemblyName, opts.ClassName, opts);
            return provider;
        }
    }
}
