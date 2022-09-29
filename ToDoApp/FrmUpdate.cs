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
    public partial class FrmUpdate : Form
    {
        private Car car;

        public FrmUpdate(Car selectedCar)
        {
            InitializeComponent();
            DBCars.LoadCompanies(cboCompanyUpdate);
            car = selectedCar;
        }

        public void closeAndRefresh()
        {
            FrmCars frmCars = new FrmCars();
            Hide();
            frmCars.ShowDialog();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            closeAndRefresh();
        }

        private void FrmUpdate_Load(object sender, EventArgs e)
        {
            txtModelUpdate.Text = car.Model;
            txtYearUpdate.Text = (car.Year).ToString();
            cboCompanyUpdate.Text = car.Company;
            cbRented.Checked = car.Rented;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure that you want to delete " + car.Company + " - " + car.Model + "?", "Info", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DBCars.DeleteCar(car.Id);
                closeAndRefresh();
            }
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DBCars.UpdateCar(car.Id, cboCompanyUpdate.Text, txtModelUpdate.Text, Int32.Parse(txtYearUpdate.Text), cbRented.Checked);
            closeAndRefresh();
        }
    }
}
