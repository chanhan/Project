using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.HRM.EmployeeData;
using System.Data;

namespace GDSBG.MiABU.Attendance.IDAL.HRM.EmployeeData
{
    [RefClass("HRM.EmployeeData.HrmEmpMoveTempDal")]
    public interface IHrmEmpMoveTempDal
    {
        List<HrmEmpMoveTempModel> SelectEmpTempMove(DataTable dtTemp);
    }
}
