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
    public partial class EditFrom : Form
    {
        public string name = "";
        public string sell = "";
        public string sell2 = "";
        public string sell3 = "";
        public string cost = "";
        public string stock = "";

        public EditFrom()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        internal void setLebel(string name, string sell, string sell2, string sell3, string cost, string stock)
        {
            textBox1.Text = name;
            textBox2.Text = sell;
            textBox3.Text = sell2;
            textBox4.Text = sell3;
            textBox5.Text = cost;
            textBox6.Text = stock;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            name = textBox1.Text;
            sell = textBox2.Text;
            sell2 = textBox3.Text;
            sell3 = textBox4.Text;
            cost = textBox5.Text;
            stock = System.Convert.ToInt32( textBox6.Text )+System.Convert.ToInt32( textBox7.Text)+"";
            
            Close();
        }

    }
}
