using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.WorkFlow
{
    public class OverTimeModel : ModelBase
    {
        private string workno;
        [Column("Workno")]
        public string Workno
        {
            get { return workno; }
            set { workno = value; }
        }
        private string ottype;
        [Column("Ottype")]
        public string Ottype
        {
            get { return ottype; }
            set { ottype = value; }
        }
        private DateTime otdate;
        [Column("Otdate")]
        public DateTime Otdate
        {
            get { return otdate; }
            set { otdate = value; }
        }
        private DateTime begintime;
        [Column("Begintime")]
        public DateTime Begintime
        {
            get { return begintime; }
            set { begintime = value; }
        }
        private DateTime endtime;
        [Column("Endtime")]
        public DateTime Endtime
        {
            get { return endtime; }
            set { endtime = value; }
        }
        private int hours;
        [Column("Hours")]
        public int Hours
        {
            get { return hours; }
            set { hours = value; }
        }
        private string workdesc;
        [Column("Workdesc")]
        public string Workdesc
        {
            get { return workdesc; }
            set { workdesc = value; }
        }
        private string remark;
        [Column("Remark")]
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        private string otmsgflag;
        [Column("Otmsgflag")]
        public string Otmsgflag
        {
            get { return otmsgflag; }
            set { otmsgflag = value; }
        }
        private string status;
        [Column("Status")]
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        private string isproject;
        [Column("Isproject")]
        public string Isproject
        {
            get { return isproject; }
            set { isproject = value; }
        }
        private string planadjust;
        [Column("Planadjust")]
        public string Planadjust
        {
            get { return planadjust; }
            set { planadjust = value; }
        }
        private string otshiftno;
        [Column("Otshiftno")]
        public string Otshiftno
        {
            get { return otshiftno; }
            set { otshiftno = value; }
        }
        private DateTime applydate;
        [Column("Applydate")]
        public DateTime Applydate
        {
            get { return applydate; }
            set { applydate = value; }
        }
       
        private string g2isforrest;
        [Column("G2isforrest")]
        public string G2isforrest
        {
            get { return g2isforrest; }
            set { g2isforrest = value; }
        }

        private string update_user;
        [Column("UPDATE_USER")]
        public string Update_user
        {
            get { return update_user; }
            set { update_user = value; }
        }

        private DateTime update_date;
        [Column("Update_date")]
        public DateTime Update_date
        {
            get { return update_date; }
            set { update_date = value; }
        }
    }
}
