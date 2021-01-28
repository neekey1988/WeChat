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
    public class WebPageController : ControllerBase
    {
        public IHostingEnvironment _hostingEnvironment { get; set; }
        public ILogHelper log { get; set; }
        [HttpGet]
        [Route("authorize")]
        public string Authorize()
        {
            return WebPageAuthorizeHelper.BuildAuthorizeUrl("http://shimiao.ren:8100/WebPage/callback", Summary.E_AuthorizeScope.snsapi_base);
        }

        [HttpGet]
        [Route("callback")]
        public IActionResult callback(string code,string state,string data)
        {
            return Ok( code+"@@@@"+data);
        }
     
    }
}
