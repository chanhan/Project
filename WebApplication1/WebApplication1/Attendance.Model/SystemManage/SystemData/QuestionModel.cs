using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.SystemManage.SystemData
{
    /// <summary>
    /// 問卷題目實體類
    /// </summary>
    [Serializable, TableName("gds_att_info_questions", SelectTable = "gds_att_info_questions_v")]
    public class QuestionModel : ModelBase
    {
        private Nullable<int> questionSeq;
        private Nullable<int> paperSeq;
        private string paperTitle;
        private string questionName;
        private string isMulti;
        private Nullable<int> questionOrder;
        private string activeFlag;
        private string createUser;
        private Nullable<DateTime> createDate;
        private string updateUser;
        private Nullable<DateTime> updateDate;



        /// <summary>
        /// 題目編號
        /// </summary>
        [Column("QUESTION_SEQ", IsPrimaryKey = true)]
        public Nullable<int> QuestionSeq
        {
            get { return questionSeq; }
            set { questionSeq = value; }
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
        [Column("QUESTION_NAME")]
        public string QuestionName
        {
            get { return questionName; }
            set { questionName = value; }
        }

        /// <summary>
        /// 是否多選
        /// </summary>
        [Column("IS_MULTI")]
        public string IsMulti
        {
            get { return isMulti; }
            set { isMulti = value; }
        }

        /// <summary>
        /// 顯示序號
        /// </summary>
        [Column("QUESTION_ORDER")]
        public Nullable<int> QuestionOrder
        {
            get { return questionOrder; }
            set { questionOrder = value; }
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
    }
}
