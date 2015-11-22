using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemData;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemData;
using System.Data;

namespace GDSBG.MiABU.Attendance.OracleDAL.SystemManage.SystemData
{
    public class EmpAnswerDal : DALBase<EmpAnswerModel>, IEmpAnswerDal
    {
        /// <summary>
        /// 根據實體條件獲取問卷答題數據
        /// </summary>
        /// <param name="model">問卷答題Model</param>
        /// <returns>問卷答題Model集</returns>
        public List<EmpAnswerModel> GetAnswer(EmpAnswerModel model)
        { 
            
            return OrmHelper.SetDataTableToList(DalHelper.Select(model));
        }

        /// <summary>
        /// 保存問卷調查答卷內容
        /// </summary>
        /// <param name="empNo">工號</param>
        /// <param name="paperSeq">問卷序號</param>
        /// <param name="epStatus">狀態（Submit\temp）</param>
        /// <param name="answerItem">答題內容</param>
        /// <param name="user">用戶Id</param>
        /// <returns>執行結果</returns>
        public int SavePaperAnswer(string empNo, int paperSeq, string epStatus, string answerItem, string user)
        {
            return DalHelper.ExecuteNonQuery("gds_att_insertpaperanswer_pro", CommandType.StoredProcedure,
                 new OracleParameter("p_emp_no", empNo), new OracleParameter("p_paper_seq", paperSeq),
                 new OracleParameter("p_ep_status", epStatus), new OracleParameter("p_answer_item", answerItem),
                 new OracleParameter("p_user", user));
        }
    }
}
