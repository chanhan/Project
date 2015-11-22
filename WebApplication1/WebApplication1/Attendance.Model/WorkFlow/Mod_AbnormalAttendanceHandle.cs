using System;
using System.Collections.Generic;
using System.Text;

namespace GDSBG.MiABU.Attendance.Model.WorkFlow
{
    [Serializable]
    public class Mod_AbnormalAttendanceHandle : ModelBase
    {
        public string DepCode { get; set; }
        public string DepName { get; set; }
        public string IsFillCard { get; set; }
        public string IsSupporter { get; set; }
        public string EmployeeNo { get; set; }
        public string EmpName { get; set; }

        public string AttendanceDateFrom { get; set; }
        public string AttendanceDateTo { get; set; }
        public string AttendHandleStatus { get; set; }
        public string ExceptionType { get; set; }
        public string ShiftNoType { get; set; }
        public string ShiftNoCode { get; set; }
        
        public string CheckBoxFlag { get; set; }
        public string sqlDep { get; set; }
        //public string sysKaoQinDataAbsent { get; set; }
        public string sysKqoQinDays { get; set; }
    }
}
