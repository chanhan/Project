/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： SchduleDatatBll.cs
 * 檔功能描述： 排班作業業務邏輯類
 * 
 * 版本：1.0
 * 創建標識：劉炎 2011.12.19
 * 
 */

using System.Collections.Generic;
using GDSBG.MiABU.Attendance.IDAL.KQM.AttendanceData;
using GDSBG.MiABU.Attendance.Model.KQM.AttendanceData;
using System.Data;
using System.Collections;
using System;
namespace GDSBG.MiABU.Attendance.BLL.KQM.AttendanceData
{
    /// <summary>
    /// 排班作業業務邏輯類
    /// </summary>
    public class SchduleDataBll : BLLBase<ISchduleDataDal>
    {
        /// <summary>
        /// 根據排班時間，在職狀態獲取排班作業集
        /// </summary>
        /// <param name="condition">條件</param>
        /// <param name="schduleDate">排班時間</param>
        /// <param name="status">在職狀態</param>
        /// <returns>排班作業Model集</returns>
        public List<ScheduleDataModel> GetSchduleInfoList(string scheduleDate, string status, ScheduleDataModel model)
        {
            Nullable<DateTime> dateschedule = string.IsNullOrEmpty(scheduleDate) ? DateTime.MinValue : Convert.ToDateTime(scheduleDate);
            return DAL.GetSchduleInfoList(dateschedule, status, model);
        }

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
        public List<ScheduleDataModel> GetPagerSchduleInfoList(string schduleDate, string status, ScheduleDataModel model, int pageIndex, int pageSize, out int totalCount)
        {
            Nullable<DateTime> dateSchedule = string.IsNullOrEmpty(schduleDate) ? DateTime.MinValue : Convert.ToDateTime(schduleDate);
            return DAL.GetPagerScheduleInfoList(dateSchedule, status, model, pageIndex, pageSize, out totalCount);
        }

        /// <summary>
        /// 反饋導入信息
        /// </summary>
        /// <param name="workno">工號</param>
        /// <param name="successNum">成功數量</param>
        /// <param name="errorNum">錯誤數量</param>
        /// <returns>導入信息</returns>
        public DataTable GetImportCount(string workNo, out int successNum, out int errorNum)
        {
            return DAL.GetImportCount(workNo, out successNum, out errorNum);
        }

        /// <summary>
        /// 獲取導入人的所有信息
        /// </summary>
        /// <param name="createUser">導入人</param>
        /// <returns>排班集</returns>
        public List<ScheduleDataModel> GetImportDate(string createUser)
        {
            return DAL.GetImportData(createUser);
        }

        /// <summary>
        /// 根據Model獲取排班集
        /// </summary>
        /// <param name="model">排班Model</param>
        /// <returns>排班集</returns>
        public List<ScheduleDataModel> GetShiftInfo(ScheduleDataModel model)
        {
            return DAL.GetShiftInfo(model);
        }
    }
}
