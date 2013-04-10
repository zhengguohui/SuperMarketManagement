using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SuperMarketManagement
{
    public partial class FormSetCustomer : Form
    {
        ClassEncryption ce = new ClassEncryption();
        int action;
        string cardnumber;
        public FormSetCustomer(int a, string b)
        {
            InitializeComponent();
            action = a;
            cardnumber = b;
        }

        private void FormSetCustomer_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            if (action == 1)
            {
                this.Text = "修改会员信息";
                this.AcceptButton = buttonOK;

                string sql = String.Format("select smm_idcard,smm_name,smm_sex,smm_address,smm_telephone,smm_cellphone,smm_password,smm_cardnumber,smm_other from smm_customer where id={0}", cardnumber);
                ClassManageDataBase db = new ClassManageDataBase();
                SqlDataReader dr = db.SQLReader(sql);
                dr.Read();
                textBoxIDCard.Text = dr[0].ToString();
                textBoxCustomerName.Text = dr[1].ToString();
                if (dr[2].ToString() == "0")
                {
                    radioButtonFemale.Checked = true;
                }
                else if (dr[2].ToString() == "1")
                {
                    radioButtonMan.Checked = true;
                }
                else
                {
                    radioButtonUnknown.Checked = true;
                }
                textBoxAddress.Text = dr[3].ToString();
                textBoxTelePhone.Text = dr[4].ToString();
                textBoxCellPhone.Text = dr[5].ToString();
                textBoxCustomerPassword.Text = ce.Decode(dr[6].ToString());
                textBoxCustomerRePassword.Text = ce.Decode(dr[6].ToString());
                textBoxCardNumber.Text = dr[7].ToString();
                textBoxCardNumber.Enabled = false;
                textBoxCustomerOther.Text = dr[8].ToString();
                label11.Text = "";
            }
            if (action == 0)
            {
                this.Text = "增加会员信息";
                ClassSettings cs = new ClassSettings();

                textBoxCustomerPassword.Text = cs.GetSettings("customernewpassword");
                textBoxCustomerRePassword.Text = cs.GetSettings("customernewpassword");
                label11.Text = "会员的初始密码是：" + cs.GetSettings("customernewpassword");
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (textBoxCardNumber.Text.Trim() == "")
            {
                MessageBox.Show("请输入卡号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxCardNumber.SelectAll();
                textBoxCardNumber.Focus();
            }
            else if (textBoxCustomerPassword.Text != textBoxCustomerRePassword.Text)
            {
                MessageBox.Show("两次密码输入不同！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxCustomerRePassword.SelectAll();
                textBoxCustomerRePassword.Focus();
            }
            else
            {
                int sex;
                if (radioButtonMan.Checked == true)
                {
                    sex = 1;
                }
                else if (radioButtonFemale.Checked == true)
                {
                    sex = 0;
                }
                else
                {
                    sex = 2;
                }
                if (action == 0)
                {
                    string sql = String.Format("select smm_cardnumber from smm_customer where smm_cardnumber='{0}'", textBoxCardNumber.Text.Trim());
                    ClassManageDataBase db = new ClassManageDataBase();
                    if (db.SQLNumber(sql) > 0)
                    {
                        MessageBox.Show("卡号重复！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBoxCardNumber.SelectAll();
                        textBoxCardNumber.Focus();
                    }
                    else
                    {

                        string sql1 = String.Format("insert into smm_customer (smm_cardnumber,smm_idcard,smm_name,smm_sex,smm_address,smm_telephone,smm_cellphone,smm_jifen,smm_password,smm_other) values ('{0}','{1}','{2}',{3},'{4}','{5}','{6}',{7},'{8}','{9}')", textBoxCardNumber.Text, textBoxIDCard.Text, textBoxCustomerName.Text, sex, textBoxAddress.Text, textBoxTelePhone.Text, textBoxCellPhone.Text, 0, ce.Encode(textBoxCustomerPassword.Text), textBoxCustomerOther.Text);
                        ClassManageDataBase db1 = new ClassManageDataBase();
                        db1.SQLExecute(sql1);
                        this.Close();
                    }



                }
                if (action == 1)
                {
                    string sql2 = String.Format("update smm_customer set smm_idcard='{0}',smm_name='{1}',smm_sex={2},smm_address='{3}',smm_telephone='{4}',smm_cellphone='{5}',smm_password='{6}',smm_other='{7}' where id={8}", textBoxIDCard.Text, textBoxCustomerName.Text, sex, textBoxAddress.Text, textBoxTelePhone.Text, textBoxCellPhone.Text, ce.Encode(textBoxCustomerPassword.Text), textBoxCustomerOther.Text, cardnumber);
                    ClassManageDataBase db2 = new ClassManageDataBase();
                    db2.SQLExecute(sql2);
                    this.Close();
                }


            }
            //this.Dispose();
        }

        private void textBoxCardNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBoxCustomerName.SelectAll();
                textBoxCustomerName.Focus();
            }
        }

        private void textBoxCardNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxCustomerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonOK_Click(sender, e);
            }
        }

        private void textBoxCellPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonOK_Click(sender, e);
            }
        }

        private void textBoxTelePhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonOK_Click(sender, e);
            }
        }

        private void textBoxIDCard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonOK_Click(sender, e);
            }
        }

        private void textBoxAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonOK_Click(sender, e);
            }
        }

        private void textBoxCustomerPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonOK_Click(sender, e);
            }
        }

        private void textBoxCustomerRePassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonOK_Click(sender, e);
            }
        }
    }
}
