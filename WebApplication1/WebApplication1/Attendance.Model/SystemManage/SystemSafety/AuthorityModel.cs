/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： AuthorityModel.cs
 * 檔功能描述： 角色功能模組實體類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2011.12.7
 * 
 */
using System;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety
{
    /// <summary>
    /// 角色功能模組實體類
    /// </summary>
    [Serializable, TableName("gds_sc_authority")]
    public class AuthorityModel : ModelBase
    {
        private string roleCode;
        private string moduleCode;
        private string functionList;
        private Nullable<DateTime> createDate;
        private string createUser;

        #region 角色代碼
        /// <summary>
        /// 角色代碼
        /// </summary>
        [Column("rolecode",IsPrimaryKey=true)]
        public string RoleCode
        {
            get { return roleCode; }
            set { roleCode = value; }
        }
        #endregion
        #region 功能模組代碼
        /// <summary>
        /// 功能模組代碼
        /// </summary>
        [Column("modulecode", IsPrimaryKey = true)]
        public string ModuleCode
        {
            get { return moduleCode; }
            set { moduleCode = value; }
        }
        #endregion
        #region 已授權功能碼
        /// <summary>
        /// 已授權功能碼
        /// </summary>
        [Column("functionlist")]
        public string FunctionList
        {
            get { return functionList; }
            set { functionList = value; }
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
        #endregion
    }
}
