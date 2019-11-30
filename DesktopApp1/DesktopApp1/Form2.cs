using System;

using System.Windows.Forms;

namespace DesktopApp1
{
    public partial class Form2 : Form
    {
         public Boolean isClick = false;
        public Form2()
        {
            InitializeComponent();
            
        }
        public void setLabel(string label)
        {
            label1.Text = label;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        public Boolean isclickOk()
        {
            return isClick;
        }
    }
}
