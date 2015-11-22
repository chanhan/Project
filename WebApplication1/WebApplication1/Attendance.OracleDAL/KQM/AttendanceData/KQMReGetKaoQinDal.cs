/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMReGetKaoQinDal.cs
 * 檔功能描述： 重新計算數據操作類
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.12.26
 * 
 */

using System;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.KQM.AttendanceData;
using GDSBG.MiABU.Attendance.Model.KQM.AttendanceData;
using System.Collections;
using System.Collections.Generic;

namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.AttendanceData
{
    public class KQMReGetKaoQinDal : DALBase<KQMReGetKaoQinModel>, IKQMReGetKaoQinDal
    {
        /// <summary>
        /// 查詢操作日期是否超過倆個月
        /// </summary>
        /// <param name="FromDate">開始日期</param>
        /// <param name="ToDate">結束日期</param>
        /// <returns></returns>
        public DataTable GetFromDate(string FromDate, string ToDate)
        {
            return DalHelper.ExecuteQuery(@"select floor(MONTHS_BETWEEN(to_date('" + Convert.ToDateTime(ToDate).ToString("yyyy/MM/dd") + "','yyyy/MM/dd'),to_date('" + Convert.ToDateTime(FromDate).ToString("yyyy/MM/dd") + "','yyyy/MM/dd'))) sDays from dual");
        }
        /// <summary>
        /// 將datatable轉化成List導出
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<KQMReGetKaoQinModel> GetModelList(DataTable dt)
        {
            return OrmHelper.SetDataTableToList(dt);
        }
        /// <summary>
        /// 重新計算查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="workNoList">批量查詢的工號集</param>
        /// <param name="temVal">checkBoxlist集 異常類型</param>
        /// <param name="shiftNo">班別代碼</param>
        /// <param name="ststus">狀態</param>
        /// <param name="pageIndex">當前頁碼</param>
        /// <param name="pageSize">一頁顯示條數</param>
        /// <param name="totalCount">總數</param>
        /// <returns></returns>
        public DataTable GetKaoQinDataList(KQMReGetKaoQinModel model, string workNoList, string temVal, string shiftNo, string status, string fromDate, string toDate, string SQLDep, string depCode, int pageIndex, int pageSize, out int totalCount)
        {
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"SELECT workno, kqdate, shiftno, localname, depname, dcode, otondutytime,otoffdutytime, shiftdesc, ondutytime,
                             offdutytime, absentqty, status,exceptiontype,exceptionname, reasontype, reasonremark, reasonname, statusname, othours, totalhours
                             FROM gds_att_kaoqindata_v a where 1=1 ";
            cmdText += strCon;
            if (!string.IsNullOrEmpty(depCode))
            {
                cmdText = cmdText + " AND a.dCode IN ((" + SQLDep + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depcode = '" + depCode + "' CONNECT BY PRIOR depcode = parentdepcode) ";
            }
            else
            {
                cmdText = cmdText + " AND a.dcode in (" + SQLDep + ")";
            }
            if (workNoList != "")
            {
                cmdText = cmdText + " and a.workno in (SELECT char_list    FROM TABLE (gds_sc_chartotable ('" + workNoList + "', '§')))";
            }
            if (temVal != "")
            {
                cmdText = cmdText + " and a.ExceptionType in (SELECT char_list    FROM TABLE (gds_sc_chartotable ('" + temVal + "', '§')))";
            }
            if (!string.IsNullOrEmpty(shiftNo))
            {
                if (shiftNo != null)
                {
                    if (!(shiftNo == "A"))
                    {
                        if (shiftNo == "C")
                        {
                            cmdText = cmdText + " AND INSTR(a.ShiftNo,'C')>0 ";
                        }
                    }
                    else
                    {
                        cmdText = cmdText + " AND (INSTR(a.ShiftNo,'A')>0 OR INSTR(a.ShiftNo,'B')>0)";
                    }
                }
            }
            if (!string.IsNullOrEmpty(status))
            {
                if (status == "1")
                {
                    cmdText = cmdText + " AND (a.Status='1' OR a.Status='2' OR a.Status='4' OR a.Status='5')";
                }
                else
                {
                    cmdText = cmdText + "  AND (a.Status ='0' or a.Status ='3') ";
                }
            }
            if (!string.IsNullOrEmpty(fromDate))
            {
                cmdText = cmdText + " AND a.KQDate >= to_date('" + DateTime.Parse(fromDate).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                cmdText = cmdText + " AND a.KQDate <= to_date('" + DateTime.Parse(toDate).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
            }
            DataTable dt = DalHelper.ExecutePagerQuery(cmdText, pageIndex, pageSize, out totalCount, listPara.ToArray());
            return dt;
        }
        /// <summary>
        /// 重新計算查詢(導出)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="workNoList">批量查詢的工號集</param>
        /// <param name="temVal">checkBoxlist集 異常類型</param>
        /// <param name="shiftNo">班別代碼</param>
        /// <param name="ststus">狀態</param>
        /// <returns></returns>
        public DataTable GetKaoQinDataList(KQMReGetKaoQinModel model, string workNoList, string temVal, string shiftNo, string status, string fromDate, string toDate, string SQLDep, string depCode)
        {
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"SELECT workno, kqdate, shiftno, localname, depname, dcode, otondutytime,otoffdutytime, shiftdesc, ondutytime,
                             offdutytime, absentqty, status,exceptiontype,exceptionname, reasontype, reasonremark, reasonname, statusname, othours, totalhours
                             FROM gds_att_kaoqindata_v a where 1=1 ";
            cmdText += strCon;
            if (!string.IsNullOrEmpty(depCode))
            {
                cmdText = cmdText + " AND a.dCode IN ((" + SQLDep + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depcode = '" + depCode + "' CONNECT BY PRIOR depcode = parentdepcode) ";
            }
            else
            {
                cmdText = cmdText + " AND a.dcode in (" + SQLDep + ")";
            }
            if (workNoList != "")
            {
                cmdText = cmdText + " and a.workno in (SELECT char_list    FROM TABLE (gds_sc_chartotable ('" + workNoList + "', '§')))";
            }
            if (temVal != "")
            {
                cmdText = cmdText + " and a.ExceptionType in (SELECT char_list    FROM TABLE (gds_sc_chartotable ('" + temVal + "', '§')))";
            }
            if (!string.IsNullOrEmpty(shiftNo)) 
            {
                if (shiftNo != null)
                {
                    if (!(shiftNo == "A"))
                    {
                        if (shiftNo == "C")
                        {
                            cmdText = cmdText + " AND INSTR(a.ShiftNo,'C')>0 ";
                        }
                    }
                    else
                    {
                        cmdText = cmdText + " AND (INSTR(a.ShiftNo,'A')>0 OR INSTR(a.ShiftNo,'B')>0)";
                    }
                }
            }
            if (!string.IsNullOrEmpty(status))
            {
                if (status == "1")
                {
                    cmdText = cmdText + " AND (a.Status='1' OR a.Status='2' OR a.Status='4' OR a.Status='5')";
                }
                else
                {
                    cmdText = cmdText + "  AND (a.Status ='0' or a.Status ='3') ";
                }
            }
            if (!string.IsNullOrEmpty(fromDate))
            {
                cmdText = cmdText + " AND a.KQDate >= to_date('" + DateTime.Parse(fromDate).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                cmdText = cmdText + " AND a.KQDate <= to_date('" + DateTime.Parse(toDate).ToString("yyyy/MM/dd") + "','yyyy/mm/dd') ";
            }
            DataTable dt = DalHelper.ExecuteQuery(cmdText, listPara.ToArray());
            return dt;
        }
        /// <summary>
        /// 查詢每月多少日之後不允許重新計算上月及以前考勤數據
        /// </summary>
        /// <returns></returns>
        public string GetValueLastDay()
        {
            DataTable dt = DalHelper.ExecuteQuery("select nvl(MAX(paravalue),'5') day from gds_sc_parameter where paraname='KQMReGetKaoQin'");
            string result = dt.Rows[0]["day"].ToString();
            return result;
        }
        /// <summary>
        /// 調用存儲過程重新計算查詢部份的員工考勤
        /// </summary>
        /// <param name="WorkNo">員工工號</param>
        /// <param name="roleCode">員工角色 空字符串</param>
        /// <param name="FromKQDate">考勤開始時間</param>
        /// <param name="ToKQDate">考勤結束時間</param>
        public void GetKaoQinData(string workNo, string roleCode, string fromKQDate, string toKQDate)
        {
            DataTable dt = DalHelper.ExecuteQuery("gds_reget_kaoqindata", CommandType.StoredProcedure,
                new OracleParameter("p_workno", workNo), new OracleParameter("p_orgcode", roleCode),
                new OracleParameter("p_frmkqdate", fromKQDate), new OracleParameter("p_tokqdate", toKQDate));
        }
    }
}
