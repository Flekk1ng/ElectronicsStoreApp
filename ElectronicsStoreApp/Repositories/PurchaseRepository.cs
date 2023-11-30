using ElectronicsStoreApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace ElectronicsStoreApp.Repositories
{
    public class PurchaseRepository : IRepository<Purchase>
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        public int Create(Purchase purchase)
        {
            byte[] loginBytes = System.Text.Encoding.UTF8.GetBytes(Authorization.Login);
            string sqlExpression = "SET CONTEXT_INFO @username; " +
                                   "INSERT INTO Покупка (idТехники, idСотрудника, idДисконтКарты, Дата) " +
                                   "VALUES (@idTechnic, @idEmployee, @idDiscountCard, @dateOfPurchase);" +
                                   "SELECT SCOPE_IDENTITY();";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddWithValue("@idTechnic", purchase.Technic.Id);
                    command.Parameters.AddWithValue("@idEmployee", purchase.Employee.Id);
                    command.Parameters.AddWithValue("@idDiscountCard", purchase.DiscountCard.Id);
                    command.Parameters.AddWithValue("@dateOfPurchase", purchase.DateOfPurchase);
                    command.Parameters.Add("@username", SqlDbType.VarBinary, 128).Value = loginBytes;
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public void Delete(int id)
        {
            byte[] loginBytes = System.Text.Encoding.UTF8.GetBytes(Authorization.Login);
            string sqlExpression = "SET CONTEXT_INFO @username; DELETE FROM Покупка WHERE id = @Id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.Add("@username", SqlDbType.VarBinary, 128).Value = loginBytes;
                    command.ExecuteNonQuery();
                }
            }
        }

        public Purchase GetById(int id)
        {
            string sqlExpression = "SELECT * FROM Покупка " +
                                   "WHERE id = @id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo);
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                return RowToPurchase(dataTable.Rows[0]);
            }
        }

        public List<Purchase> GetAll()
        {
            string sqlExpression = "SELECT * FROM Покупка";
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

        public void UpdateById(Purchase purchase)
        {
            byte[] loginBytes = System.Text.Encoding.UTF8.GetBytes(Authorization.Login);
            string sqlExpression = "SET CONTEXT_INFO @username; " +
                                   "UPDATE Покупка " +
                                   "SET idТехники = @idTechnic, " +
                                   "idСотрудника = @idEmployee, " +
                                   "idДисконтКарты = @idDiscountCard, " +
                                   "Дата = @dateOfPurchase " +
                                   "WHERE id = @id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddWithValue("@id", purchase.Id);
                    command.Parameters.AddWithValue("@idTechnic", purchase.Technic.Id);
                    command.Parameters.AddWithValue("@idEmployee", purchase.Employee.Id);
                    command.Parameters.AddWithValue("@idDiscountCard", purchase.DiscountCard.Id);
                    command.Parameters.AddWithValue("@dateOfPurchase", purchase.DateOfPurchase);
                    command.Parameters.Add("@username", SqlDbType.VarBinary, 128).Value = loginBytes;
                    command.ExecuteNonQuery();
                }
            }
        }

        public DataTable FullInformationPurchase()
        {
            string sqlExpression = "SELECT * FROM ПолнаяИнформацияОПокупках";
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

        public List<Purchase> FilterPurchaseByEmployeeAndBuyer(int? employeeId, int? discountCardId)
        {
            string storedProcedureName = "ПолучитьПокупкиСотрудникаДисконтКарты";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (employeeId == null) 
                    {
                        command.Parameters.AddWithValue("@idСотрудника", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@idСотрудника", employeeId);
                    }
                    if (discountCardId == null)
                    {
                        command.Parameters.AddWithValue("@idДисконтКарты", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@idДисконтКарты", discountCardId);
                    }
                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo);
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    return dataTableToList(dataTable);
                }
            }
        }

        public Purchase RowToPurchase(DataRow dt)
        {
            TechnicRepository technicRepo = new TechnicRepository();
            EmployeeRepository employeeRepo = new EmployeeRepository();
            DiscountCardRepository discountCardRepo = new DiscountCardRepository();

            Purchase purchase = new Purchase();
            purchase.Id = dt.Field<int>("id");
            purchase.Technic = technicRepo.GetById(dt.Field<int>("idТехники"));
            purchase.Employee = employeeRepo.GetById(dt.Field<int>("idСотрудника"));
            purchase.DiscountCard = discountCardRepo.GetById(dt.Field<int>("idДисконтКарты"));
            purchase.DateOfPurchase = dt.Field<DateTime>("Дата");
            return purchase;
        }


        public List<Purchase> dataTableToList(DataTable dt)
        {
            List<Purchase> purchaseList = new List<Purchase>();
            foreach (var purchase in dt.AsEnumerable())
            {
                purchaseList.Add(RowToPurchase(purchase));
            }
            return purchaseList;
        }
    }
}
