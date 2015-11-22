/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IKQMLeaveApplyExportDal.cs
 * 檔功能描述： 請假申請數據導出接口類
 * 
 * 版本：1.0
 * 創建標識： 陳函 2012.3.10
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.KQM.KaoQinData;
using System.Data;

namespace GDSBG.MiABU.Attendance.IDAL.Hr.KQM.KaoQinData
{
    [RefClass("Hr.KQM.KaoQinData.KQMLeaveApplyExportDal")]
    public interface IKQMLeaveApplyExportDal
    {        /// <summary>
        /// 獲取請假信息用於導出Excel
        /// </summary>
        /// <param name="Privileged">是否有組織權限</param>
        /// <param name="sqlDep">組織權限管控</param>
        /// <param name="depName">部門名稱</param>
        /// <param name="billNo">單據編號</param>
        /// <param name="workNo">工號 </param>
        /// <param name="localName">姓名 </param>
        /// <param name="LVTypeCode">請假類別 </param>
        /// <param name="status">表單狀態 </param>
        /// <param name="testify">繳交證明 </param>
        /// <param name="startDate">開始日期</param>
        /// <param name="endDate">截止日期</param>
        /// <param name="applyStartDate">申請開始日期</param>
        /// <param name="applyEndDate">申請截止日期</param>
        /// <param name="applyType">申請類別</param>
        /// <param name="flag">是否查詢因班別變動導致請假時數有問題數據 </param>
        /// <param name="IsLastYear">是否補休 </param>
        /// <returns>請假信息</returns>
        List<LeaveApplyViewModel> getApplyData(bool Privileged, string SqlDep, string depName, string billNo, string workNo, string localName, string LVTypeCode, string status, string testify, string startDate, string endDate, string applyStartDate, string applyEndDate, string applyType, bool flag, string IsLastYear);
        /// <summary>
        /// 根據model獲取請假信息
        /// </summary>
        /// <param name="leaveApplyViewModel">請假信息model</param>
        /// <returns>請假信息</returns>
        DataTable getLeaveApply(LeaveApplyViewModel leaveApplyViewModel);
        /// <summary>
        /// 獲取個人請假信息，用於導出Excel
        /// </summary>
        /// <param name="user">請假人工號</param>
        /// <param name="billNo">申請單號</param>
        /// <param name="LVTypeCode">請假類別</param>
        /// <param name="status">表單狀態</param>
        /// <param name="startDate">開始日期</param>
        /// <param name="endDate">結束日期</param>
        /// <param name="applyType">申請類別</param>
        /// <returns>個人請假信息</returns>
        List<LeaveApplyViewModel> getApplyList(string personCode, string billNo, string LVTypeCode, string status, string startDate, string endDate, string applyType);
    }
}
