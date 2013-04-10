using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace SuperMarketManagement
{
    class ClassSettings
    {
        private string Settings(string a)
        {
            ClassManageDataBase db = new ClassManageDataBase();
            string sql = String.Format("select smm_value from smm_settings where smm_settings='{0}'", a);
            SqlDataReader dr = db.SQLReader(sql);
            if (!dr.Read())
            {
                string sql1 = String.Format("insert into smm_settings(smm_settings) values ('{0}')", a);
                ClassManageDataBase db1 = new ClassManageDataBase();
                db1.SQLExecute(sql1);
                return "";
            }
            else
            {
                return dr[0].ToString();
            }
        }
        public string GetSettings(string a)
        {
            return Settings(a);

        }
        public void SetSettings(string a, string b)
        {
            Settings(a);
            string sql = String.Format("update smm_settings set smm_value='{0}' where smm_settings='{1}'", b, a);
            ClassManageDataBase db = new ClassManageDataBase();
            db.SQLExecute(sql);
        }
    }


}
