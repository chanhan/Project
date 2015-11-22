/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OrgmanagerDal.cs
 * 檔功能描述： 組織管理者資料數據操作類
 * 
 * 版本：1.0
 * 創建標識： 昝望 2011.12.12
 * 
 */

using System;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Collections;
using GDSBG.MiABU.Attendance.Model.HRM;
using GDSBG.MiABU.Attendance.IDAL.HRM;
using System.Collections.Generic;

namespace GDSBG.MiABU.Attendance.OracleDAL.HRM
{
    public class OrgmanagerDal : DALBase<OrgmanagerModel>, IOrgmanagerDal
    {
        /// <summary>
        /// 查詢組織管理者資料
        /// </summary>
        /// <param name="model">給出主鍵值的Model</param>
        /// <returns>返回對應主鍵的Model</returns>
        public DataTable GetOrgmanager(OrgmanagerModel model, int pageIndex, int pageSize, out int totalCount, string sqlDep)
        {

            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "", out strCon);
            string cmdText = @"select * from GDS_ATT_ORGMANAGER_V where DepCode IN (" + sqlDep + ")";
            cmdText = cmdText + strCon;
            DataTable dt = DalHelper.ExecutePagerQuery(cmdText, pageIndex, pageSize, out  totalCount, listPara.ToArray());
            return dt;
        }


        /// <summary>
        /// 查詢組織管理者資料(用於Ajax)
        /// </summary>
        /// <param name="model">給出主鍵值的Model</param>
        /// <returns>返回對應主鍵的Model</returns>
        public DataTable GetOrgmanager(OrgmanagerModel model)
        {

            string strCon = "";
            List<OracleParameter> listPara = DalHelper.CreateConditionParameters(model, true, "", out strCon);
            string cmdText = @"select * from GDS_ATT_ORGMANAGER_V where DepCode IN ( SELECT aa.depcode FROM gds_sc_persondept aa
                          WHERE aa.personcode = 'USERATT' AND aa.modulecode = 'HRMSYS140' AND EXISTS (  SELECT 1  FROM gds_sc_personcompany bb
                          WHERE bb.personcode = 'USERATT' AND aa.companyid = bb.companyid))";
            cmdText = cmdText + strCon;
            DataTable dt = DalHelper.ExecuteQuery(cmdText,listPara.ToArray());
            return dt;
        }

        /// <summary>
        /// 新增組織管理者
        /// </summary>
        /// <param name="model">要新增的組織管理者Model</param>
        /// <returns>是否成功</returns>
        public int AddOrgmanager(OrgmanagerModel model, SynclogModel logmodel)
        {
            OracleParameter outPara = new OracleParameter("p_flag", OracleType.Int32);
            outPara.Direction = ParameterDirection.Output;
            int i = DalHelper.ExecuteNonQuery("insert_orgmanager_p", CommandType.StoredProcedure,
                new OracleParameter("p_depcode", model.DepCode), new OracleParameter("p_depname", model.DepName),
                new OracleParameter("p_workno", model.WorkNo), new OracleParameter("p_localname", model.LocalName),
                new OracleParameter("p_manager", model.Manager), new OracleParameter("p_managername", model.ManagerName),
                new OracleParameter("p_notes", model.Notes), new OracleParameter("p_deputy", model.Deputy == null ? "" : model.Deputy),
                new OracleParameter("p_deputyname", model.DeputyName == null ? "" : model.DeputyName), new OracleParameter("p_deputynotes", model.DeputyNotes == null ? "" : model.DeputyNotes),
                new OracleParameter("p_isdirectlyunder", model.IsDirectlyUnder), new OracleParameter("p_isbgaudit", model.IsBGAudit), outPara,
                new OracleParameter("p_transactiontype", logmodel.TransactionType), new OracleParameter("p_levelno", logmodel.LevelNo), new OracleParameter("p_fromhost", logmodel.FromHost),
                new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()), new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()), new OracleParameter("p_processflag", logmodel.ProcessFlag),
                new OracleParameter("p_processowner", logmodel.ProcessOwner));
            return Convert.ToInt32(outPara.Value);
        }

        /// <summary>
        /// 根據主鍵修改組織管理者資料
        /// </summary>
        /// <param name="model">要修改的組織管理者Model</param>
        /// <returns>是否成功</returns>
        public int UpdateOrgmanagerByKey(OrgmanagerModel model,string olddepcode,string oldworkno, SynclogModel logmodel)
        {
            OracleParameter outPara = new OracleParameter("p_flag", OracleType.Int32);
            outPara.Direction = ParameterDirection.Output;
            int i = DalHelper.ExecuteNonQuery("update_orgmanager_p", CommandType.StoredProcedure,
                new OracleParameter("p_depcode", model.DepCode), new OracleParameter("p_depname", model.DepName),
                new OracleParameter("p_workno", model.WorkNo), new OracleParameter("p_localname", model.LocalName),
                new OracleParameter("p_manager", model.Manager), new OracleParameter("p_managername", model.ManagerName),
                new OracleParameter("p_notes", model.Notes), new OracleParameter("p_deputy", model.Deputy == null ? "" : model.Deputy),
                new OracleParameter("p_deputyname", model.DeputyName == null ? "" : model.DeputyName), new OracleParameter("p_deputynotes", model.DeputyNotes == null ? "" : model.DeputyNotes),
                new OracleParameter("p_isdirectlyunder", model.IsDirectlyUnder), new OracleParameter("p_isbgaudit", model.IsBGAudit), outPara,
                new OracleParameter("p_transactiontype", logmodel.TransactionType), new OracleParameter("p_levelno", logmodel.LevelNo), new OracleParameter("p_fromhost", logmodel.FromHost),
                new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()), new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()), new OracleParameter("p_processflag", logmodel.ProcessFlag),
                new OracleParameter("p_processowner", logmodel.ProcessOwner), new OracleParameter("p_olddepcode", olddepcode), new OracleParameter("p_oldworkno", oldworkno));
            return Convert.ToInt32(outPara.Value);
        }

        /// <summary>
        /// 更新人員基本表
        /// </summary>
        /// <param name="WorkNo"></param>
        /// <param name="notes"></param>
        /// <param name="managercode"></param>
        /// <param name="managername"></param>
        /// <returns></returns>
        public bool UpdateEmployeeByKey(string WorkNo, string notes, string managercode, string managername, SynclogModel logmodel)
        {
            string str;
            if (managercode != null)
            {
                str = "update GDS_ATT_EMPLOYEE set notes='" + notes + "',managercode='" + managercode + "',managername='" + managername + "' where workno='" + WorkNo + "'";
            }
            else
            {
                str = "update GDS_ATT_EMPLOYEE set notes='" + notes + "' where workno='" + WorkNo + "'";
            }
            return DalHelper.ExecuteNonQuery(str, logmodel) != -1;
        }

        /// <summary>
        /// 獲得管理職信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetManager()
        {
            string str = "select MANAGERCODE,MANAGERNAME from GDS_ATT_MANAGER where EFFECTFLAG='Y'";
            return DalHelper.ExecuteQuery(str);
        }


        /// <summary>
        /// 刪除一個組織管理者
        /// </summary>
        /// <param name="functionId">要刪除的組織管理者Id</param>
        /// <returns>刪除組織管理者條數</returns>
        public int DeleteOrgmanagerByKey(string workno, string depCode, SynclogModel logmodel)
        {
            string str = "delete from GDS_ATT_ORGMANAGER where workno='" + workno + "' and depcode='" + depCode + "'";
            return DalHelper.ExecuteNonQuery(str, logmodel);
        }

        /// <summary>
        /// 查詢工號對應的姓名與管理職
        /// </summary>
        /// <returns></returns>
        public DataTable GetInfo(string workno)
        {
            string str = "select LOCALNAME,MANAGERCODE,NOTES from GDS_ATT_EMPLOYEE where workno='" + workno + "'";
            return DalHelper.ExecuteQuery(str);
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
            DataTable dt = DalHelper.ExecuteQuery("gds_sc_orgmanager_vaildata", CommandType.StoredProcedure,
                new OracleParameter("p_personcode", personcode), outCursor, outSuccess, outError,
                new OracleParameter("p_transactiontype", logmodel.TransactionType), new OracleParameter("p_levelno", logmodel.LevelNo), new OracleParameter("p_fromhost", logmodel.FromHost),
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
        public List<OrgmanagerModel> GetList(DataTable dt)
        {
            return OrmHelper.SetDataTableToList(dt);
        }


        /// <summary>
        /// 組織人員查詢
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="workno"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable OrgEmployeeQuery(string personcode, string workno, string depcode)
        {
            OracleParameter outCursor = new OracleParameter("p_coursor", OracleType.Cursor);
            outCursor.Direction = ParameterDirection.Output;
            DataTable dt = DalHelper.ExecuteQuery("gds_sc_orgmanager_query", CommandType.StoredProcedure,
            new OracleParameter("p_personcode", personcode), new OracleParameter("p_workno", workno), new OracleParameter("p_depcode", depcode), outCursor);
            return dt;
        }

    }
}
