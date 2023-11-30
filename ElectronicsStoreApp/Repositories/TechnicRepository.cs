using ElectronicsStoreApp.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System;

namespace ElectronicsStoreApp.Repositories
{
    public class TechnicRepository : IRepository<Technic>
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        public int Create(Technic technic)
        {
            string sqlExpression = "INSERT INTO Техника " +
                                   "(idПоставщика, idВида, Модель, ДатаВыпуска, Цена, Количество, СрокГарантии, ТипГарантии) " +
                                   "VALUES " +
                                   "(@idSupplier, @idType, @model, @dateOfIssue, @price, @count, @guaranteePeriod, @typeOfGuarantee);" +
                                   "SELECT SCOPE_IDENTITY();";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddWithValue("@idSupplier", technic.Supplier.Id);
                    command.Parameters.AddWithValue("@idType", technic.Type.Id);
                    command.Parameters.AddWithValue("@model", technic.Model);
                    command.Parameters.AddWithValue("@dateOfIssue", technic.DateOfIssue);
                    command.Parameters.AddWithValue("@price", technic.Price);
                    command.Parameters.AddWithValue("@count", technic.Count);
                    command.Parameters.AddWithValue("@guaranteePeriod", technic.GuaranteePeriod);
                    command.Parameters.AddWithValue("@typeOfGuarantee", technic.TypeOfGuarantee);
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public void Delete(int id)
        {
            string sqlExpression = "DELETE FROM Техника WHERE id = @Id";
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

        public Technic GetById(int id)
        {
            string sqlExpression = "SELECT * FROM Техника " +
                                   "WHERE id = @id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo);
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                return RowToTechnic(dataTable.Rows[0]);
            }
        }

        public List<Technic> GetAll()
        {
            string sqlExpression = "SELECT * FROM Техника";
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

        public List<string> GetAllModels()
        {
            string sqlExpression = "SELECT Модель FROM Техника";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo);
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                return dataTableToListString(dataTable, "Модель");
            }
        }

        public string GetModelById(int id)
        {
            string sqlExpression = "SELECT Модель FROM Техника WHERE id = @id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo);
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                return dataTable.Rows[0].Field<string>("Модель");
            }
        }

        public Technic GetByModel(string model)
        {
            string sqlExpression = "SELECT * FROM Техника " +
                                   "WHERE Модель = @model";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@model", model);
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo);
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                return RowToTechnic(dataTable.Rows[0]);
            }
        }

        public void UpdateById(Technic technic)
        {
            string sqlExpression = "UPDATE Техника " +
                                   "SET idПоставщика = @idSupplier, " +
                                   "idВида = @idType, " +
                                   "Модель = @model, " +
                                   "ДатаВыпуска = @dateOfIssue, " +
                                   "Цена = @price, " +
                                   "Количество = @count, " +
                                   "СрокГарантии = @guaranteePeriod, " +
                                   "ТипГарантии = @typeOfGuarantee " +
                                   "WHERE id = @id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddWithValue("@id", technic.Id);
                    command.Parameters.AddWithValue("@idSupplier", technic.Supplier.Id);
                    command.Parameters.AddWithValue("@idType", technic.Type.Id);
                    command.Parameters.AddWithValue("@model", technic.Model);
                    command.Parameters.AddWithValue("@dateOfIssue", technic.DateOfIssue);
                    command.Parameters.AddWithValue("@price", technic.Price);
                    command.Parameters.AddWithValue("@count", technic.Count);
                    command.Parameters.AddWithValue("@guaranteePeriod", technic.GuaranteePeriod);
                    command.Parameters.AddWithValue("@typeOfGuarantee", technic.TypeOfGuarantee);
                    command.ExecuteNonQuery();
                }
            }
        }

        private Technic RowToTechnic(DataRow dt)
        {
            SupplierRepository supplierRepo = new SupplierRepository();
            TypeRepository typeRepo = new TypeRepository();

            Technic technic = new Technic();
            technic.Id = dt.Field<int>("id");
            technic.Supplier = supplierRepo.GetById(dt.Field<int>("idПоставщика"));
            technic.Type = typeRepo.GetById(dt.Field<int>("idВида"));
            technic.Model = dt.Field<string>("Модель");
            technic.DateOfIssue = dt.Field<DateTime>("ДатаВыпуска");
            technic.Price = dt.Field<decimal>("Цена");
            technic.Count = dt.Field<int>("Количество");
            technic.GuaranteePeriod = dt.Field<int>("СрокГарантии");
            technic.TypeOfGuarantee = dt.Field<string>("ТипГарантии");
            return technic;
        }

        private List<string> dataTableToListString(DataTable dt, string name)
        {
            List<string> stringList = new List<string>();
            foreach (var str in dt.AsEnumerable())
            {
                stringList.Add(str.Field<string>(name));
            }
            return stringList;
        }

        private List<Technic> dataTableToList(DataTable dt)
        {
            List<Technic> technicList = new List<Technic>();
            foreach (var technic in dt.AsEnumerable())
            {
                technicList.Add(RowToTechnic(technic));
            }
            return technicList;
        }
    }
}
