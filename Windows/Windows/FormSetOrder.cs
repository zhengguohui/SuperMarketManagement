using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SuperMarketManagement
{
    public partial class FormSetOrder : Form
    {
        double money1;
        public bool OK = false;
        public FormSetOrder(double a)
        {
            InitializeComponent();
            money1 = a;
        }

        private void FormSetOrder_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            textBox1.Text = money1.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OK = false;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double b = Convert.ToDouble(textBox3.Text);
                if (b >= 0)
                {
                    OK = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("输入错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.SelectAll();
                    textBox2.Focus();
                }
            }
            catch
            {
                MessageBox.Show("输入错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.SelectAll();
                textBox2.Focus();
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double b = Convert.ToDouble(textBox2.Text);
                textBox3.Text = (b - money1).ToString();
            }
            catch { }
        }
    }
}
