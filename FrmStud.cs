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
using System.IO;
using System.Globalization;

namespace ReportPRJ
{
    public partial class FrmStud : Form
    {
        public FrmStud()
        {
            InitializeComponent();
        }
        ReportingDBEntities context = new ReportingDBEntities();


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (pictureBox1.Image == null)
                {
                    throw new Exception("تصویر نباید خالی باشد");
                }
                MemoryStream str = new MemoryStream();
                pictureBox1.Image.Save(str, System.Drawing.Imaging.ImageFormat.Jpeg);
                Stud student = new Stud();
                student.Pic = str.ToArray();
                student.NameKh = txtName.Text;
                student.NameS = txtSureName.Text;
                student.NameP = txtFatherName.Text;
                student.Tavalod = mTxtBurn.Text;
                student.CodeM = txtNationalCode.Text;
                student.Tel = txtPhone.Text;
                student.Payeh = txtScholarBase.Text;
                student.Moadel = Convert.ToInt32(txtAverageNo.Text);
                student.Tarikh = mTxtRegisterDate.Text;
                context.Studs.Add(student);
                context.SaveChanges();
                MessageBox.Show("مشخصات دانش آموز با موفقیت ذخیره شد", "ثب نام", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //*************************************************
                //emtptying textboxes
                clearingTextBoxes();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                int IdToSearch = 0;
                Stud st = new Stud();
                IdToSearch = Convert.ToInt32(txtCode.Text);
                st = context.Studs.FirstOrDefault(a => a.id == IdToSearch);
                if (st != null)
                {
                    MemoryStream str = new MemoryStream(st.Pic);
                    pictureBox1.Image = Image.FromStream(str);
                    txtName.Text = st.NameKh;
                    txtSureName.Text = st.NameS;
                    txtFatherName.Text = st.NameP;
                    mTxtBurn.Text = st.Tavalod;
                    txtCode.Text = st.id.ToString();
                    txtPhone.Text = st.Tel;
                    txtScholarBase.Text = st.Payeh;
                    txtAverageNo.Text = st.Moadel.ToString();
                    mTxtRegisterDate.Text = st.Tarikh;
                    txtNationalCode.Text = st.CodeM;



                }
                else
                {
                    clearingTextBoxes();
                    throw new Exception("دانش آموزی با این مشخصات وجود ندارد");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "All Pictures(*.*)|*.jpg;*.png;*.bmp;*.gif";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(open.FileName);
            }

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void FrmStud_Load(object sender, EventArgs e)
        {
            PersianCalendar p = new PersianCalendar();
            mTxtRegisterDate.Text = p.GetYear(DateTime.Now).ToString()
                + p.GetMonth(DateTime.Now).ToString("00")
                + p.GetDayOfMonth(DateTime.Now).ToString("00");
            mTxtBurn.Text = p.GetYear(DateTime.Now).ToString()
    + p.GetMonth(DateTime.Now).ToString("00")
    + p.GetDayOfMonth(DateTime.Now).ToString("00");

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int code = Convert.ToInt32(txtCode.Text);
                Stud st = new Stud();
                st = context.Studs.Where(a => a.id == code).FirstOrDefault();
                context.Studs.Remove(st);
                context.SaveChanges();
                MessageBox.Show("دانش آموز انتخابی از لیست دانش آموزان حذف شد", "حذف", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //*************************************************
                clearingTextBoxes();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clearingTextBoxes()
        {
            //emtptying textboxes
            txtName.Text = string.Empty;
            txtSureName.Text = string.Empty;
            txtFatherName.Text = string.Empty;
            mTxtBurn.Text = string.Empty;
            txtCode.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtScholarBase.Text = string.Empty;
            txtAverageNo.Text = string.Empty;
            mTxtRegisterDate.Text = string.Empty;
            txtNationalCode.Text = string.Empty;
            pictureBox1.Image = null;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                MemoryStream str = new MemoryStream();
                pictureBox1.Image.Save(str, System.Drawing.Imaging.ImageFormat.Jpeg);
                bool IsIdExsist = false;
                Stud st = new Stud();
                st.id = Convert.ToInt32(txtCode.Text);
                st.Pic = str.ToArray();
                st.NameKh = txtName.Text;
                st.NameS = txtSureName.Text;
                st.NameP = txtFatherName.Text;
                st.Tavalod = mTxtBurn.Text;
                st.CodeM = txtNationalCode.Text;
                st.Tel = txtPhone.Text;
                st.Payeh = txtScholarBase.Text;
                st.Moadel = Convert.ToInt32(txtAverageNo.Text);
                st.Tarikh = mTxtRegisterDate.Text;
                st.id = Convert.ToInt32(txtCode.Text);
                foreach (Stud item in context.Studs)
                {
                    if (st.id == item.id)
                    {
                        IsIdExsist = true;
                        item.CodeM = st.CodeM;
                        item.Moadel = st.Moadel;
                        item.NameKh = st.NameKh;
                        item.NameP = st.NameP;
                        item.NameS = st.NameS;
                        item.Payeh = st.Payeh;
                        item.Pic = st.Pic;
                        item.Tarikh = st.Tarikh;
                        item.Tavalod = st.Tavalod;
                        item.Tel = st.Tel;
                    }
                }
                if (IsIdExsist)
                {
                    context.SaveChanges();
                    MessageBox.Show("ویرایش با موفقیت انجام شد", "ویرایش",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearingTextBoxes();


                }
                else
                {
                    throw new Exception("دانش آموزی با مشخصات فوق یافت نشد");

                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            List<Stud> li = new List<Stud>();
            foreach (Stud item in context.Studs)
            {
                li.Add(item);
            }
            FormListStudents frm = new FormListStudents(li);
            frm.ShowDialog();
        }
    }
}
