using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Website
{
    public partial class login : System.Web.UI.Page
    {
        bool autologin=true;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == "1")
            {
                Response.Redirect("default.aspx");
            }
            this.Title = ClassMain.GetPageTitle("登录");
            TextBoxLoginUsername.Focus();

            if (autologin == true)
            {
                if (Convert.ToInt32(Request.QueryString["u"])==0 || Convert.ToInt32(Request.QueryString["p"]) == 0)
                {

                }
                else
                {
                    Login(Request.QueryString["u"], Request.QueryString["p"]);
                }
            }
        }

        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            autologin = false;
            if (TextBoxLoginUsername.Text.Trim() == "")
            {
                LabelAlert.Text = ClassMain.ShowAlert("请输入用户名！");
                TextBoxLoginUsername.Focus();
            }
            else if (TextBoxLoginPassword.Text.Trim() == "")
            {
                LabelAlert.Text = ClassMain.ShowAlert("请输入密码！");
                TextBoxLoginPassword.Focus();
            }
            else
            {
                Login(TextBoxLoginUsername.Text.Trim(), TextBoxLoginPassword.Text.Trim());
         
            }
        }
        void Login(string a,string b)
        {
            if (ClassMain.CheckLogin(a, b))
            {
                Session["Login"] = "1";
                Session["username"] = a;
                Response.Redirect("default.aspx");
            }
            else
            {
                LabelAlert.Text = ClassMain.ShowAlert("密码错误！");
                TextBoxLoginUsername.Focus();
            }
        }
    }
}