using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemData;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemData;
using System.Data;

namespace GDSBG.MiABU.Attendance.OracleDAL.SystemManage.SystemData
{
    public class QuestionDal : DALBase<QuestionModel>, IQuestionDal
    {
        /// <summary>
        /// 根據主鍵獲取問卷題目數據
        /// </summary>
        /// <param name="model">問卷題目Model</param>
        /// <returns>問卷題目Model</returns>
        public QuestionModel GetQuestionByKey(QuestionModel model)
        {
            return DalHelper.SelectByKey(model);
        }

        /// <summary>
        /// 根據主鍵刪除問題
        /// </summary>
        /// <param name="questionSeq">問題Seq</param>
        /// <returns>是否成功</returns>
        public bool DeleteQuestionByKey(string questionSeq)
        {
            string cmdText = "DELETE_QUESTION_PRO";
            return DalHelper.ExecuteNonQuery(cmdText, CommandType.StoredProcedure, new OracleParameter("a_questionid", questionSeq)) != -1;
        }

        /// <summary>
        /// 根據實體條件獲取問卷題目數據
        /// </summary>
        /// <param name="model">問卷題目Model</param>
        /// <param name="orderProperties">排序方式</param>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">每頁記錄數</param>
        /// <param name="totalCount">總記錄數</param>
        /// <returns>問卷題目Model集</returns>
        public List<QuestionModel> GetOrderQuestion(QuestionModel model, int pageIndex, int pageSize, out int totalCount)
        {
            return OrmHelper.SetDataTableToList(DalHelper.Select(model, true, "QUESTION_ORDER", pageIndex, pageSize, out totalCount));
        }

        /// <summary>
        /// 根據實體條件獲取問卷題目數據
        /// </summary>
        /// <param name="model">問卷題目Model</param>
        /// <returns>問卷題目Model集</returns>
        public List<QuestionModel> GetOrderQuestion(QuestionModel model)
        {
            return OrmHelper.SetDataTableToList(DalHelper.Select(model, "QUESTION_ORDER"));
        }

        /// <summary>
        /// 新增及修改題目
        /// </summary>
        /// <param name="model">問卷題目Model</param>
        /// <param name="answerContent">題目選項內容</param>
        /// <param name="operate">操作：AddNew(新增)，Edit(更新)</param>
        /// <returns>是否成功</returns>
        public bool AddQuestion(QuestionModel model, string answerContent, string operate)
        {
            return DalHelper.ExecuteNonQuery("insert_question_pro", CommandType.StoredProcedure,
                new OracleParameter("p_question_seq", model.QuestionSeq.HasValue ? model.QuestionSeq.Value : -1), new OracleParameter("p_paper_seq", model.PaperSeq.Value),
                 new OracleParameter("p_question_name", model.QuestionName), new OracleParameter("p_is_multi", model.IsMulti),
                 new OracleParameter("p_active_flag", model.ActiveFlag), new OracleParameter("p_user", model.CreateUser),
                 new OracleParameter("p_question_order", model.QuestionOrder.Value), new OracleParameter("p_answer_content", answerContent),
                 new OracleParameter("p_operate", operate)) != -1;
        }
    }
}
