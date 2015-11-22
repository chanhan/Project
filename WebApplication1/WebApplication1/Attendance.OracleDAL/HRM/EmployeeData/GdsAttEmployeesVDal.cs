/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： GdsAttEmployeesVDal.cs
 * 檔功能描述：加班類別異動功能模組操作類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2011.12.23
 * 
 */
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.HRM.EmployeeData;
using GDSBG.MiABU.Attendance.Model.HRM.EmployeeData;

namespace GDSBG.MiABU.Attendance.OracleDAL.HRM.EmployeeData
{
    public class GdsAttEmployeesVDal : DALBase<GdsAttEmployeesVModel>, IGdsAttEmployeesVDal
    {
        /// <summary>
        /// 返回人員信息，用於導出Excel
        /// </summary>
        /// <param name="EmployeeNo">工號</param>
        /// <param name="Privileged">是否有組織權限</param>
        /// <param name="moduleCode">模組代碼</param>
        /// <param name="personCode">登陸工號</param>
        /// <param name="companyID">公司ID</param>
        /// <returns>返回人員信息</returns>
        public List<GdsAttEmployeesVModel> GetEmpList(string EmployeeNo, bool Privileged, string sqlDep)
        {
            DataTable dt = new DataTable();
            string condition = @"SELECT *
  FROM gds_att_employees_v a WHERE a.WorkNO=:p_EmployeeNo and a.status='0'  ";
            if (Privileged)
            {
                //if (moduleCode.Length != 0)
                //{
                //    condition += " AND a.depCode IN(SELECT depcode FROM gds_sc_persondept where personcode=:p_personcode and modulecode=:p_modulecode and companyid=:p_companyid)";

                //    dt = DalHelper.ExecuteQuery(condition, new OracleParameter(":p_EmployeeNo", EmployeeNo), new OracleParameter(":p_personcode", personCode), new OracleParameter(":p_modulecode", moduleCode), new OracleParameter(":p_companyid", companyID));
                //}
                //else
                //{
                //    condition += " AND a.depCode IN(SELECT 1 FROM dual )";

                //    dt = DalHelper.ExecuteQuery(condition, new OracleParameter(":p_EmployeeNo", EmployeeNo));
                //}
                condition += " and a.depCode IN(" + sqlDep + ")";
            }

            dt = DalHelper.ExecuteQuery(condition, new OracleParameter(":p_EmployeeNo", EmployeeNo));
            return OrmHelper.SetDataTableToList(dt);
        }
    }
}
