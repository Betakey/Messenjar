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
    public partial class FriendsList : UserControl
    {

        public FriendsList()
        {
            InitializeComponent();
            AutoScroll = true;

        }

        public void Update(List<FriendEntry> list)
        {
            Controls.Clear();
            int y = 0;
            foreach (FriendEntry entry in list)
            {
                FriendBubble bubble = new FriendBubble(entry);
                bubble.Location = new Point(0, y);
                bubble.Size = new System.Drawing.Size(Width, 100);
                bubble.Visible = true;
                Controls.Add(bubble);
                y += 100;
            }
        }

        public void ShowNotify(string friendName)
        {
            foreach (Control control in Controls)
            {
                if (control is FriendBubble)
                {
                    FriendBubble bubble = (FriendBubble)control;
                    if (bubble.Entry.Name == friendName)
                    {
                        bubble.NotifyEnabled = true;
                        break;
                    }
                }
            }
        }
    }
}
