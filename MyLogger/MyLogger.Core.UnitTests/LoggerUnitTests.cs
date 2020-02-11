using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogger.Core.UnitTests
{
    [TestClass]
    public class LoggerUnitTests
    {
        private static string exceptionMessage;
        private static List<IPlugin> plugins;
        private static Mock<IPlugin> p1;
        private static Mock<IPlugin> p2;

        [TestClass]
        public class Constructor
        {
            [TestInitialize]
            public void Test_Initialize()
            {
                exceptionMessage = string.Empty;
                plugins = new List<IPlugin>();

            }

            [TestCleanup]
            public void Test_Cleanup()
            {
                exceptionMessage = null;
                plugins = null;
            }

            [TestMethod]
            public void WhenCreatesAnIntanceWithNullPlugins_ThenTrowExceptionPluginsAreNull()
            {
                try
                {
                    // Arrange
                    plugins = null;

                    // Act 
                    var logger = new Logger(plugins);
                }
                catch (Exception ex)
                {
                    exceptionMessage = ex.Message;
                }

                // Assert
                StringAssert.Contains(exceptionMessage, "Logger : plugins are null or empty.");
            }

            [TestMethod]
            public void WhenCreatesAnIntanceWithNoPlugin_ThenTrowException()
            {

                try
                {
                    // Arrange
                    plugins = new List<IPlugin>();

                    // Act 
                    var logger = new Logger(plugins);
                }
                catch (Exception ex)
                {
                    exceptionMessage = ex.Message;
                }

                // Assert
                StringAssert.Contains(exceptionMessage, "Logger : plugins are null or empty.");
            }
        }

        [TestClass]
        public class LogWarning

        {
            [TestInitialize]
            public void Test_Initialize()
            {
                exceptionMessage = string.Empty;
                plugins = new List<IPlugin>();
                p1 = new Mock<IPlugin>();
                p2 = new Mock<IPlugin>();
                plugins.Add(p1.Object);
                plugins.Add(p2.Object);

            }

            [TestCleanup]
            public void Test_Cleanup()
            {
                exceptionMessage = null;
                plugins = null;
                p1 = null;
                p2 = null;
            }

            [TestMethod]
            public void WhenLogsANullMessage_ThenExceptionMsgIsNullOrEmty()
            {
                try
                {
                    // Arrange
                    var logger = new Logger(plugins);

                    // Act 
                    logger.LogWarning(null);
                }
                catch (Exception ex)
                {
                    exceptionMessage = ex.Message;
                }

                // Assert
                StringAssert.Contains(exceptionMessage, "Logger.LogWarning : message is null or empty.");
            }

            [TestMethod]
            public void WhenLogsAnEmptyMessage_ThenExceptionMsgIsNullOrEmty()
            {
                try
                {
                    // Arrange
                    var logger = new Logger(plugins);

                    // Act 
                    logger.LogWarning(string.Empty);
                }
                catch (Exception ex)
                {
                    exceptionMessage = ex.Message;
                }

                // Assert
                StringAssert.Contains(exceptionMessage, "Logger.LogWarning : message is null or empty.");
            }

            [TestMethod]
            public void WhenLogsWarning_ThenInvokePluginLogMethod()
            {
                // Arrange
                var logger = new Logger(plugins);

                // Act 
                logger.LogWarning("A warning");

                //  Assert
                p1.Verify(x => x.Log(
                    It.IsAny<DateTime>(),
                    It.Is<Severity>(s => s == Severity.Warning),
                     It.Is<string>(str => str == "A warning"))
                    , Times.Once);
                //
                p2.Verify(x => x.Log(
                    It.IsAny<DateTime>(),
                    It.Is<Severity>(s => s == Severity.Warning),
                     It.Is<string>(str => str == "A warning"))
                    , Times.Once);
            }
        }

        [TestClass]
        public class LogInfo

        {
            [TestInitialize]
            public void Test_Initialize()
            {
                exceptionMessage = string.Empty;
                plugins = new List<IPlugin>();
                p1 = new Mock<IPlugin>();
                p2 = new Mock<IPlugin>();
                plugins.Add(p1.Object);
                plugins.Add(p2.Object);

            }

            [TestCleanup]
            public void Test_Cleanup()
            {
                exceptionMessage = null;
                plugins = null;
                p1 = null;
                p2 = null;
            }

            [TestMethod]
            public void WhenLogsANullMessage_ThenExceptionMsgIsNullOrEmty()
            {
                try
                {
                    // Arrange
                    var logger = new Logger(plugins);

                    // Act 
                    logger.LogInfo(null);
                }
                catch (Exception ex)
                {
                    exceptionMessage = ex.Message;
                }

                // Assert
                StringAssert.Contains(exceptionMessage, "Logger.LogInfo : message is null or empty.");
            }

            [TestMethod]
            public void WhenLogsAnEmptyMessage_ThenExceptionMsgIsNullOrEmty()
            {
                try
                {
                    // Arrange
                    var logger = new Logger(plugins);

                    // Act 
                    logger.LogInfo(string.Empty);
                }
                catch (Exception ex)
                {
                    exceptionMessage = ex.Message;
                }

                // Assert
                StringAssert.Contains(exceptionMessage, "Logger.LogInfo : message is null or empty.");
            }

            [TestMethod]
            public void WhenLogsWarning_ThenInvokePluginLogMethod()
            {
                // Arrange
                var logger = new Logger(plugins);

                // Act 
                logger.LogInfo("A info");

                //  Assert
                p1.Verify(x => x.Log(
                    It.IsAny<DateTime>(),
                    It.Is<Severity>(s => s == Severity.Info),
                     It.Is<string>(str => str == "A info"))
                    , Times.Once);
                //
                p2.Verify(x => x.Log(
                    It.IsAny<DateTime>(),
                    It.Is<Severity>(s => s == Severity.Info),
                     It.Is<string>(str => str == "A info"))
                    , Times.Once);
            }
        }

        [TestClass]
        public class LogError

        {
            [TestInitialize]
            public void Test_Initialize()
            {
                exceptionMessage = string.Empty;
                plugins = new List<IPlugin>();
                p1 = new Mock<IPlugin>();
                p2 = new Mock<IPlugin>();
                plugins.Add(p1.Object);
                plugins.Add(p2.Object);

            }

            [TestCleanup]
            public void Test_Cleanup()
            {
                exceptionMessage = null;
                plugins = null;
                p1 = null;
                p2 = null;
            }

            [TestMethod]
            public void WhenLogsANullMessage_ThenExceptionMsgIsNullOrEmty()
            {
                try
                {
                    // Arrange
                    var logger = new Logger(plugins);

                    // Act 
                    logger.LogError(null);
                }
                catch (Exception ex)
                {
                    exceptionMessage = ex.Message;
                }

                // Assert
                StringAssert.Contains(exceptionMessage, "Logger.LogError : message is null or empty.");
            }

            [TestMethod]
            public void WhenLogsAnEmptyMessage_ThenExceptionMsgIsNullOrEmty()
            {
                try
                {
                    // Arrange
                    var logger = new Logger(plugins);

                    // Act 
                    logger.LogError(string.Empty);
                }
                catch (Exception ex)
                {
                    exceptionMessage = ex.Message;
                }

                // Assert
                StringAssert.Contains(exceptionMessage, "Logger.LogError : message is null or empty.");
            }

            [TestMethod]
            public void WhenLogsWarning_ThenInvokePluginLogMethod()
            {
                // Arrange
                var logger = new Logger(plugins);

                // Act 
                logger.LogError("A error");

                //  Assert
                p1.Verify(x => x.Log(
                    It.IsAny<DateTime>(),
                    It.Is<Severity>(s => s == Severity.Error),
                     It.Is<string>(str => str == "A error"))
                    , Times.Once);
                //
                p2.Verify(x => x.Log(
                    It.IsAny<DateTime>(),
                    It.Is<Severity>(s => s == Severity.Error),
                     It.Is<string>(str => str == "A error"))
                    , Times.Once);
            }
        }
    }
}
