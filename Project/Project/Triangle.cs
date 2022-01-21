using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Project
{
    [Serializable]
   public class Triangle:Shape
    {
        public Point Location { get; set; }
        public Color Color { get; set; }
        public int PointBx { get; set; }
        public int PointCx { get; set; }
        protected int _Side { get; set; }
       
        protected int _Height { get; set; }
        public int Height
        {
            get
            {
                return _Height;
            }
            set
            {
                _Height = value;
            }
        }
        public int Side
        {
            get => _Side;
            set
            {
                _Side = value;
            }
        }
        public bool Fill { get; set; } = true;
        public override int GetArea()
        {
            Point A = new Point(this.Location.X, this.Location.Y);
            Point B = new Point(this.PointBx, this.Location.Y + this.Height);
            Point C = new Point(this.PointCx, this.Location.Y + this.Height);

            Side = Math.Abs(B.X - C.X);
            Height = Math.Abs(B.Y - A.Y);

            return (Side * Height) / 2;
        }
        public override int GetPerimeter()
        {
            Point b = new Point(this.PointBx, this.Location.Y + this.Height);
            Point c = new Point(this.PointCx, this.Location.Y + this.Height);

            Side = Math.Abs(b.X - c.X);
            return 3 * Side;
        }
        public void Paint(Graphics g)
        {
            if (Fill)
            {
                var fillColor = Color.FromArgb(
                    Math.Min(byte.MaxValue, Color.R / 2),
                    Math.Min(byte.MaxValue, Color.G / 2),
                    Math.Min(byte.MaxValue, Color.B / 2));

                using (var brush = new SolidBrush(fillColor))
                {
                    Point[] p =
                    {
                        Location,
                        new Point(
                            PointBx,
                            Location.Y + Height),

                        new Point(
                            PointCx,
                            Location.Y + Height)
                    };
                    g.FillPolygon(brush, p);
                }
            }
            using (var pen = new Pen(Color, 3))
            {
                Point[] p =
                {
                    Location,
                    new Point(
                            PointBx,
                            Location.Y + Height),

                        new Point(
                            PointCx,
                            Location.Y + Height)
                };
                g.DrawPolygon(pen, p);
            }
        }
        public bool Contains(Point p)
        {
            return Location.X < p.X && p.X < Location.X + Side &&
                Location.Y < p.Y && p.Y < Location.Y + Height;
        }
        public bool Intersects(Triangle triangle)
        {
            Point PointA = this.Location;
            Point PointB = new Point(this.PointBx, this.Location.Y + this.Location.Y);
            Point PointC = new Point(this.PointCx, this.Location.Y + this.Location.Y);

            Point PointFrameA = triangle.Location;
            Point PointFrameB = new Point(triangle.PointBx, triangle.Location.Y + triangle.Location.Y);
            Point PointFrameC = new Point(triangle.PointCx, triangle.Location.Y + triangle.Location.Y);

            return OnLine(PointA, Orientation(PointA, PointB, PointFrameA, PointFrameC), PointB) &&
                OnLine(PointFrameA, Orientation(PointA, PointB, PointFrameA, PointFrameC), PointFrameC) ||
                OnLine(PointA, Orientation(PointA, PointB, PointFrameB, PointFrameC), PointB) &&
                OnLine(PointFrameB, Orientation(PointA, PointB, PointFrameB, PointFrameC), PointFrameC) ||
                OnLine(PointA, Orientation(PointA, PointC, PointFrameA, PointFrameB), PointC) &&
                OnLine(PointFrameA, Orientation(PointA, PointC, PointFrameA, PointFrameB), PointFrameB) ||
                OnLine(PointA, Orientation(PointA, PointC, PointFrameB, PointFrameC), PointC) &&
                OnLine(PointFrameB, Orientation(PointA, PointC, PointFrameB, PointFrameC), PointFrameC) ||
                OnLine(PointB, Orientation(PointB, PointC, PointFrameA, PointFrameB), PointC) &&
                OnLine(PointFrameA, Orientation(PointB, PointC, PointFrameA, PointFrameB), PointFrameB) ||
                OnLine(PointB, Orientation(PointB, PointC, PointFrameA, PointFrameC), PointC) &&
                OnLine(PointFrameA, Orientation(PointB, PointC, PointFrameA, PointFrameC), PointFrameC);

        }
        public static bool OnLine(Point pointA, Point OnLine, Point pointB)
        {
            if (OnLine.X <= Math.Max(pointA.X, pointB.X) && OnLine.X >= Math.Min(pointA.X, pointB.X) &&
                OnLine.Y <= Math.Max(pointA.Y, pointB.Y) && OnLine.Y >= Math.Min(pointA.Y, pointB.Y))
                return true;

            return false;
        }
        public Point Orientation(Point pointA, Point pointB, Point pointC, Point pointD)
        {
            int val = (pointB.Y - pointA.Y) * (pointC.X - pointD.X) -
                    (pointD.Y - pointC.Y) * (pointA.X - pointB.X);

            int c1 = (pointB.Y - pointA.Y) * (pointA.X) + (pointA.X - pointB.X) * (pointA.Y);
            int c2 = (pointD.Y - pointC.Y) * (pointC.X) + (pointC.X - pointD.X) * (pointC.Y);

            if (val == 0)
                return new Point(int.MaxValue, int.MaxValue);
            else
            {
                int x = (((pointC.X - pointD.X) * c1) - (pointA.X - pointB.X) * c2) / val;
                int y = ((pointB.Y - pointA.Y) * c2 - (pointD.Y - pointC.Y) * c1) / val;
                return new Point(x, y);
            }
        }
        public static implicit operator int(Triangle triangle)
        {
            return triangle.GetArea();
        }
    }
}
