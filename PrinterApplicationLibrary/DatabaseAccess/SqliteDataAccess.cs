using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;
using Dapper;
using System.Linq;

namespace PrinterApplicationLibrary.DatabaseAccess
{
    // Data Access Layer
    public class SqliteDataAccess : ISqliteDataAccess
    {
        private readonly IConfiguration _config;

        public SqliteDataAccess(IConfiguration config)
        {
            _config = config;
        }

        // Loading data
        public List<T> LoadData<T, U>(string sqlStatement, U parameters, string connectionStringName)
        {
            string connectionString = _config.GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(sqlStatement, parameters).ToList();
                return rows;
            }
        }


        // Save / Process data
        public void SaveData<T>(string sqlStatement, T parameters, string connectionStringName)
        {
            string connectionString = _config.GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Execute(sqlStatement, parameters);
            }
        }
    }
}
