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
    public partial class FrmRegistration : Form
    {
        public FrmRegistration()
        {
            InitializeComponent();
        }

        private void lblLogin_Click(object sender, EventArgs e)
        {
            FrmLogin frmLogin = new FrmLogin();
            Hide();
            frmLogin.ShowDialog();
            Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if(txtUsername.Text == "" || txtPassword.Text == "" || (!rbnCustomer.Checked && !rbnWorker.Checked))
            {
                MessageBox.Show("Please fill in all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (txtPassword.Text != txtRepeatPassword.Text)
                {
                    MessageBox.Show("Passwords do not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if(DBUsers.UsernameExists(txtUsername.Text) == true)
                    {
                        MessageBox.Show("Username already exists!");
                    }
                    else
                    {
                        string role = null;
                        DBUsers.RegisterNewUser(txtUsername.Text, txtPassword.Text, role = rbnCustomer.Checked ? "customer" : "worker");
                        MessageBox.Show("Registred!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        FrmCars frmCars = new FrmCars();
                        Hide();
                        frmCars.ShowDialog();
                        Close();
                    }
                 

                }
            }
        }
    }
}
