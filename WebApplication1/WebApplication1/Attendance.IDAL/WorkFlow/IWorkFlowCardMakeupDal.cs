/*
 * Copyright (C) 2012 GDSBG MIABU 版權所有。
 * 
 * 檔案名： WorkFLowLimitModel.cs
 * 檔功能描述： 未刷補卡
 * 
 * 版本：1.0
 * 創建標識： 劉小明 2012.02.02
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;

using GDSBG.MiABU.Attendance.Common.Attributes;
using System.Data;
using GDSBG.MiABU.Attendance.Model.WorkFlow;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.IDAL.WorkFlow
{
    [RefClass("WorkFlow.WorkFlowCardMakeupDal")]
    public interface IWorkFlowCardMakeupDal
    {
        /// <summary>
        /// 未刷補卡列表
        /// </summary>
        /// <param name="condition">查詢條件</param>
        /// <param name="pageIndex">頁面索引值</param>
        /// <param name="pageSize">頁面指定數目條數</param>
        /// <param name="totalCount">總記錄數</param>
        /// <param name="parameters">條件</param>
        /// <returns>未刷補卡統計數據</returns>
        DataTable CardMakeupList(string condition, int pageIndex, int pageSize, out int totalCount);

        /// <summary>
        /// 獲取員工信息（暫時不增加權限管控）
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <param name="sqlDep">權限</param>
        /// <returns></returns>
        DataTable GetVData(string EmployeeNo, string sqlDep);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="workno"></param>
        /// <param name="kqdate"></param>
        /// <param name="cardtime"></param>
        /// <param name="makeuptype"></param>
        /// <param name="status"></param>
        /// <param name="approver"></param>
        /// <param name="apremark"></param>
        /// <param name="approvedate"></param>
        /// <param name="billno"></param>
        /// <param name="modifier"></param>
        /// <param name="modifydate"></param>
        /// <param name="reasontype"></param>
        /// <param name="reasonremark"></param>
        /// <param name="decsalary"></param>
        /// <returns></returns>
        bool modifyCardMakeupInfo(string flag, string workno, string kqdate, string cardtime, string makeuptype, string status, string approver, string apremark, string approvedate, string billno, string modifier, string modifydate, string reasontype, string reasonremark, string decsalary);

        /// <summary>
        /// 保存數據
        /// </summary>
        /// <param name="processFlag">標志位</param>
        /// <param name="dataTable">數據表</param>
        void SaveData(string processFlag, DataTable dataTable);

        /// <summary>
        /// 簽核
        /// </summary>
        /// <param name="dataTable"></param>
        void Audit(DataTable dataTable);

        /// <summary>
        /// 取消簽核
        /// </summary>
        /// <param name="dataTable"></param>
        void CancelAudit(DataTable dataTable);

        /// <summary>
        /// 刪除數據
        /// </summary>
        /// <param name="dataTable"></param>
        void DeleteData(DataTable dataTable);

        /// <summary>
        /// 查詢數據
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        DataTable GetDataByCondition(string condition);

        DataTable GetDataByCondition(string condition, int startRow, int endRow);

        DataTable GetMXDataByCondition(string condition);

        DataTable GetMXDataByCondition(string condition, int pageSize, int out_CurrentPage, int out_TotalPage, out int out_TotalRecords);

        string SaveAuditData(string processFlag, string ID, string BillNoType, string AuditOrgCode);

        string GetValue(string SQL);

        string ReturnCardTime(string WorkNo, string KQDate, string dCardTime, string ShiftNo, string MakeType);

        DataTable KQMExceptionList(string condition);

        DataTable GetKaoQinDataByCondition(string condition);

        DataTable getTBDataByCondition(string condition);

        int GetVWorkNoCount(string WorkNo, string sqlDep);

        string GetWorkFlowOrgCode(string OrgCode, string BillTypeCode);

        void SaveData(string BillNo, string OrgCode, string ApplyMan);

        void SaveAuditStatusData(string BillNo, string OrgCode, string BillTypeCode);

        int SaveOrgAuditData(string processFlag, Dictionary<string, List<string>> diry, string BillNoType, string BillTypeCode, string Person, SynclogModel logmodel);

        List<WorkFlowCardMakeupModel> GetList(DataTable dt);

        DataTable CardMakeupListNoPage(string condition);
         
       
        bool SaveAuditData_new(string WorkNo, string BillNoType, string BillTypeCode, string ApplyMan, string AuditOrgCode, string Flow_LevelRemark,  SynclogModel logmodel);
    }
}
