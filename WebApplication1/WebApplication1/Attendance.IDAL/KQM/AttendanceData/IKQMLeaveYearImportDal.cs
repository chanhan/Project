/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IKQMLeaveYearImportDal.cs
 * 檔功能描述： 已休年假操作接口
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.12.23
 * 
 */

using System;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.KQM.AttendanceData;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.IDAL.KQM.AttendanceData
{
    /// <summary>
    ///  已休年假操作接口
    /// </summary>
    [RefClass("KQM.AttendanceData.KQMLeaveYearImportDal")]
    public interface IKQMLeaveYearImportDal
    {
        /// <summary>
        /// 根據主鍵查詢Model
        /// <param name="model">異常原因model</param>
        /// <param name="orderType">排序</param>
        /// <param name="pageIndex">當前頁</param>
        /// <param name="pageSize"></param></param>
        /// <param name="totalCount">總頁數</param>
        /// <returns></returns>
        DataTable GetLeaveDaysList(KQMLeaveYearImportModel model, string SQLDep, string depCode, int pageIndex, int pageSize, out int totalCount);
        /// <summary>
        /// 根據主鍵查詢Model
        /// <param name="model">異常原因model</param>
        /// <param name="orderType">排序</param>
        /// <returns></returns>
        DataTable GetLeaveDaysList(KQMLeaveYearImportModel model, string SQLDep, string depCode);
        /// <summary>
        /// 刪除一天休假記錄
        /// </summary>
        /// <param name="functionId">刪除一天休假記錄model</param>
        /// <returns>是否執行成功</returns>
        int DeleteLeaveYearByKey(KQMLeaveYearImportModel model, SynclogModel logmodel);
       /// <summary>
       /// 導入
       /// </summary>
       /// <param name="personcode">登錄者工號</param>
       /// <param name="companyId">登錄者事業群代碼</param>
       /// <param name="successnum">成功插入的條數</param>
       /// <param name="errornum">錯誤條數</param>
       /// <returns></returns>
        DataTable ImpoertExcel(string personcode, out int successnum, out int errornum, SynclogModel logmodel);
       /// <summary>
       /// 將datatable轉換為List
       /// </summary>
       /// <param name="dt"></param>
       /// <returns></returns>
       List<KQMLeaveYearImportModel> GetList(DataTable dt);
    }
}
