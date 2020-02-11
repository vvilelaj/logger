using MyLogger.Core.Plugin;
using MyLogger.Shared.ConfigManager;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogger.Plugins.DbPlugins
{
    public class SqlServerPlugin : IPlugin
    {
        private readonly DbParams dbParams;
        private readonly IConfigManager configManager;

        public SqlServerPlugin() : this(new ConfigManager())
        {
        }

        public SqlServerPlugin(DbParams dbParams)
        {
            if (dbParams == null) throw new ArgumentNullException("dbParams", "SqlServerPlugin : dbParams is null.");

            ThowExceptionWhenIsNullOrEmpty(dbParams.ServerName, "dbParams.ServerName", "SqlServerPlugin");
            ThowExceptionWhenIsNullOrEmpty(dbParams.Database, "dbParams.Database", "SqlServerPlugin");
            ThowExceptionWhenIsNullOrEmpty(dbParams.UserId, "dbParams.UserId", "SqlServerPlugin");
            ThowExceptionWhenIsNullOrEmpty(dbParams.Password, "dbParams.Password", "SqlServerPlugin");

            this.dbParams = dbParams;
        }


        private static void ThowExceptionWhenIsNullOrEmpty(string paramValue, string paramName, string methodName)
        {
            if (string.IsNullOrEmpty(paramValue)) throw new ArgumentNullException(paramName, $"{methodName} : {paramName} is null or empty.");
        }

        public SqlServerPlugin(IConfigManager configManager)
        {
            if (configManager == null) throw new ArgumentNullException("configManager", "SqlServerPlugin : configManager is null.");

            this.configManager = configManager;

            ThowExceptionWhenIsNullOrEmpty(configManager.ServerName, "configManager.ServerName", "SqlServerPlugin");
            ThowExceptionWhenIsNullOrEmpty(configManager.Database, "configManager.Database", "SqlServerPlugin");
            ThowExceptionWhenIsNullOrEmpty(configManager.UserId, "configManager.UserId", "SqlServerPlugin");
            ThowExceptionWhenIsNullOrEmpty(configManager.Password, "configManager.Password", "SqlServerPlugin");

            this.dbParams = new DbParams
            {
                ServerName = configManager.ServerName,
                Database = configManager.Database,
                UserId = configManager.UserId,
                Password = configManager.Password,
            };

        }

        public void Log(DateTime date, Severity severity, string message)
        {
            var cnnSB = new SqlConnectionStringBuilder();
            cnnSB.DataSource = dbParams.ServerName;
            cnnSB.InitialCatalog = dbParams.Database;
            cnnSB.UserID = dbParams.UserId;
            cnnSB.Password = dbParams.Password;

            try
            {
                using (var cnn = new SqlConnection(cnnSB.ToString()))
                {
                    cnn.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = cnn;
                        command.CommandText = "Insert Into Log(creationDate,severity,message)values(@creationDate,@severity,@message)";
                        command.CommandType = System.Data.CommandType.Text;
                        command.Parameters.AddWithValue("@creationDate", date);
                        command.Parameters.AddWithValue("@severity", severity.ToString());
                        command.Parameters.AddWithValue("@message", message);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw ex;
            }

        }

    }
}
