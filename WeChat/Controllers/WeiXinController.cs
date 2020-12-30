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
            return Message.SendTextMessage(wx, "您好，欢迎关注我的公众号");
        }

        [HttpGet]
        [HttpPost]
        public string Test()
        {
            var wx=(M_RequestMessage)HttpContext.Items["M_RequestMessage"];
            return Message.SendPicTextMessage(wx, "今日歌曲推荐", "今天点击率最高，播放次数最高的歌曲！",
                   "http://musicdata.baidu.com/data2/pic/a5c79cf978eb5302edca415cabf744f1/260983581/260983581.jpg",
                   "http://www.baidu.com/");
        } 
    }
}
