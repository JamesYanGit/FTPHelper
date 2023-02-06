using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace FTPHelper
{
    public class FTPHelperClass
    {
        private string _hostServer;
        private string _hostName;
        private string _hostPsw;
        private string _hostDirectory;
        public FTPHelperClass(string hostServer, string hostName, string hostPsw, string directory) 
        {
            _hostServer=hostServer;
            _hostName=hostName;
            _hostPsw=hostPsw;
            _hostDirectory=directory;
        }
        public bool onload(string file)
        {
            
            FtpWebRequest ftp;
            
            FileInfo f = new FileInfo(file);
            ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(@"ftp://"+_hostServer + _hostDirectory + f.Name));
            
            ftp.Credentials = new NetworkCredential(_hostName, _hostPsw);
            ftp.KeepAlive = false;
            ftp.Method = WebRequestMethods.Ftp.UploadFile;
            ftp.UseBinary = true;
            ftp.ContentLength = f.Length;
            int buffLength = 20480;
            byte[] buff = new byte[buffLength];
            int contentLen;
            try
            {
                
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public string fload(string fileName)
        {
            try
            {
                FtpWebRequest ftp;
                ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(@"ftp://" + _hostServer + _hostDirectory + fileName));
                
                ftp.Credentials = new NetworkCredential(_hostName, _hostPsw);
                WebResponse wr = ftp.GetResponse();
                StreamReader sr = new StreamReader(wr.GetResponseStream(), System.Text.Encoding.Default);
                string s = sr.ReadLine();
                while (s.Equals(""))
                {
                    s = sr.ReadLine();
                }
                return s;
            }
            catch (Exception)
            {
                //return "serverDown";
                //throw;
            }
            return "";
            
        }

        public string detectFTPServer() 
        {
            FtpWebRequest ftp;
            ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(@"ftp://" + _hostServer + _hostDirectory));
            
            ftp.Credentials = new NetworkCredential(_hostName, _hostPsw);
            ftp.Method=WebRequestMethods.Ftp.PrintWorkingDirectory;
            ftp.KeepAlive = false;
            try
            {
                WebResponse wr = ftp.GetResponse();
                return "";
            }
            catch 
            {

                return "serverDown";
            }
            
        }


        public string DeleteFile(string fileName)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(@"ftp://" + _hostServer + _hostDirectory + fileName);
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            request.Credentials = new NetworkCredential(_hostName, _hostPsw);

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                return response.StatusDescription;
            }
        }
    }
}
