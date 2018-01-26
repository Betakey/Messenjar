using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ChatServer.IO
{
    public class Config
    {
        private string path;
        private string dirPath;
        private Dictionary<ConfigKey, object> configContent;

        public Config()
        {
            dirPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\data";
            if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
            path = dirPath + "\\config.xml";
            Init();
        }

        /// <summary>
        /// Initializes the Config:
        /// - If Config doesn't exist, it gets created and filled with the default values
        /// - If Config exists the content dictonary will be filled with the values of the config
        /// </summary>
        private void Init()
        {
            configContent = new Dictionary<ConfigKey, object>();
            if (!File.Exists(path))
            {
                configContent.Add(ConfigKey.IPType, "localhost");
                configContent.Add(ConfigKey.Port, "34563");
                Save();
            }
            else
            {
                XmlDocument document = new XmlDocument();
                document.Load(path);
                foreach (ConfigKey key in Enum.GetValues(typeof(ConfigKey)))
                {
                    XmlNodeList list = document.GetElementsByTagName(key.ToString());
                    if (list.Count > 0)
                    {
                        XmlNode node = list[0];
                        if (node != null)
                        {
                            configContent.Add(key, node.InnerText);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Sets a new value of the Config Key
        /// </summary>
        public void Set(ConfigKey key, object value)
        {
            configContent[key] = value;
            Save();
        }

        /// <summary>
        /// Returns the Config Key as bool
        /// </summary>
        public bool AsBool(ConfigKey key)
        {
            return Boolean.Parse(AsString(key));
        }

        /// <summary>
        /// Returns the Config Key as string
        /// </summary>
        public string AsString(ConfigKey key)
        {
            return AsObject(key) as string;
        }

        /// <summary>
        /// Returns the Config Key as int
        /// </summary>
        public int AsInt(ConfigKey key)
        {
            return Int32.Parse(AsString(key));
        }

        /// <summary>
        /// Returns the Config Key as double
        /// </summary>
        public double AsDouble(ConfigKey key)
        {
            return Double.Parse(AsString(key));
        }

        /// <summary>
        /// Returns the Config Key as object
        /// </summary>
        public object AsObject(ConfigKey key)
        {
            return configContent[key];
        }

        /// <summary>
        /// Saves the Config
        /// </summary>
        public void Save()
        {
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace
            };
            using (XmlWriter writer = XmlWriter.Create(File.Open(path, FileMode.OpenOrCreate), settings))
            {
                writer.WriteStartElement("settings");
                foreach (ConfigKey key in configContent.Keys)
                {
                    object value = configContent[key];
                    writer.WriteElementString(key.ToString(), value.ToString());
                }
                writer.WriteEndElement();
                writer.Flush();
                writer.Close();
            }
        }
    }
}
