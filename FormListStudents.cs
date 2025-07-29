using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReportPRJ
{
    public partial class FormListStudents : Form
    {
        public FormListStudents()
        {
            InitializeComponent();
        }
        List<Stud> studentList = new List<Stud>();
        List<Stud> filteredStlist = new List<Stud>();
        private Stud st = new Stud();

        public FormListStudents(List<Stud>slist)
        {
            InitializeComponent();
            studentList = slist;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                ReportingDBDataSet ds = new ReportingDBDataSet();
                foreach (Stud itemStud in filteredStlist)
                {
                    ds.Stud.AddStudRow(itemStud.NameS, itemStud.NameKh, itemStud.NameP, itemStud.Tavalod,
                        itemStud.CodeM, itemStud.Tel, itemStud.Payeh, itemStud.Moadel, itemStud.Tarikh, itemStud.Pic);

                }
                FrmPrintCode frm = new FrmPrintCode(ds);
                rptStudCode rpt = new rptStudCode();
                rpt.SetDataSource(ds);
                frm.crystalReportViewer2.ReportSource = rpt;

                frm.ShowDialog();





            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,"خطا",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void FormListStudents_Load(object sender, EventArgs e)
        {
            RefreshGridViewList(studentList);

        }

        private void RefreshGridViewList(List<Stud> li)
        {
            if (txtStudentCode.Text!=string.Empty)
            {
                Stud s = new Stud();
                int idtocompare = Convert.ToInt32(txtStudentCode.Text);
                s = li.FirstOrDefault(a => a.id == idtocompare);
                filteredStlist.Add(s);
                


            }
            else
            {

                filteredStlist = li;
                filteredStlist=filteredStlist.Where(a => a.NameKh.Contains(txtName.Text)).ToList();
                filteredStlist = filteredStlist.Where(a => a.NameS.Contains(txtLName.Text)).ToList();
                filteredStlist = filteredStlist.Where(a => a.CodeM.Contains(txtNationalCode.Text)).ToList();
            }
            dataGridViewX1.DataSource = filteredStlist;


        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            RefreshGridViewList(studentList);

        }

        private void txtLName_TextChanged(object sender, EventArgs e)
        {
            RefreshGridViewList(studentList);

        }

        private void txtStudentCode_TextChanged(object sender, EventArgs e)
        {
            RefreshGridViewList(studentList);


        }

        private void txtNationalCode_TextChanged(object sender, EventArgs e)
        {
            RefreshGridViewList(studentList);


        }
    }
}
