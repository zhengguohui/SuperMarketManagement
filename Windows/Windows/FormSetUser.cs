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
    public partial class FormSetUser : Form
    {
        string username;
        int action;
        private ClassEncryption ce = new ClassEncryption();
        public FormSetUser(int a, String b)
        {
            InitializeComponent();
            action = a;
            username = b;
        }

        private void FormSetUser_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            if (action == 1)
            {
                this.Text = "修改用户";
                string sql = String.Format("select smm_username,smm_tag,smm_password,smm_nickname from smm_user where smm_username='{0}'", username);
                ClassManageDataBase db = new ClassManageDataBase();
                SqlDataReader dr = db.SQLReader(sql);
                dr.Read();
                textBoxUserName.Text = dr[0].ToString();
                textBoxNickName.Text = dr[3].ToString();
                textBoxPassWord.Text = ce.Decode(dr[2].ToString());
                textBoxRePassword.Text = ce.Decode(dr[2].ToString());
                comboBox1.SelectedIndex = Convert.ToInt32(dr[1]) - 1;
                textBoxUserName.Enabled = false;
            }
            if (action == 0)
            {
                this.Text = "添加用户";
                comboBox1.SelectedIndex = 0;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (textBoxUserName.Text == "")
            {
                MessageBox.Show("请输入账户！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxUserName.SelectAll();
                textBoxUserName.Focus();
            }
            else
                if (textBoxNickName.Text == "")
                {
                    MessageBox.Show("请输入昵称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxNickName.SelectAll();
                    textBoxNickName.Focus();
                }
                else if (textBoxPassWord.Text == "")
                {
                    MessageBox.Show("请输入密码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxPassWord.SelectAll();
                    textBoxPassWord.Focus();
                }
                else if (textBoxRePassword.Text == "")
                {
                    MessageBox.Show("请输入密码重复！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxRePassword.SelectAll();
                    textBoxRePassword.Focus();
                }
                else if (textBoxRePassword.Text != textBoxPassWord.Text)
                {
                    MessageBox.Show("两次密码输入不同！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxRePassword.SelectAll();
                    textBoxRePassword.Focus();
                }
                else
                {

                    string sql = "";

                    if (action == 0)
                    {
                        string sql1 = String.Format("select smm_username from smm_user where smm_username='{0}'", textBoxUserName.Text);
                        ClassManageDataBase db1 = new ClassManageDataBase();
                        SqlDataReader dr = db1.SQLReader(sql1);
                        if (dr.Read())
                        {
                            MessageBox.Show("账户重复！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textBoxUserName.SelectAll();
                            textBoxUserName.Focus();
                        }
                        else
                        {
                            sql = String.Format("insert into smm_user(smm_username,smm_password,smm_tag,smm_nickname) values('{0}','{1}','{2}','{3}')", textBoxUserName.Text, ce.Encode(textBoxPassWord.Text), comboBox1.SelectedIndex + 1, textBoxNickName.Text);
                            ClassManageDataBase db = new ClassManageDataBase();
                            db.SQLExecute(sql);
                            this.Close();
                        }



                    }
                    if (action == 1)
                    {
                        sql = String.Format("update smm_user set smm_password='{0}',smm_nickname='{1}',smm_tag='{2}' where smm_username='{3}'", ce.Encode(textBoxPassWord.Text), textBoxNickName.Text, comboBox1.SelectedIndex + 1, textBoxUserName.Text);
                        ClassManageDataBase db = new ClassManageDataBase();
                        db.SQLExecute(sql);
                        this.Close();
                    }



                }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
