using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToDoApp.Models;

namespace ToDoApp
{
    class DBCars
    {
        public static Car selectedCar = null;

        // spajanje na bazu
        public static MySqlConnection GetConnection()
        {
            string sql = "datasource=localhost;port=3306;username=root;password=1234;database=rentappcar"; // datasource=127.0.0.1
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
            cmd.CommandType = CommandType.Text; // default

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

        public static void UpdateCar(int id)
        {
            selectedCar = FindCar(id);
            MessageBox.Show(selectedCar.Model);
        }

        public static Car FindCar(int id)
        {
            string sql = "SELECT * FROM cars WHERE id_car = @CarID";
            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            // cmd.CommandType = CommandType.Text; // using SystemData
            cmd.Parameters.Add("@CarID", MySqlDbType.VarChar).Value = id;
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                selectedCar = CreateCar(dr);
            }
            else
            {
                MessageBox.Show("Error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                selectedCar = null;
            }
            conn.Close();
            return selectedCar;
        }

        public static Car CreateCar(MySqlDataReader dr)
        {
            // promijeni ovo
            int log_id = int.Parse(dr["id_car"].ToString()); // iz baze pod ""
            string log_company = dr["company"].ToString();
            string log_model = dr["model"].ToString();
            string log_year = dr["year"].ToString();
            string log_rented = dr["rented"].ToString();

            var car = new Car
            {
                Id = log_id,
                Company = log_company,
                Model = log_model,
                Year = Int32.Parse(log_year),
                //Rented = Convert.ToBoolean(log_rented)
            };

            return car;
        }

        // void deleteCar
        public static void DeleteCar(int id)
        {
            string sql = "DELETE * FROM cars WHERE id_car = @CarID";
            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text; // using SystemData
            cmd.Parameters.Add("@CarID", MySqlDbType.VarChar).Value = id;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Deleted Successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Car not deleted!\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.Close();
        }
    }
}
