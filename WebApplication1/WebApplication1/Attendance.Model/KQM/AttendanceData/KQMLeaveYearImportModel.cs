/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMLeaveYearImportModel.cs
 * 檔功能描述：已休年假導入實體類
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.12.23
 * 
 */
using System;
using GDSBG.MiABU.Attendance.Common.Attributes;


namespace GDSBG.MiABU.Attendance.Model.KQM.AttendanceData
{
    /// <summary>
    /// 已休年假導入實體類
    /// </summary>
    [Serializable, TableName(" gds_att_leaveyearimport", SelectTable = "gds_att_leaveyearimport_v")]
    public class KQMLeaveYearImportModel:ModelBase
    {
        private string workNo;
        private Nullable<int> leaveYear;
        private Nullable<float> leaveDays;
        private string createUser;
        private Nullable<DateTime> createDate;
        private string modifier;
        private Nullable<DateTime> modifyDate;
        private string localName;
        private string dName;
        private string depCode;
        private string buOTMQryName;
        private string errorMsg;

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
        #region 年份
        /// <summary>
        ///年份
        /// </summary>
        [Column("leaveyear", IsPrimaryKey = true)]
        public Nullable<int> LeaveYear
        {
            get { return leaveYear; }
            set { leaveYear = value; }
        }
        #endregion
        #region 已休年休假時數
        /// <summary>
        /// 已休年休假時數
        /// </summary>
        [Column("leaveDays")]
        public Nullable<float> LeaveDays
        {
            get { return leaveDays; }
            set { leaveDays = value; }
        }
        #endregion
        #region 創建者
        /// <summary>
        /// 創建者
        /// </summary>
        [Column("create_user")]
        public string CreateUser
        {
            get { return createUser; }
            set { createUser = value; }
        }
        #endregion
        #region 創建日期
        /// <summary>
        ///創建日期
        /// </summary>
        [Column("create_date")]
        public Nullable<DateTime> CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
        #endregion
        #region 維護人
        /// <summary>
        /// 維護人
        /// </summary>
        [Column("update_user")]
        public string Modifier
        {
            get { return modifier; }
            set { modifier = value; }
        }
        #endregion
        #region 維護日期
        /// <summary>
        /// 維護日期
        /// </summary>
        [Column("update_date")]
        public Nullable<DateTime> ModifyDate
        {
            get { return modifyDate; }
            set { modifyDate = value; }
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
        [Column("dName", OnlySelect = true)]
        public string DName
        {
            get { return dName; }
            set { dName = value; }
        }
        #endregion
        #region 部門名稱
        /// <summary>
        /// 部門名稱
        /// </summary>
        [Column("depCode", OnlySelect = true)]
        public string DepCode
        {
            get { return depCode; }
            set { depCode = value; }
        }
        #endregion

        #region 事業處名稱
        /// <summary>
        /// 事業處名稱
        /// </summary>
        [Column("BUName", OnlySelect = true)]
        public string BuOTMQryName
        {
            get { return buOTMQryName; }
            set { buOTMQryName = value; }
        }
        #endregion


        #region 錯誤原因
        /// <summary>
        /// 費用代碼
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
