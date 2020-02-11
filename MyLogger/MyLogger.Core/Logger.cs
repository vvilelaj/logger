using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogger.Core
{
    public class Logger
    {
        private List<IPlugin> plugins;

        public Logger(List<IPlugin> plugins)
        {
            if (plugins == null || plugins.Count == 0) throw new ArgumentNullException("plugins", "Logger : plugins are null or empty.");

            this.plugins = plugins;
        }

        public void LogWarning(string message)
        {
            if (string.IsNullOrEmpty(message)) throw new ArgumentNullException("message", "Logger.LogWarning : message is null or empty.");

            foreach (var p in plugins)
            {
                p.Log(DateTime.Now, Severity.Warning, message);
            }
        }

        public void LogInfo(string message)
        {
            if (string.IsNullOrEmpty(message)) throw new ArgumentNullException("message", "Logger.LogInfo : message is null or empty.");

            foreach (var p in plugins)
            {
                p.Log(DateTime.Now, Severity.Info, message);
            }
        }

        public void LogError(string message)
        {
            if (string.IsNullOrEmpty(message)) throw new ArgumentNullException("message", "Logger.LogError : message is null or empty.");

            foreach (var p in plugins)
            {
                p.Log(DateTime.Now, Severity.Error, message);
            }
        }
    }
}
