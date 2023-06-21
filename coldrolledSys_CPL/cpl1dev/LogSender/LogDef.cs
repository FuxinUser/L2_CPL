using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogSender
{
    public static class LogDef
    {

        public readonly static string ERROR = "1";
        public readonly static string ALARM = "2";
        public readonly static string INFO = "3";
        public readonly static string DEBUG = "4";

        public readonly static string SysServer = "1";      //系統編號,Server
        public readonly static string SysClient = "2";      //系統編號,Client

        
    }
}
