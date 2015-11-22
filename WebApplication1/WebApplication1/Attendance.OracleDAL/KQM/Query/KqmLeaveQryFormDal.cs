/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KqmLeaveQryFormDal.cs
 * 檔功能描述： 請假明細查詢數據操作類
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.12.29
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


namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.Query
{
    public class KqmLeaveQryFormDal : DALBase<KqmLeaveQryFormModel>, IKqmLeaveQryFormDal
    {
        /// <summary>
        /// 查詢休假類型
        /// </summary>
        /// <returns>休假類型集</returns>
        public DataTable GetLeaveType()
        {
            return DalHelper.ExecuteQuery(@"SELECT a.LVTypeName,a.LVTypeCode FROM gds_att_leavetype a ORDER BY a.LVTypeCode");
        }
        /// <summary>
        /// 查詢員工離職日期
        /// </summary>
        /// <param name="sID">請假單序號</param>
        /// <returns></returns>
        public string GetLeaveDate(string sID)
        {
            DataTable dt = DalHelper.ExecuteQuery(@"select to_char(b.Leavedate,'yyyy/MM/dd')Leavedate from gds_att_leaveapply a, gds_att_employee b where a.workno=b.workno and b.Status>'0' and a.id='" + sID + "'");
            if (dt.Rows.Count > 0 && dt != null)
            {
                return dt.Rows[0]["Leavedate"].ToString();
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 獲得離職日期和請假結束日期的早晚
        /// </summary>
        /// <param name="LeaveDate">離職日期</param>
        /// <param name="sEndDate">請假結束日期</param>
        /// <returns></returns>
        public string GetFlagEndDate(string LeaveDate, string sEndDate)
        {
            DataTable dt = DalHelper.ExecuteQuery(@"SELECT round(TO_DATE('" + sEndDate + "','yyyy/mm/dd')-TO_DATE('" + LeaveDate + "','yyyy/mm/dd')) flag FROM dual");
            if (dt.Rows.Count > 0 && dt != null)
            {
                return dt.Rows[0]["flag"].ToString();
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 查詢請假的詳細信息
        /// </summary>
        /// <param name="sID">請假單序號</param>
        /// <param name="sStartDate">開始日期</param>
        /// <param name="sEndDate">結束日期</param>
        /// <returns></returns>
        public string GetLeaveDetail(string sID, string sStartDate, string strEndDate)
        {
            DataTable dt = DalHelper.ExecuteQuery(@"SELECT NVL (SUM (lvtotal), 0) lvtotal  FROM gds_att_leavedetail WHERE ID = '" + sID + "'  AND lvdate >= TO_DATE ('" + sStartDate + "', 'yyyy/MM/dd') AND lvdate <= TO_DATE ('" + strEndDate + "', 'yyyy/MM/dd')");
            if (dt.Rows.Count > 0 && dt != null)
            {
                return dt.Rows[0]["lvtotal"].ToString();
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根據條件查詢
        /// </summary>
        /// <param name="model">請假明細查詢的model</param>
        /// <param name="BatchEmployeeNo">批量查詢的更好</param>
        /// <param name="leaveType">請假類型</param>
        /// <param name="startDate">請假開始日期</param>
        /// <param name="empStatus">職工在職狀態</param>
        /// <param name="endDate">請假技術日期</param>
        /// <param name="pageIndex">頁碼</param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount">總條數</param>
        /// <returns></returns>
        public DataTable GetLeaveDataList(KqmLeaveQryFormModel model, string depCode, string SQLDep, string BatchEmployeeNo, string leaveType, string startDate, string empStatus, string endDate, int pageIndex, int pageSize, out int totalCount)
        {
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"SELECT ID, workno, lvtotal, lvtypecode, proxy, reason, approver, stime, etime,startdate, enddate, starttime,
                             endtime,leavetype, depcode, localname, depname,status, statusname,thislvtotal,lvtotaldays FROM gds_sc_leavequerydata_v a  where 1=1 ";
            cmdText += strCon;
            if (!string.IsNullOrEmpty(depCode))
            {
                cmdText = cmdText + "  AND a.depcode IN ((" + SQLDep + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depcode = '" + depCode + "' CONNECT BY PRIOR depcode = parentdepcode) ";
            }
            else
            {
                cmdText = cmdText + " AND a.depcode in (" + SQLDep + ")";
            }
            if (BatchEmployeeNo != "")
            {
                cmdText = cmdText + " and a.workno in (SELECT char_list    FROM TABLE (gds_sc_chartotable ('" + BatchEmployeeNo + "', '§')))";
            }
            if (leaveType != "")
            {
                cmdText = cmdText + " and a.lvtypecode in (SELECT char_list    FROM TABLE (gds_sc_chartotable ('" + leaveType + "', '§')))";
            }
            if (empStatus != "")
            {
                cmdText = cmdText + " and a.Status  in (SELECT char_list    FROM TABLE (gds_sc_chartotable ('" + empStatus + "', '§')))";
            }
            if (!string.IsNullOrEmpty(startDate) && (!string.IsNullOrEmpty(endDate)))
            {
                cmdText = cmdText + @"AND ((to_date(to_char(a.startDate,'yyyy/mm/dd')||a.StartTime,'yyyy/mm/dd hh24:mi') <= to_date('" + startDate + " 07:00','yyyy/mm/dd hh24:mi') AND to_date(to_char(a.endDate,'yyyy/mm/dd')||a.EndTime,'yyyy/mm/dd hh24:mi') >= to_date('" + startDate + " 07:00','yyyy/mm/dd hh24:mi')) or (to_date(to_char(a.startDate,'yyyy/mm/dd')||a.StartTime,'yyyy/mm/dd hh24:mi') <= to_date('" + endDate + " 07:00','yyyy/mm/dd hh24:mi') AND to_date(to_char(a.endDate,'yyyy/mm/dd')||a.EndTime,'yyyy/mm/dd hh24:mi') >= to_date('" + endDate + " 07:00','yyyy/mm/dd hh24:mi')) or (to_date(to_char(a.startDate,'yyyy/mm/dd')||a.StartTime,'yyyy/mm/dd hh24:mi') >= to_date('" + startDate + " 07:00','yyyy/mm/dd hh24:mi') AND to_date(to_char(a.endDate,'yyyy/mm/dd')||a.EndTime,'yyyy/mm/dd hh24:mi') <= to_date('" + endDate + " 07:00','yyyy/mm/dd hh24:mi')))";
            }
            DataTable dt = DalHelper.ExecutePagerQuery(cmdText, pageIndex, pageSize,  out totalCount, listPara.ToArray());
            return dt;
        }
        /// <summary>
        /// 根據條件查詢
        /// </summary>
        /// <param name="model">請假明細查詢的model</param>
        /// <param name="BatchEmployeeNo">批量查詢的更好</param>
        /// <param name="leaveType">請假類型</param>
        /// <param name="startDate">請假開始日期</param>
        /// <param name="empStatus">職工在職狀態</param>
        /// <param name="endDate">請假技術日期</param>
        /// <returns></returns>
        public DataTable GetLeaveDataList(KqmLeaveQryFormModel model, string depCode, string SQLDep, string BatchEmployeeNo, string leaveType, string startDate, string empStatus, string endDate)
        {
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"SELECT ID, workno, lvtotal, lvtypecode, proxy, reason, approver, stime, etime,startdate, enddate, starttime,
                             endtime,leavetype, depcode, localname, depname,status, statusname,thislvtotal,lvtotaldays FROM gds_sc_leavequerydata_v a  where 1=1 ";
            cmdText += strCon;
            if (!string.IsNullOrEmpty(depCode))
            {
                cmdText = cmdText + "  AND a.depcode IN ((" + SQLDep + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depcode = '" + depCode + "' CONNECT BY PRIOR depcode = parentdepcode) ";
            }
            else
            {
                cmdText = cmdText + " AND a.depcode in (" + SQLDep + ")";
            }
            if (BatchEmployeeNo != "")
            {
                cmdText = cmdText + " and a.workno in (SELECT char_list    FROM TABLE (gds_sc_chartotable ('" + BatchEmployeeNo + "', '§')))";
            }
            if (leaveType != "")
            {
                cmdText = cmdText + " and a.lvtypecode in (SELECT char_list    FROM TABLE (gds_sc_chartotable ('" + leaveType + "', '§')))";
            }
            if (empStatus != "")
            {
                cmdText = cmdText + " and a.Status  in (SELECT char_list    FROM TABLE (gds_sc_chartotable ('" + empStatus + "', '§')))";
            }
            if (!string.IsNullOrEmpty(startDate) && (!string.IsNullOrEmpty(endDate)))
            {
                cmdText = cmdText + @"AND ((to_date(to_char(a.startDate,'yyyy/mm/dd')||a.StartTime,'yyyy/mm/dd hh24:mi') <= to_date('" + startDate + " 07:00','yyyy/mm/dd hh24:mi') AND to_date(to_char(a.endDate,'yyyy/mm/dd')||a.EndTime,'yyyy/mm/dd hh24:mi') >= to_date('" + startDate + " 07:00','yyyy/mm/dd hh24:mi')) or (to_date(to_char(a.startDate,'yyyy/mm/dd')||a.StartTime,'yyyy/mm/dd hh24:mi') <= to_date('" + endDate + " 07:00','yyyy/mm/dd hh24:mi') AND to_date(to_char(a.endDate,'yyyy/mm/dd')||a.EndTime,'yyyy/mm/dd hh24:mi') >= to_date('" + endDate + " 07:00','yyyy/mm/dd hh24:mi')) or (to_date(to_char(a.startDate,'yyyy/mm/dd')||a.StartTime,'yyyy/mm/dd hh24:mi') >= to_date('" + startDate + " 07:00','yyyy/mm/dd hh24:mi') AND to_date(to_char(a.endDate,'yyyy/mm/dd')||a.EndTime,'yyyy/mm/dd hh24:mi') <= to_date('" + endDate + " 07:00','yyyy/mm/dd hh24:mi')))";
            }
            DataTable dt = DalHelper.ExecuteQuery(cmdText,listPara.ToArray());
            return dt;
        }
        /// <summary>
        /// 將datatable轉換成model
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<KqmLeaveQryFormModel> GetModelList(DataTable dt)
        {
            return OrmHelper.SetDataTableToList(dt);
        }
    }
}
