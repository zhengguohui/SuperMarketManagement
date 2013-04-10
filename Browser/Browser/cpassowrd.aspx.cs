using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Website
{
    public partial class cpassowrd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = "";
            this.Title = ClassMain.GetPageTitle("修改密码");
            if (Session["Login"] != "1")
            {
                Response.Redirect("default.aspx");
            }

            if (!IsPostBack)
            {
                TextBox1.Focus();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //this.TextBox1.Attributes.Add("value", Request["TextBox1"]);
            //this.TextBox2.Attributes.Add("value", Request["TextBox2"]);
            //this.TextBox3.Attributes.Add("value", Request["TextBox3"]);
            TextBox1.Attributes["value"] = TextBox1.Text;
            TextBox2.Attributes["value"] = TextBox2.Text;
            TextBox3.Attributes["value"] = TextBox3.Text;
            ClassEncryption ce = new ClassEncryption();
            string t1 = TextBox1.Text;
            string t2 = TextBox2.Text;
            string t3 = TextBox3.Text;
            if (t1 == "")
            {
                Label1.Text = ClassMain.ShowAlert("请输入原始密码！");
                TextBox1.Focus();
            }
            else if (t2 == "")
            {
                Label1.Text = ClassMain.ShowAlert("请输入新设密码！");
                TextBox2.Focus();
            }
            else if (t3 == "")
            {
                Label1.Text = ClassMain.ShowAlert("请输入密码重复！");
                TextBox3.Focus();
            }
            else if (t3 != t2)
            {
                Label1.Text = ClassMain.ShowAlert("两次密码输入不同！");
                TextBox2.Focus();
            }
            else
            {
                string sql = "select smm_password from smm_customer where smm_cardnumber='" + Session["username"] + "' and smm_password='" + ce.Encode(t1) + "'";


                ClassManageDataBase db = new ClassManageDataBase();
                if (db.SQLNumber(sql) <= 0)
                {
                    Label1.Text = ClassMain.ShowAlert("原始密码错误！");
                    TextBox1.Focus();
                }
                else
                {
                    string sql1 = "update smm_customer set smm_password='" + ce.Encode(t2) + "' where smm_cardnumber='" + Session["username"] + "'";
                    ClassManageDataBase db1 = new ClassManageDataBase();
                    db1.SQLExecute(sql1);
                    Response.Redirect("account.aspx");
                }
            }

        }
    }
}