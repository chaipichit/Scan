using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using RestSharp;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace DesktopApp1
{


    public partial class Form1 : Form
    {

        Form2 testDialog = new Form2();

        ProductModel product;

        String output = "";
        String keyboard = "";
        int price1 = 0;
        int size = 0;
        int count = 1;
        public Form1()
        {
            InitializeComponent();
            LoadData();
            initListView();
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage_1);


        }

        private void LoadData(){

            ConnectAPI();


}
        private void initListView()
        {
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;

            listView1.Columns.Add("รายการ", 50);
            listView1.Columns.Add("รหัสสินค้า", 100);
            listView1.Columns.Add("ชื่อสินค้า", 200);
            listView1.Columns.Add("ราคา", 70);
            listView1.Columns.Add("จำนวน", 50);
            listView1.Columns.Add("รวม", 70);



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
                    string[] arr = new string[7];
                    arr[0] = size + ""; //รายการ
                    arr[1] = searchForId;//รหัสสินค้า
                    arr[2] = name; // ชื่อสินค้า
                    arr[3] = price;//ราคา
                    arr[4] = count + "";//จำนวน
                    arr[5] = (result * count) + "";// รวม
                                                   // listBox1.Items.Add(size + "\t" + searchForId + "\t" + name + "\t" + price + " \t" + " x \t" + count + "\t" + (result * count));

                    var itm = new ListViewItem(arr);
                    listView1.Items.Add(itm);

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

                SendKeys.SendWait("{ESC}");
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





            //   listBox1.Items.Add(" \t " + searchForId + "\t " + name + "\t1\t" + price + " \t");
            label11.Text = price1 + "";
            textBox1.Text = "";

        }

        private void Label19_Click_1(object sender, EventArgs e)
        {

        }

        private void SellApi()
        {
            product = new ProductModel();
            var client = new RestClient("https://script.google.com/macros/s/AKfycbyURj7T8d8gz6WdgHWy3l6XtLOVAN0CCc-EFufnAw/exec");
            var request = new RestRequest(Method.POST);
            /* request.AddHeader("Accept", "application/json");
             request.AddParameter("action", "sell");
             request.AddParameter("id", "P1001");
             request.AddParameter("count", "2");*/

            PostModel postModel = new PostModel();
            postModel.action = "sell";
            postModel.id = new string[] { "P1001", "P1002" };
            postModel.count = new string[] { "2", "1" };

            string json = JsonConvert.SerializeObject(postModel);
            // {
            //   "Name": "Apple",
            //   "Expiry": "2008-12-28T00:00:00",
            //   "Sizes": [
            //     "Small"
            //   ]
            // }
            request.RequestFormat = DataFormat.Json;
            request.AddBody(json);
            var response = client.Execute<SellModel>(request);

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
        }
        private void Button6_Click(object sender, EventArgs e)
        {





            //  string tmp = listBox1.Items[a].ToString();
            // String[] couds = tmp.Split('\t');
            //  int count = System.Convert.ToInt32(couds[5]);
            //  int oldPrice = System.Convert.ToInt32(couds[6]);
            // int count1 = System.Convert.ToInt32(couds[3]);

            //  count++;
            listView1.SelectedItems[0].SubItems[4].Text = (System.Convert.ToInt32(listView1.SelectedItems[0].SubItems[4].Text)+1)+"";
            //listView1.SelectedItems[0].SubItems[5].Text = (count1 * count)+"";

           // listBox1.Items.Insert(listBox1.SelectedIndex, couds[0] + "\t" + couds[1] + "\t" + couds[2] + "\t" + couds[3] + " \t" + " x \t" + count + "\t" + (count1* count));

          //  price1 += (count1 * count);
           // price1 -= oldPrice;
           // label11.Text = price1+"";
           // listBox1.Items.RemoveAt(listBox1.SelectedIndex);
           // listBox1.SetSelected(a, true);

        }

        private void Button3_Click(object sender, EventArgs e)
        {

            //  MessageBox.Show("เงินทอน  " +( System.Convert.ToInt32(textBox4.Text)- System.Convert.ToInt32(label11.Text)) );
            testDialog.setLabel("เงินทอน  " + (System.Convert.ToInt32(textBox4.Text) - System.Convert.ToInt32(label11.Text)));
            testDialog.ShowDialog(this);
            SellApi();
        }

        private void printDocument1_PrintPage_1(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {



            var fnt = new Font("Times new Roman", 10, FontStyle.Bold);

            int x = 0, y = 0;
            int dy = (int)fnt.GetHeight(e.Graphics) * 1;


            ///////////////////
            ///

            int receptno = 42;
            Graphics graphics = e.Graphics;
            Font font7 = new Font("Courier New", 7);
            Font font8 = new Font("Courier New", 8);
            Font font10 = new Font("Courier New", 10);
            Font font12 = new Font("Courier New", 12);
            Font font14 = new Font("Courier New", 14);

            float leading = 4;
            float lineheight7 = font7.GetHeight() + leading;
            float lineheight8 = font8.GetHeight() + leading;
            float lineheight10 = font10.GetHeight() + leading;
            float lineheight12 = font12.GetHeight() + leading;
            float lineheight14 = font14.GetHeight() + leading;

            float startX = 0;
            float startY = leading;
            float Offset = 0;

            StringFormat formatLeft = new StringFormat(StringFormatFlags.NoClip);
            StringFormat formatCenter = new StringFormat(formatLeft);
            StringFormat formatRight = new StringFormat(formatLeft);


            formatCenter.Alignment = StringAlignment.Center;
            formatRight.Alignment = StringAlignment.Far;
            formatLeft.Alignment = StringAlignment.Near;


            SizeF layoutSize = new SizeF(e.PageSettings.PrintableArea.Width - Offset * 2, lineheight14);

            RectangleF layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);

            Brush brush = Brushes.Black;

              /*  graphics.DrawString("Welcome to MSST", font14, brush, layout, formatCenter);
                Offset = Offset + lineheight14;
                layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
                graphics.DrawString("Recept No :" + receptno + 1, font14, brush, layout, formatLeft);
                Offset = Offset + lineheight14;
                layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
                graphics.DrawString("Date :" + DateTime.Today, font12, brush, layout, formatLeft);
                Offset = Offset + lineheight12;
                layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
                graphics.DrawString("".PadRight(46, '_'), font10, brush, layout, formatLeft);
                Offset = Offset + lineheight10;
                layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);

                graphics.DrawString("copyright SO", font10, brush, layout, formatRight);
                Offset = Offset + lineheight10;

                font10.Dispose(); font12.Dispose(); font14.Dispose();
                */
            graphics.DrawString("เหมือนบ้าน", font14, brush, layout, formatCenter);
            Offset = Offset + lineheight14;
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("แฟชั่นเครื่องเเต่งกายและเครื่องนอน", font8, brush, layout, formatCenter);
            Offset = Offset + lineheight8;
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("โทร 0962615027", font8, brush, layout, formatCenter);
            Offset = Offset + lineheight8;
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("ใบเสร็จรับเงิน", font8, brush, layout, formatLeft);
            Offset = Offset + lineheight8;
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);


            //////////////////////////////////
            graphics.DrawString("พิมพ์:"+DateTime.Today, font8, brush, layout, formatLeft);
            Offset = Offset + lineheight8;
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            String nameOrder = "ชุดเด็กลายลิขสิทธ์ฟหกหกหกหกห";
            if (nameOrder.Length > 24)
            {
               
                graphics.DrawString("1  " + nameOrder.Substring(0, 24), font7, brush, layout, formatLeft);
                graphics.DrawString("@150" + "  1500", font7, brush, layout, formatRight);
                Offset = Offset + lineheight7;
                layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            }
            else
            {
                graphics.DrawString("1  " + nameOrder, font7, brush, layout, formatLeft);
                graphics.DrawString("@150" + "  1500", font7, brush, layout, formatRight);
                Offset = Offset + lineheight7;
                layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            }
            ////////////////////////////////////////
            graphics.DrawString("ราคารวม: ", font8, brush, layout, formatLeft);
            graphics.DrawString("400  บาท", font8, brush, layout, formatRight);
            Offset = Offset + lineheight8;
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("สว่นลด: ", font8, brush, layout, formatLeft);
            graphics.DrawString("0  บาท", font8, brush, layout, formatRight);
            Offset = Offset + lineheight8;
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("รวมทั้งหมด: ", font8, brush, layout, formatLeft);
            graphics.DrawString("400  บาท", font8, brush, layout, formatRight);
            Offset = Offset + lineheight8;
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("***สินค้าซื้อแล้วไม่รับเปลี่ยนรับคืน***", font7, brush, layout, formatCenter);
            Offset = Offset + lineheight7;
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("**ขอบคุณที่ใช้บริการ**", font8, brush, layout, formatCenter);

            /////////////////////

            // e.Graphics.DrawString("เหมือนบ้าน", fnt, new SolidBrush(Color.Black), new Point(x, y)); y += dy;
            //e.Graphics.DrawString("Man2", fnt, new SolidBrush(Color.Black), new Point(x, y)); y += dy;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 150 / 100f;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();

            }
        }
       

    }
}
    
