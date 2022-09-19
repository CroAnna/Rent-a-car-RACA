using MySql.Data.MySqlClient;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToDoApp.Models;

namespace ToDoApp
{
    class DBUsers
    {
        public static User user = null; // ovo bu taj logirani

        public static User CheckUser(string username, string password) // javna static funkcija, ali nije tipa void, nego tipa User jer vraca korisnika
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
                // successful login
                user = CreateUser(dr);
            }
            else
            {
                MessageBox.Show("Wrong information!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.Close();
            return user;
        }

        public static User RegisterNewUser(string username, string password, string role)
        {
            string sql = "INSERT INTO users VALUES(NULL, @Username, @Password, @Role)";
            MySqlConnection conn = DBCars.GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.Add("@Username", MySqlDbType.VarChar).Value = username;
            cmd.Parameters.Add("@Password", MySqlDbType.VarChar).Value = password;
            cmd.Parameters.Add("@Role", MySqlDbType.VarChar).Value = role;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("User not registred!\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return CheckUser(username, password);
        }

        // kreiranje objekta usera 
        public static User CreateUser(MySqlDataReader dr)
        {
            int log_id = int.Parse(dr["id_users"].ToString()); // iz baze pod ""
            string log_username = dr["username"].ToString();
            string log_password = dr["password"].ToString();
            string log_role = dr["role"].ToString();

            var user = new User
            {
                Id = log_id,
                Username = log_username,
                Password = log_password,
                Role = log_role
            };

            return user;
        }
    }
}
