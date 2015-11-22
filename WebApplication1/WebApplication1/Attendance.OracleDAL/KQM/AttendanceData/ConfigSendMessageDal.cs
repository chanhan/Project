/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ConfigSendMessageDal.cs
 * 檔功能描述： 郵件提醒數據操作類
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2012.01.03
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
    public class ConfigSendMessageDal : DALBase<ConfigSendMessageModel>, IConfigSendMessageDal
    {
        /// <summary>
        /// 查詢用戶有權限的模塊代碼
        /// </summary>
        /// <param name="personCode">工號</param>
        /// <param name="roleCode">角色代碼</param>
        /// <returns></returns>
        public DataTable GetSendMsgDataByKey(string personCode, string roleCode)
        {
            OracleParameter outPara = new OracleParameter("dt", OracleType.Cursor);
            outPara.Direction = ParameterDirection.Output;
            DataTable dt = DalHelper.ExecuteQuery("gds_sc_pk.configsendmessageto", CommandType.StoredProcedure
                , new OracleParameter("v_personcode", personCode), new OracleParameter("v_rolecode", roleCode),outPara);
            return dt;
        }
        /// <summary>
        /// 查詢用戶沒有權限的模塊代碼
        /// </summary>
        /// <param name="personCode">工號</param>
        /// <param name="roleCode">角色代碼</param>
        /// <returns></returns>
        public DataTable GetSendMsgDataNotByKey(string personCode, string roleCode)
        {
            OracleParameter outPara = new OracleParameter("dt", OracleType.Cursor);
            outPara.Direction = ParameterDirection.Output;
            DataTable dt = DalHelper.ExecuteQuery("gds_sc_pk.configsendmessagenot", CommandType.StoredProcedure
                , new OracleParameter("v_personcode", personCode), new OracleParameter("v_rolecode", roleCode), outPara);
            return dt;
        }
        /// <summary>
        /// 保存樹的數據
        /// </summary>
        /// <param name="personCode">工號</param>
        /// <param name="levels">樹層級</param>
        /// <returns></returns>
        public int UpdateSendMsgByKey(string personCode, Hashtable levels, SynclogModel logmodel)
        {
            int i = 0;
            int a;
            DalHelper.ExecuteNonQuery("DELETE FROM gds_sc_employees WHERE workno ='" + personCode + "'",logmodel);
            for (int j = 1; j <=levels.Count; j++)
            {
                string levelCode = levels[j].ToString();
                OracleParameter outPara = new OracleParameter("v_out", OracleType.Int32);
                outPara.Direction = ParameterDirection.Output;
                a = DalHelper.ExecuteNonQuery("gds_sc_pk.updatesendmsg", CommandType.StoredProcedure
                    , new OracleParameter("v_personcode", personCode), new OracleParameter("v_levelcode", levelCode),new OracleParameter("p_transactiontype", logmodel.TransactionType), new OracleParameter("p_levelno", logmodel.LevelNo),
                    new OracleParameter("p_fromhost", logmodel.FromHost), new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()),
                    new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()), new OracleParameter("p_processflag", logmodel.ProcessFlag),
                    new OracleParameter("p_processowner", logmodel.ProcessOwner),outPara);
                i = Convert.ToInt32(outPara.Value);
            }
            return i;
        }
        /// <summary>
        /// 查詢用戶的權限組織
        /// </summary>
        /// <param name="personcode">工號</param>
        /// <param name="sModuleCode">模組代碼</param>
        /// <param name="sCompanyID">公司ID</param>
        /// <returns></returns>
        public DataTable GetPersonDeptDataByModule(string personcode, string sModuleCode, string sCompanyID)
        {
            OracleParameter outPara = new OracleParameter("dt", OracleType.Cursor);
            outPara.Direction = ParameterDirection.Output;
            DataTable dt = DalHelper.ExecuteQuery("gds_sc_pk.getpersondeptbymodule", CommandType.StoredProcedure
                , new OracleParameter("v_personcode", personcode), new OracleParameter("v_modulecode", sModuleCode),
                new OracleParameter("v_companyid", sCompanyID), outPara);
            return dt;
        }
        /// <summary>
        /// 保存樹信息
        /// </summary>
        /// <param name="personCode">工號</param>
        /// <param name="moduleCode">模塊代碼</param>
        /// <param name="componyID">公司代碼</param>
        /// <param name="depts">組織名稱</param>
        /// <returns></returns>
        public int SaveSRMData(string personCode, string moduleCode, string componyID, Hashtable depts, SynclogModel logmodel)
        {
            int i = 0;
            int a;
            DalHelper.ExecuteNonQuery("DELETE FROM gds_att_Depcode  WHERE personcode='" + personCode + "' and ModuleCode='" + moduleCode + "' and CompanyID='" + componyID + "'",logmodel);
            for (int j = 1; j <= depts.Count; j++)
            {
                string deptCode = depts[j].ToString();
                OracleParameter outPara = new OracleParameter("v_out", OracleType.Int32);
                outPara.Direction = ParameterDirection.Output;
                a = DalHelper.ExecuteNonQuery("gds_sc_pk.updatesrmdata", CommandType.StoredProcedure
                    , new OracleParameter("v_personcode", personCode), new OracleParameter("v_modulecode", moduleCode),
                new OracleParameter("v_companyid", componyID), new OracleParameter("v_depcode", deptCode), outPara, new OracleParameter("p_transactiontype", logmodel.TransactionType), new OracleParameter("p_levelno", logmodel.LevelNo),
                    new OracleParameter("p_fromhost", logmodel.FromHost), new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()),
                    new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()), new OracleParameter("p_processflag", logmodel.ProcessFlag),
                    new OracleParameter("p_processowner", logmodel.ProcessOwner));
                i = Convert.ToInt32(outPara.Value);
            }
            return i;
        }
    }
}
