/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMLeaveYearImportDal.cs
 * 檔功能描述： 已休年假數據操作類
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.12.23
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
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.AttendanceData
{
    public class KQMLeaveYearImportDal : DALBase<KQMLeaveYearImportModel>, IKQMLeaveYearImportDal
    {
        /// <summary>
        /// 根據主鍵查詢Model
        /// <param name="model">異常原因model</param>
        /// <param name="orderType">排序</param>
        /// <param name="pageIndex">當前頁</param>
        /// <param name="pageSize"></param></param>
        /// <param name="totalCount">總頁數</param>
        /// <returns></returns>
        public DataTable GetLeaveDaysList(KQMLeaveYearImportModel model, string SQLDep, string depCode, int pageIndex, int pageSize, out int totalCount)
        {
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"SELECT workno, leaveyear, leavedays, create_user, create_date, update_user,update_date, localname, dname, 
                               depcode, buname, errormsg FROM gds_att_leaveyearimport_v a WHERE 1 = 1 ";
            cmdText += strCon;
            if (!string.IsNullOrEmpty(depCode))
            {
                cmdText = cmdText + " AND a.depcode IN ((" + SQLDep + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depcode = '" + depCode + "' CONNECT BY PRIOR depcode = parentdepcode) ";
            }
            else
            {
                cmdText = cmdText + " AND a.depcode in (" + SQLDep + ")";
            }
            DataTable dt = DalHelper.ExecutePagerQuery(cmdText, pageIndex, pageSize, out totalCount, listPara.ToArray());
            return dt;
        }
        /// <summary>
        /// 根據主鍵查詢Model
        /// <param name="model">異常原因model</param>
        /// <param name="orderType">排序</param>
        /// <param name="pageIndex">當前頁</param>
        /// <param name="pageSize"></param></param>
        /// <param name="totalCount">總頁數</param>
        /// <returns></returns>
        public DataTable GetLeaveDaysList(KQMLeaveYearImportModel model, string SQLDep, string depCode)
        {
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"SELECT workno, leaveyear, leavedays, create_user, create_date, update_user,update_date, localname, dname, 
                               depcode, buname, errormsg FROM gds_att_leaveyearimport_v a WHERE 1 = 1 ";
            cmdText += strCon;
            if (!string.IsNullOrEmpty(depCode))
            {
                cmdText = cmdText + " AND a.depcode IN ((" + SQLDep + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depcode = '" + depCode + "' CONNECT BY PRIOR depcode = parentdepcode) ";
            }
            else
            {
                cmdText = cmdText + " AND a.depcode in (" + SQLDep + ")";
            }
            DataTable dt = DalHelper.ExecuteQuery(cmdText, listPara.ToArray());
            return dt;
        }
        /// <summary>
        /// 刪除一天休假記錄
        /// </summary>
        /// <param name="functionId">刪除一天休假記錄model</param>
        /// <returns>是否執行成功</returns>
        public int DeleteLeaveYearByKey(KQMLeaveYearImportModel model, SynclogModel logmodel)
        {
            return DalHelper.Delete(model, logmodel);
        }
        /// <summary>
        /// 導入
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="successnum"></param>
        /// <param name="errornum"></param>
        /// <returns></returns>
        public DataTable ImpoertExcel(string personcode, out int successnum, out int errornum, SynclogModel logmodel)
        {
            OracleParameter outCursor = new OracleParameter("p_coursor", OracleType.Cursor);
            OracleParameter outSuccess = new OracleParameter("p_success", OracleType.Int32);
            OracleParameter outError = new OracleParameter("p_error", OracleType.Int32);
            outCursor.Direction = ParameterDirection.Output;
            outSuccess.Direction = ParameterDirection.Output;
            outError.Direction = ParameterDirection.Output;
            DataTable dt = DalHelper.ExecuteQuery("gds_leaveyearimport_vaildata", CommandType.StoredProcedure,
                new OracleParameter("p_personcode", personcode), outCursor, outSuccess, outError, new OracleParameter("p_transactiontype", logmodel.TransactionType), new OracleParameter("p_levelno", logmodel.LevelNo), new OracleParameter("p_fromhost", logmodel.FromHost),
                new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()), new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()), new OracleParameter("p_processflag", logmodel.ProcessFlag),
                new OracleParameter("p_processowner", logmodel.ProcessOwner));
            successnum = Convert.ToInt32(outSuccess.Value);
            errornum = Convert.ToInt32(outError.Value);
            return dt;
        }
        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<KQMLeaveYearImportModel> GetList(DataTable dt)
        {
            return OrmHelper.SetDataTableToList(dt);
        }
    }
}
