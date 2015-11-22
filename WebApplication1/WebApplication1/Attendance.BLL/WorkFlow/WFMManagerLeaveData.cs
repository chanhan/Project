using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.IDAL.WorkFlow;
using System.Data;

namespace GDSBG.MiABU.Attendance.BLL.WorkFlow
{
    public class WFMManagerLeaveData : BLLBase<IWFMManagerLeaveData>
    {   
       /// <summary>
       /// 根據查詢條件查出
       /// </summary>
       /// <param name="condition"></param>
       /// <param name="pageIndex"></param>
       /// <param name="pageSize"></param>
       /// <param name="totalCount"></param>
       /// <returns></returns>
        public DataTable GetDataByCondition(string condition, int pageIndex, int pageSize, out int totalCount) 
        {
            return DAL.GetDataByCondition(condition, pageIndex, pageSize,out totalCount);
        }
         /// <summary>
        /// 刪除數據
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteData(string id)
        {
            return DAL.DeleteData(id);
        }
        /// <summary>
        /// 查詢代理原因
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DataTable GetDeputType(string condition)
        {
            return DAL.GetDeputType(condition);
        }
         /// <summary>
        /// 根據查詢條件查詢
        /// </summary>
        /// <param name="condition">id</param>
        /// <returns></returns>
        public DataTable GetDataByCondition(string condition)
        {
            return DAL.GetDataByCondition(condition);
        }
         /// <summary>
        /// 判斷名字是否存在
        /// </summary>
        /// <param name="emp_name"></param>
        /// <returns></returns>
        public bool CheckEmpName(string emp_name)
        {
            return DAL.CheckEmpName(emp_name);
        }
        /// <summary>
        /// 根據工號獲取基本信息
        /// </summary>
        /// <param name="emp_no"></param>
        /// <returns></returns>
        public DataTable GetEmpInfo(string emp_no)
        {
            return DAL.GetEmpInfo(emp_no);
        }
        /// <summary>
        /// 獲取本年度歷史代理記錄
        /// </summary>
        /// <returns></returns>
        public DataTable GetDeputyRecord(string emp_no)
        {
            return DAL.GetDeputyRecord(emp_no);
        }
        /// <summary>
        /// 保存代理信息
        /// </summary>
        /// <param name="prcessFlag">add新增 modify 修改</param>
        /// <param name="dt"></param>
        /// <param name="user">修改人</param>
        /// <returns></returns>
        public bool SaveData(string prcessFlag, DataTable dt, string user)
        {
            return DAL.SaveData(prcessFlag, dt, user);
        }
    }
}
