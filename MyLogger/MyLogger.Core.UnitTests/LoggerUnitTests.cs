using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyLogger.Core.Plugin;
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
        private static Logger logger;
        private static List<IPlugin> plugins;
        private static Mock<IPlugin> p1;
        private static Mock<IPlugin> p2;
        private static List<Severity> severities;

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



            [TestMethod]
            public void WhenCreatesAnIntanceWithNullSeverities_ThenTrowExceptionSeveritiesAreNull()
            {
                try
                {
                    // Arrange
                    severities = null;

                    // Act 
                    var logger = new Logger(severities);
                }
                catch (Exception ex)
                {
                    exceptionMessage = ex.Message;
                }

                // Assert
                StringAssert.Contains(exceptionMessage, "Logger : severities are null or empty.");
            }

            [TestMethod]
            public void WhenCreatesAnIntanceWithNoSeverities_ThenTrowException()
            {

                try
                {
                    // Arrange
                    severities = new List<Severity>();

                    // Act 
                    var logger = new Logger(severities);
                }
                catch (Exception ex)
                {
                    exceptionMessage = ex.Message;
                }

                // Assert
                StringAssert.Contains(exceptionMessage, "Logger : severities are null or empty.");

            }
        }

        [TestClass]
        public class LogWarning
        {
            [TestClass]
            public class GivenThereIsWarningSeverityRegisteredAndTwoPluginsRegistered
            {
                [TestInitialize]
                public void Test_Initialize()
                {
                    exceptionMessage = string.Empty;
                    //
                    RegisterWarningSeverity();
                    RegisterTwoPlugins();

                }

                private static void RegisterWarningSeverity()
                {
                    logger = new Logger();
                    logger.AddSeverity(Severity.Warning);
                }

                private static void RegisterTwoPlugins()
                {
                    p1 = new Mock<IPlugin>();
                    p2 = new Mock<IPlugin>();
                    logger.AddPlugin(p1.Object);
                    logger.AddPlugin(p2.Object);
                }

                

                [TestCleanup]
                public void Test_Cleanup()
                {
                    exceptionMessage = null;
                    //
                    logger = null;
                    p1 = null;
                    p2 = null;
                }

                [TestMethod]
                public void WhenLogsANullMessage_ThenExceptionMsgIsNullOrEmty()
                {
                    try
                    {
                        // Arrange

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
            public class GivenThereIsNoWarningSeverityRegisteredAndTwoPluginsRegistered
            {
                [TestInitialize]
                public void Test_Initialize()
                {
                    exceptionMessage = string.Empty;
                    //
                    DoNotRegisterErrorSeverity();
                    RegisterTwoPlugins();

                }

                private static void DoNotRegisterErrorSeverity()
                {
                    logger = new Logger();
                }

                private static void RegisterTwoPlugins()
                {
                    p1 = new Mock<IPlugin>();
                    p2 = new Mock<IPlugin>();
                    logger.AddPlugin(p1.Object);
                    logger.AddPlugin(p2.Object);
                }

                [TestCleanup]
                public void Test_Cleanup()
                {
                    exceptionMessage = null;
                    //
                    logger = null;
                    p1 = null;
                    p2 = null;
                }

                [TestMethod]
                public void WhenLogsANullMessage_ThenDoNotInvokeAnyPlugin()
                {
                    // Arrange

                    // Act 
                    logger.LogWarning(null);

                    // Assert
                    p1.Verify(x => x.Log(It.IsAny<DateTime>(),
                        It.IsAny<Severity>(),
                        It.IsAny<string>()), Times.Never);
                    //
                    p2.Verify(x => x.Log(It.IsAny<DateTime>(),
                        It.IsAny<Severity>(),
                        It.IsAny<string>()), Times.Never);
                }

                [TestMethod]
                public void WhenLogsAnEmptyMessage_ThenDoNotInvokeAnyPlugin()
                {
                    // Arrange

                    // Act 
                    logger.LogError(string.Empty);

                    // Assert
                    p1.Verify(x => x.Log(It.IsAny<DateTime>(),
                        It.IsAny<Severity>(),
                        It.IsAny<string>()), Times.Never);
                    //
                    p2.Verify(x => x.Log(It.IsAny<DateTime>(),
                        It.IsAny<Severity>(),
                        It.IsAny<string>()), Times.Never);
                }

                [TestMethod]
                public void WhenLogsError_ThenDoNotInvokeAnyPlugin()
                {
                    // Arrange

                    // Act 
                    logger.LogWarning("A warning");

                    //  Assert
                    p1.Verify(x => x.Log(
                        It.IsAny<DateTime>(),
                        It.Is<Severity>(s => s == Severity.Error),
                         It.Is<string>(str => str == "A warning"))
                        , Times.Never);
                    //
                    p2.Verify(x => x.Log(
                        It.IsAny<DateTime>(),
                        It.Is<Severity>(s => s == Severity.Error),
                         It.Is<string>(str => str == "A warning"))
                        , Times.Never);
                }
            }
        }

        [TestClass]
        public class LogInfo

        {
            [TestClass]
            public class GivenThereIsInfoSeverityRegisteredAndTwoPluginsRegistered
            {
                [TestInitialize]
                public void Test_Initialize()
                {
                    exceptionMessage = string.Empty;
                    //
                    RegisterInfoSeverity();
                    RegisterTwoPlugins();

                }

                private static void RegisterTwoPlugins()
                {
                    p1 = new Mock<IPlugin>();
                    p2 = new Mock<IPlugin>();
                    logger.AddPlugin(p1.Object);
                    logger.AddPlugin(p2.Object);
                }

                private static void RegisterInfoSeverity()
                {
                    logger = new Logger();
                    logger.AddSeverity(Severity.Info);
                }

                [TestCleanup]
                public void Test_Cleanup()
                {
                    exceptionMessage = null;
                    //
                    logger = null;
                    p1 = null;
                    p2 = null;
                }

                [TestMethod]
                public void WhenLogsANullMessage_ThenExceptionMsgIsNullOrEmty()
                {
                    try
                    {
                        // Arrange

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
            public class GivenThereIsNoInfoSeverityRegisteredAndTwoPluginsRegistered
            {
                [TestInitialize]
                public void Test_Initialize()
                {
                    exceptionMessage = string.Empty;
                    //
                    NoInfoSeverityRegistered();
                    RegisterTwoPlugins();
                }

                private static void NoInfoSeverityRegistered()
                {
                    logger = new Logger();
                }

                private static void RegisterTwoPlugins()
                {
                    p1 = new Mock<IPlugin>();
                    p2 = new Mock<IPlugin>();
                    logger.AddPlugin(p1.Object);
                    logger.AddPlugin(p2.Object);
                }

                [TestCleanup]
                public void Test_Cleanup()
                {
                    exceptionMessage = null;
                    //
                    p1 = null;
                    p2 = null;
                }

                [TestMethod]
                public void WhenLogsANullMessage_ThenDoNotInvokeAnyPlugin()
                {
                    // Arrange

                    // Act 
                    logger.LogInfo(null);

                    // Assert
                    p1.Verify(x => x.Log(It.IsAny<DateTime>(),
                        It.IsAny<Severity>(),
                        It.IsAny<string>()), Times.Never);
                    //
                    p2.Verify(x => x.Log(It.IsAny<DateTime>(),
                        It.IsAny<Severity>(),
                        It.IsAny<string>()), Times.Never);

                }

                [TestMethod]
                public void WhenLogsAnEmptyMessage_ThenDoNotInvokeAnyPlugin()
                {
                    // Arrange

                    // Act 
                    logger.LogInfo(string.Empty);

                    // Assert
                    p1.Verify(x => x.Log(It.IsAny<DateTime>(),
                        It.IsAny<Severity>(),
                        It.IsAny<string>()), Times.Never);
                    //
                    p2.Verify(x => x.Log(It.IsAny<DateTime>(),
                        It.IsAny<Severity>(),
                        It.IsAny<string>()), Times.Never);
                }

                [TestMethod]
                public void WhenLogsWarning_ThenDoNotInvokeAnyPlugin()
                {
                    // Arrange

                    // Act 
                    logger.LogInfo("A info");

                    //  Assert
                    p1.Verify(x => x.Log(
                        It.IsAny<DateTime>(),
                        It.Is<Severity>(s => s == Severity.Info),
                         It.Is<string>(str => str == "A info"))
                        , Times.Never);
                    //
                    p2.Verify(x => x.Log(
                        It.IsAny<DateTime>(),
                        It.Is<Severity>(s => s == Severity.Info),
                         It.Is<string>(str => str == "A info"))
                        , Times.Never);
                }
            }

        }

        [TestClass]
        public class LogError

        {
            [TestClass]
            public class GivenThereIsErrorSeverityRegisteredAndTwoPluginsRegistered
            {
                [TestInitialize]
                public void Test_Initialize()
                {
                    exceptionMessage = string.Empty;
                    //
                    RegisterErrorSeverity();
                    RegisterTwoPlugins();

                }

                private static void RegisterErrorSeverity()
                {
                    logger = new Logger();
                    logger.AddSeverity(Severity.Error);
                }

                private static void RegisterTwoPlugins()
                {
                    p1 = new Mock<IPlugin>();
                    p2 = new Mock<IPlugin>();
                    logger.AddPlugin(p1.Object);
                    logger.AddPlugin(p2.Object);
                }

                [TestCleanup]
                public void Test_Cleanup()
                {
                    exceptionMessage = null;
                    //
                    logger = null;
                    p1 = null;
                    p2 = null;
                }

                [TestMethod]
                public void WhenLogsANullMessage_ThenExceptionMsgIsNullOrEmty()
                {
                    try
                    {
                        // Arrange

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

            [TestClass]
            public class GivenThereIsNoErrorSeverityRegisteredAndTwoPluginsRegistered
            {
                [TestInitialize]
                public void Test_Initialize()
                {
                    exceptionMessage = string.Empty;
                    //
                    DoNotRegisterErrorSeverity();
                    RegisterTwoPlugins();

                }

                private static void DoNotRegisterErrorSeverity()
                {
                    logger = new Logger();
                }

                private static void RegisterTwoPlugins()
                {
                    p1 = new Mock<IPlugin>();
                    p2 = new Mock<IPlugin>();
                    logger.AddPlugin(p1.Object);
                    logger.AddPlugin(p2.Object);
                }

                [TestCleanup]
                public void Test_Cleanup()
                {
                    exceptionMessage = null;
                    //
                    logger = null;
                    p1 = null;
                    p2 = null;
                }

                [TestMethod]
                public void WhenLogsANullMessage_ThenDoNotInvokeAnyPlugin()
                {
                    // Arrange

                    // Act 
                    logger.LogError(null);

                    // Assert
                    p1.Verify(x => x.Log(It.IsAny<DateTime>(),
                        It.IsAny<Severity>(),
                        It.IsAny<string>()), Times.Never);
                    //
                    p2.Verify(x => x.Log(It.IsAny<DateTime>(),
                        It.IsAny<Severity>(),
                        It.IsAny<string>()), Times.Never);
                }

                [TestMethod]
                public void WhenLogsAnEmptyMessage_ThenDoNotInvokeAnyPlugin()
                {
                    // Arrange

                    // Act 
                    logger.LogError(string.Empty);

                    // Assert
                    p1.Verify(x => x.Log(It.IsAny<DateTime>(),
                        It.IsAny<Severity>(),
                        It.IsAny<string>()), Times.Never);
                    //
                    p2.Verify(x => x.Log(It.IsAny<DateTime>(),
                        It.IsAny<Severity>(),
                        It.IsAny<string>()), Times.Never);
                }

                [TestMethod]
                public void WhenLogsError_ThenDoNotInvokeAnyPlugin()
                {
                    // Arrange

                    // Act 
                    logger.LogError("A error");

                    //  Assert
                    p1.Verify(x => x.Log(
                        It.IsAny<DateTime>(),
                        It.Is<Severity>(s => s == Severity.Error),
                         It.Is<string>(str => str == "A error"))
                        , Times.Never);
                    //
                    p2.Verify(x => x.Log(
                        It.IsAny<DateTime>(),
                        It.Is<Severity>(s => s == Severity.Error),
                         It.Is<string>(str => str == "A error"))
                        , Times.Never);
                }
            }
        }

        [TestClass]
        public class AddPlugin
        {
            [TestInitialize]
            public void Test_Initialize()
            {
                exceptionMessage = string.Empty;
                p1 = new Mock<IPlugin>();
                p2 = new Mock<IPlugin>();
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
            public void WhenPluginIsNull_ThenThrowException()
            {
                try
                {
                    // Arrange
                    var logger = new Logger();

                    // Act 
                    logger.AddPlugin((IPlugin)null);

                }
                catch (Exception ex)
                {
                    exceptionMessage = ex.Message;
                }

                // Assert
                StringAssert.Contains(exceptionMessage, "Logger.AddPlugin : plugin is null.");
            }

            [TestMethod]
            public void WhenPluginIsNotNull_ThenPuginIsAddedToPluginCollection()
            {
                // Arrange
                var logger = new Logger();


                // Act 
                logger.AddPlugin(p1.Object);
                logger.AddPlugin(p2.Object);

                //Assert
                Assert.AreEqual(2, logger.PluginsCount);
            }
        }

        [TestClass]
        public class AddSeverity
        {
            [TestMethod]
            public void WhenSeverityIsAdded_ThenSeverityIsAddedToSeveritiesCollection()
            {
                // Arrange
                var logger = new Logger();


                // Act 
                logger.AddSeverity(Severity.Info);
                logger.AddSeverity(Severity.Warning);
                logger.AddSeverity(Severity.Error);

                //Assert
                Assert.AreEqual(3, logger.SeveritiesCount);
            }
        }
    }
}

