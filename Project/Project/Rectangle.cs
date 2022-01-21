using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Project
{
    [Serializable]
   public class Rectangle:Shape
    {
        protected int _width { get; set; }
        protected int _heigth { get; set; }
        public Point Location { get; set; }
        public bool Fill { get; set; } = true;
        public bool Selected { get; set; }
        public int Width 
        { 
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }
        public int Heigth 
        {
            get
            {
                return _heigth;
            }
            set 
            {
                _heigth = value;
            }
        }
        public Color Color { get; set; }

        public override int GetArea()
        {
            return Heigth * Width;
        }
        public override int GetPerimeter()
        {
            return 2 * Width + 2 * Heigth;
        }
        public void Paint(Graphics g)
        {
            if (Fill)
            {
                var fillColor = Color.FromArgb(
                            120,
                            Color);
                using (var brush = new SolidBrush(fillColor))
                    g.FillRectangle(brush, Location.X, Location.Y, Width, Heigth);
            }


            using (var pen = new Pen(Color, 3))
                g.DrawRectangle(pen, Location.X, Location.Y, Width, Heigth);
        }
        public bool Contains(Point p)
        {
            return Location.X < p.X && p.X < Location.X + Width &&
                Location.Y < p.Y && p.Y < Location.Y + Heigth;
        }

        public bool Intersects(Rectangle rectangle)
        {
            return this.Location.X < rectangle.Location.X + rectangle.Width && rectangle.Location.X < this.Location.X + this.Width &&
                this.Location.Y < rectangle.Location.Y + rectangle.Heigth && rectangle.Location.Y < this.Location.Y + this.Width;
        }
        
    }
}
