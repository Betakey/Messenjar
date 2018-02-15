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
        private Database db;
        private RegisterForm rForm;
        private ChatClientForm cForm;
        

        public LoginForm()
        {
            InitializeComponent();
            db = new Database();
            rForm = new RegisterForm();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void loginButton_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(usernameTextBox.Text))
            {
                MessageBox.Show("Username is empty", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (String.IsNullOrEmpty(passwordTextBox.Text))
            {
                MessageBox.Show("Password is empty", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                db.Login(usernameTextBox.Text, passwordTextBox.Text);
                cForm = new ChatClientForm();
                cForm.Show();
            }
          
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            rForm.Show();
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
