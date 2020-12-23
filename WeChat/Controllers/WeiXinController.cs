using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeChatLink.Common;
using WeChatLink.Model;

namespace WeChat.Controllers.Logging
{
    [ApiController]
    [Route("[controller]")]
    public class WeiXinController : ControllerBase
    {
        [HttpGet]
        public RedirectResult Index()
        {
            return Redirect("/menu");
        }

        [HttpPost]
        public string NewMusic()
        {
            var wx=(M_RequestMessage)HttpContext.Items["M_RequestMessage"];
            return Message.SendTextMessage(wx, "您好，欢迎关注任晨光的公众号");
        }
    }
}
