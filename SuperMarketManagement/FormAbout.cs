using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SuperMarketManagement
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }
        private void FormAbout_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}