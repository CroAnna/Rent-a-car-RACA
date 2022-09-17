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
    }
}
