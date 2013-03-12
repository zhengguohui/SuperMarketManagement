using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Website
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == "1")
            {
                Response.Redirect("default.aspx");
            }
            this.Title = ClassMain.GetPageTitle("登录");
            TextBoxLoginUsername.Focus();
        }

        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
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
            else if (ClassMain.CheckLogin(TextBoxLoginUsername.Text.Trim(), TextBoxLoginPassword.Text.Trim()))
            {
                Session["Login"] = "1";
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