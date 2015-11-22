/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： NetSignInSetDal.cs
 * 檔功能描述： 網上簽到名單設定數據操作類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.12
 * 
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.OracleDAL.KQM.BasicData
{
    public class NetSignInSetDal : DALBase<NetSignInSetModel>, INetSignInSetDal
    {
        /// <summary>
        /// 根據條件查詢數據
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetSignEmpPageInfo(NetSignInSetModel model,string sql, int pageIndex, int pageSize, out int totalCount)
        { 
            string strCon = "";
            string depName = "";
            if (!string.IsNullOrEmpty(model.DepName))
            {
                depName = model.DepName.ToString();
            }
            model.DepName = "";
            model.DepCode = "";
            string condition = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"select a.workno,a.flag,a.startdate,a.enddate,a.create_date,a.create_user,
                               a.update_date,a.update_user,a.startenddate,a.localname,a.depcode,a.depname,
                               a.flagname from gds_att_signemployee_v a where 1=1 ";
            if (!string.IsNullOrEmpty(depName))
            {
                condition = "AND a.depcode IN ((" + sql + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depname = :depName CONNECT BY PRIOR depcode = parentdepcode) ";
                listPara.Add(new OracleParameter(":depName", depName));
            }
            else
            {
                condition = " AND a.depcode IN (" + sql + ") ";
            }
            cmdText = cmdText + strCon+condition;
            return DalHelper.ExecutePagerQuery(cmdText, pageIndex, pageSize, out totalCount, listPara.ToArray());
        }

        /// <summary>
        /// 導出查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable GetSignEmpForExport(NetSignInSetModel model, string sql)
        {
            string strCon = "";
            string depName = "";
            if (!string.IsNullOrEmpty(model.DepName))
            {
                depName = model.DepName.ToString();
            }
            model.DepName = "";
            model.DepCode = "";
            string condition = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"select a.workno,a.flag,a.startdate,a.enddate,a.create_date,a.create_user,
                               a.update_date,a.update_user,a.startenddate,a.localname,a.depcode,a.depname,
                               a.flagname from gds_att_signemployee_v a where 1=1 ";
            if (!string.IsNullOrEmpty(depName))
            {
                condition = "AND a.depcode IN ((" + sql + ") INTERSECT SELECT DepCode FROM gds_sc_department START WITH depname = :depName CONNECT BY PRIOR depcode = parentdepcode) ";
                listPara.Add(new OracleParameter(":depName", depName));
            }
            else
            {
                condition = " AND a.depcode IN (" + sql + ") ";
            }
            cmdText = cmdText + strCon + condition;
            return DalHelper.ExecuteQuery(cmdText, listPara.ToArray());
        }

        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的功能Model</param>
        /// <returns>是否成功</returns>
        public bool AddNetSignInEmp(NetSignInSetModel model, SynclogModel logmodel)
        {
            return DalHelper.Insert(model,logmodel) != -1;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="oldmodel"></param>
        /// <param name="model"></param>
        /// <param name="logmodel"></param>
        /// <returns></returns>
        public bool UpdateNetSignInEmpByKey(NetSignInSetModel oldmodel,NetSignInSetModel model,SynclogModel logmodel)
        {
            return DalHelper.Update(oldmodel, model, true, logmodel) != -1; 
        }

        /// <summary>
        /// 根據主鍵查詢model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public NetSignInSetModel GetNetSignInEmpByKey(NetSignInSetModel model)
        {
            return DalHelper.SelectByKey(model);
        }

        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="workno"></param>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public DataTable GetNetSignInEmp(string workno, Nullable<DateTime> startdate, Nullable<DateTime> enddate)
        {
            string str = @"SELECT * FROM gds_att_signemployee WHERE workno = :workno
                 AND TRUNC (:startdate) <= TRUNC (enddate) and TRUNC (:enddate) >= TRUNC (startdate)";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":workno", workno), new OracleParameter(":startdate", startdate), new OracleParameter(":enddate", enddate));
        }

        /// <summary>
        /// 修改查詢
        /// </summary>
        /// <param name="workno"></param>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public DataTable GetNetSignInEmpForModify(string workno, Nullable<DateTime> startdate, Nullable<DateTime> enddate, Nullable<DateTime> oldstartdate, Nullable<DateTime> oldenddate)
        {
            string str = @"SELECT *
                          FROM (SELECT *
                                  FROM gds_att_signemployee
                                 WHERE (workno, TRUNC (startdate), TRUNC (enddate)) NOT IN (
                                          SELECT workno, TRUNC (startdate), TRUNC (enddate)
                                            FROM gds_att_signemployee
                                           WHERE workno = :workno
                                             AND TRUNC (:oldstartdate) = TRUNC (startdate)
                                             AND TRUNC (:oldenddate) = TRUNC (enddate)))
                         WHERE workno = :workno
                           AND TRUNC (:startdate) <= TRUNC (enddate)
                           AND TRUNC (:enddate) >= TRUNC (startdate)  ";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":workno", workno), new OracleParameter(":startdate", startdate), new OracleParameter(":enddate", enddate), new OracleParameter(":oldstartdate", oldstartdate), new OracleParameter(":oldenddate", oldenddate));
        }

        /// <summary>
        /// 根據工號是否存在
        /// </summary>
        /// <param name="workno"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DataTable GetWorkNoInfoByUser(string workno, string sql)
        {
            string str = "select count(*) from gds_att_employee where workno='" + workno + "'";
            string condition = " and depcode in (" + sql + ")";
            str += condition;
            return DalHelper.ExecuteQuery(str);
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="workno">工號</param>
        /// <returns></returns>
        public int DeleteNetSignInEmployee(string workno, string startenddate, SynclogModel logmodel)
        {
            //string str = " DELETE FROM gds_att_signemployee WHERE WorkNo=:workno";
            string str = "DELETE FROM gds_att_signemployee WHERE WorkNo=:workno and TO_CHAR (startdate, 'yyyy/MM/dd')|| '~'|| TO_CHAR (enddate, 'yyyy/MM/dd') =:startenddate";
            return DalHelper.ExecuteNonQuery(str,logmodel, new OracleParameter(":workno", workno), new OracleParameter(":startenddate", startenddate));
        }

        /// <summary>
        /// 獲得當前用戶在臨時表中創建的所有數據
        /// </summary>
        /// <param name="createuser"></param>
        /// <returns></returns>
        public DataTable GetAllTempNetSignInEmp(string createuser)
        {
            string str = "select workno,localname,startdate,enddate from gds_att_temp_signemp where create_user=:createuser";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":createuser", createuser));
        }


        /// <summary>
        /// 導入
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="successnum"></param>
        /// <param name="errornum"></param>
        /// <returns></returns>
        public DataTable ImportExcel(string personcode, string modulecode, out int successnum, out int errornum, SynclogModel logmodel)
        {
            OracleParameter outCursor = new OracleParameter("p_coursor", OracleType.Cursor);
            OracleParameter outSuccess = new OracleParameter("p_success", OracleType.Int32);
            OracleParameter outError = new OracleParameter("p_error", OracleType.Int32);
            outCursor.Direction = ParameterDirection.Output;
            outSuccess.Direction = ParameterDirection.Output;
            outError.Direction = ParameterDirection.Output;
            DataTable dt = DalHelper.ExecuteQuery("gds_sc_signemp_vaildata", CommandType.StoredProcedure,
                new OracleParameter("p_personcode", personcode), new OracleParameter("p_modulecode", modulecode), outCursor, outSuccess, outError,
                new OracleParameter("p_transactiontype", logmodel.TransactionType == null ? "" : logmodel.TransactionType.ToString()),
                new OracleParameter("p_levelno", logmodel.LevelNo == null ? "" : logmodel.LevelNo.ToString()),
                new OracleParameter("p_fromhost", logmodel.FromHost == null ? "" : logmodel.FromHost.ToString()),
                new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()),
                new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()),
                new OracleParameter("p_processflag", logmodel.ProcessFlag == null ? "" : logmodel.ProcessFlag.ToString()),
                new OracleParameter("p_processowner", logmodel.ProcessOwner == null ? "" : logmodel.ProcessOwner.ToString()));
            successnum = Convert.ToInt32(outSuccess.Value);
            errornum = Convert.ToInt32(outError.Value);

            return dt;
        }

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<NetSignInSetModel> GetList(DataTable dt)
        {
            return OrmHelper.SetDataTableToList(dt);
        }
    }
}
