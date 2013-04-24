using System;
using System.Collections.Generic;
using System.Collections;
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
            ArrayList li = get();
            int sum = li.Count / 2;
            string s = "";
            if (sum > 0)
            {
                s = "<li style='padding-top:0px;'><a href='list.aspx'><span class='badge badge-warning'>" + sum + "</span></a></li>";
            }
            string menu = "";
            menu += "<ul class='nav'>";
            menu += "<li class='divider-vertical'></li>";
            menu += "<li class='active1'><a href='default.aspx'>首页</a></li>";
            //menu+="<li class='divider-vertical'></li>";
            if (Session["Login"] == "1")
            {
                menu += "<li><a href='account.aspx'>我的账户</a></li>";
                menu += "<li><a href='logout.aspx'>退出</a></li>";
            }
            else
            {
                menu += "<li><a href='login.aspx'>登录</a></li>";
            }
            menu += "<li><a href='list.aspx'>购物车</a></li>";
            menu += s;
            menu += "</ul>";
            LabelLogin.Text = menu;
            LabelNewsList.Text = ClassMain.NewsList("select top 5 id,smm_title from smm_news order by smm_time desc");
        }
        ArrayList get()
        {
            string cookie = "";
            try
            {
                cookie = Request.Cookies["good"].Value;
            }
            catch
            {

            }
            string newCookie = "";
            try
            {
                newCookie = cookie.Substring(1);
            }
            catch { }
            string[] m = newCookie.Split(new char[] { ',' });
            ArrayList li = new ArrayList();
            int temp = 0;
            for (int i = 0; i < m.Length; i = i + 2)
            {
                if (temp == 0)
                {
                    try
                    {
                        li.Add(m[i]);
                        li.Add(m[i + 1]);
                    }
                    catch { }
                    temp++;
                }
                else
                {
                    int f = 0;
                    int x = 0;
                    for (int j = 0; j < li.Count; j = j + 2)
                    {
                        if (m[i].ToString().Trim() == li[j].ToString().Trim())
                        {
                            li[j + 1] = (Convert.ToInt32(li[j + 1]) + Convert.ToInt32(m[i + 1])).ToString();
                            x = 1;
                        }
                        else
                        {
                            f = 1;
                        }
                    }
                    if (f == 1 && x != 1)
                    {
                        li.Add(m[i]);
                        li.Add(m[i + 1]);
                    }
                }

            }
            return li;
        }
    }
}