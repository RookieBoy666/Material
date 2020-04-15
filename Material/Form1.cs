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

            string sql = "SELECT sMaterialNo as 物料编码,ISNULL(bAnalyseFinish,0)   AS   是否完成,tUpdateTime 更新时间 FROM dbo.mmMaterial where 1=1  AND  (sMaterialNo NOT LIKE '%(%') AND(sMaterialNo NOT LIKE '%/%') AND(sMaterialNo NOT LIKE '%-%')  AND(sMaterialNo NOT LIKE '% %') ";

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
                MessageBox.Show(ee.Message, "生成失败", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                int ccc = dataGridView1.CurrentCell.RowIndex;

                string a = dt.Rows[ccc]["是否完成"].ToString();
                smaterialNoFinish = dt.Rows[ccc]["物料编码"].ToString();
                //bool b = dataGridView1.Rows[1].Selected;
                //string c = dataGridView1[1, 1].ToString();


                //检验是否是 默认完成
                string sqlCheck = "select  ISNULL(bAnalyseFinish,0)   AS bAnalyseFinish from mmMaterial where sMaterialno= '" + smaterialNoFinish + "'";
                string tmp="";
                try
                {
                    SqlConnection connectionString = new SqlConnection(connStr);
                    SqlCommand cmd = new SqlCommand(sqlCheck, connectionString);
                    connectionString.Open();
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sqlCheck, connectionString);
                    da.Fill(ds);
                     tmp = ds.Tables[0].Rows[0]["bAnalyseFinish"].ToString();
                    connectionString.Close();
                }
                catch (Exception ee)
                {

                    {
                        MessageBox.Show(ee.Message, "生成失败", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }



                if (a == "True" && tmp == "False")    //手动选中 并且数据库没选中
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
                        }
                    }
                    MessageBox.Show("生成成功！");
                }
                else if (a == "False" && tmp == "False")
                {
                    MessageBox.Show("请选择是否完成！");
                }
                else if (a == "True" && tmp == "True")
                {
                    MessageBox.Show("此物料已生成，不允许生成！");
                }

                else
                {
                    MessageBox.Show("生成失败！");
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
