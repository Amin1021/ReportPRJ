using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ReportPRJ
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }
        private SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=ReportingDB;Integrated Security=true;");

        private SqlCommand cmd = new SqlCommand();
        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int i = 0;
            try
            {
                cmd = new SqlCommand("SELECT COUNT(*) FROM Karbar WHERE Uname=@a AND Password=@b", con);
                cmd.Parameters.AddWithValue("@a", txtUName.Text);
                cmd.Parameters.AddWithValue("@b", txtPassword.Text);
                con.Open();
                    i = (int)(cmd.ExecuteScalar());
                con.Close();
                    if (i==0)
                    {
                        throw new Exception("نام کاربری یا رمز عبور اشتباه است");
                    }
                else
                {
                    this.Hide();
                    new Form1().ShowDialog();
                    this.Close();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,"خطا",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
