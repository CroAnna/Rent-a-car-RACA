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

        public static User LoggedUser { get; set; }

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            DBUsers.CheckUser(txtUsername.Text, txtPassword.Text);
            //????
        
        }
    }
}
