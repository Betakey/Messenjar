using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace ChatClient
{
    public class Database
    {
        public string ConnectionString { get; private set; }

        public MySqlConnection Connection { get; private set; }

        public bool exists;

        public Database()
        {
            ConnectionString = "SERVER=gethercode.de;UID=MessenJarAdmin;PASSWORD=sUg4n?89;DATABASE=MessenJarDB";
            Connection = new MySqlConnection(ConnectionString);
            exists = false;
        }

        public void OpenConnection()
        {
            try
            {
                Connection.Open();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CloseConnection()
        {
            Connection.Close();
        }

        public void Register(string userName, string password)  
        {
            OpenConnection();
            using (MySqlCommand cmd = new MySqlCommand("SELECT * from User where Name = @name", Connection))
            {
                cmd.Parameters.Add("@name", MySqlDbType.Text);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        exists = true;
                        MessageBox.Show("This username has been used.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            if (exists == false)
            {
                using (MySqlCommand command = new MySqlCommand(
                    "INSERT INTO User (Name, Password) VALUES (@name, @pw)", Connection))
                {
                    command.Parameters.Add("@name", MySqlDbType.Text);
                    command.Parameters["@name"].Value = userName;
                    command.Parameters.Add("@pw", MySqlDbType.Text);
                    command.Parameters["@pw"].Value = CalculateMD5(password);
                }
                MessageBox.Show("Registration Completed!", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            CloseConnection();
        }

        public bool Login(string username, string password)
        {
            bool b = false;
            OpenConnection();
            using (MySqlCommand command = new MySqlCommand("SELECT * FROM User WHERE Name = @name AND Password = @pw", Connection))
            {
                command.Parameters.Add("@name", MySqlDbType.Text);
                command.Parameters["@name"].Value = username;
                command.Parameters.Add("@pw", MySqlDbType.Text);
                command.Parameters["@pw"].Value = CalculateMD5(password);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        b = true;
                }
            }
            CloseConnection();
            return b;
        }

        private string CalculateMD5(string input)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);

            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)

            {

                sb.Append(hash[i].ToString("X2"));

            }

            return sb.ToString();
        }
    }
}
