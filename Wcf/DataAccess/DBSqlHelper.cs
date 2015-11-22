using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public  class DBSqlHelper
    {
        /// <summary>
        /// 配置的Sql连接字符串
        /// </summary>
        public string conStr = "Server=SampleLife;database=WcfDemo;uid=sa;pwd=samplelife";// ConfigurationManager.AppSettings["wcf"].ToString();

        #region 执行连接数据库ExecuteScalar()方法 + public object ExecuteScalar(string sql, params SqlParameter[] parameters)
        /// <summary>
        /// 执行连接数据库ExecuteScalar()方法 
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">sql中的参数</param>
        /// <returns>返回第一行第一列数据</returns>
        public  object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            object obj = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(conStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddRange(parameters);
                    obj = cmd.ExecuteScalar();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                //_log.Error("DBSqlHelper.cs object ExecuteScalar(string sql, params SqlParameter[] parameters) Error! \r\n Message:\r\n{0}, \r\n StackTrace:\r\n{1}",ex.Message, ex.StackTrace);
                return null;
            }
        }
        #endregion
        public int ExecuteSql(string sql, params SqlParameter[] parameters)
        {
            int obj ;
            try
            {
                using (SqlConnection conn = new SqlConnection(conStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddRange(parameters);
                    obj = cmd.ExecuteNonQuery();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                //_log.Error("DBSqlHelper.cs object ExecuteScalar(string sql, params SqlParameter[] parameters) Error! \r\n Message:\r\n{0}, \r\n StackTrace:\r\n{1}",ex.Message, ex.StackTrace);
                return -1;
            }
        }

        #region 执行连接数据库ExecuteDataTable()方法 +  public DataTableCollection ExecuteDataTable(string sql, params SqlParameter[] parameters)
        /// <summary>
        /// 执行连接数据库ExecuteDataTable()方法 
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">sql中的参数</param>
        /// <returns>返回表数据</returns>
        public  DataTable ExecuteDataTable(string sql, params SqlParameter[] parameters)
        {
            DataTable table = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(conStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddRange(parameters);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    dataAdapter.Fill(table);
                    return table;
                }
            }
            catch (Exception ex)
            {
                //Public.PubHelper.DeleteLog();
               // _log.Error("Error! \r\n Message:\r\n{0}, \r\n StackTrace:\r\n{1}",ex.Message + sql, ex.StackTrace);
                return null;
            }
        }
        #endregion

        #region 执行连接数据库ExecuteDataSet()方法 +  public DataTableCollection ExecuteDataSet(string sql, params SqlParameter[] parameters)
        /// <summary>
        /// 执行连接数据库ExecuteDataSet()方法 
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">sql中的参数</param>
        /// <returns>返回表数据</returns>
        public  DataSet ExecuteDataSet(string sql, params SqlParameter[] parameters)
        {
            DataSet set = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(conStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddRange(parameters);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    dataAdapter.Fill(set);
                    return set;
                }
            }
            catch (Exception ex)
            {
               // _log.Error("Error! \r\n Message:\r\n{0}, \r\n StackTrace:\r\n{1}",ex.Message + "\r\n" + sql, ex.StackTrace);
                return null;
            }
        }
        #endregion

        #region 执行连接数据库存储过程ExecuteProcDataSet()方法 + public DataSet ExecuteProcDataSet(string procName, params SqlParameter[] parameters)
        /// <summary>
        /// 执行连接数据库存储过程ExecuteProcDataSet()方法 
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回表数据</returns>
        public  DataSet ExecuteProcDataSet(string procName, params SqlParameter[] parameters)
        {
            DataSet set = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(conStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(procName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(parameters);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    dataAdapter.Fill(set);
                    return set;
                }
            }
            catch (Exception ex)
            {
               // _log.Error("Error! \r\n Message:\r\n{0}, \r\n StackTrace:\r\n{1}",ex.Message + "\r\n" + procName, ex.StackTrace);
                return null;
            }

        }
        #endregion

        #region 执行连接数据库存储过程ExecuteProcScalar()方法+ public object ExecuteProcScalar(string procName, params SqlParameter[] parameters)
        /// <summary>
        /// 执行连接数据库存储过程ExecuteProcScalar()方法 
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回表数据</returns>
        public  object ExecuteProcScalar(string procName, params SqlParameter[] parameters)
        {
            object obj = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(conStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(procName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(parameters);
                    obj = cmd.ExecuteScalar();
                    return obj;
                }
            }
            catch (Exception ex)
            {
               // _log.Error("DBSqlHelper.cs object ExecuteScalar(string sql, params SqlParameter[] parameters) Error! \r\n Message:\r\n{0}, \r\n StackTrace:\r\n{1}",ex.Message, ex.StackTrace);
                return null;
            }
        }
        #endregion

        #region 执行连接数据库存储过程ExecuteProcDataTable()方法 +  public DataTable ExecuteProcDataTable(string procName, params SqlParameter[] parameters)
        /// <summary>
        /// 执行连接数据库存储过程ExecuteProcDataTable()方法 
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回表数据</returns>
        public  DataTable ExecuteProcDataTable(string procName, params SqlParameter[] parameters)
        {
            DataTable table = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(conStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(procName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(parameters);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    dataAdapter.Fill(table);
                    return table;
                }
            }
            catch (Exception ex)
            {
              //  _log.Error("Error! \r\n Message:\r\n{0}, \r\n StackTrace:\r\n{1}",ex.Message + "\r\n" + procName, ex.StackTrace);
                return null;
            }
        }
        #endregion
    }
}
