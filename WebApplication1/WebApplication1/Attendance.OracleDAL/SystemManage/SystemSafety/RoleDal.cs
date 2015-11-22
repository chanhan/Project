/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ModuleDal.cs
 * 檔功能描述： 模組管理數據操作類
 * 
 * 版本：1.0
 * 創建標識： 何西 2011.11.28
 * 
 */

using System;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.OracleDAL.SystemManage.SystemSafety
{
    public class RoleDal : DALBase<RoleModel>, IRoleDal
    {
        /// <summary>
        /// 根據主鍵查詢Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Model</returns>
        public RoleModel GetRoleByKey(RoleModel model)
        {
            return DalHelper.SelectByKey(model);
        }

        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        //public DataTable GetRole(RoleModel model)
        //{
        //    return DalHelper.Select(model, true);
        //}
        public DataTable GetRole(RoleModel model, bool b, string orderByString, int currentPageIndex, int pageSize, out int totalCount)
        {
            return DalHelper.Select(model, b, orderByString, currentPageIndex, pageSize, out  totalCount);
        }
        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的功能Model</param>
        /// <returns>是否成功</returns>
        public bool AddRole(RoleModel model, SynclogModel logmodel)
        {
            return DalHelper.Insert(model, logmodel) != -1;
        }

        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        public bool UpdateRoleByKey(RoleModel model, SynclogModel logmodel)
        {
            return DalHelper.UpdateByKey(model, true, logmodel) != -1;
        }

        /// <summary>
        /// 根据model删除角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns>删除的行数</returns>
        public int DeleteRole(string roleCode, SynclogModel logmodel)
        {
            string cmdText = "GDS_SC_RoleDeletepro";
            OracleParameter opInroleCode = new OracleParameter();
            opInroleCode.Direction = ParameterDirection.Input;
            opInroleCode.ParameterName = "role_code";
            opInroleCode.Value = roleCode;
            OracleParameter opOutResult = new OracleParameter();
            opOutResult.Direction = ParameterDirection.Output;
            opOutResult.ParameterName = "result";
            opOutResult.OracleType = OracleType.Int32;
            DalHelper.ExecuteNonQuery(cmdText, CommandType.StoredProcedure, opInroleCode, opOutResult, new OracleParameter("p_transactiontype", logmodel.TransactionType == null ? "" : logmodel.TransactionType.ToString()),
                   new OracleParameter("p_levelno", logmodel.LevelNo == null ? "" : logmodel.LevelNo.ToString()),
                   new OracleParameter("p_fromhost", logmodel.FromHost == null ? "" : logmodel.FromHost.ToString()),
                   new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()),
                   new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()),
                   new OracleParameter("p_processflag", logmodel.ProcessFlag == null ? "" : logmodel.ProcessFlag.ToString()),
                   new OracleParameter("p_processowner", logmodel.ProcessOwner == null ? "" : logmodel.ProcessOwner.ToString()));
            return Convert.ToInt32(opOutResult.Value);
        }

        /// <summary>
        /// 根據角色代碼查詢功能模塊
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public DataTable GetUserModuleListByRoleCode(string roleCode)
        {
            OracleParameter opRoleCode = new OracleParameter();
            opRoleCode.Direction = ParameterDirection.Input;
            opRoleCode.ParameterName = "selectRoleCode";
            opRoleCode.Value = roleCode;
            string cmdText = "SELECT * FROM (SELECT a.*,DECODE (b.modulecode, null, 'N', 'Y')AS authorized, b.functionlist functionlisted FROM gds_sc_module a, gds_sc_authority b WHERE b.rolecode(+) =:selectRoleCode AND a.modulecode = b.modulecode(+) AND a.deleted = 'N') START WITH (parentmodulecode IS NULL) CONNECT BY PRIOR modulecode = parentmodulecode  ORDER SIBLINGS BY orderid";
            DataTable dt = DalHelper.ExecuteQuery(cmdText, opRoleCode);
            return dt;
        }


        /// <summary>
        /// 失效角色
        /// </summary>
        /// <param name="roleCode">角色代碼</param>
        /// <returns>失效是否成功</returns>
        public int RoleDisable(string roleCode, SynclogModel logmodel)
        {
            string cmdText = "GDS_SC_RoleDisablepro";
            OracleParameter opInroleCode = new OracleParameter();
            opInroleCode.Direction = ParameterDirection.Input;
            opInroleCode.ParameterName = "role_code";
            opInroleCode.Value = roleCode;
            OracleParameter opOutResult = new OracleParameter();
            opOutResult.Direction = ParameterDirection.Output;
            opOutResult.ParameterName = "result";
            opOutResult.OracleType = OracleType.Int32;
            DalHelper.ExecuteNonQuery(cmdText, CommandType.StoredProcedure, opInroleCode, opOutResult, new OracleParameter("p_transactiontype", logmodel.TransactionType == null ? "" : logmodel.TransactionType.ToString()),
                   new OracleParameter("p_levelno", logmodel.LevelNo == null ? "" : logmodel.LevelNo.ToString()),
                   new OracleParameter("p_fromhost", logmodel.FromHost == null ? "" : logmodel.FromHost.ToString()),
                   new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()),
                   new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()),
                   new OracleParameter("p_processflag", logmodel.ProcessFlag == null ? "" : logmodel.ProcessFlag.ToString()),
                   new OracleParameter("p_processowner", logmodel.ProcessOwner == null ? "" : logmodel.ProcessOwner.ToString()));
            return Convert.ToInt32(opOutResult.Value);
        }


    }
}
