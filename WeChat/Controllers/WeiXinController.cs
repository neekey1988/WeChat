using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
    }
}
