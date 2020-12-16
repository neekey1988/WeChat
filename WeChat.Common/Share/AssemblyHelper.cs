using System;
using System.Collections.Generic;
using System.Text;

namespace WeChat.Common.Share
{
    public class AssemblyHelper
    {
        public T CreateInstance<T>(string namespacename, string classname, params object[] args)
        {
            Type type = Type.GetType($"{namespacename}.{classname},{namespacename}");
            return (T)Activator.CreateInstance(type, args);
        }

        public object CreateInstance(string namespacename, string classname, params object[] args)
        {
            Type type = Type.GetType($"{namespacename}.{classname},{namespacename}");
            return Activator.CreateInstance(type, args);
        }
    }
}
