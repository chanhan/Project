/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IImportExcel.cs
 * 檔功能描述： EXCEL導入空接口層
 * 
 * 版本：1.0
 * 創建標識：  2011.12.13
 * 
 */

using GDSBG.MiABU.Attendance.IDAL.ImportExcel;
namespace GDSBG.MiABU.Attendance.BLL.ImportExcel
{
    public class ImportExcelBll : BLLBase<IImportExcelDal>
    {
        /// <summary>
        /// 保存導入數據
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool InsertExcel(string sql)
        {
            return DAL.InsertExcel(sql);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool DeleteData(string sql)
        {
            return DAL.DeleteData(sql);
        }
    }
}
