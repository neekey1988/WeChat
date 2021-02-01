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
            var filePath = string.Format("/Images/result.jpg");
            //获取当前web目录
            var webRootPath = _hostingEnvironment.ContentRootPath+filePath;
            Task<M_APIResult<string>> result;
            using(FileStream fs=new FileStream(webRootPath,FileMode.Open, FileAccess.Read))
            {
                result= MateriaHelper.PostThumbAsync(fs,Summary.E_MaterialTime.Temporary);
            }
            return result.Result.Data;
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
            var result= MateriaHelper.PostNewsAsync(entity);
            return result.Result.Data;
        }

        [HttpGet]
        [Route("batch")]
        public string BatchNews()
        {
            M_Articles entity = new M_Articles();
            entity.articles = new List<M_MaterialNews>();
            entity.articles.Add(new M_MaterialNews()
            {
                author = "neekey",
                content = "test",
                content_source_url = "www.baidu.com",
                title = "图文消息测试",
                show_cover_pic = 1,
                digest = "",
                need_open_comment = 0,
                only_fans_can_comment = 0,
                thumb_media_id = "uc_4UZM-fWBuUlHiXsWTMDnOzUDuwJ4lECSSJqgO7qA6WhGEfeIUROTs1KtCi5NI"
            });
            var result = MateriaHelper.PostBatchNewsAsync(entity);
            return result.Result.Data;
        }

        [HttpGet]
        [Route("getlist")]
        public string GetMaterialsList()
        {
            string count = MateriaHelper.GetMaterialCount().Result.Data;
            return MateriaHelper.GetMaterialList( Summary.E_MaterialType.Image,0,20).Result.Data;
        }
        [HttpGet]
        [Route("getpm")]
        public IActionResult GetPermanentMaterials()
        {
            var result= MateriaHelper.GetPermanentMaterialAsync("9esnqWhnAq2hOWtSkGD37eETYh7xL8AGrrS1DPisU5Y").Result;//9esnqWhnAq2hOWtSkGD37eETYh7xL8AGrrS1DPisU5Y
            if (result.file != null)
                return File(result.file, "application/octet-stream", result.message);
            else
                return Ok(result.message);
        }
        [HttpGet]
        [Route("gettp")]
        public IActionResult GetTempMaterials()
        {
            var result = MateriaHelper.GetTemporaryMaterialAsync("SCgwH9dChZ7BBw1ZT-glla6ZFYJg5BHKKXKf4_UkxEhTH6FSTagsC0RwL12KY5fr").Result;
            if (result.file != null)
                return File(result.file, "application/octet-stream", result.message);
            else
                return Ok(result.message);
        }

        [HttpGet]
        [Route("clear")]
        public IActionResult ClearQuota()
        {
            var result = APIHelper.ClearApiQuotaAsync().Result;
            return Ok(result.Data);
        }
        [HttpGet]
        [Route("auto")]
        public IActionResult GetAutoreplyInfo()
        {
            var result =MessageHelper.GetAutoreplyInfoAsync().Result;
            return Ok(result.Data);
        }
        [HttpGet]
        [Route("getspeed")]
        public IActionResult GetSpeed()
        {
            var result = BatchMessageHelper.GetBatchSpeed().Result;
            return Ok(result.batchspeed.realspeed);
        }
        [HttpGet]
        [Route("update")]
        public IActionResult PostUpdateNewsAsync()
        {
            var entity= new M_MaterialNews(){
                author = "neekey2",
                content = "test2",
                content_source_url = "www.qq.com",
                title = "图文消息测试",
                show_cover_pic = 1,
                digest = "",
                need_open_comment = 1,
                only_fans_can_comment = 1,
                thumb_media_id = "9esnqWhnAq2hOWtSkGD37V6uq4-1TAvxjNDrqy9NY2M"
            };
            var result = MateriaHelper.PostUpdateNewsAsync("9esnqWhnAq2hOWtSkGD37eETYh7xL8AGrrS1DPisU5Y", entity).Result;
            return Ok(result.Data);
        }
        [HttpGet]
        [Route("delpm")]
        public IActionResult DelPermanentMaterials()
        {
            var result = MateriaHelper.PostDeleteMaterialAsync("9esnqWhnAq2hOWtSkGD37U9DSPhaLwWTf3eE-ehcrgo").Result;//9esnqWhnAq2hOWtSkGD37eETYh7xL8AGrrS1DPisU5Y
            return Ok(result.Data);
        }
        [HttpPost]
        public string NewMusic()
        {
            var wx=(M_MessageBase)HttpContext.Items["M_RequestMessage"];
            return MessageHelper.SendTextMessage(wx, "新歌获取测试");
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

            return MessageHelper.SendNewsMessage(wx, list);
        } 

        [HttpPost]
        public string subscribe()
        {
            var wx = (M_MessageBase)HttpContext.Items["M_RequestMessage"];
            return MessageHelper.SendTextMessage(wx, "您好，欢迎关注我的公众号");
        }

        [HttpPost]
        public string UserContent()
        {
            var wx = (M_StandardText)HttpContext.Items["M_RequestMessage"];
            if (wx.Content.Contains("image"))
            {
                return MessageHelper.SendImageMessage(wx, "9esnqWhnAq2hOWtSkGD37V6uq4-1TAvxjNDrqy9NY2M");
            }
            if (wx.Content.Contains("music"))
            {
                return MessageHelper.SendMusicMessage(wx, "测试音乐","这是一个测试音乐", "http://downsc.chinaz.net/Files/DownLoad/sound1/201906/11582.mp3", "http://downsc.chinaz.net/Files/DownLoad/sound1/201906/11582.mp3", "9esnqWhnAq2hOWtSkGD37V6uq4-1TAvxjNDrqy9NY2M");
            }
            if (wx.Content.Contains("temp")) 
            {
                M_MessageTemplate entity = new M_MessageTemplate() {
                    touser = wx.FromUserName, template_id = "zr3dBAVZrCcsahxZcR3RHNWqIxwJ8s-TedCOWTBGxyk",url="www.baidu.com",
                    data = new { msg = new { value="111111",color= "#173177" } }
                };
                TemplateHelper.SendTemplateAsync(entity);
                return MessageHelper.SendTextMessage(wx, wx.Content + ",已阅");
            }
            return MessageHelper.SendTextMessage(wx, wx.Content+",已阅");
        }

        [HttpPost]
        public string Image()
        {
            var wx = (M_StandardImage)HttpContext.Items["M_RequestMessage"];
            return MessageHelper.SendImageMessage(wx, "9esnqWhnAq2hOWtSkGD37V6uq4-1TAvxjNDrqy9NY2M");
        }

        [HttpPost]
        public string Location()
        {
            var wx = (M_StandardLocation)HttpContext.Items["M_RequestMessage"];
            return MessageHelper.SendTextMessage(wx,$"x:{wx.Location_X},y:{wx.Location_Y},Scale:{wx.Scale},Label:{wx.Label}");
        }
    }
}
