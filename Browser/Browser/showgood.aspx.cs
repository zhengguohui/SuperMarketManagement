using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Website
{
    public partial class showgood : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label3.Text = "";
            string sql = "select smm_sum,smm_number,smm_picture,smm_name,smm_price,smm_danwei,smm_tag,smm_about from smm_product where smm_number='" + Request.QueryString["id"] + "'";
            ClassManageDataBase db = new ClassManageDataBase();
            SqlDataReader da = db.SQLReader(sql);
            if (da.Read())
            {
                Label1.Text = "<img data-src='holder.js/300x200' alt='300x200' style='width: 300px; height: 200px;' src='include/goods/" + da["smm_number"].ToString() + "." + da["smm_picture"].ToString() + "'>";
                string m = "<table class='table table-bordered table-hover'>";
                m += "<tr><td colspan='2'><h4>" + da["smm_name"].ToString() + "</h4></td></tr>";
                m += "<tr><td>商品价格：</td><td>" + Convert.ToDouble(da["smm_price"]).ToString("0.00") + "元/" + da["smm_danwei"].ToString() + "</td></tr>";
                m += "<tr><td>商品标签：</td><td>" + da["smm_tag"].ToString() + "</td></tr>";
                m += "<tr><td colspan='2'>" + da["smm_about"].ToString() + "</td></tr>";
                m += "</table>";
                Label2.Text = m;
                this.Title = ClassMain.GetPageTitle(da["smm_name"].ToString());
                if (Convert.ToInt32(da["smm_sum"]) <= 0)
                {
                    Button1.Visible = false;
                    Button2.Visible = false;
                    Label3.Text = ClassMain.ShowAlert("该商品暂时无货！");
                }
            }
            db.Close();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string OldCookie = "";
            try
            {
                OldCookie = Request.Cookies["good"].Value;
            }
            catch { OldCookie = ""; }
            string NewCookie = "," + Request.QueryString["id"].ToString() + "," + Convert.ToInt32(TextBox1.Text).ToString() + OldCookie;
            HttpCookie hc = new HttpCookie("good");
            hc.Value = NewCookie;
            Response.Cookies.Add(hc);
            Label3.Text = ClassMain.ShowInformation("成功放入购物车！");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Button2_Click(sender, e);
            Response.Redirect("list.aspx");
        }
    }
}