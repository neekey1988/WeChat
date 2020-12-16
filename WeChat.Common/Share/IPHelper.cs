using System;
using System.Collections.Generic;
using System.Linq;

namespace WeChat.Common.Share
{
    public class IPHelper
    {
        public static string GetNetWork(string ip) {
            //linux不支持IsDnsEligible==true && 
            var a = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces().Select(p => p.GetIPProperties()).ToList();
            var b = a.Where(w=> w.UnicastAddresses.Any(x => x.Address.IsIPv6LinkLocal == false)).SelectMany(p => p.UnicastAddresses).ToList();
            var c = b.Where(p =>p.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork && !System.Net.IPAddress.IsLoopback(p.Address)).ToList();
            var result=c.FirstOrDefault()?.Address.ToString();
            if (!string.IsNullOrEmpty(ip))
            {
                var d = c.FirstOrDefault(w => w.Address.ToString().StartsWith(ip));
                result = d == null ? result : d.Address.ToString();
            }
            return result;
        }
    }
}
