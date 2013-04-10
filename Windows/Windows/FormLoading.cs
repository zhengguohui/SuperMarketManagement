using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SuperMarketManagement
{
    public partial class FormLoading : Form
    {
        public FormLoading()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void FormLogin_Shown(object sender, EventArgs e)
        {
            try
            {
                Directory.Delete("Temp", true);
            }
            catch { }
            Application.DoEvents();
            ClassManageDataBase cmdb = new ClassManageDataBase();
            bool a = cmdb.Open();

            Application.DoEvents();
            if (a == false)
            {
                MessageBox.Show("请在弹出的窗口里设置数据库参数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FormSetDataBase b = new FormSetDataBase();
                b.MinimizeBox = true;
                b.ShowInTaskbar = true;
                b.ShowDialog();
                this.Hide();
            }
            else
            {
                new FormLogin().Show();
                this.Hide();
            }
        }
    }
}