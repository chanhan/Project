/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： EmployeeBll.cs
 * 檔功能描述： 員工基本資料操作業務邏輯類
 * 
 * 版本：1.0
 * 創建標識：顧陳偉 2011.12.13
 * 
 */

using System.Collections.Generic;
using System.Data;
using GDSBG.MiABU.Attendance.IDAL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;

namespace GDSBG.MiABU.Attendance.BLL.KQM.BasicData
{
    public class EmployeeBll : BLLBase<IEmployeeDal>
    {
        /// <summary>
        /// 根據主鍵查詢model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public EmployeeModel GetEmpByKey(EmployeeModel model)
        {
            return DAL.GetEmpByKey(model);
        }

        /// <summary>
        /// 獲得在職員工
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        public DataTable GetEmp(string workno)
        {
            return DAL.GetEmp(workno);
        }

        /// <summary>
        /// 獲得在用戶有操作權限的員工
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        public DataTable GetEmp(string workno, string personcode, string modulecode)
        {
            return DAL.GetEmp(workno, personcode,modulecode);
        }

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<EmployeeModel> GetList(DataTable dt)
        {
            return DAL.GetList(dt);
        }
    }
}
