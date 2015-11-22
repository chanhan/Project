using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemData;
using System.Data;

namespace GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemData
{
    /// <summary>
    /// 功能管理數據操作接口
    /// </summary>
    [RefClass("SystemManage.SystemData.AnswerDal")]
    public interface IAnswerDal
    {
        /// <summary>
        /// 根據實體條件獲取問卷題目數據
        /// </summary>
        /// <param name="model">問卷題目Model</param>
        /// <returns>問卷題目Model集</returns>
        List<AnswerModel> GetOrderAnswer(AnswerModel model);
    }
}
