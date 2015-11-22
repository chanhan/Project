/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： RoleModel.cs
 * 檔功能描述： 角色實體類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2011.11.30
 * 
 */


using System;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety
{
    /// <summary>
    /// 功能實體類
    /// </summary>
//    [Serializable, TableName("gds_sc_role", SelectTable = "gds_sc_role_v")]
    [Serializable, TableName("gds_sc_role")]

    public class RoleModel : ModelBase
    {
        private string roleCode;
        private string roleName;
        private string deleted;
        private string acceptmsg;
        private Nullable<DateTime> createDate;
        private string createUser  ;
        private Nullable<DateTime> updateDate;
        private string updateUser;

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

        #region 角色名稱
        /// <summary>
        /// 角色名稱
        /// </summary>
        [Column("rolename")]
        public string RoleName
        {
            get { return roleName; }
            set { roleName = value; }
        }
        #endregion

        #region 是否有效
        /// <summary>
        /// 是否有效
        /// </summary>
        [Column("deleted")]
        public string Deleted
        {
            get { return deleted; }
            set { deleted = value; }
        }
        #endregion

        #region 是否接受tmsg
        /// <summary>
        /// 是否接受tmsg
        /// </summary>
        [Column("acceptmsg")]
        public string Acceptmsg
        {
            get { return acceptmsg; }
            set { acceptmsg = value; }
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
        
    }
}
