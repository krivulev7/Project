using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class FormScene : Form
    {

        private List<Rectangle> _rectangles = new List<Rectangle>();

        private List<Triangle> _triangles = new List<Triangle>();

        private List<Circle> _circle = new List<Circle>();

        private bool _captureMouse;
        private Point _captureLocation;
        private Rectangle _frameRectangle;
        private Triangle _frameTriangle;
        private Circle _frameCircle;

        public FormScene()
        {
            InitializeComponent();
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
               ControlStyles.OptimizedDoubleBuffer,
                true);
            KeyPreview = true;
        }
        protected override void OnPaint(PaintEventArgs e)
        {

            
                foreach (var rectangle in _rectangles)
                {
                    rectangle.Paint(e.Graphics);
                }
                if (_captureMouse && _frameRectangle != null)
                {
                    _frameRectangle.Paint(e.Graphics);
                }
            

                foreach (var triangle in _triangles)
                {
                    triangle.Paint(e.Graphics);
                }
                if (_captureMouse && _frameTriangle != null)
                {
                    _frameTriangle.Paint(e.Graphics);
                }
            
            
                foreach (var circle in _circle)
                {
                    circle.Paint(e.Graphics);
                }
                if (_captureMouse && _frameCircle != null)
                {
                    _frameCircle.Paint(e.Graphics);
                }
            

        }

        private void FormScene_MouseDown(object sender, MouseEventArgs e)
        {
            _captureMouse = true;
            _captureLocation = e.Location;

            

                foreach (var rectangle in _rectangles)
                {
                    if (rectangle.Contains(e.Location))
                        rectangle.Color = Color.Red;
                    else
                    {
                        rectangle.Color = Color.Blue;
                    }
                }
            
            
                
                foreach (var triangle in _triangles)
                {
                    if (triangle.Contains(e.Location))
                    {
                        triangle.Color = Color.Red;
                        
                    }
                    else
                    {
                        triangle.Color = Color.Blue;
                    }
                }
            
            
                foreach (var circle in _circle)
                {
                    
                    if (circle.Contains(e.Location))
                    {
                        circle.Color = Color.Red;
                        
                    }
                    else
                    {
                    circle.Color = Color.Blue;
                    }
                
                }
            
            Invalidate();
        }
        private Rectangle CreateFrameRectangle(Point location)
        {
            var frame = new Rectangle
            {
                Location = new Point(
                    Math.Min(_captureLocation.X, location.X),
                    Math.Min(_captureLocation.Y, location.Y)),
                Width =
                Math.Abs(_captureLocation.X - location.X),
                Heigth =
                Math.Abs(_captureLocation.Y - location.Y)

            };
            return frame;
        }
        private Triangle CreateFrameTriangle (Point location, int height)
        {
            Point B = new Point(_captureLocation.X - (height / 2), location.Y);
            Point C = new Point(_captureLocation.X + (height / 2), location.Y);

            var frame = new Triangle
            {
                Location = _captureLocation,
                PointBx = B.X,
                PointCx = C.X,
                Height = height

            };
            return frame;
        }
        private Circle CreateFrameCircle(Point location)
        {
            var frameCircle = new Circle
            {
                Location = _captureLocation,
                Radius = Math.Abs((_captureLocation.X - location.X) +
                    (_captureLocation.Y - location.Y))
            };

            return frameCircle;
        }

        private void FormScene_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_captureMouse)
            {
                return;
            }
            if (checkBoxRectangle.Checked)
            {
                _frameRectangle = CreateFrameRectangle(e.Location);
                _frameRectangle.Fill = false;
                _frameRectangle.Color = Color.LightGray;
                foreach (var rectangle in _rectangles)
                {
                    rectangle.Color = rectangle.Intersects(_frameRectangle)
                        ? Color.Red
                        : Color.Blue;
                    if (rectangle.Intersects(_frameRectangle))
                    {
                        rectangle.Color = Color.Red;
                    }
                }
            }
            if(checkBoxTriangle.Checked)
            {
                var h = Math.Sqrt(
                   Math.Pow(_captureLocation.X - e.Location.X, 2)
                   + Math.Pow(_captureLocation.Y - e.Location.Y, 2));

                _frameTriangle = CreateFrameTriangle(e.Location, (int)h);
                _frameTriangle.Fill = false;
                _frameTriangle.Color = Color.LightGray;

                foreach (var triangle in _triangles)
                {
                    triangle.Color = triangle.Intersects(_frameTriangle)
                       ? Color.Red
                       : Color.Blue;
                    if (triangle.Intersects(_frameTriangle))
                    {
                        triangle.Color = Color.Red;
                    }
                }
            }
            if(checkBoxCircle.Checked)
            {
                _frameCircle=CreateFrameCircle(e.Location);
                _frameCircle.Fill = false;
                _frameCircle.Color = Color.LightGray;

                foreach (var circle in _circle)
                {
                    circle.Color = circle.Intersects(_frameCircle)
                       ? Color.Red
                       : Color.Blue;
                    if (circle.Intersects(_frameCircle))
                    {
                        circle.Color = Color.Red;
                    }
                }
            }

            Invalidate();
        }
        private void RefreshArea()
        {
            var area = 0;
            if (_rectangles.Count == 0 && _triangles.Count == 0 && _circle.Count == 0)
            {
                area = 0;
                toolStripStatusLabel.Text = area.ToString();
            }
            foreach (var rectangle in _rectangles)
            {
                area += rectangle.GetArea();
                toolStripStatusLabel.Text = area.ToString();
            }
            foreach (var triangle in _triangles)
            {
                area += triangle.GetArea();
                toolStripStatusLabel.Text = area.ToString();
            }
            foreach (var circle in _circle)
            {
                area += circle.GetArea();
                toolStripStatusLabel.Text = area.ToString();
            }
          
        }
        private void RefreshPerimeter()
        {
            var perimeter = 0;
            if (_rectangles.Count == 0 && _triangles.Count == 0 && _circle.Count == 0)
            {
                perimeter = 0;
                toolStripStatusLabelPer.Text = perimeter.ToString();
            }
            foreach (var rectangle in _rectangles)
            {
                perimeter += rectangle.GetPerimeter();
                toolStripStatusLabelPer.Text = perimeter.ToString();
            }
            foreach (var triangle in _triangles)
            {
                perimeter += triangle.GetPerimeter();
                toolStripStatusLabelPer.Text = perimeter.ToString();
            }
            foreach (var circle in _circle)
            {
                perimeter += circle.GetPerimeter();
                toolStripStatusLabelPer.Text = perimeter.ToString();
            }
            
        }

        private void FormScene_MouseUp(object sender, MouseEventArgs e)
        {
            if (!_captureMouse)
            {
                return;
            }
           
            if (e.Button == MouseButtons.Right)
            {
                if (checkBoxRectangle.Checked)
                {
                    _frameRectangle.Fill = true;
                    _frameRectangle.Color = Color.Red;
                    _rectangles.Add(_frameRectangle);
                }
                else if(checkBoxTriangle.Checked)
                {
                    _frameTriangle.Fill = true;
                    _frameTriangle.Color = Color.Red;
                    _triangles.Add(_frameTriangle);
                }
                else if (checkBoxCircle.Checked)
                {
                    _frameCircle.Fill = true;
                    _frameCircle.Color = Color.Red;
                    _circle.Add(_frameCircle);
                }
            }
            _frameRectangle = null;
            _frameRectangle = null;
            _frameCircle = null;

            _captureMouse = false;

            RefreshPerimeter();
            RefreshArea();
            Invalidate();
        }

        private void FormScene_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete)
            {
                return;
            }

            for (int i = _rectangles.Count - 1; i >= 0; i--)
            {
                if (_rectangles[i].Color == Color.Red)
                {
                    _rectangles.RemoveAt(i);
                }
            }
            for (int i = _triangles.Count-1; i >= 0; i--)
            {
                if (_triangles[i].Color == Color.Red)
                    _triangles.RemoveAt(i);
            }
            for (int i = _circle.Count - 1; i >= 0; i--)
            {
                if(_circle[i].Color == Color.Red)
                {
                    _circle.RemoveAt(i);
                }
            }
            RefreshPerimeter();
            RefreshArea();
            Invalidate();
        }

        private void FormScene_DoubleClick(object sender, EventArgs e)
        {
            foreach (var rectangle in _rectangles)
            {
                if (rectangle.Color == Color.Red)
                {
                    var fc = new FormChange();
                    fc.MyWidth = rectangle.Width;
                    fc.MyHeigth = rectangle.Heigth;
                    fc.MyColor = rectangle.Color;
                    if(fc.ShowDialog() == DialogResult.OK)
                    {
                        rectangle.Width = fc.MyWidth;
                        rectangle.Heigth = fc.MyHeigth;
                        rectangle.Color = fc.MyColor;
                    }
                    
                    
                }
            }
            foreach (var triangle in _triangles)
            {
                if(triangle.Color == Color.Red)
                {
                    var fc = new FormChange();
                    fc.MyHeigth = triangle.Height;
                    fc.MyColor = triangle.Color;
                    if(fc.ShowDialog() == DialogResult.OK)
                    {
                        triangle.Height = fc.MyHeigth;
                        triangle.Color = fc.MyColor;
                    }
                   
                }
            }
            foreach (var circle in _circle)
            {
                if (circle.Color == Color.Red)
                {
                    var fc = new FormCircle();
                    fc.MyColor = circle.Color;
                    if (fc.ShowDialog() == DialogResult.OK)
                    {
                        circle.Color = fc.MyColor;
                    }
                   
                }
            }
            RefreshPerimeter();
            RefreshArea();
            Invalidate();
        }

        private void FormScene_Load(object sender, EventArgs e)
        {
            if (!File.Exists("data"))
            {
                return;
            }
            var formatter = new BinaryFormatter();
            using(var stream = new FileStream("data", FileMode.Open))
            {
                _rectangles = (List<Rectangle>)formatter.Deserialize(stream);
                _circle = (List<Circle>)formatter.Deserialize(stream);
                _triangles = (List<Triangle>)formatter.Deserialize(stream);

            }
        }

        private void FormScene_FormClosing(object sender, FormClosingEventArgs e)
        {
            var formatter = new BinaryFormatter();
            using(var stream = new FileStream("data", FileMode.Create))
            {
                formatter.Serialize(stream, _rectangles);
                formatter.Serialize(stream, _circle);
                formatter.Serialize(stream, _triangles);
            }
            
        }
    }
}
