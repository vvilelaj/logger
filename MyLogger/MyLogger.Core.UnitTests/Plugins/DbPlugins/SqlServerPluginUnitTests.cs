using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyLogger.Core.Plugin;
using MyLogger.Plugins.DbPlugins;
using MyLogger.Shared.ConfigManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogger.UnitTests.Plugins.DbPlugins
{
    [TestClass]
    public class SqlServerPluginUnitTests
    {
        [TestMethod]
        public void SqlServerPluginMustImplementIPlugin()
        {
            // Arrange
            IPlugin p;

            // Act
            p = new SqlServerPlugin();

            // Assert
            Assert.IsTrue(p is IPlugin);
        }

        [TestClass]
        public class ConstructorDbParams
        {
            [TestMethod]
            public void WhenCreatesIntanceWithNullDbParams_ThenTrownException()
            {
                var message = string.Empty;

                try
                {
                    // Arrange
                    var dbParams = (DbParams)null;

                    // Act
                    var plugin = new SqlServerPlugin(dbParams);
                }
                catch (Exception ex)
                {

                    message = ex.Message;
                }

                // Assert
                StringAssert.Contains(message, "SqlServerPlugin : dbParams is null.");
            }

            [TestMethod]
            public void GivenSereverNameInDbParamsIsNull_WhenCreatesAnPluginInstance_ThenThrowException()
            {
                var message = string.Empty;

                try
                {
                    // Arrange
                    var dbParams = new DbParams { ServerName = (string)null };

                    // Act
                    var plugin = new SqlServerPlugin(dbParams);
                }
                catch (Exception ex)
                {

                    message = ex.Message;
                }

                // Assert
                StringAssert.Contains(message, "SqlServerPlugin : dbParams.ServerName is null or empty.");
            }

            [TestMethod]
            public void GivenDatabaseInDbParamsIsNull_WhenCreatesAnPluginInstance_ThenThrowException()
            {
                var message = string.Empty;

                try
                {
                    // Arrange
                    var dbParams = new DbParams { ServerName = "servername", Database = (string)null };

                    // Act
                    var plugin = new SqlServerPlugin(dbParams);
                }
                catch (Exception ex)
                {

                    message = ex.Message;
                }

                // Assert
                StringAssert.Contains(message, "SqlServerPlugin : dbParams.Database is null or empty.");
            }

            [TestMethod]
            public void GivenUserIdInDbParamsIsNull_WhenCreatesAnPluginInstance_ThenThrowException()
            {
                var message = string.Empty;

                try
                {
                    // Arrange
                    var dbParams = new DbParams { ServerName = "servername", Database = "db", UserId = (string)null };

                    // Act
                    var plugin = new SqlServerPlugin(dbParams);
                }
                catch (Exception ex)
                {

                    message = ex.Message;
                }

                // Assert
                StringAssert.Contains(message, "SqlServerPlugin : dbParams.UserId is null or empty.");
            }

            [TestMethod]
            public void GivenPasswordInDbParamsIsNull_WhenCreatesAnPluginInstance_ThenThrowException()
            {
                var message = string.Empty;

                try
                {
                    // Arrange
                    var dbParams = new DbParams { ServerName = "servername", Database = "db", UserId = "user", Password = (string)null };

                    // Act
                    var plugin = new SqlServerPlugin(dbParams);
                }
                catch (Exception ex)
                {

                    message = ex.Message;
                }

                // Assert
                StringAssert.Contains(message, "SqlServerPlugin : dbParams.Password is null or empty.");
            }
        }

        [TestClass]
        public class ConstructorConfigManager
        {
            [TestMethod]
            public void WhenConfigManagerIsNull_ThenThrowException()
            {
                var message = string.Empty;

                try
                {
                    // Arrange
                    IConfigManager configManager=null;

                    // Act
                    var plugin = new SqlServerPlugin(configManager);
                }
                catch (Exception ex)
                {

                    message = ex.Message;
                }

                // Assert
                StringAssert.Contains(message, "SqlServerPlugin : configManager is null.");
            }

            [TestMethod]
            public void GivenSereverNameInConfigIsNull_WhenCreatesAnPluginInstance_ThenThrowException()
            {
                var message = string.Empty;

                try
                {
                    // Arrange
                    var configManager = new Mock<IConfigManager>();
                    configManager.Setup(x => x.ServerName).Returns(string.Empty);

                    // Act
                    var plugin = new SqlServerPlugin(configManager.Object);
                }
                catch (Exception ex)
                {

                    message = ex.Message;
                }

                // Assert
                StringAssert.Contains(message, "SqlServerPlugin : configManager.ServerName is null or empty.");
            }

            [TestMethod]
            public void GivenDatabaseInConfigIsNull_WhenCreatesAnPluginInstance_ThenThrowException()
            {
                var message = string.Empty;

                try
                {
                    // Arrange
                    var configManager = new Mock<IConfigManager>();
                    configManager.Setup(x => x.ServerName).Returns("server");
                    configManager.Setup(x => x.Database).Returns(string.Empty);

                    // Act
                    var plugin = new SqlServerPlugin(configManager.Object);
                }
                catch (Exception ex)
                {

                    message = ex.Message;
                }

                // Assert
                StringAssert.Contains(message, "SqlServerPlugin : configManager.Database is null or empty.");
            }

            [TestMethod]
            public void GivenUserIdInConfigIsNull_WhenCreatesAnPluginInstance_ThenThrowException()
            {
                var message = string.Empty;

                try
                {
                    // Arrange
                    var configManager = new Mock<IConfigManager>();
                    configManager.Setup(x => x.ServerName).Returns("server");
                    configManager.Setup(x => x.Database).Returns("db");
                    configManager.Setup(x => x.UserId).Returns(string.Empty);


                    // Act
                    var plugin = new SqlServerPlugin(configManager.Object);
                }
                catch (Exception ex)
                {

                    message = ex.Message;
                }

                // Assert
                StringAssert.Contains(message, "SqlServerPlugin : configManager.UserId is null or empty.");
            }

            [TestMethod]
            public void GivenPasswordInConfigIsNull_WhenCreatesAnPluginInstance_ThenThrowException()
            {
                var message = string.Empty;

                try
                {
                    // Arrange
                    var configManager = new Mock<IConfigManager>();
                    configManager.Setup(x => x.ServerName).Returns("server");
                    configManager.Setup(x => x.Database).Returns("db");
                    configManager.Setup(x => x.UserId).Returns("UserId");
                    configManager.Setup(x => x.Password).Returns(string.Empty);


                    // Act
                    var plugin = new SqlServerPlugin(configManager.Object);
                }
                catch (Exception ex)
                {

                    message = ex.Message;
                }

                // Assert
                StringAssert.Contains(message, "SqlServerPlugin : configManager.Password is null or empty.");
            }
        }


        //public class Log
        //{
        //    [TestMethod]
        //    public void When_Then()
        //    {

        //    }
        //}
    }
}
