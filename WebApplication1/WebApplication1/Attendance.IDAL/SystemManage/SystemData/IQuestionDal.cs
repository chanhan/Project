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
    [RefClass("SystemManage.SystemData.QuestionDal")]
    public interface IQuestionDal
    {
        /// <summary>
        /// 根據主鍵獲取問卷題目數據
        /// </summary>
        /// <param name="model">問卷題目Model</param>
        /// <returns>問卷題目Model</returns>
        QuestionModel GetQuestionByKey(QuestionModel model);

        /// <summary>
        /// 根據主鍵刪除問題
        /// </summary>
        /// <param name="questionSeq">問題Seq</param>
        /// <returns>是否成功</returns>
        bool DeleteQuestionByKey(string questionSeq);

        /// <summary>
        /// 根據實體條件獲取問卷題目數據
        /// </summary>
        /// <param name="model">問卷題目Model</param>
        /// <param name="orderProperties">排序方式</param>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">每頁記錄數</param>
        /// <param name="totalCount">總記錄數</param>
        /// <returns>問卷Model集</returns>
        List<QuestionModel> GetOrderQuestion(QuestionModel model, int pageIndex, int pageSize, out int totalCount);

        /// <summary>
        /// 根據實體條件獲取問卷題目數據
        /// </summary>
        /// <param name="model">問卷題目Model</param>
        /// <returns>問卷題目Model集</returns>
        List<QuestionModel> GetOrderQuestion(QuestionModel model);

        /// <summary>
        /// 新增及修改題目
        /// </summary>
        /// <param name="model">問卷題目Model</param>
        /// <param name="answerContent">題目選項內容</param>
        /// <param name="operate">操作：AddNew(新增)，Edit(更新)</param>
        /// <returns>是否成功</returns>
        bool AddQuestion(QuestionModel model, string answerContent, string operate);
    }
}
