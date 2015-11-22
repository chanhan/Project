/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： DALBase.cs
 * 檔功能描述： 所有數據操作類基類
 * 
 * 版本：1.0
 * 創建標識： Lucky Lee 2011.09.21
 * 
 */

using GDSBG.MiABU.Attendance.Common;
namespace GDSBG.MiABU.Attendance.OracleDAL
{
    /// <summary>
    /// 所有數據操作類基類
    /// </summary>
    /// <typeparam name="T">要操作的Model類型</typeparam>
    public abstract class DALBase<T> where T : Model.ModelBase, new()
    {
        private string _connectionString;
        protected DALHelper<T> DalHelper;
        protected ORMHelper<T> OrmHelper
        {
            get { return DalHelper.OrmHelper; }
        }

        /// <summary>
        /// 連接字串
        /// </summary>
        public string ConnectionString
        {
            get { return _connectionString; }
            set
            {
                _connectionString = value;
                if (DalHelper == null)
                {
                    DalHelper = new DALHelper<T>(_connectionString);
                }
                else
                {
                    DalHelper.SetConnectionString(_connectionString);
                }
            }
        }
    }
}
