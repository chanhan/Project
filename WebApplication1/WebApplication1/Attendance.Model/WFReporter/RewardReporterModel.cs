using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;

namespace GDSBG.MiABU.Attendance.Model.WFReporter
{
    [Serializable, TableName("gds_att_reward", SelectTable = "gds_att_reward_v")]
    public class RewardReporterModel : ModelBase
    {
        private string id;
        private string workno;
        private Nullable<double> reward;
        private string rewarddesc;
        private string status;
        private string billno;
        private string approver;
        private Nullable<DateTime> approvedate;
        private string dissignrmark;
        private string update_user;
        private Nullable<DateTime> update_date;
        private string uploadfile;
        private string localname;
        private string dcode;
        private string parentdepcode;
    }
}
