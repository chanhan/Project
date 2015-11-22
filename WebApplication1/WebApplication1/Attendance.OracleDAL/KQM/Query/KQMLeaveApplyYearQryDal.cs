/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMLeaveApplyYearQryDal.cs
 * 檔功能描述： 年休假統計查詢操作類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.30
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.KQM.Query;
using GDSBG.MiABU.Attendance.Model.KQM.Query;

namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.Query
{
    public class KQMLeaveApplyYearQryDal : DALBase<KQMLeaveApplyYearQryModel>, IKQMLeaveApplyYearQryDal
    {
        /// <summary>
        /// 年休假統計分頁查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="BatchEmployeeNo">多筆工號</param>
        /// <param name="joindatefrom"></param>
        /// <param name="joindateto"></param>
        /// <param name="countdatefrom"></param>
        /// <param name="countdateto"></param>
        /// <param name="Status">在職狀態</param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetLeaveApplyYear(KQMLeaveApplyYearQryModel model,string sql, string BatchEmployeeNo, string joindatefrom, string joindateto, string countdatefrom, string countdateto, string Status, int PageIndex, int PageSize, out int totalCount)
        {
            string strCon = "";
            string depName = "";
            if (!string.IsNullOrEmpty(model.DepName))
            {
                depName = model.DepName.ToString();
            }
            model.DepName = "";
            model.DepCode = "";
            string condition = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"SELECT a.workno,a.leaveyear,a.startyears,a.endyears,a.outworkyears,a.outfoxconnyears,a.standarddays,a.alreaddays,a.nextyeardays,a.leavedays,a.reachdate,a.reachleavedays,a.leaverecdays,a.update_date,a.lastyearremain,"
                              + "a.currendays,a.depcode,a.localname,a.depname,a.joindate,a.enablestartdate,a.statusname,a.sexname,a.countdays from gds_att_leaveyearsquery_v  a,gds_att_employee b where b.flag = 'Local' and b.workno=a.workno and (select TempFlag from gds_att_employees where workno = a.WorkNo) = 'N'";
            if (!string.IsNullOrEmpty(depName))
            {
                condition = "AND a.depcode IN ((" + sql + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depname = :depName CONNECT BY PRIOR depcode = parentdepcode) ";
                listPara.Add(new OracleParameter(":depName", depName));
            }
            else
            {
                condition = " AND a.depcode IN (" + sql + ") ";
            }
            cmdText += strCon + condition;
            if (!string.IsNullOrEmpty(BatchEmployeeNo))
            {
                cmdText += " and a.workno in (SELECT char_list    FROM TABLE (gds_sc_chartotable ('" + BatchEmployeeNo + "', '§')))";
            }
            if (!string.IsNullOrEmpty(joindatefrom))
            {
                cmdText += " AND a.joindate >= to_date('" + DateTime.Parse(joindatefrom).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
            }
            if (!string.IsNullOrEmpty(joindateto))
            {
                cmdText += " AND a.joindate <= to_date('" + DateTime.Parse(joindateto).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
            }
            if (!string.IsNullOrEmpty(countdatefrom))
            {
                cmdText += " AND a.update_date >= to_date('" + DateTime.Parse(countdatefrom).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
            }
            if (!string.IsNullOrEmpty(countdateto))
            {
                cmdText += " AND a.update_date <= to_date('" + DateTime.Parse(countdateto).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
            }
            if (!string.IsNullOrEmpty(Status))
            {
                cmdText += " and b.status in (" + Status + ")";
            }
            return DalHelper.ExecutePagerQuery(cmdText, PageIndex, PageSize, out totalCount, listPara.ToArray());
        }

        /// <summary>
        /// 導出查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sql"></param>
        /// <param name="BatchEmployeeNo"></param>
        /// <param name="joindatefrom"></param>
        /// <param name="joindateto"></param>
        /// <param name="countdatefrom"></param>
        /// <param name="countdateto"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public DataTable GetLeaveApplyYearForExport(KQMLeaveApplyYearQryModel model, string sql, string BatchEmployeeNo, string joindatefrom, string joindateto, string countdatefrom, string countdateto, string Status)
        {
            string strCon = "";
            string depName = "";
            if (!string.IsNullOrEmpty(model.DepName))
            {
                depName = model.DepName.ToString();
            }
            model.DepName = "";
            model.DepCode = "";
            string condition = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"SELECT a.workno,a.leaveyear,a.startyears,a.endyears,a.outworkyears,a.outfoxconnyears,a.standarddays,a.alreaddays,a.nextyeardays,a.leavedays,a.reachdate,a.reachleavedays,a.leaverecdays,a.update_date,a.lastyearremain,"
                              + "a.currendays,a.depcode,a.localname,a.depname,a.joindate,a.enablestartdate,a.statusname,a.sexname,a.countdays from gds_att_leaveyearsquery_v  a,gds_att_employee b where b.flag = 'Local' and b.workno=a.workno and (select TempFlag from gds_att_employees where workno = a.WorkNo) = 'N'";
            if (!string.IsNullOrEmpty(depName))
            {
                condition = "AND a.depcode IN ((" + sql + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depname = :depName CONNECT BY PRIOR depcode = parentdepcode) ";
                listPara.Add(new OracleParameter(":depName", depName));
            }
            else
            {
                condition = " AND a.depcode IN (" + sql + ") ";
            }
            cmdText += strCon + condition;
            if (!string.IsNullOrEmpty(BatchEmployeeNo))
            {
                cmdText += " and a.workno in (SELECT char_list    FROM TABLE (gds_sc_chartotable ('" + BatchEmployeeNo + "', '§')))";
            }
            if (!string.IsNullOrEmpty(joindatefrom))
            {
                cmdText += " AND a.joindate >= to_date('" + DateTime.Parse(joindatefrom).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
            }
            if (!string.IsNullOrEmpty(joindateto))
            {
                cmdText += " AND a.joindate <= to_date('" + DateTime.Parse(joindateto).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
            }
            if (!string.IsNullOrEmpty(countdatefrom))
            {
                cmdText += " AND a.update_date >= to_date('" + DateTime.Parse(countdatefrom).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
            }
            if (!string.IsNullOrEmpty(countdateto))
            {
                cmdText += " AND a.update_date <= to_date('" + DateTime.Parse(countdateto).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
            }
            if (!string.IsNullOrEmpty(Status))
            {
                cmdText += " and b.status in (" + Status + ")";
            }
            return DalHelper.ExecuteQuery(cmdText, listPara.ToArray());
        }

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<KQMLeaveApplyYearQryModel> GetList(DataTable dt)
        {
            return OrmHelper.SetDataTableToList(dt);
        }

        /// <summary>
        /// 獲得統計天數
        /// </summary>
        /// <param name="workno"></param>
        /// <param name="countdatefrom"></param>
        /// <param name="countdateto"></param>
        /// <returns></returns>
        public DataTable GetCountDays(string workno, string countdatefrom, string countdateto)
        {
            string str = "SELECT nvl(sum(lvtotal/8),0)lvdays from gds_att_leavedetail a where a.WorkNo=:WorkNo and a.LVTypeCode='Y' and a.lvdate >= to_date(:CountDateFrom ,'yyyy/mm/dd') and a.lvdate <= to_date(:CountDateTo,'yyyy/mm/dd')";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":workno", workno), new OracleParameter(":countdatefrom", countdatefrom), new OracleParameter(":countdateto", countdateto));
        }
    }
}
