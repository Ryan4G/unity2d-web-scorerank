using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace unity2d_web_scorerank_api.Models
{
    public class RepositoryBase : IDisposable
    {
        public static IConfigurationRoot configurationRoot { get; set; }

        private MySqlConnection conn;

        public MySqlConnection GetMySqlConnection()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(System.Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json");

            configurationRoot = builder.Build();

            var connString = configurationRoot.GetConnectionString("DefaultConnection");
            conn = new MySqlConnection(connString);

            return conn;
        }

        public void Dispose()
        {
            if (conn != null && conn.State != System.Data.ConnectionState.Closed)
            {
                conn.Close();
            }
        }
    }
}
