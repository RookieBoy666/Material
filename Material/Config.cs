using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Material
{
    public static class Config
    {

        //服务器
        public static string DataSource;
        //数据库
        public static string InitialCatalog;
        //用户Id
        public static string UserID;
        //密码
        public static string Password;
        //1、定义数组,来用接收txt里的内容
        public static Dictionary<string, string> dic = new Dictionary<string, string>();


        //2、读取远程txt配置文件的文件
        public static string getMySet(string filename)
        {
            //读取网络上的txt文件
            //WebClient client = new WebClient();
            //byte[] buffer = client.DownloadData("http://域名/myset.txt");
            // FileStream file = new FileStream("\\config.txt", FileMode.Open);
            //byte[] buffer =
            byte[] buffer = ReadFile(filename);
            string res = System.Text.ASCIIEncoding.ASCII.GetString(buffer);
            string[] items = res.Split(',');//用逗号来分割内容
            string str = "";
            if (items.Length == 0)
            {
                MessageBox.Show("读取配置文件失败");
                return "";
            }
            else
            {
                for (int i = 0; i < items.Length; i++)
                {
                    str = items[i].Replace("\r\n", ";");
                    //string a1 = str.Substring(0, str.IndexOf("="));
                    //string a2 = str.Substring(str.IndexOf("=") + 1);
                    //dic.Add(a1, a2);

                }
                //设置参数
                //if (dic.ContainsKey("Data Source")) //先判断是否存在这个key 
                //{
                //    DataSource = dic["Data Source"];
                //}
                //if (dic.ContainsKey("Initial Catalog"))
                //{
                //    InitialCatalog = dic["Initial Catalog"];
                //}
                //if (dic.ContainsKey("User ID"))
                //{
                //    UserID = dic["User ID"];
                //}
                //if (dic.ContainsKey("Password"))
                //{
                //    Password = dic["Password"];
                //}

            }
            return str;
        }

        //读filename到byte[]
        public static byte[] ReadFile(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate);
            byte[] buffer = new byte[fs.Length];
            try
            {
                fs.Read(buffer, 0, buffer.Length);
                fs.Seek(0, SeekOrigin.Begin);
                return buffer;
            }
            catch
            {
                return buffer;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }

        //加密解密
        public static string Encode(string encryptString)
        {
            try
            {
                string KEY = "zjp1202!";
                byte[] _vector = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

                var rgbKey = Encoding.UTF8.GetBytes(KEY.Substring(0, 8));
                var des = new DESCryptoServiceProvider();

                var inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, des.CreateEncryptor(rgbKey, _vector), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string Decode(string decryptString)
        {
            try
            {
                string KEY = "zjp1202!";
                byte[] _vector = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
                var provider = new DESCryptoServiceProvider();
                var rgbKey = Encoding.UTF8.GetBytes(KEY.Substring(0, 8));

                var inputByteArray = Convert.FromBase64String(decryptString);

                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, provider.CreateDecryptor(rgbKey, _vector), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                var encoding = new UTF8Encoding();

                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                return null;
            }
        }



      
        public static void WriteEncodeString(string str,string filename)
        {            //判断是否已经有了这个文件
            if (!System.IO.File.Exists(filename))
            {
                //没有则创建这个文件
                FileStream fs1 = new FileStream(filename, FileMode.Create, FileAccess.Write);//创建写入文件                //设置文件属性为隐藏
             //   System.IO.File.SetAttributes(@filename, FileAttributes.Hidden);
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine(str.Trim());//开始写入值
                sw.Close();
                fs1.Close();
                //return "注册成功";
            }
            else
            {
                FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Write);
               // System.IO.File.SetAttributes(@filename, FileAttributes.Hidden);
                StreamWriter sr = new StreamWriter(fs);

                sr.WriteLine(str.Trim());//开始写入值
                sr.Close();
                fs.Close();
                //  return "注册成功";
            }


        }
    }
}
