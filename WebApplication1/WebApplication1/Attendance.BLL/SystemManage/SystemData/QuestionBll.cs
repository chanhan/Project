using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemData;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemData;
using System.Data;

namespace GDSBG.MiABU.Attendance.BLL.SystemManage.SystemData
{
    public class QuestionBll : BLLBase<IQuestionDal>
    {
        /// <summary>
        /// 根據主鍵獲取問卷題目數據
        /// </summary>
        /// <param name="model">問卷題目Model</param>
        /// <returns>問卷題目Model</returns>
        public QuestionModel GetQuestionByKey(string questionSeq)
        {
            QuestionModel model = new QuestionModel();
            model.QuestionSeq = Convert.ToInt32(questionSeq);
            return DAL.GetQuestionByKey(model);
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
            return DAL.AddQuestion(model, answerContent, operate);
        }

        /// <summary>
        /// 根據主鍵刪除問題
        /// </summary>
        /// <param name="questionSeq"></param>
        /// <returns></returns>
        public bool DeleteQuestionByKey(string questionSeq)
        {
            return DAL.DeleteQuestionByKey(questionSeq);
        }

        /// <summary>
        /// 根據實體條件獲取問卷題目數據
        /// </summary>
        /// <param name="model">問卷題目Model</param>
        /// <param name="orderProperties">排序方式</param>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">每頁記錄數</param>
        /// <param name="totalCount">總記錄數</param>
        /// <returns>問卷Model集</returns>
        public   List<QuestionModel> GetOrderQuestion(QuestionModel model, int pageIndex, int pageSize, out int totalCount)
        {
            return DAL.GetOrderQuestion(model, pageIndex, pageSize, out totalCount);
        }

        /// <summary>
        /// 根據實體條件獲取問卷題目數據
        /// </summary>
        /// <param name="model">問卷題目Model</param>
        /// <returns>問卷題目Model集</returns>
        public List<QuestionModel> GetOrderQuestion(QuestionModel model)
        {
            return DAL.GetOrderQuestion(model);
        }
    }
}
