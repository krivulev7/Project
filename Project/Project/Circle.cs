using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Project
{
    [Serializable]
   public class Circle:Shape
    {
        public Point Location { get; set; }
        protected float _radius { get; set; }
        public Point MouseClick { get; set; }
        public bool Fill { get; set; } = true;
        public Color Color { get; set; }

        public float Radius
        {
            get
            {
                return _radius;
            }
            set
            {
                _radius = value;
            }
        }
        public override int GetArea()
        {
            return (int)(Radius * Radius * Math.PI);
        }
        public override int GetPerimeter()
        {
            return (int) (2 * Math.PI * Radius);
        }
        public void Paint(Graphics g)
        {
            var fillColor = Color.FromArgb(
                    Math.Min(byte.MaxValue, Color.R / 2),
                    Math.Min(byte.MaxValue, Color.G / 2),
                    Math.Min(byte.MaxValue, Color.B / 2));
            if (Fill)
            {

                using (var brush = new SolidBrush(Color))
                {
                    g.FillEllipse(brush, MouseClick.X + Location.X, MouseClick.Y + Location.Y, Radius, Radius);
                }
            }
            using (var pen = new Pen(fillColor, 3))
            {
                g.DrawEllipse(pen, MouseClick.X + Location.X, MouseClick.Y + Location.Y, Radius, Radius);
            }

        }
        public bool Contains(Point p)
        {
            return Location.X < p.X && p.X < Location.X + Radius &&
               Location.Y < p.Y && p.Y < Location.Y + Radius;
        }
        public bool Intersects(Circle circle)
        {
            return this.Location.X < circle.Location.X + circle.Radius && circle.Location.X < this.Location.X + this.Radius &&
                this.Location.Y < circle.Location.Y + circle.Radius && circle.Location.Y < this.Location.Y + this.Radius;
        }
        public static implicit operator int(Circle circle)
        {
            return circle.GetArea();
        }

    }
}
