using MyLogger.Core.Plugin;
using MyLogger.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogger.Plugins
{
    public class ConsolePlugin : IPlugin
    {
        private readonly TextWriter stdOut;
        private readonly TextWriter stdError;

        public ConsolePlugin() : this(Console.Out, Console.Error)
        {

        }

        public ConsolePlugin(TextWriter stdOut, TextWriter stdError)
        {
            ParameterValidator.ThowExceptionWhenIsNull(stdOut, "stdOut", "ConsolePlugin");
            ParameterValidator.ThowExceptionWhenIsNull(stdError, "stdError", "ConsolePlugin");

            this.stdOut = stdOut;
            this.stdError = stdError;
        }

        public void Log(DateTime date, Severity severity, string message)
        {
            var logMessage = $"{date.ToString()} - {severity.ToString()} : {message}";

            if (severity == Severity.Error)
            {
                this.stdError.WriteLine(logMessage);
            }
            else
            {
                this.stdOut.WriteLine(logMessage);
            }
        }
    }
}
