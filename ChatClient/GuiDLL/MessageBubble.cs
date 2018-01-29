using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuiDLL
{
    public class MessageBubble : Panel
    {
        private SolidBrush brush;
        private Color color;
        private string text;
        private DateTime time;
        private int edge;
        private int maxWidth;
        private bool timeVisible;
        private Font font;
        private Graphics graphics;

        public MessageBubble(Color color, Color foreColor, Font font, string text, DateTime time, int maxWidth, bool timeVisible = true, int edge = 50)
        {
            this.timeVisible = timeVisible;
            this.color = color;
            BackColor = color;
            brush = new SolidBrush(foreColor);
            this.text = text;
            this.edge = edge;
            this.font = font;
            this.time = time;
            this.maxWidth = maxWidth;
            graphics = CreateGraphics();  
            GenerateSize(text);
        }

        private void GenerateSize(string input)
        {
            SizeF textSize = graphics.MeasureString(input, font, maxWidth);
            if (textSize.Width + 20 < maxWidth)
            {
                Size = new Size((int)Math.Round(textSize.Width) + 20, (int)Math.Round(textSize.Height) + 20);
            }
            else
            {
                Size = new Size(maxWidth, (int)Math.Round(textSize.Height) + 20);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawString(text, font, brush, new Rectangle(10, 10, Size.Width, Size.Height));
        }
    }
}
