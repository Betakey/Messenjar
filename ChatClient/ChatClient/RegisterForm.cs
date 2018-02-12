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
        public string Username { get; private set; }

        private Database db;

        public RegisterForm()
        {
            db = new Database();
            InitializeComponent();
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(passwordTextBox.Text) || String.IsNullOrEmpty(usernameTextBox.Text))
            {
                MessageBox.Show("Username or Password is empty");
            }
            else
            {
                db.Register(usernameTextBox.Text, passwordTextBox.Text);
                usernameTextBox.Text = Username;
                MessageBox.Show("Registration Completed!");
            }
        }

        private void showPwCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (showPwCheckBox.Checked)
            {
                passwordTextBox.PasswordChar = '\0';
            }
            else
            {
                passwordTextBox.PasswordChar = char.Parse("*");
            }
        }
    }
}
