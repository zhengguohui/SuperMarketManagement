using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Website
{
    public partial class history : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "订单列表";
            Label1.Text = "";
            string str = "";
            if (Session["Login"] != "1")
            {
                Response.Redirect("default.aspx");
            }
            Button1_Click(sender, e);
            int sum = 10;
            string p = Request.QueryString["p"];
            string sqlm = "select id from smm_sell where (smm_time>='" + d3 + "' and smm_time<='" + d4 + "') and smm_customer='" + Session["username"] + "' order by id desc";
            ClassManageDataBase dbm = new ClassManageDataBase();
            int m = dbm.SQLNumber(sqlm);
            string sql = "select top " + sum.ToString() + " id,smm_time,smm_price,smm_tag from smm_sell where (smm_time>='" + d3 + "' and smm_time<='" + d4 + "') and smm_customer='" + Session["username"] + "' and id not in (select top " + (Convert.ToInt32(p) * sum).ToString() + " id from smm_sell order by id desc) order by id desc";
            // Label1.Text = sql;
            ClassManageDataBase db = new ClassManageDataBase();
            SqlDataReader dr = db.SQLReader(sql);
            while (dr.Read())
            {
                string price = Convert.ToDouble(dr["smm_price"]).ToString("0.00");
                string time = dr["smm_time"].ToString();
                string tag = "";
                if (dr["smm_tag"].ToString() == "0")
                {
                    tag = "实体店订单";
                }
                else if (dr["smm_tag"].ToString() == "1")
                {
                    tag = "网络订单（已下单）";
                }
                else if (dr["smm_tag"].ToString() == "2")
                {
                    tag = "网络订单（已打包）";
                }
                else
                {
                    tag = "网络订单（已完成）";
                }
                str += String.Format("<tr><td><div>时间：{0}</div><div>金额：{1}元</div><div>状态：{2}</div></td>", time, price, tag);
                str += "<td>";
                str += ClassMain.ShowOrder(dr["id"].ToString());
                str += "</td></tr>";
            }
            Label1.Text = str;
            db.Close();
            int page = m / sum;
            if (m % sum > 0)
            {
                page++;
            }
            string re = "";
            re += "<div class='pagination' style='clear:both;'><ul>";
            if (Convert.ToInt32(p) > 0)
            {
                re += "<li><a href='?p=" + (Convert.ToInt32(p) - 1).ToString() + "'>上一页</a></li>";
            }
            for (int i = 0; i < page; i++)
            {
                re += "<li><a href='?p=" + i.ToString() + "'>" + (i + 1).ToString() + "</a></li>";
            }
            if (Convert.ToInt32(p) < (page - 1))
            {
                re += "<li><a href='?p=" + (Convert.ToInt32(p) + 1).ToString() + "'>下一页</a></li>";
            }
            re += "</ul></div>";
            Label2.Text = re;
        }
        string d3 = Convert.ToDateTime("2013/1/1").ToShortDateString() + " 00:00:00";
        string d4 = Convert.ToDateTime(DateTime.Now).ToShortDateString() + " 23:59:59";
        protected void Button1_Click(object sender, EventArgs e)
        {
            string d1, d2;
            try
            {
                d1 = Convert.ToDateTime(TextBox1.Text).ToShortDateString(); ;
                d2 = Convert.ToDateTime(TextBox2.Text).ToShortDateString();
            }
            catch
            {
                d1 = Convert.ToDateTime("2013/1/1").ToShortDateString();
                d2 = Convert.ToDateTime(DateTime.Now).ToShortDateString();
            }
            TextBox1.Text = d1;
            TextBox2.Text = d2;
            d3 = d1 + " 00:00:00";
            d4 = d2 + " 23:59:59";
        }
    }
}