using MyLogger.Core.Plugin;
using MyLogger.Plugins.DbPlugins;
using MyLogger.Plugins.FileSystemPlugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogger.Plugins
{
    public class PluginFactory
    {
        public static IPlugin CreatePlugin(Plugin plugin)
        {
            IPlugin result = null;

            switch (plugin)
            {
                case Plugin.SqlServer:
                    result = new SqlServerPlugin();
                    break;
                case Plugin.TextFile:
                    result = new TextFilePlugin();
                    break;
                case Plugin.Console:
                    result= new ConsolePlugin();
                    break;
            }

            return result;
        }

        public static IPlugin CreatePlugin(DbParams dbParams)
        {
            return new SqlServerPlugin(dbParams);
        }

        public static IPlugin CreatePlugin(FsParams fsParams)
        {
            return new TextFilePlugin(fsParams);
        }

        public static IPlugin CreatePlugin(TextWriter sdtOut, TextWriter stdError)
        {
            return new ConsolePlugin(sdtOut, stdError);
        }
    }
}
