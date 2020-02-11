using MyLogger.Core;
using MyLogger.Plugins.DbPlugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new Logger();
            logger.AddPlugin(new SqlServerPlugin());
            logger.AddSeverity(MyLogger.Core.Plugin.Severity.Info);
            logger.LogInfo("test 01");
        }
    }
}
