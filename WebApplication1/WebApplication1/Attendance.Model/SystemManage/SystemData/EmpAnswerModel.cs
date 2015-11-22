using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.SystemManage.SystemData
{
    /// <summary>
    /// 問卷調查員工答案實體類
    /// </summary>
    [Serializable, TableName("gds_att_info_emp_answers", SelectTable = "gds_att_info_emp_answers_v")]
    public class EmpAnswerModel : ModelBase
    {
        private Nullable<int> eaSeq;
        private string empNo;
        private Nullable<int> answerSeq;
        private string answerContent;
        private string createUser;
        private Nullable<DateTime> createDate;
        private string updateUser;
        private Nullable<DateTime> updateDate;
        private Nullable<int> questionSeq;
        private Nullable<int> paperSeq;


        /// <summary>
        /// 序號
        /// </summary>
        [Column("EA_SEQ")]
        public Nullable<int> EaSeq
        {
            get { return eaSeq; }
            set { eaSeq = value; }
        }

        /// <summary>
        /// 工號
        /// </summary>
        [Column("EMP_NO")]
        public string EmpNo
        {
            get { return empNo; }
            set { empNo = value; }
        }

        /// <summary>
        /// 答案編號
        /// </summary>
        [Column("ANSWER_SEQ")]
        public Nullable<int> AnswerSeq
        {
            get { return answerSeq; }
            set { answerSeq = value; }
        }

        /// <summary>
        /// 答題內容
        /// </summary>
        [Column("ANSWER_CONTENT")]
        public string AnswerContent
        {
            get { return answerContent; }
            set { answerContent = value; }
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
        /// 問題編號
        /// </summary>
        [Column("QUESTION_SEQ", OnlySelect = true)]
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



    }
}
