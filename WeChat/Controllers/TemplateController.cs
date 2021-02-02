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
    public class TemplateController : ControllerBase
    {
        public IHostingEnvironment _hostingEnvironment { get; set; }
        public ILogHelper log { get; set; }
        [HttpGet]
        [Route("set")]
        public string SetTemplate()
        {
            return TemplateHelper.SetIndustryAsync(2,10).Result.Data;
        }
        [HttpGet]
        [Route("get")]
        public string GetTemplate()
        {
            return TemplateHelper.GetIndustryAsync().Result.Data;
        }
        [HttpGet]
        [Route("getlist")]
        public string GetTemplateList()
        {
            return TemplateHelper.GetTemplateListAsync().Result.Data;
        }

        [HttpGet]
        [Route("del")]
        public string DelTemplateList(string id)
        {
            return TemplateHelper.DeleteTemplateAsync(id).Result.Data.errmsg;
        }

        [HttpPost]
        [Route("Finish")]
        public string Finish(M_EventTemplateSendJobFinish wx)
        {
            Console.WriteLine(wx.MsgID+":"+wx.Status);
            return MessageHelper.SendTextMessage(wx, wx.Status+",已阅");
        }
        [HttpPost]
        [Route("batchfinish")]
        public string BatchFinish(M_EventMASSSENDJOBFINISH wx)
        {
            Console.WriteLine(wx.MsgID+":"+wx.Status);
            return MessageHelper.SendTextMessage(wx, wx.Status+",已阅");
        }

        [HttpPost]
        [Route("location")]
        public IActionResult Location(M_EventLOCATION wx)
        {
            Console.WriteLine(wx.Latitude+":"+wx.Longitude);
            return Ok("success");
        }
    }
}
