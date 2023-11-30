using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace ElectronicsStoreApp
{
    public class Authorization
    {
        public static string Role { get; set; }
        public static string Login { get; set; }
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        public static bool Authorize(string login, string password)
        {
            string sqlExpression1 = "SELECT Роль FROM Пользователи WHERE Логин = @login and Пароль = @password";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression1, connection);
                command.Parameters.AddWithValue("@login", login);
                command.Parameters.AddWithValue("@password", password);
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo);
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                if (dataTable.Rows.Count == 0)
                {
                    return false;
                }
                Role = dataTable.Rows[0].Field<string>("Роль");
                Login = login;
                return true;
            }
        }
    }
}
