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
        private readonly string SERVER_NAME = "SERVER_NAME";
        private readonly string DATABASE = "DATABASE";
        private readonly string USER_ID = "USER_ID";
        private readonly string PASSWORD = "PASSWORD";
        //
        private readonly string LOG_PATH = "LOG_PATH";
        private readonly string LOG_NAME = "LOG_NAME";
        private readonly string LOG_EXTENSION = "LOG_EXTENSION";

        public string ServerName => ConfigurationManager.AppSettings.Get(SERVER_NAME);

        public string Database => ConfigurationManager.AppSettings.Get(DATABASE);

        public string UserId => ConfigurationManager.AppSettings.Get(USER_ID);

        public string Password => ConfigurationManager.AppSettings.Get(PASSWORD);

        public string LogPath => ConfigurationManager.AppSettings.Get(LOG_PATH);

        public string LogName => ConfigurationManager.AppSettings.Get(LOG_NAME);

        public string LogExtension => ConfigurationManager.AppSettings.Get(LOG_EXTENSION);
    }
}
