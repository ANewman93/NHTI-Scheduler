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
    public partial class frmBmpViewer : Form
    {
        private Bitmap _image; 

        public frmBmpViewer()
        {
            InitializeComponent();
        }

        public Bitmap imageToDisplay
        {
            set { 
                _image = value;
                pictBoxImage.Image = _image; 
            }
        }

        private void frmBmpViewer_Load(object sender, EventArgs e)
        {

        }
    }
}
