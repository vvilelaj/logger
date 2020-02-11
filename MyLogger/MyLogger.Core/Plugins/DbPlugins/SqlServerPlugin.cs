using MyLogger.Core.Plugin;
using MyLogger.Shared;
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

            ParameterValidator.ThowExceptionWhenIsNullOrEmpty(dbParams.ServerName, "dbParams.ServerName", "SqlServerPlugin");
            ParameterValidator.ThowExceptionWhenIsNullOrEmpty(dbParams.Database, "dbParams.Database", "SqlServerPlugin");
            ParameterValidator.ThowExceptionWhenIsNullOrEmpty(dbParams.UserId, "dbParams.UserId", "SqlServerPlugin");
            ParameterValidator.ThowExceptionWhenIsNullOrEmpty(dbParams.Password, "dbParams.Password", "SqlServerPlugin");

            this.dbParams = dbParams;
        }

        public SqlServerPlugin(IConfigManager configManager)
        {
            if (configManager == null) throw new ArgumentNullException("configManager", "SqlServerPlugin : configManager is null.");

            this.configManager = configManager;

            ParameterValidator.ThowExceptionWhenIsNullOrEmpty(configManager.ServerName, "configManager.ServerName", "SqlServerPlugin");
            ParameterValidator.ThowExceptionWhenIsNullOrEmpty(configManager.Database, "configManager.Database", "SqlServerPlugin");
            ParameterValidator.ThowExceptionWhenIsNullOrEmpty(configManager.UserId, "configManager.UserId", "SqlServerPlugin");
            ParameterValidator.ThowExceptionWhenIsNullOrEmpty(configManager.Password, "configManager.Password", "SqlServerPlugin");

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
