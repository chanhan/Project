/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OTMDetailQtyDal.cs
 * 檔功能描述： 加班預報查詢數據操作類
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.30
 * 
 */

using System;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Collections;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Model.KQM.Query.OTM;
using GDSBG.MiABU.Attendance.IDAL.KQM.Query.OTM;

namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.Query.OTM
{
    public class OTMDetailQryDal : DALBase<OTMAdvanceApplyQryModel>, IOTMDetailQryDal
    {

        /// <summary>
        /// 加班預報查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dCode"></param>
        /// <param name="batchEmployeeNo"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="hoursCondition"></param>
        /// <param name="hours"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetOTMAdvanceQryList(OTMAdvanceApplyQryModel model, string sqlDep, string dCode, string batchEmployeeNo, string dateFrom, string dateTo, string hoursCondition, string hours, int pageIndex, int pageSize, out int totalCount)
        {
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"select * from gds_att_otmadvanceapply_v a where 1=1 ";
            cmdText += strCon;

            if (!String.IsNullOrEmpty(dCode))
            {
                cmdText += " AND a.dCode   IN  ((" + sqlDep + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depname = :depCode CONNECT BY PRIOR depcode = parentdepcode) ";
                listPara.Add(new OracleParameter(":depCode", dCode));
            }
            else
            {
                cmdText += " AND a.dcode in (" + sqlDep + ")";
            }   

            if (!String.IsNullOrEmpty(batchEmployeeNo))
            {
                cmdText = cmdText + " and a.workno in (SELECT char_list  FROM TABLE (gds_sc_chartotable ('" + batchEmployeeNo + "','§')))";
            }

            if (!String.IsNullOrEmpty(dateFrom) && !String.IsNullOrEmpty(dateTo))
            {
                cmdText += " and (a.OTDATE between to_date(:dateFrom,'yyyy/mm/dd') and to_date(:dateTo,'yyyy/mm/dd')) ";
                listPara.Add(new OracleParameter(":dateFrom", dateFrom));
                listPara.Add(new OracleParameter(":dateTo", dateTo));
            }

            if (!String.IsNullOrEmpty(hoursCondition) && !String.IsNullOrEmpty(hours))
            {
                cmdText += " and  a.Hours "+hoursCondition+" :hours ";
                listPara.Add(new OracleParameter(":hours", hours));
            }
            DataTable dt = DalHelper.ExecutePagerQuery(cmdText, pageIndex, pageSize, out totalCount, listPara.ToArray());
            return dt;

        }

        /// <summary>
        /// 根據條件查詢
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DataTable GetDataByCondition(string condition)
        {
            DataTable dt = new DataTable();
            string cmdText = @"SELECT DataType,DataCode,DataValue,DataTypeDetail,(DataCode||'?B'||DataValue)as newDataValue FROM GDS_ATT_TYPEDATA ";
            if (condition == "condition1")
            {
                cmdText += " WHERE DataType='OTMAdvanceApplyStatus' ORDER BY OrderId ";
            }
            if (condition == "condition2")
            {
                cmdText += " WHERE DataType='OverTimeType' ORDER BY OrderId ";
            }


            dt = DalHelper.ExecuteQuery(cmdText);
            return dt;
        }

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt">徐轉化的Datatable</param>
        /// <returns></returns>
        public List<OTMAdvanceApplyQryModel> GetList(DataTable dt)
        {
            return OrmHelper.SetDataTableToList(dt);
        }

        /// <summary>
        /// 根據SQL語句查詢
        /// </summary>
        /// <param name="workNo"></param>
        /// <param name="oTdate"></param>
        /// <returns></returns>
        public DataTable GetDataTableBySQL(string workNo,string oTdate)
        {
            DataTable dt = new DataTable();
            string cmdText = @"select nvl(sum(Decode(OTType,'G1',ConfirmHours,0)),0)G1Total,
                        nvl(sum(Decode(OTType,'G2',ConfirmHours,0)),0)G2Total,
                        nvl(sum(Decode(OTType,'G3',ConfirmHours,0)),0)G3Total 
                        from gds_att_realApply where WorkNo=:workNo 
                        and otdate>last_day(add_months(to_date(:oTdate,'yyyy/MM/dd'),-1)) 
                        and OTDate<=to_date(:oTdate,'yyyy/MM/dd') and status<'3' ";
            dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":workNo", workNo), new OracleParameter(":oTdate", oTdate));
            return dt;
        }


        /// <summary>
        /// 加班預報查詢(不分頁)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dCode"></param>
        /// <param name="batchEmployeeNo"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="hoursCondition"></param>
        /// <param name="hours"></param>
        /// <returns></returns>
        public DataTable GetOTMAdvanceQryList(OTMAdvanceApplyQryModel model,string sqlDep, string dCode, string batchEmployeeNo, string dateFrom, string dateTo, string hoursCondition, string hours)
        {
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"select * from gds_att_otmadvanceapply_v a where 1=1 ";
            cmdText += strCon;

            if (!String.IsNullOrEmpty(dCode))
            {
                cmdText += " AND a.dCode   IN  ((" + sqlDep + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depname = :depCode CONNECT BY PRIOR depcode = parentdepcode) ";
                listPara.Add(new OracleParameter(":depCode", dCode));
            }
            else
            {
                cmdText += " AND a.dcode in (" + sqlDep + ")";
            }   

            if (!String.IsNullOrEmpty(batchEmployeeNo))
            {
                cmdText = cmdText + " and a.workno in (SELECT char_list  FROM TABLE (gds_sc_chartotable ('" + batchEmployeeNo + "','§')))";
            }

            if (!String.IsNullOrEmpty(dateFrom) && !String.IsNullOrEmpty(dateTo))
            {
                cmdText += " and (a.OTDATE between to_date(:dateFrom,'yyyy/mm/dd') and to_date(:dateTo,'yyyy/mm/dd')) ";
                listPara.Add(new OracleParameter(":dateFrom", dateFrom));
                listPara.Add(new OracleParameter(":dateTo", dateTo));
            }

            if (!String.IsNullOrEmpty(hoursCondition) && !String.IsNullOrEmpty(hours))
            {
                cmdText += " and  a.Hours " + hoursCondition + " :hours ";
                listPara.Add(new OracleParameter(":hours", hours));
            }
            DataTable dt = DalHelper.ExecuteQuery(cmdText, listPara.ToArray());
            return dt;

        }
    }
}
