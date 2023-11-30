using ElectronicsStoreApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace ElectronicsStoreApp.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        public int Create(Employee employee)
        {
            string sqlExpression = "INSERT INTO Сотрудник (ФИО, Адрес, ЭлПочта) VALUES(@fullName, @address, @email);" +
                                   "SELECT SCOPE_IDENTITY();";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddWithValue("@fullName", employee.FullName);
                    command.Parameters.AddWithValue("@address", employee.Address);
                    command.Parameters.AddWithValue("@email", employee.Email);
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public void Delete(int id)
        {
            string sqlExpression = "DELETE FROM Сотрудник " +
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

        public Employee GetById(int id)
        {
            string sqlExpression = "SELECT * FROM Сотрудник " +
                                   "WHERE id = @id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo);
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                return RowToEmployee(dataTable.Rows[0]);
            }
        }

        public List<Employee> GetAll()
        {
            string sqlExpression = "SELECT * FROM Сотрудник";
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

        public void UpdateById(Employee employee)
        {
            string sqlExpression = "UPDATE Сотрудник " +
                                   "SET ФИО = @fullName," +
                                   "    Адрес = @address, " +
                                   "    ЭлПочта = @email " +
                                   "WHERE id = @id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddWithValue("@id", employee.Id);
                    command.Parameters.AddWithValue("@fullName", employee.FullName);
                    command.Parameters.AddWithValue("@address", employee.Address);
                    command.Parameters.AddWithValue("@email", employee.Email);
                    command.ExecuteNonQuery();
                }
            }
        }

        public Employee RowToEmployee(DataRow dt)
        {
            Employee employee = new Employee();
            employee.Id = dt.Field<int>("id");
            employee.FullName = dt.Field<string>("ФИО");
            employee.Address = dt.Field<string>("Адрес");
            employee.Email = dt.Field<string>("ЭлПочта");
            return employee;
        }


        public List<Employee> dataTableToList(DataTable dt)
        {
            List<Employee> employeeList = new List<Employee>();
            foreach (var employee in dt.AsEnumerable())
            {
                employeeList.Add(RowToEmployee(employee));
            }
            return employeeList;
        }
    }
}
