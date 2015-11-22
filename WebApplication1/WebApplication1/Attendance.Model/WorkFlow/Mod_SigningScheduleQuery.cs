using System;
using System.Collections.Generic;
using System.Text;

namespace GDSBG.MiABU.Attendance.Model.WorkFlow
{
    [Serializable]
    public class Mod_SigningScheduleQuery
    {
        public string DepCode { get; set; }
        public string DepName { get; set; }

        public string Privileged { get; set; }
        public string sqlDep { get; set; }
        public string BillTypeCode { get; set; }

        public string BillNo { get; set; }
        public string Status { get; set; }
        public string ApplyDateFrom { get; set; }

        public string ApplyDateTo { get; set; }

        public string AttendanceDateTo { get; set; }
        public string AuditMan { get; set; }

        public string ApplyMan { get; set; }

    }
}
