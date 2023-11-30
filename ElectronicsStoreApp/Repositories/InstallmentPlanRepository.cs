using ElectronicsStoreApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace ElectronicsStoreApp.Repositories
{
    public class InstallmentPlanRepository : IRepository<InstallmentPlan>
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        public int Create(InstallmentPlan installmentPlan)
        {
            string sqlExpression = "INSERT INTO Рассрочка (idПокупки, СуммаОплаты, ДатаОплаты) " +
                                   "VALUES(@idPurchase, @paymentAmount, @dateOfPayment);" +
                                   "SELECT SCOPE_IDENTITY();";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddWithValue("@idPurchase", installmentPlan.Purchase.Id);
                    command.Parameters.AddWithValue("@paymentAmount", installmentPlan.PaymentAmount);
                    command.Parameters.AddWithValue("@dateOfPayment", installmentPlan.DateOfPayment);
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public void Delete(int id)
        {
            string sqlExpression = "DELETE FROM Рассрочка " +
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

        public InstallmentPlan GetById(int id)
        {
            string sqlExpression = "SELECT * FROM Рассрочка " +
                                   "WHERE id = @id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo);
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                return RowToInstallmentPlan(dataTable.Rows[0]);
            }
        }

        public List<InstallmentPlan> GetAll()
        {
            string sqlExpression = "SELECT * FROM Рассрочка";
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

        public void UpdateById(InstallmentPlan installmentPlan)
        {
            string sqlExpression = "UPDATE Рассрочка " +
                                   "SET idПокупки = @idPurchase, " +
                                   "СуммаОплаты = @paymentAmount, " +
                                   "ДатаОплаты = @dateOfPurchase " +
                                   "WHERE id = @id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                { 
                    command.Parameters.AddWithValue("@id", installmentPlan.Id);
                    command.Parameters.AddWithValue("@idPurchase", installmentPlan.Purchase.Id);
                    command.Parameters.AddWithValue("@paymentAmount", installmentPlan.PaymentAmount);
                    command.Parameters.AddWithValue("@dateOfPurchase", installmentPlan.DateOfPayment);
                    command.ExecuteNonQuery();
                }
            }
        }

        public InstallmentPlan RowToInstallmentPlan(DataRow dt)
        {
            PurchaseRepository purchaseRepo = new PurchaseRepository();
            
            InstallmentPlan installmentPlan = new InstallmentPlan();
            installmentPlan.Id = dt.Field<int>("id");
            installmentPlan.Purchase = purchaseRepo.GetById(dt.Field<int>("idПокупки"));
            installmentPlan.PaymentAmount = dt.Field<decimal>("СуммаОплаты");
            installmentPlan.DateOfPayment = dt.Field<DateTime>("ДатаОплаты");
            return installmentPlan;
        }


        public List<InstallmentPlan> dataTableToList(DataTable dt)
        {
            List<InstallmentPlan> installmentPlanList = new List<InstallmentPlan>();
            foreach (var installmentPlan in dt.AsEnumerable())
            {
                installmentPlanList.Add(RowToInstallmentPlan(installmentPlan));
            }
            return installmentPlanList;
        }
    }
}
