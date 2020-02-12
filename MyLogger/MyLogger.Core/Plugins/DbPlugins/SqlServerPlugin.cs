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

            dbParams = LoadDbParamsFromConfig();
        }

        private DbParams LoadDbParamsFromConfig()
        {
            ParameterValidator.ThowExceptionWhenIsNullOrEmpty(this.configManager.ServerName, "configManager.ServerName", "SqlServerPlugin");
            ParameterValidator.ThowExceptionWhenIsNullOrEmpty(this.configManager.Database, "configManager.Database", "SqlServerPlugin");
            ParameterValidator.ThowExceptionWhenIsNullOrEmpty(this.configManager.UserId, "configManager.UserId", "SqlServerPlugin");
            ParameterValidator.ThowExceptionWhenIsNullOrEmpty(this.configManager.Password, "configManager.Password", "SqlServerPlugin");

            return new DbParams
            {
                ServerName = this.configManager.ServerName,
                Database = this.configManager.Database,
                UserId = this.configManager.UserId,
                Password = this.configManager.Password,
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
                throw;
            }

        }

    }
}
