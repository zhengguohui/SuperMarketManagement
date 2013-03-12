using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;

namespace Website
{
    public static class ClassMain
    {
        public static string GetCompanyName()
        {
            ClassSettings cs = new ClassSettings();
            return cs.GetSettings("supermarketname");
        }
        public static string GetPageTitle(string title)
        {
            string re = "";
            if (title == "")
            {
                re = GetCompanyName();
            }
            else
            {
                re = title + " - " + GetCompanyName();
            }
            return re;
        }
        public static string GetGoodList()
        {
            string re = "";
            int sum = 0;
            string[][] goods ={
                                 new string[]{"热门商品","手机","超级本","奶粉","电视","平板电脑"},
                                 new string[]{"食品","饼干"},
                                 new string[]{"食品","饼干"},
                                 new string[]{"食品","饼干"},
                                 new string[]{"食品","饼干"},
                                 new string[]{"食品","饼干"},
                                 new string[]{"食品","饼干"},
                                 new string[]{"你好","测试"}
                             };
            re += "<div class='accordion' id='accordion2'>";
            foreach (string[] a in goods)
            {

                re += "<div class='accordion-group' style='-webkit-border-radius: 0px;-moz-border-radius: 0px;border-radius: 0px;'>";
                re += "<div class='accordion-heading' style='background-image: url(include/title.jpg); background-repeat: repeat-x;'>";
                re += "<a class='accordion-toggle' data-toggle='collapse' data-parent='#accordion2' href='#collapse" + sum.ToString() + "'>";
                re += a[0];
                re += "</a>";
                re += "</div>";
                if (sum == 0)
                {
                    re += " <div id='collapse" + sum.ToString() + "' class='accordion-body collapse in'>";
                }
                else
                {
                    re += " <div id='collapse" + sum.ToString() + "' class='accordion-body collapse'>";
                }
                re += "<div class='accordion-inner' style='line-height:25px;'>";
                foreach (string b in a)
                {
                    re += "<a href=\"search.aspx?s=" + b + "\">" + b + "</a>&nbsp;&nbsp;";
                }
                re += "</div> </div></div>";
                sum++;
            }
            re += "</div>";
            return re;
            //
        }
        public static string ShowAlert(string str)
        {
            string a = "";
            a += "<div class='alert alert-error'>";
            a += "<button type='button' class='close' data-dismiss='alert'>&times;</button>";
            a += str;
            a += "</div>";
            return a;
        }
        public static string ShowInformation(string str)
        {
            string a = "";
            a += "<div class='alert alert-info'>";
            a += "<button type='button' class='close' data-dismiss='alert'>&times;</button>";
            a += str;
            a += "</div>";
            return a;
        }
        public static bool CheckLogin(string username, string password)
        {
            ClassManageDataBase db = new ClassManageDataBase();
            ClassEncryption ce = new ClassEncryption();
            string sql = "select smm_cardnumber from smm_customer where smm_cardnumber='" + username + "' and smm_password='" + ce.Encode(password) + "'";
            int m = db.SQLNumber(sql);
            if (m > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static string ShowGoods(string sql)
        {
            string re = "";
            int sum=0;
            ClassManageDataBase db = new ClassManageDataBase();
            SqlDataReader dr = db.SQLReader(sql);
            while (dr.Read())
            {
                if (sum == 0)
                {
                    re += "<div class='row-fluid'>";
                    re += "<ul class='thumbnails'>";
                }
                sum++;
               
                re += "<li class='span4'>";
                re += "<div class='thumbnail'>";
                re += "<a target='_blank' href='showgood.aspx?id=" + dr["smm_number"].ToString() + "'>";
                re += "<img data-src='holder.js/300x200' alt='300x200' style='width: 300px; height: 200px;'  src='include/goods/" + dr["smm_number"].ToString() + "." + dr["smm_picture"].ToString() + "' />";
                re += "<div class='caption'>";
                re += "<h4>"+dr["smm_name"].ToString()+"</h4>";
                re += "<p>价格："+dr["smm_price"].ToString()+"元/"+dr["smm_danwei"].ToString()+"</p>";
                re += "</div></a></div></li>";
                if (sum == 3)
                {
                    re += "</ul>";
                    re += "</div>";
                }
               
                if (sum >=3)
                {
                    sum = 0;
                }

            }
           
            return re;













        }
        public static string NewsList(string sql)
        {
            string re = "";
            ClassManageDataBase db = new ClassManageDataBase();
            SqlDataReader dr = db.SQLReader(sql);
            while (dr.Read())
            {
                re += "<div style='border-bottom:1px dashed #ccc; line-height:30px;'><a title='" + dr["smm_title"].ToString() + "' target='_blank' href='news.aspx?id=" + dr["id"].ToString() + "'>" + dr["smm_title"].ToString() + "</a></div>";
            }
            return re;
        }
        public static string ShowNewsTitle(string id)
        {
            string sql = "select smm_title from smm_news where id="+id;
            ClassManageDataBase db = new ClassManageDataBase();
            SqlDataReader dr = db.SQLReader(sql);
            dr.Read();
            return dr["smm_title"].ToString();
        }
        public static string ShowNewsContent(string id)
        {
            string sql = "select smm_content from smm_news where id=" + id;
            ClassManageDataBase db = new ClassManageDataBase();
            SqlDataReader dr = db.SQLReader(sql);
            dr.Read();
            return dr["smm_content"].ToString();
        }
        public static string ShowNewsTime(string id)
        {
            string sql = "select smm_time from smm_news where id=" + id;
            ClassManageDataBase db = new ClassManageDataBase();
            SqlDataReader dr = db.SQLReader(sql);
            dr.Read();
            return dr["smm_time"].ToString();
        }
    }
}