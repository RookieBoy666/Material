using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Material
{
    public partial class sMaterialNo : Form
    {
         //private string connStr = ConfigurationManager.AppSettings["connectionstring"];
        private string connStr = Config.getMySet("Config.txt");
        private string connStrTmp = Config.getMySet("Config1.txt");



        public sMaterialNo()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string connStrEncode;
            string connStrDecode;
            if (connStrTmp != connStr)
            {
                connStrEncode = Config.Encode(connStr);//加密
                Config.WriteEncodeString(connStrEncode, "Config.txt");  //保存加密的文件()
                Config.WriteEncodeString(connStrEncode, "Config1.txt");  //保存加密的文件
            }
            else
            {
                connStrDecode = Config.Decode(connStr.Substring(0, connStr.Length - 1));//解密
                connStr = connStrDecode;
            }
            //string connStrMD5 = Config.MD5Encrypt(connStr);
            // string connStrEncode = Config.Encode(connStr);//加密
            //Config.WriteEncodeString(connStrEncode,"Config1.txt");  //保存加密的文件
            // string connStrDecode = Config.Decode(connStrEncode);//解密
        }

        private void search_Click(object sender, EventArgs e)
        {
            var textsMaterialNo = textBox1.Text.ToUpper();
            if (textsMaterialNo.Length < 4)
            {
                MessageBox.Show("查询字符少于4位");
            }
            else
            {
                string sql = "SELECT sMaterialNo as 物料编码,ISNULL(bAnalyseFinish,0)   AS   是否完成,tUpdateTime 更新时间 FROM dbo.mmMaterial(nolock) where sMaterialCategory='fabric' ";
                sql += " and   sMaterialNo like  '%" + textsMaterialNo + "%'";

                sql += "   order by tUpdateTime desc ";
                try
                {
                    SqlConnection Sqlconn = new SqlConnection(connStr);
                    SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                    Sqlconn.Open();
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sql, Sqlconn);
                    da.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0];
                    Sqlconn.Close();
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message, "查找失败", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        private void finish_Click(object sender, EventArgs e)
        {
            string smaterialNoFinish;
            string execSql;
            //int ccc = dataGridView1.CurrentCell.RowIndex;
            //string[] str = new string[dataGridView1.Rows.Count];
            DataTable dt = (DataTable)this.dataGridView1.DataSource;
            if (dataGridView1.Rows.Count != 0)
            {
                int ccc = dataGridView1.CurrentCell.RowIndex;//当前行下标
                                                             //string a = dt.Rows[ccc]["是否完成"].ToString();
                                                             //smaterialNoFinish = dt.Rows[ccc]["物料编码"].ToString();
                                                             //bool b = dataGridView1.Rows[1].Selected;
                smaterialNoFinish = dataGridView1[0, ccc].Value.ToString();//当前行物料编码
                string a = dataGridView1[1, ccc].Value.ToString();//当前行  是否完成

                if (a == "True")    // 当前行已完成勾选，生成
                {
                    execSql = "  DECLARE @name VARCHAR(200) SET @name ='" + smaterialNoFinish + "'  SELECT dbo.FN_Post('http://192.168.88.206:8088/fn/task/generate?materialNo=' + @name)";
                    try
                    {
                        SqlConnection connectionString1 = new SqlConnection(connStr);
                        SqlCommand cmd1 = new SqlCommand(execSql, connectionString1);
                        connectionString1.Open();
                        cmd1.ExecuteNonQuery();   //完成
                        //string tUpdateTime = DateTime.Now.ToString();
                        ////更新 bAnalyseFinish
                        //string updatebAnalyseFinish = "update mmMaterial set bAnalyseFinish='1',tUpdateTime= '" + tUpdateTime + "' where smaterialNo='" + smaterialNoFinish + "' ";

                        //SqlCommand cmdUpdate = new SqlCommand(updatebAnalyseFinish, connectionString);
                        //cmdUpdate.ExecuteNonQuery();
                        ////刷新页面
                        //DataSet ds = new DataSet();
                        //SqlDataAdapter da = new SqlDataAdapter(sqlReset, conn);
                        //da.Fill(ds);
                        //dataGridView1.DataSource = ds.Tables[0];
                        connectionString1.Close();
                    }
                    catch (Exception ee)
                    {
                        {
                            MessageBox.Show(ee.Message, "生成失败", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            Log logFail= new Log();
                            var failTime = System.DateTime.Now;
                            logFail.RegisterLog("物料编码:" + smaterialNoFinish, "失败时间:" + failTime.ToString());
                        }
                    }
                    //   dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Red;
                    dataGridView1.Rows[ccc].DefaultCellStyle.BackColor = Color.Green;
                    //MessageBox.Show("生成成功！");
                    Log log = new Log();
                    var now = System.DateTime.Now;
                    log.RegisterLog("物料编码:" + smaterialNoFinish, "生成时间:" + now.ToString());
                }
                else
                {
                    MessageBox.Show("当前物料未完成，不允许生成！");
                }
            }
            else
            {
                MessageBox.Show("请选择需要生成的物料！");
            }
        }
        private void quit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                search.PerformClick();
        }
    }
}
