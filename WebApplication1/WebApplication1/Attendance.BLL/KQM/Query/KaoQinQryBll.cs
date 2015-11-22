/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KaoQinQryBll.cs
 * 檔功能描述： 考勤結果查詢業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.27
 * 
 */
using System.Collections.Generic;
using System.Data;
using GDSBG.MiABU.Attendance.IDAL.KQM.Query;
using GDSBG.MiABU.Attendance.Model.KQM.Query;

namespace GDSBG.MiABU.Attendance.BLL.KQM.Query
{
    public class KaoQinQryBll : BLLBase<IKaoQinQryDal>
    {
        /// <summary>
        /// 根據條件分頁查詢
        /// </summary>
        /// <param name="sql">組織權限</param>
        /// <param name="depname">單位</param>
        /// <param name="IsMakeup">是否補卡</param>
        /// <param name="IsSupporter">是否支援</param>
        /// <param name="EmployeeNo">一筆工號</param>
        /// <param name="BatchEmployeeNo">多筆工號</param>
        /// <param name="LocalName">姓名</param>
        /// <param name="fromDate">考勤起始日期</param>
        /// <param name="toDate">考勤截止日期</param>
        /// <param name="StatusName">考勤狀態</param>
        /// <param name="ExceptionType">結果</param>
        /// <param name="ShiftNo">班別</param>
        /// <param name="Status">在職狀態</param>
        /// <param name="flagA"></param>
        /// <param name="flagB"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetKaoQinDataList(string sql, string depname, string IsMakeup, string IsSupporter, string EmployeeNo, string BatchEmployeeNo, string LocalName, string fromDate, string toDate, string StatusName, string ExceptionType, string ShiftNo, string Status, bool flagA, bool flagB, int PageIndex, int PageSize, out int totalCount)
        {
            return DAL.GetKaoQinDataList(sql, depname, IsMakeup, IsSupporter, EmployeeNo, BatchEmployeeNo, LocalName, fromDate, toDate, StatusName, ExceptionType, ShiftNo, Status, flagA, flagB, PageIndex, PageSize, out totalCount);
        }

        /// <summary>
        /// 導出查詢
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="depname"></param>
        /// <param name="IsMakeup"></param>
        /// <param name="IsSupporter"></param>
        /// <param name="EmployeeNo"></param>
        /// <param name="BatchEmployeeNo"></param>
        /// <param name="LocalName"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="StatusName"></param>
        /// <param name="ExceptionType"></param>
        /// <param name="ShiftNo"></param>
        /// <param name="Status"></param>
        /// <param name="flagA"></param>
        /// <param name="flagB"></param>
        /// <returns></returns>
        public DataTable GetKaoQinDataForExport(string sql, string depname, string IsMakeup, string IsSupporter, string EmployeeNo, string BatchEmployeeNo, string LocalName, string fromDate, string toDate, string StatusName, string ExceptionType, string ShiftNo, string Status, bool flagA, bool flagB)
        {
            return DAL.GetKaoQinDataForExport(sql, depname, IsMakeup, IsSupporter, EmployeeNo, BatchEmployeeNo, LocalName, fromDate, toDate, StatusName, ExceptionType, ShiftNo, Status, flagA, flagB);
        }

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<KaoQinDataQueryModel> GetList(DataTable dt)
        {
            return DAL.GetList(dt);
        }

        /// <summary>
        /// 獲得當月曠工累計
        /// </summary>
        /// <param name="workno"></param>
        /// <param name="kqdate"></param>
        /// <returns></returns>
        public DataTable GetAbsentTotal(string workno, string kqdate)
        {
            return DAL.GetAbsentTotal(workno, kqdate);
        }
    }
}
