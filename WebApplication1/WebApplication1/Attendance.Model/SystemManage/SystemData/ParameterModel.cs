/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ParameterModel.cs
 * 檔功能描述： 系統參數實體類
 * 
 * 版本：1.0
 * 創建標識： 何西 2011.12.03
 * 
 */

using System;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.SystemManage.SystemData
{
    /// <summary>
    /// 系統參數實體類
    /// </summary>
    [Serializable, TableName("GDS_SC_PARAMETER")]
    public class ParameterModel : ModelBase
    {
        private string paraName;
        private string paraValue;
        private string description;
        private string isallowmodify;
        private Nullable<DateTime> createDate;
        private string createUser;
        private Nullable<DateTime> updateDate;
        private string updateUser;

        #region 參數名稱
        /// <summary>
        /// 參數名稱
        /// </summary>
        [Column("PARANAME",IsPrimaryKey=true)]
        public string ParaName
        {
            get { return paraName; }
            set { paraName = value; }
        }
        #endregion

        #region 參數值
        /// <summary>
        /// 參數值
        /// </summary>
        [Column("PARAVALUE")]
        public string ParaValue
        {
            get { return paraValue; }
            set { paraValue = value; }
        }
        #endregion

        #region 描述
        /// <summary>
        /// 描述
        /// </summary>
        [Column("DESCRIPTION")]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        #endregion

        #region 是否允許修改
        /// <summary>
        /// 是否允許修改
        /// </summary>
        [Column("ISALLOWMODIFY")]
        public string Isallowmodify
        {
            get { return isallowmodify; }
            set { isallowmodify = value; }
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
