using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SuperMarketManagement
{
    public partial class FormCustomerJifen : Form
    {
        string cardnumber;
        public FormCustomerJifen(string a)
        {
            InitializeComponent();
            cardnumber = a;
        }

        private void FormJifen_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            string sql = String.Format("select smm_jifen from smm_customer where id={0}", cardnumber);
            ClassManageDataBase db = new ClassManageDataBase();
            SqlDataReader dr = db.SQLReader(sql);
            dr.Read();
            textBox1.Text = dr[0].ToString();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Trim() == "")
            {
                MessageBox.Show("请输入消费积分数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.SelectAll();
                textBox2.Focus();
            }
            else if (Convert.ToInt32(textBox2.Text) > Convert.ToInt32(textBox1.Text))
            {
                MessageBox.Show("可消费积分不足！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.SelectAll();
                textBox2.Focus();
            }
            else if (Convert.ToInt32(textBox2.Text) < 0)
            {
                MessageBox.Show("消费积分不可以为负数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.SelectAll();
                textBox2.Focus();
            }

            else
            {
                string sql = String.Format("update smm_customer set smm_jifen={0} where id={1}", Convert.ToInt32(textBox1.Text) - Convert.ToInt32(textBox2.Text), cardnumber);
                ClassManageDataBase db = new ClassManageDataBase();
                db.SQLExecute(sql);
                MessageBox.Show("积分成功扣除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
    }
}
