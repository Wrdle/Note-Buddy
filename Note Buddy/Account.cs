using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Note_Buddy
{
    public class Account
    {
        string Username { get; set; }
        string Password { get; set; }

        public static bool Login(string username, string password)
        {
            DB db = new DB();
            db.ConnectNoteBuddy();

            string commandText = "SELECT username, password FROM [NoteBuddy].[dbo].[Users] WHERE Username = @username AND Password= @password;";
            SqlCommand command = new SqlCommand(commandText);
            command.Parameters.Add("@username", SqlDbType.NVarChar);
            command.Parameters.Add("@password", SqlDbType.NVarChar);
            command.Parameters["@username"].Value = username;
            command.Parameters["@password"].Value = Encryption.Encrypt(password);

            DataTable result = db.Select(command);

            return result.Rows.Count == 1 ? (result.Rows[0]["username"].ToString() == username && Encryption.Decrypt(result.Rows[0]["password"].ToString()) == password ? true : false) : false;
        }
    }
}
