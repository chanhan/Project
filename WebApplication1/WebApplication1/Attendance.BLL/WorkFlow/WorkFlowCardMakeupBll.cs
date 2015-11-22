using System;
using System.Collections.Generic;
using System.Text;

using GDSBG.MiABU.Attendance.IDAL.WorkFlow;
using System.Data;
using GDSBG.MiABU.Attendance.Model.WorkFlow;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.BLL.WorkFlow
{
    public class WorkFlowCardMakeupBll:BLLBase<IWorkFlowCardMakeupDal>
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
        public DataTable getCardMakeupList(string condition, int pageIndex, int pageSize, out int totalCount)
        {
            return DAL.CardMakeupList(condition, pageIndex, pageSize, out totalCount);
        }


        /// <summary>
        /// 獲取員工信息（暫時不增加權限管控）
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <param name="sqlDep">權限</param>
        /// <returns></returns>
        public DataTable GetVData(string EmployeeNo, string sqlDep)
        {
            return DAL.GetVData(EmployeeNo, sqlDep);
        }

        public bool modifyCardMakeupInfo(string flag, string workno, string kqdate, string cardtime, string makeuptype, string status, string approver, string apremark, string approvedate, string billno, string modifier, string modifydate, string reasontype, string reasonremark, string decsalary)
        {
             return DAL.modifyCardMakeupInfo(flag, workno, kqdate, cardtime, makeuptype, status, approver, apremark, approvedate, billno, modifier, modifydate, reasontype, reasonremark, decsalary);
        }


        public void SaveData(string processFlag, DataTable dataTable)
        {
            DAL.SaveData(processFlag, dataTable);
        }
 

        public void Audit(DataTable dataTable)
        {
            DAL.Audit(dataTable);
        }

        public void CancelAudit(DataTable dataTable)
        {
            DAL.CancelAudit(dataTable);
        }

        public void DeleteData(DataTable dataTable)
        {
             DAL.DeleteData(dataTable);
        }

        public DataTable GetDataByCondition(string condition)
        {
            return DAL.GetDataByCondition(condition);
        }

        public DataTable GetDataByCondition(string condition, int startRow, int endRow)
        {
            return DAL.GetDataByCondition(condition,startRow,endRow);
        }

        public DataTable GetMXDataByCondition(string condition)
        {
            return DAL.GetMXDataByCondition(condition);
        }

        public DataTable GetMXDataByCondition(string condition, int pageSize, int out_CurrentPage, int out_TotalPage, out int out_TotalRecords)
        {
         return DAL.GetMXDataByCondition( condition,  pageSize,  out_CurrentPage,  out_TotalPage, out out_TotalRecords);
        }

        public string SaveAuditData(string processFlag, string ID, string BillNoType, string AuditOrgCode)
        {
            return DAL.SaveAuditData(processFlag, ID, BillNoType, AuditOrgCode);
        }

        /// <summary>
        /// 根據指定的SQL語句獲取值
        /// </summary>
        /// <param name="SQL">SQL查詢語句</param>
        /// <returns>返回查詢的string對象值</returns>
        public string GetValue(string SQL)
        {
            return DAL.GetValue(SQL);
        }


        /// <summary>
        /// 返回刷卡時間
        /// </summary>
        /// <param name="WorkNo">工號</param>
        /// <param name="KQDate">考勤日</param>
        /// <param name="dCardTime">刷卡時間</param>
        /// <param name="ShiftNo">班別</param>
        /// <param name="MakeType">類型</param>
        /// <returns>刷卡時間</returns>
        public string ReturnCardTime(string WorkNo, string KQDate, string dCardTime, string ShiftNo, string MakeType)
        {
            return DAL.ReturnCardTime(WorkNo, KQDate, dCardTime, ShiftNo, MakeType);
        }

        /// <summary>
        /// 考勤異常信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DataTable KQMExceptionList(string condition)
        {
            return DAL.KQMExceptionList(condition);
        }

        /// <summary>
        /// 獲取考勤記錄信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DataTable GetKaoQinDataByCondition(string condition)
        {
            return DAL.GetKaoQinDataByCondition(condition);
        }


        public DataTable getTBDataByCondition(string condition)
        {
            return DAL.getTBDataByCondition(condition);
        }

        public int GetVWorkNoCount(string WorkNo, string sqlDep)
        {
            return DAL.GetVWorkNoCount(WorkNo, sqlDep);
        }

        public string GetWorkFlowOrgCode(string OrgCode, string BillTypeCode)
        {
            return DAL.GetWorkFlowOrgCode(OrgCode,BillTypeCode);
        }

        public void SaveData(string BillNo, string OrgCode, string ApplyMan)
        { 
            DAL.SaveData(BillNo, OrgCode, ApplyMan);
        }

        public void SaveAuditStatusData(string BillNo, string OrgCode, string BillTypeCode)
        {
            DAL.SaveAuditStatusData(BillNo, OrgCode, BillTypeCode);
        }

        public int SaveOrgAuditData(string processFlag, Dictionary<string, List<string>> diry, string BillNoType, string BillTypeCode, string Person, SynclogModel logmodel)
        {
            return DAL.SaveOrgAuditData(processFlag, diry, BillNoType, BillTypeCode, Person, logmodel);
        
        }

        public List<WorkFlowCardMakeupModel> GetList(DataTable dt)
        {
            return DAL.GetList(dt);
        }

        public DataTable CardMakeupListNoPage(string condition)
        {
            return DAL.CardMakeupListNoPage(condition);
        }
        public bool SaveAuditData_new(string WorkNo, string BillNoType, string BillTypeCode, string ApplyMan, string AuditOrgCode, string Flow_LevelRemark,   SynclogModel logmodel)
        {
            return DAL.SaveAuditData_new(WorkNo,  BillNoType,  BillTypeCode,  ApplyMan,  AuditOrgCode,  Flow_LevelRemark,   logmodel);
        }
    }
}
