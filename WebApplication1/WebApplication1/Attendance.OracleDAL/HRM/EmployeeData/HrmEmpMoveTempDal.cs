using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.IDAL.HRM.EmployeeData;
using GDSBG.MiABU.Attendance.Model.HRM.EmployeeData;
using GDSBG.MiABU.Attendance.Common.Attributes;
using System.Data;

namespace GDSBG.MiABU.Attendance.OracleDAL.HRM.EmployeeData
{
    public class HrmEmpMoveTempDal : DALBase<HrmEmpMoveTempModel>, IHrmEmpMoveTempDal
    {
        public List<HrmEmpMoveTempModel> SelectEmpTempMove(DataTable dtTemp)
        {
            return OrmHelper.SetDataTableToList(dtTemp);
        }

    }
}
