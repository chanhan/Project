/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMEvectionQryFormDal.cs
 * 檔功能描述： 出差明細查詢數據操作類
 * 
 * 版本：1.0
 * 創建標識： 陳函 2011.11.30
 * 
 */
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.KQM.Query;
using GDSBG.MiABU.Attendance.Model.KQM.Query;

namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.Query
{
    public class KQMEvectionQryFormDal : DALBase<EvectionApplyVModel>, IKQMEvectionQryFormDal
    {
        /// <summary>
        /// 查詢數據綁定DropDownList
        /// </summary>
        /// <param name="dataType">參數類型</param>
        /// <returns>查詢結果DataTable</returns>
        public DataTable getData(string dataType)
        {
            string cmdText = "SELECT DataType,DataCode,DataValue,DataTypeDetail,(DataCode||'?B'||DataValue)as newDataValue FROM gds_att_TYPEDATA where  DataType=:p_dataType ORDER BY OrderId";
            return DalHelper.ExecuteQuery(cmdText, new OracleParameter(":p_dataType", dataType));
        }

        /// <summary>
        /// 查詢出差明細
        /// </summary>
        /// <param name="Privileged">是否有組織權限</param>
        /// <param name="depName">部門名稱</param>
        /// <param name="Billno">申請單號</param>
        /// <param name="Workno">工號</param>
        /// <param name="Localname">姓名</param>
        /// <param name="Evectiontype">出差類別</param>
        /// <param name="Status">表單狀態</param>
        /// <param name="StartDate">出差日期(起)</param>
        /// <param name="EndDate">出差日期(迄)</param>
        /// <param name="Evectionaddress">出差地址</param>
        /// <param name="PageIndex">分頁索引</param>
        /// <param name="PageSize">每頁顯示的記錄數</param>
        /// <param name="totalCount">總記錄數</param>
        /// <returns>查詢結果DataTable</returns>
        public DataTable SelectEvectionData(bool Privileged, string sqlDep, string depName, string Billno, string Workno, string Localname, string Evectiontype, string Status, string StartDate, string EndDate, string Evectionaddress, int PageIndex, int PageSize, out int totalCount)
        {
            string cmdText = "select * from gds_att_evectionapply_v  a where 1=1";
            //   string sqlDep = "";
            //if (moduleCode.Length > 0)
            //{
            //    sqlDep = "SELECT depcode FROM gds_sc_persondept where personcode='" + personCode + "' and modulecode='" + moduleCode + "' and companyid='" + companID + "'";
            //}
            //else
            //{
            //    sqlDep = "SELECT 1 FROM dual";
            //}

            if (depName.Length > 0)
            {
                if (Privileged)
                {

                    cmdText += " AND a.dCode IN ((" + sqlDep + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depname ='" + depName + "' CONNECT BY PRIOR depcode = parentdepcode)";
                }
                else
                {
                    cmdText += " AND a.dCode IN (SELECT DepCode FROM gds_sc_department START WITH depname ='" + depName + "'CONNECT BY PRIOR depcode = parentdepcode)";
                }
            }
            else if (Privileged)
            {
                cmdText += " and a.dcode in (" + sqlDep + ")";
            }
            if (Billno.Length > 0)
            {
                cmdText += "AND a.BillNo like '%" + Billno + "%'";
            }
            if (Workno.Length > 0)
            {
                cmdText += " AND a.WorkNO like '%" + Workno + "%'";
            }

            if (Localname.Length > 0)
            {
                cmdText += " AND a.LocalName like '%" + Localname + "%'";
            }
            if (Evectiontype.Length > 0)
            {
                cmdText += " AND a.EvectionType = '" + Evectiontype + "'";
            }
            if (Status.Length > 0)
            {
                cmdText += " AND a.Status = '" + Status + "'";
            }

            if ((StartDate.Length != 0) && (EndDate.Length != 0))
            {

                cmdText += " AND ((a.StartDate <= to_date('" + StartDate + "','mm/dd/yyyy') AND a.EndDate >= to_date('" + StartDate + "','mm/dd/yyyy')) or (a.StartDate <= to_date('" + EndDate + "','mm/dd/yyyy') AND a.EndDate >= to_date('" + EndDate + "','mm/dd/yyyy')) or (a.StartDate >= to_date('" + StartDate + "','mm/dd/yyyy') AND a.EndDate <= to_date('" + EndDate + "','mm/dd/yyyy')))";
            }
            if (Evectionaddress.Length != 0)
            {
                cmdText += " AND a.EvectionAddress = '" + Evectionaddress + "'";
            }

            DataTable dt = DalHelper.ExecutePagerQuery(cmdText, PageIndex, PageSize, out totalCount);
            return dt;
        }

        /// <summary>
        /// 查詢出差明細，用於導出Excel
        /// </summary>
        /// <param name="Privileged">是否有組織權限</param>
        /// <param name="depName">部門名稱</param>
        /// <param name="Billno">申請單號</param>
        /// <param name="Workno">工號</param>
        /// <param name="Localname">姓名</param>
        /// <param name="Evectiontype">出差類別</param>
        /// <param name="Status">表單狀態</param>
        /// <param name="StartDate">出差日期(起)</param>
        /// <param name="EndDate">出差日期(迄)</param>
        /// <param name="Evectionaddress">出差地址</param>
        /// <returns>查詢結果List</returns>
        public List<EvectionApplyVModel> SelectEvectionData(bool Privileged, string sqlDep, string depName, string Billno, string Workno, string Localname, string Evectiontype, string Status, string StartDate, string EndDate, string Evectionaddress)
        {
            string cmdText = "select * from gds_att_evectionapply_v  a where 1=1";
            // string sqlDep = "";
            //if (moduleCode.Length > 0)
            //{
            //    sqlDep = "SELECT depcode FROM gds_sc_persondept where personcode='" + personCode + "' and modulecode='" + moduleCode + "' and companyid='" + companID + "'";
            //}
            //else
            //{
            //    sqlDep = "SELECT 1 FROM dual";
            //}

            if (depName.Length > 0)
            {
                if (Privileged)
                {

                    cmdText += " AND a.dCode IN ((" + sqlDep + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depname ='" + depName + "' CONNECT BY PRIOR depcode = parentdepcode)";
                }
                else
                {
                    cmdText += " AND a.dCode IN (SELECT DepCode FROM gds_sc_department START WITH depname ='" + depName + "'CONNECT BY PRIOR depcode = parentdepcode)";
                }
            }
            else if (Privileged)
            {
                cmdText += " and a.dcode in (" + sqlDep + ")";
            }
            if (Billno.Length > 0)
            {
                cmdText += "AND a.BillNo like '%" + Billno + "%'";
            }
            if (Workno.Length > 0)
            {
                cmdText += " AND a.WorkNO like '%" + Workno + "%'";
            }

            if (Localname.Length > 0)
            {
                cmdText += " AND a.LocalName like '%" + Localname + "%'";
            }
            if (Evectiontype.Length > 0)
            {
                cmdText += " AND a.EvectionType = '" + Evectiontype + "'";
            }
            if (Status.Length > 0)
            {
                cmdText += " AND a.Status = '" + Status + "'";
            }

            if ((StartDate.Length != 0) && (EndDate.Length != 0))
            {
                cmdText += " AND ((a.StartDate <= to_date('" + StartDate + "','mm/dd/yyyy') AND a.EndDate >= to_date('" + StartDate + "','mm/dd/yyyy')) or (a.StartDate <= to_date('" + EndDate + "','mm/dd/yyyy') AND a.EndDate >= to_date('" + EndDate + "','mm/dd/yyyy')) or (a.StartDate >= to_date('" + StartDate + "','mm/dd/yyyy') AND a.EndDate <= to_date('" + EndDate + "','mm/dd/yyyy')))";
            }
            if (Evectionaddress.Length != 0)
            {
                cmdText += " AND a.EvectionAddress = '" + Evectionaddress + "'";
            }

            DataTable dt = DalHelper.ExecuteQuery(cmdText);
            return OrmHelper.SetDataTableToList(dt);
        }

       //public DataTable SelectEvectionData(bool Privileged, string sqlDep, EvectionApplyVModel evectionApplyVModel, int currentPageIndex, int pageSize, out int totalCount)
       // {
       //     string strCon = "";
       //     List<OracleParameter> listPara = DalHelper.CreateConditionParameters(evectionApplyVModel, true, "a", out strCon);
       //     string cmdText = @"select * from gds_att_evectionapply_v  a where 1=1";
       //     if (Privileged)
       //     {
       //         cmdText += " and a.dcode in (" + sqlDep + ")";
       //     }
       //     cmdText = cmdText + strCon;
       //     return DalHelper.ExecutePagerQuery(cmdText, currentPageIndex, pageSize, out  totalCount, listPara.ToArray());
       // }
    }
}
