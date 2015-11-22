/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IBellCardQueryDal.cs
 * 檔功能描述： 原始刷卡數據接口類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.26
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.KQM.Query;

namespace GDSBG.MiABU.Attendance.IDAL.KQM.Query
{
    [RefClass("KQM.Query.BellCardQueryDal")]
    public interface IBellCardQueryDal
    {
        /// <summary>
        /// 獲得卡鐘數據
        /// </summary>
        /// <returns></returns>
        DataTable GetBellNo();
        /// <summary>
        /// 查詢刷卡的詳細信息（在重新計算的頁面使用）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<BellCardDataModel> GetBellDataReList(BellCardDataModel model, string KQdate, string shiftNo);

        /// <summary>
        /// 根據條件分頁查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        DataTable GetBellCardData(BellCardDataModel model, string sql, bool ischecked, Nullable<DateTime> FromDate, Nullable<DateTime> ToDate, int pageIndex, int pageSize, out int totalCount);

        /// <summary>
        /// 導出查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sql"></param>
        /// <param name="ischecked"></param>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <returns></returns>
        DataTable GetBellCardDataForExport(BellCardDataModel model, string sql, bool ischecked, Nullable<DateTime> FromDate, Nullable<DateTime> ToDate);

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        List<BellCardDataModel> GetList(DataTable dt);
        
    }
}
