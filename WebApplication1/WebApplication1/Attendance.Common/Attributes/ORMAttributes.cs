/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ORMAttributes.cs
 * 檔功能描述： 實體關係映射屬性
 * 
 * 版本：1.0
 * 創建標識： Lucky Lee 2011.09.20
 * 
 */

using System;

namespace GDSBG.MiABU.Attendance.Common.Attributes
{
    /// <summary>
    /// 實體映射到表名
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class TableNameAttribute : Attribute
    {
        string tablename;
        string selecttable;

        /// <summary>
        /// 表名
        /// </summary>
        /// <param name="tableName">增刪改查主表名</param>
        public TableNameAttribute(string tableName)
        {
            tablename = tableName;
        }

        /// <summary>
        /// 增刪改查主表名
        /// </summary>
        public string TableName
        {
            get { return tablename; }
        }

        /// <summary>
        /// 供查詢表 or 視圖名,未設置時返回主表名
        /// </summary>
        public string SelectTable
        {
            set { selecttable = value; }
            get
            {
                if (string.IsNullOrEmpty(selecttable))
                {
                    return tablename;
                }
                return selecttable;
            }
        }
    }

    /// <summary>
    /// 實體屬性映射到表中欄位
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class ColumnAttribute : Attribute
    {
        string column;
        bool isprimary = false;
        bool onselect = false;
        /// <summary>
        /// 實體屬性映射到表中欄位
        /// </summary>
        /// <param name="columnName">映射的欄位名</param>
        public ColumnAttribute(string columnName)
        {
            column = columnName;
        }

        /// <summary>
        /// 映射的欄位名
        /// </summary>
        public string ColumnName
        {
            get { return column; }
        }

        /// <summary>
        /// 是否主鍵,默認為'否'
        /// </summary>
        public bool IsPrimaryKey
        {
            set { isprimary = value; }
            get { return isprimary; }
        }

        /// <summary>
        /// 是否只能查詢,默認為'否'
        /// </summary>
        public bool OnlySelect
        {
            set { onselect = value; }
            get { return onselect; }
        }
    }
}