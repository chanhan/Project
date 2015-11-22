using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GDSBG.MiABU.Attendance.Model.WorkFlow;
using GDSBG.MiABU.Attendance.IDAL.WorkFlow;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.BLL.WorkFlow
{

    public class Bll_AbnormalAttendanceHandle : BLLBase<IDal_AbnormalAttendanceHandle>
    {
        public DataTable GetAbnormalAttendanceInfo(Mod_AbnormalAttendanceHandle model, int pageIndex, int pageSize, out int totalCount)
        {
            return DAL.GetAbnormalAttendanceInfo(model, pageIndex, pageSize, out  totalCount);
        }
        public DataTable GetAbnormalAttendanceInfo(Mod_AbnormalAttendanceHandle model)
        {
            return DAL.GetAbnormalAttendanceInfo(model);
        }
        public DataTable GetAbnormalAttendanceInfo(string EmployeeNo, string KqDate, string sysKqoQinDays, string sysKaoQinDataAbsent)
        {
            return DAL.GetAbnormalAttendanceInfo(EmployeeNo, KqDate, sysKqoQinDays, sysKaoQinDataAbsent);
        }
        public DataTable GetAbnormalAttendanceInfo(string EmployeeNo, string KqDate)
        {
            return DAL.GetAbnormalAttendanceInfo(EmployeeNo,KqDate);
        }
        public string GetExceptionqty(string EmployeeNo, string KqDate)
        {
            return DAL.GetExceptionqty(EmployeeNo, KqDate);
        }
        public string GetSysKaoqinDataAbsent()
        {
            return DAL.GetSysKaoqinDataAbsent();
        }
        public int GetVWorkNoCount(string EmployeeNo, string DCode)
        {
            return DAL.GetVWorkNoCount(EmployeeNo, DCode);
        }
        public string GetWorkFlowOrgCode(string DepCode, string BillTypeCode, string reason1)
        {
            return DAL.GetWorkFlowOrgCode(DepCode, BillTypeCode, reason1);
        }
        public DataTable GetBellCardData(string WorkNo, string KQDate, string ShiftNo)
        {
            return DAL.GetBellCardData(WorkNo, KQDate, ShiftNo);
        }
        public DataTable GetAbnormalAttendanceHandleStatus(string DataType)
        {
            return DAL.GetAbnormalAttendanceHandleStatus(DataType);
        }
        public DataTable ExceltoDataView(string strFilePath)
        {
            return DAL.ExceltoDataView(strFilePath);
        }
        public string GetsysKqoQinDays(string CompanyId, string RoleCode)
        {
            return DAL.GetsysKqoQinDays(CompanyId, RoleCode);
        }

        public bool KQMSaveAbnormalAttendanceInfo(string processFlag, DataTable dataTable, string appUser, SynclogModel logmodel)
        {
            return DAL.KQMSaveAbnormalAttendanceInfo(processFlag, dataTable, appUser, logmodel);
        }
        public bool KQMSaveAuditData(string WorkNo, string KQDate, string BillNoType, string BillTypeCode, string ApplyMan, string AuditOrgCode, string Flow_LevelRemark, SynclogModel logmodel)
        {
            return DAL.KQMSaveAuditData( WorkNo,  KQDate,  BillNoType,  BillTypeCode,  ApplyMan,  AuditOrgCode,  Flow_LevelRemark,  logmodel);
        }
        public bool WFMSaveData(string BillNo, string OrgCode, string ApplyMan, SynclogModel logmodel)
        {
            return DAL.WFMSaveData(BillNo, OrgCode, ApplyMan, logmodel);
        }
        public bool WFMSaveAuditStatusData(string BillNo, string OrgCode, string BillTypeCode, string SendUser, string Flow_LevelRemark, SynclogModel logmodel)
        {
            return DAL.WFMSaveAuditStatusData(BillNo, OrgCode, BillTypeCode, SendUser, Flow_LevelRemark, logmodel);
        }
        public DataTable GetSignAbnormalAttendanceInfo(string BillNo, string Status)
        {
            return DAL.GetSignAbnormalAttendanceInfo(BillNo, Status);
        }
        public int SaveOrgAuditData(string processFlag, Dictionary<string, List<string>> diry, string BillNoType, string BillTypeCode, string Person, string Flow_LevelRemark, SynclogModel logmodel)
        {
            return DAL.SaveOrgAuditData(processFlag, diry, BillNoType, BillTypeCode, Person,  Flow_LevelRemark, logmodel);
        }
    }
}
