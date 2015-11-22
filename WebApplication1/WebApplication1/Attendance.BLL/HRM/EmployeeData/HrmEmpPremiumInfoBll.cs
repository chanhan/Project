/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： HrmEmpPremiumInfoModel.cs
 * 檔功能描述：員工行政處分業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 劉炎 2012.03.12
 * 
 */

using System;
using System.Data;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.IDAL.HRM.EmployeeData;
using GDSBG.MiABU.Attendance.Model.HRM.EmployeeData;


namespace GDSBG.MiABU.Attendance.BLL.HRM.EmployeeData
{
    /// <summary>
    /// 員工行政處分業務邏輯類
    /// </summary>
    public class HrmEmpPremiumInfoBll : BLLBase<IHrmEmpPremiumInfoDal>
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
        public List<HrmEmpPremiumInfoModel> GetEmpPremiumInfo(string empNoList, HrmEmpPremiumInfoModel model, string startDate, string endDate, string deptList, string jobSituationCode, int pageIndex, int pageSize, out int totalCount)
        {
            DateTime dateStart = DateTime.MinValue, dateEnd = DateTime.MaxValue;
            if (!string.IsNullOrEmpty(startDate))
            {
                dateStart = Convert.ToDateTime(startDate);
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                dateEnd = Convert.ToDateTime(endDate);
            }
            return DAL.GetEmpPremiumInfo(empNoList, model, dateStart, dateEnd, deptList, jobSituationCode, pageIndex, pageSize, out totalCount);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetPremiumName()
        {
            return DAL.GetPremiumName();
        }
    }
}
