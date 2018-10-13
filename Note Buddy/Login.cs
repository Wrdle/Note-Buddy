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
    public partial class Login : Form
    {
        public static Bitmap LoginPicture;
        public static string City;
        public static string Country;

        public Login()
        {
            InitializeComponent();
            var pos = this.PointToScreen(pnlLocationInfo.Location);
            pos = imgSidebar.PointToClient(pos);
            pnlLocationInfo.Parent = imgSidebar;
            pnlLocationInfo.Location = pos;
            pnlLocationInfo.BackColor = Color.Transparent;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            imgSidebar.Image = LoginPicture;
            imgSidebar.SizeMode = PictureBoxSizeMode.Zoom;
            imgSidebar.Refresh();
            lblCity.Text = City;
            lblCountry.Text = Country.ToUpper();
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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (Account.Login(txtUsername.Text, txtPassword.Text) == true)
                MessageBox.Show("yay");
            else
                MessageBox.Show("no fun");
        }
    }
}
