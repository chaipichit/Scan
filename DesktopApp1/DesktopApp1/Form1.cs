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
using ZXing;
using System.Runtime.InteropServices;


// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace DesktopApp1
{


    public partial class Form1 : Form
    {

        Form2 testDialog = new Form2();
        Form3 printDialog = new Form3();
        EditFrom editFrom = new EditFrom();
        WaitFrom waitFrom = new WaitFrom();
        AddItemFrom itemFrom = new AddItemFrom();
        ProductModel product;
        HistoryModel histiry;
        SellMountModel sellMountModel;
        int print = 0;
        string dayStart = "";
        string mountStart = "";
        string yearStart = "";
        string dayStop = "";
        string mountStop = "";
        string yearStop = "";
        string nameShop = "";
        string descrip = "";
        string tel = "";
        string warning = "";
        string thank = "";
        int price1 = 0;
        int size = 1;
        int count = 1;
        int j = 0;
        int findBy = 1; // 1=barcode ,2 = name
        String change = "";
        public Form1()
        {
            InitializeComponent();
            LoadData();
            initListView();
            initListViewHistory();
            initDropDown();
            LoadSellOfToday();
            radioButton1.Checked = true;
            radioButton4.Checked = true;
            textBox5.Text = "0";
            dayStart = DateTime.Today.Day + "";
            mountStart = DateTime.Today.Month + "";
            yearStart = DateTime.Today.Year + "";
            dayStop = DateTime.Today.Day + "";
            mountStop = DateTime.Today.Month + "";
            yearStop = DateTime.Today.Year + "";
            LoadPrinterSettup();
            textBox1.Focus();


        }
        private void LoadPrinterSettup()
        {
            textBox3.Text = nameShop;
            textBox6.Text = descrip;
            textBox7.Text = tel;
            textBox8.Text = warning;
            textBox9.Text = thank;

        }
        private void LoadSellOfToday()
        {
            var date = DateTime.Now + "";
            var dateOnly = date.Split(' ');

            LoadHistory(dateOnly[0]);
        }

        private void initDropDown()
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Items.Add("มกราคม");
            comboBox1.Items.Add("กุมภาพันธ์");
            comboBox1.Items.Add("มีนาคม");
            comboBox1.Items.Add("เมษายน");
            comboBox1.Items.Add("พฤษภาคม");
            comboBox1.Items.Add("มิถุนายน");
            comboBox1.Items.Add("กรกฏาคม");
            comboBox1.Items.Add("สิงหาคม");
            comboBox1.Items.Add("กันยายน");
            comboBox1.Items.Add("ตุลาคม");
            comboBox1.Items.Add("พฤษจิกายน");
            comboBox1.Items.Add("ธันวาคม");

            var date = DateTime.Now.Year;
            var mount = DateTime.Now.Month;
            label15.Text = DateTime.Now + "";

            comboBox1.SelectedIndex = mount - 1;



            for (int i = 0; i < 5; i++)
            {
                comboBox2.Items.Add(date - i + "");
            }
            comboBox2.SelectedIndex = comboBox2.FindStringExact(date + "");

            upDateMoney(comboBox1.SelectedIndex + 1 + "", comboBox2.SelectedItem + "");

        }

        private void initListViewHistory()
        {
            listView3.Items.Clear();
            listView3.View = View.Details;
            listView3.GridLines = true;
            listView3.FullRowSelect = true;

            listView3.Columns.Add("บาร์โค้ด", 150);
            listView3.Columns.Add("ชื่อสินค้า", 300);

            listView3.Columns.Add("ทุน", 75);
            listView3.Columns.Add("ราคาขาย", 90);
            listView3.Columns.Add("จำนวน", 75);

            listView3.Columns.Add("รวม", 75);
            listView3.Columns.Add("กำไร", 75);
            listView3.Columns.Add("ประเภทการขาย", 150);
            listView3.Columns.Add("เวลา", 200);


        }

        private void initListView2()
        {
            listView2.Clear();
            listView2.Items.Clear();
         
            listView2.View = View.Details;
            listView2.GridLines = true;
            listView2.FullRowSelect = true;

            listView2.Columns.Add("บาร์โค้ด", 125);
            listView2.Columns.Add("ชื่อสินค้า", 225);

            listView2.Columns.Add("ราคาปลีก", 80);
            listView2.Columns.Add("ราคาส่ง", 70);
            listView2.Columns.Add("ราคาสามตัว", 100);

            listView2.Columns.Add("ต้นทุน", 50);
            listView2.Columns.Add("จำนวน", 50);

            for (int i = 0; i < product.product.Count; i++)
            {
                string[] arr = new string[7];
                arr[0] = product.product[i].barcode + ""; //บาร์โค้ด
                arr[1] = product.product[i].name;//ชื่อสินค้า
                arr[2] = product.product[i].sell; // ราคาปลีก
                arr[3] = product.product[i].sell2; // ราคาส่ง
                arr[4] = product.product[i].sell3; // ราคาสาม
                arr[5] = product.product[i].cost;//ต้นทุน
                arr[6] = product.product[i].stock + "";//จำนวน                
                var itm = new ListViewItem(arr);
                listView2.Items.Add(itm);
            }

        }

        private void LoadData() {

            ConnectAPI();


        }
        private void initListView()
        {
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;

            listView1.Columns.Add("รายการ", 80);
            listView1.Columns.Add("รหัสสินค้า", 150);
            listView1.Columns.Add("ชื่อสินค้า", 300);
            listView1.Columns.Add("ราคา", 70);
            listView1.Columns.Add("จำนวน", 70);
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

        private void TabControl1_KeyDown(object sender, KeyEventArgs e)
        {
           
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
            var client = new RestClient("https://script.google.com/macros/s/AKfycbzhQxew4wj_DPTqUcxxK_H3DqQ8iQWo095iIvmqISJXDacEtZ5L/exec");

            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/json");


            var response = client.Execute<ProductModel>(request);

            //Response 200 OK
            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                //Do something with response.Content

                SendKeys.SendWait("{ESC}");
                
                product = response.Data;
                initListView2();
                nameShop = product.nameShop;
                descrip = product.descrip;
                tel = product.tel;
                warning = product.warning;
                thank = product.thank;
                button9.Enabled = true;
                waitFrom.Close();
                /* var searchForId = "P100000070";
                 int index = product.product.FindIndex(p => p.barcode == searchForId);
                 string name = product.product[index].name;*/
                //label17.Text = name;
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
            SearchItem();

        }

        private void Label19_Click_1(object sender, EventArgs e)
        {

        }

        private void SellApi()
        {
            waitFrom = new WaitFrom();
            waitFrom.Show(this);
            var client = new RestClient("https://script.google.com/macros/s/AKfycbzhQxew4wj_DPTqUcxxK_H3DqQ8iQWo095iIvmqISJXDacEtZ5L/exec");
            var request = new RestRequest(Method.POST);
            /* request.AddHeader("Accept", "application/json");
             request.AddParameter("action", "sell");
             request.AddParameter("id", "P1001");
             request.AddParameter("count", "2");*/

            PostModel postModel = new PostModel();
            postModel.action = "sell";
            postModel.Time = DateTime.Now.ToString();
            postModel.id = new List<string>();
            postModel.name = new List<string>();
            postModel.sum = new List<string>();
            postModel.sell = new List<string>();
            postModel.count = new List<string>();
            postModel.cost = new List<string>();

            if (radioButton1.Checked)
            {
                postModel.sellType = "ขายปลีก";

            }
            else if (radioButton2.Checked)
            {
                postModel.sellType = "ขายส่ง";

            }
            else if (radioButton3.Checked)
            {
                postModel.sellType = "ขายราคาสาม";

            }

            var id = new List<string>();
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                postModel.id.Add(listView1.Items[i].SubItems[1].Text);
                postModel.name.Add(listView1.Items[i].SubItems[2].Text);
                postModel.sell.Add(listView1.Items[i].SubItems[3].Text);
                postModel.count.Add(listView1.Items[i].SubItems[4].Text);
                postModel.sum.Add(listView1.Items[i].SubItems[5].Text);

                var a = listView1.Items[i].SubItems[1].Text;
                int index = product.product.FindIndex(p => p.barcode == a);
                string cost = product.product[index].cost;
                postModel.cost.Add(cost);
            }

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
                listView1.Items.Clear();
                label11.Text = "0";
                textBox4.Text = "0";
                textBox5.Text = "0";
                //Do something with response.Content


                /*   product = response.Data;
                   var searchForId = "P1001";
                   int index = product.product.FindIndex(p => p.id == searchForId);
                   string name = product.product[index].name;
                   label17.Text = name;*/
                waitFrom.Close();
            }

            textBox1.Focus();

        }
        private void Button6_Click(object sender, EventArgs e)
        {





            //  string tmp = listBox1.Items[a].ToString();
            // String[] couds = tmp.Split('\t');
            //  int count = System.Convert.ToInt32(couds[5]);
            //  int oldPrice = System.Convert.ToInt32(couds[6]);
            // int count1 = System.Convert.ToInt32(couds[3]);

            //  count++;
            listView1.SelectedItems[0].SubItems[4].Text = (System.Convert.ToInt32(listView1.SelectedItems[0].SubItems[4].Text) + 1) + "";
            listView1.SelectedItems[0].SubItems[5].Text = (System.Convert.ToInt32(listView1.SelectedItems[0].SubItems[4].Text)) * (System.Convert.ToInt32(listView1.SelectedItems[0].SubItems[3].Text)) + "";
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

            sellItem();
           
        }

        private void sellItem()
        {
            printDocument1.PrinterSettings.PrinterName = "Slip";
            size = 1;
            price1 = 0;
            //  MessageBox.Show("เงินทอน  " +( System.Convert.ToInt32(textBox4.Text)- System.Convert.ToInt32(label11.Text)) );

            change = (System.Convert.ToInt32(textBox4.Text) - System.Convert.ToInt32(label11.Text) + System.Convert.ToInt32(textBox5.Text)) + "";
            testDialog.setLabel("เงินทอน  " + change);
            testDialog.ShowDialog(this);
            if (testDialog.DialogResult == DialogResult.OK)
            {
                printPreviewDialog1.PrintPreviewControl.Zoom = 150 / 100f;
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();

                }
            }
            SellApi();
        }

        private void printDocument1_PrintPage_1(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {



            var fnt = new Font("Times new Roman", 10, FontStyle.Bold);

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
            graphics.DrawString(nameShop, font14, brush, layout, formatCenter);
            Offset = Offset + lineheight14;
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString(descrip, font8, brush, layout, formatCenter);
            Offset = Offset + lineheight8;
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("โทร "+tel, font8, brush, layout, formatCenter);
            Offset = Offset + lineheight8;
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("ใบเสร็จรับเงิน", font8, brush, layout, formatLeft);
            Offset = Offset + lineheight8;
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);


            //////////////////////////////////
            graphics.DrawString("พิมพ์:" + DateTime.Now, font8, brush, layout, formatLeft);
            Offset = Offset + lineheight8;
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);

            ////////////////////////////////////////////
            ///

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                String nameOrder = listView1.Items[i].SubItems[2].Text;
                String price = listView1.Items[i].SubItems[3].Text;
                String number = listView1.Items[i].SubItems[4].Text;
                String sum = listView1.Items[i].SubItems[5].Text;
                if (nameOrder.Length > 24)
                {

                    graphics.DrawString(number + " " + nameOrder.Substring(0, 24), font7, brush, layout, formatLeft);
                    graphics.DrawString("@" + price + " " + sum, font7, brush, layout, formatRight);
                    Offset = Offset + lineheight7;
                    layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
                }
                else
                {
                    graphics.DrawString(number + " " + nameOrder, font7, brush, layout, formatLeft);
                    graphics.DrawString("@" + price + " " + sum, font7, brush, layout, formatRight);

                    Offset = Offset + lineheight7;
                    layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
                }
            }


            ////////////////////////////////////////
            graphics.DrawString("ราคารวม: ", font8, brush, layout, formatLeft);
            graphics.DrawString(label11.Text + " บาท", font8, brush, layout, formatRight);
            Offset = Offset + lineheight8;
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("ส่วนลด: ", font8, brush, layout, formatLeft);
            graphics.DrawString(textBox5.Text + " บาท", font8, brush, layout, formatRight);
            Offset = Offset + lineheight8;
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("รวมทั้งหมด: ", font8, brush, layout, formatLeft);
            string sumOf = System.Convert.ToInt32(label11.Text) - System.Convert.ToInt32(textBox5.Text) + "";
            graphics.DrawString(sumOf + " บาท", font8, brush, layout, formatRight);
            Offset = Offset + lineheight8;
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("รับเงิน: " + textBox4.Text, font8, brush, layout, formatLeft);
            graphics.DrawString("เงินทอน: " + change, font8, brush, layout, formatRight);
            Offset = Offset + lineheight8;
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);


            graphics.DrawString("***"+warning+"***", font7, brush, layout, formatCenter);
            Offset = Offset + lineheight7;
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("**"+thank+"**", font8, brush, layout, formatCenter);

            /////////////////////

            // e.Graphics.DrawString("เหมือนบ้าน", fnt, new SolidBrush(Color.Black), new Point(x, y)); y += dy;
            //e.Graphics.DrawString("Man2", fnt, new SolidBrush(Color.Black), new Point(x, y)); y += dy;

        }

        private void printDocument1_PrintPage_2(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            var fnt = new Font("Times new Roman", 10, FontStyle.Bold);

            ///////////////////
            ///

            int receptno = 42;
            Graphics graphics = e.Graphics;

            Font font7 = new Font("Courier New", 7);
            Font font8 = new Font("Courier New", 8,FontStyle.Bold);
            Font font10 = new Font("Courier New", 10);
            Font font12 = new Font("Courier New", 12);
            Font font14 = new Font("Courier New", 14);
            Font font16 = new Font("Courier New", 18);

            float leading = 4;
            float lineheight7 = font7.GetHeight() + leading;
            float lineheight8 = font8.GetHeight() + leading;
            float lineheight10 = font10.GetHeight() + leading;
            float lineheight12 = font12.GetHeight() + leading;
            float lineheight14 = font14.GetHeight() + leading;
            float lineheight16 = font16.GetHeight() + leading;

            float startX = 0;
            float startY = leading;
            float Offset = 0;

            StringFormat formatLeft = new StringFormat(StringFormatFlags.NoClip);
            StringFormat formatCenter = new StringFormat(formatLeft);
            StringFormat formatRight = new StringFormat(formatLeft);


            formatCenter.Alignment = StringAlignment.Center;
            formatRight.Alignment = StringAlignment.Far;
            formatLeft.Alignment = StringAlignment.Near;
            BarcodeWriter writer = new BarcodeWriter() { Format = BarcodeFormat.CODE_128, Options = new ZXing.Common.EncodingOptions { Height = 40, Width = 10 } };


            SizeF layoutSize = new SizeF(e.PageSettings.PrintableArea.Width - Offset * 2, lineheight14);

            SizeF layoutSizeBar = new SizeF(e.PageSettings.PrintableArea.Width - Offset * 2, lineheight16);

            RectangleF layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);

            Brush brush = Brushes.Black;

            graphics.DrawString(listView2.SelectedItems[0].SubItems[1].Text, font7, brush, layout, formatCenter);
            Offset = Offset + lineheight7;
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSizeBar);
            var pic = writer.Write(listView2.SelectedItems[0].SubItems[0].Text);
            graphics.DrawImage(pic, layout);
            Offset = Offset + lineheight16;
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString("ราคา", font7, brush, layout, formatLeft);
            graphics.DrawString(listView2.SelectedItems[0].SubItems[2].Text, font8, brush, layout, formatCenter);
            graphics.DrawString("บาท", font7, brush, layout, formatRight);
            Offset = Offset + lineheight8;
            layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);
            graphics.DrawString(listView2.SelectedItems[0].SubItems[2].Text+listView2.SelectedItems[0].SubItems[3].Text+ listView2.SelectedItems[0].SubItems[4].Text, font8, brush, layout, formatCenter);

            e.HasMorePages = true;

            if (j == print) {
                e.HasMorePages = false;
                return;
            }
            j++;



        }

        private void button7_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 150 / 100f;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {

            printPreviewDialog2.PrintPreviewControl.Zoom = 150 / 100f;
            if (printPreviewDialog2.ShowDialog() == DialogResult.OK)
            {
                j = 1;
                printDocument2.Print();

            }

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            waitFrom = new WaitFrom();
            waitFrom.Show(this);

            LoadData();

        }

        private void button9_Click(object sender, EventArgs e)
        {
           waitFrom = new WaitFrom();
            waitFrom.Show(this);
            button9.Enabled = false;
            if (string.IsNullOrWhiteSpace(tbNameProduct.Text) || string.IsNullOrWhiteSpace(tbCount.Text) || string.IsNullOrWhiteSpace(tbCost.Text) || string.IsNullOrWhiteSpace(tbSell.Text)
                || string.IsNullOrWhiteSpace(tbSell2.Text) || string.IsNullOrWhiteSpace(tbSell3.Text))
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบ");
            }
            else
            {
                var client = new RestClient("https://script.google.com/macros/s/AKfycbyURj7T8d8gz6WdgHWy3l6XtLOVAN0CCc-EFufnAw/exec");
                var request = new RestRequest(Method.POST);
                /* request.AddHeader("Accept", "application/json");
                 request.AddParameter("action", "sell");
                 request.AddParameter("id", "P1001");
                 request.AddParameter("count", "2");*/

                AddModel postModel = new AddModel();
                postModel.action = "add";
                /*   postModel.name = new string[] { "กางเกงตัวละ100" };
                   postModel.count = new string[] { "10"};
                   postModel.cost = new string[] { "20" };
                   postModel.price = new string[] { "50" };
                   postModel.date = new string[] { DateTime.Now+"" };*/
                postModel.name = new string[] { tbNameProduct.Text };
                postModel.count = new string[] { tbCount.Text };
                postModel.cost = new string[] { tbCost.Text };
                postModel.sell = new string[] { tbSell.Text };
                postModel.sell2 = new string[] { tbSell2.Text };
                postModel.sell3 = new string[] { tbSell3.Text };
                postModel.date = new string[] { DateTime.Now + "" };
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

                    LoadData();

                    // initListView2();
                    /*   product = response.Data;
                       var searchForId = "P1001";
                       int index = product.product.FindIndex(p => p.id == searchForId);
                       string name = product.product[index].name;
                       label17.Text = name;*/
                }
            }


        }

        private void button10_Click(object sender, EventArgs e)
        {




            printDialog.setPrintNumber(listView2.SelectedItems[0].SubItems[6]);

            printDialog.ShowDialog(this);
            printDocument2.PrinterSettings.PrinterName = "BarcodePrint";
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                j = 1;
                print = System.Convert.ToInt32(printDialog.print);
                printDocument2.Print();

            }
            /* printPreviewDialog2.PrintPreviewControl.Zoom = 150 / 100f;


                 if (printPreviewDialog2.ShowDialog() == DialogResult.OK)
                 {





             }*/

        }

        public int getPrintNumber()
        {
            return (System.Convert.ToInt32(listView2.SelectedItems[0].SubItems[4].Text));
        }

        private void tbCost_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void tbSell_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void tbSell2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void tbSell3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void tbCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

            calculateNew();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            calculateNew();

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            calculateNew();

        }

        private void calculateNew()
        {
            label11.Text = "0";
            int sum = 0;
            if (radioButton1.Checked)
            {
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    int index = product.product.FindIndex(p => p.barcode == listView1.Items[i].SubItems[1].Text);
                    int price = System.Convert.ToInt32(product.product[index].sell);
                    listView1.Items[i].SubItems[3].Text = price + "";
                    listView1.Items[i].SubItems[5].Text = (price * System.Convert.ToInt32(listView1.Items[i].SubItems[4].Text)) + "";
                    sum += (price * System.Convert.ToInt32(listView1.Items[0].SubItems[4].Text));

                }

            }
            else if (radioButton2.Checked)
            {
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    int index = product.product.FindIndex(p => p.barcode == listView1.Items[i].SubItems[1].Text);
                    int price = System.Convert.ToInt32(product.product[index].sell2);
                    listView1.Items[i].SubItems[3].Text = price + "";
                    listView1.Items[i].SubItems[5].Text = (price * System.Convert.ToInt32(listView1.Items[i].SubItems[4].Text)) + "";
                    sum += (price * System.Convert.ToInt32(listView1.Items[0].SubItems[4].Text));

                }

            }
            else if (radioButton3.Checked)
            {
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    int index = product.product.FindIndex(p => p.barcode == listView1.Items[i].SubItems[1].Text);
                    int price = System.Convert.ToInt32(product.product[index].sell3);
                    listView1.Items[i].SubItems[3].Text = price + "";
                    listView1.Items[i].SubItems[5].Text = (price * System.Convert.ToInt32(listView1.Items[i].SubItems[4].Text)) + "";
                    sum += (price * System.Convert.ToInt32(listView1.Items[0].SubItems[4].Text));

                }
            }
            label11.Text = sum + "";



        }

        private void button2_Click(object sender, EventArgs e)
        {
            j = 1;
            print = 1;
            printPreviewDialog2.ShowDialog();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {

            var date = e.Start.ToString();
            var dateOnly = date.Split(' ');
            label15.Text = dateOnly[0];
            LoadHistory(dateOnly[0]);


        }

        private void LoadHistory(string date)
        {
            histiry = new HistoryModel();
            var client = new RestClient("https://script.google.com/macros/s/AKfycbyURj7T8d8gz6WdgHWy3l6XtLOVAN0CCc-EFufnAw/exec");
            var request = new RestRequest(Method.POST);
            /* request.AddHeader("Accept", "application/json");
             request.AddParameter("action", "sell");
             request.AddParameter("id", "P1001");
             request.AddParameter("count", "2");*/

            PostHistory postHistory = new PostHistory();

            postHistory.action = "history";
            postHistory.dateTime = date;
            string json = JsonConvert.SerializeObject(postHistory);
            // {
            //   "Name": "Apple",
            //   "Expiry": "2008-12-28T00:00:00",
            //   "Sizes": [
            //     "Small"
            //   ]
            // }
            request.RequestFormat = DataFormat.Json;
            request.AddBody(json);
            var response = client.Execute<HistoryModel>(request);

            //Response 200 OK
            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {

                //Do something with response.Content

                histiry = response.Data;
                //  MessageBox.Show(response.Data.message);
                upDateListHistory();

                // initListView2();
                /*   product = response.Data;
                   var searchForId = "P1001";
                   int index = product.product.FindIndex(p => p.id == searchForId);
                   string name = product.product[index].name;
                   label17.Text = name;*/
            }
        }

        private void upDateListHistory()
        {
            listView3.Items.Clear();

            for (int i = 0; i < histiry.history.Count; i++)
            {
                string[] arr = new string[9];
                arr[0] = histiry.history[i].barcode + ""; //บาร์โค้ด
                arr[1] = histiry.history[i].name;//ชื่อสินค้า
                arr[2] = histiry.history[i].cost; // ทุน
                arr[3] = histiry.history[i].sell; // ราคาขาย
                arr[4] = histiry.history[i].count; // จำนวน
                arr[5] = histiry.history[i].sum;//รวม
                arr[6] = histiry.history[i].profit + "";//กำไร
                arr[7] = histiry.history[i].sellType + "";//ประเภทการขาย
                arr[8] = histiry.history[i].time.Replace('a', ' ') + "";//เวลา
                var itm = new ListViewItem(arr);
                listView3.Items.Add(itm);
            }
            label16.Text = histiry.sumSell;
            label22.Text = histiry.sumProfit;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem + "" != "")
            {
                upDateMoney(comboBox1.SelectedIndex + 1 + "", comboBox2.SelectedItem + "");
            }
        }

        private void upDateMoney(string v, string selectedItem)
        {
            sellMountModel = new SellMountModel();
            histiry = new HistoryModel();
            var client = new RestClient("https://script.google.com/macros/s/AKfycbyURj7T8d8gz6WdgHWy3l6XtLOVAN0CCc-EFufnAw/exec");
            var request = new RestRequest(Method.POST);
            /* request.AddHeader("Accept", "application/json");
             request.AddParameter("action", "sell");
             request.AddParameter("id", "P1001");
             request.AddParameter("count", "2");*/

            PostSellMount postSellMount = new PostSellMount();

            postSellMount.action = "sellMount";
            postSellMount.mount = v;
            postSellMount.year = (Convert.ToInt32(selectedItem) + 543) + "";
            string json = JsonConvert.SerializeObject(postSellMount);
            // {
            //   "Name": "Apple",
            //   "Expiry": "2008-12-28T00:00:00",
            //   "Sizes": [
            //     "Small"
            //   ]
            // }
            request.RequestFormat = DataFormat.Json;
            request.AddBody(json);
            var response = client.Execute<SellMountModel>(request);

            //Response 200 OK
            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {

                histiry.history = response.Data.history;
                upDateListHistory();
                //Do something with response.Content

                sellMountModel = response.Data;
                label19.Text = sellMountModel.sumMount;
                label25.Text = sellMountModel.ProfitMount;
                //  MessageBox.Show(response.Data.message);


                // initListView2();
                /*   product = response.Data;
                   var searchForId = "P1001";
                   int index = product.product.FindIndex(p => p.id == searchForId);
                   string name = product.product[index].name;
                   label17.Text = name;*/
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem + "" != "")
            {
                upDateMoney(comboBox1.SelectedIndex + 1 + "", comboBox2.SelectedItem + "");
            }
        }

        private void listView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string name = listView2.SelectedItems[0].SubItems[1].Text;
            string sell = listView2.SelectedItems[0].SubItems[2].Text;
            string sell2 = listView2.SelectedItems[0].SubItems[3].Text;
            string sell3 = listView2.SelectedItems[0].SubItems[4].Text;
            string cost = listView2.SelectedItems[0].SubItems[5].Text;
            string stock = listView2.SelectedItems[0].SubItems[6].Text;


            editFrom.setLebel(name, sell, sell2, sell3, cost, stock);
            editFrom.ShowDialog(this);
            if (editFrom.DialogResult == DialogResult.OK)
            {
                editProduct(listView2.SelectedItems[0].SubItems[0].Text, editFrom.name, editFrom.sell, editFrom.sell2, editFrom.sell3, editFrom.cost, editFrom.stock);
                MessageBox.Show(editFrom.name);

            }

        }

        private void editProduct(string barcode, string name, string sell, string sell2, string sell3, string cost, string stock)
        {

            var client = new RestClient("https://script.google.com/macros/s/AKfycbyURj7T8d8gz6WdgHWy3l6XtLOVAN0CCc-EFufnAw/exec");
            var request = new RestRequest(Method.POST);
            /* request.AddHeader("Accept", "application/json");
             request.AddParameter("action", "sell");
             request.AddParameter("id", "P1001");
             request.AddParameter("count", "2");*/

            EditModel postModel = new EditModel();
            postModel.action = "edit";
            /*   postModel.name = new string[] { "กางเกงตัวละ100" };
               postModel.count = new string[] { "10"};
               postModel.cost = new string[] { "20" };
               postModel.price = new string[] { "50" };
               postModel.date = new string[] { DateTime.Now+"" };*/
            postModel.name = new string[] { name };
            postModel.stock = new string[] { stock };
            postModel.cost = new string[] { cost };
            postModel.sell = new string[] { sell };
            postModel.sell2 = new string[] { sell2 };
            postModel.sell3 = new string[] { sell3 };
            postModel.barcode = new string[] { barcode };
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

                LoadData();

                // initListView2();
                /*   product = response.Data;
                   var searchForId = "P1001";
                   int index = product.product.FindIndex(p => p.id == searchForId);
                   string name = product.product[index].name;
                   label17.Text = name;*/
            }

        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            serchItem();
           
           
        }

        private void serchItem()
        {
            if (findBy == 1)
            {
                listView2.Items.Clear();
                var index = product.product.FindIndex(p => p.barcode == textBox2.Text);

                string[] arr = new string[7];
                if (index == -1)
                {
                    MessageBox.Show("ไม่พบสินค้า");
                }
                else
                {
                    arr[0] = product.product[index].barcode + ""; //บาร์โค้ด
                    arr[1] = product.product[index].name;//ชื่อสินค้า
                    arr[2] = product.product[index].sell; // ราคาปลีก
                    arr[3] = product.product[index].sell2; // ราคาส่ง
                    arr[4] = product.product[index].sell3; // ราคาสาม
                    arr[5] = product.product[index].cost;//ต้นทุน
                    arr[6] = product.product[index].stock + "";//จำนวน                
                    var itm = new ListViewItem(arr);
                    listView2.Items.Add(itm);
                }
               
            }
            else
            {
                listView2.Items.Clear();

                var item = product.product.FindAll(s => s.name.Contains(textBox2.Text));
                for (int i = 0; i < item.Count; i++)
                {

                    string[] arr = new string[7];
                    arr[0] = item[i].barcode + ""; //บาร์โค้ด
                    arr[1] = item[i].name;//ชื่อสินค้า
                    arr[2] = item[i].sell; // ราคาปลีก
                    arr[3] = item[i].sell2; // ราคาส่ง
                    arr[4] = item[i].sell3; // ราคาสาม
                    arr[5] = item[i].cost;//ต้นทุน
                    arr[6] = item[i].stock + "";//จำนวน                
                    var itm = new ListViewItem(arr);
                    listView2.Items.Add(itm);
                }
            }
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            listView2.Items.Clear();
            for (int i = 0; i < product.product.Count; i++)
            {
                string[] arr = new string[7];
                arr[0] = product.product[i].barcode + ""; //บาร์โค้ด
                arr[1] = product.product[i].name;//ชื่อสินค้า
                arr[2] = product.product[i].sell; // ราคาปลีก
                arr[3] = product.product[i].sell2; // ราคาส่ง
                arr[4] = product.product[i].sell3; // ราคาสาม
                arr[5] = product.product[i].cost;//ต้นทุน
                arr[6] = product.product[i].stock + "";//จำนวน                
                var itm = new ListViewItem(arr);
                listView2.Items.Add(itm);
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar== (char)Keys.Enter)
            {
                serchItem();
              /*  listView2.Items.Clear();
                var index = product.product.FindIndex(p => p.barcode == textBox2.Text);

                string[] arr = new string[7];
                arr[0] = product.product[index].barcode + ""; //บาร์โค้ด
                arr[1] = product.product[index].name;//ชื่อสินค้า
                arr[2] = product.product[index].sell; // ราคาปลีก
                arr[3] = product.product[index].sell2; // ราคาส่ง
                arr[4] = product.product[index].sell3; // ราคาสาม
                arr[5] = product.product[index].cost;//ต้นทุน
                arr[6] = product.product[index].stock + "";//จำนวน                
                var itm = new ListViewItem(arr);
                listView2.Items.Add(itm);*/
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {

                SearchItem();

                

            }
        }

        private void SearchItem()
        {
            if (textBox4.ContainsFocus == true)
            {
                MessageBox.Show("เงินทอน  " + (System.Convert.ToInt32(textBox4.Text) - System.Convert.ToInt32(label11.Text)));
            }
            else
            {
                int index = -1;
                var searchForId = textBox1.Text;
                index = product.product.FindIndex(p => p.barcode == searchForId);
                if (index == -1)
                {
                    MessageBox.Show("ไม่พบสินค้า");
                    textBox1.Text = "";

                }
                else
                {

                    //////// เปลี่ยนราคาตรงนี่//////////////
                    string name = product.product[index].name;
                    string price = "0";

                    if (radioButton1.Checked)
                    {
                        price = product.product[index].sell;

                    }
                    else if (radioButton2.Checked)
                    {
                        price = product.product[index].sell2;

                    }
                    else if (radioButton3.Checked)
                    {
                        price = product.product[index].sell3;

                    }
                    int result = System.Convert.ToInt32(price);
                    price1 += result;
                    string[] arr = new string[7];
                    arr[0] = size + ""; //รายการ
                    arr[1] = searchForId;//รหัสสินค้า
                    arr[2] = name; // ชื่อสินค้า
                    arr[3] = price;//ราคา
                    arr[4] = count + "";//จำนวน
                    arr[5] = (result * count) + "";// รวม
                       // listBox1.Items.Add(size + "\t" + searchForId + "\t" + name + "\t" + price + " \t" + " x \t" + count + "\t" + (result * count));
                    var fond = listView1.FindItemWithText(searchForId);
                    if (fond != null)
                    {
                        var a = listView1.Items.IndexOf(fond);
                        listView1.Items[a].SubItems[4].Text = (System.Convert.ToInt32(listView1.Items[a].SubItems[4].Text) + 1) + "";
                        listView1.Items[a].SubItems[5].Text = (System.Convert.ToInt32(listView1.Items[a].SubItems[4].Text) * System.Convert.ToInt32(listView1.Items[a].SubItems[3].Text)) + "";

                    }
                    else
                    {
                        var itm = new ListViewItem(arr);
                        listView1.Items.Add(itm);
                        size++;

                    }

                    label11.Text = price1 + "";
                    textBox1.Text = "";
                }


            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            findBy = 1;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            findBy = 2;
        }

        private void monthCalendar2_DateSelected(object sender, DateRangeEventArgs e)
        {
            var a = e.Start.ToString();
            var b = a.Split(' ');
            var c = b[0].Split('/');
            dayStart = c[0];
            mountStart = c[1];
            yearStart = c[2];


        }

        private void monthCalendar3_DateSelected(object sender, DateRangeEventArgs e)
        {
            var a = e.Start.ToString();
            var b = a.Split(' ');
            var c = b[0].Split('/');
            dayStop = c[0];
            mountStop = c[1];
            yearStop = c[2];

        }

        private void button12_Click(object sender, EventArgs e)
        {
     

     

            if (dayStart == "")
            {
                MessageBox.Show("กรุณาเลือกวันเริ่มต้น");
            }else if (dayStop == "")
            {
                MessageBox.Show("กรุณาเลือกวันเริ่มต้น");

            }
            else
            {
                histiry = new HistoryModel();
                var client = new RestClient("https://script.google.com/macros/s/AKfycbyURj7T8d8gz6WdgHWy3l6XtLOVAN0CCc-EFufnAw/exec");
                var request = new RestRequest(Method.POST);
                /* request.AddHeader("Accept", "application/json");
                 request.AddParameter("action", "sell");
                 request.AddParameter("id", "P1001");
                 request.AddParameter("count", "2");*/

                PostHistoryDate postHistory = new PostHistoryDate();

                postHistory.action = "checkItem";
                postHistory.dayStart = dayStart;
                postHistory.mountStart = mountStart;
                postHistory.yearStart = yearStart;
                postHistory.dayStop = dayStop;
                postHistory.mountStop = mountStop;
                postHistory.yearStop = yearStop;
                string json = JsonConvert.SerializeObject(postHistory);
                // {
                //   "Name": "Apple",
                //   "Expiry": "2008-12-28T00:00:00",
                //   "Sizes": [
                //     "Small"
                //   ]
                // }
                request.RequestFormat = DataFormat.Json;
                request.AddBody(json);
                var response = client.Execute<HistoryModel>(request);

                //Response 200 OK
                if (response.StatusCode.Equals(HttpStatusCode.OK))
                {

                    //Do something with response.Content

                    histiry = response.Data;
                    //  MessageBox.Show(response.Data.message);
                    upDateListHistory();

                    // initListView2();
                    /*   product = response.Data;
                       var searchForId = "P1001";
                       int index = product.product.FindIndex(p => p.id == searchForId);
                       string name = product.product[index].name;
                       label17.Text = name;*/
                }
            }


           
        }

        private void button13_Click(object sender, EventArgs e)
        {
            var client = new RestClient("https://script.google.com/macros/s/AKfycbyURj7T8d8gz6WdgHWy3l6XtLOVAN0CCc-EFufnAw/exec");
            var request = new RestRequest(Method.POST);
            /* request.AddHeader("Accept", "application/json");
             request.AddParameter("action", "sell");
             request.AddParameter("id", "P1001");
             request.AddParameter("count", "2");*/

            PrinterModel postHistory = new PrinterModel();

            postHistory.action = "printer";
            postHistory.nameShop =textBox3.Text;
            postHistory.descrip = textBox6.Text;
            postHistory.tel = textBox7.Text;
            postHistory.warning = textBox8.Text;
            postHistory.thank = textBox9.Text;
            string json = JsonConvert.SerializeObject(postHistory);
            // {
            //   "Name": "Apple",
            //   "Expiry": "2008-12-28T00:00:00",
            //   "Sizes": [
            //     "Small"
            //   ]
            // }
            request.RequestFormat = DataFormat.Json;
            request.AddBody(json);
            var response = client.Execute<PrinterMessage>(request);

            //Response 200 OK
            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                MessageBox.Show("ok");

                //Do something with response.Content

            

                // initListView2();
                /*   product = response.Data;
                   var searchForId = "P1001";
                   int index = product.product.FindIndex(p => p.id == searchForId);
                   string name = product.product[index].name;
                   label17.Text = name;*/
            }else
            {
                MessageBox.Show(response.Content);

            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            itemFrom.setText(listView1.SelectedItems[0].SubItems[2], listView1.SelectedItems[0].SubItems[3], listView1.SelectedItems[0].SubItems[4]);
            itemFrom.ShowDialog(this);
            if (itemFrom.DialogResult == DialogResult.OK)
            {
                if (itemFrom.count != "a")
                {
                    listView1.SelectedItems[0].SubItems[4].Text = itemFrom.count;
                    listView1.SelectedItems[0].SubItems[5].Text = System.Convert.ToInt32(listView1.SelectedItems[0].SubItems[3].Text) * System.Convert.ToInt32(listView1.SelectedItems[0].SubItems[4].Text) + "";

                }
                else
                {
                    listView1.Items.Remove(listView1.SelectedItems[0]);
                }

            }

        }

        private void monthCalendar2_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {


                sellItem();

            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {


                sellItem();

            }
        }
    }
}
    
