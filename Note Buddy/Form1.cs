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
    }
}
