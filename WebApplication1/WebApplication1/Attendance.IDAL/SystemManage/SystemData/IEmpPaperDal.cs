using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemData;
using System.Data;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemData
{ /// <summary>
    /// 答題數據操作接口
    /// </summary>
    [RefClass("SystemManage.SystemData.EmpPaperDal")]
    public interface IEmpPaperDal
    {
        /// <summary>
        /// 根據實體條件獲取答卷數據
        /// </summary>
        /// <param name="model">答卷Model</param>
        /// <returns>答卷Model集</returns>
        List<EmpPaperModel> GetPaperAnswer(EmpPaperModel model);

    }
}
