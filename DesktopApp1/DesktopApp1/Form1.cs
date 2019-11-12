using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace DesktopApp1
{


    public partial class Form1 : Form
    {

        ProductModel product;

        String output = "";
        String keyboard = "";
        int price1 = 0;
        int size = 0;
        int count = 1;
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
         /*   if (System.Text.RegularExpressions.Regex.IsMatch(textBox4.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                textBox4.Text = textBox4.Text.Remove(textBox4.Text.Length - 1);
            }*/
        }

        private void Label11_Click(object sender, EventArgs e)
        {

        }

        private void TextBox5_TextChanged_1(object sender, EventArgs e)
        {
          /*  if (System.Text.RegularExpressions.Regex.IsMatch(textBox4.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                textBox4.Text = textBox4.Text.Remove(textBox4.Text.Length - 1);
            }*/
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
            if (textBox4.ContainsFocus == true)
            {

            }
            else
            {
                textBox1.Focus();

            }
        }       

        private void TabControl1_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyValue == (char)Keys.Enter) {

                if (textBox4.ContainsFocus == true)
                {
                    MessageBox.Show("เงินทอน  " + (System.Convert.ToInt32(textBox4.Text) - System.Convert.ToInt32(label11.Text)));

                    product = new ProductModel();
                    var client = new RestClient("https://script.google.com/macros/s/AKfycbyURj7T8d8gz6WdgHWy3l6XtLOVAN0CCc-EFufnAw/exec");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Accept", "application/json");
                    var listid = new[];
                    String[]  =["P1001", "P1003"];
                    String[] listcount =["2", "1"];
                    request.AddBody(listid, "id");
                    request.AddBody("sell", "action");
                    request.AddBody(listcount, "count");
                    var response = client.Execute<ProductModel>(request);

                    //Response 200 OK
                    if (response.StatusCode.Equals(HttpStatusCode.OK))
                    {
                        //Do something with response.Content


                     /*   product = response.Data;
                        var searchForId = "P1001";
                        int index = product.product.FindIndex(p => p.id == searchForId);
                        string name = product.product[index].name;
                        label17.Text = name;*/
                    }
                    listBox1.Items.Clear();

                }
                else
                {
                    var searchForId = textBox1.Text;
                    int index = product.product.FindIndex(p => p.id == searchForId);
                    string name = product.product[index].name;
                    string price = product.product[index].price;
                    int result = System.Convert.ToInt32(price);
                    price1 += result;



                    size++;
                    listBox1.Items.Add(size + "\t" + searchForId + "\t" + name + "\t" + price + " \t" + " x \t" + count + "\t" + (result * count));
                    label11.Text = price1 + "";
                    textBox1.Text = "";

                }

            }
    }

        private void Label19_Click(object sender, EventArgs e)
        {

        }

        private async Task Button5_ClickAsync(object sender, EventArgs e)
        {

            //var data1 = await ProductProcessor.LoadProduct();

          //  label17.Text = data1;
        }

        private void ConnectAPI()
        {
            product = new ProductModel();
            var client = new RestClient("https://script.google.com/macros/s/AKfycbyURj7T8d8gz6WdgHWy3l6XtLOVAN0CCc-EFufnAw/exec");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/json");

            var response = client.Execute<ProductModel>(request);

            //Response 200 OK
            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                //Do something with response.Content

                
                product = response.Data;
                var searchForId = "P1001";
                int index = product.product.FindIndex(p => p.id == searchForId);
                string name = product.product[index].name;
                label17.Text = name;
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            ConnectAPI();

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var searchForId = textBox1.Text;
            int index = product.product.FindIndex(p => p.id == searchForId);
            string name = product.product[index].name;
            string price = product.product[index].price;
            int result = System.Convert.ToInt32(price);
            price1 += result;



            listBox1.Items.Add(" \t " + searchForId + " \t เบอร์ 28 \t " + name + "\t1" + price + " \t150  ");
            label11.Text = price1 + "";
            textBox1.Text = "";

        }

        private void Label19_Click_1(object sender, EventArgs e)
        {

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            int a = listBox1.SelectedIndex;

            string tmp = listBox1.Items[a].ToString();
            String[] couds = tmp.Split('\t');
            int count = System.Convert.ToInt32(couds[5]);
            int oldPrice = System.Convert.ToInt32(couds[6]);
            int count1 = System.Convert.ToInt32(couds[3]);

            count++;
            listBox1.Items.Insert(listBox1.SelectedIndex, couds[0] + "\t" + couds[1] + "\t" + couds[2] + "\t" + couds[3] + " \t" + " x \t" + count + "\t" + (count1* count));

            price1 += (count1 * count);
            price1 -= oldPrice;
            label11.Text = price1+"";
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            listBox1.SetSelected(a, true);

        }

        private void Button3_Click(object sender, EventArgs e)
        {

            MessageBox.Show("เงินทอน  " +( System.Convert.ToInt32(textBox4.Text)- System.Convert.ToInt32(label11.Text)) );

        }
    }
}
    
