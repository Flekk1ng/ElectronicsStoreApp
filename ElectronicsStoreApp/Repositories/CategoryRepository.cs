using ElectronicsStoreApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ElectronicsStoreApp.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        public int Create(Category category)
        {
            string sqlExpression = "INSERT INTO Категория (Категория) VALUES(@name);" +
                                   "SELECT SCOPE_IDENTITY();";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddWithValue("@name", category.Name);
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public void Delete(int id)
        {
            string sqlExpression = "DELETE FROM Категория " +
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

        public Category GetById(int id)
        {
            string sqlExpression = "SELECT * FROM Категория " +
                                   "WHERE id = @id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo);
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                return RowToCategory(dataTable.Rows[0]);
            }
        }

        public List<Category> GetAll()
        {
            string sqlExpression = "SELECT * FROM Категория";
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
            
        public void UpdateById(Category category)
        {
            string sqlExpression = "UPDATE Категория " +
                                   "SET Категория = @name " +
                                   "WHERE id = @id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddWithValue("@id", category.Id);
                    command.Parameters.AddWithValue("@name", category.Name);
                    command.ExecuteNonQuery();
                }
            }
        }

        public Category RowToCategory(DataRow dt)
        {
            Category category = new Category();
            category.Id = dt.Field<int>("id");
            category.Name = dt.Field<string>("Категория");
            return category;
        }


        public List<Category> dataTableToList(DataTable dt)
        {
            List<Category> categoryList = new List<Category>();
            foreach (var category in dt.AsEnumerable())
            {
                categoryList.Add(RowToCategory(category));
            }
            return categoryList;
        }
    }
}
