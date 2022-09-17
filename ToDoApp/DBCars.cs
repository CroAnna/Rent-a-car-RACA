using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDoApp
{
    class DBCars
    {
        // spajanje na bazu
        public static MySqlConnection GetConnection()
        {
            string sql = "datasource=localhost;port=3306;username=root;password=1234;database=rentappcar";
            MySqlConnection conn = new MySqlConnection(sql);
            try
            {
                conn.Open();
            }
            catch(MySqlException ex)
            {
                MessageBox.Show("MySql Connection!\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return conn;
        }

        // prikaz podataka u glavnoj tablici
        public static void DisplayData(string query, DataGridView dgv) 
        {
            string sql = query;
            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            adp.Fill(tbl);
            dgv.DataSource = tbl;
            conn.Close();
        }

        // prikaz podataka iz baze u dropdownu
        public static void LoadCompanies (ComboBox cbo)
        {
            string sql = "SELECT * FROM rentappcar.companies";
            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand (sql, conn);

            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cbo.Items.Add(dr.GetString("company_name"));
            }
        }

        // insert
        public static void AddCar(Car car)
        {
            string sql = "INSERT INTO cars VALUES(NULL, @CarCompany, @CarModel, @CarYear, @CarRented)"; // @ protiv SQL injectiona
            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@CarCompany", MySqlDbType.VarChar).Value = car.Company;
            cmd.Parameters.Add("@CarModel", MySqlDbType.VarChar).Value = car.Model;
            cmd.Parameters.Add("@CarYear", MySqlDbType.VarChar).Value = car.Year;
            cmd.Parameters.Add("@CarRented", MySqlDbType.Bit).Value = car.Rented;

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Added Successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(MySqlException ex)
            {
                MessageBox.Show("Car not inserted!\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.Close();
        }
    }
}
