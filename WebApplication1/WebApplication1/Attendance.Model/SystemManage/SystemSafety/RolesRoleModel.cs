/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： RolesRoleModel.cs
 * 檔功能描述： 群組與角色設定實體類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.3
 * 
 */


using System;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety
{
    /// <summary>
    /// 群組與角色實體類
    /// </summary>
    // [Serializable, TableName("gds_sc_rolesrole", SelectTable = "gds_sc_rolesrole_v")]
    [Serializable, TableName("gds_sc_rolesrole")]

    public class RolesRoleModel : ModelBase
    {
        private string rolesCode;
        private string roleCode;
        private Nullable<DateTime> createDate;
        private string createUser;
        #region 群組代碼
        /// <summary>
        /// 群組代碼
        /// </summary>
        [Column("rolescode", IsPrimaryKey = true)]
        public string RolesCode
        {
            get { return rolesCode; }
            set { rolesCode = value; }
        }
        #endregion

        #region 角色代碼
        /// <summary>
        /// 角色代碼
        /// </summary>
        [Column("rolecode")]
        public string RoleCode
        {
            get { return roleCode; }
            set { roleCode = value; }
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
    }
}
