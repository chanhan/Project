/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMLeaveApplyYearQryModel.cs
 * 檔功能描述： 年休假統計查詢實體類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.29
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.KQM.Query
{
    [Serializable, TableName("gds_att_leaveyearsquery_v")]
    public class KQMLeaveApplyYearQryModel : ModelBase
    {
        private string workno;
        private int leaveyear;
        private Nullable<Decimal> startyears;
        private Nullable<Decimal> endyears;
        private Nullable<Decimal> outworkyears;
        private Nullable<Decimal> outfoxconnyears;
        private Nullable<Decimal> standarddays;
        private Nullable<Decimal> alreaddays;
        private Nullable<Decimal> nextyeardays;
        private Nullable<Decimal> leavedays;
        private Nullable<DateTime> reachdate;
        private Nullable<Decimal> reachleavedays;
        private Nullable<Decimal> leaverecdays;
        private Nullable<DateTime> updatedate;
        private Nullable<Decimal> lastyearremain;
        private Nullable<Decimal> currendays;
        private string depcode;
        private string localname;
        private string depname;
        private Nullable<DateTime> joindate;
        private Nullable<DateTime> enablestartdate;
        private string statusname;
        private string sexname;
        private string countdays;

        #region 工號
        /// <summary>
        /// 工號
        /// </summary>
        [Column("workno", OnlySelect = true)]
        public string WorkNo
        {
            get { return workno; }
            set { workno = value; }
        }
        #endregion

        #region 年份
        /// <summary>
        /// 年份
        /// </summary>
        [Column("leaveyear", OnlySelect = true)]
        public int LeaveYear
        {
            get { return leaveyear; }
            set { leaveyear = value; }
        }
        #endregion

        #region 年初年資
        /// <summary>
        /// 年初年資
        /// </summary>
        [Column("startyears",OnlySelect=true)]
        public Nullable<Decimal> StartYears
        {
            get { return startyears; }
            set { startyears = value; }
        }
        #endregion

        #region 年末年資
        /// <summary>
        /// 年末年資
        /// </summary>
        [Column("endyears",OnlySelect=true)]
        public Nullable<Decimal> EndYears
        {
            get { return endyears; }
            set { endyears = value; }
        }
        #endregion

        #region 廠外年資
        /// <summary>
        /// 廠外年資
        /// </summary>
        [Column("outworkyears",OnlySelect=true)]
        public Nullable<Decimal> OutWorkYears
        {
            get { return outworkyears; }
            set { outworkyears = value; }
        }
        #endregion

        #region 富士康經歷年資
        /// <summary>
        /// 富士康經歷年資
        /// </summary>
        [Column("outfoxconnyears",OnlySelect=true)]
        public Nullable<Decimal> OutFoxconnYears
        {
            get { return outfoxconnyears; }
            set { outfoxconnyears = value; }
        }
        #endregion

        #region 標準天數
        /// <summary>
        /// 標準天數
        /// </summary>
        [Column("standarddays",OnlySelect=true)]
        public Nullable<Decimal> StandardDays
        {
            get { return standarddays; }
            set { standarddays = value; }
        }
        #endregion

        #region 當年次年已休天數
        /// <summary>
        /// 當年次年已休天數
        /// </summary>
        [Column("alreaddays",OnlySelect=true)]
        public Nullable<Decimal> AlreadDays
        {
            get { return alreaddays; }
            set { alreaddays = value; }
        }
        #endregion

        #region 次年已休天數
        /// <summary>
        /// 次年已休天數
        /// </summary>
        [Column("nextyeardays",OnlySelect=true)]
        public Nullable<Decimal> NextYearDays
        {
            get { return nextyeardays; }
            set { nextyeardays = value; }
        }
        #endregion

        #region 目前可休天數
        /// <summary>
        /// 目前可休天數
        /// </summary>
        [Column("leavedays",OnlySelect=true)]
        public Nullable<Decimal> LeaveDays
        {
            get { return leavedays; }
            set { leavedays = value; }
        }
        #endregion

        #region 當天入廠日期
        /// <summary>
        /// 當天入廠日期
        /// </summary>
        [Column("reachdate",OnlySelect=true)]
        public Nullable<DateTime> ReachDate
        {
            get { return reachdate; }
            set { reachdate = value; }
        }
        #endregion

        #region 跨年資段多休天數
        /// <summary>
        /// 跨年資段多休天數
        /// </summary>
        [Column("reachleavedays",OnlySelect=true)]
        public Nullable<Decimal> ReachLeaveDays
        {
            get { return reachleavedays; }
            set { reachleavedays = value; }
        }
        #endregion

        #region 扣項天數
        /// <summary>
        /// 扣項天數
        /// </summary>
        [Column("leaverecdays",OnlySelect=true)]
        public Nullable<Decimal> LeaveRecDays
        {
            get { return leaverecdays; }
            set { leaverecdays = value; }
        }
        #endregion

        #region 更新時間
        /// <summary>
        /// 更新時間
        /// </summary>
        [Column("update_date",OnlySelect=true)]
        public Nullable<DateTime> UpdateDate
        {
            get { return updatedate; }
            set { updatedate = value; }
        }
        #endregion

        #region lastyearremain
        /// <summary>
        /// lastyearremain
        /// </summary>
        [Column("lastyearremain",OnlySelect=true)]
        public Nullable<Decimal> LastYearRemain
        {
            get { return lastyearremain; }
            set { lastyearremain = value; }
        }
        #endregion

        #region currendays
        /// <summary>
        /// currendays
        /// </summary>
        [Column("currendays", OnlySelect = true)]
        public Nullable<Decimal> CurrenDays
        {
            get { return currendays; }
            set { currendays = value; }
        }
        #endregion

        #region 部門CODE
        /// <summary>
        /// 部門CODE
        /// </summary>
        [Column("depcode", OnlySelect = true)]
        public string DepCode
        {
            get { return depcode; }
            set { depcode = value; }
        }
        #endregion

        #region 姓名
        /// <summary>
        /// 姓名
        /// </summary>
        [Column("localname", OnlySelect = true)]
        public string LocalName
        {
            get { return localname; }
            set { localname = value; }
        }
        #endregion

        #region 單位
        /// <summary>
        /// 單位
        /// </summary>
        [Column("depname", OnlySelect = true)]
        public string DepName
        {
            get { return depname; }
            set { depname = value; }
        }
        #endregion

        #region 入集團日期
        /// <summary>
        /// 入集團日期
        /// </summary>
        [Column("joindate",OnlySelect=true)]
        public Nullable<DateTime> JoinDate
        {
            get { return joindate; }
            set { joindate = value; }
        }
        #endregion

        #region 可休假開始日期
        /// <summary>
        /// 可休假開始日期
        /// </summary>
        [Column("enablestartdate",OnlySelect=true)]
        public Nullable<DateTime> EnableStartDate
        {
            get { return enablestartdate; }
            set { enablestartdate = value; }
        }
        #endregion

        #region 在職狀態
        /// <summary>
        /// 在職狀態
        /// </summary>
        [Column("statusname", OnlySelect = true)]
        public string StatusName
        {
            get { return statusname; }
            set { statusname = value; }
        }
        #endregion

        #region 性別
        /// <summary>
        /// 性別
        /// </summary>
        [Column("sexname", OnlySelect = true)]
        public string SexName
        {
            get { return sexname; }
            set { sexname = value; }
        }
        #endregion

        #region 統計天數
        /// <summary>
        /// 統計天數
        /// </summary>
        [Column("countdays", OnlySelect = true)]
        public string CountDays
        {
            get { return countdays; }
            set { countdays = value; }
        }
        #endregion


    }
}
