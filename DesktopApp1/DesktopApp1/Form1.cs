using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace DesktopApp1
{


    public partial class Form1 : Form
    {

        String output = "";
        String keyboard = "";
        public Form1()
        {
            InitializeComponent();
        }


        private void Button2_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Add("1 \t 01000100 \t กางเกงยีนส์ขายาว เบอร์ 28 \t150 \t1 \t150  ");

        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox4.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                textBox4.Text = textBox4.Text.Remove(textBox4.Text.Length - 1);
            }
        }

        private void Label11_Click(object sender, EventArgs e)
        {

        }

        private void TextBox5_TextChanged_1(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox4.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                textBox4.Text = textBox4.Text.Remove(textBox4.Text.Length - 1);
            }
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
        }

   

    private void TabControl1_KeyPress(object sender, KeyPressEventArgs e)
    {

        keyboard += e.KeyChar;
        label17.Text = keyboard;
    }

    private void TabControl1_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyValue == (char)Keys.Enter) {

            listBox1.Items.Add("1 \t " + keyboard + " \t กางเกงยีนส์ขายาว เบอร์ 28 \t150 \t1 \t150  ");
            keyboard = "";

        }
    }

        private void Label19_Click(object sender, EventArgs e)
        {

        }

        private async Task Button5_ClickAsync(object sender, EventArgs e)
        {
            var data1 = await ProductProcessor.LoadProduct();

            label17.Text = data1;
        }
    }
}
    
