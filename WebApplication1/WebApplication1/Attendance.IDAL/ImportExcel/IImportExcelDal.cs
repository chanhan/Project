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

using System;
using GDSBG.MiABU.Attendance.Common.Attributes;


namespace GDSBG.MiABU.Attendance.IDAL.ImportExcel
{
    /// <summary>
    /// 卡鐘信息數據操作接口
    /// </summary>
    [RefClass("ImportExcel.ImportExcelDal")]
    public interface IImportExcelDal
    {
        /// <summary>
        /// 保存導入數據
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        bool InsertExcel(string sql);

        /// <summary>
        /// 刪除數據
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        bool DeleteData(string sql);
    }
}
