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

        private void login()
        {
            if (txtUsername.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Please fill in all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                LoggedUser = DBUsers.CheckUser(txtUsername.Text, txtPassword.Text);
                if (LoggedUser != null)
                {
                    // MessageBox.Show(LoggedUser.Id + LoggedUser.Username + LoggedUser.Password + LoggedUser.Role); // ispis podataka o uspjesno logiranom useru
                    FrmCars frmCars = new FrmCars();
                    Hide();
                    frmCars.ShowDialog();
                    Close();
                }
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            login();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                login();
            }
        }

        private void lblRegistration_Click(object sender, EventArgs e)
        {
            FrmRegistration frmRegistration = new FrmRegistration();
            Hide();
            frmRegistration.ShowDialog();
            Close();
        }

   
    }
}
