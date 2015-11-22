using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GDSBG.MiABU.Attendance.Model.WorkFlow;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.IDAL.WorkFlow
{
    [RefClass("WorkFlow.Dal_SigningScheduleQuery")]
    public interface IDal_SigningScheduleQuery
    {
        /// <summary>
        /// 單據類型
        /// </summary>
        /// <returns></returns>
        DataTable GetBillType();
        DataTable GetSignState();
        DataTable GetSigningScheduleStatus(string DataType);
        DataTable GetSigningScheduleInfo(Mod_SigningScheduleQuery model, int pageIndex, int pageSize, out int totalCount);
        DataTable GetSigningScheduleInfo(Mod_SigningScheduleQuery model);
        string GetAllAuditDept(string sDepCode);
        string GetBillTypeCode(string BillTypeNo);
        bool SaveBatchDisAuditData(string BillNo, string AuditMan, string BillTypeCode,SynclogModel logmodel);
        //bool SaveReSendAuditData(string BillNo, string BillTypeCode, SynclogModel logmodel);
        bool SaveReSendAuditData(string BillNo, string BillTypeCode, string ApplyMan, string AuditOrgCode, string Flow_LevelRemark, SynclogModel logmodel);
        bool SaveSendNotesData(string BillNo, SynclogModel logmodel);
    }

}
