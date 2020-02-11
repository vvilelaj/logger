using MyLogger.Core;
using MyLogger.Core.Plugin;
using MyLogger.Plugins;
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
            //
            logger.AddPlugin(new SqlServerPlugin());
            logger.AddPlugin(new TextFilePlugin());
            logger.AddPlugin(new ConsolePlugin());
            //
            logger.AddSeverity(Severity.Info);
            logger.AddSeverity(Severity.Warning);
            logger.AddSeverity(Severity.Error);
            //
            logger.LogInfo("test info");
            logger.LogWarning("test warning");
            logger.LogError("test error");
        }
    }
}
