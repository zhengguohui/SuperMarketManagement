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
    public partial class FormAccountInfomation : Form
    {
        private string username;
        public FormAccountInfomation(string username1)
        {
            InitializeComponent();
            username = username1;
        }
        private void FormAccountInfomation_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            string sql = String.Format("select smm_nickname,smm_tag from smm_user where smm_username='{0}'", username);
            ClassManageDataBase db = new ClassManageDataBase();
            SqlDataReader dr = db.SQLReader(sql);
            dr.Read();
            label1.Text = "账户：" + username;
            label2.Text = "昵称：" + dr[0].ToString();
            string js = "";
            if (dr[1].ToString() == "1")
            {
                js = "超级管理员";
            }
            else if (dr[1].ToString() == "2")
            {
                js = "会员管理员";
            }
            else if (dr[1].ToString() == "3")
            {
                js = "商品信息管理员";
            }
            else if (dr[1].ToString() == "4")
            {
                js = "入库员";
            }
            else if (dr[1].ToString() == "5")
            {
                js = "销售员";
            }
            else if (dr[1].ToString() == "6")
            {
                js = "订单管理员";
            }
            else if (dr[1].ToString() == "7")
            {
                js = "新闻管理员";
            }
            else
            {
                js = "未知";
            }
            label3.Text = "角色：" + js;
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}