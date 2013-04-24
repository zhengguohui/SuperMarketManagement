using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace SuperMarketManagement
{
    class ClassManageFTP
    {
        private string address = "";
        private string username = "";
        private string password = "";
        public ClassManageFTP()
        {
            ClassSettings cs = new ClassSettings();
            address = cs.GetSettings("ftpaddress");
            username = cs.GetSettings("ftpusername");
            password = cs.GetSettings("ftppassword");
        }
        public ClassManageFTP(string a, string b, string c)
        {
            address = a;
            username = b;
            password = c;
        }
        public bool Upload(string OldFile, string UploadFileName)
        {
            try
            {
                FtpWebRequest ftp;
                FileInfo f = new FileInfo(OldFile);
                ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + address + "/" + UploadFileName));
                ftp.Credentials = new NetworkCredential(username, password);
                ftp.KeepAlive = false;
                ftp.Method = WebRequestMethods.Ftp.UploadFile;
                ftp.ContentLength = f.Length;
                int buffLength = 20480;
                byte[] buff = new byte[buffLength];
                int contentLen;
                FileStream fs = f.OpenRead();
                Stream sw = ftp.GetRequestStream();
                contentLen = fs.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    sw.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                sw.Close();
                fs.Close();
                return true;
            }
            catch { return false; }
        }
        public bool Download(string GetFilename, string SaveFileName)
        {
            try
            {
                FtpWebRequest ftp;
                ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + address + "/" + GetFilename));
                ftp.Credentials = new NetworkCredential(username, password);
                FileStream outputStream = new FileStream(SaveFileName, FileMode.Create);
                ftp.Method = WebRequestMethods.Ftp.DownloadFile;
                ftp.UseBinary = true;
                ftp.KeepAlive = false;
                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }
                ftpStream.Close();
                outputStream.Close();
                response.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(string filename)
        {
            try
            {
                FtpWebRequest ftp;
                ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + address + "/" + filename));
                ftp.Credentials = new NetworkCredential(username, password);
                ftp.Method = WebRequestMethods.Ftp.DeleteFile;
                ftp.KeepAlive = false;
                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
                response.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
