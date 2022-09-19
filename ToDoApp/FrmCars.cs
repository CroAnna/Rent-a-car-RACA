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
            DBCars.DisplayData("SELECT id_car, company, model, year, rented FROM cars", dgvCars); 
            // stupci iz baze 
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            FrmUpdate frmUpdate = new FrmUpdate();
            Hide();
            frmUpdate.ShowDialog();
            Close();
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
    }
}
