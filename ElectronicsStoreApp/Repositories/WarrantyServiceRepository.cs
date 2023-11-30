using ElectronicsStoreApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Xml.Linq;

namespace ElectronicsStoreApp.Repositories
{
    public class WarrantyServiceRepository : IRepository<WarrantyService>
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        public int Create(WarrantyService warrantyService)
        {
            string sqlExpression = "INSERT INTO ГарантийноеОбслуживание " +
                                   "(idПокупки, idСервисЦентра, ПричинаОбращения, ДатаОбращения, Результат) " +
                                   "VALUES " +
                                   "(@idPurchase, @idServiceCenter, @reasonForPetition, @dateOfPetition, @result);" +
                                   "SELECT SCOPE_IDENTITY();";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddWithValue("@idPurchase", warrantyService.Purchase.Id);
                    command.Parameters.AddWithValue("@idServiceCenter", warrantyService.ServiceCenter.Id);
                    command.Parameters.AddWithValue("@reasonForPetition", warrantyService.ReasonForPetition);
                    command.Parameters.AddWithValue("@dateOfPetition", warrantyService.DateOfPetition);
                    if (warrantyService.Result == null)
                    {
                        command.Parameters.AddWithValue("@result", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@result", warrantyService.Result);
                    }
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public void Delete(int id)
        {
            string sqlExpression = "DELETE FROM ГарантийноеОбслуживание WHERE id = @Id";
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

        public WarrantyService GetById(int id)
        {
            string sqlExpression = "SELECT * FROM ГарантийноеОбслуживание " +
                                   "WHERE id = @id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo);
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                return RowToWarrantyService(dataTable.Rows[0]);
            }
        }

        public List<WarrantyService> GetAll()
        {
            string sqlExpression = "SELECT * FROM ГарантийноеОбслуживание";
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

        public List<WarrantyService> GetAllSortDate()
        {
            string sqlExpression = "SELECT * FROM ГарантийноеОбслуживание ORDER BY ДатаОбращения DESC";
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

        public List<WarrantyService> GetAllSortResult()
        {
            string sqlExpression = "SELECT * FROM ГарантийноеОбслуживание ORDER BY Результат";
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

        public WarrantyService FindByReason(string reason)
        {
            string sqlExpression = "SELECT TOP 1 * FROM ГарантийноеОбслуживание WHERE ПричинаОбращения LIKE @str";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@str", "%" + reason + "%");
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo);
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                if (dataTable.Rows.Count == 0)
                {
                    return null;
                }
                return RowToWarrantyService(dataTable.Rows[0]);
            }
        }

        public void UpdateById(WarrantyService warrantyService)
        {
            string sqlExpression = "UPDATE ГарантийноеОбслуживание " +
                                   "SET idПокупки = @idPurchase, " +
                                   "idСервисЦентра = @idServiceCenter, " +                     
                                   "ПричинаОбращения = @reasonForPetition, " +
                                   "ДатаОбращения = @dateOfPetition, " +
                                   "Результат = @result " +
                                   "WHERE id = @id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddWithValue("@id", warrantyService.Id);
                    command.Parameters.AddWithValue("@idPurchase", warrantyService.Purchase.Id);
                    command.Parameters.AddWithValue("@idServiceCenter", warrantyService.ServiceCenter.Id);
                    command.Parameters.AddWithValue("@reasonForPetition", warrantyService.ReasonForPetition);
                    command.Parameters.AddWithValue("@dateOfPetition", warrantyService.DateOfPetition);
                    if (warrantyService.Result == null)
                    {
                        command.Parameters.AddWithValue("@result", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@result", warrantyService.Result);
                    }
                    command.ExecuteNonQuery();
                }
            }
        }
        
        public WarrantyService RowToWarrantyService(DataRow dt)
        {
            PurchaseRepository purchaseRepo = new PurchaseRepository();
            ServiceCenterRepository serviceCenterRepo = new ServiceCenterRepository();

            WarrantyService warrantyService = new WarrantyService();
            warrantyService.Id = dt.Field<int>("id");
            warrantyService.Purchase = purchaseRepo.GetById(dt.Field<int>("idПокупки"));
            warrantyService.ServiceCenter = serviceCenterRepo.GetById(dt.Field<int>("idСервисЦентра"));
            warrantyService.ReasonForPetition = dt.Field<string>("ПричинаОбращения");
            warrantyService.DateOfPetition = dt.Field<DateTime>("ДатаОбращения");
            warrantyService.Result = dt.Field<string>("Результат");
            return warrantyService;
        }


        public List<WarrantyService> dataTableToList(DataTable dt)
        {
            List<WarrantyService> warrantyServiceList = new List<WarrantyService>();
            foreach (var warrantyService in dt.AsEnumerable())
            {
                warrantyServiceList.Add(RowToWarrantyService(warrantyService));
            }
            return warrantyServiceList;
        }
    }
}
