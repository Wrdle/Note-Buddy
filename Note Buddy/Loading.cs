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
    public partial class Loading : Form
    {
        DB db = new DB();
        public Loading()
        {
            InitializeComponent();
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(500);
                Invoke(new Action(LoadLoginImg));
            });
        }

        private void LoadLoginImg()
        {
            db.ConnectWallpapers();
            string query = "SELECT * FROM Places WHERE id=(SELECT FLOOR(RAND()*((SELECT TOP 1 id FROM [Wallpapers].[dbo].[Places] ORDER BY id DESC)-1+1)+1));";
            var results = db.Select(query);

            try
            {
                if (results.Rows.Count > 0)
                {
                    Login.LoginPicture = createImg(new Bitmap(Image.FromStream(new MemoryStream((byte[])results.Rows[0]["image"]))));
                    Login.Country = (string)results.Rows[0]["country"];
                    Login.City = (string)results.Rows[0]["city"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                      MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            db.Dispose();
            this.Dispose();
        }

        public Bitmap createImg(Bitmap finalImage)
        {
            int requiredWidth = Convert.ToInt32(Math.Ceiling(0.5617977528089888M * finalImage.Height));
            finalImage = finalImage.Clone(new Rectangle(Convert.ToInt32(finalImage.Width - requiredWidth) / 2, 0, requiredWidth, finalImage.Height), finalImage.PixelFormat);

            using (Graphics g = Graphics.FromImage(finalImage))
            {
                Bitmap Bmp = new Bitmap(finalImage.Width, finalImage.Height);
                using (Graphics gfx = Graphics.FromImage(Bmp))
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(70,0,0,0)))
                {
                    gfx.FillRectangle(brush, 0, 0, finalImage.Width, finalImage.Height);
                }
                g.DrawImage(Bmp, new Rectangle(0, 0, Bmp.Width, Bmp.Height));
            }
            return finalImage;
        }
    }
}
