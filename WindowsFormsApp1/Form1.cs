using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        //public class MyClass1
        //{
        //    public void MyMethod1()
        //    {

        //    }
        //}

        public Form1()
        {
            InitializeComponent();
        }

        private void getButton_Click(object sender, EventArgs e)
        {
            //Type myType = typeof(MyClass1);
            //Guid myGuid = (Guid)myType.GUID;
            //courseIdTextBox.Text = myType.GUID.ToString();


            char[] charsToTrimId = { '"', ':', 'i', 'd', ' ', '\'' };
            char[] charsToTrimTitle = { '"', ':', 't', 'i', 'l', 'e', ' ', '\'' }; //fix this

            // open file

            OpenFileDialog openFile = new OpenFileDialog();

            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

            try
            {
                System.IO.StreamReader reader = new System.IO.StreamReader(openFile.FileName);

                List<string> lines = new List<string>();
                string line;

                List<string> guids = new List<string>();
                string guid;

                List<string> titles = new List<string>();
                string title;

                // create list of courses
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }

                for(int i = 0; i < lines.Count; i++)
                {
                    if(lines[i].Contains('"' + "course" + '"' ) && i != 0)
                    {
                        int j = 1;

                        j += i;

                        guid = lines[j];

                        string result = guid.Trim(charsToTrimId);

                        guids.Add(result);

                        //courseIdTextBox.Text = result;
                    }
                    if(lines[i].Contains('"' + "status" + '"') && i != 0)
                    {
                        int j = 1;

                        j += i;

                        title = lines[j];

                        string result = title.Trim(charsToTrimTitle);

                        titles.Add(result);
                    }
                }

                reader.Close();

                if (lines.Count <= 0)
                {
                    MessageBox.Show("No items read", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                for (int j = 0; j < titles.Count; j++)
                {
                    ListViewItem item = new ListViewItem(guids[j]);
                    item.SubItems.Add(titles[j]);
                    listView1.Items.Add(item);
                }

                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("Failed to open file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
    }
}
