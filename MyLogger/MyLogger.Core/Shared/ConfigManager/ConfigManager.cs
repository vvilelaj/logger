using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogger.Shared.ConfigManager
{
    public class ConfigManager : IConfigManager
    {
        public string ServerName => ConfigurationManager.AppSettings.Get("SERVER_NAME");

        public string Database => ConfigurationManager.AppSettings.Get("DATABASE");

        public string UserId => ConfigurationManager.AppSettings.Get("USER_ID");

        public string Password => ConfigurationManager.AppSettings.Get("PASSWORD");
    }
}
