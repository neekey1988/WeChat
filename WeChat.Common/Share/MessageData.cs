using System;
using System.Collections.Generic;
using System.Text;

namespace WeChat.Common.Share
{
    /// <summary>
    /// 统一消息返回类
    /// </summary>
    public class MessageData
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 返回状态
        /// </summary>
        public bool status { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 响应数据
        /// </summary>
        public object data { get; set; }
    }
}
