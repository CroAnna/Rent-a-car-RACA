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
    public partial class FrmAddCar : Form
    {
        

        public FrmAddCar()
        {
            InitializeComponent();
        }

        public void closeAndRefresh()
        {
            FrmCars frmCars = new FrmCars();
            Hide();
            frmCars.ShowDialog();
            Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            Car newCar = new Car(txtCompany.Text, txtModel.Text, Int32.Parse(txtYear.Text), Convert.ToBoolean(0));
            DBCars.AddCar(newCar);
            closeAndRefresh();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            closeAndRefresh();
        }
    }
}
