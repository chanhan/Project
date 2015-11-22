using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemData;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemData;
using System.Data;

namespace GDSBG.MiABU.Attendance.OracleDAL.SystemManage.SystemData
{
    public class AnswerDal : DALBase<AnswerModel>, IAnswerDal
    {
        /// <summary>
        /// 根據實體條件獲取問卷題目數據
        /// </summary>
        /// <param name="model">問卷題目Model</param>
        /// <returns>問卷題目Model集</returns>
        public List<AnswerModel> GetOrderAnswer(AnswerModel model)
        {

            return OrmHelper.SetDataTableToList(DalHelper.Select(model, "ANSWER_SEQ"));
        }
    }
}
