using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogger.Core
{
    public interface IPlugin
    {
        void Log(DateTime date, Severity severity, string message);
    }
}
