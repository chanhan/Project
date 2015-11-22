/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ActivityModel.cs
 * 檔功能描述： 免卡人員加班導入實體類
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.8
 * 
 */
using System;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.KQM.OTM
{
    /// <summary>
    /// 免卡人員加班導入實體類
    /// </summary>
    [Serializable, TableName("GDS_ATT_ACTIVITY", SelectTable = "gds_att_activity_v")]
    public class ActivityModel : ModelBase
    {
        private string id;
        private string workNo;
        private string oTType;
        private Nullable<DateTime> oTDate;
        private Nullable<decimal> confirmHours;
        private string workDesc;
        private string remark;
        private string status;
        private string yearMonth;
        private Nullable<DateTime> createDate;
        private string createUser;
        private Nullable<DateTime> updateDate;
        private string updateUser;
        private string depName;
        private string localName;
        private string statusName;
        private string overTimeType;
        private string startTime;
        private string endTime;

        #region ID
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID", IsPrimaryKey = true)]
        public string ID
        {
            get { return id; }
            set { id = value; }
        }
        #endregion

        #region 工號
        /// <summary>
        /// 工號
        /// </summary>
        [Column("WORKNO")]
        public string WorkNo
        {
            get { return workNo; }
            set { workNo = value; }
        }
        #endregion

        #region 加班類型
        /// <summary>
        /// 加班類型
        /// </summary>
        [Column("OTTYPE")]
        public string OTType
        {
            get { return oTType; }
            set { oTType = value; }
        }
        #endregion

        #region 加班日期
        /// <summary>
        /// 加班日期
        /// </summary>
        [Column("OTDATE")]
        public Nullable<DateTime> OTDate
        {
            get { return oTDate; }
            set { oTDate = value; }
        }
        #endregion

        #region 確認時數
        /// <summary>
        /// 確認時數
        /// </summary>
        [Column("CONFIRMHOURS")]
        public Nullable<decimal> ConfirmHours
        {
            get { return confirmHours; }
            set { confirmHours = value; }
        }
        #endregion

        #region 加班內容描述
        /// <summary>
        /// 加班內容描述
        /// </summary>
        [Column("WORKDESC")]
        public string WorkDesc
        {
            get { return workDesc; }
            set { workDesc = value; }
        }
        #endregion

        #region 備註
        /// <summary>
        /// 備註
        /// </summary>
        [Column("REMARK")]
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        #endregion

        #region 審核狀態
        /// <summary>
        /// 審核狀態
        /// </summary>
        [Column("STATUS")]
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        #endregion

        #region 年度月份
        /// <summary>
        /// 年度月份
        /// </summary>
        [Column("YEARMONTH")]
        public string YearMonth
        {
            get { return yearMonth; }
            set { yearMonth = value; }
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


        #region 組織
        /// <summary>
        /// 組織
        /// </summary>
        [Column("DEPNAME",OnlySelect=true)]
        public string DepName
        {
            get { return depName; }
            set { depName = value; }
        }
        #endregion

        #region 姓名
        /// <summary>
        /// 姓名
        /// </summary>
        [Column("LOCALNAME",OnlySelect=true)]
        public string LocalName
        {
            get { return localName; }
            set { localName = value; }
        }
        #endregion

        #region 審核狀態名稱
        /// <summary>
        /// 審核狀態名稱
        /// </summary>
        [Column("STATUSNAME", OnlySelect = true)]
        public string StatusName
        {
            get { return statusName; }
            set { statusName = value; }
        }
        #endregion

        #region 審核狀態名稱
        /// <summary>
        /// 審核狀態名稱
        /// </summary>
        [Column("OVERTIMETYPE", OnlySelect = true)]
        public string OverTimeType
        {
            get { return overTimeType; }
            set { overTimeType = value; }
        }
        #endregion

        #region 開始時間
        /// <summary>
        /// 開始時間
        /// </summary>
        [Column("STARTTIME")]
        public string StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }
        #endregion

        #region 結束時間
        /// <summary>
        /// 結束時間
        /// </summary>
        [Column("ENDTIME")]
        public string EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }
        #endregion
    }
}
