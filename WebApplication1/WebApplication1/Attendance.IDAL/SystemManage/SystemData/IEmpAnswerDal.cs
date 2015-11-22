using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemData;
using GDSBG.MiABU.Attendance.Common.Attributes;
using System.Data;

namespace GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemData
{/// <summary>
    /// 功能管理數據操作接口
    /// </summary>
    [RefClass("SystemManage.SystemData.EmpAnswerDal")]
    public interface IEmpAnswerDal
    {
        /// <summary>
        /// 根據實體條件獲取問卷答題數據
        /// </summary>
        /// <param name="model">問卷答題Model</param>
        /// <returns>問卷答題Model集</returns>
        List<EmpAnswerModel> GetAnswer(EmpAnswerModel model);

        /// <summary>
        /// 保存問卷調查答卷內容
        /// </summary>
        /// <param name="empNo">工號</param>
        /// <param name="paperSeq">問卷序號</param>
        /// <param name="epStatus">狀態（Submit\temp）</param>
        /// <param name="answerItem">答題內容</param>
        /// <param name="user">用戶Id</param>
        /// <returns>執行結果</returns>
        int SavePaperAnswer(string empNo, int paperSeq, string epStatus, string answerItem, string user);
    }
}
