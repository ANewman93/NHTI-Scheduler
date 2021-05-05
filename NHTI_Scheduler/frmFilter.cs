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
    public partial class frmFilter : Form
    {
        public CFilter _filter; 

        public frmFilter(CFilter filter)
        {
            InitializeComponent();

            if (filter == null) return;

            _filter = filter; 

            if(_filter[(int)CFilter.FILTERS.REMAINING_MIN].action != CFilter.FILTERS_ACTION.NONE)
            {
                chkBoxRemaining.Checked = true;
                txtRemainingStudentsMin.Text = _filter[(int)CFilter.FILTERS.REMAINING_MIN].filter_value; 
            }

            if (_filter[(int)CFilter.FILTERS.DAYS_INCLUDED].action != CFilter.FILTERS_ACTION.NONE)
            {
                
                chkBoxDays.Checked = true;
                txtDaysToInclude.Text = _filter[(int)CFilter.FILTERS.DAYS_INCLUDED].filter_value;
            }
        }

        private void txtRemainingStudentsMin_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
            if (chkBoxRemaining.Checked == true)
            {
                int n; 

                if (!Int32.TryParse(txtRemainingStudentsMin.Text, out n))
                {
                    MessageBox.Show("Invalid value for remaining students");
                    
                    return; 
                }

                CFilter.FILTER f = new CFilter.FILTER();

                f.filter = CFilter.FILTERS.REMAINING_MIN;
                f.action = CFilter.FILTERS_ACTION.HIGHLIGHT; 
                f.filter_value = txtRemainingStudentsMin.Text; // convert to int later 

                _filter[(int)CFilter.FILTERS.REMAINING_MIN] = f;

            }
            else // not checked
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;

                CFilter.FILTER f = new CFilter.FILTER();

                f.action = CFilter.FILTERS_ACTION.NONE;

                _filter[(int)CFilter.FILTERS.REMAINING_MIN] = f;

            }

            if(chkBoxDays.Checked == true)
            {
                CFilter.FILTER f = new CFilter.FILTER();

                f.filter = CFilter.FILTERS.DAYS_INCLUDED;
                f.action = CFilter.FILTERS_ACTION.HIGHLIGHT;
                f.filter_value = txtDaysToInclude.Text;

                _filter[(int)CFilter.FILTERS.DAYS_INCLUDED] = f; 
            }
            else
            {
                CFilter.FILTER f = new CFilter.FILTER();

                f.action = CFilter.FILTERS_ACTION.NONE;

                _filter[(int)CFilter.FILTERS.DAYS_INCLUDED] = f; 
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close(); 
        }

        private void txtDaysToInclude_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDaysToInclude_KeyPress(object sender, KeyPressEventArgs e)
        {
            string allowed = "MTWRFS";

            if (e.KeyChar == (char)Keys.Back) return; 

            try
            {
                e.KeyChar  = char.ToUpper(e.KeyChar);
            }
            catch { e.Handled = true; return; } // failed to convert to upper


            if (allowed.IndexOf(e.KeyChar) < 0) // key not allowed
            {
                e.Handled = true;
                return; 
            }

            if(txtDaysToInclude.Text.Count() > 6) // too many keys
            {
                e.Handled = true;
                return; 
            }

            if (txtDaysToInclude.Text.IndexOf(e.KeyChar) >= 0) // duplicate key
            {
                e.Handled = true;
                return;
            }
        }
    }
}
