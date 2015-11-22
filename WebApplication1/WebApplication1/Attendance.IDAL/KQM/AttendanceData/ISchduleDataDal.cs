/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ISchduleDataDal.cs
 * 檔功能描述： 排班數據操作接口
 * 
 * 版本：1.0
 * 創建標識：劉炎 2011.12.19
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
    /// 排班數據操作接口
    /// </summary>
    [RefClass("KQM.AttendanceData.SchduleDataDal")]
    public interface ISchduleDataDal
    {
        /// <summary>
        /// 根據排班時間，在職狀態獲取排班作業集
        /// </summary>
        /// <param name="condition">條件</param>
        /// <param name="schduleDate">排班時間</param>
        /// <param name="status">在職狀態</param>
        /// <returns>排班作業Model集</returns>
        List<ScheduleDataModel> GetSchduleInfoList(Nullable<DateTime> schduleDate, string status, ScheduleDataModel model);

        /// <summary>
        /// 根據排班時間，在職狀態獲取排班作業分頁集
        /// </summary>
        /// <param name="schduleDate">排班時間</param>
        /// <param name="status">在職狀態</param>
        /// <param name="model">排班</param>
        /// <param name="pageIndex">頁索引</param>
        /// <param name="pageSize">頁面大小</param>
        /// <param name="totalCount">頁總數</param>
        /// <returns>排班作業Model集</returns>
        List<ScheduleDataModel> GetPagerScheduleInfoList(Nullable<DateTime> schduleDate, string status, ScheduleDataModel model, int pageIndex, int pageSize, out int totalCount);

        /// <summary>
        /// 反饋導入信息
        /// </summary>
        /// <param name="workno">工號</param>
        /// <param name="successNum">成功數量</param>
        /// <param name="errorNum">錯誤數量</param>
        /// <returns>導入信息</returns>
        DataTable GetImportCount(string workno, out int successNum, out int errorNum);

        /// <summary>
        /// 獲取導入人的所有信息
        /// </summary>
        /// <param name="createUser">導入人</param>
        /// <returns>排班集</returns>
        List<ScheduleDataModel> GetImportData(string createUser);

        /// <summary>
        /// 根據Model獲取排班集
        /// </summary>
        /// <param name="model">排班Model</param>
        /// <returns>排班集</returns>
        List<ScheduleDataModel> GetShiftInfo(ScheduleDataModel model);

        /// <summary>
        /// 向數據庫中插入一條數據
        /// </summary>
        /// <param name="model"></param>
        /// <returns>是否成功</returns>
        bool AddShiftInfo(ScheduleDataModel model);

         /// <summary>
        /// 修改數據
        /// </summary>
        /// <param name="model"></param>
        /// <returns>是否成功</returns>
        bool EditShiftInfo(ScheduleDataModel model);
    }
}
