/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： AttKQParamsEmpTempModel.cs
 * 檔功能描述： 考勤參數設定(人員)臨時表實體類
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
    /// 考勤參數設定(人員)臨時表實體類
    /// </summary>
    [Serializable, TableName("GDS_ATT_KQPARAMSEMP_TEMP")]
    public class AttKQParamsEmpTempModel : ModelBase
    {

        private string workNo;
        private string bellNo;
        private string isNotKaoQin;
        private Nullable<DateTime> createDate;
        private string createUser;
        private Nullable<DateTime> updateDate;
        private string updateUser;
        private string errorMsg;

        #region 工號
        /// <summary>
        /// 工號
        /// </summary>
        [Column("WORKNO", IsPrimaryKey = true)]
        public string WorkNo
        {
            get { return workNo; }
            set { workNo = value; }
        }
        #endregion

        #region 卡鐘編號
        /// <summary>
        /// 卡鐘編號
        /// </summary>
        [Column("BellNo")]
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


        #region 錯誤說明
        /// <summary>
        /// 錯誤說明
        /// </summary>
        [Column("ErrorMsg", OnlySelect = true)]
        public string ErrorMsg
        {
            get { return errorMsg; }
            set { errorMsg = value; }
        }
        #endregion
    }
}
