/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ImportExcelDal.cs
 * 檔功能描述： EXCEL導入數據訪問層
 * 
 * 版本：1.0
 * 創建標識： 何西 2011.12.13
 * 
 */

using GDSBG.MiABU.Attendance.IDAL.ImportExcel;
using GDSBG.MiABU.Attendance.Model.ImportExcel;

namespace GDSBG.MiABU.Attendance.OracleDAL.ImportExcel
{
    public class ImportExcelDal : DALBase<ImportExcelModel>, IImportExcelDal
    {
        /// <summary>
        /// 保存導入數據
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool InsertExcel(string sql)
        {
            return DalHelper.ExecuteNonQuery(sql) >= 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool DeleteData(string sql) 
        {
            return DalHelper.ExecuteNonQuery(sql) != -1;
        }
    }
}
