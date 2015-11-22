/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： NightAllowanceToExcelDal.cs
 * 檔功能描述： 夜宵補助數據操作類
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2012.01.09
 * 
 */

using System;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Collections;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.IDAL.KQM.Query;
using GDSBG.MiABU.Attendance.Model.KQM.Query;
using GDSBG.MiABU.Attendance.Model.KQM;


namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.Query
{
    public class NightAllowanceToExcelDal : DALBase<NightAllowanceToExcelModel>, INightAllowanceToExcelDal
    {
        /// <summary>
        /// 查詢導出數據
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dateFrom">開始日期</param>
        /// <param name="dateTo">結束日期</param>
        /// <param name="deptCode">部門代碼</param>
        /// <returns></returns>
        public DataTable GetNightExcel(NightAllowanceToExcelModel model, string dateFrom, string dateTo, string deptCode, string status, string SqlDep)
        {
            string condition = "";
            string condition2 = "";
            if (!string.IsNullOrEmpty(dateFrom))
            {
                condition=condition+" AND KQDate >=to_date('" + dateFrom + "','yyyy/mm/dd')";
            }
            if (!string.IsNullOrEmpty(dateTo))
            {
                condition=condition+" AND KQDate <=to_date('" + dateTo + "','yyyy/mm/dd')";
            }
            if (!string.IsNullOrEmpty(model.WorkNo))
            {
                condition=condition+" AND WorkNo LIKE '" + model.WorkNo + "%'";
            }
            if (!string.IsNullOrEmpty(deptCode))
            {
                condition2=condition2+" AND b.dCode IN ((" + SqlDep + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depcode = '" + deptCode + "' CONNECT BY PRIOR depcode = parentdepcode) ";

            }
            else
            {
                condition2=condition2+" AND b.dcode in (" + SqlDep + ")";
                //condition2 = "";
            }
            if (!string.IsNullOrEmpty(model.LocalName))
            {
               condition2=condition2+" AND b.LocalName LIKE '" +model.LocalName+ "%'";
            }
            string cmdText = @"SELECT ROWNUM num, depcode, dname, a.workno, localname, allowance FROM (SELECT   workno, SUM (1) allowance
                             FROM gds_att_kaoqindata WHERE (exceptiontype IS NULL OR exceptiontype IN ('A', 'B', 'U', 'I', 'E', 'C', 'D', 
                             'G', 'H')) AND TO_CHAR (offdutytime, 'hh24:mi') < '12:00' AND shiftno LIKE 'C%' " + condition + " GROUP BY workno) a,  gds_att_employee_v b WHERE a.workno = b.workno(+) " + condition2 + "";
            DataTable dt = DalHelper.ExecuteQuery(cmdText);
            return dt;
        }
        /// <summary>
        /// 查詢狀態是0的情況下的數據導出
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public DataTable GetNightExcelStatus0(string dateFrom, string dateTo,string  workNo)
        {
            string condition = " AND KQDate>=to_date('" + dateFrom + "','yyyy/mm/dd')  AND KQDate<=to_date('" + dateTo+"','yyyy/mm/dd')  AND WorkNo='" + workNo + "'";
            string cmdText = @"SELECT workno, kqdate  FROM gds_att_kaoqindata WHERE ( exceptiontype IS NULL OR exceptiontype IN ('A', 'B', 
                             'U', 'I', 'E', 'C', 'D', 'G', 'H') )  AND TO_CHAR (offdutytime, 'hh24:mi') < '12:00'  AND shiftno LIKE 'C%' ";
            cmdText = cmdText + condition;
            DataTable dt = DalHelper.ExecuteQuery(cmdText);
            return dt;
        }
    }
}
