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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
        }

        private void btnKarbar_Click(object sender, EventArgs e)
        {
            FrmKarbar frm = new FrmKarbar();
            frm.ShowDialog();
        }

        private void btnStud_Click(object sender, EventArgs e)
        {
            FrmStud frm = new FrmStud();
            frm.ShowDialog();
        }
    }
}
