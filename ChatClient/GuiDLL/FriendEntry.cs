using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GuiDLL
{
    public class FriendEntry
    {
        public string Name { get; private set; }

        public Bitmap Image { get; private set; }

        public FriendEntry(string name, Bitmap image = null)
        {
            Name = name;
            Image = image == null ? Properties.Resources.profile : image;
        }
    }
}
