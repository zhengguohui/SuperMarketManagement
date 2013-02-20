using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SuperMarketManagement
{
    public partial class FormSetSellNumber : Form
    {
        string name;
        public string number;

        public FormSetSellNumber(string a,string b)
        {
            InitializeComponent();
            name = a;
            number = b;
 
        }

        private void FormSetSellNumber_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size; 
            textBox1.Text = name;
            textBox2.Text = number;
            textBox2.SelectAll();
            textBox2.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

           int  a = Convert.ToInt32(number);
            try
            {
                a = Convert.ToInt32(textBox2.Text);
                if (a < 0)
                {
                    a = 0;
                }
                number = a.ToString();
                this.Close();
            }
            catch
            {
                MessageBox.Show("输入错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.SelectAll();
                textBox2.Focus();
            }
            
        }
    }
}
