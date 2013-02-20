using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Collections;

namespace SuperMarketManagement
{
    public partial class FormMain : Form
    {
        private string username;
        private string buynumber;
        public FormMain(string username1)
        {
            InitializeComponent();
            username = username1;
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            //this.MaximumSize = this.Size;
            //this.MinimumSize = this.Size;

            timer1.Interval = 1000;
            timer1.Enabled = true;
            timer1_Tick(sender, e);
            string sql = String.Format("select smm_nickname,smm_tag from smm_user where smm_username='{0}'", username);
            ClassManageDataBase db = new ClassManageDataBase();
            SqlDataReader dr = db.SQLReader(sql);
            dr.Read();
            if (dr[0].ToString().Trim() == "")
            {
                MenuAccount.Text = "我的账户(&M)";
            }
            else
            {
                MenuAccount.Text = dr[0].ToString() + "(&M)";
            }
            if (dr[1].ToString().Trim() == "1")
            {
                ClassSettings cs = new ClassSettings();
                textBoxSuperMarketName.Text = cs.GetSettings("supermarketname");
                textBoxCustomerNewPassword.Text = cs.GetSettings("customernewpassword");

                textBoxFTPAddress.Text = cs.GetSettings("ftpaddress");
                textBoxFTPUsername.Text = cs.GetSettings("ftpusername");
                textBoxFTPPassword.Text = cs.GetSettings("ftppassword");
                SetUser();
                SetCustomer();
                SetProduct();
                SetBuy();
                SetSell();
                SetOrder();
            }
            else if (dr[1].ToString().Trim() == "2")
            {
                tabControl1.TabPages.Remove(tabProduct);
                tabControl1.TabPages.Remove(tabBuy);
                tabControl1.TabPages.Remove(tabSell);
                tabControl1.TabPages.Remove(tabOrder);
                tabControl1.TabPages.Remove(tabUser);
                tabControl1.TabPages.Remove(tabSystem);
                SetCustomer();
            }
            else if (dr[1].ToString().Trim() == "3")
            {
                tabControl1.TabPages.Remove(tabBuy);
                tabControl1.TabPages.Remove(tabSell);
                tabControl1.TabPages.Remove(tabOrder);
                tabControl1.TabPages.Remove(tabCustom);
                tabControl1.TabPages.Remove(tabUser);
                tabControl1.TabPages.Remove(tabSystem);
                SetProduct();
            }
            else if (dr[1].ToString().Trim() == "4")
            {
                tabControl1.TabPages.Remove(tabProduct);
                tabControl1.TabPages.Remove(tabSell);
                tabControl1.TabPages.Remove(tabOrder);
                tabControl1.TabPages.Remove(tabCustom);
                tabControl1.TabPages.Remove(tabUser);
                tabControl1.TabPages.Remove(tabSystem);
                SetBuy();
            }
            else if (dr[1].ToString().Trim() == "5")
            {
                tabControl1.TabPages.Remove(tabProduct);
                tabControl1.TabPages.Remove(tabBuy);
                tabControl1.TabPages.Remove(tabOrder);
                tabControl1.TabPages.Remove(tabCustom);
                tabControl1.TabPages.Remove(tabUser);
                tabControl1.TabPages.Remove(tabSystem);
                SetSell();

            }
            else if (dr[1].ToString().Trim() == "6")
            {
                tabControl1.TabPages.Remove(tabProduct);
                tabControl1.TabPages.Remove(tabBuy);
                tabControl1.TabPages.Remove(tabSell);
                tabControl1.TabPages.Remove(tabCustom);
                tabControl1.TabPages.Remove(tabUser);
                tabControl1.TabPages.Remove(tabSystem);
                SetOrder();
            }
            else
            {
                tabControl1.TabPages.Remove(tabProduct);
                tabControl1.TabPages.Remove(tabBuy);
                tabControl1.TabPages.Remove(tabSell);
                tabControl1.TabPages.Remove(tabOrder);
                tabControl1.TabPages.Remove(tabCustom);
                tabControl1.TabPages.Remove(tabUser);
                tabControl1.TabPages.Remove(tabSystem);
            }
            db.Close();

        }
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void MenuQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void MenuAbout_Click(object sender, EventArgs e)
        {
            new FormAbout().ShowDialog();
        }
        private void MenuAccountInfomation_Click(object sender, EventArgs e)
        {
            new FormAccountInfomation(username).ShowDialog();
        }
        private void MenuChangePassword_Click(object sender, EventArgs e)
        {
            new FormChangePassword(username).ShowDialog();
        }
        private void MenuReLogin_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确定要切换用户吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                new FormLogin().Show();
                this.Dispose();
            }
        }
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("您确定要退出吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            MenuTime.Text = DateTime.Now.Year.ToString() + "年" + DateTime.Now.Month.ToString() + "月" + DateTime.Now.Day.ToString() + "日 " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
        }

        private void buttonSetSuperMarketName_Click(object sender, EventArgs e)
        {
            ClassSettings cs = new ClassSettings();
            cs.SetSettings("supermarketname", textBoxSuperMarketName.Text);
            MessageBox.Show("设置成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonSetDataBase_Click_1(object sender, EventArgs e)
        {
            FormSetDataBase a = new FormSetDataBase();
            a.ShowDialog();
        }

        private void SetUser()
        {
            comboBox1.SelectedIndex = 0;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.View = View.Details;
            listView1.Scrollable = true;
            listView1.MultiSelect = false;
            listView1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView1.Columns.Add("用户名", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("昵称", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("角色", 100, HorizontalAlignment.Left);
            RefreshUser();
        }
        private void RefreshUser()
        {

            string sql;
            if (comboBox1.SelectedIndex == 0)
            {
                sql = String.Format("select smm_username,smm_nickname,smm_tag from smm_user order by id desc");
            }
            else
            {
                sql = String.Format("select smm_username,smm_nickname,smm_tag from smm_user where smm_tag='{0}' order by id desc", comboBox1.SelectedIndex);
            }
            ClassManageDataBase db = new ClassManageDataBase();
            SqlDataReader dr = db.SQLReader(sql);
            listView1.Items.Clear();
            while (dr.Read())
            {
                string js = "";
                if (dr[2].ToString() == "1")
                {
                    js = "超级管理员";
                }
                else if (dr[2].ToString() == "2")
                {
                    js = "会员管理员";
                }
                else if (dr[2].ToString() == "3")
                {
                    js = "商品信息管理员";
                }
                else if (dr[2].ToString() == "4")
                {
                    js = "入库员";
                }
                else if (dr[2].ToString() == "5")
                {
                    js = "销售员";
                }
                else if (dr[2].ToString() == "6")
                {
                    js = "订单管理员";
                }
                else
                {
                    js = "未知";
                }
                ListViewItem li = new ListViewItem();
                li.SubItems.Clear();
                li.SubItems[0].Text = dr[0].ToString();
                li.SubItems.Add(dr[1].ToString());
                li.SubItems.Add(js);
                li.Tag = dr[0].ToString();
                listView1.Items.Add(li);


            }

            try
            {
                listView1.Items[0].Selected = true;
            }
            catch { }


        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshUser();

        }

        private void buttonDeleteUser_Click(object sender, EventArgs e)
        {
            string user = SeleteUser();
            if (user != "")
            {
                if (MessageBox.Show("您确定要删除“" + user + "”吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (user == username)
                    {
                        MessageBox.Show("您不能删除自己！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        string sql = String.Format("delete from smm_user where smm_username='{0}'", user);
                        ClassManageDataBase db = new ClassManageDataBase();
                        db.SQLExecute(sql);
                        RefreshUser();
                        MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        private string SeleteUser()
        {
            string user = "";
            try
            {
                user = listView1.SelectedItems[0].Tag.ToString();
            }
            catch
            {
                MessageBox.Show("请选择用户！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return user;
        }

        private void buttonUpdateUser_Click(object sender, EventArgs e)
        {
            string user = SeleteUser();
            if (user != "")
            {
                new FormSetUser(1, user).ShowDialog();
                RefreshUser();
            }
        }

        private void buttonAddUser_Click(object sender, EventArgs e)
        {

            new FormSetUser(0, "").ShowDialog();
            RefreshUser();

        }


        private void SetCustomer()
        {

            listView2.GridLines = true;
            listView2.FullRowSelect = true;
            listView2.View = View.Details;
            listView2.Scrollable = true;
            listView2.MultiSelect = false;
            listView2.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView2.Columns.Add("会员卡号", 75, HorizontalAlignment.Left);
            listView2.Columns.Add("姓名", 60, HorizontalAlignment.Left);
            listView2.Columns.Add("性别", 40, HorizontalAlignment.Left);
            listView2.Columns.Add("积分", 100, HorizontalAlignment.Left);
            listView2.Columns.Add("手机", 80, HorizontalAlignment.Left);
            listView2.Columns.Add("电话", 85, HorizontalAlignment.Left);
            listView2.Columns.Add("身份证号", 120, HorizontalAlignment.Left);
            listView2.Columns.Add("住址", 200, HorizontalAlignment.Left);
            listView2.Columns.Add("备注", 200, HorizontalAlignment.Left);



            RefreshCustomer();

        }

        private string SeleteCustomer()
        {
            string user = "";
            try
            {
                user = listView2.SelectedItems[0].Tag.ToString();
            }
            catch
            {
                MessageBox.Show("请选择会员！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return user;
        }
        private void RefreshCustomer()
        {

            string sql = "";
            if (textBoxCutomerSearch.Text.Trim() == "")
            {
                sql = String.Format("select smm_cardnumber,smm_name,smm_sex,smm_jifen,smm_cellphone,smm_telephone,smm_idcard,smm_address,id,smm_other from smm_customer order by id desc");
            }
            else
            {
                sql = String.Format("select smm_cardnumber,smm_name,smm_sex,smm_jifen,smm_cellphone,smm_telephone,smm_idcard,smm_address,id,smm_other from smm_customer where smm_cardnumber like '%{0}%' or smm_idcard like '%{1}%' or smm_address like '%{2}%' or smm_telephone like '%{3}%' or smm_cellphone like '%{4}%' or smm_name like '%{5}%' or smm_other like '%{6}%'order by id desc", textBoxCutomerSearch.Text, textBoxCutomerSearch.Text, textBoxCutomerSearch.Text, textBoxCutomerSearch.Text, textBoxCutomerSearch.Text, textBoxCutomerSearch.Text, textBoxCutomerSearch.Text);
            }
            ClassManageDataBase db = new ClassManageDataBase();
            SqlDataReader dr = db.SQLReader(sql);
            listView2.Items.Clear();
            while (dr.Read())
            {
                string js = "";
                if (dr[2].ToString() == "1")
                {
                    js = "男";
                }
                else if (dr[2].ToString() == "0")
                {
                    js = "女";
                }
                else
                {
                    js = "未知";
                }
                ListViewItem li = new ListViewItem();
                li.SubItems.Clear();
                li.SubItems[0].Text = dr[0].ToString();
                li.SubItems.Add(dr[1].ToString());
                li.SubItems.Add(js);
                li.SubItems.Add(dr[3].ToString());
                li.SubItems.Add(dr[4].ToString());
                li.SubItems.Add(dr[5].ToString());
                li.SubItems.Add(dr[6].ToString());
                li.SubItems.Add(dr[7].ToString());
                li.SubItems.Add(dr[9].ToString());
                li.Tag = dr[8].ToString();
                listView2.Items.Add(li);

            }
            try
            {
                listView2.Items[0].Selected = true;
            }
            catch { }




        }

        private void tabUser_Click(object sender, EventArgs e)
        {

        }

        private void buttonAddCustomer_Click(object sender, EventArgs e)
        {
            new FormSetCustomer(0, "").ShowDialog();
            RefreshCustomer();
        }

        private void buttonUpdateCustomer_Click(object sender, EventArgs e)
        {

            string user = SeleteCustomer();
            if (user != "")
            {
                new FormSetCustomer(1, user).ShowDialog();
                RefreshCustomer();
            }

        }

        private void buttonDeleteCustomer_Click(object sender, EventArgs e)
        {
            string user = SeleteCustomer();
            if (user != "")
            {
                if (MessageBox.Show("您确定要删除“" + user + "”吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string sql = String.Format("delete from smm_customer where id={0}", user);
                    ClassManageDataBase db = new ClassManageDataBase();
                    db.SQLExecute(sql);
                    RefreshCustomer();
                    MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        private void buttonJifenXiaoFei_Click(object sender, EventArgs e)
        {
            string user = SeleteCustomer();
            if (user != "")
            {
                new FormCustomerJifen(user).ShowDialog();
                RefreshCustomer();
            }
        }

        private void buttonChangeCard_Click(object sender, EventArgs e)
        {
            string user = SeleteCustomer();
            if (user != "")
            {
                new FormCustomerChangeCard(user).ShowDialog();
                RefreshCustomer();
            }
        }

        private void buttonAllCustomer_Click(object sender, EventArgs e)
        {
            textBoxCutomerSearch.Text = "";
        }

        private void textBoxCutomerSearch_TextChanged(object sender, EventArgs e)
        {
            RefreshCustomer();
        }

        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            buttonUpdateCustomer_Click(sender, e);
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            buttonUpdateUser_Click(sender, e);
        }

        private void listView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                buttonDeleteCustomer_Click(sender, e);
            }
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                buttonDeleteUser_Click(sender, e);
            }

        }

        private void buttonCustomerNewPassword_Click(object sender, EventArgs e)
        {
            ClassSettings cs = new ClassSettings();
            cs.SetSettings("customernewpassword", textBoxCustomerNewPassword.Text);
            MessageBox.Show("设置成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        private void SetProduct()
        {

            listView3.GridLines = true;
            listView3.FullRowSelect = true;
            listView3.View = View.Details;
            listView3.Scrollable = true;
            listView3.MultiSelect = false;
            listView3.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView3.Columns.Add("商品编号", 90, HorizontalAlignment.Left);
            listView3.Columns.Add("商品名称", 75, HorizontalAlignment.Left);
            listView3.Columns.Add("商品单位", 60, HorizontalAlignment.Left);
            listView3.Columns.Add("商品单价", 60, HorizontalAlignment.Right);
            listView3.Columns.Add("商品数量", 60, HorizontalAlignment.Right);
            listView3.Columns.Add("商品标签", 200, HorizontalAlignment.Left);
            listView3.Columns.Add("商品介绍", 200, HorizontalAlignment.Left);




            RefreshProduct();

        }
        private void RefreshProduct()
        {

            string sql = "";
            if (textBoxProductSearch.Text.Trim() == "")
            {
                sql = String.Format("select smm_number,smm_name,smm_danwei,smm_tag,smm_about,smm_price,smm_sum from smm_product order by id desc");
            }
            else
            {
                sql = String.Format("select smm_number,smm_name,smm_danwei,smm_tag,smm_about,smm_price,smm_sum from smm_product where smm_number like '%{0}%' or smm_name like '%{1}%' or smm_danwei like '%{2}%' or smm_about like '%{3}%' or smm_tag like '%{4}%' order by id desc", textBoxProductSearch.Text, textBoxProductSearch.Text, textBoxProductSearch.Text, textBoxProductSearch.Text, textBoxProductSearch.Text);
            }
            ClassManageDataBase db = new ClassManageDataBase();
            SqlDataReader dr = db.SQLReader(sql);
            listView3.Items.Clear();
            while (dr.Read())
            {

                ListViewItem li = new ListViewItem();
                li.SubItems.Clear();
                li.SubItems[0].Text = dr[0].ToString();
                li.SubItems.Add(dr[1].ToString());
                li.SubItems.Add(dr[2].ToString());
                li.SubItems.Add(dr[5].ToString());
                li.SubItems.Add(dr[6].ToString());
                li.SubItems.Add(dr[3].ToString());
                li.SubItems.Add(dr[4].ToString());

                li.Tag = dr[0].ToString();
                listView3.Items.Add(li);

            }


            try
            {
                listView3.Items[0].Selected = true;
            }
            catch { }

        }

        private void textBoxProductSearch_TextChanged(object sender, EventArgs e)
        {
            RefreshProduct();
        }

        private void buttonAllProduct_Click(object sender, EventArgs e)
        {
            textBoxProductSearch.Text = "";
        }

        private void buttonAddProduct_Click(object sender, EventArgs e)
        {
            new FormSetProduct(0, "").ShowDialog();
            RefreshProduct();
        }

        private void buttonUpdateProduct_Click(object sender, EventArgs e)
        {
            string user = SeleteProduct();
            if (user != "")
            {
                new FormSetProduct(1, user).ShowDialog();
                RefreshProduct();
            }
        }

        private void buttonDeleteProduct_Click(object sender, EventArgs e)
        {
            string user = SeleteProduct();
            if (user != "")
            {
                if (MessageBox.Show("您确定要删除“" + user + "”吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string sql2 = String.Format("select smm_picture from smm_product where smm_number='{0}'", user);
                    ClassManageDataBase db2 = new ClassManageDataBase();
                    SqlDataReader dr = db2.SQLReader(sql2);
                    dr.Read();
                    string deletefile = user + "." + dr[0].ToString();
                    ClassManageFTP cmt1 = new ClassManageFTP();
                    cmt1.Delete(deletefile);

                    string sql = String.Format("delete from smm_product where smm_number='{0}'", user);
                    ClassManageDataBase db = new ClassManageDataBase();
                    db.SQLExecute(sql);



                    RefreshProduct();
                    MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        private void listView3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                buttonDeleteProduct_Click(sender, e);
            }
        }

        private void listView3_DoubleClick(object sender, EventArgs e)
        {
            buttonUpdateProduct_Click(sender, e);
        }
        private string SeleteProduct()
        {
            string user = "";
            try
            {
                user = listView3.SelectedItems[0].Tag.ToString();
            }
            catch
            {
                MessageBox.Show("请选择商品！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return user;
        }

        private void tabSystem_Click(object sender, EventArgs e)
        {

        }

        private void buttonSetFTP_Click(object sender, EventArgs e)
        {
            ClassManageFTP cmt = new ClassManageFTP(textBoxFTPAddress.Text, textBoxFTPUsername.Text, textBoxFTPPassword.Text);

            if (!cmt.Upload(Application.ExecutablePath, "test.zip"))
            {
                MessageBox.Show("FTP无法联通，请检查参数是否正确！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxFTPAddress.SelectAll();
                textBoxFTPAddress.Focus();
            }
            else
            {
                cmt.Delete("test.zip");
                ClassSettings cs = new ClassSettings();
                cs.SetSettings("ftpaddress", textBoxFTPAddress.Text);
                cs.SetSettings("ftpusername", textBoxFTPUsername.Text);
                cs.SetSettings("ftppassword", textBoxFTPPassword.Text);
                MessageBox.Show("设置成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private void buttonBuySearch_Click(object sender, EventArgs e)
        {
            string sql = String.Format("select smm_name,smm_danwei,smm_tag,smm_about,smm_price,smm_picture from smm_product where smm_number='{0}'", textBoxProductNumber.Text.Trim());
            ClassManageDataBase db = new ClassManageDataBase();
            SqlDataReader dr = db.SQLReader(sql);
            if (dr.Read())
            {
                labelProductName.Text = "商品名称：" + dr[0].ToString();
                labelProductDanwei.Text = "商品单位：" + dr[1].ToString();
                labelProductTag.Text = "商品标签：" + dr[2].ToString();
                labelProductAbout.Text = "商品简介：" + dr[3].ToString();
                labelProductPrice.Text = "商品价格：" + dr[4].ToString();
                string oldkzm = dr[5].ToString();
                buynumber = textBoxProductNumber.Text.Trim();
                if (oldkzm != "")
                {
                    ClassManageFTP cmt = new ClassManageFTP();
                    string now = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                    string kzm = dr[5].ToString();
                    string OldFile = textBoxProductNumber.Text.Trim() + "." + kzm;
                    string NewFile = System.Windows.Forms.Application.StartupPath + "\\" + "Temp" + "\\" + now + textBoxProductNumber.Text.Trim() + "." + kzm;
                    try
                    {
                        if (!Directory.Exists("Temp"))
                        {
                            Directory.CreateDirectory("Temp");
                        }
                        cmt.Download(OldFile, NewFile);
                        pictureBoxProduct.Image = Image.FromFile(NewFile);
                        pictureBoxProduct.Visible = true;
                    }
                    catch
                    {
                        pictureBoxProduct.Visible = false;
                    }
                }
                else
                {
                    pictureBoxProduct.Visible = false;
                }





                labelProductName.Visible = true;
                labelProductDanwei.Visible = true;

                labelProductPrice.Visible = true;
                labelProductTag.Visible = true;
                labelProductAbout.Visible = true;



                textBoxProductSum.Enabled = true;
                textBoxProductPrice.Enabled = true;
                buttonBuyNow.Enabled = true;
                textBoxProductSum.SelectAll();
                textBoxProductSum.Focus();
            }
            else
            {
                pictureBoxProduct.Visible = false;
                labelProductName.Visible = false;
                labelProductDanwei.Visible = false;

                labelProductPrice.Visible = false;
                labelProductTag.Visible = false;
                labelProductAbout.Visible = false;
                textBoxProductSum.Enabled = false;
                textBoxProductPrice.Enabled = false;
                buttonBuyNow.Enabled = false;
                MessageBox.Show("该商品不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxProductNumber.SelectAll();
                textBoxProductNumber.Focus();
            }
        }

        private void textBoxProductNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonBuySearch_Click(sender, e);
            }
        }

        private void buttonBuyNow_Click(object sender, EventArgs e)
        {
            int productsum = 0;
            float productprice = 0;
            bool buy = true;
            if (textBoxProductSum.Text.Trim() == "")
            {
                MessageBox.Show("请输入入库数量！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxProductSum.SelectAll();
                textBoxProductSum.Focus();
                buy = false;
            }
            else if (textBoxProductPrice.Text.Trim() == "")
            {
                MessageBox.Show("请输入入库总价！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxProductPrice.SelectAll();
                textBoxProductPrice.Focus();
                buy = false;
            }
            else
            {
                try
                {
                    productsum = Convert.ToInt32(textBoxProductSum.Text.Trim());
                }
                catch
                {
                    MessageBox.Show("入库数量不正确！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxProductSum.SelectAll();
                    textBoxProductSum.Focus();
                    buy = false;
                }
                if (buy == true)
                {
                    try
                    {
                        productprice = Convert.ToInt32(textBoxProductPrice.Text.Trim());
                    }
                    catch
                    {
                        MessageBox.Show("入库总价不正确！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBoxProductPrice.SelectAll();
                        textBoxProductPrice.Focus();
                        buy = false;
                    }
                }

            }
            if (buy == true)
            {
                string sql1 = String.Format("select smm_sum from smm_product where smm_number='{0}'", buynumber);
                ClassManageDataBase db1 = new ClassManageDataBase();
                SqlDataReader dr = db1.SQLReader(sql1);
                dr.Read();
                int a = Convert.ToInt32(dr[0]);
                int b = a + productsum;

                string sql = String.Format("update smm_product set smm_sum={0} where smm_number='{1}'", b, buynumber);
                ClassManageDataBase db = new ClassManageDataBase();
                db.SQLExecute(sql);

                string sql2 = String.Format("insert into smm_buy (smm_number,smm_sum,smm_price,smm_time) values ('{0}',{1},{2},'{3}')", buynumber, productsum, productprice, DateTime.Now);
                ClassManageDataBase db2 = new ClassManageDataBase();
                db2.SQLExecute(sql2);

                RefreshBuy();
                MessageBox.Show("入库成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pictureBoxProduct.Visible = false;
                labelProductName.Visible = false;
                labelProductDanwei.Visible = false;

                labelProductPrice.Visible = false;
                labelProductTag.Visible = false;
                labelProductAbout.Visible = false;
                textBoxProductSum.Enabled = false;
                textBoxProductPrice.Enabled = false;
                buttonBuyNow.Enabled = false;
                textBoxProductSum.Text = "";
                textBoxProductPrice.Text = "";
                textBoxProductNumber.Text = "";
                textBoxProductNumber.SelectAll();
                textBoxProductNumber.Focus();
            }

        }

        private void textBoxProductSum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonBuyNow_Click(sender, e);
            }
        }

        private void textBoxProductPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonBuyNow_Click(sender, e);
            }
        }

        private void SetBuy()
        {

            listView4.GridLines = true;
            listView4.FullRowSelect = true;
            listView4.View = View.Details;
            listView4.Scrollable = true;
            listView4.MultiSelect = false;
            listView4.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView4.Columns.Add("商品编号", 90, HorizontalAlignment.Left);
            listView4.Columns.Add("商品名称", 75, HorizontalAlignment.Left);
            listView4.Columns.Add("入库数量", 70, HorizontalAlignment.Right);
            listView4.Columns.Add("入库总价", 70, HorizontalAlignment.Right);
            listView4.Columns.Add("入库时间", 160, HorizontalAlignment.Left);





            RefreshBuy();

        }
        private void RefreshBuy()
        {

            string sql = String.Format("select id,smm_number,smm_sum,smm_price,smm_time from smm_buy order by id desc");

            ClassManageDataBase db = new ClassManageDataBase();
            SqlDataReader dr = db.SQLReader(sql);
            listView4.Items.Clear();
            while (dr.Read())
            {
                string name = "无或已删除";

                try
                {
                    string sql1 = String.Format("select smm_name from smm_product where smm_number='{0}'", dr[1].ToString());
                    ClassManageDataBase db1 = new ClassManageDataBase();
                    SqlDataReader dr1 = db1.SQLReader(sql1);
                    dr1.Read();
                    name = dr1[0].ToString();
                }
                catch { }

                ListViewItem li = new ListViewItem();
                li.SubItems.Clear();
                li.SubItems[0].Text = dr[1].ToString();
                li.SubItems.Add(name);
                li.SubItems.Add(dr[2].ToString());
                li.SubItems.Add(dr[3].ToString());
                li.SubItems.Add(dr[4].ToString());

                li.Tag = dr[0].ToString();
                listView4.Items.Add(li);

            }

            try
            {
                listView4.Items[0].Selected = true;
            }
            catch { }


        }
        private string SeleteBuy()
        {
            string user = "";
            try
            {
                user = listView4.SelectedItems[0].Tag.ToString();
            }
            catch
            {
                MessageBox.Show("请选择入库记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return user;
        }

        private void buttonDeleteBuy_Click(object sender, EventArgs e)
        {
            string user = SeleteBuy();
            if (user != "")
            {
                if (MessageBox.Show("您确定要删除该记录吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        string sql2 = String.Format("select smm_sum,smm_number from smm_buy where id={0}", user);
                        ClassManageDataBase db2 = new ClassManageDataBase();
                        SqlDataReader dr2 = db2.SQLReader(sql2);
                        dr2.Read();
                        int b = Convert.ToInt32(dr2[0]);

                        string sql1 = String.Format("select smm_sum from smm_product where smm_number='{0}'", dr2[1].ToString());
                        ClassManageDataBase db1 = new ClassManageDataBase();
                        SqlDataReader dr = db1.SQLReader(sql1);
                        dr.Read();
                        int a = Convert.ToInt32(dr[0]);

                        int c = a - b;

                        string sql3 = String.Format("update smm_product set smm_sum={0} where smm_number='{1}'", c, dr2[1].ToString());
                        ClassManageDataBase db3 = new ClassManageDataBase();
                        db3.SQLExecute(sql3);


                    }
                    catch { }
                    finally
                    {
                        string sql = String.Format("delete from smm_buy where id={0}", user);
                        ClassManageDataBase db = new ClassManageDataBase();
                        db.SQLExecute(sql);
                    }


                    RefreshBuy();
                    MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        private void buttonRefreshProduct_Click(object sender, EventArgs e)
        {
            RefreshProduct();
        }

        private void listView4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                buttonDeleteBuy_Click(sender, e);
            }
        }
        string customercardnumber = "";
        private void textBoxCardNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBoxCardNumber.Text.Trim().Length == 0)
                {
                    buttonSellOK_Click(sender, e);
                }
                else if (textBoxCardNumber.Text.Trim().Length == 10)
                {
                    string sql = String.Format("select smm_cardnumber from smm_customer where smm_cardnumber='{0}'", textBoxCardNumber.Text.Trim());
                    ClassManageDataBase db = new ClassManageDataBase();
                    int a = db.SQLNumber(sql);
                    if (a > 0)
                    {
                        customercardnumber = textBoxCardNumber.Text.Trim();
                        RefreshSell();
                    }
                    else
                    {
                        customercardnumber = "";
                        MessageBox.Show("未找到该会员！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                else if (textBoxCardNumber.Text.Trim().Length == 13)
                {
                    string sql = String.Format("select smm_number from smm_product where smm_number='{0}'", textBoxCardNumber.Text.Trim());
                    ClassManageDataBase db = new ClassManageDataBase();
                    int a = db.SQLNumber(sql);
                    if (a > 0)
                    {
                        bool s = true;
                        for (int i = 0; i < Sell.Count; i = i + 2)
                        {
                            if (Sell[i].ToString() == textBoxCardNumber.Text.Trim())
                            {
                                Sell[i + 1] = (Convert.ToInt32(Sell[i + 1]) + 1).ToString();
                                s = false;
                            }
                        }

                        if (s)
                        {
                            Sell.Add(textBoxCardNumber.Text.Trim());
                            Sell.Add("1");
                        }
                        RefreshSell();
                    }
                    else
                    {

                        MessageBox.Show("未找到该商品！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                else
                {
                    MessageBox.Show("输入错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                textBoxCardNumber.Text = "";
                textBoxCardNumber.SelectAll();
                textBoxCardNumber.Focus();
            }
        }
        private void SetSell()
        {

            listView5.GridLines = true;
            listView5.FullRowSelect = true;
            listView5.View = View.Details;
            listView5.Scrollable = true;
            listView5.MultiSelect = false;
            listView5.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView5.Columns.Add("商品编号", 90, HorizontalAlignment.Left);
            listView5.Columns.Add("商品名称", 75, HorizontalAlignment.Left);
            listView5.Columns.Add("商品单价", 70, HorizontalAlignment.Right);
            listView5.Columns.Add("商品数量", 70, HorizontalAlignment.Right);
            listView5.Columns.Add("小结", 70, HorizontalAlignment.Right);





            RefreshSell();

        }
        double total;
        private void RefreshSell()
        {
            if (customercardnumber == "")
            {
                labelUsingCard.Text = "未使用会员卡。";
            }
            else
            {
                labelUsingCard.Text = "已使用会员卡。";
            }
            listView5.Items.Clear();
            total = 0;
            try
            {
                for (int i = 0; i < Sell.Count; i = i + 2)
                {
                    string sql = String.Format("select smm_name,smm_price from smm_product where smm_number='{0}'", Sell[i]);
                    ClassManageDataBase db = new ClassManageDataBase();
                    SqlDataReader dr = db.SQLReader(sql);
                    dr.Read();

                    ListViewItem li = new ListViewItem();
                    li.SubItems.Clear();
                    li.SubItems[0].Text = Sell[i].ToString();
                    li.SubItems.Add(dr[0].ToString());
                    li.SubItems.Add(dr[1].ToString());
                    li.SubItems.Add(Sell[i + 1].ToString());
                    li.SubItems.Add((Convert.ToDouble(dr[1]) * Convert.ToInt32(Sell[i + 1])).ToString());

                    li.Tag = Sell[i].ToString();
                    listView5.Items.Add(li);
                    total += Convert.ToDouble(dr[1]) * Convert.ToInt32(Sell[i + 1]);

                }
                labelSumPrice.Text = "总价：" + total.ToString();
            }
            catch { }







            try
            {
                listView5.Items[0].Selected = true;
            }
            catch { }

        }
        private string SeleteSell()
        {
            string user = "";
            try
            {
                user = listView5.SelectedItems[0].Tag.ToString();
            }
            catch
            {
                MessageBox.Show("请选择商品记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return user;
        }
        ArrayList Sell = new ArrayList();

        private void buttonDeleteSell_Click(object sender, EventArgs e)
        {
            string user = SeleteSell();
            if (user != "")
            {
                if (MessageBox.Show("您确定要删除该商品吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {


                    for (int i = 0; i < Sell.Count; i = i + 2)
                    {
                        if (Sell[i].ToString() == user)
                        {
                            Sell.RemoveAt(i + 1);
                            Sell.RemoveAt(i);
                        }

                    }

                    RefreshSell();
                    //MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            textBoxCardNumber.SelectAll();
            textBoxCardNumber.Focus();
        }

        private void listView5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                buttonDeleteSell_Click(sender, e);
            }
        }

        private void buttonUpdateSell_Click(object sender, EventArgs e)
        {
            string user = SeleteSell();
            if (user != "")
            {


                string sql = String.Format("select smm_name from smm_product where smm_number='{0}'", user);
                ClassManageDataBase db = new ClassManageDataBase();
                SqlDataReader dr = db.SQLReader(sql);
                dr.Read();
                string a = dr[0].ToString();
                string b = "";
                for (int i = 0; i < Sell.Count; i = i + 2)
                {
                    if (Sell[i].ToString() == user)
                    {
                        b = (Sell[i + 1].ToString());
                    }

                }
                FormSetSellNumber abc = new FormSetSellNumber(a, b);
                abc.ShowDialog();
                if (abc.number == "0")
                {
                    for (int i = 0; i < Sell.Count; i = i + 2)
                    {
                        if (Sell[i].ToString() == user)
                        {
                            Sell.RemoveAt(i + 1);
                            Sell.RemoveAt(i);
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < Sell.Count; i = i + 2)
                    {
                        if (Sell[i].ToString() == user)
                        {
                            Sell[i + 1] = abc.number;
                        }

                    }
                }

                RefreshSell();


                textBoxCardNumber.SelectAll();
                textBoxCardNumber.Focus();
            }
        }

        private void listView5_DoubleClick(object sender, EventArgs e)
        {
            buttonUpdateSell_Click(sender, e);
        }

        private void buttonSellOK_Click(object sender, EventArgs e)
        {
            FormSetOrder abc = new FormSetOrder(total);
            abc.ShowDialog();
            if (abc.OK)
            {
                //开始写入数据库
                string sql = String.Format("insert into smm_sell (smm_customer,smm_price,smm_time,smm_tag) values('{0}','{1}','{2}',0) select @@identity", customercardnumber, total, DateTime.Now);
                ClassManageDataBase db = new ClassManageDataBase();
                SqlDataReader dr = db.SQLReader(sql);
                dr.Read();
                dr[0].ToString();

                for (int j = 0; j < Sell.Count; j = j + 2)
                {
                    string sql2 = String.Format("select smm_price,smm_sum from smm_product where smm_number='{0}'", Sell[j].ToString());
                    ClassManageDataBase db2 = new ClassManageDataBase();
                    SqlDataReader dr2 = db2.SQLReader(sql2);
                    dr2.Read();

                    int s = Convert.ToInt32(dr2[1]);
                    s = s - Convert.ToInt32(Sell[j + 1]);
                    string sql5 = String.Format("update smm_product set smm_sum={0} where smm_number='{1}'", s, Sell[j]);
                    ClassManageDataBase db5 = new ClassManageDataBase();
                    db5.SQLExecute(sql5);

                    string sql1 = String.Format("insert into smm_order(smm_product,smm_sum,smm_price,smm_sell) values('{0}','{1}','{2}',{3})", Sell[j].ToString(), Sell[j + 1].ToString(), dr2[0].ToString(), dr[0].ToString());
                    ClassManageDataBase db1 = new ClassManageDataBase();
                    db1.SQLExecute(sql1);
                }
                if (customercardnumber != "")
                {
                    string sql4 = String.Format("select smm_jifen from smm_customer where smm_cardnumber='{0}'", customercardnumber);
                    ClassManageDataBase db4 = new ClassManageDataBase();
                    SqlDataReader dr4 = db4.SQLReader(sql4);
                    dr4.Read();
                    int m = Convert.ToInt32(dr4[0]);
                    int n = m + Convert.ToInt32(total);

                    string sql3 = String.Format("update smm_customer set smm_jifen={0} where smm_cardnumber='{1}'", n, customercardnumber);
                    ClassManageDataBase db3 = new ClassManageDataBase();
                    db3.SQLExecute(sql3);
                }


                for (int i = Sell.Count - 1; i >= 0; i--)
                {

                    Sell.RemoveAt(i);
                }
                customercardnumber = "";
                RefreshSell();

            }
        }

        private void buttonClearOrder_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确定要清除本订单吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = Sell.Count - 1; i >= 0; i--)
                {
                    Sell.RemoveAt(i);
                }
                customercardnumber = "";
                RefreshSell();
                MessageBox.Show("订单已清除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxCardNumber.SelectAll();
                textBoxCardNumber.Focus();
            }
        }



        private void SetOrder()
        {
            comboBoxOrderList.SelectedIndex = 0;
            listView6.GridLines = true;
            listView6.FullRowSelect = true;
            listView6.View = View.Details;
            listView6.Scrollable = true;
            listView6.MultiSelect = false;
            listView6.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView6.Columns.Add("订单号", 100, HorizontalAlignment.Left);
            listView6.Columns.Add("顾客", 100, HorizontalAlignment.Left);
            listView6.Columns.Add("订单金额", 100, HorizontalAlignment.Right);
            listView6.Columns.Add("订单时间", 150, HorizontalAlignment.Left);
            listView6.Columns.Add("状态", 150, HorizontalAlignment.Left);
            RefreshOrder();
        }
        private void RefreshOrder()
        {

            string str;
            if (comboBoxOrderList.SelectedIndex == 0)
            {
                if (textBoxOrderSearch.Text.Trim() == "")
                {
                    str = String.Format("");
                }
                else
                {
                    str = String.Format("where smm_customer like '%{0}%' or id like '%{1}%'", textBoxOrderSearch.Text.Trim(), textBoxOrderSearch.Text.Trim());
                }

            }
            else
            {
                if (textBoxOrderSearch.Text.Trim() == "")
                {
                    str = String.Format("where smm_tag='{0}'", comboBoxOrderList.SelectedIndex - 1);
                }
                else
                {
                    str = String.Format("where smm_tag='{0}' and (smm_customer like '%{1}%' or id like '%{2}%')", comboBoxOrderList.SelectedIndex - 1, textBoxOrderSearch.Text.Trim(), textBoxOrderSearch.Text.Trim());
                }

            }
            string sql = String.Format("select id,smm_price,smm_time,smm_customer,smm_tag from smm_sell " + str + " order by id desc");

            ClassManageDataBase db = new ClassManageDataBase();
            SqlDataReader dr = db.SQLReader(sql);
            listView6.Items.Clear();
            while (dr.Read())
            {
                string sql1 = String.Format("select smm_name from smm_customer where smm_cardnumber='{0}'", dr[3].ToString());
                ClassManageDataBase db1 = new ClassManageDataBase();
                SqlDataReader dr1 = db1.SQLReader(sql1);
                string name = "非会员";
                if (dr1.Read())
                {
                    name = dr1[0].ToString();
                }

                ListViewItem li = new ListViewItem();
                li.SubItems.Clear();
                li.SubItems[0].Text = dr[0].ToString();
                li.SubItems.Add(name);
                li.SubItems.Add(dr[1].ToString());
                li.SubItems.Add(dr[2].ToString());
                string g = "";
                if (dr[4].ToString() == "0")
                {
                    g = "实体店订单";
                }
                else if (dr[4].ToString() == "1")
                {
                    g = "网络订单（已下单）";
                }
                else if (dr[4].ToString() == "2")
                {
                    g = "网络订单（已打包）";
                }
                else
                {
                    g = "网络订单（已完成）";
                }
                li.SubItems.Add(g);
                li.Tag = dr[0].ToString();
                listView6.Items.Add(li);


            }

            try
            {
                listView6.Items[0].Selected = true;
            }
            catch { }


        }

        private void comboBoxOrderList_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshOrder();
        }

        private void textBoxOrderSearch_TextChanged(object sender, EventArgs e)
        {
            RefreshOrder();
        }

        private void buttonOrderAll_Click(object sender, EventArgs e)
        {
            textBoxOrderSearch.Text = "";
        }
        private string SeleteOrder()
        {
            string user = "";
            try
            {
                user = listView6.SelectedItems[0].Tag.ToString();
            }
            catch
            {
                MessageBox.Show("请选择订单记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return user;
        }

        private void buttonOrder_Click(object sender, EventArgs e)
        {
            string user = SeleteOrder();
            if (user != "")
            {
                new FormOrder(user).ShowDialog();
            }
            RefreshOrder();
            textBoxOrderSearch.SelectAll();
            textBoxOrderSearch.Focus();
        }

        private void listView6_DoubleClick(object sender, EventArgs e)
        {
            buttonOrder_Click(sender, e);
        }

        private void textBoxSuperMarketName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonSetSuperMarketName_Click(sender,  e);
            }
        }

        private void textBoxCustomerNewPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonCustomerNewPassword_Click(sender, e);
            }
        }

        private void textBoxFTPAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonSetFTP_Click(sender, e);
            }
        }

        private void textBoxFTPUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonSetFTP_Click(sender, e);
            }
        }

        private void textBoxFTPPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonSetFTP_Click(sender, e);
            }
        }
    }
}