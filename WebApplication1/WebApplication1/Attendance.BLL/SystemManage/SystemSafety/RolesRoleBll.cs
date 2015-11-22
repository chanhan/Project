/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： RolesRoleBll.cs
 * 檔功能描述： 群組與角色設定業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.3
 * 
 */

using System.Data;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.BLL.SystemManage.SystemSafety
{
    public class RolesRoleBll : BLLBase<IRolesRoleDal>
    {
        // <summary>
        /// 根據主鍵查詢Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Model</returns>
        public int GetRolesRoleCount(RolesRoleModel model)
        {
            RolesRoleModel rrmodel = new RolesRoleModel();
            DataTable dt = new DataTable();
            dt = DAL.GetRolesRole(model);
            return dt.Rows.Count;
            
        }

        /// <summary>
        /// 查询所有群組與角色
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetAllRolesRole()
        {
            return DAL.GetAllRolesRole();
        }
        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的功能Model</param>
        /// <returns>是否成功</returns>
        public bool AddRolesRole(RolesRoleModel model, SynclogModel logmodel)
        {
            return DAL.AddRolesRole(model,logmodel);
            
        }

        /// <summary>
        /// 根據條件查询群組與角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataTable GetRolesRole(RolesRoleModel model)
        {
            return DAL.GetRolesRole(model);
        }

        /// <summary>
        /// 根據條件刪除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int DeleteRolesRole(RolesRoleModel model, SynclogModel logmodel)
        {
            return DAL.DeleteRolesRole(model,logmodel);
        }

        /// <summary>
        /// 查詢群組設定的角色
        /// </summary>
        /// <param name="rolescode"></param>
        /// <returns></returns>
        public DataTable GetRoleIsExist(string rolescode)
        {
            return DAL.GetRoleIsExist(rolescode);
        }

        /// <summary>
        /// 查詢群組沒有設定的角色
        /// </summary>
        /// <param name="rolecode"></param>
        /// <returns></returns>
        public DataTable GetRoleNotExist(string rolecode)
        {
            return DAL.GetRoleNotExist(rolecode);
        }

        /// <summary>
        /// 為群組設定角色
        /// </summary>
        /// <param name="RolesCode"></param>
        /// <param name="createuser"></param>
        /// <param name="Rolecode"></param>
        /// <returns></returns>
        public bool SaveRolesRole(string RolesCode, string createuser, string RolesList, SynclogModel logmodel)
        {
            if (DAL.SaveRolesRole(RolesCode, createuser, RolesList,logmodel) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
