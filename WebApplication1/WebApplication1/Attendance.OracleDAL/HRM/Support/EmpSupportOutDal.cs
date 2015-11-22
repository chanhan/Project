/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： EmpSupportOutDal.cs
 * 檔功能描述： 外部支援數據操作類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2012.01.04
 * 
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.HRM.Support;
using GDSBG.MiABU.Attendance.Model.HRM.Support;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.OracleDAL.HRM.Support
{
    public class EmpSupportOutDal : DALBase<EmpSupportOutModel>, IEmpSupportOutDal
    {
        /// <summary>
        /// 根據條件分頁查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="LevelCondition"></param>
        /// <param name="ManagerCondition"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetEmpSupportOutPageInfo(EmpSupportOutModel model, string sql, string SupportDept, string LevelCondition, string ManagerCondition, int pageIndex, int pageSize, out int totalCount)
        {
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"select a.WORKNO, to_char(a.SUPPORTORDER) SUPPORTORDER, a.LOCALNAME, a.SEX, a.LEVELCODE,
                              a.MANAGERCODE, a.DEPNAME, a.ISKAOQIN, a.OVERTIMETYPE, a.SUPPORTDEPT,
                              a.STARTDATE, a.PREPENDDATE, a.ENDDATE, a.REMARK, a.STATE, a.BYNAME,
                              a.CARDNO, a.JOINDATE, a.MARRYSTATE, a.IDENTITYNO, a.TECHNICALCODE,
                              a.PROFESSTIONALCODE, a.TEAMCODE, a.NOTES, a.PASSWD, a.UPDATE_USER,
                              a.UPDATE_DATE, a.CREATE_DATE, a.CREATE_USER, a.SUPPORTDEPTNAME,
                              a.LEVELNAME, a.MANAGERNAME, a.OVERTIMETYPENAME, a.STATENAME,
                              a.SEXNAME from gds_att_empsupportout_v a where 1=1 ";
            string condition = " and a.SUPPORTDEPT in (" + sql + ")";
            cmdText = cmdText + strCon + condition;
            if (!string.IsNullOrEmpty(SupportDept))
            {
                cmdText = cmdText + "and a.SUPPORTDEPT in (SELECT DepCode FROM gds_sc_department START WITH depname LIKE '" + SupportDept + "%' CONNECT BY PRIOR depcode = parentdepcode )";
            }
            if (!string.IsNullOrEmpty(LevelCondition))
            {
                cmdText = cmdText + " and a.levelcode in (" + LevelCondition + ")";
            }
            if (!string.IsNullOrEmpty(ManagerCondition))
            {
                cmdText = cmdText + " and a.managercode in (" + ManagerCondition + ")";
            }
            return DalHelper.ExecutePagerQuery(cmdText, pageIndex, pageSize, out totalCount, listPara.ToArray());
        }

        /// <summary>
        /// 導出查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sql"></param>
        /// <param name="SupportDept"></param>
        /// <param name="LevelCondition"></param>
        /// <param name="ManagerCondition"></param>
        /// <returns></returns>
        public DataTable GetEmpSupportOutForExport(EmpSupportOutModel model, string sql, string SupportDept, string LevelCondition, string ManagerCondition)
        {
            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "a", out strCon);
            string cmdText = @"select a.WORKNO, to_char(a.SUPPORTORDER) SUPPORTORDER, a.LOCALNAME, a.SEX, a.LEVELCODE,
                              a.MANAGERCODE, a.DEPNAME, a.ISKAOQIN, a.OVERTIMETYPE, a.SUPPORTDEPT,
                              a.STARTDATE, a.PREPENDDATE, a.ENDDATE, a.REMARK, a.STATE, a.BYNAME,
                              a.CARDNO, a.JOINDATE, a.MARRYSTATE, a.IDENTITYNO, a.TECHNICALCODE,
                              a.PROFESSTIONALCODE, a.TEAMCODE, a.NOTES, a.PASSWD, a.UPDATE_USER,
                              a.UPDATE_DATE, a.CREATE_DATE, a.CREATE_USER, a.SUPPORTDEPTNAME,
                              a.LEVELNAME, a.MANAGERNAME, a.OVERTIMETYPENAME, a.STATENAME,
                              a.SEXNAME from gds_att_empsupportout_v a where 1=1 ";
            string condition = " and a.SUPPORTDEPT in (" + sql + ")";
            cmdText = cmdText + strCon + condition;
            if (!string.IsNullOrEmpty(SupportDept))
            {
                cmdText = cmdText + "and a.SUPPORTDEPT in (SELECT DepCode FROM gds_sc_department START WITH depname LIKE '" + SupportDept + "%' CONNECT BY PRIOR depcode = parentdepcode )";
            }
            if (!string.IsNullOrEmpty(LevelCondition))
            {
                cmdText = cmdText + " and a.levelcode in (" + LevelCondition + ")";
            }
            if (!string.IsNullOrEmpty(ManagerCondition))
            {
                cmdText = cmdText + " and a.managercode in (" + ManagerCondition + ")";
            }
            return DalHelper.ExecuteQuery(cmdText,listPara.ToArray());
        }

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<EmpSupportOutModel> GetList(DataTable dt)
        {
            return OrmHelper.SetDataTableToList(dt);
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int DeleteEmpSupportOut(EmpSupportOutModel model, SynclogModel logmodel)
        {
            return DalHelper.Delete(model,logmodel);
        }

        /// <summary>
        /// 根據主鍵查詢
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public EmpSupportOutModel GetEmpSupportOutInfo(EmpSupportOutModel model)
        {
            return DalHelper.SelectByKey(model);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddEmpSupportOut(EmpSupportOutModel model, SynclogModel logmodel)
        {
            return DalHelper.Insert(model,logmodel) != -1;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateEmpSupportOut(EmpSupportOutModel model, SynclogModel logmodel)
        {
            return DalHelper.UpdateByKey(model, true,logmodel) != -1;
        }

        /// <summary>
        /// 獲得支援中員工
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        public DataTable GetEmpSupportOut(string workno)
        {
            string str = "select workno from gds_att_EmpSupportOut where workno=:workno and State='0'";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":workno", workno));
        }

        /// <summary>
        /// 獲得員工的支援順序號
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        public DataTable GetEmpsupportOrder(string workno)
        {
            string str = "select NVL(max(SUPPORTORDER),0)+1 from gds_att_EmpSupportOut where WORKNO=:WORKNO";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":workno", workno));
        }

        /// <summary>
        /// 由工號和支援序號查詢工號
        /// </summary>
        /// <param name="workno"></param>
        /// <param name="supportorder"></param>
        /// <returns></returns>
        public DataTable GetEmpSupportByWorkNoAndOrder(string workno, string supportorder)
        {
            string str = "select workno from gds_att_EmpSupportOut where workno=:workno and State='0' AND SUPPORTORDER ! = :SUPPORTORDER";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":workno", workno), new OracleParameter(":SUPPORTORDER", supportorder));
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
            DataTable dt = DalHelper.ExecuteQuery("gds_att_empsupportout_vaildata", CommandType.StoredProcedure,
                new OracleParameter("p_personcode", personcode), outCursor, outSuccess, outError,
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
    }
}
