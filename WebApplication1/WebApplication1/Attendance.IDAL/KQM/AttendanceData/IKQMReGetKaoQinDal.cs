/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IKQMReGetKaoQinDal.cs
 * 檔功能描述： 重新計算操作接口
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.12.26
 * 
 */

using System;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.KQM.AttendanceData;
using System.Data;
using System.Collections;
using System.Collections.Generic;
namespace GDSBG.MiABU.Attendance.IDAL.KQM.AttendanceData
{
    /// <summary>
    ///  重新計算操作接口
    /// </summary>
    [RefClass("KQM.AttendanceData.KQMReGetKaoQinDal")]
    public interface IKQMReGetKaoQinDal
    {
        /// <summary>
        /// 查詢操作日期是否超過倆個月
        /// </summary>
        /// <param name="FromDate">開始日期</param>
        /// <param name="ToDate">結束日期</param>
        /// <returns></returns>
        DataTable GetFromDate(string FromDate, string ToDate);
        /// <summary>
        /// 將datatable轉化成List導出
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        List<KQMReGetKaoQinModel> GetModelList(DataTable dt);
        /// <summary>
        /// 重新計算查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="workNoList">批量查詢的工號集</param>
        /// <param name="temVal">checkBoxlist集 異常類型</param>
        /// <param name="shiftNo">班別代碼</param>
        /// <param name="ststus">狀態</param>
        /// <param name="pageIndex">當前頁碼</param>
        /// <param name="pageSize">一頁顯示條數</param>
        /// <param name="totalCount">總數</param>
        /// <returns></returns>
        DataTable GetKaoQinDataList(KQMReGetKaoQinModel model, string workNoList, string temVal, string shiftNo, string status, string fromDate, string toDate, string SQLDep, string depCode, int pageIndex, int pageSize, out int totalCount);
        /// <summary>
        /// 重新計算查詢(導出）
        /// </summary>
        /// <param name="model"></param>
        /// <param name="workNoList">批量查詢的工號集</param>
        /// <param name="temVal">checkBoxlist集 異常類型</param>
        /// <param name="shiftNo">班別代碼</param>
        /// <param name="ststus">狀態</param>
        /// <returns></returns>
        DataTable GetKaoQinDataList(KQMReGetKaoQinModel model, string workNoList, string temVal, string shiftNo, string status, string fromDate, string toDate, string SQLDep, string depCode);
        /// <summary>
        /// 查詢每月多少日之後不允許重新計算上月及以前考勤數據
        /// </summary>
        /// <returns></returns>
        string GetValueLastDay();
        /// <summary>
        /// 調用存儲過程重新計算查詢部份的員工考勤
        /// </summary>
        /// <param name="WorkNo">員工工號</param>
        /// <param name="roleCode">員工角色 空字符串</param>
        /// <param name="FromKQDate">考勤開始時間</param>
        /// <param name="ToKQDate">考勤結束時間</param>
        void GetKaoQinData(string workNo, string roleCode, string fromKQDate, string toKQDate);
    }
}
