using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NHTI_Scheduler
{
    public partial class frmWarnings : Form
    {
        public frmWarnings(List<string> warnings)
        {
            InitializeComponent();

            if(warnings == null)
            {
                MessageBox.Show("No warnings to display", "oops", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return; 
            }

            foreach(string s in warnings)
            {
                txtWarnings.AppendText(s);
                txtWarnings.AppendText(System.Environment.NewLine); 
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void cmdCopy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtWarnings.Text)) return; 

            System.Windows.Forms.Clipboard.SetText(txtWarnings.Text); 
        }
    }
}
