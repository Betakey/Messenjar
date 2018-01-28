using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuiDLL
{
    public class MessageBubble : Panel
    {
        private SolidBrush brush;
        private Pen pen;
        private string text;
        private DateTime time;
        private int edge;
        private int maxWidth;
        private bool timeVisible;
        private Font font;

        public MessageBubble(Color color, Color foreColor, Font font, string text, DateTime time, int maxWidth, bool timeVisible = true, int edge = 50)
        {
            this.timeVisible = timeVisible;
            pen = new Pen(color, 2f);
            BackColor = color;
            brush = new SolidBrush(foreColor);
            this.text = text;
            this.edge = edge;
            this.font = font;
            this.time = time;
            this.maxWidth = maxWidth;
            GenerateSize(text);
        }

        private void GenerateSize(string input)
        {
            Size textSize = TextRenderer.MeasureText(input, font);
            if (textSize.Width + 20 < maxWidth)
            {
                Size = new Size(textSize.Width + 20, textSize.Height + 20);
            }
            else
            {
                Size = new Size(maxWidth, textSize.Height + 20);
            }
        }

        private Rectangle GetLeftUpper(int e)
        {
            return new Rectangle(0, 0, e, e);
        }

        private Rectangle GetRightUpper(int e)
        {
            return new Rectangle(Width - e, 0, e, e);
        }

        private Rectangle GetRightLower(int e)
        {
            return new Rectangle(Width - e, Height - e, e, e);
        }

        private Rectangle GetLeftLower(int e)
        {
            return new Rectangle(0, Height - e, e, e);
        }

        private void ExtendedDraw(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.StartFigure();
            path.AddArc(GetLeftUpper(edge), 180, 90);
            path.AddLine(edge, 0, Width - edge, 0);
            path.AddArc(GetRightUpper(edge), 270, 90);
            path.AddLine(Width, edge, Width, Height - edge);
            path.AddArc(GetRightLower(edge), 0, 90);
            path.AddLine(Width - edge, Height, edge, Height);
            path.AddArc(GetLeftLower(edge), 90, 90);
            path.AddLine(0, Height - edge, 0, edge);
            path.CloseFigure();
            Region = new Region(path);
        }

        private void DrawSingleBorder(Graphics graphics)
        {
            graphics.DrawArc(pen, new Rectangle(0, 0, edge, edge), 180, 90);
            graphics.DrawArc(pen, new Rectangle(Width - edge - 1, -1, edge, edge), 270, 90);
            graphics.DrawArc(pen, new Rectangle(Width - edge - 1, Height - edge - 1, edge, edge), 0, 90);
            graphics.DrawArc(pen, new Rectangle(0, Height - edge - 1, edge, edge), 90, 90);
            graphics.DrawRectangle(pen, 0F, 0F, (float)(Width - 1), (float)(Height - 1));
        }

        private void DrawBorder(Graphics graphics)
        {
            DrawSingleBorder(graphics);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ExtendedDraw(e);
            DrawBorder(e.Graphics);
            e.Graphics.DrawString(text, font, brush, new Rectangle(10, 10, Size.Width - 20, Size.Height - 20));
        }
    }
}
