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
        private ChatClientForm cForm;
        

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
                string s = db.Login(usernameTextBox.Text, passwordTextBox.Text);
                if (s != null)
                {
                    cForm = new ChatClientForm(usernameTextBox.Text, s);
                    cForm.Show();
                    Hide();
                }
            }
          
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            new RegisterForm().ShowDialog();
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
