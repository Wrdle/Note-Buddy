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
        public Loading()
        {
            InitializeComponent();
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            Task.Factory.StartNew
 (
  () =>
  {
      Thread.Sleep(100);
      Invoke(new Action(LoadLoginImg));
  }
 );
        }

        private void LoadLoginImg()
        {
            SqlConnection con = new SqlConnection(DBHandler.GetConnectionString());
            SqlCommand cmd = new SqlCommand("SELECT * FROM Places WHERE id=1;", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    MemoryStream ms = new MemoryStream((byte[])dt.Rows[0]["image"]);
                    Bitmap finalImg = new Bitmap(Image.FromStream(ms));

                    int requiredWidth = Convert.ToInt32(Math.Ceiling(0.5617977528089888M * finalImg.Height));


                    Login.LoginPicture = CropBitmap(finalImg, Convert.ToInt32(finalImg.Width - requiredWidth) / 2, 0, requiredWidth, finalImg.Height);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                      MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            this.Close();
        }
        public Bitmap CropBitmap(Bitmap bitmap, int cropX, int cropY, int cropWidth, int cropHeight)
        {
            Rectangle rect = new Rectangle(cropX, cropY, cropWidth, cropHeight);
            Bitmap cropped = bitmap.Clone(rect, bitmap.PixelFormat);
            return cropped;
        }
    }
}
