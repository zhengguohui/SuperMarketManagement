using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Website
{
    public partial class editaccount : System.Web.UI.Page
    {
        string t1, t2, t3, t4, t5, t;
        protected void Page_Load(object sender, EventArgs e)
        {
            t1 = TextBox1.Text;
            t2 = TextBox2.Text;
            t3 = TextBox3.Text;
            t4 = TextBox4.Text;
            t5 = TextBox5.Text;
            t = DropDownList1.SelectedIndex.ToString();
            this.Title = ClassMain.GetPageTitle("会员信息修改");
            if (Session["Login"] == "1")
            {
                string sql = "select smm_idcard,smm_address,smm_name,smm_sex,smm_telephone,smm_cellphone,smm_jifen from smm_customer where smm_cardnumber='" + Session["username"] + "'";
                ClassManageDataBase db = new ClassManageDataBase();
                SqlDataReader dr = db.SQLReader(sql);
                dr.Read();
                if (dr["smm_sex"].ToString() == "1")
                {
                    DropDownList1.SelectedIndex = 0;
                }
                else if (dr["smm_sex"].ToString() == "0")
                {
                    DropDownList1.SelectedIndex = 1;
                }
                else
                {
                    DropDownList1.SelectedIndex = 2;
                }
                TextBox1.Text = dr["smm_name"].ToString();
                TextBox2.Text = dr["smm_cellphone"].ToString();
                TextBox3.Text = dr["smm_telephone"].ToString();
                TextBox4.Text = dr["smm_idcard"].ToString();
                TextBox5.Text = dr["smm_address"].ToString();
                db.Close();

            }
            else { Response.Redirect("default.aspx"); }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string sex = "";
            if (t == "0")
            {
                sex = "1";
            }
            else if (t == "1")
            {
                sex = "0";
            }
            else
            {
                sex = "2";
            }
            string sql = String.Format("update smm_customer set smm_name='{0}',smm_sex='{1}',smm_cellphone='{2}',smm_telephone='{3}',smm_idcard='{4}',smm_address='{5}' where smm_cardnumber='{6}'", t1, sex, t2, t3, t4, t5, Session["username"]);
            ClassManageDataBase db = new ClassManageDataBase();
            db.SQLExecute(sql);
            Response.Redirect("account.aspx");

        }
    }
}