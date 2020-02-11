using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyLogger.Core.Plugin;
using MyLogger.Plugins.FileSystemPlugins;
using MyLogger.Shared.ConfigManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogger.UnitTests.Plugins.FileSystemPlugins
{
    [TestClass]
    public class TextFilePluginUnitTests
    {
        [TestClass]
        public class Constructor
        {
            [TestMethod]
            public void WhenFsParamsIsNull_ThenTrowException()
            {
                var message = string.Empty;

                try
                {
                    // Arrange
                    var fsParams = (FsParams)null;

                    // Act
                    IPlugin plugin = new TextFilePlugin(fsParams);
                }
                catch (Exception ex)
                {

                    message = ex.Message;
                }

                // Assert
                StringAssert.Contains(message, "TextFilePlugin : fsParams is null.");
            }

            [TestMethod]
            public void GivenFilePathInFsParamsIsNull_WhenCreatesAnPluginInstance_ThenThrowException()
            {
                var message = string.Empty;

                try
                {
                    // Arrange
                    var fsParams = new FsParams { LogPath = (string)null };

                    // Act
                    IPlugin plugin = new TextFilePlugin(fsParams);
                }
                catch (Exception ex)
                {

                    message = ex.Message;
                }

                // Assert
                StringAssert.Contains(message, "TextFilePlugin : fsParams.LogPath is null or empty.");
            }

            [TestMethod]
            public void GivenFileNameInFsParamsIsNull_WhenCreatesAnPluginInstance_ThenThrowException()
            {
                var message = string.Empty;

                try
                {
                    // Arrange
                    var fsParams = new FsParams { LogPath = ".\\", LogName = (string)null };

                    // Act
                    IPlugin plugin = new TextFilePlugin(fsParams);
                }
                catch (Exception ex)
                {

                    message = ex.Message;
                }

                // Assert
                StringAssert.Contains(message, "TextFilePlugin : fsParams.LogName is null or empty.");
            }

            [TestMethod]
            public void GivenFileExtensionInFsParamsIsNull_WhenCreatesAnPluginInstance_ThenThrowException()
            {
                var message = string.Empty;

                try
                {
                    // Arrange
                    var fsParams = new FsParams { LogPath = ".\\", LogName = "log-file", LogExtension = (string)null };

                    // Act
                    IPlugin plugin = new TextFilePlugin(fsParams);
                }
                catch (Exception ex)
                {

                    message = ex.Message;
                }

                // Assert
                StringAssert.Contains(message, "TextFilePlugin : fsParams.LogExtension is null or empty.");
            }
        }

        [TestClass]
        public class ConstructorWithConfigManager
        {
            [TestMethod]
            public void WhenConfigurationManagerIsNull_ThenTrowException()
            {
                var message = string.Empty;

                try
                {
                    // Arrange
                    IConfigManager configManager = null;

                    // Act
                    IPlugin plugin = new TextFilePlugin(configManager);
                }
                catch (Exception ex)
                {

                    message = ex.Message;
                }

                // Assert
                StringAssert.Contains(message, "TextFilePlugin : configManager is null.");
            }

            [TestMethod]
            public void WhenFilePathInConfigIsNull_ThenTrowException()
            {
                var message = string.Empty;

                try
                {
                    // Arrange
                    var configManager = new Mock<IConfigManager>();
                    configManager.Setup(x => x.LogPath).Returns(string.Empty);

                    // Act
                    IPlugin plugin = new TextFilePlugin(configManager.Object);
                }
                catch (Exception ex)
                {

                    message = ex.Message;
                }

                // Assert
                StringAssert.Contains(message, "TextFilePlugin : configManager.LogPath is null or empty.");
            }

            [TestMethod]
            public void WhenLogNameInConfigIsNull_ThenTrowException()
            {
                var message = string.Empty;

                try
                {
                    // Arrange
                    var configManager = new Mock<IConfigManager>();
                    configManager.Setup(x => x.LogPath).Returns("./");
                    configManager.Setup(x => x.LogName).Returns(string.Empty);

                    // Act
                    IPlugin plugin = new TextFilePlugin(configManager.Object);
                }
                catch (Exception ex)
                {

                    message = ex.Message;
                }

                // Assert
                StringAssert.Contains(message, "TextFilePlugin : configManager.LogName is null or empty.");
            }

            [TestMethod]
            public void WhenLogExtensionInConfigIsNull_ThenTrowException()
            {
                var message = string.Empty;

                try
                {
                    // Arrange
                    var configManager = new Mock<IConfigManager>();
                    configManager.Setup(x => x.LogPath).Returns("./");
                    configManager.Setup(x => x.LogName).Returns("log-file");
                    configManager.Setup(x => x.LogExtension).Returns(string.Empty);

                    // Act
                    IPlugin plugin = new TextFilePlugin(configManager.Object);
                }
                catch (Exception ex)
                {

                    message = ex.Message;
                }

                // Assert
                StringAssert.Contains(message, "TextFilePlugin : configManager.LogExtension is null or empty.");
            }
        }
    }
}
