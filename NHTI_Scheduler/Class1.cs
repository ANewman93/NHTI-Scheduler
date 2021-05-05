using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 

namespace NHTI_Scheduler
{
    // https://social.msdn.microsoft.com/Forums/windows/en-US/00a91481-c931-4efd-b289-ab1f7c79c96f/disable-default-automatic-tooltip-?forum=winforms
    // disables auto tool tip
    // Had to change class in initialization of form

    public class MyTreeView : TreeView
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams parms = base.CreateParams;
                parms.Style |= 0x80;  // Turn on TVS_NOTOOLTIPS 
                return parms;
            }
        }
    } 
}
