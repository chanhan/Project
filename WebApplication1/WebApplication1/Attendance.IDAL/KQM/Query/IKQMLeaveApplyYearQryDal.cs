﻿/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IKQMLeaveApplyYearQryDal.cs
 * 檔功能描述： 年休假統計查詢接口類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.30
 * 
 */
using System.Collections.Generic;
using System.Data;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.KQM.Query;

namespace GDSBG.MiABU.Attendance.IDAL.KQM.Query
{
    [RefClass("KQM.Query.KQMLeaveApplyYearQryDal")]
    public interface IKQMLeaveApplyYearQryDal
    {
        /// <summary>
        /// 年休假統計分頁查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="BatchEmployeeNo">多筆工號</param>
        /// <param name="joindatefrom"></param>
        /// <param name="joindateto"></param>
        /// <param name="countdatefrom"></param>
        /// <param name="countdateto"></param>
        /// <param name="Status">在職狀態</param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        DataTable GetLeaveApplyYear(KQMLeaveApplyYearQryModel model,string sql, string BatchEmployeeNo, string joindatefrom, string joindateto, string countdatefrom, string countdateto, string Status, int PageIndex, int PageSize, out int totalCount);

        /// <summary>
        /// 導出查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sql"></param>
        /// <param name="BatchEmployeeNo"></param>
        /// <param name="joindatefrom"></param>
        /// <param name="joindateto"></param>
        /// <param name="countdatefrom"></param>
        /// <param name="countdateto"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        DataTable GetLeaveApplyYearForExport(KQMLeaveApplyYearQryModel model, string sql, string BatchEmployeeNo, string joindatefrom, string joindateto, string countdatefrom, string countdateto, string Status);

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        List<KQMLeaveApplyYearQryModel> GetList(DataTable dt);

        /// <summary>
        /// 獲得統計天數
        /// </summary>
        /// <param name="workno"></param>
        /// <param name="countdatefrom"></param>
        /// <param name="countdateto"></param>
        /// <returns></returns>
        DataTable GetCountDays(string workno, string countdatefrom, string countdateto);
    }
}