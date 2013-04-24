using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Website
{
    public partial class money : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] != "1")
            {
                Response.Redirect("default.aspx");
            }
            this.Title = ClassMain.GetPageTitle("消费金额查询");
            Button1_Click(sender, e);
            int max = 1;
            ArrayList li = new ArrayList();
            string[] m1 = d3.Split(new char[] { '/' });
            string[] m2 = d4.Split(new char[] { '/' });
            for (int i = Convert.ToInt32(m2[0]); i >= Convert.ToInt32(m1[0]); i--)
            {
                for (int j = 12; j >= 1; j--)
                {
                    bool ok = true;
                    if (i == Convert.ToInt32(m1[0]))
                    {
                        if (j < Convert.ToInt32(m1[1]))
                        {
                            ok = false;
                        }
                    }
                    if (i == Convert.ToInt32(m2[0]))
                    {
                        if (j > Convert.ToInt32(m2[1]))
                        {
                            ok = false;
                        }
                    }
                    if (ok)
                    {
                        string a = i + "/" + j + "/1 00:00:01";
                        string b = i + "/" + (j + 1) + "/1 00:00:00";
                        string sql = "select sum(smm_price) from smm_sell where (smm_time >= '" + a + "' and smm_time <= '" + b + "') and smm_customer='" + Session["username"] + "'";
                        ClassManageDataBase db = new ClassManageDataBase();
                        SqlDataReader dr = db.SQLReader(sql);
                        try
                        {
                            if (dr.Read())
                            {
                                li.Add(i + "年" + j + "月");

                                double sum = 0;
                                if (dr[0].ToString() != "")
                                {
                                    sum = Convert.ToDouble(dr[0].ToString());
                                }
                                li.Add(sum.ToString("0.00"));
                                if (sum > max)
                                {
                                    max = Convert.ToInt32(sum);
                                }
                            }
                        }
                        catch { }
                    }
                }
            }
            string str = "";
            for (int x = 0; x < li.Count; x = x + 2)
            {
                str += "<tr><td>";
                str += li[x].ToString();
                str += "</td><td>";
                str += li[x + 1].ToString() + "元";
                str += "</td><td>";
                str += ClassMain.ShowProgress((Convert.ToInt32(Convert.ToDouble(li[x + 1]) * 100 / max)).ToString());
                str += "</td></tr>";
            }
            Label1.Text = str;
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