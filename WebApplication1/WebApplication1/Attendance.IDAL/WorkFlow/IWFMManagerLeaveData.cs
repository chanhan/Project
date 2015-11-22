using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;
using System.Data;

namespace GDSBG.MiABU.Attendance.IDAL.WorkFlow
{
    [RefClass("WorkFlow.WFMManagerLeaveData")]
    public interface IWFMManagerLeaveData
    {    
        /// <summary>
        /// 根據條件查詢
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        DataTable GetDataByCondition(string condition, int pageIndex, int pageSize, out int totalCount);
        /// <summary>
        /// 刪除數據
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
         bool DeleteData(string id);
        /// <summary>
        /// 查詢代理原因
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
         DataTable GetDeputType(string condition);
        /// <summary>
        /// 根據查詢條件查詢（id）
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
         DataTable GetDataByCondition(string condition);
         /// <summary>
        /// 判斷名字是否存在
        /// </summary>
        /// <param name="emp_name"></param>
        /// <returns></returns>
         bool CheckEmpName(string emp_name);
         /// <summary>
         /// 根據工號獲取基本信息
         /// </summary>
         /// <param name="emp_no"></param>
         /// <returns></returns>
         DataTable GetEmpInfo(string emp_no);
        /// <summary>
        /// 獲取本年度歷史代理記錄
        /// </summary>
        /// <returns></returns>
         DataTable GetDeputyRecord(string emp_no);
         /// <summary>
         /// 保存代理信息
         /// </summary>
         /// <param name="prcessFlag">add新增 modify 修改</param>
         /// <param name="dt"></param>
         /// <param name="user">修改人</param>
         /// <returns></returns>
         bool SaveData(string prcessFlag, DataTable dt, string user);
    }
}
