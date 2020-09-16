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
    public partial class AddItemFrom : Form
    {
        public string count = "";
        public AddItemFrom()
        {
            InitializeComponent();
        }

        internal void setText(ListViewItem.ListViewSubItem listViewSubItem1, ListViewItem.ListViewSubItem listViewSubItem2, ListViewItem.ListViewSubItem listViewSubItem3)
        {
            label2.Text = listViewSubItem1.Text;
            label4.Text = listViewSubItem2.Text;
            textBox1.Text = listViewSubItem3.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            count = textBox1.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            count = "a";
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
