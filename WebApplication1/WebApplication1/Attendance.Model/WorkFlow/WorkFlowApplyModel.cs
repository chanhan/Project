using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.WorkFlow
{
    [Serializable]
    public class WorkFlowApplyModel : ModelBase
    {
        private string emp_no;
        private string companyId;
        private string emp_name;

        public string Emp_no
        {
            get { return emp_no; }
            set { emp_no = value; }
        }

        public string CompanyId
        {
            get { return companyId; }
            set { companyId = value; }
        }

        public string Emp_name
        {
            get { return emp_name; }
            set { emp_name = value; }
        }



    }
}
