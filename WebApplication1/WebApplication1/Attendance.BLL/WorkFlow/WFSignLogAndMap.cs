using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.IDAL.WorkFlow;
using System.Data;
namespace GDSBG.MiABU.Attendance.BLL.WorkFlow
{
    public class WFSignLogAndMap : BLLBase<IWFSignLogAndMap>
    {
          /// <summary>
        /// 獲取簽核流程圖
        /// </summary>
        /// <param name="doc">單號</param>
        /// <returns></returns>
        public DataTable GetFlowMap(string doc)
        {
            return DAL.GetFlowMap(doc);
        }
        /// <summary>
        /// 獲取簽核Log
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public DataTable GetFlowLog(string doc)
        {
            return DAL.GetFlowLog(doc);
        }
    }
}
