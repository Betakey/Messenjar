using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GuiDLL;

namespace ChatClient
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<DateTime, List<ChatBoxEntry>> entries = new Dictionary<DateTime, List<ChatBoxEntry>>();
            List<ChatBoxEntry> date1 = new List<ChatBoxEntry>();
            date1.Add(new ChatBoxEntry("Markus", "Hi", new DateTime(0, 0, 0, 10, 1, 12)));
            date1.Add(new ChatBoxEntry("Darki", "Hey", new DateTime(0, 0, 0, 10, 1, 20)));
            entries.Add(new DateTime(2016, 12, 28), date1);
            chatBox1.AddChatContent(entries, "Darki");
        }
    }
}
