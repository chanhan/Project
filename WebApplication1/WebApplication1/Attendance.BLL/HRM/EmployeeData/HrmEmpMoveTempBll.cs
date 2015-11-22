using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDSBG.MiABU.Attendance.IDAL.HRM.EmployeeData;
using GDSBG.MiABU.Attendance.Model.HRM.EmployeeData;
using System.Data;

namespace GDSBG.MiABU.Attendance.BLL.HRM.EmployeeData
{
    public class HrmEmpMoveTempBll : BLLBase<IHrmEmpMoveTempDal>
    {

        public  List<HrmEmpMoveTempModel> SelectEmpTempMove(DataTable dtTemp)
        {
            return DAL.SelectEmpTempMove(dtTemp);
        }
    }
}
