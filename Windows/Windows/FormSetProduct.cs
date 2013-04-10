using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace SuperMarketManagement
{
    public partial class FormSetProduct : Form
    {
        int action;
        string number;
        bool upload = false;
        string uploadpicture = "";
        string tempdir = "Temp";
        string oldkzm = "";
        ClassEncryption ce = new ClassEncryption();
        public FormSetProduct(int a, string b)
        {
            InitializeComponent();
            action = a;
            number = b;
        }

        private void FormSetProduct_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            if (!Directory.Exists(tempdir))
            {
                Directory.CreateDirectory(tempdir);
            }
            if (action == 0)
            {
                this.Text = "添加商品";
                comboBoxDanwei.SelectedIndex = 0;
            }
            if (action == 1)
            {
                this.Text = "修改商品信息";
                string sql = String.Format("select smm_name,smm_danwei,smm_tag,smm_about,smm_price,smm_picture from smm_product where smm_number='{0}'", number);
                ClassManageDataBase db = new ClassManageDataBase();
                SqlDataReader dr = db.SQLReader(sql);
                dr.Read();
                textBoxNumber.Text = number;
                textBoxNumber.Enabled = false;
                textBoxName.Text = dr[0].ToString();
                comboBoxDanwei.Text = dr[1].ToString();
                textBoxTag.Text = dr[2].ToString();
                textBoxAbout.Text = dr[3].ToString();
                textBoxPrice.Text = dr[4].ToString();
                oldkzm = dr[5].ToString();

                if (oldkzm != "")
                {
                    ClassManageFTP cmt = new ClassManageFTP();
                    string now = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                    string kzm = dr[5].ToString();
                    string OldFile = number + "." + kzm;
                    string NewFile = System.Windows.Forms.Application.StartupPath + "\\" + tempdir + "\\" + now + number + "." + kzm;
                    if (!cmt.Download(OldFile, NewFile))
                    {
                        MessageBox.Show("加载图片失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        pictureBox1.Image = Image.FromFile(NewFile);
                        ////////File.Delete(NewFile);
                    }
                }
                // ;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (textBoxNumber.Text.Trim() == "")
            {
                MessageBox.Show("请输入商品编号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxNumber.SelectAll();
                textBoxNumber.Focus();
            }
            else if (textBoxName.Text.Trim() == "")
            {
                MessageBox.Show("请输入商品名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxName.SelectAll();
                textBoxName.Focus();
            }
            else if (textBoxPrice.Text.Trim() == "")
            {
                MessageBox.Show("请输入商品价格！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxPrice.SelectAll();
                textBoxPrice.Focus();
            }
            else
            {
                double price = -1;
                try
                {

                    price = Convert.ToDouble(textBoxPrice.Text);

                }
                catch
                {
                    MessageBox.Show("商品价格填写错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxPrice.SelectAll();
                    textBoxPrice.Focus();
                }

                if (price >= 0)
                {
                    if (action == 0)
                    {
                        string sql = String.Format("select smm_number from smm_product where smm_number='{0}'", textBoxNumber.Text);
                        ClassManageDataBase db = new ClassManageDataBase();
                        if (db.SQLNumber(sql) > 0)
                        {
                            MessageBox.Show("商品编号已经存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textBoxNumber.SelectAll();
                            textBoxNumber.Focus();
                        }
                        else
                        {
                            string kzm = oldkzm;
                            if (upload == true)
                            {

                                ClassManageFTP cmt = new ClassManageFTP();
                                kzm = uploadpicture.Substring(uploadpicture.LastIndexOf(".") + 1, (uploadpicture.Length - uploadpicture.LastIndexOf(".") - 1));
                                string newFile = textBoxNumber.Text + "." + kzm;
                                if (!cmt.Upload(uploadpicture, newFile))
                                {
                                    MessageBox.Show("已经添加成功，但是图片没有成功上传！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                // cmt.download("20030505.jpg","d:\\1.jpg");
                            }





                            string sql1 = String.Format("insert into smm_product (smm_number,smm_name,smm_danwei,smm_about,smm_tag,smm_price,smm_sum,smm_picture) values('{0}','{1}','{2}','{3}','{4}','{5}',0,'{6}')", textBoxNumber.Text, textBoxName.Text, comboBoxDanwei.Text, textBoxAbout.Text, textBoxTag.Text, price, kzm);
                            ClassManageDataBase db1 = new ClassManageDataBase();
                            db1.SQLExecute(sql1);
                            this.Close();


                        }
                    }
                    if (action == 1)
                    {


                        string kzm = oldkzm;
                        if (upload == true)
                        {
                            string deletefile = number + "." + oldkzm;
                            ClassManageFTP cmt1 = new ClassManageFTP();
                            cmt1.Delete(deletefile);


                            ClassManageFTP cmt = new ClassManageFTP();
                            kzm = uploadpicture.Substring(uploadpicture.LastIndexOf(".") + 1, (uploadpicture.Length - uploadpicture.LastIndexOf(".") - 1));
                            string newFile = number + "." + kzm;
                            if (!cmt.Upload(uploadpicture, newFile))
                            {
                                MessageBox.Show("已经修改成功，但是图片没有成功上传！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            // cmt.download("20030505.jpg","d:\\1.jpg");
                        }

                        string sql = String.Format("update smm_product set smm_name='{0}',smm_danwei='{1}',smm_about='{2}',smm_tag='{3}',smm_price='{4}',smm_picture='{5}' where smm_number='{6}'", textBoxName.Text, comboBoxDanwei.Text, textBoxAbout.Text, textBoxTag.Text, price, kzm, number);
                        ClassManageDataBase db = new ClassManageDataBase();
                        db.SQLExecute(sql);
                        this.Close();
                    }
                }
            }
        }

        private void textBoxNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBoxName.SelectAll();
                textBoxName.Focus();
            }
        }

        private void textBoxName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonOK_Click(sender, e);
            }
        }

        private void comboBoxDanwei_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonOK_Click(sender, e);
            }
        }

        private void textBoxPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonOK_Click(sender, e);
            }
        }

        private void textBoxTag_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonOK_Click(sender, e);
            }
        }

        private void buttonSelectPicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "打开图片";
            openFileDialog1.Filter = "所有图片文件|*.jpg;*.png;*.gif;*.bmp";
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                upload = true;
                uploadpicture = openFileDialog1.FileName;

                pictureBox1.Image = Image.FromFile(uploadpicture);






            }
        }
    }
}
