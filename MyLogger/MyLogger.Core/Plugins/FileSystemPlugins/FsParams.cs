using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogger.Plugins.FileSystemPlugins
{
    public class FsParams
    {
        public string LogPath { get; set; }
        public string LogName { get; set; }
        public string LogExtension { get; set; }
    }
}
