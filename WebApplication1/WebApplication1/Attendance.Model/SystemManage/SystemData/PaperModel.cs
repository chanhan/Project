using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.SystemManage.SystemData
{  /// <summary>
    /// 問卷調查實體類
    /// </summary>
    [Serializable, TableName("gds_att_info_papers", SelectTable = "gds_att_info_papers_v")]
    public class PaperModel : ModelBase
    {
        private Nullable<int> paperSeq;
        private string paperTitle;
        private Nullable<DateTime> paperDate;
        private string paperRemarks;
        private string activeFlag;
        private string createUser;
        private Nullable<DateTime> createDate;
        private string updateUser;
        private Nullable<DateTime> updateDate;
        private Nullable<int> paperCount;

        /// <summary>
        /// 問卷編號
        /// </summary>
        [Column("PAPER_SEQ", IsPrimaryKey = true)]
        public Nullable<int> PaperSeq
        {
            get { return paperSeq; }
            set { paperSeq = value; }
        }

        /// <summary>
        /// 問卷標題
        /// </summary>
        [Column("PAPER_TITLE")]
        public string PaperTitle
        {
            get { return paperTitle; }
            set { paperTitle = value; }
        }

        /// <summary>
        /// 發布日期
        /// </summary>
        [Column("PAPER_DATE")]
        public Nullable<DateTime> PaperDate
        {
            get { return paperDate; }
            set { paperDate = value; }
        }

        /// <summary>
        /// 備注
        /// </summary>
        [Column("PAPER_REMARKS")]
        public string PaperRemarks
        {
            get { return paperRemarks; }
            set { paperRemarks = value; }
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
        /// 投票統計
        /// </summary>
        [Column("paper_count", OnlySelect = true)]
        public Nullable<int> PaperCount
        {
            get { return paperCount; }
            set { paperCount = value; }
        }

    }
}
