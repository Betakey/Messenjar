using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuiDLL
{
    public partial class ChatBox : UserControl
    {
        public Color BackgroundColor { get; set; }

        public Color ForegroundColor { get; set; }

        public Color FriendMessageColor { get; set; }

        public Color YourMessageColor { get; set; }

        public Color DateBoxColor { get; set; }

        public ChatBox()
        {
            InitializeComponent();
            BackColor = BackgroundColor;
            ForeColor = ForegroundColor;
        }

        public void AddChatContent(Dictionary<DateTime, List<ChatBoxEntry>> history, string yourName)
        {
            Controls.Clear();
            Dictionary<DateTime, List<ChatBoxEntry>> content = new Dictionary<DateTime, List<ChatBoxEntry>>();
            foreach (DateTime time in history.Keys.OrderByDescending(d => d))
            {
                content.Add(time, history[time]);
            }
            int y = 10;
            foreach (DateTime time in content.Keys)
            {
                MessageBubble timeBubble = new MessageBubble(Color.Cyan, Color.DimGray, Font, time.ToString("dd.MM.yy"), time, Width / 3, false);
                timeBubble.Location = new Point(Width / 2 - timeBubble.Width / 2, y);
                timeBubble.Visible = true;
                Controls.Add(timeBubble);
                y += 10 + timeBubble.Height;
                foreach (ChatBoxEntry entry in content[time])
                {
                    MessageBubble msgBubble = new MessageBubble(entry.Sender.ToLower().Equals(yourName.ToLower()) ? Color.Chartreuse : Color.CornflowerBlue, Color.Black, Font, entry.Message, time, (Width / 3) * 2, false);
                    msgBubble.Location = new Point(entry.Sender.ToLower().Equals(yourName.ToLower()) ? 10 : Width - 10 - msgBubble.Width, y);
                    msgBubble.Visible = true;
                    Controls.Add(msgBubble);
                    y += 10 + msgBubble.Height;
                }
            }
        }
    }
}
