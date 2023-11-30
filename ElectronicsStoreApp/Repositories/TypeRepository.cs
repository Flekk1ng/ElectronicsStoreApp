using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using Type = ElectronicsStoreApp.Models.Type;

namespace ElectronicsStoreApp.Repositories
{
    public class TypeRepository : IRepository<Type>
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        public int Create(Type type)
        {
            string sqlExpression = "INSERT INTO Вид (idКатегории, Вид) VALUES(@idCategory, @name);" +
                                   "SELECT SCOPE_IDENTITY();";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddWithValue("@idCategory", type.Category.Id);
                    command.Parameters.AddWithValue("@name", type.Name);
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public void Delete(int id)
        {
            string sqlExpression = "DELETE FROM Вид WHERE id = @id";
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

        public Type GetById(int id)
        {
            string sqlExpression = "SELECT * FROM Вид " +
                                   "WHERE id = @id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo);
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                return RowToType(dataTable.Rows[0]);
            }
        }

        public List<Type> GetAll()
        {
            string sqlExpression = "SELECT * FROM Вид";
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
            string sqlExpression = "SELECT Вид FROM Вид WHERE id = @id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo);
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                return dataTable.Rows[0].Field<string>("Вид");
            }
        }

        public Type GetByName(string name)
        {
            string sqlExpression = "SELECT * FROM Вид " +
                                   "WHERE Вид = @name";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@name", name);
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo);
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                return RowToType(dataTable.Rows[0]);
            }
        }

        public void UpdateById(Type type)
        {
            string sqlExpression = "UPDATE Вид " +
                                   "SET idКатегории = @idCategory, " +
                                   "Вид = @name " +
                                   "WHERE id = @id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddWithValue("@id", type.Id);
                    command.Parameters.AddWithValue("@idCategory", type.Category.Id);
                    command.Parameters.AddWithValue("@name", type.Name);
                    command.ExecuteNonQuery();
                }
            }
        }

        public Type RowToType(DataRow dt)
        {
            CategoryRepository categoryRepo = new CategoryRepository();
            Type type = new Type();
            type.Id = dt.Field<int>("id");
            type.Category = categoryRepo.GetById(dt.Field<int>("idКатегории"));
            type.Name = dt.Field<string>("Вид");
            return type;
        }


        public List<Type> dataTableToList(DataTable dt)
        {
            List<Type> typeList = new List<Type>();
            foreach (var type in dt.AsEnumerable())
            {
                typeList.Add(RowToType(type));
            }
            return typeList;
        }
    }
}
