using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeChat.Common.Logging
{
    public interface ILogHelper
    {
        void Info(string msg = null, Exception ex = null);
        void InfoAsync(string msg = null, Exception ex = null);

        void Error(string msg = null, Exception ex = null);
        void ErrorAsync(string msg = null, Exception ex = null);

        void JsonData(string msg);
        void JsonDataAsync(string msg);

        void XmlData(string msg);
        void XmlDataAsync(string msg);

        //void MailData(string msg);
        //void MailDataAsync(string msg);
    }
}
