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

        [HttpGet]
        [HttpPost]
        public string NewMusic()
        {
            var wx=(M_RequestMessage)HttpContext.Items["M_RequestMessage"];
            return Message.SendTextMessage(wx, "新歌获取测试");
        }

        [HttpGet]
        [HttpPost]
        public string Test()
        {
            var wx=(M_RequestMessage)HttpContext.Items["M_RequestMessage"];
            return Message.SendPicTextMessage(wx, "今日歌曲推荐", "今天点击率最高，播放次数最高的歌曲！",
                   "https://images.ali213.net/photo/M00/5D/B8/5db8b30a808fdff8052f30903f42a27c6212.jpg",
                   "https://0day.ali213.net/html/2011/7429.html");
        } 

        [HttpPost]
        public string subscribe()
        {
            var wx = (M_RequestMessage)HttpContext.Items["M_RequestMessage"];
            return Message.SendTextMessage(wx, "您好，欢迎关注我的公众号");
        }

        [HttpPost]
        public string UserContent()
        {
            var wx = (M_RequestMessage)HttpContext.Items["M_RequestMessage"];
            return Message.SendTextMessage(wx, wx.Content+",已阅");
        }
    }
}
