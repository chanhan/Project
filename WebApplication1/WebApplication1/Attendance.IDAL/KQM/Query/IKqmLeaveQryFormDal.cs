/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IKqmLeaveQryFormDal.cs
 * 檔功能描述： 請假明細查詢操作接口
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.12.29
 * 
 */

using System;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.KQM.AttendanceData;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Model.KQM.Query;

namespace GDSBG.MiABU.Attendance.IDAL.KQM.Query
{
    /// <summary>
    ///  請假明細查詢操作接口
    /// </summary>
    [RefClass("KQM.Query.KqmLeaveQryFormDal")]
    public interface IKqmLeaveQryFormDal
    {
        /// <summary>
        /// 查詢休假類型
        /// </summary>
        /// <returns>休假類型集</returns>
        DataTable GetLeaveType();
        /// <summary>
        /// 查詢員工離職日期
        /// </summary>
        /// <param name="sID">請假單序號</param>
        /// <returns></returns>
        string GetLeaveDate(string sID);
        /// <summary>
        /// 獲得離職日期和請假結束日期的早晚
        /// </summary>
        /// <param name="LeaveDate">離職日期</param>
        /// <param name="sEndDate">請假結束日期</param>
        /// <returns></returns>
        string GetFlagEndDate(string LeaveDate, string sEndDate);
        /// <summary>
        /// 將datatable轉換成model
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        List<KqmLeaveQryFormModel> GetModelList(DataTable dt);
        /// <summary>
        /// 查詢請假的詳細信息
        /// </summary>
        /// <param name="sID">請假單序號</param>
        /// <param name="sStartDate">開始日期</param>
        /// <param name="sEndDate">結束日期</param>
        /// <returns></returns>
        string GetLeaveDetail(string sID, string sStartDate, string strEndDate);
        /// <summary>
        /// 根據條件查詢
        /// </summary>
        /// <param name="model">請假明細查詢的model</param>
        /// <param name="BatchEmployeeNo">批量查詢的更好</param>
        /// <param name="leaveType">請假類型</param>
        /// <param name="StartDate">請假開始日期</param>
        /// <param name="empStatus">職工在職狀態</param>
        /// <param name="EndDate">請假技術日期</param>
        /// <param name="pageIndex">頁碼</param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount">總條數</param>
        /// <returns></returns>
        DataTable GetLeaveDataList(KqmLeaveQryFormModel model, string depCode, string SQLDep, string BatchEmployeeNo, string leaveType, string startDate, string empStatus, string endDate, int pageIndex, int pageSize, out int totalCount);
        /// <summary>
        /// 根據條件查詢(導出）
        /// </summary>
        /// <param name="model">請假明細查詢的model</param>
        /// <param name="BatchEmployeeNo">批量查詢的更好</param>
        /// <param name="leaveType">請假類型</param>
        /// <param name="StartDate">請假開始日期</param>
        /// <param name="empStatus">職工在職狀態</param>
        /// <param name="EndDate">請假技術日期</param>
        /// <returns></returns>
        DataTable GetLeaveDataList(KqmLeaveQryFormModel model, string depCode, string SQLDep, string BatchEmployeeNo, string leaveType, string startDate, string empStatus, string endDate);
    }
}
