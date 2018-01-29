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
            date1.Add(new ChatBoxEntry("Markus", "Hi", new DateTime(2016, 1, 12, 10, 1, 12)));
            date1.Add(new ChatBoxEntry("Darki", "Hey", new DateTime(2016, 1, 12, 10, 1, 20)));
            date1.Add(new ChatBoxEntry("Markus", "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.", new DateTime(2016, 1, 12, 10, 1, 30)));
            date1.Add(new ChatBoxEntry("Darki", "TEst", new DateTime(2016, 1, 12, 10, 1, 40)));
            date1.Add(new ChatBoxEntry("Darki", "TEst 2", new DateTime(2016, 1, 12, 10, 1, 40)));
            date1.Add(new ChatBoxEntry("Darki", "T", new DateTime(2016, 1, 12, 10, 1, 40)));
            entries.Add(new DateTime(2016, 12, 28), date1);
            chatBox1.AddChatContent(entries, "Darki");
        }
    }
}
