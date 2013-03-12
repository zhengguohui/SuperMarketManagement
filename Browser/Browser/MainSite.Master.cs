using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Website
{
    public partial class MainSite : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LabelCompanyName.Text = ClassMain.GetCompanyName();
            LabelCopyRight.Text = ClassMain.GetCompanyName();
            LabelGoodList.Text = ClassMain.GetGoodList();
            string menu="";
             menu+="<ul class='nav'>";
                    menu+="<li class='divider-vertical'></li>";
                    menu+="<li class='active1'><a href='default.aspx'>首页</a></li>";
                  //menu+="<li class='divider-vertical'></li>";
                    menu += "<li><a href='list.aspx'>购物车</a></li>";
            if (Session["Login"] == "1")
            {
                 menu+="<li><a href='account.aspx'>我的账户</a></li>";
                 menu += "<li><a href='logout.aspx'>退出</a></li>";

            }
            else
            {
                menu += "<li><a href='login.aspx'>登录</a></li>";
            }
                   
                    menu += "</ul>";
                    LabelLogin.Text = menu;

        }
    }
}