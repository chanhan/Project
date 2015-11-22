/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KaoQinQryDal.cs
 * 檔功能描述： 考勤結果查詢操作類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.27
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
    public class KaoQinQryDal : DALBase<KaoQinDataQueryModel>, IKaoQinQryDal
    {
        /// <summary>
        /// 根據條件分頁查詢
        /// </summary>
        /// <param name="sql">組織權限</param>
        /// <param name="depname">單位</param>
        /// <param name="IsMakeup">是否補卡</param>
        /// <param name="IsSupporter">是否支援</param>
        /// <param name="EmployeeNo">一筆工號</param>
        /// <param name="BatchEmployeeNo">多筆工號</param>
        /// <param name="LocalName">姓名</param>
        /// <param name="fromDate">考勤起始日期</param>
        /// <param name="toDate">考勤截止日期</param>
        /// <param name="StatusName">考勤狀態</param>
        /// <param name="ExceptionType">結果</param>
        /// <param name="ShiftNo">班別</param>
        /// <param name="Status">在職狀態</param>
        /// <param name="flagA"></param>
        /// <param name="flagB"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetKaoQinDataList(string sql,string depname,string IsMakeup,string IsSupporter,string EmployeeNo,string BatchEmployeeNo,string LocalName,string fromDate,string toDate,string StatusName,string ExceptionType,string ShiftNo,string Status,bool flagA,bool flagB, int PageIndex, int PageSize, out int totalCount)
        {
            string cmdText = "";
            cmdText = "select a.workno,a.kqdate,a.shiftno,a.localname,a.workhours,a.depname,a.dcode,a.otondutytime,a.otoffdutytime,a.shiftdesc,a.ondutytime,a.offdutytime,a.absentqty,"
                        +"a.status,a.exceptiontype,a.reasontype,a.reasonremark,a.reasonname,a.exceptiontypename,a.statusname,a.ismakeup,a.othours,a.totalhours,a.absenttotal,b.flag,b.status empstatus from gds_att_kaoqindataquery_v a,gds_att_employee b where a.workno=b.workno ";
            string condition = " and a.dCode in (" + sql + ")";
            cmdText = cmdText + condition;
            if(!string.IsNullOrEmpty(depname))
            {
                cmdText+=" AND a.dCode IN (SELECT DepCode FROM gds_sc_department START WITH depname = '" + depname + "' CONNECT BY PRIOR depcode = parentdepcode)";
            }
            if(IsMakeup.Equals("Y"))
            {
                cmdText+=" AND exists (select 1 from gds_att_makeup c where c.workno=a.workno and c.kqdate=a.kqdate and c.Status='2')";
            }
            if (!string.IsNullOrEmpty(IsSupporter))
            {
                if (IsSupporter.Equals("N"))
                {
                    cmdText+= " AND b.flag='Local' ";
                }
                else
                {
                    cmdText+=" AND b.flag='Supporter' ";
                }
            }
            if(!string.IsNullOrEmpty(EmployeeNo))
            {
                cmdText+=" AND a.WorkNo like '" + EmployeeNo + "%'";
            }
            if (!string.IsNullOrEmpty(BatchEmployeeNo))
            {
                cmdText+= " and a.workno in (SELECT char_list    FROM TABLE (gds_sc_chartotable ('" + BatchEmployeeNo + "', '§')))";
            }
            if (!string.IsNullOrEmpty(LocalName))
            {
               cmdText+=" AND b.LocalName like '" + LocalName + "%'";
            }
            if(!string.IsNullOrEmpty(StatusName))
            {
                if (StatusName == "1")
                {
                    cmdText += " AND (a.Status='1' OR a.Status='2' OR a.Status='4' OR a.Status='5')";
                }
                else
                {
                    cmdText += " AND (a.Status='0' OR a.Status='3')";
                }
            }
            if(!string.IsNullOrEmpty(ExceptionType))
            {
                cmdText += " and a.exceptiontype in (" + ExceptionType + ")";
            }
            if (!string.IsNullOrEmpty(ShiftNo))
            {
                cmdText += " AND INSTR(a.ShiftNo,'" + ShiftNo + "')>0 ";
            }
            if (!string.IsNullOrEmpty(Status))
            {
                cmdText += " and b.status in (" + Status + ")";
            }
            if (flagA == true)
            {
                cmdText += "AND (((INSTR (a.shiftno, 'A') = 1 OR INSTR (a.shiftno, 'B') = 1) AND a.kqdate = TO_DATE ('" + DateTime.Parse(fromDate).ToString("yyyy/MM/dd") + "', 'yyyy/mm/dd')) OR (INSTR (a.shiftno, 'C') = 1 AND a.kqdate = TO_DATE ('2011/11/12', 'yyyy/mm/dd')))";
            }
            else if (flagB == true)
            {
                cmdText += "AND (((INSTR (a.shiftno, 'A') = 1 OR INSTR (a.shiftno, 'B') = 1) AND a.kqdate = TO_DATE ('" + DateTime.Parse(fromDate).ToString("yyyy/MM/dd") + "', 'yyyy/mm/dd')) AND EXISTS (SELECT 1 FROM kqm_kaoqindata x WHERE x.workno = a.workno AND INSTR (x.shiftno, 'C') = 1AND x.kqdate = a.kqdate - 1))";
            }
            else
            {
                if (!string.IsNullOrEmpty(fromDate))
                {
                    cmdText += " AND a.kqdate >= to_date('" + DateTime.Parse(fromDate).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
                }
                if (!string.IsNullOrEmpty(toDate))
                {
                    cmdText += " AND a.kqdate <= to_date('" + DateTime.Parse(toDate).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
                }
            }
            return DalHelper.ExecutePagerQuery(cmdText, PageIndex, PageSize, out totalCount);
        }

        /// <summary>
        /// 導出查詢
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="depname"></param>
        /// <param name="IsMakeup"></param>
        /// <param name="IsSupporter"></param>
        /// <param name="EmployeeNo"></param>
        /// <param name="BatchEmployeeNo"></param>
        /// <param name="LocalName"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="StatusName"></param>
        /// <param name="ExceptionType"></param>
        /// <param name="ShiftNo"></param>
        /// <param name="Status"></param>
        /// <param name="flagA"></param>
        /// <param name="flagB"></param>
        /// <returns></returns>
        public DataTable GetKaoQinDataForExport(string sql, string depname, string IsMakeup, string IsSupporter, string EmployeeNo, string BatchEmployeeNo, string LocalName, string fromDate, string toDate, string StatusName, string ExceptionType, string ShiftNo, string Status, bool flagA, bool flagB)
        {
            string cmdText = "";
            cmdText = "select a.workno,a.kqdate,a.shiftno,a.localname,a.workhours,a.depname,a.dcode,a.otondutytime,a.otoffdutytime,a.shiftdesc,a.ondutytime,a.offdutytime,a.absentqty,"
                        + "a.status,a.exceptiontype,a.reasontype,a.reasonremark,a.reasonname,a.exceptiontypename,a.statusname,a.ismakeup,a.othours,a.totalhours,a.absenttotal,b.flag,b.status empstatus from gds_att_kaoqindataquery_v a,gds_att_employee b where a.workno=b.workno ";
            string condition = " and a.dCode in (" + sql + ")";
            cmdText = cmdText + condition;
            if (!string.IsNullOrEmpty(depname))
            {
                cmdText += " AND a.dCode IN (SELECT DepCode FROM gds_sc_department START WITH depname = '" + depname + "' CONNECT BY PRIOR depcode = parentdepcode";
            }
            if (IsMakeup.Equals("Y"))
            {
                cmdText += " AND exists (select 1 from gds_att_makeup c where c.workno=workno and c.kqdate=kqdate and c.Status='2')";
            }
            if (!string.IsNullOrEmpty(IsSupporter))
            {
                if (IsSupporter.Equals("N"))
                {
                    cmdText += " AND b.flag='Local' ";
                }
                else
                {
                    cmdText += " AND b.flag='Supporter' ";
                }
            }
            if (!string.IsNullOrEmpty(EmployeeNo))
            {
                cmdText += " AND a.WorkNo like '" + EmployeeNo + "%'";
            }
            if (!string.IsNullOrEmpty(BatchEmployeeNo))
            {
                cmdText += " and a.workno in (SELECT char_list    FROM TABLE (gds_sc_chartotable ('" + BatchEmployeeNo + "', '§')))";
            }
            if (!string.IsNullOrEmpty(LocalName))
            {
                cmdText += " AND b.LocalName like '" + LocalName + "%'";
            }
            if (!string.IsNullOrEmpty(StatusName))
            {
                if (StatusName == "1")
                {
                    cmdText += " AND (Status='1' OR Status='2' OR Status='4' OR Status='5')";
                }
                else
                {
                    cmdText += " AND (Status='0' OR Status='3')";
                }
            }
            if (!string.IsNullOrEmpty(ExceptionType))
            {
                cmdText += " and a.exceptiontype in (" + ExceptionType + ")";
            }
            if (!string.IsNullOrEmpty(ShiftNo))
            {
                cmdText += " AND INSTR(a.ShiftNo,'" + ShiftNo + "')>0 ";
            }
            if (!string.IsNullOrEmpty(Status))
            {
                cmdText += " and b.status in (" + Status + ")";
            }
            if (flagA == true)
            {
                cmdText += "AND (((INSTR (a.shiftno, 'A') = 1 OR INSTR (a.shiftno, 'B') = 1) AND a.kqdate = TO_DATE ('" + DateTime.Parse(fromDate).ToString("yyyy/MM/dd") + "', 'yyyy/mm/dd')) OR (INSTR (a.shiftno, 'C') = 1 AND a.kqdate = TO_DATE ('2011/11/12', 'yyyy/mm/dd')))";
            }
            else if (flagB == true)
            {
                cmdText += "AND (((INSTR (a.shiftno, 'A') = 1 OR INSTR (a.shiftno, 'B') = 1) AND a.kqdate = TO_DATE ('" + DateTime.Parse(fromDate).ToString("yyyy/MM/dd") + "', 'yyyy/mm/dd')) AND EXISTS (SELECT 1 FROM kqm_kaoqindata x WHERE x.workno = a.workno AND INSTR (x.shiftno, 'C') = 1AND x.kqdate = a.kqdate - 1))";
            }
            else
            {
                if (!string.IsNullOrEmpty(fromDate))
                {
                    cmdText += " AND a.kqdate >= to_date('" + DateTime.Parse(fromDate).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
                }
                if (!string.IsNullOrEmpty(toDate))
                {
                    cmdText += " AND a.kqdate <= to_date('" + DateTime.Parse(toDate).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
                }
            }
            return DalHelper.ExecuteQuery(cmdText);
        }

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<KaoQinDataQueryModel> GetList(DataTable dt)
        {
            return OrmHelper.SetDataTableToList(dt);
        }

        /// <summary>
        /// 獲得當月曠工累計
        /// </summary>
        /// <param name="workno"></param>
        /// <param name="kqdate"></param>
        /// <returns></returns>
        public DataTable GetAbsentTotal(string workno, string kqdate)
        {
            string str = "SELECT ExcepTionQty FROM (SELECT SUM(DECODE(instr(ExceptionType,'C'),1,1,0))+SUM(DECODE(instr(ExceptionType,'D'),1,1,0)) ExcepTionQty FROM gds_att_kaoqindata WHERE workno =:WorkNo and kqdate>last_day(add_months(to_date(:KqDate,'yyyy/MM/dd'),-1)) and kqdate<=last_day(to_date(:KqDate,'yyyy/MM/dd')))";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":workno", workno), new OracleParameter(":kqdate", kqdate));
        }
    }
}
