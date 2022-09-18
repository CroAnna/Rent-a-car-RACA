using MySql.Data.MySqlClient;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDoApp
{
    class DBUsers
    {
        public static void CheckUser(string username, string password)
        {
            string sql = "SELECT * FROM users WHERE username = @username && password = @password";
            MySqlConnection conn = DBCars.GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.Add("@username", MySqlDbType.VarChar).Value = username;
            cmd.Parameters.Add("@password", MySqlDbType.VarChar).Value = password;

            MySqlDataReader dr = cmd.ExecuteReader();
            // ExecuteReader is used for any result set with multiple rows/columns (e.g., SELECT col1, col2 from sometable ).
            // ExecuteNonQuery is typically used for SQL statements without results (e.g., UPDATE, INSERT, etc.)

            if (dr.Read())
            {
                MessageBox.Show("Logged in!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            else
            {
                MessageBox.Show("Wrong information!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.Close();
        }

    }
}
