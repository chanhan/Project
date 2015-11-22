using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.SystemManage.SystemData
{
    /// <summary>
    /// 表單下載實體類
    /// </summary>
    [Serializable, TableName("gds_att_info_answers", SelectTable = "gds_att_info_answers_v")]
    public class AnswerModel : ModelBase
    {
        private Nullable<int> answerSeq;
        private Nullable<int> questionSeq;
        private Nullable<int> paperSeq;
        private string paperTitle;
        private string questionName;
        private string questionActive;
        private string answerContent;
        private string answerType;
        private Nullable<int> answerOrder;
        private string activeFlag;
        private string createUser;
        private Nullable<DateTime> createDate;
        private string updateUser;
        private Nullable<DateTime> updateDate;
        private Nullable<int> answerVote;
        private Nullable<decimal> answerRate;

        /// <summary>
        /// 選項編號
        /// </summary>
        [Column("ANSWER_SEQ", IsPrimaryKey = true)]
        public Nullable<int> AnswerSeq
        {
            get { return answerSeq; }
            set { answerSeq = value; }
        }

        /// <summary>
        /// 題目編號
        /// </summary>
        [Column("QUESTION_SEQ")]
        public Nullable<int> QuestionSeq
        {
            get { return questionSeq; }
            set { questionSeq = value; }
        }

        /// <summary>
        /// 問卷編號
        /// </summary>
        [Column("PAPER_SEQ", OnlySelect = true)]
        public Nullable<int> PaperSeq
        {
            get { return paperSeq; }
            set { paperSeq = value; }
        }

        /// <summary>
        /// 問卷標題
        /// </summary>
        [Column("paper_title", OnlySelect = true)]
        public string PaperTitle
        {
            get { return paperTitle; }
            set { paperTitle = value; }
        }

        /// <summary>
        /// 題目內容
        /// </summary>
        [Column("QUESTION_NAME", OnlySelect = true)]
        public string QuestionName
        {
            get { return questionName; }
            set { questionName = value; }
        }

        /// <summary>
        /// 題目是否有效
        /// </summary>
        [Column("question_active", OnlySelect = true)]
        public string QuestionActive
        {
            get { return questionActive; }
            set { questionActive = value; }
        }

        /// <summary>
        /// 選項內容
        /// </summary>
        [Column("ANSWER_CONTENT")]
        public string AnswerContent
        {
            get { return answerContent; }
            set { answerContent = value; }
        }

        /// <summary>
        /// 選項類型
        /// </summary>
        [Column("ANSWER_TYPE")]
        public string AnswerType
        {
            get { return answerType; }
            set { answerType = value; }
        }

        /// <summary>
        /// 顯示序號
        /// </summary>
        [Column("ANSWER_ORDER")]
        public Nullable<int> AnswerOrder
        {
            get { return answerOrder; }
            set { answerOrder = value; }
        }

        /// <summary>
        /// 是否有效
        /// </summary>
        [Column("ACTIVE_FLAG")]
        public string ActiveFlag
        {
            get { return activeFlag; }
            set { activeFlag = value; }
        }

        /// <summary>
        /// 創建用戶
        /// </summary>
        [Column("CREATE_USER")]
        public string CreateUser
        {
            get { return createUser; }
            set { createUser = value; }
        }

        /// <summary>
        /// 創建日期
        /// </summary>
        [Column("CREATE_DATE")]
        public Nullable<DateTime> CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        /// <summary>
        /// 更新用戶
        /// </summary>
        [Column("UPDATE_USER")]
        public string UpdateUser
        {
            get { return updateUser; }
            set { updateUser = value; }
        }

        /// <summary>
        /// 更新日期
        /// </summary>
        [Column("UPDATE_DATE")]
        public Nullable<DateTime> UpdateDate
        {
            get { return updateDate; }
            set { updateDate = value; }
        }

        /// <summary>
        /// 選項投票數
        /// </summary>
        [Column("answer_vote", OnlySelect = true)]
        public Nullable<int> AnswerVote
        {
            get { return answerVote; }
            set { answerVote = value; }
        }


        /// <summary>
        /// 選項投票比率
        /// </summary>
        [Column("answer_rate", OnlySelect = true)]
        public Nullable<decimal> AnswerRate
        {
            get { return answerRate; }
            set { answerRate = value; }
        }
    }
}
