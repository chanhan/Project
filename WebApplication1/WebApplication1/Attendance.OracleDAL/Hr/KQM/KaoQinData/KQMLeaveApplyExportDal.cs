/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMLeaveApplyExportDal.cs
 * 檔功能描述： 請假申請數據導出數據操作類
 * 
 * 版本：1.0
 * 創建標識： 陳函 2012.3.10
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDSBG.MiABU.Attendance.Model.KQM.KaoQinData;
using System.Data;
using GDSBG.MiABU.Attendance.IDAL.Hr.KQM.KaoQinData;

namespace GDSBG.MiABU.Attendance.OracleDAL.Hr.KQM.KaoQinData
{
    public class KQMLeaveApplyExportDal : DALBase<LeaveApplyViewModel>, IKQMLeaveApplyExportDal
    {
        /// <summary>
        /// 獲取請假信息用於導出Excel
        /// </summary>
        /// <param name="Privileged">是否有組織權限</param>
        /// <param name="sqlDep">組織權限管控</param>
        /// <param name="depName">部門名稱</param>
        /// <param name="billNo">單據編號</param>
        /// <param name="workNo">工號 </param>
        /// <param name="localName">姓名 </param>
        /// <param name="LVTypeCode">請假類別 </param>
        /// <param name="status">表單狀態 </param>
        /// <param name="testify">繳交證明 </param>
        /// <param name="startDate">開始日期</param>
        /// <param name="endDate">截止日期</param>
        /// <param name="applyStartDate">申請開始日期</param>
        /// <param name="applyEndDate">申請截止日期</param>
        /// <param name="applyType">申請類別</param>
        /// <param name="flag">是否查詢因班別變動導致請假時數有問題數據 </param>
        /// <param name="IsLastYear">是否補休 </param>
        /// <returns>請假信息</returns>
        public List<LeaveApplyViewModel> getApplyData(bool Privileged, string sqlDep, string depName, string billNo, string workNo, string localName, string LVTypeCode, string status, string testify, string startDate, string endDate, string applyStartDate, string applyEndDate, string applyType, bool flag, string IsLastYear)
        {
            string cmdText = "select * from gds_att_leaveapply_v where 1=1 ";
            if (depName.Length > 0)
            {
                if (Privileged)
                {
                    cmdText += " AND dCode IN ((" + sqlDep + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depname like '" + depName + "%' CONNECT BY PRIOR depcode = parentdepcode) ";
                }
                else
                {
                    cmdText += " AND dCode IN (SELECT DepCode FROM gds_sc_department START WITH depname like '" + depName + "%' CONNECT BY PRIOR depcode = parentdepcode) ";
                }
            }
            else
            {
                if (Privileged)
                {
                    cmdText += " AND dcode in (" + sqlDep + ")";
                }
            }
            if (billNo.Length > 0)
            {
                cmdText += " AND BillNo like '" + billNo + "%'";
            }
            if (workNo.Length > 0)
            {
                cmdText += " AND WorkNO like '" + workNo.ToUpper() + "%'";
            }
            if (localName.Length > 0)
            {
                cmdText += " AND LocalName like '" + localName + "%'";
            }
            if (LVTypeCode.Length > 0)
            {
                cmdText += " AND LVTypeCode = '" + LVTypeCode + "'";
            }
            if (status.Length > 0)
            {
                cmdText += " AND Status = '" + status + "'";
            }
            if (testify.Length > 0)
            {
                cmdText += " AND lvtypecode IN(SELECT lvtypecode FROM gds_att_leavetype WHERE istestify='Y')";
                if (testify.Equals("Y"))
                {
                    cmdText += " AND TestifyFile>' '";
                }
                else if (testify.Equals("N"))
                {
                    cmdText += " AND Testifyfile IS NULL";
                }
            }
            string StartDate = DateTime.Parse(startDate).ToString("yyyy/MM/dd");
            string EndDate = DateTime.Parse(endDate).AddDays(1).ToString("yyyy/MM/dd");
            cmdText += " AND ((to_date(to_char(StartDate,'yyyy/mm/dd')||StartTime,'yyyy/mm/dd hh24:mi') <= to_date('" + StartDate + " 07:00','yyyy/mm/dd hh24:mi') AND to_date(to_char(EndDate,'yyyy/mm/dd')||EndTime,'yyyy/mm/dd hh24:mi') >= to_date('" + StartDate + " 07:00','yyyy/mm/dd hh24:mi')) or " +
                             "(to_date(to_char(StartDate,'yyyy/mm/dd')||StartTime,'yyyy/mm/dd hh24:mi') <= to_date('" + EndDate + " 07:00','yyyy/mm/dd hh24:mi') AND to_date(to_char(EndDate,'yyyy/mm/dd')||EndTime,'yyyy/mm/dd hh24:mi') >= to_date('" + EndDate + " 07:00','yyyy/mm/dd hh24:mi')) or " +
                             "(to_date(to_char(StartDate,'yyyy/mm/dd')||StartTime,'yyyy/mm/dd hh24:mi') >= to_date('" + StartDate + " 07:00','yyyy/mm/dd hh24:mi') AND to_date(to_char(EndDate,'yyyy/mm/dd')||EndTime,'yyyy/mm/dd hh24:mi') <= to_date('" + EndDate + " 07:00','yyyy/mm/dd hh24:mi')))";
            if (startDate.Length > 0)
            {
                cmdText += " AND TRUNC(UPDATE_DATE) >= to_date('" + startDate + "','yyyy/mm/dd')";
            }
            if (endDate.Length > 0)
            {
                cmdText += " AND TRUNC(UPDATE_DATE) <= to_date('" + endDate + "','yyyy/mm/dd')";
            }
            if (applyType.Length > 0)
            {
                cmdText += " AND ApplyType = '" + applyType + "'";
            }
            if (flag)
            {
                cmdText += " AND lvtotal <> (select nvl(sum(lvtotal),0)from gds_att_leavedetail e where e.id=id)";
            }
            if (IsLastYear.Length > 0)
            {
                cmdText += " AND IsLastYear = '" + IsLastYear + "'";
            }
            DataTable dt = DalHelper.ExecuteQuery(cmdText);
            return OrmHelper.SetDataTableToList(dt);
        }
        /// <summary>
        /// 根據model獲取請假信息
        /// </summary>
        /// <param name="leaveApplyViewModel">請假信息model</param>
        /// <returns>請假信息</returns>
        public DataTable getLeaveApply(LeaveApplyViewModel leaveApplyViewModel)
        {
            return DalHelper.Select(leaveApplyViewModel);
        }
        /// <summary>
        /// 獲取個人請假信息，用於導出Excel
        /// </summary>
        /// <param name="user">請假人工號</param>
        /// <param name="billNo">申請單號</param>
        /// <param name="LVTypeCode">請假類別</param>
        /// <param name="status">表單狀態</param>
        /// <param name="startDate">開始日期</param>
        /// <param name="endDate">結束日期</param>
        /// <param name="applyType">申請類別</param>
        /// <returns>個人請假信息</returns>
        public List<LeaveApplyViewModel> getApplyList(string user, string billNo, string LVTypeCode, string status, string startDate, string endDate, string applyType)
        {
            string cmdText = "select * from gds_att_leaveapply_v where 1=1 ";

            cmdText += " and workno='" + user + "'";

            if (billNo.Length > 0)
            {
                cmdText += " AND BillNo like '" + billNo + "%'";
            }
            if (LVTypeCode.Length > 0)
            {
                cmdText += " AND LVTypeCode = '" + LVTypeCode + "'";
            }
            if (status.Length > 0)
            {
                cmdText += " AND Status = '" + status + "'";
            }
            string StartDate = "";
            string EndDate = "";
            if ((startDate.Length != 0) && (endDate.Length != 0))
            {
                StartDate = DateTime.Parse(startDate).ToString("yyyy/MM/dd");
                EndDate = DateTime.Parse(endDate).AddDays(1.0).ToString("yyyy/MM/dd");
                cmdText += " AND ((to_date(to_char(a.StartDate,'yyyy/mm/dd')||a.StartTime,'yyyy/mm/dd hh24:mi') <= to_date('" + StartDate + " 07:00','yyyy/mm/dd hh24:mi') AND to_date(to_char(a.EndDate,'yyyy/mm/dd')||a.EndTime,'yyyy/mm/dd hh24:mi') >= to_date('" + StartDate + " 07:00','yyyy/mm/dd hh24:mi')) or (to_date(to_char(a.StartDate,'yyyy/mm/dd')||a.StartTime,'yyyy/mm/dd hh24:mi') <= to_date('" + EndDate + " 07:00','yyyy/mm/dd hh24:mi') AND to_date(to_char(a.EndDate,'yyyy/mm/dd')||a.EndTime,'yyyy/mm/dd hh24:mi') >= to_date('" + EndDate + " 07:00','yyyy/mm/dd hh24:mi')) or (to_date(to_char(a.StartDate,'yyyy/mm/dd')||a.StartTime,'yyyy/mm/dd hh24:mi') >= to_date('" + StartDate + " 07:00','yyyy/mm/dd hh24:mi') AND to_date(to_char(a.EndDate,'yyyy/mm/dd')||a.EndTime,'yyyy/mm/dd hh24:mi') <= to_date('" + EndDate + " 07:00','yyyy/mm/dd hh24:mi')))";
            }

            if ((startDate.Length != 0) && (endDate.Length == 0))
            {
                StartDate = DateTime.Parse(startDate).ToString("yyyy/MM/dd");
                cmdText += " AND a.StartDate >= to_date('" + StartDate + "','yyyy/mm/dd')";
            }
            if ((startDate.Length == 0) && (endDate.Length != 0))
            {
                EndDate = DateTime.Parse(endDate).ToString("yyyy/MM/dd");
                cmdText += " AND a.StartDate >= to_date('" + EndDate + "','yyyy/mm/dd')";
            }
            //string StartDate = DateTime.Parse(startDate).ToString("yyyy/MM/dd");
            //string EndDate = DateTime.Parse(endDate).AddDays(1).ToString("yyyy/MM/dd");
            //cmdText += " AND ((to_date(to_char(StartDate,'yyyy/mm/dd')||StartTime,'yyyy/mm/dd hh24:mi') <= to_date('" + StartDate + " 07:00','yyyy/mm/dd hh24:mi') AND to_date(to_char(EndDate,'yyyy/mm/dd')||EndTime,'yyyy/mm/dd hh24:mi') >= to_date('" + StartDate + " 07:00','yyyy/mm/dd hh24:mi')) or " +
            //                 "(to_date(to_char(StartDate,'yyyy/mm/dd')||StartTime,'yyyy/mm/dd hh24:mi') <= to_date('" + EndDate + " 07:00','yyyy/mm/dd hh24:mi') AND to_date(to_char(EndDate,'yyyy/mm/dd')||EndTime,'yyyy/mm/dd hh24:mi') >= to_date('" + EndDate + " 07:00','yyyy/mm/dd hh24:mi')) or " +
            //                 "(to_date(to_char(StartDate,'yyyy/mm/dd')||StartTime,'yyyy/mm/dd hh24:mi') >= to_date('" + StartDate + " 07:00','yyyy/mm/dd hh24:mi') AND to_date(to_char(EndDate,'yyyy/mm/dd')||EndTime,'yyyy/mm/dd hh24:mi') <= to_date('" + EndDate + " 07:00','yyyy/mm/dd hh24:mi')))";
            //if (startDate.Length > 0)
            //{
            //    cmdText += " AND TRUNC(UPDATE_DATE) >= to_date('" + startDate + "','yyyy/mm/dd')";
            //}
            //if (endDate.Length > 0)
            //{
            //    cmdText += " AND TRUNC(UPDATE_DATE) <= to_date('" + endDate + "','yyyy/mm/dd')";
            //}
            if (applyType.Length > 0)
            {
                cmdText += " AND ApplyType = '" + applyType + "'";
            }
            DataTable dt= DalHelper.ExecuteQuery(cmdText);
            return OrmHelper.SetDataTableToList(dt);
        }
    }
}
