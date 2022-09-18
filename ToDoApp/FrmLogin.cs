using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToDoApp.Models;

namespace ToDoApp
{
    public partial class FrmLogin : Form
    {
        public static User LoggedUser = null;
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoggedUser = DBUsers.CheckUser(txtUsername.Text, txtPassword.Text);
            if (LoggedUser != null)
            {
                // MessageBox.Show(LoggedUser.Id + LoggedUser.Username + LoggedUser.Password); // ispis podataka o uspjesno logiranom useru
                FrmCars frmCars = new FrmCars();
                Hide();
                frmCars.ShowDialog();
                Close();
            }            
        }
    }
}
