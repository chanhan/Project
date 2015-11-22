/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： BUCalendarModel.cs
 * 檔功能描述： BU行事歷實體類
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.12.20
 * 
 */
using System;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.KQM.BasicData
{
    /// <summary>
    /// BU行事歷實體類
    /// </summary>
    [Serializable, TableName(" gds_att_bucalendar", SelectTable = "gds_att_bucalendar_v")]
    public class BUCalendarModel:ModelBase
    {
        private string buCode;
        private Nullable<DateTime> workDay;
        private string weekNo;
        private string weekDay;
        private string workFlag;
        private string holidayFlag;
        private string remark;
        private string modifier;
        private Nullable<DateTime> modifyDate;
        private string orgName;
        private string costCode;
        private string errorMsg;
        //private string depName;


        #region BU事業處CODE
        /// <summary>
        /// BU事業處CODE
        /// </summary>
        [Column("buCode",IsPrimaryKey=true)]
        public string BUCode
        {
            get { return buCode; }
            set { buCode = value; }
        }
        #endregion

        //#region BU事業處名稱
        ///// <summary>
        ///// BU事業處CODE
        ///// </summary>
        //[Column("depname")]
        //public string DepName
        //{
        //    get { return depName; }
        //    set { depName = value; }
        //}
        //#endregion
        #region 日期
        /// <summary>
        ///日期
        /// </summary>
        [Column("workDay",IsPrimaryKey=true)]
        public Nullable<DateTime> WorkDay
        {
            get { return workDay; }
            set { workDay = value; }
        }
        #endregion
        #region 第幾周
        /// <summary>
        /// 第幾周
        /// </summary>
        [Column("weekNo")]
        public string  WeekNo
        {
            get { return weekNo; }
            set { weekNo = value; }
        }
        #endregion
        #region 星期幾
        /// <summary>
        /// 星期幾
        /// </summary>
        [Column("weekDay")]
        public string WeekDay
        {
            get { return weekDay; }
            set { weekDay = value; }
        }
        #endregion

        #region 是否工作日
        /// <summary>
        ///是否工作日
        /// </summary>
        [Column("workFlag")]
        public string WorkFlag
        {
            get { return workFlag; }
            set { workFlag = value; }
        }
        #endregion
        #region 是否節假日
        /// <summary>
        ///是否節假日
        /// </summary>
        [Column("holidayFlag")]
        public string  HolidayFlag
        {
            get { return holidayFlag; }
            set { holidayFlag = value; }
        }
        #endregion
        #region 備註
        /// <summary>
        /// 備註
        /// </summary>
        [Column("remark")]
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
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

        #region 單位名稱
        /// <summary>
        /// 單位名稱
        /// </summary>
        [Column("orgName", OnlySelect = true)]
        public string  OrgName
        {
            get { return orgName; }
            set { orgName = value;}
        }
        #endregion
        #region 費用代碼
        /// <summary>
        /// 費用代碼
        /// </summary>
        [Column("costcode", OnlySelect = true)]
        public string CostCode
        {
            get { return costCode; }
            set { costCode = value; }
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
