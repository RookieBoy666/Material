using System.Collections.Generic;
using System.IO;
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
        public static string getMySet()
        {
            //读取网络上的txt文件
            //WebClient client = new WebClient();
            //byte[] buffer = client.DownloadData("http://域名/myset.txt");
           // FileStream file = new FileStream("\\config.txt", FileMode.Open);
            //byte[] buffer =
            byte[] buffer= ReadFile("config.txt");
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
            return   str;
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
    }
}
