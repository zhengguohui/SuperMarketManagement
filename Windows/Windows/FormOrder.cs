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
    public partial class FormOrder : Form
    {
        string id;
        public FormOrder(string a)
        {
            InitializeComponent();
            id = a;
        }
        double total = 0;
        private void FormOrder_Load(object sender, EventArgs e)
        {
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.View = View.Details;
            listView1.Scrollable = true;
            listView1.MultiSelect = false;
            listView1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView1.Columns.Add("商品", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("数量", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("单价", 100, HorizontalAlignment.Right);
            listView1.Columns.Add("小结", 150, HorizontalAlignment.Left);
            string sql1 = String.Format("select smm_product,smm_sum,smm_price from smm_order where smm_sell='{0}'", id);
            ClassManageDataBase db1 = new ClassManageDataBase();
            SqlDataReader dr1 = db1.SQLReader(sql1);
            while (dr1.Read())
            {
                ListViewItem li = new ListViewItem();
                li.SubItems.Clear();
                string sql2 = String.Format("select smm_name from smm_product where smm_number='{0}'", dr1[0]);
                ClassManageDataBase db2 = new ClassManageDataBase();
                SqlDataReader dr2 = db2.SQLReader(sql2);
                string name = "该商品已删除";
                if (dr2.Read())
                {
                    name = dr2[0].ToString();
                }
                li.SubItems[0].Text = name;
                li.SubItems.Add(dr1[1].ToString());
                li.SubItems.Add(dr1[2].ToString());
                double g = Convert.ToInt32(dr1[1]) * Convert.ToDouble(dr1[2]);
                total += g;
                li.SubItems.Add(g.ToString());
                //li.Tag = dr1[0].ToString();
                listView1.Items.Add(li);
            }
            label1.Text = "总价：" + total.ToString();
            string sql = String.Format("select smm_tag from smm_sell where id={0}", id);
            ClassManageDataBase db = new ClassManageDataBase();
            SqlDataReader dr = db.SQLReader(sql);
            dr.Read();
            if (dr[0].ToString() == "0")
            {
                button1.Visible = false;
                button2.Visible = false;
                this.AcceptButton = button3;
            }
            else if (dr[0].ToString() == "1")
            {
                button2.Visible = false;
                this.AcceptButton = button1;
            }
            else if (dr[0].ToString() == "2")
            {
                button1.Visible = false;
                this.AcceptButton = button2;
            }
            else
            {
                button1.Visible = false;
                button2.Visible = false;
                this.AcceptButton = button3;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确定完成打包？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                String sql = String.Format("update smm_sell set smm_tag=2 where id={0}", id);
                ClassManageDataBase db = new ClassManageDataBase();
                db.SQLExecute(sql);
                /*
                button1.Visible = false;
                button2.Visible = true;
                this.AcceptButton = button2;
                 * */
                MessageBox.Show("打包成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormSetOrder abc = new FormSetOrder(total);
            abc.ShowDialog();
            if (abc.OK)
            {
                String sql = String.Format("update smm_sell set smm_tag=3 where id={0}", id);
                ClassManageDataBase db = new ClassManageDataBase();
                db.SQLExecute(sql);
                /*
                button1.Visible = false;
                button2.Visible = true;
                this.AcceptButton = button2;
                 * */
                MessageBox.Show("订单完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
    }
}
