using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.KQM.KaoQinData
{
    [Serializable, TableName("gds_att_leaveapply_temp")]
    public class LeaveApplyTempModel : ModelBase
    {
     

       private string workno;

        #region 
        /// <summary>
        /// 
        /// </summary>
        [Column("WORKNO")]
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
        public string LvTypeCode
        {
            get { return lvtypecode; }
            set { lvtypecode = value; }
        }
        #endregion

        string startdate;
        #region 
        /// <summary>
        /// 
        /// </summary>
        [Column("startdate")]
        public string StartDate
        {
            get { return startdate; }
            set { startdate = value; }
        }
        #endregion
        string starttime;

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
        string enddate;

        #region 
        /// <summary>
        /// 
        /// </summary>
        [Column("enddate")]
        public string EndDate
        {
            get { return enddate; }
            set { enddate = value; }
        }
        #endregion
        string endtime;

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
        string lvtotal;

        #region 
        /// <summary>
        /// 
        /// </summary>
        [Column("lvtotal")]
        public string LvTotal
        {
            get { return lvtotal; }
            set { lvtotal = value; }
        }
        #endregion
        string reason;

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
        string proxy;

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
        string remark;

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
        string applytype;

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
        //string create_date  date,
        //string create_user;
        string errormsg;

        #region 
        /// <summary>
        /// 
        /// </summary>
        [Column("errormsg")]
        public string ErrorMsg
        {
            get { return errormsg; }
            set { errormsg = value; }
        }
        #endregion
    }
}
