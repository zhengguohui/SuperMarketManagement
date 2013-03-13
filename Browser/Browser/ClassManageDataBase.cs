using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Website
{
    class ClassManageDataBase
    {
        private string ConnectionString;
        private SqlConnection con;
        private bool IsOpen;
        public ClassManageDataBase()
        {
            //ClassEncryption ce = new ClassEncryption();
            //ClassManageINI cm = new ClassManageINI();
            //string DataBaseAddress = ce.Decode(cm.getINI("DataBaseAddress"));
            //string DataBaseUserName = ce.Decode(cm.getINI("DataBaseUserName"));
            //string DataBasePassword = ce.Decode(cm.getINI("DataBasePassword"));
            //string DataBaseName = ce.Decode(cm.getINI("DataBaseName"));
            string DataBaseAddress = "localhost\\SQLEXPRESS";
            string DataBaseUserName = "sa";
            string DataBasePassword = "123456";
            string DataBaseName = "SuperMarketManagement";
            ConnectionString = String.Format("Server={0};DataBase={1};Uid={2};pwd={3};", DataBaseAddress, DataBaseName, DataBaseUserName, DataBasePassword);
            //Data Source=CANDISON\SQLEXPRESS;Initial Catalog=SuperMarketManagement;User ID=sa;Password=***********
            //ConnectionString = "Data Source=CANDISON\\SQLEXPRESS;Initial Catalog=SuperMarketManagement;Integrated Security=True;Pooling=False";
            con = new SqlConnection(ConnectionString);
            try
            {
                con.Open();
                IsOpen = true;
            }
            catch
            {
                //MessageBox.Show("连接数据库失败，请联系管理员！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                IsOpen = false;
            }
        }
        public bool Open()
        {
            return IsOpen;
        }
        public void Close()
        {
            try
            {
                con.Close();
            }
            catch
            {
            }
        }
        public bool SQLExecute(string sql)
        {
            SqlCommand com = new SqlCommand(sql, con);
            int num = com.ExecuteNonQuery();
            Close();
            if (num > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public SqlDataReader SQLReader(string sql)
        {

            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader da = com.ExecuteReader();
            return da;
        }
        public int SQLNumber(string sql)
        {
            SqlDataReader da = SQLReader(sql);
            int a = 0;
            while (da.Read())
            {
                a++;
            }
            Close();
            return a;
        }
           
    }
}