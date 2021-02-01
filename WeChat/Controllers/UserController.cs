using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeChatLink.Common;
using WeChatLink.Model;

namespace WeChat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        [HttpGet]
        [Route("addtag")]
        public void AddTag()
        {
            var b = UserHelper.GetTagList();
            var a = UserHelper.CreateTag("武汉10").Result;

        }
        [HttpGet]
        [Route("list")]
        public IActionResult TList()
        {
            var b = UserHelper.GetTagList();
            return Json(b.Result.Data.tags);
        }
        [HttpGet]
        [Route("listtaguser")]
        public string TagList()
        {
            var b = UserHelper.GetTagUserList(100, "").Result;
            return b.Data;
        }
        [HttpGet]
        [Route("del")]
        public IActionResult DelTag(int id)
        {
            var b = UserHelper.DeleteTag(id);
            return Json(b.Result.Data.errmsg);
        }
        [HttpGet]
        [Route("update")]
        public IActionResult UpdateTag()
        {
            M_Tag entity = new M_Tag()
            {
                tag = new M_TagInfo()
                {
                    id = 100,
                    name = "aaaaaaaaaaaaaa",
                    count = 100
                }
            };
            var b = UserHelper.UpdateTag(entity);
            return Json(b.Result.Data.errmsg);
        }
        [HttpGet]
        [Route("bind")]
        public IActionResult BindTag()
        {
            var list = new List<string>();
            list.Add("omrPh5m7e1VT1qar7L2aul_LM0Tk");
            var b = UserHelper.BatchUserTagBind(100, list);
            return Json(b.Result.Data.errmsg);
        }


        [HttpGet]
        [Route("users")]
        public string UserList()
        {
            var b = UserHelper.GetUserList("");
            return b.Result.Data;
        }

        [HttpGet]
        [Route("remark")]
        public IActionResult UpdateUserRemark()
        {
            var b = UserHelper.UpdateUserRemark("omrPh5m7e1VT1qar7L2aul_LM0Tk", "neekey");
            return Json(b.Result.Data.errmsg);
        }

        [HttpGet]
        [Route("userinfo")]
        public JsonResult GetUserInfo()
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };
            var b = UserHelper.GetUserInfo("omrPh5m7e1VT1qar7L2aul_LM0Tk").Result;
            return Json(b.Data, options);
        }

        [HttpGet]
        [Route("userinfos")]
        public JsonResult GetUserList()
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };
            Dictionary<string, string> dicts = new Dictionary<string, string>();
            dicts.Add("omrPh5m7e1VT1qar7L2aul_LM0Tk", "zh_CN");
            var b = UserHelper.GetUserList(dicts).Result;
            return Json(b.Data, options);
        }

        [HttpGet]
        [Route("bbu")]
        public JsonResult BatchBlackUser()
        {
            List<string> list = new List<string>();
            list.Add("omrPh5m7e1VT1qar7L2aul_LM0Tk");
            var b = UserHelper.BatchBlackUser(list).Result;
            return Json(b.Data);
        }

        [HttpGet]
        [Route("gbl")]
        public string GetBlackList()
        {
            List<string> list = new List<string>();
            list.Add("omrPh5m7e1VT1qar7L2aul_LM0Tk");
            var b = UserHelper.GetBlackList().Result;
            return b.Data;
        }
        [HttpGet]
        [Route("bubu")]
        public JsonResult BatchUnBlackUser()
        {
            List<string> list = new List<string>();
            list.Add("omrPh5m7e1VT1qar7L2aul_LM0Tk");
            var b = UserHelper.BatchUnBlackUser(list).Result;
            return Json(b.Data);
        }
    }
}
