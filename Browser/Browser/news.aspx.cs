using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Website
{
    public partial class news : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            Label1.Text = ClassMain.ShowNewsTitle(Request.QueryString["id"]);
            Label2.Text = ClassMain.ShowNewsContent(Request.QueryString["id"]);
            Label3.Text = ClassMain.ShowNewsTime(Request.QueryString["id"]);
            this.Title = ClassMain.GetPageTitle(Label1.Text);
        }
    }
}