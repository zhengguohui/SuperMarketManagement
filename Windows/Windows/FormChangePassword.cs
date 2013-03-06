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
    public partial class FormChangePassword : Form
    {
        private string username;
        private ClassEncryption ce = new ClassEncryption();
        public FormChangePassword(string username1)
        {
            InitializeComponent();
            username = username1;
        }
        private void FormChangePassword_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (textBoxOldPassword.Text.Trim() == "")
            {
                MessageBox.Show("请输入原始密码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxOldPassword.SelectAll();
                textBoxOldPassword.Focus();
            }
            else if (textBoxNewPassword.Text.Trim() == "")
            {
                MessageBox.Show("请输入新设密码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxNewPassword.SelectAll();
                textBoxNewPassword.Focus();
            }
            else if (textBoxRePassword.Text.Trim() == "")
            {
                MessageBox.Show("请输入重复密码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxRePassword.SelectAll();
                textBoxRePassword.Focus();
            }
            else if (textBoxRePassword.Text != textBoxNewPassword.Text)
            {
                MessageBox.Show("两次新密码输入不同！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxRePassword.SelectAll();
                textBoxRePassword.Focus();
            }
            else
            {
                string sql = String.Format("select smm_password from smm_user where smm_username='{0}'", username);
                ClassManageDataBase db = new ClassManageDataBase();
                SqlDataReader dr = db.SQLReader(sql);
                dr.Read();
                if (dr[0].ToString() != ce.Encode(textBoxOldPassword.Text))
                {
                    MessageBox.Show("原始密码错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxOldPassword.SelectAll();
                    textBoxOldPassword.Focus();
                }
                else
                {
                    string sql1 = String.Format("update smm_user set smm_password='{0}' where smm_username='{1}'", ce.Encode(textBoxNewPassword.Text), username);
                    ClassManageDataBase db1 = new ClassManageDataBase();
                    if (db1.SQLExecute(sql1))
                    {
                        MessageBox.Show("密码修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("密码修改出现问题！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}