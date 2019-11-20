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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            
        }
        public void setLabel(string label)
        {
            label1.Text = label;
        }

       
    }
}
