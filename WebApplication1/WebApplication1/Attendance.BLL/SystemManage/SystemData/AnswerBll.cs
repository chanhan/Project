using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemData;
using System.Data;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemData;

namespace GDSBG.MiABU.Attendance.BLL.SystemManage.SystemData
{
    public class AnswerBll : BLLBase<IAnswerDal>
    {
        /// <summary>
        /// 根據實體條件獲取問卷題目數據
        /// </summary>
        /// <param name="model">問卷題目Model</param>
        /// <returns>問卷題目Model集</returns>
        public List<AnswerModel> GetOrderAnswer(AnswerModel model)
        {
            return DAL.GetOrderAnswer(model);
        }
    }
}
