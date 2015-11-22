/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ORMHelper.cs
 * 檔功能描述： 對象關係映射幫助類
 * 
 * 版本：1.0
 * 創建標識： Lucky Lee 2011.09.20
 * 
 */

using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;
using System;

namespace GDSBG.MiABU.Attendance.Common
{
    /// <summary>
    /// 對象關係映射幫助類
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ORMHelper<T> where T : new()
    {
        private PropertyInfo[] properties = typeof(T).GetProperties();
        private TableNameAttribute tableAttribute;
        private string selectColumnsString, editableColumnsString;
        private Dictionary<string, string> dicPrimaryKeys;
        private Dictionary<string, string> dicPropertyColumns;

        #region 供查詢之表名
        /// <summary>
        /// 供查詢之表名
        /// </summary>
        public string SelectTableName
        {
            get
            {
                if (tableAttribute == null)
                {
                    tableAttribute = (TableNameAttribute)typeof(T).GetCustomAttributes(typeof(TableNameAttribute), false)[0];
                }
                return tableAttribute.SelectTable;
            }
        }
        #endregion

        #region 主表名，可更新之表名
        /// <summary>
        /// 可更新之表名
        /// </summary>
        public string TableName
        {
            get
            {
                if (tableAttribute == null)
                {
                    tableAttribute = (TableNameAttribute)typeof(T).GetCustomAttributes(typeof(TableNameAttribute), false)[0];
                }
                return tableAttribute.TableName;
            }
        }
        #endregion

        #region 可查詢之欄位字串
        /// <summary>
        /// 可查詢欄位字串，如：column1,column2,...columnN
        /// </summary>
        public string SelectColumnsString
        {
            get
            {
                if (string.IsNullOrEmpty(selectColumnsString))
                {
                    StringBuilder colsStr = new StringBuilder();
                    foreach (PropertyInfo property in properties)
                    {
                        try
                        {
                            ColumnAttribute columnAttr = (ColumnAttribute)property.GetCustomAttributes(typeof(ColumnAttribute), false)[0];
                            if (!string.IsNullOrEmpty(columnAttr.ColumnName))
                            {
                                colsStr.Append(columnAttr.ColumnName).Append(",");
                            }
                        }
                        catch
                        {
                        }
                    }
                    selectColumnsString = colsStr.ToString().TrimEnd(',');
                }
                return selectColumnsString;
            }
        }
        #endregion

        #region 可更新的欄位字串
        /// <summary>
        /// 可更新的欄位字串，如：column1,column2,...columnN
        /// </summary>
        public string EditableColumnsString
        {
            get
            {
                if (string.IsNullOrEmpty(editableColumnsString))
                {
                    StringBuilder colsStr = new StringBuilder();
                    foreach (PropertyInfo property in properties)
                    {
                        try
                        {
                            ColumnAttribute columnAttr = (ColumnAttribute)property.GetCustomAttributes(typeof(ColumnAttribute), false)[0];
                            if (!string.IsNullOrEmpty(columnAttr.ColumnName) && !columnAttr.OnlySelect)
                            {
                                colsStr.Append(columnAttr.ColumnName).Append(",");
                            }
                        }
                        catch
                        {
                        }
                    }
                    editableColumnsString = colsStr.ToString().TrimEnd(',');
                }
                return editableColumnsString;
            }
        }
        #endregion

        #region 屬性及主鍵欄位之鍵值對
        /// <summary>
        /// 屬性及主鍵欄位之鍵值對
        /// </summary>
        public Dictionary<string, string> PropertyPrimaryKeys
        {
            get
            {
                if (dicPrimaryKeys == null)
                {
                    dicPrimaryKeys = new Dictionary<string, string>();
                    foreach (PropertyInfo property in properties)
                    {
                        try
                        {
                            ColumnAttribute columnAttr = (ColumnAttribute)property.GetCustomAttributes(typeof(ColumnAttribute), false)[0];
                            if (!string.IsNullOrEmpty(columnAttr.ColumnName) && columnAttr.IsPrimaryKey)
                            {
                                dicPrimaryKeys.Add(property.Name, columnAttr.ColumnName);
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                return dicPrimaryKeys;
            }
        }
        #endregion

        #region 屬性及欄位之鍵值對
        /// <summary>
        /// 屬性及欄位之鍵值對
        /// </summary>
        public Dictionary<string, string> PropertyColumns
        {
            get
            {
                if (dicPropertyColumns == null)
                {
                    dicPropertyColumns = new Dictionary<string, string>();
                    foreach (PropertyInfo property in properties)
                    {
                        try
                        {
                            ColumnAttribute columnAttr = (ColumnAttribute)property.GetCustomAttributes(typeof(ColumnAttribute), false)[0];
                            if (!string.IsNullOrEmpty(columnAttr.ColumnName))
                            {
                                dicPropertyColumns.Add(property.Name, columnAttr.ColumnName);
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                return dicPropertyColumns;
            }
        }
        #endregion

        #region 將DataTable映射到Model集合
        /// <summary>
        /// 將DataTable映射到Model集合
        /// </summary>
        /// <param name="dtData"></param>
        /// <returns></returns>
        public List<T> SetDataTableToList(DataTable dtData)
        {
            List<T> list = new List<T>();
            foreach (DataRow dr in dtData.Rows)
            {
                T tModel = new T();
                foreach (PropertyInfo property in properties)
                {                    
                    try
                    {
                        ColumnAttribute columnAttr = (ColumnAttribute)property.GetCustomAttributes(typeof(ColumnAttribute), false)[0];

                        if (dr[columnAttr.ColumnName] == DBNull.Value) { continue; }

                        if (property.PropertyType.IsGenericType && property.PropertyType.IsValueType &&
                                property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            property.SetValue(tModel, Convert.ChangeType(dr[columnAttr.ColumnName],
                                property.PropertyType.GetGenericArguments()[0]), null);
                        }
                        else
                        {
                            property.SetValue(tModel, dr[columnAttr.ColumnName], null);
                        }
                    }
                    catch
                    {
                    }
                }
                list.Add(tModel);
            }
            return list;
        }
        #endregion

        #region 將DataReader映射到Model集合
        /// <summary>
        /// 將DataReader映射到Model集合
        /// </summary>
        /// <param name="drData"></param>
        /// <returns></returns>
        public List<T> SetDataReaderToList(IDataReader drData)
        {
            List<T> list = new List<T>();
            while (drData.Read())
            {
                T tModel = new T();
                foreach (PropertyInfo property in properties)
                {                    
                    try
                    {
                        ColumnAttribute columnAttr = (ColumnAttribute)property.GetCustomAttributes(typeof(ColumnAttribute), false)[0];

                        if (drData[columnAttr.ColumnName] == DBNull.Value) { continue; }

                        if (property.PropertyType.IsGenericType && property.PropertyType.IsValueType &&
                                property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            property.SetValue(tModel, Convert.ChangeType(drData[columnAttr.ColumnName],
                                property.PropertyType.GetGenericArguments()[0]), null);
                        }
                        else
                        {
                            property.SetValue(tModel, drData[columnAttr.ColumnName], null);
                        }
                    }
                    catch
                    {
                    }
                }
                list.Add(tModel);
            }
            return list;
        }
        #endregion

        #region 獲取Model的各屬性對應的值
        /// <summary>
        /// 獲取Model的各屬性對應的值
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Dictionary<string, object> GetModelMapValues(T model)
        {
            Dictionary<string, object> dicValues = new Dictionary<string, object>();
            foreach (PropertyInfo property in properties)
            {
                try
                {
                    object val = property.GetValue(model, null);
                    dicValues.Add(property.Name, val);
                }
                catch
                {
                }
            }
            return dicValues;
        }
        #endregion
    }
}
