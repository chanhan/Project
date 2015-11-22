/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： SchduleDataModel.cs
 * 檔功能描述： 排班作業實體類
 * 
 * 版本：1.0
 * 創建標識： 劉炎 2011.12.19
 * 
 */
using System;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.KQM.AttendanceData
{
    /// <summary>
    /// 排班作業實體類
    /// </summary>
    [Serializable, TableName("GDS_ATT_EMPLOYEESHIFT", SelectTable = "GDS_ATT_SCHDULE_V")]
    public class ScheduleDataModel : ModelBase
    {
        private string id;
        private string workNo;
        private string localName;
        private string dCode;
        private string dName;
        private Nullable<DateTime> startDate;
        private Nullable<DateTime> endDate;
        private string startEndDate;
        private string shiftNo;
        private Nullable<DateTime> leaveDate;
        private string shiftDesc;
        private string shiftFlag;
        private Nullable<DateTime> updateUser;
        private Nullable<DateTime> updateDate;
        private Nullable<decimal> shiftDate;

        #region ID
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID", IsPrimaryKey = true)]
        public string Id
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

        #region 姓名
        /// <summary>
        /// 姓名
        /// </summary>
        [Column("LOCALNAME", OnlySelect = true)]
        public string LocalName
        {
            get { return localName; }
            set { localName = value; }
        }
        #endregion

        #region 如果TEAMCODE為空或Null：DCODE=DEPCODE，DNAME=DEPNAME；否則：DCODE=TEAMCODE，DNAME=DEPNAME(TEAMNAME)
        /// <summary>
        /// 如果TEAMCODE為空或Null：DCODE=DEPCODE，DNAME=DEPNAME；否則：DCODE=TEAMCODE，DNAME=DEPNAME(TEAMNAME)
        /// </summary>
        [Column("DCODE", OnlySelect = true)]
        public string DCode
        {
            get { return dCode; }
            set { dCode = value; }
        }
        #endregion

        #region  如果TEAMCODE為空或Null：DCODE=DEPCODE，DNAME=DEPNAME；否則：DCODE=TEAMCODE，DNAME=DEPNAME(TEAMNAME)
        /// <summary>
        /// 如果TEAMCODE為空或Null：DCODE=DEPCODE，DNAME=DEPNAME；否則：DCODE=TEAMCODE，DNAME=DEPNAME(TEAMNAME)
        /// </summary>
        [Column("DNAME", OnlySelect = true)]
        public string DName
        {
            get { return dName; }
            set { dName = value; }
        }
        #endregion

        #region 開始時間
        /// <summary>
        /// 開始時間
        /// </summary>
        [Column("STARTDATE")]
        public Nullable<DateTime> StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        #endregion

        #region 結束時間
        /// <summary>
        /// 結束時間
        /// </summary>
        [Column("ENDDATE")]
        public Nullable<DateTime> EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }
        #endregion

        #region 開始結束時間
        /// <summary>
        /// 開始結束時間
        /// </summary>
        [Column("STARTENDDATE", OnlySelect = true)]
        public string StartEndDate
        {
            get { return startEndDate; }
            set { startEndDate = value; }
        }
        #endregion

        #region 班別編碼
        /// <summary>
        /// 班別編碼
        /// </summary>
        [Column("SHIFTNO")]
        public string ShiftNo
        {
            get { return shiftNo; }
            set { shiftNo = value; }
        }
        #endregion

        #region 離職時間
        /// <summary>
        /// 離職時間
        /// </summary>
        [Column("LEAVEDATE", OnlySelect = true)]
        public Nullable<DateTime> LeaveDate
        {
            get { return leaveDate; }
            set { leaveDate = value; }
        }
        #endregion

        #region 班別描述
        /// <summary>
        /// 班別描述
        /// </summary>
        [Column("SHIFTDESC", OnlySelect = true)]
        public string ShiftDesc
        {
            get { return shiftDesc; }
            set { shiftDesc = value; }
        }
        #endregion

        #region  是否免考勤
        /// <summary>
        /// 是否免考勤
        /// </summary>
        [Column("SHIFTFLAG", OnlySelect = true)]
        public string ShiftFlag
        {
            get { return shiftFlag; }
            set { shiftFlag = value; }
        }
        #endregion

        #region 維護人
        /// <summary>
        /// 維護人
        /// </summary>
        [Column("UPDATE_USER")]
        public Nullable<DateTime> UpdateUser
        {
            get { return updateUser; }
            set { updateUser = value; }
        }
        #endregion

        #region 維護時間
        /// <summary>
        /// 維護時間
        /// </summary>
        [Column("UPDATE_DATE")]
        public Nullable<DateTime> UpdateDate
        {
            get { return updateDate; }
            set { updateDate = value; }
        }
        #endregion

        #region 排班時間
        /// <summary>
        /// 排班時間
        /// </summary>
        [Column("SHIFTDATE")]
        public Nullable<decimal> ShiftDate
        {
            get { return shiftDate; }
            set { shiftDate = value; }
        }
        #endregion
    }
}
