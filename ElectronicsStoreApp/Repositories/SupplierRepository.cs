using ElectronicsStoreApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace ElectronicsStoreApp.Repositories
{
    public class SupplierRepository : IRepository<Supplier>
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        public int Create(Supplier supplier)
        {
            string sqlExpression = "INSERT INTO Поставщик (Название, Адрес, ЭлПочта) VALUES(@name, @address, @email);" +
                                   "SELECT SCOPE_IDENTITY();";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddWithValue("@name", supplier.Name);
                    command.Parameters.AddWithValue("@address", supplier.Address);
                    command.Parameters.AddWithValue("@email", supplier.Email);
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public void Delete(int id)
        {
            string sqlExpression = "DELETE FROM Поставщик " +
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

        public Supplier GetById(int id)
        {
            string sqlExpression = "SELECT * FROM Поставщик " +
                                   "WHERE id = @id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo);
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                return RowToSupplier(dataTable.Rows[0]);
            }
        }

        public List<Supplier> GetAll()
        {
            string sqlExpression = "SELECT * FROM Поставщик";
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

        public string GetNameById(int id)
        {
            string sqlExpression = "SELECT Название FROM Поставщик WHERE id = @id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo);
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                return dataTable.Rows[0].Field<string>("Название");
            }
        }

        public Supplier GetByName(string name)
        {
            string sqlExpression = "SELECT * FROM Поставщик " +
                                   "WHERE Название = @name";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@name", name);
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo);
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                return RowToSupplier(dataTable.Rows[0]);
            }
        }

        public void UpdateById(Supplier supplier)
        {
            string sqlExpression = "UPDATE Поставщик " +
                                   "SET Название = @name," +
                                   "    Адрес = @address, " +
                                   "    ЭлПочта = @email " +
                                   "WHERE id = @id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddWithValue("@id", supplier.Id);
                    command.Parameters.AddWithValue("@name", supplier.Name);
                    command.Parameters.AddWithValue("@address", supplier.Address);
                    command.Parameters.AddWithValue("@email", supplier.Email);
                    command.ExecuteNonQuery();
                }
            }
        }

        public Supplier RowToSupplier(DataRow dt)
        {
            Supplier supplier = new Supplier();
            supplier.Id = dt.Field<int>("id");
            supplier.Name = dt.Field<string>("Название");
            supplier.Address = dt.Field<string>("Адрес");
            supplier.Email = dt.Field<string>("ЭлПочта");
            return supplier;
        }


        public List<Supplier> dataTableToList(DataTable dt)
        {
            List<Supplier> supplierList = new List<Supplier>();
            foreach (var supplier in dt.AsEnumerable())
            {
                supplierList.Add(RowToSupplier(supplier));
            }
            return supplierList;
        }
    }
}
