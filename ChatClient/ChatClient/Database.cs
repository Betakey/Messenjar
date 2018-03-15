using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Drawing;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace ChatClient
{
    public class Database
    {
        public string ConnectionString { get; private set; }

        public MySqlConnection Connection { get; private set; }

        public bool exists; // Boolean that checks if a user exisits

        public Database()
        {
            ConnectionString = "SERVER=gethercode.de;UID=MessenJarAdmin;PASSWORD=sUg4n?89;DATABASE=MessenJarDB";
            Connection = new MySqlConnection(ConnectionString);
            exists = false;
        }

        /// <summary>
        /// Opens a connnection to the database
        /// </summary>
        
        public void OpenConnection()
        {
            try
            {
                Connection.Open();
            }
            catch (Exception)
            {
                CloseConnection();
                OpenConnection();
            }
        }

        /// <summary>
        /// Updates friends in the Database.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="friends"></param>
        
        public void UpdateFriends(string name, string friends)
        {
            OpenConnection();
            using (MySqlCommand cmd = new MySqlCommand("UPDATE User SET Friends = @friends WHERE Name = @name", Connection))
            {
                cmd.Parameters.Add("@friends", MySqlDbType.Text);
                cmd.Parameters["@friends"].Value = friends;
                cmd.Parameters.Add("@name", MySqlDbType.Text);
                cmd.Parameters["@name"].Value = name;
                cmd.ExecuteNonQuery();
            }
            CloseConnection();
        }

        /// <summary>
        /// Closes the connection to the database.
        /// </summary>
        
        public void CloseConnection()
        {
            Connection.Close();
        }

        /// <summary>
        /// Gets the ProfilImage from the Database.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Bitmap or Null</returns>
        
        public Bitmap GetProfileImage(string name)
        {
            OpenConnection();
            using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM User WHERE Name = @name", Connection))
            {
                cmd.Parameters.Add("@name", MySqlDbType.Text);
                cmd.Parameters["@name"].Value = name;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        byte[] bytes = reader["ProfileImage"] as byte[];
                        CloseConnection();
                        if (bytes == null || bytes.Length <= 0) return null;
                        MemoryStream stream = new MemoryStream(bytes);
                        Bitmap bitmap = (Bitmap)Bitmap.FromStream(stream);
                        stream.Close();
                        return bitmap;
                    }
                }
            }
            CloseConnection();
            return null;
        }

        /// <summary>
        /// Updates the profilImage in the Database.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="name"></param>
        
        public void ChangePicture(byte[] bytes, string name)
        {
            OpenConnection();
            using (MySqlCommand cmd = new MySqlCommand("UPDATE User SET ProfileImage = @bytes WHERE Name = @name", Connection))
            {
                cmd.Parameters.Add("@bytes", MySqlDbType.LongBlob);
                cmd.Parameters["@bytes"].Value = bytes;
                cmd.Parameters.Add("@name", MySqlDbType.Text);
                cmd.Parameters["@name"].Value = name;
                cmd.ExecuteNonQuery();
            }
            CloseConnection();
        }

        /// <summary>
        /// Checks if user is already existing in the database
        /// </summary>
        /// <param name="name"></param>
        /// <returns>true or false</returns>
        
        public bool IsUserExisting(string name)
        {
            OpenConnection();
            using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM User WHERE Name = @name", Connection))
            {
                cmd.Parameters.Add("@name", MySqlDbType.Text);
                cmd.Parameters["@name"].Value = name;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        reader.Close();
                        CloseConnection();
                        return true;
                    }
                }
            }
            CloseConnection();
            return false;
        }

        /// <summary>
        /// Registration for a User.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>true or false</returns>
        
        public bool Register(string userName, string password)  
        {
            if (IsUserExisting(userName))
            {
                MessageBox.Show("Der Benutzername existiert bereits!", "Fehler!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                OpenConnection();
                using (MySqlCommand command = new MySqlCommand(
                            "INSERT INTO User (Name, Password, Friends) VALUES (@name, @pw, @fr)", Connection))
                {
                    command.Parameters.Add("@name", MySqlDbType.Text);
                    command.Parameters["@name"].Value = userName;
                    command.Parameters.Add("@pw", MySqlDbType.Text);
                    command.Parameters["@pw"].Value = CalculateMD5(password);
                    command.Parameters.Add("@fr", MySqlDbType.Text);
                    command.Parameters["@fr"].Value = "Darki";
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Registration Completed!", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CloseConnection();
                return true;
            }
        }

        /// <summary>
        /// Login for a user checks if user has friends.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="imageBytes"></param>
        /// <returns>friends or null</returns>
        
        public string Login(string username, string password, out byte[] imageBytes)
        {
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
                    {
                        string friends = reader["Friends"] as string;
                        if (friends == null) friends = "";
                        byte[] bytes = reader["ProfileImage"] as byte[];
                        imageBytes = bytes;
                        return friends;
                    }
                }
            }
            CloseConnection();
            imageBytes = null;
            return null;
        }

        /// <summary>
        /// Calculates MD5 Hash to Hash Password
        /// </summary>
        /// <param name="input">The decrypted Password to hash</param>
        /// <returns>The hashed Password</returns>
        
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
