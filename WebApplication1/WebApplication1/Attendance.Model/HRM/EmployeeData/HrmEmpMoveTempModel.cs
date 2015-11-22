using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.HRM.EmployeeData
{
    [Serializable, TableName("gds_att_empmove_temp")]
    public class HrmEmpMoveTempModel : ModelBase
    {
        private string workno;

        [Column("workno")]
        public string Workno
        {
            get { return workno; }
            set { workno = value; }
        }

        private string localname;

        [Column("localname")]
        public string Localname
        {
            get { return localname; }
            set { localname = value; }
        }

        private string aftervaluename;

        [Column("aftervaluename")]
        public string Aftervaluename
        {
            get { return aftervaluename; }
            set { aftervaluename = value; }
        }

        private string effectdate;

        [Column("effectdate")]
        public string Effectdate
        {
            get { return effectdate; }
            set { effectdate = value; }
        }

        private string movereason;

        [Column("movereason")]
        public string Movereason
        {
            get { return movereason; }
            set { movereason = value; }
        }

        private string remark;

        [Column("remark")]
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        private string errormsg;

        [Column("errormsg")]
        public string Errormsg
        {
            get { return errormsg; }
            set { errormsg = value; }
        }
    }
}
