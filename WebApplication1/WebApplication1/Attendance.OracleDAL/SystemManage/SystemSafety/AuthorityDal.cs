/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： AuthorityBll.cs
 * 檔功能描述： 角色功能模組操作類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2011.12.7
 * 
 */
using System;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.OracleDAL.SystemManage.SystemSafety
{
    public class AuthorityDal : DALBase<AuthorityModel>, IAuthorityDal
    {
        /// <summary>
        /// 根據角色代碼查詢該角色是否有系統功能權限
        /// </summary>
        /// <param name="authorityModel">角色功能模組實體類</param>
        /// <returns>角色是否有系統功能權限</returns>
        public DataTable GetAuthorityBykey(AuthorityModel authorityModel)
        {
            return DalHelper.Select(authorityModel);
        }
        /// <summary>
        /// 查詢角色已授權的功能模組
        /// </summary>
        /// <param name="authorityModel">角色功能模組實體類</param>
        /// <returns>角色已授權的功能模組</returns>
        public DataTable GetModule(AuthorityModel authorityModel)
        {
            return DalHelper.Select(authorityModel);
        }
        /// <summary>
        /// 保存角色模組資料
        /// </summary>
        /// <param name="moduleList">功能模組</param>
        /// <param name="roleCode">角色代碼</param>
        /// <param name="workNo">工號</param>
        /// <param name="logmodel">日誌管理實體類</param>
        /// <returns>保存角色模組資料是否成功</returns>
        public bool SaveRoleModule(string moduleList, string roleCode, string workNo, SynclogModel logmodel,string selectModuleCode)
        {
            OracleParameter outPara = new OracleParameter("p_out", OracleType.Int32);
            outPara.Direction = ParameterDirection.Output;
            DalHelper.ExecuteNonQuery("gds_sc_saverolemod_pro", CommandType.StoredProcedure,
                new OracleParameter("p_modulelist", moduleList), new OracleParameter("p_rolecode", roleCode), new OracleParameter("p_user", workNo), outPara,
                new OracleParameter("p_transactiontype", logmodel.TransactionType == null ? "" : logmodel.TransactionType.ToString()),
                   new OracleParameter("p_levelno", logmodel.LevelNo == null ? "" : logmodel.LevelNo.ToString()),
                   new OracleParameter("p_fromhost", logmodel.FromHost == null ? "" : logmodel.FromHost.ToString()),
                   new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()),
                   new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()),
                   new OracleParameter("p_processflag", logmodel.ProcessFlag == null ? "" : logmodel.ProcessFlag.ToString()),
                   new OracleParameter("p_processowner", logmodel.ProcessOwner == null ? "" : logmodel.ProcessOwner.ToString()), new OracleParameter("p_hidSelectModuleCode", selectModuleCode == "" ? "###" : selectModuleCode));
            return Convert.ToInt32(outPara.Value) == 1;
        }

    }
}
