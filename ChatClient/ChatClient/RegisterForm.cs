using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class RegisterForm : Form
    {

        Database db;
        bool check;

        public RegisterForm()
        {
            db = new Database();
            InitializeComponent();
            check = false;
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(passwordTextBox.Text) || String.IsNullOrEmpty(usernameTextBox.Text))
            {
                check = true;
                MessageBox.Show("Username or Password is empty");
            }
            if (check == false)
            {
                db.Register(usernameTextBox.Text, passwordTextBox.Text);
                MessageBox.Show("Registration Completed!");
            }
        }
    }
}
