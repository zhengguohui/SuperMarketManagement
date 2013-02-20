using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SuperMarketManagement
{
    public partial class FormSetDataBase : Form
    {
        private ClassEncryption ce = new ClassEncryption();
        public FormSetDataBase()
        {
            InitializeComponent();
        }
        private void FormSetDataBase_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            ClassManageINI cm = new ClassManageINI();
            this.textBoxAddress.Text = ce.Decode(cm.getINI("DataBaseAddress"));
            this.textBoxUsername.Text = ce.Decode(cm.getINI("DataBaseUserName"));
            this.textBoxPassword.Text = ce.Decode(cm.getINI("DataBasePassword"));
            this.textBoxDataBaseName.Text = ce.Decode(cm.getINI("DataBaseName"));
        }
        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
            if (this.ShowInTaskbar == true)
            {
                Application.Exit();
            }
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (textBoxAddress.Text.Trim() == "")
            {
                MessageBox.Show("请输入数据库服务器地址！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxAddress.SelectAll();
                textBoxAddress.Focus();
            }
            else if (textBoxUsername.Text.Trim() == "")
            {
                MessageBox.Show("请输入数据库用户名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxUsername.SelectAll();
                textBoxUsername.Focus();
            }
            else if (textBoxPassword.Text.Trim() == "")
            {
                MessageBox.Show("请输入数据库密码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxPassword.SelectAll();
                textBoxPassword.Focus();
            }
            else if (textBoxDataBaseName.Text.Trim() == "")
            {
                MessageBox.Show("请输入数据库名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxDataBaseName.SelectAll();
                textBoxDataBaseName.Focus();
            }
            else
            {
                ClassManageINI cm = new ClassManageINI();
                cm.setINI("DataBaseAddress", ce.Encode(textBoxAddress.Text));
                cm.setINI("DataBaseUserName", ce.Encode(textBoxUsername.Text));
                cm.setINI("DataBasePassword", ce.Encode(textBoxPassword.Text));
                cm.setINI("DataBaseName", ce.Encode(textBoxDataBaseName.Text));
                ClassManageDataBase da = new ClassManageDataBase();
                if (da.Open() == true)
                {
                    MessageBox.Show("数据库设置成功，请重新登录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    System.Diagnostics.Process.Start(Application.ExecutablePath);
                    Application.ExitThread();
                }
            }
        }
        private void FormSetDataBase_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.ShowInTaskbar == true)
            {
                Application.Exit();
            }
        }
    }
}