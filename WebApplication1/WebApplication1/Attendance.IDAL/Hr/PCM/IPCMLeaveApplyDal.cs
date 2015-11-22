/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IPCMLeaveApplyDal.cs
 * 檔功能描述： 個人中心請假申請接口類
 * 
 * 版本：1.0
 * 創建標識： 陳函 2012.3.10
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.IDAL.Hr.PCM
{
    [RefClass("Hr.PCM.PCMLeaveApplyDal")]

    public interface IPCMLeaveApplyDal
    {
        /// <summary>
        /// 獲取個人請假信息
        /// </summary>
        /// <param name="user">請假人工號</param>
        /// <param name="billNo">申請單號</param>
        /// <param name="LVTypeCode">請假類別</param>
        /// <param name="status">表單狀態</param>
        /// <param name="startDate">開始日期</param>
        /// <param name="endDate">結束日期</param>
        /// <param name="applyType">申請類別</param>
        /// <param name="currentPageIndex">當前頁數</param>
        /// <param name="pageSize">每頁顯示的最大記錄數</param>
        /// <param name="totalCount">總記錄數</param>
        /// <returns>個人請假信息</returns>
        DataTable getApplyData(string user, string billNo, string LVTypeCode, string status, string startDate, string endDate, string applyType, int currentPageIndex, int pageSize, out int totalCount);
        /// <summary>
        /// 獲取員工基本信息
        /// </summary>
        /// <param name="workNo">員工工號</param>
        /// <returns>員工基本信息</returns>
        DataTable getEmployeeDataByCondition(string workNo);
        /// <summary>
        /// 根據性別獲取員工能請的假別
        /// </summary>
        /// <param name="marrystate">是否已婚</param>
        /// <param name="sexCode">性別代碼</param>
        /// <returns>員工能請的假別</returns>
        DataTable getKQMLeaveTypeData(string marrystate, string sexCode);
        /// <summary>
        /// 獲取代理人的基本信息
        /// </summary>
        /// <param name="workNo">代理人工號</param>
        /// <param name="localName">代理人姓名</param>
        /// <returns>代理人的基本信息</returns>
        DataTable getAuditData(string workNo, string localName);
        /// <summary>
        /// 獲取提示窗口信息
        /// </summary>
        /// <param name="workNo">登陸人工號</param>
        /// <returns>登錄人員工基本信息</returns>
        DataTable GetDataSetBySQL(string workNo);
        /// <summary>
        /// 獲取員工基本信息
        /// </summary>
        /// <param name="employeeNo">員工工號</param>
        /// <returns>員工基本信息</returns>
        DataTable getVDataByCondition(string employeeNo);

        /// <summary>
        /// 根據ID獲取請假信息
        /// </summary>
        /// <param name="id">請假信息ID</param>
        /// <returns>請假信息</returns>
        DataTable getDataById(string id);
        /// <summary>
        /// 新增短信通知記錄
        /// </summary>
        /// <param name="window">窗口類型</param>
        /// <param name="remindContent">短信類容</param>
        /// <param name="logmodel">操作日誌</param>
        void ExcuteSQL(string window, string remindContent,SynclogModel logmodel);
        /// <summary>
        /// 根據工號獲取請假信息
        /// </summary>
        /// <param name="id">請假人工號</param>
        /// <returns>請假信息</returns>
        DataTable getVDataByWorkNo(string workNo);

      //  DataTable getApptypetoBillConfig(string LVTypeCode);

        //DataTable getBillTypeCode(string LVTypeCode);
        /// <summary>
        /// 是否允許個人申請此假別
        /// </summary>
        /// <param name="lvTypeCode">請假類別代碼</param>
        /// <returns>是否允許申請此假別</returns>
        DataTable iSAllowPCM(string lvTypeCode);
    }
}
