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
        public FrmUpdate()
        {
            InitializeComponent();
            DBCars.LoadCompanies(cboCompanyUpdate);
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
    }
}
