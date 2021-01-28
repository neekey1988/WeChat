using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Newtonsoft.Json;
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
        public IActionResult callback(string code,string state, string data)
        {
            var result = JsonConvert.DeserializeObject<M_APIResult<M_AccessToken>>(data);
            if (result.State==false)
            {
                return Ok(result.Error.errmsg);
            }
            if (result.Data.scope.ToLower() == Summary.E_AuthorizeScope.snsapi_userinfo.ToString().ToLower())
            {
                var userinfo=WebPageAuthorizeHelper.GetUserInfo(result.Data.access_token,result.Data.openid).Result;
                return Ok(userinfo.Data.nickname);
            }
            return Ok(result.Data.access_token+"@"+result.Data.scope);
        }
     
    }
}
