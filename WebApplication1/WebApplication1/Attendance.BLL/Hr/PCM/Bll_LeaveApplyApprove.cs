using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GDSBG.MiABU.Attendance.IDAL.Hr.PCM;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.BLL.Hr.PCM
{
    public class Bll_LeaveApplyApprove : BLLBase<IDal_LeaveApplyApprove>
    {
        public DataTable GetLeaveApplyApproveInfo(string condition, int pageIndex, int pageSize, out int totalCount)
        {
            return DAL.GetLeaveApplyApproveInfo(condition, pageIndex, pageSize, out  totalCount);
        }
        public DataTable GetLeaveApplyApproveInfo(string condition)
        {
            return DAL.GetLeaveApplyApproveInfo(condition);
        }
        public bool SaveDisLeaveAuditData(string BillNo, string AuditMan, string ApRemark,SynclogModel logmodel)
        {
            return DAL.SaveDisLeaveAuditData(BillNo, AuditMan, ApRemark, logmodel);
        }
        public DataTable GetVDataByCondition(string condition)
        {
            return DAL.GetVDataByCondition(condition);
        }
        public bool SaveZBLHLeaveAuditData(string BillNo, string AuditMan, string ApRemark, string Flow_LevelRemark, SynclogModel logmodel)
        {
            return DAL.SaveZBLHLeaveAuditData(BillNo, AuditMan, ApRemark, Flow_LevelRemark, logmodel);
        }
        public string GetSex(string sEmpNo)
        {
            return DAL.GetSex(sEmpNo);
        }
        public DataTable GetDataByCondition(string condition)
        {
            return DAL.GetDataByCondition(condition);
        }
        public string GetValue(string sql)
        {
            return DAL.GetValue(sql);
        }
        public string GetYearLeaveDays(string EmployeeNo, string ReportYear, string ApplyDate)
        {
            return DAL.GetYearLeaveDays(EmployeeNo, ReportYear, ApplyDate);
        }
    }
}
