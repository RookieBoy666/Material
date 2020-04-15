using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Material
{
    public partial class sMaterialNo : Form
    {
        private string connStr = ConfigurationManager.AppSettings["connectionstring"];
        private int countNum;
        private string sqlReset = "SELECT  sMaterialNo ,bAnalyseFinish,tUpdateTime FROM dbo.mmMaterial  WHERE bAnalyseFinish=0 order by tUpdateTime desc";
        private string sqlCount = "SELECT  count(sMaterialNo) ";

        public sMaterialNo()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //string sql = sqlReset;
            //try
            //{
            //    SqlConnection connectionString = new SqlConnection(conn);
            //    SqlCommand cmd = new SqlCommand(sql, connectionString);
            //    connectionString.Open();
            //    DataSet ds = new DataSet();
            //    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            //    da.Fill(ds);
            //    dataGridView1.DataSource = ds.Tables[0];
            //    connectionString.Close();
            //}
            //catch (Exception ee)
            //{
            //    {
            //        MessageBox.Show(ee.Message, "数据库操作失败", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    }
            //}

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void mmMaterialBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void search_Click(object sender, EventArgs e)
        {
            var textsMaterialNo = textBox1.Text.ToUpper();
            //var textbAnalyseFinish = bAnalyseFinish.Text;
            //if (textbAnalyseFinish == "是")
            //{
            //    textbAnalyseFinish = "1";
            //}
            //else
            //{
            //    textbAnalyseFinish = "0";
            //}

            string sql = "SELECT sMaterialNo as 物料编码,bAnalyseFinish as 是否完成,tUpdateTime 更新时间 FROM dbo.mmMaterial where 1=1  AND  (sMaterialNo NOT LIKE '%(%') AND(sMaterialNo NOT LIKE '%/%') AND(sMaterialNo NOT LIKE '%-%')  AND(sMaterialNo NOT LIKE '% %') ";

            if (!string.IsNullOrEmpty(textsMaterialNo))
            {
                sql += " and   sMaterialNo like  '%" + textsMaterialNo + "%'";
            }
            //if (!string.IsNullOrEmpty(textbAnalyseFinish))
            //{
            //    sql += "  and  bAnalyseFinish =  " + textbAnalyseFinish;
            //}
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
                MessageBox.Show(ee.Message, "数据库操作失败", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void finish_Click(object sender, EventArgs e)
        {
            string smaterialNoFinish;
            string execSql;
            //string[] str = new string[dataGridView1.Rows.Count];

            DataTable dt = (DataTable)this.dataGridView1.DataSource;
            if (dt != null)
            {
                string a = dt.Rows[0]["bAnalyseFinish"].ToString();
                smaterialNoFinish = dt.Rows[0]["sMaterialNo"].ToString();
                //string c = dataGridView1[1, 1].ToString();
                //bool b = dataGridView1.Rows[1].Selected;
                if (a == "True")
                {

                    execSql = "  DECLARE @name VARCHAR(200) SET @name ='" + smaterialNoFinish + "'  SELECT dbo.FN_Post('http://192.168.88.206:8088/fn/task/generate?materialNo=' + @name)";
                    try
                    {
                        SqlConnection connectionString = new SqlConnection(connStr);
                        SqlCommand cmd = new SqlCommand(execSql, connectionString);

                        connectionString.Open();
                        cmd.ExecuteNonQuery();   //完成

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
                        connectionString.Close();
                    }
                    catch (Exception ee)
                    {

                        {
                            MessageBox.Show(ee.Message, "数据库操作失败", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("请选择是否完成！");
                }
            }
            else
            {
                MessageBox.Show("请选择需要生成的物料！");
            }


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void quit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
