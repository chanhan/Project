/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KaoQinDataQuery.cs
 * 檔功能描述： 考勤結果查詢實體類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.27
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;
namespace GDSBG.MiABU.Attendance.Model.KQM.Query
{
    [Serializable, TableName("gds_att_kaoqindataquery_v")]
    public class KaoQinDataQueryModel : ModelBase
    {
        private string workno;
        private Nullable<DateTime> kqdate;
        private string shiftno;
        private string localname;
        private Nullable<Decimal> workhours;
        private string depname;
        private string dcode;
        private string otondutytime;
        private string otoffdutytime;
        private string shiftdesc;
        private string ondutytime;
        private string offdutytime;
        private string absentqty;
        private string status;
        private string exceptiontype;
        private string reasontype;
        private string reasonremark;
        private string reasonname;
        private string exceptiontypename;
        private string statusname;
        private string ismakeup;
        private Nullable<Decimal> othours;
        private Nullable<Decimal> totalhours;
        private string absenttotal;

        #region 工號
        /// <summary>
        /// 工號
        /// </summary>
        [Column("workno")]
        public string WorkNo
        {
            get { return workno; }
            set { workno = value; }
        }
        #endregion

        #region 考勤日
        /// <summary>
        /// 考勤日
        /// </summary>
        [Column("kqdate")]
        public Nullable<DateTime> KQDate
        {
            get { return kqdate; }
            set { kqdate = value; }
        }
        #endregion

        #region 班別代碼
        /// <summary>
        /// 班別代碼
        /// </summary>
        [Column("shiftno")]
        public string ShiftNo
        {
            get { return shiftno; }
            set { shiftno = value; }
        }
        #endregion

        #region 姓名
        /// <summary>
        /// 姓名
        /// </summary>
        [Column("localname")]
        public string LocalName
        {
            get { return localname; }
            set { localname = value; }
        }
        #endregion

        #region 工作時長
        /// <summary>
        /// 工作時長
        /// </summary>
        [Column("workhours")]
        public Nullable<Decimal> WorkHours
        {
            get { return workhours; }
            set { workhours = value; }
        }
        #endregion

        #region 單位
        /// <summary>
        /// 單位
        /// </summary>
        [Column("depname")]
        public string DepName
        {
            get { return depname; }
            set { depname = value; }
        }
        #endregion

        #region 部門CODE
        /// <summary>
        /// 部門CODE
        /// </summary>
        [Column("dcode")]
        public string DCode
        {
            get { return dcode; }
            set { dcode = value; }
        }
        #endregion

        #region 加班開始
        /// <summary>
        /// 加班開始
        /// </summary>
        [Column("otondutytime")]
        public string OTOnDutyTime
        {
            get { return otondutytime; }
            set { otondutytime = value; }
        }
        #endregion

        #region 加班結束
        /// <summary>
        /// 加班結束
        /// </summary>
        [Column("otoffdutytime")]
        public string OTOffDutyTime
        {
            get { return otoffdutytime; }
            set { otoffdutytime = value; }
        }
        #endregion

        #region 班別
        /// <summary>
        /// 班別
        /// </summary>
        [Column("shiftdesc")]
        public string ShiftDesc
        {
            get { return shiftdesc; }
            set { shiftdesc = value; }
        }
        #endregion

        #region 上班時間
        /// <summary>
        /// 上班時間
        /// </summary>
        [Column("ondutytime")]
        public string OnDutyTime
        {
            get { return ondutytime; }
            set { ondutytime = value; }
        }
        #endregion

        #region 下班時間
        /// <summary>
        /// 下班時間
        /// </summary>
        [Column("offdutytime")]
        public string OffDutyTime
        {
            get { return offdutytime; }
            set { offdutytime = value; }
        }
        #endregion

        #region 缺勤分鐘
        /// <summary>
        /// 缺勤分鐘
        /// </summary>
        [Column("absentqty")]
        public string AbsentQty
        {
            get { return absentqty; }
            set { absentqty = value; }
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
        [Column("exceptiontype")]
        public string ExceptionType
        {
            get { return exceptiontype; }
            set { exceptiontype = value; }
        }
        #endregion

        #region 異常
        /// <summary>
        /// 異常
        /// </summary>
        [Column("reasontype")]
        public string ReasonType
        {
            get { return reasontype; }
            set { reasontype = value; }
        }
        #endregion

        #region 異常原因
        /// <summary>
        /// 異常原因
        /// </summary>
        [Column("reasonremark")]
        public string ReasonRemark
        {
            get { return reasonremark; }
            set { reasonremark = value; }
        }
        #endregion

        #region 異常類別
        /// <summary>
        /// 異常類別
        /// </summary>
        [Column("reasonname")]
        public string ReasonName
        {
            get { return reasonname; }
            set { reasonname = value; }
        }
        #endregion

        #region 結果
        /// <summary>
        /// 結果
        /// </summary>
        [Column("exceptiontypename")]
        public string ExceptionTypeName
        {
            get { return exceptiontypename; }
            set { exceptiontypename = value; }
        }
        #endregion

        #region 狀態
        /// <summary>
        /// 狀態
        /// </summary>
        [Column("statusname")]
        public string StatusName
        {
            get { return statusname; }
            set { statusname = value; }
        }
        #endregion

        #region 是否補卡
        /// <summary>
        /// 是否補卡
        /// </summary>
        [Column("ismakeup")]
        public string IsMakeUp
        {
            get { return ismakeup; }
            set { ismakeup = value; }
        }
        #endregion

        #region Othours
        /// <summary>
        /// Othours
        /// </summary>
        [Column("othours")]
        public Nullable<Decimal> Othours
        {
            get { return othours; }
            set { othours = value; }
        }
        #endregion

        #region TotalHours
        /// <summary>
        /// TotalHours
        /// </summary>
        [Column("totalhours")]
        public Nullable<Decimal> TotalHours
        {
            get { return totalhours; }
            set { totalhours = value; }
        }
        #endregion

        #region 當月曠工累計
        /// <summary>
        /// 當月曠工累計
        /// </summary>
        [Column("absenttotal")]
        public string AbsentTotal
        {
            get { return absenttotal; }
            set { absenttotal = value; }
        }
        #endregion

        
    }
}
