/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： FormModel.cs
 * 檔功能描述： 表單下載實體類
 * 
 * 版本：1.0
 * 創建標識： 陳函 2012.01.05
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.SystemManage.SystemData
{
     /// <summary>
    /// 表單下載實體類
    /// </summary>
    [Serializable, TableName("gds_att_info_forms", SelectTable = "gds_att_info_forms_v")]
    public class FormModel : ModelBase
    {
        private Nullable<int> formSeq;
        private Nullable<int> typeId;
        private string typeName;
        private string formName;
        private string formPath;
        private Nullable<DateTime> uploadDate;
        private string formRemark;
        private string activeFlag;
        private string createUser;
        private Nullable<DateTime> createDate;
        private string updateUser;
        private Nullable<DateTime> updateDate;

        /// <summary>
        /// 序號
        /// </summary>
        [Column("FORM_SEQ", IsPrimaryKey = true)]
        public Nullable<int> FormSeq
        {
            get { return formSeq; }
            set { formSeq = value; }
        }

        /// <summary>
        /// 表單類型Id
        /// </summary>
        [Column("TYPE_ID")]
        public Nullable<int> TypeId
        {
            get { return typeId; }
            set { typeId = value; }
        }

        /// <summary>
        /// 表單類型
        /// </summary>
        [Column("type_name", OnlySelect = true)]
        public string TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }

        /// <summary>
        /// 表單名稱
        /// </summary>
        [Column("FORM_NAME")]
        public string FormName
        {
            get { return formName; }
            set { formName = value; }
        }

        /// <summary>
        /// 表單路徑
        /// </summary>
        [Column("FORM_PATH")]
        public string FormPath
        {
            get { return formPath; }
            set { formPath = value; }
        }

        /// <summary>
        /// 上傳日期
        /// </summary>
        [Column("UPLOAD_DATE")]
        public Nullable<DateTime> UploadDate
        {
            get { return uploadDate; }
            set { uploadDate = value; }
        }

        /// <summary>
        /// 備注
        /// </summary>
        [Column("FORM_REMARK")]
        public string FormRemark
        {
            get { return formRemark; }
            set { formRemark = value; }
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
