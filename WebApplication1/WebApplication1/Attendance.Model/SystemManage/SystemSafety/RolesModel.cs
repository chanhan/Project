/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： RolesModel.cs
 * 檔功能描述： 群組實體類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.1
 * 
 */


using System;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety
{
    /// <summary>
    /// 群組實體類
    /// </summary>
    // [Serializable, TableName("gds_sc_roles", SelectTable = "gds_sc_roles_v")]
    [Serializable, TableName("gds_sc_roles")]

    public class RolesModel : ModelBase
    {
        private string rolesCode;
        private string rolesName;
        private string allowFlag;
        private string acceptMsg;
        private string deleted;
        private Nullable<DateTime> createDate;
        private string createUser;
        private Nullable<DateTime> updateDate;
        private string updateUser;

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

        #region 群組名稱
        /// <summary>
        /// 群組名稱
        /// </summary>
        [Column("rolesname")]
        public string RolesName
        {
            get { return rolesName; }
            set { rolesName = value; }
        }
        #endregion

        #region 是否允許BU開戶
        /// <summary>
        /// 是否允許BU開戶
        /// </summary>
        [Column("allowflag")]
        public string AllowFlag
        {
            get { return allowFlag; }
            set { allowFlag = value; }
        }
        #endregion

        #region 是否接受tmsg
        /// <summary>
        /// 是否接受tmsg
        /// </summary>
        [Column("acceptmsg")]
        public string AcceptMsg
        {
            get { return acceptMsg; }
            set { acceptMsg = value; }
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
