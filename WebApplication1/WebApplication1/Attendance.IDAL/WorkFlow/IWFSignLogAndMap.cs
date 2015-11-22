using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;
using System.Data;

namespace GDSBG.MiABU.Attendance.IDAL.WorkFlow
{
    [RefClass("WorkFlow.WFSignLogAndMap")]
    public interface IWFSignLogAndMap
    {
        /// <summary>
        /// 獲取簽核流程圖
        /// </summary>
        /// <param name="doc">單號</param>
        /// <returns></returns>
        DataTable GetFlowMap(string doc);
        /// <summary>
        /// 獲取簽核Log
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        DataTable GetFlowLog(string doc);
    }
}
