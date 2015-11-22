/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMAbsentMonthDal.cs
 * 檔功能描述： 缺勤統計報表數據訪問層
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.30
 * 
 */

using System;
using GDSBG.MiABU.Attendance.Common.Attributes;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.IDAL.Hr.KQM.Query;
using GDSBG.MiABU.Attendance.Model.Hr.PCM;
using System.Data.OracleClient;
namespace GDSBG.MiABU.Attendance.OracleDAL.Hr.KQM.Query
{
    public class KQMAbsentMonthDal : DALBase<KaoQinDataModel>,  IKQMAbsentMonthDal
    {
        /// <summary>
        /// 異常原因類別
        /// </summary>
        /// <returns></returns>
        public DataTable GetExceptReason()
        {
            string str = "SELECT * FROM gds_att_EXCEPTREASON WHERE REASONTYPE='MAKEUP' AND EFFECTFLAG='Y'";
            return DalHelper.ExecuteQuery(str);
        }

        /// <summary>
        /// 廠區
        /// </summary>
        /// <returns></returns>
        public DataTable GetAreaCode()
        {
            string str = "select singlename,AreaCode from gds_att_AREACODE where deleted='N' order by orderid";
            return DalHelper.ExecuteQuery(str);
        }

        /// <summary>
        /// 獲得Paravalue
        /// </summary>
        /// <returns></returns>
        public DataTable GetParavalue()
        {
            string str = "select paravalue from gds_sc_parameter where paraname='AreaCode'";
            return DalHelper.ExecuteQuery(str);
        }

        /// <summary>
        /// 驗證日期
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public DataTable CheckDateMonths(string StartDate, string EndDate)
        {
            string str = "select floor(MONTHS_BETWEEN(to_date('" + EndDate + "','yyyy/MM/dd'),to_date('" + StartDate + "','yyyy/MM/dd'))) sDays from dual";
            return DalHelper.ExecuteQuery(str);
        }

        /// <summary>
        /// 驗證出勤日報表是否有數據
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="successnum"></param>
        /// <param name="errornum"></param>
        /// <returns></returns>
        public int KaoQinDay_val(string kqdate, string personcode, string modulecode, string companyid, string depcode)
        {
            OracleParameter outCursor = new OracleParameter("p_coursor", OracleType.Int32);
            outCursor.Direction = ParameterDirection.Output;
            int flag = DalHelper.ExecuteNonQuery("kaoqinday_pro", CommandType.StoredProcedure,
                new OracleParameter("p_kqdate", kqdate), new OracleParameter("p_personcode", personcode), new OracleParameter("p_modulecode", modulecode), new OracleParameter("p_companyid", companyid), new OracleParameter("p_depcode", depcode), outCursor);
            return flag = Convert.ToInt32(outCursor.Value); ;
        }

        /// <summary>
        /// 驗證缺勤統計報表是否有數據
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="successnum"></param>
        /// <param name="errornum"></param>
        /// <returns></returns>
        public int AbsentMonth_val(string personcode, string modulecode, string companyid, string depcode, string startdate, string enddate, string workno)
        {
            OracleParameter outCursor = new OracleParameter("p_coursor", OracleType.Int32);
            outCursor.Direction = ParameterDirection.Output;
            int flag = DalHelper.ExecuteNonQuery("AbsentMonth_pro", CommandType.StoredProcedure,
                new OracleParameter("p_personcode", personcode), new OracleParameter("p_modulecode", modulecode), new OracleParameter("p_companyid", companyid),
                new OracleParameter("p_depcode", depcode), new OracleParameter("p_startdate", startdate), new OracleParameter("p_enddate", enddate), new OracleParameter("p_workno", workno), outCursor);
            return flag = Convert.ToInt32(outCursor.Value); ;
        }

    }
}
