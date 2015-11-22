/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ModuleModel.cs
 * 檔功能描述： 模組實體類
 * 
 * 版本：1.0
 * 創建標識： 何西 2011.11.28
 * 
 */

using System;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety
{
    /// <summary>
    /// 功能實體類
    /// </summary>
    [Serializable, TableName("GDS_SC_MODULE", SelectTable = "gds_sc_module_v")]
    public class ModuleModel : ModelBase
    {
        private string moduleCode;
        private string functionList;
        private string description;
        private string functionDesc;
        private string formName;
        private string url;
        private string parentModuleCode;
        private Nullable<int> orderId;
        private string deleted;
        private string privileged;
        private string languageKey;
        private Nullable<DateTime> createDate;
        private string createUser;
        private Nullable<DateTime> updateDate;
        private string updateUser;
        private string description1;
        private string parentmodulename;
        private string functionId;
        private string functionMenuType;
        private string listFlag;
        private string functionComment;
        private string functionVersion;
        private string functionImage;
        private string functionMouseImage;
        private string functionRanking;
        private string isKaoQin;


        #region 模組代碼
        /// <summary>
        /// 模組代碼
        /// </summary>
        [Column("modulecode",IsPrimaryKey=true)]
        public string ModuleCode
        {
            get { return moduleCode; }
            set { moduleCode = value; }
        } 
        #endregion

        #region 功能清單
        /// <summary>
        /// 功能清單
        /// </summary>
        [Column("functionlist")]
        public string FunctionList
        {
            get { return functionList; }
            set { functionList = value; }
        }
        #endregion

        #region 功能描述
        /// <summary>
        /// 功能描述
        /// </summary>
        [Column("description")]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        #endregion

        #region 模組描述
        /// <summary>
        /// 模組描述
        /// </summary>
        [Column("functiondesc")]
        public string FunctionDesc
        {
            get { return functionDesc; }
            set { functionDesc = value; }
        }
        #endregion

        #region 功能名稱
        /// <summary>
        /// 功能名稱
        /// </summary>
        [Column("formname")]
        public string FormName
        {
            get { return formName; }
            set { formName = value; }
        }
        #endregion

        #region 鏈接地址
        /// <summary>
        /// 功能清單
        /// </summary>
        [Column("url")]
        public string Url
        {
            get { return url; }
            set { url = value; }
        }
        #endregion

        #region 父級代碼
        /// <summary>
        /// 功能清單
        /// </summary>
        [Column("parentmodulecode")]
        public string ParentModuleCode
        {
            get { return parentModuleCode; }
            set { parentModuleCode = value; }
        }
        #endregion

        #region 顯示序號
        /// <summary>
        /// 顯示序號
        /// </summary>
        [Column("orderid")]
        public Nullable<int> OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }
        #endregion

        #region 是否刪除
        /// <summary>
        /// 是否刪除
        /// </summary>
        [Column("deleted")]
        public string Deleted
        {
            get { return deleted; }
            set { deleted = value; }
        }
        #endregion

        #region 
        /// <summary>
        /// 
        /// </summary>
        [Column("privileged")]
        public string Privileged
        {
            get { return privileged; }
            set { privileged = value; }
        }
        #endregion

        #region 多國語言值
        /// <summary>
        /// 多國語言值
        /// </summary>
        [Column("language_key")]
        public string LanguageKey
        {
            get { return languageKey; }
            set { languageKey = value; }
        }
        #endregion

        #region 創建日期
        /// <summary>
        /// 創建日期
        /// </summary>
        [Column("create_date")]
        public Nullable<DateTime> CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
        #endregion

        #region 創建用戶
        /// <summary>
        /// 創建用戶
        /// </summary>
        [Column("create_user")]
        public string CreateUser
        {
            get { return createUser; }
            set { createUser = value; }
        }
        #endregion

        #region 更新日期
        /// <summary>
        /// 更新日期
        /// </summary>
        [Column("update_date")]
        public Nullable<DateTime> UpdateDate
        {
            get { return updateDate; }
            set { updateDate = value; }
        }
        #endregion

        #region 更新用戶
        /// <summary>
        /// 更新用戶
        /// </summary>
        [Column("update_user")]
        public string UpdateUser
        {
            get { return updateUser; }
            set { updateUser = value; }
        }
        #endregion

        #region 
        /// <summary>
        /// 
        /// </summary>
        [Column("description1",OnlySelect=true)]
        public string Description1
        {
            get { return description1; }
            set { description1 = value; }
        }
        #endregion

        #region 父模組名稱
        /// <summary>
        /// 父模組名稱
        /// </summary>
        [Column("parentmodulename", OnlySelect = true)]
        public string Parentmodulename
        {
            get { return parentmodulename; }
            set { parentmodulename = value; }
        }
        #endregion

        #region function_ID功能SEQ
        /// <summary>
        /// function_ID功能SEQ
        /// </summary>
        [Column("function_id")]
        public string FunctionId
        {
            get { return functionId; }
            set { functionId = value; }
        }
        #endregion

        #region 功能類型
        /// <summary>
        /// 功能類型
        /// </summary>
        [Column("function_menu_type")]
        public string FunctionMenuType
        {
            get { return functionMenuType; }
            set { functionMenuType = value; }
        }
        #endregion

        #region 選單是否顯示
        /// <summary>
        /// 選單是否顯示
        /// </summary>
        [Column("list_flag")]
        public string ListFlag
        {
            get { return listFlag; }
            set { listFlag = value; }
        }
        #endregion

        #region 功能備註
        /// <summary>
        /// 功能備註
        /// </summary>
        [Column("function_comment")]
        public string FunctionComment
        {
            get { return functionComment; }
            set { functionComment = value; }
        }
        #endregion

        #region 版本序號
        /// <summary>
        /// 版本序號
        /// </summary>
        [Column("function_version")]
        public string FunctionVersion
        {
            get { return functionVersion; }
            set { functionVersion = value; }
        }
        #endregion

        #region 默認實現圖表
        /// <summary>
        /// 默認實現圖表
        /// </summary>
        [Column("function_image")]
        public string FunctionImage
        {
            get { return functionImage; }
            set { functionImage = value; }
        }
        #endregion

        #region 鼠標移上顯示圖表
        /// <summary>
        /// 鼠標移上顯示圖表
        /// </summary>
        [Column("function_mouse_image")]
        public string FunctionMouseImage
        {
            get { return functionMouseImage; }
            set { functionMouseImage = value; }
        }
        #endregion

        #region 排序SEQ
        /// <summary>
        /// 排序SEQ
        /// </summary>
        [Column("function_ranking")]
        public string FunctionRanking
        {
            get { return functionRanking; }
            set { functionRanking = value; }
        }
        #endregion

        #region 是否屬於考勤系統
        /// <summary>
        /// 排序SEQ
        /// </summary>
        [Column("isKaoQin")]
        public string IsKaoQin
        {
            get { return isKaoQin; }
            set { isKaoQin = value; }
        }
        #endregion
    }
}
