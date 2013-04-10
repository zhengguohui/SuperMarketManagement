using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Website
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = ClassMain.GetPageTitle("首页");
            Label1.Text = ClassMain.ShowGoods("select top 9 smm_number,smm_picture,smm_name,smm_price,smm_danwei from smm_product order by id desc");
        }
    }
}