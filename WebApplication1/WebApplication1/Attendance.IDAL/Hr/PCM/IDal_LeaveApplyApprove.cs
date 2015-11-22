using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;
using System.Data;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.IDAL.Hr.PCM
{
    [RefClass("Hr.PCM.Dal_LeaveApplyApprove")]
    public interface IDal_LeaveApplyApprove
    {
        DataTable GetLeaveApplyApproveInfo(string condition, int pageIndex, int pageSize, out int totalCount);
        DataTable GetLeaveApplyApproveInfo(string condition);
        bool SaveDisLeaveAuditData(string BillNo, string AuditMan, string ApRemark,SynclogModel logmodel);
        DataTable GetVDataByCondition(string condition);
        bool SaveZBLHLeaveAuditData(string BillNo, string AuditMan, string ApRemark, string Flow_LevelRemark, SynclogModel logmodel);
        string GetSex(string sEmpNo);
        DataTable GetDataByCondition(string condition);
        string GetValue(string sql);
        string GetYearLeaveDays(string EmployeeNo, string ReportYear, string ApplyDate);
    }
}
