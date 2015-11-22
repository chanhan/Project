/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： GdsAttEmployeesVBll.cs
 * 檔功能描述：加班類別異動功能模組業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2011.12.23
 * 
 */
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.IDAL.HRM.EmployeeData;
using GDSBG.MiABU.Attendance.Model.HRM.EmployeeData;

namespace GDSBG.MiABU.Attendance.BLL.HRM.EmployeeData
{
    public class GdsAttEmployeesVBll : BLLBase<IGdsAttEmployeesVDal>
    {
        /// <summary>
        /// 返回人員信息，用於導出Excel
        /// </summary>
        /// <param name="EmployeeNo">工號</param>
        /// <param name="Privileged">是否有特權</param>
        /// <param name="moduleCode">模組代碼</param>
        /// <param name="personCode">登陸工號</param>
        /// <param name="companyID">公司ID</param>
        /// <returns>返回人員信息</returns>
        public List<GdsAttEmployeesVModel> GetEmpList(string EmployeeNo, bool Privileged, string sqlDep)
        {
            return DAL.GetEmpList(EmployeeNo, Privileged, sqlDep);
        }
    }
}
