using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Website
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int sum =20;

            string p = Request.QueryString["p"];
            string sql1 = "select id from smm_news";
            ClassManageDataBase db = new ClassManageDataBase();

            int m = db.SQLNumber(sql1);
            this.Title =ClassMain.GetPageTitle("新闻");
            Label1.Text = ClassMain.NewsList("select top " + sum.ToString() + " id,smm_title from smm_news where id not in (select top " + (Convert.ToInt32(p) * sum).ToString() + " id from smm_news order by smm_time desc) order by smm_time desc");




            int page = m / sum;
            if (m % sum > 0)
            {
                page++;
            }

            string re = "";
            re += "<div class='pagination' style='clear:both;'><ul>";
            if (Convert.ToInt32(p) > 0)
            {
                re += "<li><a href='?p=" + (Convert.ToInt32(p) - 1).ToString() +"'>上一页</a></li>";
            }
            for (int i = 0; i < page; i++)
            {
                re += "<li><a href='?p=" + i.ToString() +"'>" + (i + 1).ToString() + "</a></li>";
            }

            if (Convert.ToInt32(p) < (page - 1))
            {
                re += "<li><a href='?p=" + (Convert.ToInt32(p) + 1).ToString() +  "'>下一页</a></li>";
            }
            re += "</ul></div>";




            Label2.Text = re;
        }
    }
}