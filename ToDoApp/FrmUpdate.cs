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
        private Car newcar;

        public FrmUpdate(Car selectedCar)
        {
            InitializeComponent();
            DBCars.LoadCompanies(cboCompanyUpdate);
            newcar = selectedCar;
       
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
            txtModelUpdate.Text = newcar.Model;
        }
    }
}
