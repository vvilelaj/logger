using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyLogger.Core.Plugin;
using MyLogger.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogger.UnitTests.Plugins
{
    [TestClass]
    public class ConsolePluginUnitTests
    {
        [TestClass]
        public class Constructor
        {
            [TestMethod]
            public void WhenStdOutIsNull_ThrowException()
            {
                var message = string.Empty;
                try
                {
                    // Arrange
                    TextWriter stdOut = null;

                    // Act
                    IPlugin plugin = new ConsolePlugin(stdOut, null);

                    // Assert
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }

                StringAssert.Contains(message, "ConsolePlugin : stdOut is null.");
            }

            [TestMethod]
            public void WhenStdErrorIsNull_ThrowException()
            {
                var message = string.Empty;
                try
                {
                    // Arrange
                    TextWriter stdOut = Console.Out;
                    TextWriter stdError = null;

                    // Act
                    IPlugin plugin = new ConsolePlugin(stdOut, stdError);

                    // Assert
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }

                StringAssert.Contains(message, "ConsolePlugin : stdError is null.");
            }
        }
    }
}
