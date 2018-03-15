using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
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
            if (File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\data\\data0.enc"))
            {
                string dec = Encryptor.DecryptText(File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\data\\data0.enc"), "ASDASKDAJDKFLFFGD4G455AF45SD4S124!=?$)=)sadfafawadsfgwgdhgesfaf54f4a6f45a 6asd4a5sd4q");
                passwordTextBox.Text = dec.Split(new string[] { "[!]" }, StringSplitOptions.None)[0];
                usernameTextBox.Text = dec.Split(new string[] { "[!]" }, StringSplitOptions.None)[1];
                rememberCheckBox.Checked = true;
            }
            db = new Database();
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
                byte[] imageBytes = null;
                string s = db.Login(usernameTextBox.Text, passwordTextBox.Text, out imageBytes);
                if (s != null)
                {
                    if (rememberCheckBox.Checked)
                    {
                        File.WriteAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\data\\data0.enc", 
                            Encryptor.EncryptText(passwordTextBox.Text + "[!]" + usernameTextBox.Text, "ASDASKDAJDKFLFFGD4G455AF45SD4S124!=?$)=)sadfafawadsfgwgdhgesfaf54f4a6f45a 6asd4a5sd4q"));
                    }
                    else
                    {
                        if(File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\data\\data0.enc"))
                        {
                            File.Delete(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\data\\data0.enc");
                        }
                    }
                    cForm = new ChatClientForm(db, usernameTextBox.Text, s, imageBytes);
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
