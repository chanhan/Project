/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OTReporterDal.cs
 * 檔功能描述： 加班統計表數據操作類
 * 
 * 版本：1.0
 * 創建標識： 張明強 2012.3.03
 * 
 * 
 */

using System;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Collections;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Model.KQM.Query.OTM;
using GDSBG.MiABU.Attendance.IDAL.WFReporter;
using System.Data.OleDb;

namespace GDSBG.MiABU.Attendance.OracleDAL.WFReporter
{
    public class OTReporterDal : DALBase<OTMTotalQryModel>, IOTReporterDal
    {
        /// <summary>
        /// 加班統計表查詢,依據月份
        /// </summary>
        /// <param name="yearMonth"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetOTMQryList(string SQLDep, string yearMonth, int pageIndex, int pageSize, out int totalCount)
        {

            List<OracleParameter> listPara = new List<OracleParameter>();
            string cmdText = @"SELECT workno,yearmonth,g1apply, g2apply, g3apply, g1relsalary, g2relsalary,g3relsalary, madjust1, g2remain,billno,apremark,approver,approvedate,
                             mreladjust, approveflag, specg1apply,specg2apply, specg3apply, specg1salary, specg2salary, specg3salary,depcode,dissignrmark,
                             overtimetype, localname, dname, day1, day2, day3, day4, day5, day6,day7, day8, day9, day10, day11, day12, day13, day14, day15, day16,
                             day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,day27, day28, day29, day30, day31, buname, approveflagname
                             FROM gds_att_monthtotal_v a where 1=1 ";

            cmdText = cmdText + " AND a.depcode in (" + SQLDep + ")";
            if (!string.IsNullOrEmpty(yearMonth))
            {
                cmdText = cmdText + " AND a.yearmonth=:yearmonth ";
                listPara.Add(new OracleParameter(":yearmonth", yearMonth));
            }
            DataTable dt = DalHelper.ExecutePagerQuery(cmdText, pageIndex, pageSize, out totalCount, listPara.ToArray());
            return dt;
        }
    }
}
