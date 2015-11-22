/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： LeaveApplyViewModel.cs
 * 檔功能描述： 請假申請實體類
 * 
 * 版本：1.0
 * 創建標識：陳函  2012.03.10
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;
using System.Data;

namespace GDSBG.MiABU.Attendance.Model.KQM.KaoQinData
{
    [Serializable, TableName("gds_att_leaveapply", SelectTable = "gds_att_leaveapply_v")]
    public class LeaveApplyViewModel : ModelBase
    {
        private string id;

        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("id")]
        public string ID
        {
            get { return id; }
            set { id = value; }
        }
        #endregion

        private string workno;
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("workno")]
        public string WorkNo
        {
            get { return workno; }
            set { workno = value; }
        }
        #endregion

        private string lvtypecode;
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("lvtypecode")]
        public string LvtypeCode
        {
            get { return lvtypecode; }
            set { lvtypecode = value; }
        }
        #endregion
        private Nullable<DateTime> startdate;

        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("startdate")]
        public Nullable<DateTime> StartDate
        {
            get { return startdate; }
            set { startdate = value; }
        }
        #endregion

        private string starttime;

        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("starttime")]
        public string StartTime
        {
            get { return starttime; }
            set { starttime = value; }
        }
        #endregion

        private Nullable<DateTime> enddate;

        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("enddate")]
        public Nullable<DateTime> EndDate
        {
            get { return enddate; }
            set { enddate = value; }
        }
        #endregion

        private string endtime;

        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("endtime")]
        public string EndTime
        {
            get { return endtime; }
            set { endtime = value; }
        }
        #endregion

        private Nullable<double> lvtotal;
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("lvtotal")]
        public Nullable<double> LVTotal
        {
            get { return lvtotal; }
            set { lvtotal = value; }
        }
        #endregion

        private string reason;
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("reason")]
        public string Reason
        {
            get { return reason; }
            set { reason = value; }
        }
        #endregion

        private string proxy;
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("proxy")]
        public string Proxy
        {
            get { return proxy; }
            set { proxy = value; }
        }
        #endregion
        private string remark;
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("remark")]
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        #endregion

        private string applytype;
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("applytype")]
        public string ApplyType
        {
            get { return applytype; }
            set { applytype = value; }
        }
        #endregion

        private string status;
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("status")]
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        #endregion

        private string billno;
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("billno")]
        public string BillNo
        {
            get { return billno; }
            set { billno = value; }
        }
        #endregion

        private string approver;
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("approver")]
        public string Approver
        {
            get { return approver; }
            set { approver = value; }
        }
        #endregion

        private Nullable<DateTime> approvedate;

        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("approvedate")]
        public Nullable<DateTime> ApproveDate
        {
            get { return approvedate; }
            set { approvedate = value; }
        }
        #endregion

        private string update_user;
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("update_user")]
        public string Update_User
        {
            get { return update_user; }
            set { update_user = value; }
        }
        #endregion
        private string update_datestr;
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("update_datestr")]
        public string Update_DateStr
        {
            get { return update_datestr; }
            set { update_datestr = value; }
        }
        #endregion

        private Nullable<DateTime> update_date;
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("update_date")]
        public Nullable<DateTime> Update_Date
        {
            get { return update_date; }
            set { update_date = value; }
        }
        #endregion

        private string apremark;
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("apremark")]
        public string Apremark
        {
            get { return apremark; }
            set { apremark = value; }
        }
        #endregion

        private string islastyear;
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("islastyear")]
        public string IsLastYear
        {
            get { return islastyear; }
            set { islastyear = value; }
        }
        #endregion
        private string proxyworkno;
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("proxyworkno")]
        public string ProxyWorkno
        {
            get { return proxyworkno; }
            set { proxyworkno = value; }
        }
        #endregion
        private string proxystatus;

        private string wifeisfoxconn;
        private string wifebg;
        private string wifeworkno;
        private string wifename;
        private string wifelevelname;
        private string wifelivepay;
        private string testifyfile;
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("testifyfile")]
        public string TestifyFile
        {
            get { return testifyfile; }
            set { testifyfile = value; }
        }
        #endregion
        private string startshiftno;
        private string endshiftno;
        private string uploadfile;
        /// <summary>
        /// 
        /// </summary>
        [Column("uploadfile")]
        public string UpLoadFile
        {
            get { return uploadfile; }
            set { uploadfile = value; }
        }


        private string localname;
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("LocalName")]
        public string LocalName
        {
            get { return localname; }
            set { localname = value; }
        }
        #endregion
        private string dcode;
        private string levelname;

        private string sexname;
        [Column("sexname")]
        public string SexName
        {
            get { return sexname; }
            set { sexname = value; }
        }
        private string sex;

        private string depname;
        [Column("depname")]
        public string DepName
        {
            get { return depname; }
            set { depname = value; }
        }
        private string levelcode;
        private string managercode;

        private string buname;
        [Column("buname")]
        public string BuName
        {
            get { return buname; }
            set { buname = value; }
        }

        private string lvtypename;
        [Column("lvtypename")]
        public string LVTypeName
        {
            get { return lvtypename; }
            set { lvtypename = value; }
        }
        private string applytypename;

        [Column("applytypename")]
        public string ApplyTypeName
        {
            get { return applytypename; }
            set { applytypename = value; }
        }
        private string statusname;
        [Column("statusname")]
        public string StatusName
        {
            get { return statusname; }
            set { statusname = value; }
        }
        private string proxynotes;
        private string proxyflag;
        private string modifyname;
        private string proxystatusname;
        private string istestify;
        private string comeyears;



        private string thislvtotal;
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("thislvtotal")]
        public string ThisLvTotal
        {
            get { return thislvtotal; }
            set { thislvtotal = value; }
        }
        #endregion

        private string lvtotaldays;
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("lvtotaldays")]
        public string LvTotalDays
        {
            get { return lvtotaldays; }
            set { lvtotaldays = value; }
        }
        #endregion

        private string lvworkdays;
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("lvworkdays")]
        public string LvWorkDays
        {
            get { return lvworkdays; }
            set { lvworkdays = value; }
        }
        #endregion



        private string emergencycontactperson;
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("EMERGENCYCONTACTPERSON")]
        public string EmergencyContactPerson
        {
            get { return emergencycontactperson; }
            set { emergencycontactperson = value; }
        }
        #endregion


        private string emergencytelephone;
        #region
        /// <summary>
        /// 
        /// </summary>
        [Column("EMERGENCYTELEPHONE")]
        public string EmergencyTelephone
        {
            get { return emergencytelephone; }
            set { emergencytelephone = value; }
        }
        #endregion

        //#region
        ///// <summary>
        ///// 
        ///// </summary>
        //[Column("WORKSTIME")]
        //public string WorkSTime
        //{
        //    get { return workSTime; }
        //    set { workSTime = value; }
        //}
        //#endregion
        //#region
        ///// <summary>
        ///// 
        ///// </summary>
        //[Column("WORKSTIME")]
        //public string WorkSTime
        //{
        //    get { return workSTime; }
        //    set { workSTime = value; }
        //}
        //#endregion
        //#region
        ///// <summary>
        ///// 
        ///// </summary>
        //[Column("WORKSTIME")]
        //public string WorkSTime
        //{
        //    get { return workSTime; }
        //    set { workSTime = value; }
        //}
        //#endregion
        //#region
        ///// <summary>
        ///// 
        ///// </summary>
        //[Column("WORKSTIME")]
        //public string WorkSTime
        //{
        //    get { return workSTime; }
        //    set { workSTime = value; }
        //}
        //#endregion
        //#region
        ///// <summary>
        ///// 
        ///// </summary>
        //[Column("WORKSTIME")]
        //public string WorkSTime
        //{
        //    get { return workSTime; }
        //    set { workSTime = value; }
        //}
        //#endregion
        //#region
        ///// <summary>
        ///// 
        ///// </summary>
        //[Column("WORKSTIME")]
        //public string WorkSTime
        //{
        //    get { return workSTime; }
        //    set { workSTime = value; }
        //}
        //#endregion
        //#region
        ///// <summary>
        ///// 
        ///// </summary>
        //[Column("WORKSTIME")]
        //public string WorkSTime
        //{
        //    get { return workSTime; }
        //    set { workSTime = value; }
        //}
        //#endregion

        //#region
        ///// <summary>
        ///// 
        ///// </summary>
        //[Column("WORKSTIME")]
        //public string WorkSTime
        //{
        //    get { return workSTime; }
        //    set { workSTime = value; }
        //}
        //#endregion
    }
}
