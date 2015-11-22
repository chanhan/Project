/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IHrmEmpPremiumInfoModel.cs
 * 檔功能描述：員工行政處分接口類
 * 
 * 版本：1.0
 * 創建標識： 劉炎 2012.03.12
 * 
 */
using System;
using System.Data;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.HRM.EmployeeData;

namespace GDSBG.MiABU.Attendance.IDAL.HRM.EmployeeData
{
    /// <summary>
    /// 員工行政處分接口類
    /// </summary>
    [RefClass("HRM.EmployeeData.HrmEmpPremiumInfoDal")]
    public interface IHrmEmpPremiumInfoDal
    {
        /// <summary>
        /// 根據條件查詢員工檢查信息
        /// </summary>
        /// <param name="empNoList">所要查詢員工號</param>
        /// <param name="model">獎懲信息數據集</param>
        /// <param name="startDate">開始時間</param>
        /// <param name="endDate">結束時間</param>
        /// <param name="deptList">所要查詢部門</param>
        /// <param name="jobSituationCode">在職狀態</param>
        /// <param name="pageIndex">頁面索引</param>
        /// <param name="pageSize">頁面大小</param>
        /// <param name="totalCount">頁面總數</param>
        /// <returns>員工獎懲信息Model集</returns>
        List<HrmEmpPremiumInfoModel> GetEmpPremiumInfo(string empNoList, HrmEmpPremiumInfoModel model, DateTime startDate, DateTime endDate, string deptList, string jobSituationCode, int pageIndex, int pageSize, out int totalCount);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        DataTable GetPremiumName();

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //DataTable GetJobStatus();
    }
}
