/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： BellCardQueryBll.cs
 * 檔功能描述： 剩余加班導入業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.26
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using GDSBG.MiABU.Attendance.IDAL.KQM.Query;
using GDSBG.MiABU.Attendance.Model.KQM.Query;

namespace GDSBG.MiABU.Attendance.BLL.KQM.Query
{
    public class BellCardQueryBll : BLLBase<IBellCardQueryDal>
    {
        /// <summary>
        /// 獲得卡鐘數據
        /// </summary>
        /// <returns></returns>
        public DataTable GetBellNo()
        {
            return DAL.GetBellNo();
        }
        /// <summary>
        /// 查詢刷卡的詳細信息（在重新計算的頁面使用）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<BellCardDataModel> GetBellDataReList(BellCardDataModel model, string KQdate, string shiftNo)
        {
            return DAL.GetBellDataReList(model, KQdate, shiftNo);
        }

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
        public DataTable GetBellCardData(BellCardDataModel model, string sql, bool ischecked, string strFromDate, string strToDate, int pageIndex, int pageSize, out int totalCount)
        {
            DateTime FromDate = DateTime.MinValue,ToDate = DateTime.MaxValue;
            if (!string.IsNullOrEmpty(strFromDate))
            {
                FromDate = Convert.ToDateTime(strFromDate);
            }
            if (!string.IsNullOrEmpty(strToDate))
            {
                ToDate = Convert.ToDateTime(strToDate);
            }
            return DAL.GetBellCardData(model, sql, ischecked, FromDate, ToDate, pageIndex, pageSize, out totalCount);
        }

        /// <summary>
        /// 導出查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sql"></param>
        /// <param name="ischecked"></param>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <returns></returns>
        public DataTable GetBellCardDataForExport(BellCardDataModel model, string sql, bool ischecked, string strFromDate, string strToDate)
        {
            DateTime FromDate = DateTime.MinValue, ToDate = DateTime.MaxValue;
            if (!string.IsNullOrEmpty(strFromDate))
            {
                FromDate = Convert.ToDateTime(strFromDate);
            }
            if (!string.IsNullOrEmpty(strToDate))
            {
                ToDate = Convert.ToDateTime(strToDate);
            }
            return DAL.GetBellCardDataForExport(model, sql, ischecked, FromDate, ToDate);
        }

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<BellCardDataModel> GetList(DataTable dt)
        {
            return DAL.GetList(dt);
        }
    }
}
