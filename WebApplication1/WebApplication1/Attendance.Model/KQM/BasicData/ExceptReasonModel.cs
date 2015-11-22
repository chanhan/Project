/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： WorkShiftModel.cs
 * 檔功能描述： 班別定義實體類
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.12.06
 * 
 */
using System;
using GDSBG.MiABU.Attendance.Common.Attributes;


namespace GDSBG.MiABU.Attendance.Model.KQM.BasicData
{
    /// <summary>
    /// 班別定義實體類
    /// </summary>
    [Serializable, TableName(" gds_att_exceptreason")]
    public class ExceptReasonModel : ModelBase
    {
        private string reasonNo;
        private string reasonName;
        private string updateUser;
        private Nullable<DateTime> updateDate;
        private string effectFlag;
        private string reasonType;
        private string salaryFlag;

        #region 異常編號
        /// <summary>
        /// 異常編號
        /// </summary>
        [Column("reasonNo", IsPrimaryKey = true)]
        public string ReasonNo
        {
            get { return reasonNo; }
            set { reasonNo = value; }
        }
        #endregion
        #region 異常名稱
        /// <summary>
        /// 異常名稱
        /// </summary>
        [Column("reasonName")]
        public string ReasonName
        {
            get { return reasonName; }
            set { reasonName = value; }
        }
        #endregion
        #region 維護者
        /// <summary>
        /// 維護者
        /// </summary>
        [Column("update_user")]
        public string UpdateUser
        {
            get { return updateUser; }
            set { updateUser = value; }
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

        #region 是否有效
        /// <summary>
        /// 是否有效
        /// </summary>
        [Column("effectflag")]
        public string EffectFlag
        {
            get { return effectFlag; }
            set { effectFlag = value; }
        }
        #endregion
        #region 異常類型
        /// <summary>
        /// 異常類型
        /// </summary>
        [Column("reasontype")]
        public string ReasonType
        {
            get { return reasonType; }
            set { reasonType = value; }
        }
        #endregion
        #region 扣薪否
        /// <summary>
        /// 扣薪否
        /// </summary>
        [Column("salaryflag")]
        public string SalaryFlag
        {
            get { return salaryFlag; }
            set { salaryFlag = value; }
        }
        #endregion
    }
}
