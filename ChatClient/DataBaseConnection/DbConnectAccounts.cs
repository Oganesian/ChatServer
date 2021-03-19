using ChatClient.ClientConnection;
using MySql.Data.MySqlClient;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ChatClient.DataBaseConnection
{
    //public class DbConnectAccounts : DbContextBase
    //{
    //    public static async Task<Account> GetByEmail(string email)
    //    {
    //        using MySqlConnection connection = new MySqlConnection(CONNECTION_STRING);
    //        string queryString = "Select * from accounts where email=@email";

    //        MySqlCommand command = new MySqlCommand(queryString, connection);
    //        command.Parameters.AddWithValue("@email", email);

    //        connection.Open();
    //        using MySqlDataReader reader = command.ExecuteReader();
    //        if (reader.Read())
    //        {
    //            return new Account()
    //            {
    //                Id = int.Parse(reader["unique_id"].ToString()),
    //                PublicId = int.Parse(reader["id"].ToString()),
    //                Email = email,
    //                PasswordHash = reader["password_hash"].ToString(),
    //                Username = reader["username"].ToString()
    //            };
    //        }
    //        connection.Close();

    //        return null;
    //    }

    //    public static async Task<Account> GetByUsername(string username)
    //    {
    //        using MySqlConnection connection = new MySqlConnection(CONNECTION_STRING);
    //        string queryString = "Select * from accounts where username=@username";

    //        MySqlCommand command = new MySqlCommand(queryString, connection);
    //        command.Parameters.AddWithValue("@username", username);

    //        connection.Open();
    //        using MySqlDataReader reader = command.ExecuteReader();
    //        if (reader.Read())
    //        {
    //            return new Account()
    //            {
    //                Id = int.Parse(reader["unique_id"].ToString()),
    //                PublicId = int.Parse(reader["id"].ToString()),
    //                Email = reader["email"].ToString(),
    //                PasswordHash = reader["password_hash"].ToString(),
    //                Username = username
    //            };
    //        }
    //        connection.Close();

    //        return null;
    //    }

    //    public static async Task<Account> Write(Account account)
    //    {
    //        return await Write(account.PublicId, account.Username, account.Email, account.PasswordHash);
    //    }

    //    public static async Task<Account> Write(int id, string username, string email, string passwordHash)
    //    {
    //        try
    //        {

    //            using MySqlConnection connection = new MySqlConnection(CONNECTION_STRING);
    //            string queryString = "INSERT INTO accounts (unique_id, id, username, email, password_hash)";
    //            queryString += " VALUES (NULL, @id, @username, @email, @password_hash)";

    //            MySqlCommand command = new MySqlCommand(queryString, connection);

    //            command.Parameters.AddWithValue("@id", id);
    //            command.Parameters.AddWithValue("@username", username);
    //            command.Parameters.AddWithValue("@email", email);
    //            command.Parameters.AddWithValue("@password_hash", passwordHash);

    //            connection.Open();
    //            command.ExecuteNonQuery();
    //            connection.Close();
    //        }
    //        catch (Exception ex)
    //        {
    //            Debug.WriteLine(ex.Message);
    //            return null;
    //        }
    //        return await GetByEmail(email);
    //    }
    //}
}
