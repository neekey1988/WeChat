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
            return TemplateHelper.SetIndustryAsync(2,10).Result.message;
        }
        [HttpGet]
        [Route("get")]
        public string GetTemplate()
        {
            return TemplateHelper.GetIndustryAsync().Result.message;
        }
        [HttpGet]
        [Route("getlist")]
        public string GetTemplateList()
        {
            return TemplateHelper.GetTemplateListAsync().Result.message;
        }

        [HttpGet]
        [Route("del")]
        public string DelTemplateList(string id)
        {
            return TemplateHelper.DeleteTemplateAsync(id).Result.message;
        }
    }
}
