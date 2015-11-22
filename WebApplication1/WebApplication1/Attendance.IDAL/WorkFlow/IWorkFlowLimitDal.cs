using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;
using System.Data;
using GDSBG.MiABU.Attendance.Model.WorkFlow;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.IDAL.WorkFlow
{
    [RefClass("WorkFlow.WorkFlowLimitDal")]
    public interface IWorkFlowLimitDal
    {
        /// <summary>
        /// 獲取必簽人員信息列表
        /// </summary>
        /// <param name="deptcode"></param>
        /// <param name="formtype"></param>
        /// <param name="resonlist"></param>
        /// <returns></returns>
        DataTable GetSignLimitInfo(string deptcode, string formtype, List<string> resonlist);

        bool SaveflowlimitInfo(string deptid, string Formtype, List<string> resonlist, Dictionary<int, List<string>> driy, SynclogModel logmodel);

    }
}
