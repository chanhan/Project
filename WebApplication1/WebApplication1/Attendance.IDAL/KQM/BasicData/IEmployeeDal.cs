/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IEmployeeDal.cs
 * 檔功能描述： 員工基本資料操作數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.13
 * 
 */

using System.Collections.Generic;
using System.Data;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;

namespace GDSBG.MiABU.Attendance.IDAL.KQM.BasicData
{
    /// <summary>
    /// 員工基本資料操作數據操作接口
    /// </summary>
    [RefClass("KQM.BasicData.EmployeeDal")]
    public interface IEmployeeDal
    {
        /// <summary>
        /// 根據主鍵查詢model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        EmployeeModel GetEmpByKey(EmployeeModel model);

        /// <summary>
        /// 獲得在職員工
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        DataTable GetEmp(string workno);

        /// <summary>
        /// 獲得在用戶有操作權限的員工
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        DataTable GetEmp(string workno, string personcode, string modulecode);

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        List<EmployeeModel> GetList(DataTable dt);
    }
}
