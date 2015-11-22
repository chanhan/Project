/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： AttKQParamsOrgModel.cs
 * 檔功能描述： 考勤參數設定(組織)實體類
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.8
 * 
 */

using System;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.KQM.BasicData
{
    /// <summary>
    /// 考勤參數設定(組織)實體類
    /// </summary>
    [Serializable, TableName("GDS_ATT_KQPARAMSORG", SelectTable = "GDS_ATT_KQPARAMSORG_V")]
    public class AttKQParamsOrgModel : ModelBase
    {
        private string orgCode;
        private string bellNo;   
        private string isNotKaoQin;
        private Nullable<DateTime> createDate;
        private string createUser;
        private Nullable<DateTime> updateDate;
        private string updateUser;
        private string depName;

        #region 組織CODE
        /// <summary>
        /// 組織CODE
        /// </summary>
        [Column("ORGCODE",IsPrimaryKey=true)]
        public string OrgCode
        {
            get { return orgCode; }
            set { orgCode = value; }
        }
        #endregion

        #region 卡鐘編號
        /// <summary>
        /// 卡鐘編號
        /// </summary>
        [Column("BELLNO")]
        public string BellNo
        {
            get { return bellNo; }
            set { bellNo = value; }
        }
        #endregion

        #region 是否免考勤
        /// <summary>
        /// 是否免考勤
        /// </summary>
        [Column("IsNotKaoQin")]
        public string IsNotKaoQin
        {
            get { return isNotKaoQin; }
            set { isNotKaoQin = value; }
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

        #region 組織名稱
        /// <summary>
        /// 組織名稱
        /// </summary>
        [Column("depname", OnlySelect = true)]
        public string DepName
        {
            get { return depName; }
            set { depName = value; }
        }

        #endregion
    }
}
