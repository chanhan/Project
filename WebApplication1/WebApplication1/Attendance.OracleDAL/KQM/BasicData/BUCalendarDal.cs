/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： BUCalendarDal.cs
 * 檔功能描述： BU行事歷數據操作類
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.12.20
 * 
 */

using System;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using System.Collections;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;


namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.BasicData
{
    public class BUCalendarDal : DALBase<BUCalendarModel>, IBUcalendarDal
    {
        /// <summary>
        /// 根據主鍵查詢Model
        /// <param name="model">異常原因model</param>
        /// <param name="orderType">排序</param>
        /// <param name="pageIndex">當前頁</param>
        /// <param name="pageSize"></param></param>
        /// <param name="totalCount">總頁數</param>
        /// <returns></returns>
        public DataTable GetBUCalendarList(BUCalendarModel model, string SQLDep,string depCode, int pageIndex, int pageSize, out int totalCount)
        {
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"SELECT bucode,  orgname, workday, weekno, weekday, workflag, holidayflag,COSTCODE, remark,update_user, update_date
                             FROM gds_att_bucalendar_v a  WHERE 1 = 1  ";
            cmdText = cmdText + strCon;
            cmdText = cmdText + "  AND exists (SELECT 1 FROM (" + SQLDep + ") e where e.DepCode=a.BUCODE)";
            if (!string.IsNullOrEmpty(depCode))
            {
                cmdText = cmdText + " AND bucode IN (SELECT depcode FROM gds_sc_department START WITH DEPCODE ='"+depCode+"' CONNECT BY PRIOR depcode = parentdepcode)";
            }
            cmdText = cmdText + " order by a.WORKDAY desc";
            DataTable dt = DalHelper.ExecutePagerQuery(cmdText, pageIndex, pageSize, out totalCount, listPara.ToArray());
            return dt;
        }
        /// <summary>
        /// 刪除一條BU行事歷
        /// </summary>
        /// <param name="functionId">要刪除的一條BU行事歷model</param>
        /// <returns>是否執行成功</returns>
        public int DeleteBUCalendarByKey(BUCalendarModel model, SynclogModel logmodel)
        {
            return DalHelper.Delete(model, logmodel);
        }
        /// <summary>
        /// 查詢BG行事歷中某天是不是節假日
        /// </summary>
        /// <param name="model">BU行事歷的model</param>
        /// <param name="CompanyId">公司ID</param>
        /// <returns></returns>
        public DataTable GetBGCalendarByKey(BUCalendarModel model, string companyId)
        {
            string cmdText = "";
            cmdText = @"SELECT holidayflag FROM gds_att_bgcalendar WHERE to_char(workday,'yyyy/mm/dd') =:workdate 
                    AND bgcode IN (SELECT depcode FROM gds_sc_department WHERE levelcode = '0' AND companyid = :companyId)";
            DataTable dt = DalHelper.ExecuteQuery(cmdText, new OracleParameter(":workdate", model.WorkDay.Value.ToString("yyyy/MM/dd")), new OracleParameter(":companyId", companyId));
            return dt;
        }
        /// <summary>
        /// 插入BU行事歷
        /// </summary>
        /// <param name="functionId">要插入的BU行事歷model</param>
        /// <returns>插入是否成功</returns>
        public int InsertBUCalendarByKey(BUCalendarModel model, SynclogModel logmodel)
        {
            return DalHelper.Insert(model, logmodel);
        }
        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        public int UpdateBUCalendarByKey(BUCalendarModel model, BUCalendarModel newmodel, SynclogModel logmodel)
        {
            return DalHelper.Update(model, newmodel, true, logmodel);
        }
        /// <summary>
        /// 根據主鍵查詢
        /// </summary>
        /// <param name="BUCode">單位代碼</param>
        /// <param name="workDay">日期</param>
        /// <returns>該行事歷的行數</returns>
        public DataTable GetBUCalendarNum(BUCalendarModel model)
        {
            return DalHelper.SelectByKey(model, true);
        }
        /// <summary>
        /// 導入
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="successnum"></param>
        /// <param name="errornum"></param>
        /// <returns></returns>
        public DataTable ImpoertExcel(string personcode, string companyId, out int successnum, out int errornum, SynclogModel logmodel)
        {
            OracleParameter outCursor = new OracleParameter("p_coursor", OracleType.Cursor);
            OracleParameter outSuccess = new OracleParameter("p_success", OracleType.Int32);
            OracleParameter outError = new OracleParameter("p_error", OracleType.Int32);
            outCursor.Direction = ParameterDirection.Output;
            outSuccess.Direction = ParameterDirection.Output;
            outError.Direction = ParameterDirection.Output;
            DataTable dt = DalHelper.ExecuteQuery("gds_att_bucalendar_vaildata", CommandType.StoredProcedure,
                new OracleParameter("p_personcode", personcode), new OracleParameter("p_companyid", companyId), outCursor, outSuccess, outError, new OracleParameter("p_transactiontype", logmodel.TransactionType), new OracleParameter("p_levelno", logmodel.LevelNo), new OracleParameter("p_fromhost", logmodel.FromHost),
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
        public List<BUCalendarModel> GetList(DataTable dt)
        {
            return OrmHelper.SetDataTableToList(dt);
        }
        /// <summary>
        /// 獲得選擇組織的層級
        /// </summary>
        /// <param name="depCode"></param>
        /// <returns></returns>
        public string GetValue(string depCode)
        {
            DataTable dt = DalHelper.ExecuteQuery("select levelcode from gds_sc_department where DepCode='" + depCode + "'");
            if (dt != null)
            {
                return Convert.ToString(dt.Rows[0]["levelcode"].ToString());
            }
            else
            {
                return "";
            }
        }
    }
}
