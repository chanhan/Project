/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： INightAllowanceToExcelDal.cs
 * 檔功能描述： 夜宵補助操作接口
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2012.01.09
 * 
 */

using System;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.KQM.AttendanceData;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Model.KQM.Query;
using GDSBG.MiABU.Attendance.Model.KQM;


namespace GDSBG.MiABU.Attendance.IDAL.KQM.Query
{
    /// <summary>
    ///  夜宵補助操作接口
    /// </summary>
    [RefClass("KQM.Query.NightAllowanceToExcelDal")]
    public interface INightAllowanceToExcelDal
    {
        /// <summary>
        /// 查詢導出數據
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dateFrom">開始日期</param>
        /// <param name="dateTo">結束日期</param>
        /// <param name="deptCode">部門代碼</param>
        /// <returns></returns>
        DataTable GetNightExcel(NightAllowanceToExcelModel model, string dateFrom, string dateTo, string deptCode, string status, string SqlDep);
        /// <summary>
        /// 查詢狀態是0的情況下的數據導出
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        DataTable GetNightExcelStatus0(string dateFrom, string dateTo,string  workNo);
    }
}
