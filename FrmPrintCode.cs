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
    public partial class FrmPrintCode : Form
    {
        /// <summary>
        /// this property support the students list for dataset to print.
        /// </summary>
        private ReportingDBDataSet ds = new ReportingDBDataSet();

        public FrmPrintCode()
        {
            InitializeComponent();
        }
        public FrmPrintCode(ReportingDBDataSet dataSet)
        {
            InitializeComponent();
            ds = dataSet;
        }


        private void FrmPrintCode_Load(object sender, EventArgs e)
        {
        }
    }
}
