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
            logger.AddPlugin("sql", PluginFactory.CreatePlugin(Plugin.SqlServer));
            logger.AddPlugin("tf1", PluginFactory.CreatePlugin(Plugin.TextFile));
            logger.AddPlugin("tf2", PluginFactory.CreatePlugin(new FsParams { LogPath="./", LogName="log-file-02",LogExtension=".log" }));
            logger.AddPlugin("cp", PluginFactory.CreatePlugin(Plugin.Console));
            //
            logger.AddSeverity(Severity.Info);
            logger.AddSeverity(Severity.Warning);
            logger.AddSeverity(Severity.Error);
            //
            logger.LogInfo("test info");
            logger.LogWarning("test warning");
            logger.LogError("test error");
            //
            logger.RemovePlugin("tf2");
            logger.RemoveSeverity(Severity.Error);
            logger.LogInfo("test info 02");
            logger.LogWarning("test warning 02");
            logger.LogError("test error 01");

            //
            logger.AddSeverity(Severity.Error);
            logger.LogInfo("test info 03");
            logger.LogWarning("test warning 03");
            logger.LogError("test error 03");
        }
    }
}
