using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DailyNotes
{
    public partial class 事务回滚和批量插入 : System.Web.UI.Page
    {
        private static string ConnStr => ConfigurationManager.ConnectionStrings["Test"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            DataTable dtTemp = new DataTable();
            DataColumn[] columns = new DataColumn[]
            {
                new DataColumn("Name",typeof(string)),
                new DataColumn("Age",typeof(Int32)),
                new DataColumn("Sex",typeof(string)),
                new DataColumn("test",typeof(string)),
            };
            dtTemp.Columns.AddRange(columns);
            //构造一个10000行的DataTable
            for (var i = 0; i < 100 * 100; i++)
            {
                var n_row = dtTemp.NewRow();
                var tt = i + 1;
                n_row["Name"] = "Name" + tt;
                n_row["Age"] = tt;
                n_row["Sex"] = "Sex" + tt;
                n_row["test"] = "test" + tt;
                dtTemp.Rows.Add(n_row);
            }
            stopWatch.Stop();
            Response.Write("构造一万行的Datatable所需时间：" + stopWatch.Elapsed);

            Stopwatch stopwatcj = new Stopwatch();
            stopwatcj.Start();
            DtDrTable(dtTemp, "t_student");
            stopwatcj.Stop();
            Response.Write("10000行数据批量插入表中所需时间：" + stopwatcj.Elapsed);
            GetMsgBySJ();
        }
        //测试事务回滚
        public void GetMsgBySJ()
        {
            SqlConnection conn = new SqlConnection(ConnStr);
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();//打开之后开启事务
            SqlTransaction tran = conn.BeginTransaction();//开启事务
            cmd.Transaction = tran;//将事务应用于CMD
            try
            {
                cmd.CommandText = " INSERT into t_student VALUES ('huage1','11','男神') ";
                cmd.ExecuteNonQuery();
                cmd.CommandText = " INSERT into t_student VALUES ('huage','11','女神','') ";
                cmd.ExecuteNonQuery();
                tran.Commit();//提交事务（不提交不会回滚错误）
                Page.ClientScript.RegisterStartupScript(GetType(), "key1", "alert('插入成功!');", true);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                Page.ClientScript.RegisterStartupScript(GetType(), "key1", "alert(' 插入失败，事物回滚!');", true);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                tran.Dispose();
            }
        }
        //批量将数据导入目的表
        public void DtDrTable(DataTable dt, string tableName)
        {
            try
            {
                //这边可以使用事务回滚机制
                SqlBulkCopy bcp = new SqlBulkCopy(ConnStr, SqlBulkCopyOptions.UseInternalTransaction);
                //指定目标数据库的表名
                bcp.DestinationTableName = tableName;
                //每一批次的行数
                bcp.BatchSize = 100 * 100;
                //建立数据源表字段和目标表中的列之间的映射
                //----既然dt的列名需要与表明完全一致，直接循环dt的列即可----//
                foreach (DataColumn dc in dt.Columns)
                    bcp.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
                //写入数据库表 dt 是数据源DataTable
                bcp.WriteToServer(dt);
                //关闭SqlBulkCopy实例
                bcp.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //批量将数据导入目的表可回滚
        public void TranBatchImportData(DataTable dt, string tableName)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                using (SqlBulkCopy sqlBC = new SqlBulkCopy(conn, SqlBulkCopyOptions.UseInternalTransaction, tran))
                {
                    sqlBC.BatchSize = 100 * 100;
                    //sqlBC.BulkCopyTimeout = 60;//超时之前操作完成所允许的秒数
                    sqlBC.DestinationTableName = tableName;
                    foreach (DataColumn dc in dt.Columns)
                        sqlBC.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);

                    sqlBC.WriteToServer(dt);
                    tran.Commit();
                }
            }
        }
    }
}