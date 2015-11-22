using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.IDAL.WorkFlow;
using System.Data;
using GDSBG.MiABU.Attendance.Model.WorkFlow;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.BLL.WorkFlow
{
    public class Bll_SigningScheduleQuery : BLLBase<IDal_SigningScheduleQuery>
    {
        /// <summary>
        /// 獲取單據類型
        /// </summary>
        /// <returns></returns>
        public DataTable GetBillType()
        {
            return DAL.GetBillType();
        }
        public DataTable GetSignState()
        {
            return DAL.GetSignState();
        }
        public DataTable GetSigningScheduleStatus(string DataType)
        {
            return DAL.GetSigningScheduleStatus(DataType);
        }
        public DataTable GetSigningScheduleInfo(Mod_SigningScheduleQuery model)
        {
            return DAL.GetSigningScheduleInfo(model);
        }
        public DataTable GetSigningScheduleInfo(Mod_SigningScheduleQuery model, int pageIndex, int pageSize, out int totalCount)
        {
            return DAL.GetSigningScheduleInfo(model, pageIndex, pageSize, out totalCount);
        }
        public string GetAllAuditDept(string sDepCode)
        {
            return DAL.GetAllAuditDept(sDepCode);
        }
        public string GetBillTypeCode(string BillTypeNo)
        {
            return DAL.GetBillTypeCode(BillTypeNo);
        }
        public bool SaveBatchDisAuditData(string BillNo, string AuditMan, string BillTypeCode, SynclogModel logmodel)
        {
            return DAL.SaveBatchDisAuditData(BillNo, AuditMan, BillTypeCode, logmodel);
        }
        public bool SaveReSendAuditData(string BillNo, string BillTypeCode, string ApplyMan, string AuditOrgCode, string Flow_LevelRemark, SynclogModel logmodel)
        {
            return DAL.SaveReSendAuditData( BillNo,  BillTypeCode,  ApplyMan,   AuditOrgCode,  Flow_LevelRemark,  logmodel);
        }

        public bool SaveSendNotesData(string BillNo, SynclogModel logmodel)
        {
            return DAL.SaveSendNotesData(BillNo, logmodel);
        }
                
    }

}
