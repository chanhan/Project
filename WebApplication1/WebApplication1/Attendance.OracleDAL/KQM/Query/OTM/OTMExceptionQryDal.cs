/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OTMDetailQtyDal.cs
 * 檔功能描述： 加班實報查詢數據操作類
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
using GDSBG.MiABU.Attendance.Model.KQM.Query;

namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.Query.OTM
{
    public class OTMExceptionQryDal : DALBase<OTMExceptionApplyQryModel>, IOTMExceptionQryDal
    {

        /// <summary>
        /// 根據條件查詢
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DataTable GetDataByCondition(string condition)
        {
            DataTable dt = new DataTable();
            string cmdText = @"SELECT DataType,DataCode,DataValue,DataTypeDetail,(DataCode||'?B'||DataValue)as newDataValue FROM gds_att_TYPEDATA ";
            if (condition == "condition1")
            {
                cmdText += " WHERE DataType='OTType' ORDER BY OrderId ";
            }
            if (condition == "condition2")
            {
                cmdText += " WHERE DataType='ApproveFlag' ORDER BY OrderId ";
            }
            if (condition == "condition3")
            {
                cmdText += " WHERE DataType='DiffReason' ORDER BY OrderId ";
            }
            if (condition == "condition4")
            {
                cmdText += " WHERE DataType='OverTimeType' ORDER BY OrderId ";
            }


            dt = DalHelper.ExecuteQuery(cmdText);
            return dt;
        }


        /// <summary>
        /// 加班異常查詢
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
        public DataTable GetOTMExceptionQryList(OTMExceptionApplyQryModel model, string sqlDep, string dCode, string dateFrom, string dateTo, string hoursCondition, string hours, int pageIndex, int pageSize, out int totalCount)
        {
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"select * from gds_att_kaoqindata_exception_v a where 1=1 and a.RealType='E' ";
            cmdText = cmdText + strCon;
            if (!String.IsNullOrEmpty(dCode))
            {
                cmdText += " AND a.dCode   IN  ((" + sqlDep + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depname = :depCode CONNECT BY PRIOR depcode = parentdepcode) ";
                listPara.Add(new OracleParameter(":depCode", dCode));
            }
            else
            {
                cmdText += " AND a.dcode in (" + sqlDep + ")";
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

            
            DataTable dt = DalHelper.ExecutePagerQuery(cmdText, pageIndex, pageSize, out totalCount, listPara.ToArray());
            return dt;

        }

        /// <summary>
        /// 加班異常查詢
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
        public DataTable GetOTMExceptionQryList(OTMExceptionApplyQryModel model, string sqlDep, string dCode, string dateFrom, string dateTo, string hoursCondition, string hours)
        {
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"select * from gds_att_kaoqindata_exception_v a where 1=1 and a.RealType='E' ";
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



        /// <summary>
        /// 獲取ParaValue
        /// </summary>
        /// <returns></returns>
        public string GetValue(string flag, string strTempBeginTime, string strTempEndTime, string strMidTime, string strMidTime2)
        {
            DataTable dt = new DataTable();
            string value = "";

            if (flag == "condition1")
            {

                dt = DalHelper.ExecuteQuery(@"select to_date(:strTempEndTime,'yyyy/mm/dd hh24:mi:ss')-to_date(:strTempBeginTime,'yyyy/mm/dd hh24:mi:ss') from dual", new OracleParameter(":strTempBeginTime", strTempBeginTime), new OracleParameter(":strTempEndTime", strTempEndTime));
            }
            if (flag == "condition2")
            {

                dt = DalHelper.ExecuteQuery(@"select to_date(:strTempBeginTime,'yyyy/mm/dd hh24:mi:ss')-to_date(:strMidTime,'yyyy/mm/dd hh24:mi:ss') from dual", new OracleParameter(":strTempBeginTime", strTempBeginTime), new OracleParameter(":strMidTime", strMidTime));
            }
            if (flag == "condition3")
            {

                dt = DalHelper.ExecuteQuery(@"select to_date(:strTempEndTime,'yyyy/mm/dd hh24:mi:ss')-to_date(:strMidTime,'yyyy/mm/dd hh24:mi:ss') from dual", new OracleParameter(":strTempEndTime", strTempEndTime), new OracleParameter(":strMidTime", strMidTime));
            }
            if (flag == "condition4")
            {

                dt = DalHelper.ExecuteQuery(@"select to_date(:strTempBeginTime,'yyyy/mm/dd hh24:mi:ss')-to_date(strMidTime2,'yyyy/mm/dd hh24:mi:ss') from dual", new OracleParameter(":strTempBeginTime", strTempBeginTime), new OracleParameter(":strMidTime2", strMidTime2));
            }
            if (flag == "condition5")
            {
                dt = DalHelper.ExecuteQuery(@"select nvl(max(paravalue),'Y') from gds_sc_parameter where paraname='IsOTMHours10M'");
            }
            if (dt != null)
            {
                value = dt.Rows[0][0].ToString().Trim();
            }
            return value;
        }


        /// <summary>
        /// 獲取員工班別
        /// </summary>
        /// <param name="sWorkNo"></param>
        /// <param name="sDate"></param>
        /// <returns></returns>
        public string GetShiftNo(string workNo, string date)
        {
            string value = "";
            OracleParameter shiftno = new OracleParameter("v_shiftno", OracleType.VarChar, 20);
            shiftno.Direction = ParameterDirection.Output;
            int a = DalHelper.ExecuteNonQuery("prog_getempshiftno", CommandType.StoredProcedure,
                new OracleParameter("v_workno", workNo), new OracleParameter("v_date", date), shiftno);
            if (a > 0)
            {
                value = Convert.ToString(shiftno.Value);
            }
            return value;
        }

        /// <summary>
        /// 根據班別ShiftNo查詢信息
        /// </summary>
        /// <param name="strShiftNo"></param>
        /// <returns></returns>
        public DataTable GetDataTableBySQL(string strShiftNo)
        {
            DataTable dt = new DataTable();
            dt = DalHelper.ExecuteQuery(@"select OnDutyTime,OffDutyTime,AMRestSTime,AMRestETime,PMRestSTime,PMRestETime,ShiftType from gds_att_WorkShift where ShiftNo=:strShiftNo ", new OracleParameter(":strShiftNo", strShiftNo));
            return dt;
        }

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt">徐轉化的Datatable</param>
        /// <returns></returns>
        public List<OTMExceptionApplyQryModel> GetList(DataTable dt)
        {
            return OrmHelper.SetDataTableToList(dt);
        }


    }
}
