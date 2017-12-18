using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;

namespace DailyNotes
{
    public class SqlHelper
    {
        private static readonly string ConnectionString = BaseClass.DecodeCode(ConfigurationManager.AppSettings["sqlStr"]);//获取解密后的数据库连接字符串

        /// <summary>执行不带参数的增删改SQL语句或存储过程</summary>
        /// <param name="cmdText">增删改SQL语句或存储过程</param>
        /// <param name="ct">命令类型</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(string cmdText, CommandType ct)
        {
            int res = 0;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                    {
                        cmd.CommandType = ct;
                        res = cmd.ExecuteNonQuery();
                    }
                }
                catch
                {

                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            return res;
        }

        /// <summary>执行带参数的增删改SQL语句或存储过程</summary>
        /// <param name="cmdText">增删改SQL语句或存储过程</param>
        /// <param name="paras"></param>
        /// <param name="ct">命令类型</param>
        /// <exception cref="DbException"></exception>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(string cmdText, SqlParameter[] paras, CommandType ct)
        {
            int res = 0;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                    {
                        cmd.CommandType = ct;
                        cmd.Parameters.AddRange(paras);
                        res = cmd.ExecuteNonQuery();
                    }
                }
                catch
                {
                    // ignored
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            return res;
        }

        /// <summary> 执行带参数的查询SQL语句或存储过程</summary>
        /// <param name="cmdText">查询SQL语句或存储过程ì</param>
        /// <param name="ct">命令类型</param>
        /// <returns>DataTable对象</returns>
        public static DataTable ExecuteQuery(string cmdText, CommandType ct)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                    {
                        cmd.CommandType = ct;
                        using (SqlDataReader sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            dt.Load(sdr);
                        }
                    }
                }
                catch
                {
                    // ignored
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            return dt;
        }

        /// <summary> 执行带参数的查询SQL语句或存储过程</summary>
        /// <param name="cmdText">查询SQL语句或存储过程ì</param>
        /// <param name="paras">参数集合?</param>
        /// <param name="ct">命令类型</param>
        /// <returns>DataTable对象</returns>
        public static DataTable ExecuteQuery(string cmdText, SqlParameter[] paras, CommandType ct)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                    {
                        cmd.CommandType = ct;
                        cmd.Parameters.AddRange(paras);
                        using (SqlDataReader sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            dt.Load(sdr);
                        }
                    }
                }
                catch
                {
                    // ignored
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            return dt;
        }
    }
}