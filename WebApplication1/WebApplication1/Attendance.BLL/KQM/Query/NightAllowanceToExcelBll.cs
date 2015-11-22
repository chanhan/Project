/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： NightAllowanceToExcelBll.cs
 * 檔功能描述： 夜宵補助業務邏輯類
 * 
 * 版本：1.0
 * 創建標識：高子焱 2012.01.09
 * 
 */

using System.Collections.Generic;
using System.Data;
using System.Collections;
using System;
using GDSBG.MiABU.Attendance.IDAL.KQM.Query;
using GDSBG.MiABU.Attendance.Model.KQM.Query;
using GDSBG.MiABU.Attendance.Model.KQM;

namespace GDSBG.MiABU.Attendance.BLL.KQM.Query
{
    public class NightAllowanceToExcelBll : BLLBase<INightAllowanceToExcelDal>
    {
        /// <summary>
        /// 查詢導出數據
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dateFrom">開始日期</param>
        /// <param name="dateTo">結束日期</param>
        /// <param name="deptCode">部門代碼</param>
        /// <returns></returns>
        public DataTable GetNightExcel(NightAllowanceToExcelModel model, string dateFrom, string dateTo, string deptCode, string status, string SqlDep)
        {
            return DAL.GetNightExcel(model, dateFrom, dateTo, deptCode, status, SqlDep);
        }
        /// <summary>
        /// 查詢狀態是0的情況下的數據導出
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public DataTable GetNightExcelStatus0(string dateFrom, string dateTo, string workNo)
        {
            return DAL.GetNightExcelStatus0(dateFrom, dateTo, workNo);
        }
    }
}
