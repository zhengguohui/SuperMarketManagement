using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Website
{
    public partial class account : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == "1")
            {
                this.Title = ClassMain.GetPageTitle("我的账户");
                string sql = "select smm_cardnumber,smm_idcard,smm_address,smm_name,smm_sex,smm_telephone,smm_cellphone,smm_jifen from smm_customer where smm_cardnumber='" + Session["username"] + "'";
                ClassManageDataBase db = new ClassManageDataBase();
                SqlDataReader dr = db.SQLReader(sql);
                dr.Read();
                Label0.Text = Session["username"].ToString();
                Label1.Text = dr["smm_name"].ToString();
                string sex = "";
                if (dr["smm_sex"].ToString() == "1")
                {
                    sex = "男";
                }
                else if (dr["smm_sex"].ToString() == "0")
                {
                    sex = "女";
                }
                else
                {
                    sex = "未知";
                }
                Label2.Text = sex;
                Label3.Text = dr["smm_cellphone"].ToString();
                Label4.Text = dr["smm_telephone"].ToString();
                Label5.Text = dr["smm_idcard"].ToString();
                Label6.Text = dr["smm_address"].ToString();
                Label00.Text = dr["smm_jifen"].ToString();
                db.Close();
            }
            else
            {
                Response.Redirect("default.aspx");
            }
        }
    }
}