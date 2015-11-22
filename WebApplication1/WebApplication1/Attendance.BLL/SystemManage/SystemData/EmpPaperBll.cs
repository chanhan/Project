using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemData;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemData;

namespace GDSBG.MiABU.Attendance.BLL.SystemManage.SystemData
{
    public class EmpPaperBll : BLLBase<IEmpPaperDal>
    {
        /// <summary>
        /// 根據實體條件獲取答卷數據
        /// </summary>
        /// <param name="model">答卷Model</param>
        /// <returns>答卷Model集</returns>
        public List<EmpPaperModel> GetPaperAnswer(int paperSeq, string empNo)
        {
            EmpPaperModel model = new EmpPaperModel();
            model.PaperSeq = paperSeq;
            model.EmpNo = empNo;
            return DAL.GetPaperAnswer(model);
        }
    }
}
