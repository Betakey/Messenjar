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
    public sealed partial class FriendsList : UserControl
    {
        public event Action<object, MouseEventArgs, FriendEntry> BubbleClick;
        public FriendEntry Selected
        {
            get
            {
                foreach(FriendBubble bubble in Controls)
                {
                    if (bubble.IsSelected)
                    {
                        return bubble.Entry;
                    }
                }
                return null;
            }
        }

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
                bubble.Size = new Size(Width, 100);
                bubble.Visible = true;
                bubble.BubbleClicked += (args) =>
                {
                    ResetSelection();
                    bubble.IsSelected = true;
                    BubbleClick?.Invoke(this, args, entry);
                };
                Controls.Add(bubble);
                y += 100;
            }
        }

        public void ResetSelection()
        {
            foreach (Control control in Controls)
            {
                if (control is FriendBubble)
                {
                    FriendBubble bubble = (FriendBubble)control;
                    if (bubble.IsSelected)
                    {
                        bubble.IsSelected = false;
                        break;
                    }
                }
            }
        }

        public void HideNotify(string friendName)
        {
            foreach (Control control in Controls)
            {
                if (control is FriendBubble)
                {
                    FriendBubble bubble = (FriendBubble)control;
                    if (bubble.Entry.Name == friendName)
                    {
                        bubble.NotifyEnabled = false;
                        break;
                    }
                }
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
