using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Project
{
    public partial class FormChange : Form
    {
        public FormChange()
        {
            InitializeComponent();
        }
        private int _Width;
        private int _Heigth;
        private Color _color;
        public int MyWidth
        {
            get
            {
                return _Width;
            }
            set 
            {
                _Width = value;
                textBoxWidth.Text = _Width.ToString();
            }
        }
        public int MyHeigth
        {
            get
            {
                return _Heigth;
            }
            set
            {
                _Heigth = value;
                textBoxHeigth.Text = _Heigth.ToString();
            }
        }
        public Color MyColor
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                buttonColor.BackColor = _color;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            
            _Width = int.Parse(textBoxWidth.Text);
            _Heigth = int.Parse(textBoxHeigth.Text);

            DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            var cd = new ColorDialog();
            if(cd.ShowDialog() == DialogResult.OK)
            {
                MyColor = cd.Color;
            }
        }
    }
}
