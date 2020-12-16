using log4net;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WeChat.Common;

namespace WeChat.Common.Logging
{
    public class Log4NetHelper : ILogHelper
    {
        private static readonly ILoggerRepository Repository = LogManager.CreateRepository("WebCoreLog");

        public static void ConfigureAndWatch(string config)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(Repository, new FileInfo(System.AppDomain.CurrentDomain.BaseDirectory + config));
        }
        /// <summary>
        /// 常规日志
        /// </summary>
        private ILog logbase = log4net.LogManager.GetLogger(Log4NetHelper.Repository.Name, "logbase");
        /// <summary>
        /// xml日志
        /// </summary>
        private ILog logxml = log4net.LogManager.GetLogger(Log4NetHelper.Repository.Name, "logxml");
        /// <summary>
        /// json日志
        /// </summary>
        private ILog logjson = log4net.LogManager.GetLogger(Log4NetHelper.Repository.Name, "logjson");
        /// <summary>
        /// 邮件日志
        /// </summary>
        //private ILog logmail = log4net.LogManager.GetLogger(Log4NetHelper.Repository.Name, "logmail");




        public void Error(string msg = null, Exception ex = null)
        {
            if (logbase == null)
                return;
            logbase.Error(msg, ex);
        }

        public void ErrorAsync(string msg = null, Exception ex = null)
        {
            if (logbase == null)
                return;
            Task.Run(() => logbase.Error(msg, ex));   //异步
        }

        public void Info(string msg = null, Exception ex = null)
        {
            if (logbase == null)
                return;
            logbase.Info(msg, ex);
        }

        public void InfoAsync(string msg = null, Exception ex = null)
        {
            if (logbase == null)
                return;
            Task.Run(() => logbase.Info(msg, ex));   //异步
        }

        public void JsonData(string msg)
        {
            if (logjson == null)
                return;
            logjson.Info(msg);
        }

        public void JsonDataAsync(string msg)
        {
            if (logjson == null)
                return;
            Task.Run(() => logjson.Info(msg));   //异步
        }

        public void XmlData(string msg)
        {
            if (logxml == null)
                return;
            logxml.Info(msg);
        }

        public void XmlDataAsync(string msg)
        {
            if (logxml == null)
                return;
            Task.Run(() => logxml.Info(msg));   //异步
        }

        //public void MailData(string msg)
        //{
        //    if (logmail == null)
        //        return;
        //    logmail.Info(msg);
        //}

        //public void MailDataAsync(string msg)
        //{
        //    if (logmail == null)
        //        return;
        //    Task.Run(() => logmail.Info(msg));   //异步
        //}
    }
}
