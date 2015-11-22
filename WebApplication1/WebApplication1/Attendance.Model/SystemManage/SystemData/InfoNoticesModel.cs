/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： InfoNoticesModel.cs
 * 檔功能描述： 公告信息實體類
 * 
 * 版本：1.0
 * 創建標識： 陳函 2012.01.03
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.SystemManage.SystemData
{
    /// <summary>
    /// 公告信息實體類
    /// </summary>
    [Serializable, TableName("GDS_ATT_INFO_NOTICES", SelectTable = "gds_att_info_notices_v")]
    public class InfoNoticesModel : ModelBase
    {
        private Nullable<int> noticeId;
        private string noticeTitle;
        private Nullable<int> noticeTypeId;
        private string noticeTypeName;
        private Nullable<DateTime> noticeDate;
        private string noticeAuthor;
        private string authorTel;
        private Nullable<int> browseTimes;
        private string noticeDept;
        private string annexFilePath;
        private string noticeContent;
        private string activeFlag;
        private string createUser;
        private Nullable<DateTime> createDate;
        private string updateUser;
        private Nullable<DateTime> updateDate;

        /// <summary>
        /// 公告ID
        /// </summary>
        [Column("NOTICE_ID", IsPrimaryKey = true)]
        public Nullable<int> NoticeId
        {
            get { return noticeId; }
            set { noticeId = value; }
        }

        /// <summary>
        /// 公告標題
        /// </summary>
        [Column("NOTICE_TITLE")]
        public string NoticeTitle
        {
            get { return noticeTitle; }
            set { noticeTitle = value; }
        }

        /// <summary>
        /// 公告類型ID
        /// </summary>
        [Column("NOTICE_TYPE_ID")]
        public Nullable<int> NoticeTypeId
        {
            get { return noticeTypeId; }
            set { noticeTypeId = value; }
        }

        /// <summary>
        /// 公告類型名稱
        /// </summary>
        [Column("NOTICE_TYPE_NAME", OnlySelect = true)]
        public string NoticeTypeName
        {
            get { return noticeTypeName; }
            set { noticeTypeName = value; }
        }

        /// <summary>
        /// 公告時間
        /// </summary>
        [Column("NOTICE_DATE")]
        public Nullable<DateTime> NoticeDate
        {
            get { return noticeDate; }
            set { noticeDate = value; }
        }

        /// <summary>
        /// 公告人
        /// </summary>
        [Column("NOTICE_AUTHOR")]
        public string NoticeAuthor
        {
            get { return noticeAuthor; }
            set { noticeAuthor = value; }
        }

        /// <summary>
        /// 公告人聯繫電話
        /// </summary>
        [Column("AUTHOR_TEL")]
        public string AuthorTel
        {
            get { return authorTel; }
            set { authorTel = value; }
        }

        /// <summary>
        /// 流覽次數
        /// </summary>
        [Column("BROWSE_TIMES")]
        public Nullable<int> BrowseTimes
        {
            get { return browseTimes; }
            set { browseTimes = value; }
        }

        /// <summary>
        /// 公告單位
        /// </summary>
        [Column("NOTICE_DEPT")]
        public string NoticeDept
        {
            get { return noticeDept; }
            set { noticeDept = value; }
        }

        /// <summary>
        /// 附件路徑
        /// </summary>
        [Column("ANNEX_FILE_PATH")]
        public string AnnexFilePath
        {
            get { return annexFilePath; }
            set { annexFilePath = value; }
        }

        /// <summary>
        /// 公告內容
        /// </summary>
        [Column("NOTICE_CONTENT")]
        public string NoticeContent
        {
            get { return noticeContent; }
            set { noticeContent = value; }
        }

        /// <summary>
        /// 是否有效(Y/N)
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
