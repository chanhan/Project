/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： RolesRoleDal.cs
 * 檔功能描述： 群組與角色設定數據操作類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.3
 * 
 */

using System;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.OracleDAL.SystemManage.SystemSafety
{
    public class RolesRoleDal : DALBase<RolesRoleModel>, IRolesRoleDal
    {
        /// <summary>
        /// 查询所有群組與角色
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetAllRolesRole()
        {
            return DalHelper.SelectAll();
        }
        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的功能Model</param>
        /// <returns>是否成功</returns>
        public bool AddRolesRole(RolesRoleModel model, SynclogModel logmodel)
        {
            return DalHelper.Insert(model,logmodel) != -1;
        }

        /// <summary>
        /// 根據條件查询群組與角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataTable GetRolesRole(RolesRoleModel model)
        {
            return DalHelper.Select(model);
        }

        /// <summary>
        /// 根據條件刪除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int DeleteRolesRole(RolesRoleModel model, SynclogModel logmodel)
        {
            return DalHelper.Delete(model,logmodel);
        }

        /// <summary>
        /// 查詢群組設定的角色
        /// </summary>
        /// <param name="rolescode"></param>
        /// <returns></returns>
        public DataTable GetRoleIsExist(string rolescode)
        {
            string str = "select a.rolecode,b.rolename from gds_sc_rolesrole a,gds_sc_role b where a.rolescode =:rolescode and a.rolecode = b.rolecode and b.Deleted = 'N' order by a.rolecode";
            return DalHelper.ExecuteQuery(str,new OracleParameter(":rolescode",rolescode));
        }

        /// <summary>
        /// 查詢群組沒有設定的角色
        /// </summary>
        /// <param name="rolecode"></param>
        /// <returns></returns>
        public DataTable GetRoleNotExist(string rolescode)
        {
            string str = "select rolecode,rolename from gds_sc_role where rolecode not in (select rolecode from gds_sc_rolesrole where rolescode =:rolescode) and Deleted='N' order by rolecode";
            return DalHelper.ExecuteQuery(str, new OracleParameter(":rolescode", rolescode));
        }

        /// <summary>
        /// 為群組設定角色
        /// </summary>
        /// <param name="RolesCode"></param>
        /// <param name="createuser"></param>
        /// <param name="Rolecode"></param>
        /// <returns></returns>
        public int SaveRolesRole(string RolesCode, string createuser, string RolesList, SynclogModel logmodel)
        {
            if (string.IsNullOrEmpty(RolesList))
            {
                string str = "delete from gds_sc_rolesrole where rolescode = '" + RolesCode + "'";
                DalHelper.ExecuteNonQuery(str,logmodel);
                return 1;
            }
            OracleParameter outPara = new OracleParameter("v_out", OracleType.Int32);
            outPara.Direction = ParameterDirection.Output;
            DalHelper.ExecuteNonQuery("gds_sc_saverolesrole", CommandType.StoredProcedure
                , new OracleParameter("v_rolescode", RolesCode), new OracleParameter("v_rolecode", RolesList), new OracleParameter("v_createuser", createuser), outPara,
                new OracleParameter("p_transactiontype", logmodel.TransactionType == null ? "" : logmodel.TransactionType.ToString()),
                new OracleParameter("p_levelno", logmodel.LevelNo == null ? "" : logmodel.LevelNo.ToString()),
                new OracleParameter("p_fromhost", logmodel.FromHost == null ? "" : logmodel.FromHost.ToString()),
                new OracleParameter("p_tohost", logmodel.ToHost == null ? "" : logmodel.ToHost.ToString()),
                new OracleParameter("p_docno", logmodel.DocNo == null ? "" : logmodel.DocNo.ToString()),
                new OracleParameter("p_processflag", logmodel.ProcessFlag == null ? "" : logmodel.ProcessFlag.ToString()),
                new OracleParameter("p_processowner", logmodel.ProcessOwner == null ? "" : logmodel.ProcessOwner.ToString()));
            return Convert.ToInt32(outPara.Value);

        }
    }
}
