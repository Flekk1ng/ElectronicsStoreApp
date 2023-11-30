using ElectronicsStoreApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace ElectronicsStoreApp.Repositories
{
    public class ServiceCenterRepository : IRepository<ServiceCenter>
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        public int Create(ServiceCenter serviceCenter)
        {
            string sqlExpression = "INSERT INTO СервисныйЦентр (Название, Адрес, ЭлПочта) VALUES(@name, @address, @email);" +
                                   "SELECT SCOPE_IDENTITY();";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddWithValue("@name", serviceCenter.Name);
                    command.Parameters.AddWithValue("@address", serviceCenter.Address);
                    command.Parameters.AddWithValue("@email", serviceCenter.Email);
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public void Delete(int id)
        {
            string sqlExpression = "DELETE FROM СервисныйЦентр " +
                                   "WHERE id = @id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }

        }

        public ServiceCenter GetById(int id)
        {
            string sqlExpression = "SELECT * FROM СервисныйЦентр " +
                                   "WHERE id = @id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo);
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                return RowToServiceCenter(dataTable.Rows[0]);
            }
        }

        public List<ServiceCenter> GetAll()
        {
            string sqlExpression = "SELECT * FROM СервисныйЦентр";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo);
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                return dataTableToList(dataTable);
            }

        }

        public void UpdateById(ServiceCenter serviceCenter)
        {
            string sqlExpression = "UPDATE СервисныйЦентр " +
                                   "SET Название = @name, " +
                                   "Адрес = @address, " +
                                   "ЭлПочта = @email " +
                                   "WHERE id = @id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddWithValue("@id", serviceCenter.Id);
                    command.Parameters.AddWithValue("@name", serviceCenter.Name);
                    command.Parameters.AddWithValue("@address", serviceCenter.Address);
                    command.Parameters.AddWithValue("@email", serviceCenter.Email);
                    command.ExecuteNonQuery();
                }
            }
        }

        public ServiceCenter RowToServiceCenter(DataRow dt)
        {
            ServiceCenter serviceCenter = new ServiceCenter();
            serviceCenter.Id = dt.Field<int>("id");
            serviceCenter.Name = dt.Field<string>("Название");
            serviceCenter.Address = dt.Field<string>("Адрес");
            serviceCenter.Email = dt.Field<string>("ЭлПочта");
            return serviceCenter;
        }


        public List<ServiceCenter> dataTableToList(DataTable dt)
        {
            List<ServiceCenter> serviceCenterList = new List<ServiceCenter>();
            foreach (var supplier in dt.AsEnumerable())
            {
                serviceCenterList.Add(RowToServiceCenter(supplier));
            }
            return serviceCenterList;
        }
    }
}
