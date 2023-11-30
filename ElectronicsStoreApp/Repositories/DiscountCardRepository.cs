using ElectronicsStoreApp.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System;

namespace ElectronicsStoreApp.Repositories
{
    public class DiscountCardRepository : IRepository<DiscountCard>
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        public int Create(DiscountCard discountCard)
        {
            string sqlExpression = "INSERT INTO ДисконтКарта (ФИО, ЭлПочта) VALUES(@fullName, @email);" +
                                   "SELECT SCOPE_IDENTITY();";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddWithValue("@fullname", discountCard.FullName);
                    if (discountCard.Email == null)
                    {
                        command.Parameters.AddWithValue("@email", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@email", discountCard.Email);
                    }
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public void Delete(int id)
        {
            string sqlExpression = "DELETE FROM ДисконтКарта " +
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

        public DiscountCard GetById(int id)
        {
            string sqlExpression = "SELECT * FROM ДисконтКарта " +
                                   "WHERE id = @id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo);
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                return RowToDiscountCard(dataTable.Rows[0]);
            }
        }

        public List<DiscountCard> GetAll()
        {
            string sqlExpression = "SELECT * FROM ДисконтКарта";
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

        public void UpdateById(DiscountCard discountCard)
        {
            string sqlExpression = "UPDATE ДисконтКарта " +
                                   "SET ФИО = @fullName," +
                                   "    ЭлПочта = @email " +
                                   "WHERE id = @id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddWithValue("@id", discountCard.Id);
                    command.Parameters.AddWithValue("@fullName", discountCard.FullName);
                    if (discountCard.Email == null)
                    {
                        command.Parameters.AddWithValue("@email", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@email", discountCard.Email);
                    }
                    command.ExecuteNonQuery();
                }
            }
        }

        public DiscountCard RowToDiscountCard(DataRow dt)
        {
            DiscountCard discountCard = new DiscountCard();
            discountCard.Id = dt.Field<int>("id");
            discountCard.FullName = dt.Field<string>("ФИО");
            discountCard.Email = dt.Field<string>("ЭлПочта");
            return discountCard;
        }


        public List<DiscountCard> dataTableToList(DataTable dt)
        {
            List<DiscountCard> discountCardList = new List<DiscountCard>();
            foreach (var discountCard in dt.AsEnumerable())
            {
                discountCardList.Add(RowToDiscountCard(discountCard));
            }
            return discountCardList;
        }
    }
}
