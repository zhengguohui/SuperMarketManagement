using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Website
{
    public partial class search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int sum = 12;
            string s = Request.QueryString["s"];
            string p = Request.QueryString["p"];

            

            ClassManageDataBase db = new ClassManageDataBase();
            string sql1 = "select smm_number from smm_product where smm_name like '%" + s + "%' or smm_number like '%" + s + "%' or smm_about like '%" + s + "%' or smm_tag like '%" + s + "%'";
            int m = db.SQLNumber(sql1);
            if (m > 0)
            {
                string sql = "select top " + sum.ToString() + " smm_number,smm_picture,smm_name,smm_price,smm_danwei from smm_product where (smm_name like '%" + s + "%' or smm_number like '%" + s + "%' or smm_about like '%" + s + "%' or smm_tag like '%" + s + "%')  and  smm_number not in (select top " + (Convert.ToInt32(p) * sum).ToString() + " smm_number from smm_product)";
               
                    LabelAlert.Text = ClassMain.ShowInformation("搜索商品：" + s);
         
                LabelSearch.Text = ClassMain.ShowGoods(sql);
                this.Title = ClassMain.GetPageTitle("搜索商品：" + s);
            }
            else
            {
                LabelAlert.Text = ClassMain.ShowAlert("您搜索的商品不存在！");
                LabelSearch.Text ="";
                this.Title =ClassMain.GetPageTitle( "您搜索的商品不存在！");
            }
           
           
            int page=m / sum;
            if (m % sum > 0)
            {
                page++;
            }

            string re="";
            re+="<div class='pagination' style='clear:both;'><ul>";
            if(Convert.ToInt32(p)>0)
            {
                re += "<li><a href='?p=" + (Convert.ToInt32(p) -1).ToString()+ "&s="+s+"'>上一页</a></li>";
            }
            for(int i=0;i<page;i++)
            {
                re += "<li><a href='?p=" + i.ToString() + "&s=" + s + "'>" + (i + 1).ToString() + "</a></li>";
            }

             if(Convert.ToInt32(p)<(page-1))
            {
                re += "<li><a href='?p=" + (Convert.ToInt32(p) + 1).ToString() + "&s=" + s + "'>下一页</a></li>";
             }
            re+="</ul></div>";
   
    
   
    
            LabelBanner.Text = re;
        }
    }
}