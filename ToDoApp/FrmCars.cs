using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDoApp
{
    public partial class FrmCars : Form
    {
        Car selectedCar;
        int selectedCarId;

        public FrmCars()
        {
            InitializeComponent();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            FrmAddCar frmAddCar = new FrmAddCar();
            Hide(); 
            frmAddCar.ShowDialog(); 
            Close();
        }

        private void FrmCars_Shown(object sender, EventArgs e)
        {
            DBCars.DisplayData("SELECT * FROM cars", dgvCars); // ili "SELECT id_car, company, model, year, rented FROM cars" --> stupci iz baze 
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // MessageBox.Show(dgvCars.CurrentCell.ColumnIndex.ToString());
            // string proba = dgvCars.Rows[e.RowIndex].Cells["id"].Value.ToString();
          
            MessageBox.Show(selectedCarId.ToString());
            if (selectedCarId != null)
            {
                DBCars.UpdateCar(selectedCarId);
                // MessageBox.Show(selectedCar.Model.ToString());
            }
            else
            {

                MessageBox.Show("prazno");
            }
            // FrmUpdate frmUpdate = new FrmUpdate(selectedCar);
            // Hide();
            // frmUpdate.ShowDialog();
            // Close();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to log out?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                FrmLogin.LoggedUser = null;
                FrmLogin frmLogin = new FrmLogin();
                Hide();
                frmLogin.ShowDialog();
                Close();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DBCars.DisplayData("SELECT * FROM cars WHERE LOWER(model) LIKE LOWER('%" + txtSearch.Text + "%')", dgvCars); // case insensitive
        }

        private void cbNotRented_CheckedChanged(object sender, EventArgs e)
        {
            if (cbNotRented.Checked)
            {
                DBCars.DisplayData("SELECT * FROM cars WHERE rented = 0", dgvCars);
            }
            else
            {
                DBCars.DisplayData("SELECT * FROM cars", dgvCars);
            }
            

        }

        private void dgvCars_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            // MessageBox.Show(index.ToString());
            DataGridViewRow selectedCarRow = dgvCars.Rows[index];
            selectedCarId = (int)selectedCarRow.Cells[0].Value;
            MessageBox.Show("id: " + selectedCarId.ToString()); // spremi se ID od odabranog auta

            // Car selectedCar = new Car(selectedCarId.Cells[1].Value.ToString(), selectedCarId.Cells[2].Value.ToString(), Int32.Parse(selectedCarId.Cells[3].Value), selectedCarId.Cells[4].Value);
        }
    }
}
