using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.IDAL;
using System.Data;
using GDSBG.MiABU.Attendance.IDAL.WorkFlow;

namespace GDSBG.MiABU.Attendance.BLL.WorkFlow
{
    public class WorkFlowBillChangeAuditFlowBll : BLLBase<IWorkFlowBillChangeAuditFlowDal>
    {
        public void SaveChangeAuditData(string BillNo, DataTable dataTable)
        {
           DAL.SaveChangeAuditData(BillNo,dataTable);
        }

        public DataTable GetAuditStatusDataByCondition(string condition)
        {
            return DAL.GetAuditStatusDataByCondition(condition);
        }
    }
}
