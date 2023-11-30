using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using ElectronicsStoreApp.Models;
using System.Collections.Generic;
using System;

namespace ElectronicsStoreApp.Repositories
{
    public class LogRepository
    {

        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        public DataTable GetLogs()
        {
            string sqlExpression = "SELECT * FROM LogsView";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo);
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    return dataTable;
                }
            }
        }

        public void RestorePurchase(int lastid)
        {
            string storedProcedureName = "restoreПокупка";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@lastid", lastid);
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
