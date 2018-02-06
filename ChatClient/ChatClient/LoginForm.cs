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
    public partial class LoginForm : Form
    {
        Database db;
        public LoginForm()
        {
            InitializeComponent();
            db = new Database();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            bool check = true;

            if (String.IsNullOrEmpty(passwordTextBox.Text) || String.IsNullOrEmpty(usernameTextBox.Text))
            {
                MessageBox.Show("Username or Password is empty");
                check = false;
            }
            if(check == true)
            {
                db.Login(usernameTextBox.Text, passwordTextBox.Text);
            }
        }
    }
}
