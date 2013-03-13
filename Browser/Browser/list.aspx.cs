using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Website
{
    public partial class list : System.Web.UI.Page
    {
        string renum = "";
        double sum = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = ClassMain.GetPageTitle("购物车");
            if (Request.QueryString["del"] != null)
            {
                string cookie1 = Request.Cookies["good"].Value;
                string newCookie1 = cookie1.Substring(1);
                string[] mn = newCookie1.Split(new char[] { ',' });
                ArrayList li1 = new ArrayList();
                for (int i = 0; i < mn.Length; i = i + 2)
                {
                    if (mn[i] != Request.QueryString["del"].ToString().Trim())
                    {
                        li1.Add(mn[i]);
                        li1.Add(mn[i + 1]);
                    }
                }
                string xx = "";
                for (int mm = 0; mm < li1.Count; mm++)
                {
                    xx += "," + li1[mm];
                }

                string NewCookie = xx;
                HttpCookie hc = new HttpCookie("good");
                hc.Value = NewCookie;
                Response.Cookies.Add(hc);
                Response.Redirect("list.aspx");
            }


            if (Request.QueryString["edit"] != null)
            {
                Button2.Visible = false;
                Label4.Visible = false;
                renum = TextBox1.Text;
                string sql = "select smm_name from smm_product where smm_number='" + Request.QueryString["edit"] + "'";
                ClassManageDataBase db1 = new ClassManageDataBase();
                SqlDataReader dr1 = db1.SQLReader(sql);
                dr1.Read();

                Label2.Text = "您要修改“" + dr1["smm_name"] + "”的数量：";
                TextBox1.Visible = true;
                Button1.Visible = true;
                TextBox1.Text = Request.QueryString["num"];
                TextBox1.Focus();
                db1.Close();
            }
            else
            {
                Label2.Visible = false;
                TextBox1.Visible = false;
                Button1.Visible = false;
            }



            ArrayList li = get();
            string str = "";

            str += "<table class='table table-bordered table-hover'>";
            str += "<tr><th>商品</th><th>单价</th><th>数量</th><th>小结(元)</th><th>操作</th></tr>";
            for (int k = 0; k < li.Count; k=k+2)
            {
                string sql = "select smm_name,smm_price,smm_danwei from smm_product where smm_number='" + li[k].ToString() + "'";
                ClassManageDataBase db = new ClassManageDataBase();
                SqlDataReader dr = db.SQLReader(sql);
                try
                {
                    dr.Read();
                    double sum1 = Convert.ToDouble(li[k + 1]) * Convert.ToDouble(dr["smm_price"]);
                    sum += sum1;
                    str += "<tr><td><a target='_blank' title='" + dr["smm_name"].ToString() + "' href='showgood.aspx?id=" + li[k] + "'>" + dr["smm_name"].ToString() + "</a></td>";
                    str += "<td>" + Convert.ToDouble(dr["smm_price"]).ToString("0.00") + "元/" + dr["smm_danwei"] + "</td>";
                    str += "<td>" + li[k + 1] + "</td>";
                    str += "<td>" + sum1 + "</td>";
                    str += "<td><a href='?del=" + li[k] + "&num=" + li[k + 1] + "'>删除</a>&nbsp;&nbsp;<a href='?edit=" + li[k] + "&num=" + li[k + 1] + "'>修改数量</a></td></tr>";
                }
                catch { }
                db.Close();
            }
            str += "</table>";
            Label1.Text = str;
            Label4.Text = "总价：" + Convert.ToDouble(sum).ToString("0.00") +"元";
            //Label1.Text += Request.Cookies["good"].Value;
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
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["edit"] != null)
            {
                if (Convert.ToInt32(renum) > 0)
                {
                    string OldCookie = "";
                    try
                    {
                        OldCookie = Request.Cookies["good"].Value;
                    }
                    catch { OldCookie = ""; }
                    int newnum = Convert.ToInt32(renum) - Convert.ToInt32(Request.QueryString["num"]);

                    string NewCookie = "," + Request.QueryString["edit"].ToString() + "," + newnum.ToString() + OldCookie;
                    HttpCookie hc = new HttpCookie("good");
                    hc.Value = NewCookie;
                    Response.Cookies.Add(hc);
                    Button2.Visible = true;
                    Label4.Visible = true;
                    Response.Redirect("list.aspx");
                }
                else
                {
                    Label3.Text = ClassMain.ShowAlert("输入错误！");
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            ArrayList li = get();
            bool OK = true;
            string str = "";
            for (int i = 0; i < li.Count; i = i + 2)
            {

                string sql = "select smm_sum,smm_name,smm_danwei from smm_product where smm_number='" + li[i] + "'";
                ClassManageDataBase db = new ClassManageDataBase();
                SqlDataReader dr = db.SQLReader(sql);
                dr.Read();
                if (Convert.ToInt32(dr["smm_sum"]) < Convert.ToInt32(li[i + 1]))
                {


                    OK = false;
                    str += dr["smm_name"].ToString() + "还剩" + dr["smm_sum"].ToString() + dr["smm_danwei"].ToString() + "，";
                }
                db.Close();

            }
            if (OK == true)
            {
                if (Session["login"] != "1")
                {
                    Response.Redirect("login.aspx");
                }
                else
                {
                    //开始写入数据库
                    string sql = String.Format("insert into smm_sell (smm_customer,smm_price,smm_time,smm_tag) values('{0}','{1}','{2}',1) select @@identity", Session["username"], sum, DateTime.Now);
                    ClassManageDataBase db = new ClassManageDataBase();
                    SqlDataReader dr = db.SQLReader(sql);
                    dr.Read();
                    dr[0].ToString();

                    for (int j = 0; j < li.Count; j = j + 2)
                    {
                        string sql2 = String.Format("select smm_price,smm_sum from smm_product where smm_number='{0}'", li[j].ToString());
                        ClassManageDataBase db2 = new ClassManageDataBase();
                        SqlDataReader dr2 = db2.SQLReader(sql2);
                        dr2.Read();

                        int s = Convert.ToInt32(dr2[1]);
                        s = s - Convert.ToInt32(li[j + 1]);
                        string sql5 = String.Format("update smm_product set smm_sum={0} where smm_number='{1}'", s, li[j]);
                        ClassManageDataBase db5 = new ClassManageDataBase();
                        db5.SQLExecute(sql5);

                        string sql1 = String.Format("insert into smm_order(smm_product,smm_sum,smm_price,smm_sell) values('{0}','{1}','{2}',{3})", li[j].ToString(), li[j + 1].ToString(), dr2[0].ToString(), dr[0].ToString());
                        ClassManageDataBase db1 = new ClassManageDataBase();
                        db1.SQLExecute(sql1);
                        db2.Close();
                    }
                    if (Session["username"] != "")
                    {
                        string sql4 = String.Format("select smm_jifen from smm_customer where smm_cardnumber='{0}'", Session["username"]);
                        ClassManageDataBase db4 = new ClassManageDataBase();
                        SqlDataReader dr4 = db4.SQLReader(sql4);
                        dr4.Read();
                        int m = Convert.ToInt32(dr4[0]);
                        int n = m + Convert.ToInt32(sum);

                        string sql3 = String.Format("update smm_customer set smm_jifen={0} where smm_cardnumber='{1}'", n, Session["username"]);
                        ClassManageDataBase db3 = new ClassManageDataBase();
                        db3.SQLExecute(sql3);
                        db4.Close();
                    }
                    db.Close();
                    string NewCookie = "";
                    HttpCookie hc = new HttpCookie("good");
                    hc.Value = NewCookie;
                    Response.Cookies.Add(hc);
                    Response.Redirect("orderlist.aspx");
                }
            }
            else
            {
                str += "请您修改购买数量，谢谢！";
                Label3.Text = ClassMain.ShowAlert(str);
            }
        }
    }
}