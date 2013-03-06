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
    public partial class FormCustomerChangeCard : Form
    {
        string cardnumber;
        public FormCustomerChangeCard(string a)
        {
            InitializeComponent();
            cardnumber = a;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Trim() == "")
            {
                MessageBox.Show("请输入新卡号数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.SelectAll();
                textBox2.Focus();
            }
            else
            {
                string sql = String.Format("select smm_cardnumber from smm_customer where smm_cardnumber='{0}'", textBox2.Text.Trim());
                ClassManageDataBase db = new ClassManageDataBase();
                if (db.SQLNumber(sql) > 0)
                {
                    MessageBox.Show("新卡号已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.SelectAll();
                    textBox2.Focus();
                }
                else
                {
                    string sql1 = String.Format("update smm_customer set smm_cardnumber='{0}' where id={1}",textBox2.Text,cardnumber);
                    ClassManageDataBase db1 = new ClassManageDataBase();
                    db1.SQLExecute(sql1);
                    MessageBox.Show("换卡成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private void FormCustomerChangeCard_Load(object sender, EventArgs e)
        {
            ClassManageDataBase db = new ClassManageDataBase();
            string sql = String.Format("select smm_cardnumber from smm_customer where id={0}",cardnumber);
            SqlDataReader dr = db.SQLReader(sql);
            dr.Read();
            textBox1.Text = dr[0].ToString();
        }
    }
}
