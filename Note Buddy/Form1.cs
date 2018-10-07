using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Note_Buddy
{
    public partial class Form1 : Form
    {
        public static Bitmap LoginPicture;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            sidebar.Image = LoginPicture;
            sidebar.SizeMode = PictureBoxSizeMode.Zoom;
            sidebar.Refresh();
        }

        bool focus = false;
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (focus)
            {
                txtUsername.BorderStyle = BorderStyle.None;
                Pen p = new Pen(Color.Red);
                Graphics g = e.Graphics;
                int variance = 3;
                g.DrawRectangle(p, new Rectangle(txtUsername.Location.X - variance, txtUsername.Location.Y - variance, txtUsername.Width + variance, txtUsername.Height + variance));
            }
            else
            {
                txtUsername.BorderStyle = BorderStyle.FixedSingle;
            }
        }


        private void textBox1_Enter(object sender, EventArgs e)
        {
            focus = true;
            this.Refresh();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            focus = false;
            this.Refresh();
        }
    }
}
