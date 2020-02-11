using MyLogger.Core.Plugin;
using MyLogger.Shared;
using MyLogger.Shared.ConfigManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogger.Plugins.FileSystemPlugins
{
    public class TextFilePlugin : IPlugin
    {
        private readonly FsParams fsParams;
        private readonly IConfigManager configManager;

        public TextFilePlugin() : this(new ConfigManager())
        {

        }

        public TextFilePlugin(FsParams fsParams)
        {
            ParameterValidator.ThowExceptionWhenIsNull(fsParams, "fsParams", "TextFilePlugin");

            ParameterValidator.ThowExceptionWhenIsNullOrEmpty(fsParams.LogPath, "fsParams.LogPath", "TextFilePlugin");

            ParameterValidator.ThowExceptionWhenIsNullOrEmpty(fsParams.LogName, "fsParams.LogName", "TextFilePlugin");

            ParameterValidator.ThowExceptionWhenIsNullOrEmpty(fsParams.LogExtension, "fsParams.LogExtension", "TextFilePlugin");

            this.fsParams = fsParams;
        }

        public TextFilePlugin(IConfigManager configManager)
        {
            ParameterValidator.ThowExceptionWhenIsNull(configManager, "configManager", "TextFilePlugin");

            this.configManager = configManager;

            fsParams = LoadFsParamsFromConfig();
        }

        private FsParams LoadFsParamsFromConfig()
        {
            ParameterValidator.ThowExceptionWhenIsNullOrEmpty(configManager.LogPath, "configManager.LogPath", "TextFilePlugin");

            ParameterValidator.ThowExceptionWhenIsNullOrEmpty(configManager.LogName, "configManager.LogName", "TextFilePlugin");

            ParameterValidator.ThowExceptionWhenIsNullOrEmpty(configManager.LogExtension, "configManager.LogExtension", "TextFilePlugin");

            return new FsParams
            {
                LogPath = this.configManager.LogPath,
                LogName = this.configManager.LogName,
                LogExtension = this.configManager.LogExtension,
            };
        }

        public void Log(DateTime date, Severity severity, string message)
        {
            try
            {
                var dateStr = DateTime.Now.ToShortDateString();
                var logName = $"{dateStr}_{this.fsParams.LogName}{this.fsParams.LogExtension}";
                var logPath = Path.Combine(this.fsParams.LogPath, logName);
                var logMessage = $"{date.ToString()} - {severity.ToString()} : {message}";
                using (var sw = new StreamWriter(logPath, true))
                {
                    sw.WriteLine(logMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }
    }
}
