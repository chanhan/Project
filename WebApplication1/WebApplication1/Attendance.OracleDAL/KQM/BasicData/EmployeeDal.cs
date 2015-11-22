/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： EmployeeDal.cs
 * 檔功能描述： 員工基本資料數據操作類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.13
 * 
 */

using System;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using System.Collections;
using System.Collections.Generic;

namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.BasicData
{
    public class EmployeeDal : DALBase<EmployeeModel>, IEmployeeDal
    {
        /// <summary>
        /// 根據主鍵查詢model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public EmployeeModel GetEmpByKey(EmployeeModel model)
        {
            return DalHelper.SelectByKey(model);
        }

        /// <summary>
        /// 獲得在職員工
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        public DataTable GetEmp(string workno)
        {
            string str = "select workno from gds_att_employee where workno=:workno and Status<'2'";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":workno", workno));
        }

        /// <summary>
        /// 獲得在用戶有操作權限的員工
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        public DataTable GetEmp(string workno, string personcode, string modulecode)
        {
            string str = "select workno from gds_att_employee where workno=:workno and Status<'2'";
            //string condition = " and depcode in (" + sql + ")";
            string condition = "and depcode in (SELECT aa.depcode FROM gds_sc_persondept aa WHERE aa.personcode =:personcode AND aa.modulecode =:modulecode AND EXISTS (SELECT 1 FROM gds_sc_personcompany bb WHERE  bb.personcode=:personcode AND aa.companyid = bb.companyid))";
            str += condition;
            return DalHelper.ExecuteQuery(str, new OracleParameter(":workno", workno), new OracleParameter(":personcode", personcode), new OracleParameter(":modulecode", modulecode));
        }

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<EmployeeModel> GetList(DataTable dt)
        {
            return OrmHelper.SetDataTableToList(dt);
        }
    }
}
