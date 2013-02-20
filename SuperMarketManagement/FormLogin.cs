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
    public partial class FormLogin : Form
    {
        private ClassEncryption ce = new ClassEncryption();
        public FormLogin()
        {
            InitializeComponent();
        }
        private void FormLogin_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            ClassManageDataBase db = new ClassManageDataBase();
            string sql = "select smm_tag from smm_user where smm_tag=1";
            SqlDataReader da = db.SQLReader(sql);
            if (!da.Read())
            {
                ClassManageDataBase db1 = new ClassManageDataBase();
                string sql1 = String.Format("insert into smm_user (smm_username,smm_password,smm_tag,smm_nickname) values ('admin','{0}','1','超级管理员')", ce.Encode("admin"));
                db1.SQLExecute(sql1);
            }
        }
        private void FormLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (textBoxUsername.Text.Trim() == "")
            {
                MessageBox.Show("请输入用户名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxUsername.SelectAll();
                textBoxUsername.Focus();
            }
            else if (textBoxPassword.Text.Trim() == "")
            {
                MessageBox.Show("请输入密码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxPassword.SelectAll();
                textBoxPassword.Focus();
            }
            else
            {
                string sql = String.Format("select smm_username from smm_user where smm_username='{0}' and smm_password='{1}'", textBoxUsername.Text, ce.Encode(textBoxPassword.Text));
                ClassManageDataBase db = new ClassManageDataBase();
                SqlDataReader dr = db.SQLReader(sql);
                if (dr.Read())
                {
                    new FormMain(dr[0].ToString()).Show();
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("用户名或密码错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxPassword.SelectAll();
                    textBoxPassword.Focus();
                }
            }
        }
        private void textBoxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
        }
        private void textBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonLogin_Click(sender, e);
            }
        }
        private void textBoxUsername_KeyDown(object sender, KeyEventArgs e)
        {
            textBoxPassword_KeyDown(sender, e);
        }
    }
}