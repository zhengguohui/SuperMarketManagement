using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Website
{
    public partial class price : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] != "1")
            {
                Response.Redirect("default.aspx");
            }
            

            string sql = "select smm_name from smm_product where smm_number='"+Request.QueryString["id"]+"'";
            ClassManageDataBase db = new ClassManageDataBase();
            SqlDataReader dr = db.SQLReader(sql);
            dr.Read();

            Label1.Text = "价格趋势："+dr["smm_name"].ToString();
            this.Title = ClassMain.GetPageTitle(dr["smm_name"].ToString()+"的价格趋势");
            db.Close();

            Label2.Text = ClassMain.ShowPriceList(Session["username"].ToString(), Request.QueryString["id"].ToString());
        }
    }
}