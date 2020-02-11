using MyLogger.Core;
using MyLogger.Plugins.DbPlugins;
using MyLogger.Plugins.FileSystemPlugins;
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
            logger.AddPlugin(new TextFilePlugin());
            logger.AddSeverity(MyLogger.Core.Plugin.Severity.Info);
            logger.LogInfo("test 01");
        }
    }
}
