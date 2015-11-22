using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.WorkFlow
{
    public class WorkFlowSignModel:ModelBase
    {
        private string deptcode;
        [Column("Deptcode")]
        public string Deptcode
        {
            get { return deptcode; }
            set { deptcode = value; }
        }
        private string formtype;
        [Column("Formtype")]
        public string Formtype
        {
            get { return formtype; }
            set { formtype = value; }
        }
        private string reason1;
        [Column("Reason1")]
        public string Reason1
        {
            get { return reason1; }
            set { reason1 = value; }
        }
        private string reason2;
        [Column("Reason2")]
        public string Reason2
        {
            get { return reason2; }
            set { reason2 = value; }
        }
        private string reason3;
        [Column("Reason3")]
        public string Reason3
        {
            get { return reason3; }
            set { reason3 = value; }
        }
        private string reason4;
        [Column("Reason4")]
        public string Reason4
        {
            get { return reason4; }
            set { reason4 = value; }
        }
      
        private string flow_empno;
        [Column("Flow_empno")]
        public string Flow_empno
        {
            get { return flow_empno; }
            set { flow_empno = value; }
        }
        private string flow_empname;
        [Column("Flow_empname")]
        public string Flow_empname
        {
            get { return flow_empname; }
            set { flow_empname = value; }
        }
        private string flow_notes;
        [Column("Flow_notes")]
        public string Flow_notes
        {
            get { return flow_notes; }
            set { flow_notes = value; }
        }
        private string flow_manager;
        [Column("Flow_manager")]
        public string Flow_manager
        {
            get { return flow_manager; }
            set { flow_manager = value; }
        }
        private string flow_type;
        [Column("Flow_type")]
        public string Flow_type
        {
            get { return flow_type; }
            set { flow_type = value; }
        }
        private string flow_level;
        [Column("Flow_level")]
        public string Flow_level
        {
            get { return flow_level; }
            set { flow_level = value; }
        }
        private int orderid;
        [Column("Orderid")]
        public int Orderid
        {
            get { return orderid; }
            set { orderid = value; }
        }
    }
}
