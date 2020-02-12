using MyLogger.Core.Plugin;
using MyLogger.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogger.Core
{
    public class Logger
    {
        private readonly Dictionary<string, IPlugin> plugins;
        private readonly List<Severity> severities;

        public int PluginsCount
        {
            get
            {
                return plugins.Count;
            }
        }

        public int SeveritiesCount
        {
            get
            {
                return severities.Count;
            }
        }

        public Logger()
        {
            plugins = new Dictionary<string, IPlugin>();
            severities = new List<Severity>();
        }

        //public Logger(List<IPlugin> plugins)
        //{
        //    if (plugins == null || plugins.Count == 0) throw new ArgumentNullException("plugins", "Logger : plugins are null or empty.");

        //    this.plugins = plugins;
        //}

        public Logger(List<Severity> severities)
        {
            if (severities == null || severities.Count == 0) throw new ArgumentNullException("severities", "Logger : severities are null or empty.");

            this.severities = severities;
        }

        public void LogWarning(string message)
        {
            if (!this.severities.Any(x => x == Severity.Warning)) return;

            ParameterValidator.ThowExceptionWhenIsNullOrEmpty(message, "message", "Logger.LogWarning");
            var date = DateTime.Now;
            foreach (var p in plugins)
            {
                p.Value.Log(DateTime.Now, Severity.Warning, message);
            }
        }

        public void LogInfo(string message)
        {
            if (!this.severities.Any(x => x == Severity.Info)) return;

            ParameterValidator.ThowExceptionWhenIsNullOrEmpty(message, "message", "Logger.LogInfo");
            var date = DateTime.Now;
            foreach (var p in plugins)
            {
                p.Value.Log(date, Severity.Info, message);
            }
        }

        public void LogError(string message)
        {
            if (!this.severities.Any(x => x == Severity.Error)) return;

            ParameterValidator.ThowExceptionWhenIsNullOrEmpty(message, "message", "Logger.LogError");
            var date = DateTime.Now;
            foreach (var p in plugins)
            {
                p.Value.Log(date, Severity.Error, message);
            }
        }

        public void AddPlugin(string key, IPlugin plugin)
        {
            ParameterValidator.ThowExceptionWhenIsNullOrEmpty(key, "key", "Logger.AddPlugin");

            ParameterValidator.ThowExceptionWhenIsNull(plugin, "plugin", "Logger.AddPlugin");

            if (this.plugins.ContainsKey(key)) throw new ArgumentException("Logger.AddPlugin : key is duplicated.", "key");

            this.plugins.Add(key, plugin);
        }

        public void RemovePlugin(string key)
        {
            ParameterValidator.ThowExceptionWhenIsNullOrEmpty(key, "key", "Logger.AddPlugin");

            if (this.plugins.ContainsKey(key))
            {
                this.plugins.Remove(key);
            }
        }

        public void AddSeverity(Severity severity)
        {
            if (!this.severities.Any(x => x == severity))
            {
                this.severities.Add(severity);
            }
        }

        public void RemoveSeverity(Severity severity)
        {
            if (this.severities.Any(x => x == severity))
            {
                this.severities.Remove(severity);
            }
        }
    }
}
