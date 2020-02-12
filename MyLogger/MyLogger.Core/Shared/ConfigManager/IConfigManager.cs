using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogger.Shared.ConfigManager
{
    public interface IConfigManager
    {
        string ServerName { get; }
        string Database { get; }
        string UserId { get; }
        string Password { get; }
        string LogPath { get; }
        string LogName { get; }
        string LogExtension { get; }
    }
}
