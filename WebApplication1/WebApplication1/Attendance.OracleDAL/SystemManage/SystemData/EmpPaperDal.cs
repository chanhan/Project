using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemData;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemData;
using System.Data;

namespace GDSBG.MiABU.Attendance.OracleDAL.SystemManage.SystemData
{
    public class EmpPaperDal : DALBase<EmpPaperModel>, IEmpPaperDal
    {
        /// <summary>
        /// 根據實體條件獲取答卷數據
        /// </summary>
        /// <param name="model">答卷Model</param>
        /// <returns>答卷Model集</returns>
        public List<EmpPaperModel> GetPaperAnswer(EmpPaperModel model)
        {
            return OrmHelper.SetDataTableToList(DalHelper.Select(model));
        }
    }
}
