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
    public partial class FrmKarbar : Form
    {

        #region ConstructorAndProperties
        public FrmKarbar()
        {
            InitializeComponent();
        }

        private SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=ReportingDB;Integrated Security=true;");

        private SqlCommand cmd = new SqlCommand();

        #endregion
        #region Events
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "INSERT INTO Karbar(UName,Password)values(@a,@b)";
                cmd.Parameters.AddWithValue("@a", txtUName.Text);
                cmd.Parameters.AddWithValue("@b", txtPassword.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("ثبت کاربر با موفقیت انجام شد", "ثبت کاربر", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassword.Text = string.Empty;
                txtUName.Text = string.Empty;
                DisplayInToDGV();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int idtodelete = Convert.ToInt32(dgvKarbar.SelectedRows[0].Cells[0].Value);
            try
            {
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "DELETE FROM Karbar WHERE Id=@n";
                cmd.Parameters.AddWithValue("@n", idtodelete);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("حذف کاربر با موفقیت انجام شد", "حذف کاربر", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassword.Text = string.Empty;
                txtUName.Text = string.Empty;
                DisplayInToDGV();

            }
            catch (Exception exception)
            {
                System.Windows.Forms.MessageBox.Show(exception.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int idToEdit = Convert.ToInt32(dgvKarbar.SelectedRows[0].Cells[0].Value);
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "UPDATE Karbar SET UName=@uname, Password=@pass WHERE Id=@w";
                cmd.Parameters.AddWithValue("@uname", txtUName.Text);
                cmd.Parameters.AddWithValue("@pass", txtPassword.Text);
                cmd.Parameters.AddWithValue("@w", idToEdit);
                cmd.Connection = con;  // این خط مهمه

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("ویرایش کاربر با موفقیت انجام شد", "ویرایش کاربر", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassword.Text = string.Empty;
                txtUName.Text = string.Empty;
                DisplayInToDGV();
            }
            catch (Exception exception)
            {
                System.Windows.Forms.MessageBox.Show(exception.Message,"خطا",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }

        }

        private void FrmKarbar_Load(object sender, EventArgs e)
        {
            DisplayInToDGV();
        }

        private void dgvKarbar_MouseUp(object sender, MouseEventArgs e)
        {
            txtUName.Text = dgvKarbar.SelectedRows[0].Cells[1].Value.ToString();
            txtPassword.Text = dgvKarbar.SelectedRows[0].Cells[2].Value.ToString();

        }

        #endregion
        #region Methods
        /// <summary>
        /// this method filled data grid view rows by database Karbar table records
        /// </summary>
        void DisplayInToDGV()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "Select * FROM Karbar";
            adp.Fill(ds,"Karbar");
            dgvKarbar.DataSource = ds;
            dgvKarbar.DataMember = "karbar";
            dgvKarbar.Columns[0].HeaderText = "کد";
            dgvKarbar.Columns[1].HeaderText = "نام کاربری";
            dgvKarbar.Columns[2].HeaderText = "کلمه عبور";
            dgvKarbar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKarbar.MultiSelect = false;


        }

        #endregion 
    }
    }
