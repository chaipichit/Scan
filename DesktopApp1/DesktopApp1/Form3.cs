using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopApp1
{
    public partial class Form3 : Form
    {
        int printNumber = 0;
        public Form3()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void setPrintNumber(ListViewItem.ListViewSubItem listViewSubItem)
        {
            printNumber = System.Convert.ToInt32(listViewSubItem.Text);
            textBox1.Text = printNumber.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
