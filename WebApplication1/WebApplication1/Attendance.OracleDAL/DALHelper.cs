/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： DALHelper.cs
 * 檔功能描述： 數據訪問層幫助類
 * 
 * 版本：1.0
 * 創建標識： Lucky Lee 2011.09.20
 * 
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Text;
using GDSBG.MiABU.Attendance.Common;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.OracleDAL
{
    /// <summary>
    /// 數據訪問層幫助類
    /// </summary>
    /// <typeparam name="T">要操作的Model類型</typeparam>
    public class DALHelper<T> where T : new()
    {
        private ORMHelper<T> ormHelper = new ORMHelper<T>();
        internal ORMHelper<T> OrmHelper { get { return ormHelper; } }
        string selectFormat = "SELECT {0} FROM {1} WHERE 1=1 {2}";
        string updateFormat = "UPDATE {0} SET {1} WHERE 1=1 {2}";
        string insertFormat = "INSERT INTO {0} ({1}) VALUES ({2})";
        string deleteFormat = "DELETE FROM {0} WHERE 1=1 {1}";
        private string connecString;
        private OracleConnection _Connection;

        #region 構造函數
        /// <summary>
        /// 構造函數
        /// </summary>
        /// <param name="connectionString">連接字串</param>
        public DALHelper(string connectionString)
        {
            if (_Connection != null)
            {
                connecString = connectionString;
                _Connection.ConnectionString = connectionString;
            }
            else
            {
                connecString = connectionString;
                _Connection = new OracleConnection(connectionString);
            }
        }
        #endregion

        #region 設置連接字串
        /// <summary>
        /// 設置連接字串
        /// </summary>
        /// <param name="connectionString"></param>
        public void SetConnectionString(string connectionString)
        {
            connecString = connectionString;
            _Connection.ConnectionString = connectionString;
        }
        #endregion

        #region 數據庫連接
        /// <summary>
        /// 數據庫連接
        /// </summary>
        public OracleConnection Connection
        {
            get
            {
                _Connection.ConnectionString = connecString;
                return _Connection;
            }
        }
        #endregion

        #region 根據Model組織條件參數
        /// <summary>
        /// 根據Model組織條件參數
        /// </summary>
        /// <param name="model">Model</param>
        /// <param name="isFuzzy">模糊匹配</param>
        /// <param name="tableAlias">查詢主表別名</param>
        /// <param name="conditionClause">條件語句</param>
        /// <returns>參數集</returns>
        public List<OracleParameter> CreateConditionParameters(T model, bool isFuzzy, string tableAlias, out string conditionClause)
        {
            string tableName = string.IsNullOrEmpty(tableAlias) ? "" : tableAlias + ".";
            StringBuilder condition = new StringBuilder();
            List<OracleParameter> paraList = new List<OracleParameter>();
            Dictionary<string, string> dicMapCols = ormHelper.PropertyColumns;
            Dictionary<string, object> dicMapValues = ormHelper.GetModelMapValues(model);
            //組織Where條件語句
            foreach (KeyValuePair<string, string> mapCol in dicMapCols)
            {
                if (dicMapValues[mapCol.Key] != null && !string.IsNullOrEmpty(dicMapValues[mapCol.Key].ToString()))
                {
                    if (isFuzzy)
                    {
                        condition.AppendFormat("AND UPPER({0}{1}) LIKE '%'||UPPER(:a_{2})||'%' ", tableName, mapCol.Value, mapCol.Key);
                    }
                    else
                    {
                        condition.AppendFormat("AND {0}{1} = :a_{2} ", tableName, mapCol.Value, mapCol.Key);
                    }
                    OracleParameter para = new OracleParameter(":a_" + mapCol.Key, dicMapValues[mapCol.Key]);
                    para.Direction = ParameterDirection.Input;
                    paraList.Add(para);
                }
            }
            conditionClause = condition.ToString();
            return paraList;
        }
        #endregion

        #region 根據Model創建查詢數據Command對象
        /// <summary>
        /// 根據Model創建查詢數據的Command對象
        /// </summary>
        /// <param name="model">model實體</param>
        /// <param name="isprimary">是否只根據主鍵查詢</param>
        /// <param name="isFuzzy">是否模糊匹配，為true時不區分大小寫并以Like方式查詢</param>
        /// <param name="orderByString">需排序的欄位字串：Column_A,Column_B DESC,..Column_N ASC</param>
        /// <returns>Command對象</returns>
        private OracleCommand CreateSelectCommand(T model, bool isprimary, bool isFuzzy, string orderByString)
        {
            OracleCommand cmd = Connection.CreateCommand();
            cmd.CommandType = CommandType.Text;

            StringBuilder condition = new StringBuilder();

            Dictionary<string, string> dicMapCols = isprimary ? ormHelper.PropertyPrimaryKeys : ormHelper.PropertyColumns;
            Dictionary<string, object> dicMapValues = ormHelper.GetModelMapValues(model);
            //組織Where條件語句
            foreach (KeyValuePair<string, string> mapCol in dicMapCols)
            {
                if (dicMapValues[mapCol.Key] != null && !string.IsNullOrEmpty(dicMapValues[mapCol.Key].ToString()))
                {
                    if (isFuzzy)
                    {
                        condition.AppendFormat("AND UPPER({0}) LIKE '%'||UPPER(:a_{1})||'%' ", mapCol.Value, mapCol.Key);
                    }
                    else
                    {
                        condition.AppendFormat("AND {0} = :a_{1} ", mapCol.Value, mapCol.Key);
                    }
                    OracleParameter para = new OracleParameter(":a_" + mapCol.Key, dicMapValues[mapCol.Key]);
                    para.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(para);
                }
                else if (isprimary)
                {
                    throw new Exception("The Primary Property Value Of The Model " + typeof(T).Name + " Can't Be Null!");
                }
            }
            //組織查詢語句
            if (string.IsNullOrEmpty(orderByString))
            {
                cmd.CommandText = string.Format(selectFormat, "ROWNUM ROW_NUM," + ormHelper.SelectColumnsString, ormHelper.SelectTableName, condition.ToString());
            }
            else
            {
                condition.Append("ORDER BY ").Append(orderByString.TrimEnd(','));
                cmd.CommandText = "SELECT ROWNUM ROW_NUM, A.* FROM (" +
                    string.Format(selectFormat, ormHelper.SelectColumnsString, ormHelper.SelectTableName, condition.ToString()) + ") A";
            }
            return cmd;
        }
        #endregion

        #region 根據Model創建新增數據的Command對象
        /// <summary>
        /// 根據Model創建新增數據的Command對象
        /// </summary>
        /// <param name="model">model實體</param>
        /// <returns>Command對象</returns>
        private OracleCommand CreateInsertCommand(T model)
        {
            OracleCommand cmd = Connection.CreateCommand();
            cmd.CommandType = CommandType.Text;

            StringBuilder insertCols = new StringBuilder();
            StringBuilder values = new StringBuilder();

            Dictionary<string, string> dicMapCols = ormHelper.PropertyColumns;
            Dictionary<string, object> dicMapValues = ormHelper.GetModelMapValues(model);
            string insertColsStr = "," + ormHelper.EditableColumnsString + ",";

            foreach (KeyValuePair<string, string> mapCol in dicMapCols)
            {
                if (insertColsStr.Contains("," + mapCol.Value + ",") && dicMapValues[mapCol.Key] != null)
                {
                    insertCols.Append(mapCol.Value).Append(",");
                    values.Append(":a_").Append(mapCol.Key).Append(",");
                    OracleParameter para = new OracleParameter(":a_" + mapCol.Key, dicMapValues[mapCol.Key]);
                    para.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(para);
                }
            }
            cmd.CommandText = string.Format(insertFormat, ormHelper.TableName, insertCols.ToString().TrimEnd(','), values.ToString().TrimEnd(','));
            return cmd;
        }
        #endregion

        #region 根據Model創建更改數據Command對象
        /// <summary>
        /// 根據Model創建查詢數據的Command對象
        /// </summary>
        /// <param name="oldModel">原model實體</param>
        /// <param name="newModel">要修改成的Model實體</param>
        /// <param name="ignoreNull">是否忽略Null值,即Model中值為Null時是否也將數據庫相應欄位更新為Null,true:不更新為Null,false:更新為Null</param>
        /// <returns>Command對象</returns>
        private OracleCommand CreateUpdateCommand(T oldModel, T newModel, bool ignoreNull)
        {
            OracleCommand cmd = Connection.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string updateColsStr = "," + ormHelper.EditableColumnsString + ",";
            StringBuilder condition = new StringBuilder();
            StringBuilder setvalue = new StringBuilder();

            Dictionary<string, string> dicMapCols = ormHelper.PropertyColumns;
            Dictionary<string, object> dicMapNewValues = ormHelper.GetModelMapValues(newModel);
            Dictionary<string, object> dicMapOldValues = ormHelper.GetModelMapValues(oldModel);

            foreach (KeyValuePair<string, string> mapCol in dicMapCols)
            {
                if (updateColsStr.Contains("," + mapCol.Value + ","))
                {
                    if (dicMapNewValues[mapCol.Key] != null && !string.IsNullOrEmpty(dicMapNewValues[mapCol.Key].ToString()))
                    {
                        setvalue.AppendFormat("{0} = :a_{1},", mapCol.Value, mapCol.Key);
                        OracleParameter para = new OracleParameter(":a_" + mapCol.Key, dicMapNewValues[mapCol.Key]);
                        para.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(para);
                    }
                    else if (!ignoreNull)
                    {
                        setvalue.AppendFormat("{0} = NULL,", mapCol.Value);
                    }

                    if (dicMapOldValues[mapCol.Key] != null && !string.IsNullOrEmpty(dicMapOldValues[mapCol.Key].ToString()))
                    {
                        condition.AppendFormat("AND {0} = :o_{1} ", mapCol.Value, mapCol.Key);
                        OracleParameter para = new OracleParameter(":o_" + mapCol.Key, dicMapOldValues[mapCol.Key]);
                        para.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(para);
                    }
                }
            }
            cmd.CommandText = string.Format(updateFormat, ormHelper.TableName, setvalue.ToString().TrimEnd(','), condition.ToString());
            return cmd;
        }
        #endregion

        #region 根據Model創建根據主鍵更改數據的Command對象
        /// <summary>
        /// 根據Model創建根據主鍵更改數據的Command對象(更新前后主鍵不變)
        /// </summary>
        /// <param name="model">model實體</param>
        /// <param name="ignoreNull">是否忽略Null值,即Model中值為Null時是否也將數據庫相應欄位更新為Null,true:不更新為Null,false:更新為Null</param>
        /// <returns>Command對象</returns>
        private OracleCommand CreateUpdateCommand(T model, bool ignoreNull)
        {
            OracleCommand cmd = Connection.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string updateColsStr = "," + ormHelper.EditableColumnsString + ",";
            StringBuilder condition = new StringBuilder();
            StringBuilder setvalue = new StringBuilder();

            Dictionary<string, string> dicMapPrimarys = ormHelper.PropertyPrimaryKeys;
            Dictionary<string, string> dicMapCols = ormHelper.PropertyColumns;
            Dictionary<string, object> dicMapValues = ormHelper.GetModelMapValues(model);

            foreach (KeyValuePair<string, string> mapCol in dicMapCols)
            {
                if (updateColsStr.Contains("," + mapCol.Value + ","))
                {
                    if (dicMapValues[mapCol.Key] != null)
                    {
                        setvalue.AppendFormat("{0} = :a_{1},", mapCol.Value, mapCol.Key);
                        OracleParameter para = new OracleParameter(":a_" + mapCol.Key, dicMapValues[mapCol.Key]);
                        para.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(para);
                    }
                    else if (!ignoreNull)
                    {
                        setvalue.AppendFormat("{0} = NULL,", mapCol.Value);
                    }
                }
            }

            foreach (KeyValuePair<string, string> mapCol in dicMapPrimarys)
            {
                if (dicMapValues[mapCol.Key] != null && !string.IsNullOrEmpty(dicMapValues[mapCol.Key].ToString()))
                {
                    condition.AppendFormat("AND {0} = :p_{1} ", mapCol.Value, mapCol.Key);
                    OracleParameter para = new OracleParameter(":p_" + mapCol.Key, dicMapValues[mapCol.Key]);
                    para.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(para);
                }
                else
                {
                    throw new Exception("The Primary Property Value Of The Model " + typeof(T).Name + " Can't Be Null!");
                }
            }
            cmd.CommandText = string.Format(updateFormat, ormHelper.TableName, setvalue.ToString().TrimEnd(','), condition.ToString());
            return cmd;
        }
        #endregion

        #region 根據Model創建刪除數據的Command對象
        /// <summary>
        /// 根據Model創建刪除數據的Command對象
        /// </summary>
        /// <param name="model">model實體</param>
        /// <returns>Command對象</returns>
        private OracleCommand CreateDeleteCommand(T model)
        {
            OracleCommand cmd = Connection.CreateCommand();
            cmd.CommandType = CommandType.Text;

            StringBuilder condition = new StringBuilder();

            Dictionary<string, string> dicMapCols = ormHelper.PropertyColumns;
            Dictionary<string, object> dicMapValues = ormHelper.GetModelMapValues(model);
            string conditionColsStr = "," + ormHelper.EditableColumnsString + ",";

            foreach (KeyValuePair<string, string> mapCol in dicMapCols)
            {
                if (conditionColsStr.Contains("," + mapCol.Value + ",") && dicMapValues[mapCol.Key] != null && !string.IsNullOrEmpty(dicMapValues[mapCol.Key].ToString()))
                {
                    condition.AppendFormat("AND {0} = :a_{1} ", mapCol.Value, mapCol.Key);
                    OracleParameter para = new OracleParameter(":a_" + mapCol.Key, dicMapValues[mapCol.Key]);
                    para.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(para);
                }
            }
            cmd.CommandText = string.Format(deleteFormat, ormHelper.TableName, condition.ToString());
            return cmd;
        }
        #endregion

        #region 執行非查詢命令
        /// <summary>
        /// 執行非查詢命令
        /// </summary>
        /// <param name="cmd">要執行的命令</param>
        /// <returns>返回影響記錄條數，異常返回-1</returns>
        public int ExecuteNonQuery(OracleCommand cmd)
        {
            try
            {
                using (cmd.Connection)
                {
                    if (cmd.Connection.State != ConnectionState.Open)
                    {
                        cmd.Connection.Open();
                    }
                    return cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                return -1;
            }
        }
        #endregion

        #region 執行非查詢命令(帶插入系統日誌)
        /// <summary>
        /// 執行非查詢命令(帶插入系統日誌)
        /// </summary>
        /// <param name="cmd">要執行的命令</param>
        /// <param name="model">系統日誌數據模型</param>
        /// <returns>返回影響記錄條數，異常返回-1</returns>
        public int ExecuteNonQuery(OracleCommand cmd, SynclogModel model)
        {
            InsertLog(cmd, model);
            cmd.Connection = Connection;
            try
            {
                using (cmd.Connection)
                {
                    if (cmd.Connection.State != ConnectionState.Open)
                    {
                        cmd.Connection.Open();
                    }
                    return cmd.ExecuteNonQuery();
                    
                }
            }
            catch
            {
                return -1;
            }
        }
        #endregion

        #region 執行非查詢命令
        /// <summary>
        /// 執行非查詢命令
        /// </summary>
        /// <param name="cmd">要執行的命令</param>
        /// <returns>返回影響記錄條數，異常返回-1</returns>
        public int ExecuteNonQueryReader(OracleCommand cmd)
        {
            try
            {
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }
                return cmd.ExecuteNonQuery();
            }
            catch
            {
                return -1;
            }
        }
        #endregion

        #region 執行查詢命令
        /// <summary>
        /// 執行查詢命令
        /// </summary>
        /// <param name="cmd">要執行的查詢命令</param>
        /// <returns>返回查詢數據集合，異常返回null</returns>
        public DataTable ExecuteQuery(OracleCommand cmd)
        {
            try
            {
                using (cmd.Connection)
                {
                    OracleDataAdapter adapt = new OracleDataAdapter(cmd);
                    DataTable dtData = new DataTable();
                    adapt.Fill(dtData);
                    return dtData;
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 查詢所有
        /// <summary>
        /// 取得所有Model
        /// </summary>
        /// <returns>所有Model的集合</returns>
        public DataTable SelectAll()
        {
            return Select(new T());
        }
        #endregion

        #region 查詢所有(排序）
        /// <summary>
        /// 取得所有Model
        /// </summary>
        /// <param name="orderByString">需排序的欄位字串：Column_A,Column_B DESC,..Column_N ASC</param>
        /// <returns>所有Model的集合</returns>
        public DataTable SelectAll(string orderByString)
        {
            return Select(new T(), orderByString);
        }
        #endregion

        #region 根據Model條件查詢記錄數
        /// <summary>
        /// 根據Model條件查詢記錄數
        /// </summary>
        /// <param name="model">條件Model</param>
        /// <param name="isFuzzy">是否模糊匹配，為true時不區分大小寫并以Like方式查詢</param>
        /// <returns>記錄數</returns>
        public int GetCount(T model)
        {
            return GetCount(model, false);
        }
        #endregion

        #region 根據Model條件查詢記錄數
        /// <summary>
        /// 根據Model條件查詢記錄數
        /// </summary>
        /// <param name="model">條件Model</param>
        /// <param name="isFuzzy">是否模糊匹配，為true時不區分大小寫并以Like方式查詢</param>
        /// <returns>記錄數</returns>
        public int GetCount(T model, bool isFuzzy)
        {
            OracleCommand cmd = Connection.CreateCommand();
            cmd.CommandType = CommandType.Text;

            StringBuilder condition = new StringBuilder();

            Dictionary<string, string> dicMapCols = ormHelper.PropertyColumns;
            Dictionary<string, object> dicMapValues = ormHelper.GetModelMapValues(model);
            //組織Where條件語句
            foreach (KeyValuePair<string, string> mapCol in dicMapCols)
            {
                if (dicMapValues[mapCol.Key] != null)
                {
                    object mapValue = null;
                    if (isFuzzy)
                    {
                        condition.AppendFormat("AND UPPER({0}) LIKE '%'||UPPER(:a_{1})||'%' ", mapCol.Value, mapCol.Key);
                    }
                    else
                    {
                        condition.AppendFormat("AND {0} = :a_{1} ", mapCol.Value, mapCol.Key);
                    }
                    mapValue = dicMapValues[mapCol.Key];
                    OracleParameter para = new OracleParameter(":a_" + mapCol.Key, mapValue);
                    para.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(para);
                }
            }
            cmd.CommandText = string.Format(selectFormat, "COUNT(1)", ormHelper.SelectTableName, condition.ToString());
            try
            {
                using (cmd.Connection)
                {
                    if (cmd.Connection.State != ConnectionState.Open)
                    {
                        cmd.Connection.Open();
                    }
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch
            {
                return -1;
            }
        }
        #endregion

        #region 根據Model值查詢
        /// <summary>
        /// 根據Model值查詢
        /// </summary>
        /// <param name="model">model實體</param>
        /// <returns>符合條件之實體集合</returns>
        public DataTable Select(T model)
        {
            return Select(model, false);
        }
        #endregion

        #region 根據Model值查詢
        /// <summary>
        /// 根據Model值查詢
        /// </summary>
        /// <param name="model">model實體</param>
        /// <param name="isFuzzy">是否模糊匹配，為true時不區分大小寫并以Like方式查詢</param>
        /// <returns>符合條件之實體集合</returns>
        public DataTable Select(T model, bool isFuzzy)
        {
            return Select(model, isFuzzy, null);
        }
        #endregion

        #region 根據Model值排序查詢
        /// <summary>
        /// 根據Model值查詢
        /// </summary>
        /// <param name="model">model實體</param>
        /// <param name="orderByString">需排序的欄位字串：Column_A,Column_B DESC,..Column_N ASC</param>
        /// <returns>符合條件之實體集合</returns>
        public DataTable Select(T model, string orderByString)
        {
            return Select(model, false, orderByString);
        }
        #endregion

        #region 根據Model值排序查詢
        /// <summary>
        /// 根據Model值查詢
        /// </summary>
        /// <param name="model">model實體</param>
        /// <param name="isFuzzy">是否模糊匹配，為true時不區分大小寫并以Like方式查詢</param>
        /// <param name="orderByString">需排序的欄位字串：Column_A,Column_B DESC,..Column_N ASC</param>
        /// <returns>符合條件之實體集合</returns>
        public DataTable Select(T model, bool isFuzzy, string orderByString)
        {
            OracleCommand cmd = CreateSelectCommand(model, false, isFuzzy, orderByString);
            return ExecuteQuery(cmd);
        }
        #endregion

        #region 根據Model值分頁查詢
        /// <summary>
        /// 根據Model值分頁查詢
        /// </summary>
        /// <param name="model">model實體</param>
        /// <param name="pageIndex">頁索引</param>
        /// <param name="pageSize">頁大小</param>
        /// <returns>Model集合</returns>
        public DataTable Select(T model, int pageIndex, int pageSize)
        {
            return Select(model, false, pageIndex, pageSize);
        }
        #endregion

        #region 根據Model值分頁查詢
        /// <summary>
        /// 根據Model值分頁查詢
        /// </summary>
        /// <param name="model">model實體</param>
        /// <param name="isFuzzy">是否模糊匹配，為true時不區分大小寫并以Like方式查詢</param>
        /// <param name="pageIndex">頁索引</param>
        /// <param name="pageSize">頁大小</param>
        /// <returns>Model集合</returns>
        public DataTable Select(T model, bool isFuzzy, int pageIndex, int pageSize)
        {
            return Select(model, isFuzzy, null, pageIndex, pageSize);
        }
        #endregion

        #region 根據Model值分頁排序查詢
        /// <summary>
        /// 根據Model值分頁查詢
        /// </summary>
        /// <param name="model">model實體</param>
        /// <param name="orderByString">需排序的欄位字串：Column_A,Column_B DESC,..Column_N ASC</param>
        /// <param name="pageIndex">頁索引</param>
        /// <param name="pageSize">頁大小</param>
        /// <returns>Model集合</returns>
        public DataTable Select(T model, string orderByString, int pageIndex, int pageSize)
        {
            return Select(model, false, orderByString, pageIndex, pageSize);
        }
        #endregion

        #region 根據Model值分頁排序查詢
        /// <summary>
        /// 根據Model值分頁查詢
        /// </summary>
        /// <param name="model">model實體</param>
        /// <param name="isFuzzy">是否模糊匹配，為true時不區分大小寫并以Like方式查詢</param>
        /// <param name="orderByString">需排序的欄位字串：Column_A,Column_B DESC,..Column_N ASC</param>
        /// <param name="pageIndex">頁索引</param>
        /// <param name="pageSize">頁大小</param>
        /// <returns>Model集合</returns>
        public DataTable Select(T model, bool isFuzzy, string orderByString, int pageIndex, int pageSize)
        {
            int minRowIndex = (pageIndex - 1) * pageSize + 1;
            int maxRowIndex = pageSize * pageIndex;

            OracleCommand cmd = CreateSelectCommand(model, false, isFuzzy, orderByString);
            cmd.CommandText = "SELECT B.* FROM (" + cmd.CommandText + ") B WHERE ROW_NUM <= :v_MaxRowIndex AND ROW_NUM >= :v_MinRowIndex";

            OracleParameter para = new OracleParameter(":v_MaxRowIndex", maxRowIndex);
            para.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(para);

            para = new OracleParameter(":v_MinRowIndex", minRowIndex);
            para.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(para);

            return ExecuteQuery(cmd);
        }
        #endregion

        #region 根據Model值分頁查詢(返回總記錄數)
        /// <summary>
        /// 根據Model值分頁查詢(返回總記錄數)
        /// </summary>
        /// <param name="model">model實體</param>
        /// <param name="pageIndex">頁索引</param>
        /// <param name="pageSize">頁大小</param>
        /// <returns>Model集合</returns>
        public DataTable Select(T model, int pageIndex, int pageSize, out int totalCount)
        {
            return Select(model, false, pageIndex, pageSize, out totalCount);
        }
        #endregion

        #region 根據Model值分頁查詢(返回總記錄數)
        /// <summary>
        /// 根據Model值分頁查詢(返回總記錄數)
        /// </summary>
        /// <param name="model">model實體</param>
        /// <param name="isFuzzy">是否模糊匹配，為true時不區分大小寫并以Like方式查詢</param>
        /// <param name="pageIndex">頁索引</param>
        /// <param name="pageSize">頁大小</param>
        /// <returns>Model集合</returns>
        public DataTable Select(T model, bool isFuzzy, int pageIndex, int pageSize, out int totalCount)
        {
            return Select(model, isFuzzy, null, pageIndex, pageSize, out totalCount);
        }
        #endregion

        #region 根據Model值分頁排序查詢(返回總記錄數)
        /// <summary>
        /// 根據Model值分頁查詢(返回總記錄數)
        /// </summary>
        /// <param name="model">model實體</param>
        /// <param name="orderByString">需排序的欄位字串：Column_A,Column_B DESC,..Column_N ASC</param>
        /// <param name="pageIndex">頁索引</param>
        /// <param name="pageSize">頁大小</param>
        /// <param name="totalCount">總記錄數</param>
        /// <returns>Model集合</returns>
        public DataTable Select(T model, string orderByString, int pageIndex, int pageSize, out int totalCount)
        {
            return Select(model, false, orderByString, pageIndex, pageSize, out totalCount);
        }
        #endregion

        #region 根據Model值分頁排序查詢(返回總記錄數)
        /// <summary>
        /// 根據Model值分頁查詢(返回總記錄數)
        /// </summary>
        /// <param name="model">model實體</param>
        /// <param name="isFuzzy">是否模糊匹配，為true時不區分大小寫并以Like方式查詢</param>
        /// <param name="orderByString">需排序的欄位字串：Column_A,Column_B DESC,..Column_N ASC</param>
        /// <param name="pageIndex">頁索引</param>
        /// <param name="pageSize">頁大小</param>
        /// <param name="totalCount">總記錄數</param>
        /// <returns>Model集合</returns>
        public DataTable Select(T model, bool isFuzzy, string orderByString, int pageIndex, int pageSize, out int totalCount)
        {
            OracleCommand cmd = CreateSelectCommand(model, false, isFuzzy, orderByString);
            string cmdCount = string.IsNullOrEmpty(orderByString) ? cmd.CommandText :
                cmd.CommandText.Substring(cmd.CommandText.IndexOf("SELECT", 6), cmd.CommandText.LastIndexOf("ORDER BY ") - cmd.CommandText.IndexOf("SELECT", 6));
            int fromIdx = cmdCount.IndexOf(" FROM ") + 1;
            cmdCount = "SELECT COUNT(1) " + cmdCount.Substring(fromIdx);

            int minRowIndex = (pageIndex - 1) * pageSize + 1;
            int maxRowIndex = pageSize * pageIndex;
            string commandText = "SELECT B.* FROM (" + cmd.CommandText + ") B WHERE ROW_NUM <= :v_MaxRowIndex AND ROW_NUM >= :v_MinRowIndex";

            cmd.CommandText = cmdCount;
            try
            {
                using (cmd.Connection)
                {
                    if (cmd.Connection.State != ConnectionState.Open)
                    {
                        cmd.Connection.Open();
                    }
                    totalCount = Convert.ToInt32(cmd.ExecuteScalar());

                    cmd.CommandText = commandText;

                    OracleParameter para = new OracleParameter(":v_MaxRowIndex", maxRowIndex);
                    para.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(para);

                    para = new OracleParameter(":v_MinRowIndex", minRowIndex);
                    para.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(para);

                    OracleDataAdapter adapt = new OracleDataAdapter(cmd);
                    DataTable dtData = new DataTable();
                    adapt.Fill(dtData);
                    return dtData;
                }
            }
            catch
            {
                totalCount = -1;
                return null;
            }
        }
        #endregion

        #region 根據Model主鍵查詢
        /// <summary>
        /// 根據Model主鍵查詢
        /// </summary>
        /// <param name="model">model實體</param>
        /// <returns>符合主鍵值的實體</returns>
        public T SelectByKey(T model)
        {
            List<T> list = ormHelper.SetDataTableToList(SelectByKey(model, false));
            return list != null && list.Count == 1 ? list[0] : default(T);
        }
        #endregion

        #region 根據Model主鍵查詢
        /// <summary>
        /// 根據Model主鍵查詢
        /// </summary>
        /// <param name="model">model實體</param>
        /// <returns>符合主鍵值的實體</returns>
        public DataTable SelectByKey(T model, bool isFuzzy)
        {
            OracleCommand cmd = CreateSelectCommand(model, true, isFuzzy, null);
            return ExecuteQuery(cmd);
        }
        #endregion

        #region 根據Model插入一筆數據
        /// <summary>
        /// 根據Model插入一筆數據
        /// </summary>
        /// <param name="model">model實體</param>
        /// <returns>影響數據條數,異常返回-1</returns>
        public int Insert(T model)
        {
            OracleCommand cmd = CreateInsertCommand(model);
            return ExecuteNonQuery(cmd);
        }
        #endregion

        #region 根據Model插入一筆數據(帶插入系統日誌)
        /// <summary>
        /// 根據Model插入一筆數據(帶插入系統日誌)
        /// </summary>
        /// <param name="model">model實體</param>
        /// <returns>影響數據條數,異常返回-1</returns>
        public int Insert(T model, SynclogModel logmodel)
        {
            OracleCommand cmd = CreateInsertCommand(model);
            return ExecuteNonQuery(cmd,logmodel);
        }
        #endregion

        #region 根據oldModel值為條件更新為newModel
        /// <summary>
        /// 根據oldModel值為條件更新為newModel
        /// </summary>
        /// <param name="oldModel">要變更的Model</param>
        /// <param name="newModel">要變成的新Model</param>
        /// <returns>影響數據條數，異常返回-1</returns>
        public int Update(T oldModel, T newModel)
        {
            return Update(oldModel, newModel, false);
        }
        #endregion

        #region 根據oldModel值為條件更新為newModel(帶插入系統日誌)
        /// <summary>
        /// 根據oldModel值為條件更新為newModel(帶插入系統日誌)
        /// </summary>
        /// <param name="oldModel">要變更的Model</param>
        /// <param name="newModel">要變成的新Model</param>
        /// <returns>影響數據條數，異常返回-1</returns>
        public int Update(T oldModel, T newModel,SynclogModel logmodel)
        {
            return Update(oldModel, newModel, false, logmodel);
        }
        #endregion

        #region 根據oldModel值為條件更新為newModel
        /// <summary>
        /// 根據oldModel值為條件更新為newModel
        /// </summary>
        /// <param name="oldModel">要變更的Model</param>
        /// <param name="newModel">要變成的新Model</param>
        /// <param name="ignoreNull">是否忽略Null值,即Model中值為Null時是否也將數據庫相應欄位更新為Null,true:不更新為Null,false:更新為Null</param>
        /// <returns>影響數據條數，異常返回-1</returns>
        public int Update(T oldModel, T newModel, bool ignoreNull)
        {
            OracleCommand cmd = CreateUpdateCommand(oldModel, newModel, ignoreNull);
            return ExecuteNonQuery(cmd);
        }
        #endregion

        #region 根據oldModel值為條件更新為newModel(帶插入系統日誌)
        /// <summary>
        /// 根據oldModel值為條件更新為newModel(帶插入系統日誌)
        /// </summary>
        /// <param name="oldModel">要變更的Model</param>
        /// <param name="newModel">要變成的新Model</param>
        /// <param name="ignoreNull">是否忽略Null值,即Model中值為Null時是否也將數據庫相應欄位更新為Null,true:不更新為Null,false:更新為Null</param>
        /// <returns>影響數據條數，異常返回-1</returns>
        public int Update(T oldModel, T newModel, bool ignoreNull, SynclogModel logmodel)
        {
            OracleCommand cmd = CreateUpdateCommand(oldModel, newModel, ignoreNull);
            return ExecuteNonQuery(cmd,logmodel);
        }
        #endregion

        #region 根據Model主鍵值更新
        /// <summary>
        /// 根據Model主鍵值更新,更新前后主鍵一致
        /// </summary>
        /// <param name="model">要更新的Model</param>
        /// <returns>影響數據條數，異常返回-1</returns>
        public int UpdateByKey(T model)
        {
            return UpdateByKey(model, false);
        }
        #endregion

        #region 根據Model主鍵值更新(帶插入日誌)
        /// <summary>
        /// 根據Model主鍵值更新,更新前后主鍵一致(帶插入日誌)
        /// </summary>
        /// <param name="model">要更新的Model</param>
        /// <returns>影響數據條數，異常返回-1</returns>
        public int UpdateByKey(T model,SynclogModel logmodel)
        {
            return UpdateByKey(model, false, logmodel);
        }
        #endregion

        #region 根據Model主鍵值更新
        /// <summary>
        /// 根據Model主鍵值更新,更新前后主鍵一致
        /// </summary>
        /// <param name="model">要更新的Model</param>
        /// <param name="ignoreNull">是否忽略Null值,即Model中值為Null時是否也將數據庫相應欄位更新為Null,true:不更新為Null,false:更新為Null</param>
        /// <returns>影響數據條數，異常返回-1</returns>
        public int UpdateByKey(T model, bool ignoreNull)
        {
            OracleCommand cmd = CreateUpdateCommand(model, ignoreNull);
            return ExecuteNonQuery(cmd);
        }
        #endregion

        #region 根據Model主鍵值更新(帶插入日誌)
        /// <summary>
        /// 根據Model主鍵值更新,更新前后主鍵一致(帶插入日誌)
        /// </summary>
        /// <param name="model">要更新的Model</param>
        /// <param name="ignoreNull">是否忽略Null值,即Model中值為Null時是否也將數據庫相應欄位更新為Null,true:不更新為Null,false:更新為Null</param>
        /// <returns>影響數據條數，異常返回-1</returns>
        public int UpdateByKey(T model, bool ignoreNull, SynclogModel logmodel)
        {
            OracleCommand cmd = CreateUpdateCommand(model, ignoreNull);
            return ExecuteNonQuery(cmd,logmodel);
        }
        #endregion

        #region 根據model值為條件刪除數據
        /// <summary>
        /// 根據Model值為條件刪除數據
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>影響記錄條數</returns>
        public int Delete(T model)
        {
            OracleCommand cmd = CreateDeleteCommand(model);
            return ExecuteNonQuery(cmd);
        }
        #endregion

        #region 根據model值為條件刪除數據(帶插入系統日誌)
        /// <summary>
        /// 根據Model值為條件刪除數據(帶插入系統日誌)
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>影響記錄條數</returns>
        public int Delete(T model,SynclogModel logmodel)
        {
            OracleCommand cmd = CreateDeleteCommand(model);
            return ExecuteNonQuery(cmd, logmodel);
        }
        #endregion

        #region 創建OracleCommand
        /// <summary>
        /// 創建OracleCommand(默認文本類型)
        /// </summary>
        /// <param name="cmdText">命令語句</param>
        /// <param name="parameters">參數</param>
        /// <returns>OracleCommand對象</returns>
        public OracleCommand CreateCommand(string cmdText, params OracleParameter[] parameters)
        {
            return CreateCommand(cmdText, CommandType.Text, parameters);
        }

        /// <summary>
        /// 創建OracleCommand
        /// </summary>
        /// <param name="cmdText">命令語句</param>
        /// <param name="cmdType">命令類型</param>
        /// <param name="parameters">參數</param>
        /// <returns>OracleCommand對象</returns>
        public OracleCommand CreateCommand(string cmdText, CommandType cmdType, params OracleParameter[] parameters)
        {
            OracleCommand cmd = Connection.CreateCommand();
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            if (parameters != null)
            {
                foreach (OracleParameter para in parameters)
                {
                    cmd.Parameters.Add(para);
                }
            }
            return cmd;
        }
        #endregion

        #region 非查詢命令執行
        /// <summary>
        /// 執行非查詢命令
        /// </summary>
        /// <param name="cmdText">命令語句</param>
        /// <param name="parameters">參數</param>
        /// <returns>影響數據條數,異常返回-1</returns>
        public int ExecuteNonQuery(string cmdText, params OracleParameter[] parameters)
        {
            return ExecuteNonQuery(cmdText, CommandType.Text, parameters);
        }

        /// <summary>
        /// 執行非查詢命令
        /// </summary>
        /// <param name="cmdText">命令語句</param>
        /// <param name="cmdType">命令類型</param>
        /// <param name="parameters">參數</param>
        /// <returns>影響數據條數,異常返回-1</returns>
        public int ExecuteNonQuery(string cmdText, CommandType cmdType, params OracleParameter[] parameters)
        {
            OracleCommand cmd = CreateCommand(cmdText, cmdType, parameters);
            return ExecuteNonQuery(cmd);
        }
        #endregion

        #region 非查詢命令執行(帶插入系統日誌)
        /// <summary>
        /// 執行非查詢命令
        /// </summary>
        /// <param name="cmdText">命令語句</param>
        /// <param name="parameters">參數</param>
        /// <returns>影響數據條數,異常返回-1</returns>
        public int ExecuteNonQuery(string cmdText, SynclogModel logmodel, params OracleParameter[] parameters)
        {
            return ExecuteNonQuery(cmdText, CommandType.Text,logmodel, parameters);
        }


        /// <summary>
        /// 執行非查詢命令(帶插入系統日誌)
        /// </summary>
        /// <param name="cmdText">命令語句</param>
        /// <param name="cmdType">命令類型</param>
        /// <param name="parameters">參數</param>
        /// <returns>影響數據條數,異常返回-1</returns>
        public int ExecuteNonQuery(string cmdText, CommandType cmdType, SynclogModel logmodel, params OracleParameter[] parameters)
        {
            OracleCommand cmd = CreateCommand(cmdText, cmdType, parameters);
            return ExecuteNonQuery(cmd,logmodel);
        }
        #endregion

        #region 執行查詢命令
        /// <summary>
        /// 執行查詢命令(默認文本類型)
        /// </summary>
        /// <param name="cmdText">命令語句</param>
        /// <param name="parameters">參數</param>
        /// <returns>Model集合</returns>
        public DataTable ExecuteQuery(string cmdText, params OracleParameter[] parameters)
        {
            return ExecuteQuery(cmdText, CommandType.Text, parameters);
        }

        /// <summary>
        /// 執行查詢命令
        /// </summary>
        /// <param name="cmdText">命令語句</param>
        /// <param name="cmdType">命令類型</param>
        /// <param name="parameters">參數</param>
        /// <returns>Model集合</returns>
        public DataTable ExecuteQuery(string cmdText, CommandType cmdType, params OracleParameter[] parameters)
        {
            OracleCommand cmd = CreateCommand(cmdText, cmdType, parameters);
            return ExecuteQuery(cmd);
        }

        /// <summary>
        /// 執行單值查詢命令
        /// </summary>
        /// <param name="cmdText">命令語句</param>
        /// <param name="cmdType">命令類型</param>
        /// <param name="parameters">參數</param>
        /// <returns>返回結果的第一行第一列的單個數據</returns>
        public object ExecuteScalar(string cmdText, CommandType cmdType, params OracleParameter[] parameters)
        {
            OracleCommand cmd = CreateCommand(cmdText, cmdType, parameters);
            using (cmd.Connection)
            {
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }
                return cmd.ExecuteScalar();
            }
        }

        /// <summary>
        /// 執行單值查詢命令(默認文本類型)
        /// </summary>
        /// <param name="cmdText">命令語句</param>
        /// <param name="parameters">參數</param>
        /// <returns>返回結果的第一行第一列的單個數據</returns>
        public object ExecuteScalar(string cmdText, params OracleParameter[] parameters)
        {
            return ExecuteScalar(cmdText, CommandType.Text, parameters);
        }
        #endregion

        #region 執行分頁查詢命令
        /// <summary>
        /// 執行分頁查詢命令(僅支持Sql語句)
        /// </summary>
        /// <param name="cmdText">命令語句</param>
        /// <param name="pageIndex">頁索引</param>
        /// <param name="pageSize">頁大小</param>
        /// <param name="parameters">參數</param>
        /// <returns>Model集合</returns>
        public DataTable ExecutePagerQuery(string cmdText, int pageIndex, int pageSize, out int totalCount, params OracleParameter[] parameters)
        {
            string cmdCount = cmdText.ToUpper();
            int fromIdx = cmdCount.IndexOf(" FROM ") + 1;
            if (fromIdx < 1) { fromIdx = cmdCount.IndexOf("FROM("); }
            if (fromIdx < 0) { fromIdx = cmdCount.IndexOf(")FROM") + 1; }

            int orderIdx = cmdCount.LastIndexOf(" ORDER ");
            if (orderIdx < 0) { orderIdx = cmdCount.LastIndexOf(")ORDER "); }
            if (orderIdx > -1) { orderIdx++; }

            cmdCount = "SELECT COUNT(1) " + (orderIdx > 0 ? cmdText.Substring(fromIdx, orderIdx - fromIdx) : cmdText.Substring(fromIdx));

            string commandText = "SELECT B.* FROM (SELECT ROWNUM ROW_NUM, A.* FROM (" + cmdText + ") A WHERE ROWNUM >= 1 AND ROWNUM <= :v_MaxRowIndex) B WHERE ROW_NUM >= :v_MinRowIndex";
            int minRowIndex = (pageIndex - 1) * pageSize + 1;
            int maxRowIndex = pageSize * pageIndex;

            OracleCommand cmd = Connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = cmdCount;

            if (parameters != null)
            {
                foreach (OracleParameter opara in parameters)
                {
                    cmd.Parameters.Add(opara);
                }
            }
            try
            {
                using (cmd.Connection)
                {
                    if (cmd.Connection.State != ConnectionState.Open)
                    {
                        cmd.Connection.Open();
                    }
                    totalCount = Convert.ToInt32(cmd.ExecuteScalar());

                    cmd.CommandText = commandText;

                    OracleParameter para = new OracleParameter(":v_MaxRowIndex", maxRowIndex);
                    para.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(para);

                    para = new OracleParameter(":v_MinRowIndex", minRowIndex);
                    para.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(para);

                    OracleDataAdapter adapt = new OracleDataAdapter(cmd);
                    DataTable dtData = new DataTable();
                    adapt.Fill(dtData);
                    return dtData;
                }
            }
            catch
            {
                totalCount = -1;
                return null;
            }
        }

        /// <summary>
        /// 執行分頁查詢命令(僅支持Sql語句)
        /// </summary>
        /// <param name="cmdText">命令語句</param>
        /// <param name="pageIndex">頁索引</param>
        /// <param name="pageSize">頁大小</param>
        /// <param name="parameters">參數</param>
        /// <returns>Model集合</returns>
        public DataTable ExecutePagerQuery(string cmdText, int pageIndex, int pageSize, params OracleParameter[] parameters)
        {
            int minRowIndex = (pageIndex - 1) * pageSize + 1;
            int maxRowIndex = pageSize * pageIndex;

            string commandText = "SELECT B.* FROM (SELECT ROWNUM ROW_NUM, A.* FROM (" + cmdText + ") A) B WHERE ROW_NUM <= :v_MaxRowIndex AND ROW_NUM >= :v_MinRowIndex";
            OracleCommand cmd = CreateCommand(commandText, CommandType.Text, parameters);

            OracleParameter para = new OracleParameter(":v_MaxRowIndex", maxRowIndex);
            para.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(para);

            para = new OracleParameter(":v_MinRowIndex", minRowIndex);
            para.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(para);

            return ExecuteQuery(cmd);
        }
        #endregion


        #region 插入系統日誌

        public void InsertLog(OracleCommand cmd, SynclogModel model)
        {
            string str = cmd.CommandText;
            for (int i = 0; i < cmd.Parameters.Count; i++)
            {
                str = str.Replace(cmd.Parameters[i].ParameterName.ToString(), "'"+cmd.Parameters[i].Value.ToString()+"'");
            }
            str=str.Replace("'","''");
            string sqlText = "insert into gds_sc_synclog values('','" + model.TransactionType + "','" + model.LevelNo + "','" + model.FromHost + "','" + model.ToHost + "','" + model.DocNo + "','" + str + "',sysdate,'" + model.ProcessFlag + "','" + model.ProcessOwner + "')";
            int flag = ExecuteNonQuery(sqlText);
        }

        #endregion
    }
}