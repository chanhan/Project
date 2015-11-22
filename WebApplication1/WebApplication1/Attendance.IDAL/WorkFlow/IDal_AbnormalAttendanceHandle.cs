using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;
using System.Data;
using GDSBG.MiABU.Attendance.Model.WorkFlow;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.IDAL.WorkFlow
{
    [RefClass("WorkFlow.Dal_AbnormalAttendanceHandle")]
    public interface IDal_AbnormalAttendanceHandle
    {
        /// <summary>
        /// 獲取必簽人員信息列表
        /// </summary>
        /// <param name="deptcode"></param>
        /// <param name="formtype"></param>
        /// <param name="resonlist"></param>
        /// <returns></returns>
        DataTable GetAbnormalAttendanceInfo(Mod_AbnormalAttendanceHandle model, int pageIndex, int pageSize, out int totalCount);
        DataTable GetAbnormalAttendanceInfo(Mod_AbnormalAttendanceHandle model);
        DataTable GetAbnormalAttendanceInfo(string EmployeeNo, string KqDate, string sysKqoQinDays, string sysKaoQinDataAbsent);
        DataTable GetAbnormalAttendanceInfo(string EmployeeNo, string KqDate);
        DataTable GetSignAbnormalAttendanceInfo(string BillNo, string Status);
        string GetExceptionqty(string EmployeeNo, string KqDate);
        int GetVWorkNoCount(string EmployeeNo, string DCode);
        string GetSysKaoqinDataAbsent();
        string GetWorkFlowOrgCode(string DepCode, string BillTypeCode, string reason1);
        DataTable GetBellCardData(string WorkNo, string KQDate, string ShiftNo);
        DataTable GetAbnormalAttendanceHandleStatus(string DataType);
        DataTable ExceltoDataView(string strFilePath);
        string GetsysKqoQinDays(string CompanyId, string RoleCode);

        bool KQMSaveAbnormalAttendanceInfo(string processFlag, DataTable dataTable, string appUser, SynclogModel logmodel);
        bool KQMSaveAuditData(string WorkNo, string KQDate, string BillNoType, string BillTypeCode, string ApplyMan, string AuditOrgCode, string Flow_LevelRemark, SynclogModel logmodel);
        bool WFMSaveData(string BillNo, string OrgCode, string ApplyMan, SynclogModel logmodel);
        bool WFMSaveAuditStatusData(string BillNo, string OrgCode, string BillTypeCode, string SendUser, string Flow_LevelRemark, SynclogModel logmodel);
        int SaveOrgAuditData(string processFlag, Dictionary<string, List<string>> diry, string BillNoType, string BillTypeCode, string Person, string Flow_LevelRemark, SynclogModel logmodel);
    }
}
