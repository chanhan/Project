using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.IDAL.WorkFlow;
using System.Data;
using GDSBG.MiABU.Attendance.Model.WorkFlow;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.BLL.WorkFlow
{
    public class WorkFlowLimitBll:BLLBase<IWorkFlowLimitDal>
    {
        /// <summary>
        ///  獲取必簽人員信息列表
        /// </summary>
        /// <param name="deptcode"></param>
        /// <param name="formtype"></param>
        /// <param name="resonlist"></param>
        /// <returns></returns>

        public DataTable GetSignLimitInfo(string deptcode, string formtype, List<string> resonlist)
        {
            return DAL.GetSignLimitInfo(deptcode, formtype, resonlist);
        }

        /// <summary>
        /// 流程設定保存
        /// </summary>
        /// <param name="deptid"></param>
        /// <param name="Formtype"></param>
        /// <param name="resonlist"></param>
        /// <param name="driy"></param>
        /// <returns></returns>
        public bool SaveflowlimitInfo(string deptid, string Formtype, List<string> resonlist, Dictionary<int, List<string>> driy, SynclogModel logmodel)
        {
            return DAL.SaveflowlimitInfo(deptid, Formtype, resonlist, driy, logmodel);
        }
    }
}
