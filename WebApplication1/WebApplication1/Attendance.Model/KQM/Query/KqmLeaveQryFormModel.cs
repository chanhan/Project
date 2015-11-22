/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KqmLeaveQryFormModel.cs
 * 檔功能描述：請假類別定義實體類
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.12.29
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;


namespace GDSBG.MiABU.Attendance.Model.KQM.Query
{
    /// <summary>
    /// 請假類別定義實體類
    /// </summary>
    [Serializable, TableName("gds_att_leaveapply", SelectTable = "gds_sc_leavequerydata_v")]
    public class KqmLeaveQryFormModel : ModelBase
    {
        private string id;
        private string workNo;
        private Nullable<decimal> lvTotal;
        private string lvTypeCode;
        private string proxy;
        private string reason;
        private string approver;
        private string sTime;
        private string eTime;
        private string startDate;
        private string endDate;
        private string leaveType;
        private string depCode;
        private string localName;
        private string depName;
        private string statusName;
        private string statusCode;
        private Nullable<decimal> thisLVTotal;
        private Nullable<decimal> lvTotalDays;

        #region 序號
        /// <summary>
        /// 序號
        /// </summary>
        [Column("id", IsPrimaryKey = true)]
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
        [Column("workNo")]
        public string WorkNo
        {
            get { return workNo; }
            set { workNo = value; }
        }
        #endregion

        #region 請假總時長
        /// <summary>
        /// 請假總時長
        /// </summary>
        [Column("LVTOTAL")]
        public Nullable<decimal> LVTotal
        {
            get { return lvTotal; }
            set { lvTotal = value; }
        }
        #endregion

        #region 請假類別代碼
        /// <summary>
        /// 請假類別代碼
        /// </summary>
        [Column("lvTypeCode")]
        public string LVTypeCode
        {
            get { return lvTypeCode; }
            set { lvTypeCode = value; }
        }
        #endregion

        #region 代理人
        /// <summary>
        /// 代理人
        /// </summary>
        [Column("proxy")]
        public string Proxy
        {
            get { return proxy; }
            set { proxy = value; }
        }
        #endregion

        #region 請假原因
        /// <summary>
        /// 請假原因
        /// </summary>
        [Column("reason")]
        public string Reason
        {
            get { return reason; }
            set { reason = value; }
        }
        #endregion

        #region 審核人
        /// <summary>
        /// 審核人
        /// </summary>
        [Column("approver")]
        public string Approver
        {
            get { return approver; }
            set { approver = value; }
        }
        #endregion

        #region  請假開始時間（年月日時分秒）
        /// <summary>
        /// 請假開始時間（年月日時分秒）
        /// </summary>
        [Column("sTime", OnlySelect = true)]
        public string STime
        {
            get { return sTime; }
            set { sTime = value; }
        }
        #endregion

        #region  請假結束時間（年月日時分秒）
        /// <summary>
        /// 請假結束時間（年月日時分秒）
        /// </summary>
        [Column("eTime", OnlySelect = true)]
        public string ETime
        {
            get { return eTime; }
            set { eTime = value; }
        }
        #endregion

        #region  請假開始日期
        /// <summary>
        /// 請假開始日期
        /// </summary>
        [Column("startDate")]
        public string StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        #endregion

        #region  請假結束日期
        /// <summary>
        ///請假結束日期
        /// </summary>
        [Column("endDate")]
        public string EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }
        #endregion

        #region 請假類別名稱
        /// <summary>
        /// 請假類別名稱
        /// </summary>
        [Column("leaveType", OnlySelect = true)]
        public string LeaveType
        {
            get { return leaveType; }
            set { leaveType = value; }
        }
        #endregion

        #region 部門代碼
        /// <summary>
        /// 部門代碼
        /// </summary>
        [Column("depCode", OnlySelect = true)]
        public string DepCode
        {
            get { return depCode; }
            set { depCode = value; }
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

        #region 簽核狀態名稱
        /// <summary>
        /// 簽核狀態名稱
        /// </summary>
        [Column("statusName", OnlySelect = true)]
        public string StatusName
        {
            get { return statusName; }
            set { statusName = value; }
        }
        #endregion

        #region 簽核狀態代碼
        /// <summary>
        /// 簽核狀態代碼
        /// </summary>
        [Column("statusCode", OnlySelect = true)]
        public string StatusCode
        {
            get { return statusCode; }
            set { statusCode = value; }
        }
        #endregion

        #region 查詢時段休假時數
        /// <summary>
        /// 查詢時段休假時數
        /// </summary>
        [Column("thisLVTotal", OnlySelect = true)]
        public Nullable<decimal> ThisLVTotal
        {
            get { return thisLVTotal; }
            set { thisLVTotal = value; }
        }
        #endregion

        #region 天數
        /// <summary>
        /// 天數
        /// </summary>
        [Column("lvtotaldays", OnlySelect = true)]
        public Nullable<decimal> LVTotalDays
        {
            get { return lvTotalDays; }
            set { lvTotalDays = value; }
        }
        #endregion
    }
}
