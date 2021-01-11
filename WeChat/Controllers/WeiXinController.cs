using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using WeChat.Common.Logging;
using WeChatLink;
using WeChatLink.Common;
using WeChatLink.Model;

namespace WeChat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeiXinController : ControllerBase
    {
        public IHostingEnvironment _hostingEnvironment { get; set; }
        public ILogHelper log { get; set; }
        [HttpGet]
        [Route("index")]
        public string Index()
        {
            var filePath = string.Format("/Images/redducati.PNG");
            //获取当前web目录
            var webRootPath = _hostingEnvironment.ContentRootPath+filePath;
            Task<(bool state,string message)> result;
            using(FileStream fs=new FileStream(webRootPath,FileMode.Open, FileAccess.Read))
            {
                result=HttpHelper.PostImageAsync(fs,Summary.E_MaterialTime.Permanent);
            }
            return result.Result.message;
        }

        [HttpGet]
        [Route("index2")]
        public string Index2()
        {
            M_Articles entity = new M_Articles();
            entity.articles = new List<M_MaterialNews>();
            entity.articles.Add(new M_MaterialNews() { 
             author="neekey", content="test", content_source_url="www.baidu.com", title="图文消息测试", show_cover_pic=1, digest="", 
                need_open_comment=0, only_fans_can_comment=0, thumb_media_id= "9esnqWhnAq2hOWtSkGD37V6uq4-1TAvxjNDrqy9NY2M"
            });
            var result=HttpHelper.PostNewsAsync(entity);
            return result.Result.message;
        }

        [HttpPost]
        public string NewMusic()
        {
            var wx=(M_MessageBase)HttpContext.Items["M_RequestMessage"];
            return Message.SendTextMessage(wx, "新歌获取测试");
        }

        [HttpPost]
        public string Test()
        {
            var wx=(M_MessageBase)HttpContext.Items["M_RequestMessage"];
            List<M_MessageNews> list = new List<M_MessageNews>();
            list.Add(new M_MessageNews()
            {
                Title = "今日歌曲推荐", Description= "今天点击率最高，播放次数最高的歌曲！",
                PicUrl= "https://images.ali213.net/photo/M00/5D/B8/5db8b30a808fdff8052f30903f42a27c6212.jpg",
                Url= "https://0day.ali213.net/html/2011/7429.html"
            }); ;

            return Message.SendNewsMessage(wx, list);
        } 

        [HttpPost]
        public string subscribe()
        {
            var wx = (M_MessageBase)HttpContext.Items["M_RequestMessage"];
            return Message.SendTextMessage(wx, "您好，欢迎关注我的公众号");
        }

        [HttpPost]
        public string UserContent()
        {
            var wx = (M_StandardText)HttpContext.Items["M_RequestMessage"];
            if (wx.Content.Contains("image"))
            {
                return Message.SendImageMessage(wx, "9esnqWhnAq2hOWtSkGD37V6uq4-1TAvxjNDrqy9NY2M");
            }
            if (wx.Content.Contains("music"))
            {
                return Message.SendMusicMessage(wx, "测试音乐","这是一个测试音乐", "http://downsc.chinaz.net/Files/DownLoad/sound1/201906/11582.mp3","", "9esnqWhnAq2hOWtSkGD37V6uq4-1TAvxjNDrqy9NY2M");
            }
            return Message.SendTextMessage(wx, wx.Content+",已阅");
        }

        [HttpPost]
        public string Image()
        {
            var wx = (M_StandardImage)HttpContext.Items["M_RequestMessage"];
            return Message.SendImageMessage(wx, "9esnqWhnAq2hOWtSkGD37V6uq4-1TAvxjNDrqy9NY2M");
        }

        [HttpPost]
        public string Location()
        {
            var wx = (M_StandardLocation)HttpContext.Items["M_RequestMessage"];
            return Message.SendTextMessage(wx,$"x:{wx.Location_X},y:{wx.Location_Y},Scale:{wx.Scale},Label:{wx.Label}");
        }
    }
}
