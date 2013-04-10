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
    public partial class FormSetNews : Form
    {
        int action;
        string id;
        public FormSetNews(int a, string b)
        {
            InitializeComponent();
            action = a;
            id = b;
        }

        private void FormSetNews_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            if (action == 1)
            {
                this.Text = "添加新闻";
            }
            else
            {
                this.Text = "修改新闻";
                string sql = String.Format("select smm_title,smm_content from smm_news where id={0}", id);
                ClassManageDataBase db = new ClassManageDataBase();
                SqlDataReader dr = db.SQLReader(sql);
                dr.Read();
                textBox1.Text = dr["smm_title"].ToString();
                textBox2.Text = dr["smm_content"].ToString();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("请输入标题！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.SelectAll();
                textBox1.Focus();
            }
            else if (textBox2.Text.Trim() == "")
            {
                MessageBox.Show("请输入内容！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.SelectAll();
                textBox2.Focus();
            }
            else
            {
                string sql = "";
                if (action == 1)
                {
                    sql = String.Format("insert into smm_news (smm_title,smm_content,smm_time) values ('{0}','{1}','{2}')", textBox1.Text, textBox2.Text, DateTime.Now);
                }
                else
                {
                    sql = String.Format("update smm_news set smm_title='{0}',smm_content='{1}' where id={2}", textBox1.Text, textBox2.Text, id);
                }
                ClassManageDataBase db = new ClassManageDataBase();
                db.SQLExecute(sql);
                this.Close();
            }
        }
    }
}
