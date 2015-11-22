using System;
using System.Collections.Generic; 
using System.Text;
using GDSBG.MiABU.Attendance.Model.WorkFlow;
using GDSBG.MiABU.Attendance.Common.Attributes;
using System.Data;

namespace GDSBG.MiABU.Attendance.IDAL.WorkFlow
{
    [RefClass("WorkFlow.WorkFlowBillChangeAuditFlowDal")]
    public interface IWorkFlowBillChangeAuditFlowDal
    {
         void SaveChangeAuditData(string BillNo, DataTable dataTable);

         DataTable GetAuditStatusDataByCondition(string condition);
    }

}
