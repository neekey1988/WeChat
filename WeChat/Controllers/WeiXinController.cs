using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WeChat.Controllers.Logging
{
    public class WeiXinController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
