/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OTMTotalQryModel.cs
 * 檔功能描述：加班匯總查詢實體類
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.12.30
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.KQM.Query.OTM
{
    /// <summary>
    /// 加班匯總查詢實體類
    /// </summary>
    [Serializable, TableName("gds_att_monthtotal", SelectTable = "gds_att_monthtotal_v")]
    public class OTMTotalQryModel : ModelBase
    {

        private string workNo;
        private string yearMonth;
        private string depCode;
        //private string g1UpLmt;
        //private string g2UpLmt;
        //private string g3UpLmt;
        //private string g12UpLmt;
        private Nullable<decimal> g1Apply;
        private Nullable<decimal> g2Apply;
        private Nullable<decimal> g3Apply;
        private Nullable<decimal> g1RelSalary;
        private Nullable<decimal> g2RelSalary;
        private Nullable<decimal> g3RelSalary;
        private Nullable<decimal> mAdjust1;
        private Nullable<decimal> g1Remain;
        private Nullable<decimal> g2Remain;
        private Nullable<decimal> g3Remain;
        private Nullable<decimal> mRelAdjust;
        //private Nullable<decimal> canadjPrehy;
        //private Nullable<decimal> canadjLasthy;
        private string billNo;
        private string apRemark;
        private string approver;
        private Nullable<DateTime> approveDate;

        private string approveFlag;

        private Nullable<decimal> specg1Apply;
        private Nullable<decimal> specg2Apply;
        private Nullable<decimal> specg3Apply;
        private Nullable<decimal> specg1Salary;
        private Nullable<decimal> specg2Salary;
        private Nullable<decimal> specg3Salary;
        private string overTimeType;

        //private Nullable<decimal> advanceAdjust;
        //private Nullable<decimal> restAdjust;

        private string localName;
        private string dName;
        //private string monthTotal;
        private Nullable<decimal> day1;
        private Nullable<decimal> day2;
        private Nullable<decimal> day3;
        private Nullable<decimal> day4;
        private Nullable<decimal> day5;
        private Nullable<decimal> day6;
        private Nullable<decimal> day7;
        private Nullable<decimal> day8;
        private Nullable<decimal> day9;
        private Nullable<decimal> day10;
        private Nullable<decimal> day11;
        private Nullable<decimal> day12;
        private Nullable<decimal> day13;
        private Nullable<decimal> day14;
        private Nullable<decimal> day15;
        private Nullable<decimal> day16;
        private Nullable<decimal> day17;
        private Nullable<decimal> day18;
        private Nullable<decimal> day19;
        private Nullable<decimal> day20;
        private Nullable<decimal> day21;
        private Nullable<decimal> day22;
        private Nullable<decimal> day23;
        private Nullable<decimal> day24;
        private Nullable<decimal> day25;
        private Nullable<decimal> day26;
        private Nullable<decimal> day27;
        private Nullable<decimal> day28;
        private Nullable<decimal> day29;
        private Nullable<decimal> day30;
        private Nullable<decimal> day31;

        private Nullable<decimal> specDay1;
        private Nullable<decimal> specDay2;
        private Nullable<decimal> specDay3;
        private Nullable<decimal> specDay4;
        private Nullable<decimal> specDay5;
        private Nullable<decimal> specDay6;
        private Nullable<decimal> specDay7;
        private Nullable<decimal> specDay8;
        private Nullable<decimal> specDay9;
        private Nullable<decimal> specDay10;
        private Nullable<decimal> specDay11;
        private Nullable<decimal> specDay12;
        private Nullable<decimal> specDay13;
        private Nullable<decimal> specDay14;
        private Nullable<decimal> specDay15;
        private Nullable<decimal> specDay16;
        private Nullable<decimal> specDay17;
        private Nullable<decimal> specDay18;
        private Nullable<decimal> specDay19;
        private Nullable<decimal> specDay20;
        private Nullable<decimal> specDay21;
        private Nullable<decimal> specDay22;
        private Nullable<decimal> specDay23;
        private Nullable<decimal> specDay24;
        private Nullable<decimal> specDay25;
        private Nullable<decimal> specDay26;
        private Nullable<decimal> specDay27;
        private Nullable<decimal> specDay28;
        private Nullable<decimal> specDay29;
        private Nullable<decimal> specDay30;
        private Nullable<decimal> specDay31;

        //private string premonDay28;
        //private string premonDay29;
        //private string premonDay30;
        //private string premonDay31;
        private string buName;
        private string approveFlagName;
        private string flag;

        #region  1日專案加班時數(otm_realapply中的專案加班時數)
        /// <summary>
        ///1日專案加班時數(otm_realapply中的專案加班時數)
        /// </summary>
        [Column("specDay1", OnlySelect = true)]
        public Nullable<decimal> SpecDay1
        {
            get { return specDay1; }
            set { specDay1 = value; }
        }
        #endregion
        #region  2日專案加班時數(otm_realapply中的專案加班時數)
        /// <summary>
        ///2日專案加班時數(otm_realapply中的專案加班時數)
        /// </summary>
        [Column("specDay2", OnlySelect = true)]
        public Nullable<decimal> SpecDay2
        {
            get { return specDay2; }
            set { specDay2 = value; }
        }
        #endregion
        #region  3日專案加班時數(otm_realapply中的專案加班時數)
        /// <summary>
        ///3日專案加班時數(otm_realapply中的專案加班時數)
        /// </summary>
        [Column("specDay3", OnlySelect = true)]
        public Nullable<decimal> SpecDay3
        {
            get { return specDay3; }
            set { specDay3 = value; }
        }
        #endregion
        #region  4日專案加班時數(otm_realapply中的專案加班時數)
        /// <summary>
        ///4日專案加班時數(otm_realapply中的專案加班時數)
        /// </summary>
        [Column("specDay4", OnlySelect = true)]
        public Nullable<decimal> SpecDay4
        {
            get { return specDay4; }
            set { specDay4 = value; }
        }
        #endregion
        #region  5日專案加班時數(otm_realapply中的專案加班時數)
        /// <summary>
        ///5日專案加班時數(otm_realapply中的專案加班時數)
        /// </summary>
        [Column("specDay5", OnlySelect = true)]
        public Nullable<decimal> SpecDay5
        {
            get { return specDay5; }
            set { specDay5 = value; }
        }
        #endregion
        #region  6日專案加班時數(otm_realapply中的專案加班時數)
        /// <summary>
        ///6日專案加班時數(otm_realapply中的專案加班時數)
        /// </summary>
        [Column("specDay6", OnlySelect = true)]
        public Nullable<decimal> SpecDay6
        {
            get { return specDay6; }
            set { specDay6 = value; }
        }
        #endregion
        #region  7日專案加班時數(otm_realapply中的專案加班時數)
        /// <summary>
        ///7日專案加班時數(otm_realapply中的專案加班時數)
        /// </summary>
        [Column("specDay7", OnlySelect = true)]
        public Nullable<decimal> SpecDay7
        {
            get { return specDay7; }
            set { specDay7 = value; }
        }
        #endregion
        #region  8日專案加班時數(otm_realapply中的專案加班時數)
        /// <summary>
        ///8日專案加班時數(otm_realapply中的專案加班時數)
        /// </summary>
        [Column("specDay8", OnlySelect = true)]
        public Nullable<decimal> SpecDay8
        {
            get { return specDay8; }
            set { specDay8 = value; }
        }
        #endregion
        #region  9日專案加班時數(otm_realapply中的專案加班時數)
        /// <summary>
        ///9日專案加班時數(otm_realapply中的專案加班時數)
        /// </summary>
        [Column("specDay9", OnlySelect = true)]
        public Nullable<decimal> SpecDay9
        {
            get { return specDay9; }
            set { specDay9 = value; }
        }
        #endregion
        #region  10日專案加班時數(otm_realapply中的專案加班時數)
        /// <summary>
        ///10日專案加班時數(otm_realapply中的專案加班時數)
        /// </summary>
        [Column("specDay10", OnlySelect = true)]
        public Nullable<decimal> SpecDay10
        {
            get { return specDay10; }
            set { specDay10 = value; }
        }
        #endregion
        #region  11日專案加班時數(otm_realapply中的專案加班時數)
        /// <summary>
        ///11日專案加班時數(otm_realapply中的專案加班時數)
        /// </summary>
        [Column("specDay11", OnlySelect = true)]
        public Nullable<decimal> SpecDay11
        {
            get { return specDay11; }
            set { specDay11 = value; }
        }
        #endregion
        #region  12日專案加班時數(otm_realapply中的專案加班時數)
        /// <summary>
        ///12日專案加班時數(otm_realapply中的專案加班時數)
        /// </summary>
        [Column("specDay12", OnlySelect = true)]
        public Nullable<decimal> SpecDay12
        {
            get { return specDay12; }
            set { specDay12 = value; }
        }
        #endregion
        #region  13日專案加班時數(otm_realapply中的專案加班時數)
        /// <summary>
        ///13日專案加班時數(otm_realapply中的專案加班時數)
        /// </summary>
        [Column("specDay13", OnlySelect = true)]
        public Nullable<decimal> SpecDay13
        {
            get { return specDay13; }
            set { specDay13 = value; }
        }
        #endregion
        #region  14日專案加班時數(otm_realapply中的專案加班時數)
        /// <summary>
        ///14日專案加班時數(otm_realapply中的專案加班時數)
        /// </summary>
        [Column("specDay14", OnlySelect = true)]
        public Nullable<decimal> SpecDay14
        {
            get { return specDay14; }
            set { specDay14 = value; }
        }
        #endregion
        #region  15日專案加班時數(otm_realapply中的專案加班時數)
        /// <summary>
        ///15日專案加班時數(otm_realapply中的專案加班時數)
        /// </summary>
        [Column("specDay15", OnlySelect = true)]
        public Nullable<decimal> SpecDay15
        {
            get { return specDay15; }
            set { specDay15 = value; }
        }
        #endregion
        #region  16日專案加班時數(otm_realapply中的專案加班時數)
        /// <summary>
        ///16日專案加班時數(otm_realapply中的專案加班時數)
        /// </summary>
        [Column("specDay16", OnlySelect = true)]
        public Nullable<decimal> SpecDay16
        {
            get { return specDay16; }
            set { specDay16 = value; }
        }
        #endregion
        #region  17日專案加班時數(otm_realapply中的專案加班時數)
        /// <summary>
        ///17日專案加班時數(otm_realapply中的專案加班時數)
        /// </summary>
        [Column("specDay17", OnlySelect = true)]
        public Nullable<decimal> SpecDay17
        {
            get { return specDay17; }
            set { specDay17 = value; }
        }
        #endregion
        #region  18日專案加班時數(otm_realapply中的專案加班時數)
        /// <summary>
        ///18日專案加班時數(otm_realapply中的專案加班時數)
        /// </summary>
        [Column("specDay18", OnlySelect = true)]
        public Nullable<decimal> SpecDay18
        {
            get { return specDay18; }
            set { specDay18 = value; }
        }
        #endregion
        #region  19日專案加班時數(otm_realapply中的專案加班時數)
        /// <summary>
        ///19日專案加班時數(otm_realapply中的專案加班時數)
        /// </summary>
        [Column("specDay19", OnlySelect = true)]
        public Nullable<decimal> SpecDay19
        {
            get { return specDay19; }
            set { specDay19 = value; }
        }
        #endregion
        #region  20日專案加班時數(otm_realapply中的專案加班時數)
        /// <summary>
        ///20日專案加班時數(otm_realapply中的專案加班時數)
        /// </summary>
        [Column("specDay20", OnlySelect = true)]
        public Nullable<decimal> SpecDay20
        {
            get { return specDay20; }
            set { specDay20 = value; }
        }
        #endregion
        #region  21日專案加班時數(otm_realapply中的專案加班時數)
        /// <summary>
        ///21日專案加班時數(otm_realapply中的專案加班時數)
        /// </summary>
        [Column("specDay21", OnlySelect = true)]
        public Nullable<decimal> SpecDay21
        {
            get { return specDay21; }
            set { specDay21 = value; }
        }
        #endregion
        #region  22日專案加班時數(otm_realapply中的專案加班時數)
        /// <summary>
        ///22日專案加班時數(otm_realapply中的專案加班時數)
        /// </summary>
        [Column("specDay22", OnlySelect = true)]
        public Nullable<decimal> SpecDay22
        {
            get { return specDay22; }
            set { specDay22 = value; }
        }
        #endregion
        #region  23日專案加班時數(otm_realapply中的專案加班時數)
        /// <summary>
        ///23日專案加班時數(otm_realapply中的專案加班時數)
        /// </summary>
        [Column("specDay23", OnlySelect = true)]
        public Nullable<decimal> SpecDay23
        {
            get { return specDay23; }
            set { specDay23 = value; }
        }
        #endregion
        #region  24日專案加班時數(otm_realapply中的專案加班時數)
        /// <summary>
        ///24日專案加班時數(otm_realapply中的專案加班時數)
        /// </summary>
        [Column("specDay24", OnlySelect = true)]
        public Nullable<decimal> SpecDay24
        {
            get { return specDay24; }
            set { specDay24 = value; }
        }
        #endregion
        #region  25日專案加班時數(otm_realapply中的專案加班時數)
        /// <summary>
        ///25日專案加班時數(otm_realapply中的專案加班時數)
        /// </summary>
        [Column("specDay25", OnlySelect = true)]
        public Nullable<decimal> SpecDay25
        {
            get { return specDay25; }
            set { specDay25 = value; }
        }
        #endregion
        #region  26日專案加班時數(otm_realapply中的專案加班時數)
        /// <summary>
        ///26日專案加班時數(otm_realapply中的專案加班時數)
        /// </summary>
        [Column("specDay26", OnlySelect = true)]
        public Nullable<decimal> SpecDay26
        {
            get { return specDay26; }
            set { specDay26 = value; }
        }
        #endregion
        #region  27日專案加班時數(otm_realapply中的專案加班時數)
        /// <summary>
        ///27日專案加班時數(otm_realapply中的專案加班時數)
        /// </summary>
        [Column("specDay27", OnlySelect = true)]
        public Nullable<decimal> SpecDay27
        {
            get { return specDay27; }
            set { specDay27 = value; }
        }
        #endregion
        #region  28日專案加班時數(otm_realapply中的專案加班時數)
        /// <summary>
        ///28日專案加班時數(otm_realapply中的專案加班時數)
        /// </summary>
        [Column("specDay28", OnlySelect = true)]
        public Nullable<decimal> SpecDay28
        {
            get { return specDay28; }
            set { specDay28 = value; }
        }
        #endregion
        #region  29日專案加班時數(otm_realapply中的專案加班時數)
        /// <summary>
        ///29日專案加班時數(otm_realapply中的專案加班時數)
        /// </summary>
        [Column("specDay29", OnlySelect = true)]
        public Nullable<decimal> SpecDay29
        {
            get { return specDay29; }
            set { specDay29 = value; }
        }
        #endregion
        #region  30日專案加班時數(otm_realapply中的專案加班時數)
        /// <summary>
        ///30日專案加班時數(otm_realapply中的專案加班時數)
        /// </summary>
        [Column("specDay30", OnlySelect = true)]
        public Nullable<decimal> SpecDay30
        {
            get { return specDay30; }
            set { specDay30 = value; }
        }
        #endregion
        #region  31日專案加班時數(otm_realapply中的專案加班時數)
        /// <summary>
        ///31日專案加班時數(otm_realapply中的專案加班時數)
        /// </summary>
        [Column("specDay31", OnlySelect = true)]
        public Nullable<decimal> SpecDay31
        {
            get { return specDay31; }
            set { specDay31 = value; }
        }
        #endregion
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

        #region 年月
        /// <summary>
        /// 年月
        /// </summary>
        [Column("yearMonth", IsPrimaryKey = true)]
        public string YearMonth
        {
            get { return yearMonth; }
            set { yearMonth = value; }
        }
        #endregion
        #region 部門代碼
        /// <summary>
        /// 部門代碼
        /// </summary>
        [Column("depCode")]
        public string DepCode
        {
            get { return depCode; }
            set { depCode = value; }
        }
        #endregion
        //#region G1月加班上限
        ///// <summary>
        ///// G1月加班上限
        ///// </summary>
        //[Column("g1UpLmt")]
        //public string G1UpLmt
        //{
        //    get { return g1UpLmt; }
        //    set { g1UpLmt = value; }
        //}
        //#endregion
        //#region G2月加班上限
        ///// <summary>
        ///// G2月加班上限
        ///// </summary>
        //[Column("g2UpLmt")]
        //public string G2UpLmt
        //{
        //    get { return g2UpLmt; }
        //    set { g2UpLmt = value; }
        //}
        //#endregion
        //#region G3月加班上限
        ///// <summary>
        ///// G3月加班上限
        ///// </summary>
        //[Column("g3UpLmt")]
        //public string G3UpLmt
        //{
        //    get { return g3UpLmt; }
        //    set { g3UpLmt = value; }
        //}
        //#endregion
        //#region G12月加班上限
        ///// <summary>
        ///// G1月加班上限
        ///// </summary>
        //[Column("g12UpLmt")]
        //public string G12UpLmt
        //{
        //    get { return g12UpLmt; }
        //    set { g12UpLmt = value; }
        //}
        //#endregion

        #region G1有效加班時數
        /// <summary>
        /// G1有效加班時數
        /// </summary>
        [Column("g1Apply")]
        public Nullable<decimal> G1Apply
        {
            get { return g1Apply; }
            set { g1Apply = value; }
        }
        #endregion
        #region G2有效加班時數
        /// <summary>
        /// G2有效加班時數
        /// </summary>
        [Column("g2Apply")]
        public Nullable<decimal> G2Apply
        {
            get { return g2Apply; }
            set { g2Apply = value; }
        }
        #endregion
        #region G3有效加班時數
        /// <summary>
        /// G3有效加班時數
        /// </summary>
        [Column("g3Apply")]
        public Nullable<decimal> G3Apply
        {
            get { return g3Apply; }
            set { g3Apply = value; }
        }
        #endregion

        #region G1實發加班時數
        /// <summary>
        /// G1實發加班時數
        /// </summary>
        [Column("g1RelSalary")]
        public Nullable<decimal> G1RelSalary
        {
            get { return g1RelSalary; }
            set { g1RelSalary = value; }
        }
        #endregion
        #region G2實發加班時數
        /// <summary>
        /// G2實發加班時數
        /// </summary>
        [Column("g2RelSalary")]
        public Nullable<decimal> G2RelSalary
        {
            get { return g2RelSalary; }
            set { g2RelSalary = value; }
        }
        #endregion
        #region G3實發加班時數
        /// <summary>
        /// G3實發加班時數
        /// </summary>
        [Column("g3RelSalary")]
        public Nullable<decimal> G3RelSalary
        {
            get { return g3RelSalary; }
            set { g3RelSalary = value; }
        }
        #endregion

        #region 當月調休
        /// <summary>
        /// 當月調休
        /// </summary>
        [Column("mAdjust1")]
        public Nullable<decimal> MAdjust1
        {
            get { return mAdjust1; }
            set { mAdjust1 = value; }
        }
        #endregion
        #region  G1剩餘加班時數
        /// <summary>
        /// G1剩餘加班時數
        /// </summary>
        [Column("g1Remain")]
        public Nullable<decimal> G1Remain
        {
            get { return g1Remain; }
            set { g1Remain = value; }
        }
        #endregion
        #region  G2剩餘加班時數
        /// <summary>
        /// G2剩餘加班時數
        /// </summary>
        [Column("g2Remain")]
        public Nullable<decimal> G2Remain
        {
            get { return g2Remain; }
            set { g2Remain = value; }
        }
        #endregion
        #region  G3剩餘加班時數
        /// <summary>
        /// G3剩餘加班時數
        /// </summary>
        [Column("g3Remain")]
        public Nullable<decimal> G3Remain
        {
            get { return g3Remain; }
            set { g3Remain = value; }
        }
        #endregion

        #region  當月可調
        /// <summary>
        ///當月可調
        /// </summary>
        [Column("mRelAdjust")]
        public Nullable<decimal> MRelAdjust
        {
            get { return mRelAdjust; }
            set { mRelAdjust = value; }
        }
        #endregion
        #region  G1專案有效時數
        /// <summary>
        ///G1專案有效時數
        /// </summary>
        [Column("specg1Apply")]
        public Nullable<decimal> SpecG1Apply
        {
            get { return specg1Apply; }
            set { specg1Apply = value; }
        }
        #endregion
        #region  G2專案有效時數
        /// <summary>
        ///G2專案有效時數
        /// </summary>
        [Column("specg2Apply")]
        public Nullable<decimal> SpecG2Apply
        {
            get { return specg2Apply; }
            set { specg2Apply = value; }
        }
        #endregion
        #region  G3專案有效時數
        /// <summary>
        ///G3專案有效時數
        /// </summary>
        [Column("specg3Apply")]
        public Nullable<decimal> SpecG3Apply
        {
            get { return specg3Apply; }
            set { specg3Apply = value; }
        }
        #endregion

        #region  G1專案實發時數
        /// <summary>
        ///G1專案實發時數
        /// </summary>
        [Column("specg1Salary")]
        public Nullable<decimal> SpecG1Salary
        {
            get { return specg1Salary; }
            set { specg1Salary = value; }
        }
        #endregion
        #region  G2專案實發時數
        /// <summary>
        ///G2專案實發時數
        /// </summary>
        [Column("specg2Salary")]
        public Nullable<decimal> SpecG2Salary
        {
            get { return specg2Salary; }
            set { specg2Salary = value; }
        }
        #endregion
        #region  G3專案實發時數
        /// <summary>
        ///G3專案實發時數
        /// </summary>
        [Column("specg3Salary")]
        public Nullable<decimal> SpecG3Salary
        {
            get { return specg3Salary; }
            set { specg3Salary = value; }
        }
        #endregion

        #region  加班類別
        /// <summary>
        ///加班類別
        /// </summary>
        [Column("overTimeType")]
        public string OverTimeType
        {
            get { return overTimeType; }
            set { overTimeType = value; }
        }
        #endregion
        #region  1日非專案加班時數(otm_realapply中的非專案加班時數)
        /// <summary>
        ///1日非專案加班時數(otm_realapply中的非專案加班時數)
        /// </summary>
        [Column("day1", OnlySelect = true)]
        public Nullable<decimal> Day1
        {
            get { return day1; }
            set { day1 = value; }
        }
        #endregion
        #region  2日非專案加班時數(otm_realapply中的非專案加班時數)
        /// <summary>
        ///2日非專案加班時數(otm_realapply中的非專案加班時數)
        /// </summary>
        [Column("day2", OnlySelect = true)]
        public Nullable<decimal> Day2
        {
            get { return day2; }
            set { day2 = value; }
        }
        #endregion
        #region  3日非專案加班時數(otm_realapply中的非專案加班時數)
        /// <summary>
        ///3日非專案加班時數(otm_realapply中的非專案加班時數)
        /// </summary>
        [Column("day3", OnlySelect = true)]
        public Nullable<decimal> Day3
        {
            get { return day3; }
            set { day3 = value; }
        }
        #endregion
        #region  4日非專案加班時數(otm_realapply中的非專案加班時數)
        /// <summary>
        ///4日非專案加班時數(otm_realapply中的非專案加班時數)
        /// </summary>
        [Column("day4", OnlySelect = true)]
        public Nullable<decimal> Day4
        {
            get { return day4; }
            set { day4 = value; }
        }
        #endregion
        #region  5日非專案加班時數(otm_realapply中的非專案加班時數)
        /// <summary>
        ///5日非專案加班時數(otm_realapply中的非專案加班時數)
        /// </summary>
        [Column("day5", OnlySelect = true)]
        public Nullable<decimal> Day5
        {
            get { return day5; }
            set { day5 = value; }
        }
        #endregion
        #region  6日非專案加班時數(otm_realapply中的非專案加班時數)
        /// <summary>
        ///6日非專案加班時數(otm_realapply中的非專案加班時數)
        /// </summary>
        [Column("day6", OnlySelect = true)]
        public Nullable<decimal> Day6
        {
            get { return day6; }
            set { day6 = value; }
        }
        #endregion
        #region  7日非專案加班時數(otm_realapply中的非專案加班時數)
        /// <summary>
        ///7日非專案加班時數(otm_realapply中的非專案加班時數)
        /// </summary>
        [Column("day7", OnlySelect = true)]
        public Nullable<decimal> Day7
        {
            get { return day7; }
            set { day7 = value; }
        }
        #endregion
        #region  8日非專案加班時數(otm_realapply中的非專案加班時數)
        /// <summary>
        ///8日非專案加班時數(otm_realapply中的非專案加班時數)
        /// </summary>
        [Column("day8", OnlySelect = true)]
        public Nullable<decimal> Day8
        {
            get { return day8; }
            set { day8 = value; }
        }
        #endregion
        #region  9日非專案加班時數(otm_realapply中的非專案加班時數)
        /// <summary>
        ///9日非專案加班時數(otm_realapply中的非專案加班時數)
        /// </summary>
        [Column("day9", OnlySelect = true)]
        public Nullable<decimal> Day9
        {
            get { return day9; }
            set { day9 = value; }
        }
        #endregion
        #region  10日非專案加班時數(otm_realapply中的非專案加班時數)
        /// <summary>
        ///10日非專案加班時數(otm_realapply中的非專案加班時數)
        /// </summary>
        [Column("day10", OnlySelect = true)]
        public Nullable<decimal> Day10
        {
            get { return day10; }
            set { day10 = value; }
        }
        #endregion
        #region  11日非專案加班時數(otm_realapply中的非專案加班時數)
        /// <summary>
        ///11日非專案加班時數(otm_realapply中的非專案加班時數)
        /// </summary>
        [Column("day11", OnlySelect = true)]
        public Nullable<decimal> Day11
        {
            get { return day11; }
            set { day11 = value; }
        }
        #endregion
        #region  12日非專案加班時數(otm_realapply中的非專案加班時數)
        /// <summary>
        ///12日非專案加班時數(otm_realapply中的非專案加班時數)
        /// </summary>
        [Column("day12", OnlySelect = true)]
        public Nullable<decimal> Day12
        {
            get { return day12; }
            set { day12 = value; }
        }
        #endregion
        #region  13日非專案加班時數(otm_realapply中的非專案加班時數)
        /// <summary>
        ///13日非專案加班時數(otm_realapply中的非專案加班時數)
        /// </summary>
        [Column("day13", OnlySelect = true)]
        public Nullable<decimal> Day13
        {
            get { return day13; }
            set { day13 = value; }
        }
        #endregion
        #region  14日非專案加班時數(otm_realapply中的非專案加班時數)
        /// <summary>
        ///14日非專案加班時數(otm_realapply中的非專案加班時數)
        /// </summary>
        [Column("day14", OnlySelect = true)]
        public Nullable<decimal> Day14
        {
            get { return day14; }
            set { day14 = value; }
        }
        #endregion
        #region  15日非專案加班時數(otm_realapply中的非專案加班時數)
        /// <summary>
        ///15日非專案加班時數(otm_realapply中的非專案加班時數)
        /// </summary>
        [Column("day15", OnlySelect = true)]
        public Nullable<decimal> Day15
        {
            get { return day15; }
            set { day15 = value; }
        }
        #endregion
        #region  16日非專案加班時數(otm_realapply中的非專案加班時數)
        /// <summary>
        ///16日非專案加班時數(otm_realapply中的非專案加班時數)
        /// </summary>
        [Column("day16", OnlySelect = true)]
        public Nullable<decimal> Day16
        {
            get { return day16; }
            set { day16 = value; }
        }
        #endregion
        #region  17日非專案加班時數(otm_realapply中的非專案加班時數)
        /// <summary>
        ///17日非專案加班時數(otm_realapply中的非專案加班時數)
        /// </summary>
        [Column("day17", OnlySelect = true)]
        public Nullable<decimal> Day17
        {
            get { return day17; }
            set { day17 = value; }
        }
        #endregion
        #region  18日非專案加班時數(otm_realapply中的非專案加班時數)
        /// <summary>
        ///18日非專案加班時數(otm_realapply中的非專案加班時數)
        /// </summary>
        [Column("day18", OnlySelect = true)]
        public Nullable<decimal> Day18
        {
            get { return day18; }
            set { day18 = value; }
        }
        #endregion
        #region  19日非專案加班時數(otm_realapply中的非專案加班時數)
        /// <summary>
        ///19日非專案加班時數(otm_realapply中的非專案加班時數)
        /// </summary>
        [Column("day19", OnlySelect = true)]
        public Nullable<decimal> Day19
        {
            get { return day19; }
            set { day19 = value; }
        }
        #endregion
        #region  20日非專案加班時數(otm_realapply中的非專案加班時數)
        /// <summary>
        ///20日非專案加班時數(otm_realapply中的非專案加班時數)
        /// </summary>
        [Column("day20", OnlySelect = true)]
        public Nullable<decimal> Day20
        {
            get { return day20; }
            set { day20 = value; }
        }
        #endregion
        #region  21日非專案加班時數(otm_realapply中的非專案加班時數)
        /// <summary>
        ///21日非專案加班時數(otm_realapply中的非專案加班時數)
        /// </summary>
        [Column("day21", OnlySelect = true)]
        public Nullable<decimal> Day21
        {
            get { return day21; }
            set { day21 = value; }
        }
        #endregion
        #region  22日非專案加班時數(otm_realapply中的非專案加班時數)
        /// <summary>
        ///22日非專案加班時數(otm_realapply中的非專案加班時數)
        /// </summary>
        [Column("day22", OnlySelect = true)]
        public Nullable<decimal> Day22
        {
            get { return day22; }
            set { day22 = value; }
        }
        #endregion
        #region  23日非專案加班時數(otm_realapply中的非專案加班時數)
        /// <summary>
        ///23日非專案加班時數(otm_realapply中的非專案加班時數)
        /// </summary>
        [Column("day23", OnlySelect = true)]
        public Nullable<decimal> Day23
        {
            get { return day23; }
            set { day23 = value; }
        }
        #endregion
        #region  24日非專案加班時數(otm_realapply中的非專案加班時數)
        /// <summary>
        ///24日非專案加班時數(otm_realapply中的非專案加班時數)
        /// </summary>
        [Column("day24", OnlySelect = true)]
        public Nullable<decimal> Day24
        {
            get { return day24; }
            set { day24 = value; }
        }
        #endregion
        #region  25日非專案加班時數(otm_realapply中的非專案加班時數)
        /// <summary>
        ///25日非專案加班時數(otm_realapply中的非專案加班時數)
        /// </summary>
        [Column("day25", OnlySelect = true)]
        public Nullable<decimal> Day25
        {
            get { return day25; }
            set { day25 = value; }
        }
        #endregion
        #region  26日非專案加班時數(otm_realapply中的非專案加班時數)
        /// <summary>
        ///26日非專案加班時數(otm_realapply中的非專案加班時數)
        /// </summary>
        [Column("day26", OnlySelect = true)]
        public Nullable<decimal> Day26
        {
            get { return day26; }
            set { day26 = value; }
        }
        #endregion
        #region  27日非專案加班時數(otm_realapply中的非專案加班時數)
        /// <summary>
        ///27日非專案加班時數(otm_realapply中的非專案加班時數)
        /// </summary>
        [Column("day27", OnlySelect = true)]
        public Nullable<decimal> Day27
        {
            get { return day27; }
            set { day27 = value; }
        }
        #endregion
        #region  28日非專案加班時數(otm_realapply中的非專案加班時數)
        /// <summary>
        ///28日非專案加班時數(otm_realapply中的非專案加班時數)
        /// </summary>
        [Column("day28", OnlySelect = true)]
        public Nullable<decimal> Day28
        {
            get { return day28; }
            set { day28 = value; }
        }
        #endregion
        #region  29日非專案加班時數(otm_realapply中的非專案加班時數)
        /// <summary>
        ///29日非專案加班時數(otm_realapply中的非專案加班時數)
        /// </summary>
        [Column("day29", OnlySelect = true)]
        public Nullable<decimal> Day29
        {
            get { return day29; }
            set { day29 = value; }
        }
        #endregion
        #region  30日非專案加班時數(otm_realapply中的非專案加班時數)
        /// <summary>
        ///30日非專案加班時數(otm_realapply中的非專案加班時數)
        /// </summary>
        [Column("day30", OnlySelect = true)]
        public Nullable<decimal> Day30
        {
            get { return day30; }
            set { day30 = value; }
        }
        #endregion
        #region  31日非專案加班時數(otm_realapply中的非專案加班時數)
        /// <summary>
        ///1日非專案加班時數(otm_realapply中的非專案加班時數)
        /// </summary>
        [Column("day31", OnlySelect = true)]
        public Nullable<decimal> Day31
        {
            get { return day31; }
            set { day31 = value; }
        }
        #endregion

        #region 簽核人
        /// <summary>
        /// 當前簽核人的上一步簽核人
        /// </summary>
        [Column("approver")]
        public string Approver
        {
            get { return approver; }
            set { approver = value; }
        }
        #endregion
        #region 簽核日期
        /// <summary>
        /// 簽核日期
        /// </summary>
        [Column("approveDate")]
        public Nullable<DateTime> ApproveDate
        {
            get { return approveDate; }
            set { approveDate = value; }
        }
        #endregion
        #region 簽核意見
        /// <summary>
        /// 簽核意見
        /// </summary>
        [Column("apRemark")]
        public string ApRemark
        {
            get { return apRemark; }
            set { apRemark = value; }
        }
        #endregion


        #region 簽核單號
        /// <summary>
        /// 簽核單號
        /// </summary>
        [Column("billNo")]
        public string BillNo
        {
            get { return billNo; }
            set { billNo = value; }
        }
        #endregion

        #region 簽核狀態名稱
        /// <summary>
        /// 簽核狀態名稱
        /// </summary>
        [Column("approveFlagName", OnlySelect = true)]
        public string ApproveFlagName
        {
            get { return approveFlagName; }
            set { approveFlagName = value; }
        }
        #endregion

        #region 簽核狀態
        /// <summary>
        /// 簽核狀態
        /// </summary>
        [Column("approveFlag")]
        public string ApproveFlag
        {
            get { return approveFlag; }
            set { approveFlag= value; }
        }
        #endregion



        #region 事業處
        /// <summary>
        /// 事業處
        /// </summary>
        [Column("buName", OnlySelect = true)]
        public string BuName
        {
            get { return buName; }
            set { buName = value; }
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

        #region 人员类别（本土、支援）
        /// <summary>
        /// 人员类别（本土、支援）
        /// </summary>
        [Column("flag", OnlySelect = true)]
        public string Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        #endregion
    }
}
