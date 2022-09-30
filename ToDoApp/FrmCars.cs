using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDoApp
{
    public partial class FrmCars : Form
    {
        Car selectedCar;
        int selectedCarId = 1; // slozi da je ovo prvi u tablici

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
            selectedCar = DBCars.FindCar(selectedCarId);
            FrmUpdate frmUpdate = new FrmUpdate(selectedCar);
            // ili skraceno: FrmUpdate frmUpdate = new FrmUpdate(DBCars.FindCar(selectedCarId));

            Hide();
            frmUpdate.ShowDialog();
            Close();                
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to log out?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Program.LoggedUser = null;
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
            DataGridViewRow selectedCarRow = dgvCars.Rows[index];
            selectedCarId = (int)selectedCarRow.Cells[0].Value;
        }

        private void btnRent_Click(object sender, EventArgs e)
        {
            selectedCar = DBCars.FindCar(selectedCarId);
            if (selectedCar.Rented == true) 
            {
                MessageBox.Show("Car already rented!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (MessageBox.Show("Are you sure that you want to rent " + selectedCar.Company + " - " + selectedCar.Model + "?", "Info", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
            {
                DBCars.RentReturnCar(selectedCar.Id, selectedCar.Rented);              

                FrmCars frmCars = new FrmCars(); // refresh
                Hide();
                frmCars.ShowDialog();
                Close();
            }
        }

        private void FrmCars_Load(object sender, EventArgs e)
        {
            if(Program.LoggedUser.Role != "worker")
            {
                btnAddNew.Visible = false;
                btnUpdate.Visible = false;
            }
        }
    }
}
