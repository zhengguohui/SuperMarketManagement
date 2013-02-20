using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;//引用DLL

namespace SuperMarketManagement
{
    class ClassManageINI
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);
        private String strFilePath = System.Environment.CurrentDirectory + "\\Config.ini";//获取INI文件路径
        private String ContentValue(String Section, String key)
        {
            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(Section, key, "", temp, 1024, strFilePath);
            return temp.ToString();
        }
        public void setINI(String a, String b)
        {
            WritePrivateProfileString("Settings", a, b, strFilePath);
        }
        public String getINI(String a)
        {
            return ContentValue("Settings", a);
        }
    }
}