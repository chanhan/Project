using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.SystemManage.SystemData
{
    /// <summary>
    /// 表單下載實體類
    /// </summary>
    [Serializable, TableName("gds_att_info_emp_papers")]
    public class EmpPaperModel : ModelBase
    {
        private Nullable<int> epSeq;
        private string empNo;
        private Nullable<int> paperSeq;
        private string epStatus;
        private Nullable<DateTime> answerDate;
        private string createUser;
        private Nullable<DateTime> createDate;
        private string updateUser;
        private Nullable<DateTime> updateDate;


        /// <summary>
        /// 序號
        /// </summary>
        [Column("EP_SEQ")]
        public Nullable<int> EpSeq
        {
            get { return epSeq; }
            set { epSeq = value; }
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
        /// 問卷編號
        /// </summary>
        [Column("PAPER_SEQ")]
        public Nullable<int> PaperSeq
        {
            get { return paperSeq; }
            set { paperSeq = value; }
        }

        /// <summary>
        /// 狀態（提交、暫存）
        /// </summary>
        [Column("EP_STATUS")]
        public string EpStatus
        {
            get { return epStatus; }
            set { epStatus = value; }
        }

        /// <summary>
        /// 回答日期
        /// </summary>
        [Column("ANSWER_DATE")]
        public Nullable<DateTime> AnswerDate
        {
            get { return answerDate; }
            set { answerDate = value; }
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
    }
}
