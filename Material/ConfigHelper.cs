using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace Material
{
    public static class ConfigHelper
    {
        public static void SetValue(String AppKey, String AppValue)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(System.Windows.Forms.Application.ExecutablePath + ".config");
            XmlNode xNode;
            XmlElement xElem1;
            XmlElement xElem2;
            xNode = xDoc.SelectSingleNode("//appSettings");
            xElem1 = (XmlElement)xNode.SelectSingleNode("//add[@key='" + AppKey + "']");
            if (xElem1 != null)
                xElem1.SetAttribute("value", AppValue);
            else
            {
                xElem2 = xDoc.CreateElement("add");
                xElem2.SetAttribute("key", AppKey);
                xElem2.SetAttribute("value", AppValue);
                xNode.AppendChild(xElem2);
            }
            xDoc.Save(Application.ExecutablePath + ".config");
        }
        /// <summary>
        /// 返回字典，字典的值不是动态的
        /// </summary>
        /// <param name="xmlName"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDictionaryFromXml(string xmlName)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            XmlDocument xDoc = new XmlDocument();
            string filePath = System.Environment.CurrentDirectory.Replace("\\bin\\Debug", "\\") + xmlName;// "\\SQL.xml";
            xDoc.Load(filePath);

            XmlNode xNode;

            xNode = xDoc.SelectSingleNode("//SQLSettings");
            //xElem1 = (XmlElement)xNode.SelectSingleNode("//add[@key='" + AppKey + "']");
            XmlElement DataSourcexElem = (XmlElement)xNode.SelectSingleNode("//add[@key='" + "Data Source" + "']");
            XmlElement InitialCatalogxElem = (XmlElement)xNode.SelectSingleNode("//add[@key='" + "Initial Catalog" + "']");
            XmlElement PersistSecurityInfoxElem = (XmlElement)xNode.SelectSingleNode("//add[@key='" + "Persist Security Info" + "']");
            XmlElement ConnectTimeoutxElem = (XmlElement)xNode.SelectSingleNode("//add[@key='" + "Connect Timeout" + "']");
            XmlElement UserIDxElem = (XmlElement)xNode.SelectSingleNode("//add[@key='" + "User ID" + "']");
            XmlElement PasswordxElem = (XmlElement)xNode.SelectSingleNode("//add[@key='" + "Password" + "']");

            string DataSourceKey = DataSourcexElem.GetAttribute("key");
            String DataSourceValue = DataSourcexElem.GetAttribute("value");


            string InitialCatalogKey = InitialCatalogxElem.GetAttribute("key");
            string InitialCatalogValue = InitialCatalogxElem.GetAttribute("value");

            string PersistSecurityInfoKey = PersistSecurityInfoxElem.GetAttribute("key");
            string PersistSecurityInfoValue = PersistSecurityInfoxElem.GetAttribute("value");

            string ConnectTimeoutKey = ConnectTimeoutxElem.GetAttribute("key");
            string ConnectTimeoutValue = ConnectTimeoutxElem.GetAttribute("value");

            string UserIDKey = UserIDxElem.GetAttribute("key");
            string UserIDValue = UserIDxElem.GetAttribute("value");

            string PasswordKey = PasswordxElem.GetAttribute("key");
            string PasswordValue = PasswordxElem.GetAttribute("value");


            keyValuePairs.Add(DataSourceKey, DataSourceValue);
            keyValuePairs.Add(InitialCatalogKey, InitialCatalogValue);
            keyValuePairs.Add(PersistSecurityInfoKey, PersistSecurityInfoValue);
            keyValuePairs.Add(ConnectTimeoutKey, ConnectTimeoutValue);
            keyValuePairs.Add(UserIDKey, UserIDValue);
            keyValuePairs.Add(PasswordKey, PasswordValue);

            return keyValuePairs;
            //ArrayList arrayList = new ArrayList();
            //arrayList.Add(DataSourceValue);
            //arrayList.Add(InitialCatalogValue);
            //arrayList.Add(PersistSecurityInfoValue);
            //arrayList.Add(ConnectTimeoutValue);
            //arrayList.Add(UserIDValue);
            //arrayList.Add(PasswordValue);
            //return arrayList;

        }
        /// <summary>
        /// 返回Hashtable ,Hashtable的值不是动态的
        /// </summary>
        /// <param name="xmlName"></param>
        /// <returns></returns>
        public static Hashtable GetHashTableFromXml(string xmlName)
        {
            Hashtable hashtable = new Hashtable();
            XmlDocument xDoc = new XmlDocument();
            string filePath = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "") + xmlName;// "\\SQL.xml";
            try
            {
                xDoc.Load(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //Log log = new Log();
            //log.RegisterLog(filePath,DateTime.Now.ToString());
            XmlNode xNode;

            xNode = xDoc.SelectSingleNode("//SQLSettings");
            //xElem1 = (XmlElement)xNode.SelectSingleNode("//add[@key='" + AppKey + "']");
            XmlElement DataSourcexElem = (XmlElement)xNode.SelectSingleNode("//add[@key='" + "Data Source" + "']");
            XmlElement InitialCatalogxElem = (XmlElement)xNode.SelectSingleNode("//add[@key='" + "Initial Catalog" + "']");
            XmlElement PersistSecurityInfoxElem = (XmlElement)xNode.SelectSingleNode("//add[@key='" + "Persist Security Info" + "']");
            XmlElement ConnectTimeoutxElem = (XmlElement)xNode.SelectSingleNode("//add[@key='" + "Connect Timeout" + "']");
            XmlElement UserIDxElem = (XmlElement)xNode.SelectSingleNode("//add[@key='" + "User ID" + "']");
            XmlElement PasswordxElem = (XmlElement)xNode.SelectSingleNode("//add[@key='" + "Password" + "']");

            string DataSourceKey = DataSourcexElem.GetAttribute("key");
            string DataSourceValue = DataSourcexElem.GetAttribute("value");


            string InitialCatalogKey = InitialCatalogxElem.GetAttribute("key");
            string InitialCatalogValue = InitialCatalogxElem.GetAttribute("value");

            string PersistSecurityInfoKey = PersistSecurityInfoxElem.GetAttribute("key");
            string PersistSecurityInfoValue = PersistSecurityInfoxElem.GetAttribute("value");

            string ConnectTimeoutKey = ConnectTimeoutxElem.GetAttribute("key");
            string ConnectTimeoutValue = ConnectTimeoutxElem.GetAttribute("value");

            string UserIDKey = UserIDxElem.GetAttribute("key");
            string UserIDValue = UserIDxElem.GetAttribute("value");

            string PasswordKey = PasswordxElem.GetAttribute("key");
            string PasswordValue = PasswordxElem.GetAttribute("value");
            hashtable.Add(DataSourceKey, DataSourceValue);
            hashtable.Add(InitialCatalogKey, InitialCatalogValue);
            hashtable.Add(PersistSecurityInfoKey, PersistSecurityInfoValue);
            hashtable.Add(ConnectTimeoutKey, ConnectTimeoutValue);
            hashtable.Add(UserIDKey, UserIDValue);
            hashtable.Add(PasswordKey, PasswordValue);
            return hashtable;


        }
        //public static string  GetConnectSql(string xmlName)
        //{
        //     string connectString="";
        //    // ArrayList arrayList = new ArrayList();
        //    //arrayList= GetSQLArrayFromXml(xmlName);
        //    //Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
        //    //keyValuePairs= GetDictionaryFromXml(xmlName);
        //    //for (int i=0;i<arrayList.Count;i++)
        //    //{
        //    //    if (arrayList[i].ToString() != "User ID" || arrayList[i].ToString() != "Password")
        //    //    {
        //    //        connectString += arrayList[i] + ";"; 
        //    //    }
        //    //    else
        //    //    {
        //    //        connectString += Decode(arrayList[i].ToString(), "zjp1202!") + ";";
        //    //    }
        //    //}
        //    return connectString;
        //}
        /// <summary>
        /// 把HashTable中的键值对集合拼接成字符串
        /// </summary>
        /// <param name="xmlName"></param>
        /// <returns></returns>
        public static string GetConnectSql(string xmlName)
        {
            Hashtable hashtable = ConfigHelper.GetHashTableFromXml(xmlName);
            string sqlStr = "";
            foreach (DictionaryEntry kvp in hashtable)
            {
                //Console.WriteLine(kvp.Key);
                //Console.WriteLine(kvp.Value);
                if (kvp.Key.ToString() != "User ID" && kvp.Key.ToString() != "Password")
                {
                    sqlStr += kvp.Key.ToString() + "=" + kvp.Value.ToString() + ";";
                }
                else
                {
                    //解密
                    sqlStr += kvp.Key.ToString() + "=" + Decode(kvp.Value.ToString()) + ";";
                }
            }
            return sqlStr;
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
        /// <summary>
        /// 获取字符串中的数字 
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>数字</returns>
        public static decimal GetNumber(string str)
        {
            decimal result = 0;
            if (str != null && str != string.Empty)
            {
                // 正则表达式剔除非数字字符（不包含小数点.）
                str = Regex.Replace(str, @"[^/d./d]", "");
                //str = Regex.Replace(str, @"[^\d.\d]", "");
                // 如果是数字，则转换为decimal类型
                if (Regex.IsMatch(str, @"^[+-]?\d*[.]?\d*$"))
                {
                    result = decimal.Parse(str);
                }
            }
            return result;
        }
    }
}
