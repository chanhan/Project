/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMReGetKaoQinModel.cs
 * 檔功能描述：重新計算實體類
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.12.27
 * 
 */
using System;
using GDSBG.MiABU.Attendance.Common.Attributes;


namespace GDSBG.MiABU.Attendance.Model.KQM.AttendanceData
{
    /// <summary>
    /// 重新計算實體類
    /// </summary>
    [Serializable, TableName(" gds_att_kaoqindata", SelectTable = "gds_att_kaoqindata_v")]
    public class KQMReGetKaoQinModel : ModelBase
    {
        private string workNo;
        private Nullable<DateTime> kqDate;
        private string shiftNo;
        private string localName;
        private string depName;
        private string dCode;
        private string otOnDutyTime;
        private string otOffDutyTime;
        private string shiftDesc;
        private string onDutyTime;
        private string offDutyTime;
        private string absentQty;
        private string status;
        private string exceptionType;
        private string exceptionName;
        private string reasonType;
        private string reasonRemark;
        private string reasonName;
        private string statusName;
        private string otHours;
        private string totalHours;

        #region 工號
        /// <summary>
        /// 工號
        /// </summary>
        [Column("workNo", IsPrimaryKey = true)]
        public string WorkNo
        {
            get { return workNo; }
            set { workNo = value; }
        }
        #endregion
        #region 考勤日
        /// <summary>
        ///考勤日
        /// </summary>
        [Column("kqDate", IsPrimaryKey = true)]
        public Nullable<DateTime> KQDate
        {
            get { return kqDate; }
            set { kqDate = value; }
        }
        #endregion
        #region 班別代碼
        /// <summary>
        /// 班別代碼
        /// </summary>
        [Column("shiftNo")]
        public string ShiftNo
        {
            get { return shiftNo; }
            set { shiftNo = value; }
        }
        #endregion
        #region 姓名
        /// <summary>
        /// 姓名
        /// </summary>
        [Column("localName", OnlySelect = true)]
        public string LocalName
        {
            get { return localName; }
            set { localName = value; }
        }
        #endregion
        #region 部門名稱
        /// <summary>
        /// 部門名稱
        /// </summary>
        [Column("depName", OnlySelect = true)]
        public string DepName
        {
            get { return depName; }
            set { depName = value; }
        }
        #endregion
        #region 部門代碼
        /// <summary>
        /// 部門代碼
        /// </summary>
        [Column("dCode", OnlySelect = true)]
        public string DCode
        {
            get { return dCode; }
            set { dCode = value; }
        }
        #endregion

        #region 加班開始時間
        /// <summary>
        ///加班開始時間
        /// </summary>
        [Column("otOnDutyTime")]
        public string OtOnDutyTime
        {
            get { return otOnDutyTime; }
            set { otOnDutyTime = value; }
        }
        #endregion
        #region 加班結束時間
        /// <summary>
        ///加班結束時間
        /// </summary>
        [Column("otOffDutyTime")]
        public string OtOffDutyTime
        {
            get { return otOffDutyTime; }
            set { otOffDutyTime = value; }
        }
        #endregion
        #region 班別描述
        /// <summary>
        /// 班別描述
        /// </summary>
        [Column("shiftDesc", OnlySelect = true)]
        public string ShiftDesc
        {
            get { return shiftDesc; }
            set { shiftDesc = value; }
        }
        #endregion
        #region 上班開始時間
        /// <summary>
        ///上班開始時間
        /// </summary>
        [Column("onDutyTime")]
        public string OnDutyTime
        {
            get { return onDutyTime; }
            set { onDutyTime = value; }
        }
        #endregion
        #region 下班時間
        /// <summary>
        ///下班時間
        /// </summary>
        [Column("offDutyTime")]
        public string OffDutyTime
        {
            get { return offDutyTime; }
            set { offDutyTime = value; }
        }
        #endregion


        #region 缺勤(遲到、早退、曠工)分鐘數
        /// <summary>
        /// 缺勤(遲到、早退、曠工)分鐘數
        /// </summary>
        [Column("absentQty")]
        public string AbsentQty
        {
            get { return absentQty; }
            set { absentQty = value; }
        }
        #endregion
        #region 考勤狀態
        /// <summary>
        /// 考勤狀態
        /// </summary>
        [Column("status")]
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        #endregion
        #region 考勤異常類別
        /// <summary>
        /// 考勤異常類別
        /// </summary>
        [Column("exceptionType")]
        public string ExceptionType
        {
            get { return exceptionType; }
            set { exceptionType = value; }
        }
        #endregion
        #region 異常原因類別
        /// <summary>
        /// 異常原因類別
        /// </summary>
        [Column("reasonType")]
        public string ReasonType
        {
            get { return reasonType; }
            set { reasonType = value; }
        }
        #endregion
        #region 異常原因描述
        /// <summary>
        /// 異常原因描述
        /// </summary>
        [Column("reasonRemark")]
        public string ReasonRemark
        {
            get { return reasonRemark; }
            set { reasonRemark = value; }
        }
        #endregion
        #region 異常原因名稱
        /// <summary>
        /// 異常原因名稱
        /// </summary>
        [Column("reasonName", OnlySelect = true)]
        public string ReasonName
        {
            get { return reasonName; }
            set { reasonName = value; }
        }
        #endregion
        #region 狀態名稱
        /// <summary>
        /// 狀態名稱
        /// </summary>
        [Column("statusName", OnlySelect = true)]
        public string StatusName
        {
            get { return statusName; }
            set { statusName = value; }
        }
        #endregion
        #region 加班時間
        /// <summary>
        /// 加班時間
        /// </summary>
        [Column("otHours", OnlySelect = true)]
        public string OtHours
        {
            get { return otHours; }
            set { otHours = value; }
        }
        #endregion
        #region 總時間
        /// <summary>
        /// 總時間
        /// </summary>
        [Column("totalHours", OnlySelect = true)]
        public string TotalHours
        {
            get { return totalHours; }
            set { totalHours = value; }
        }
        #endregion
        #region 總時間
        /// <summary>
        /// 總時間
        /// </summary>
        [Column("exceptionName", OnlySelect = true)]
        public string ExceptionName
        {
            get { return exceptionName; }
            set { exceptionName = value; }
        }
        #endregion
    }
}
