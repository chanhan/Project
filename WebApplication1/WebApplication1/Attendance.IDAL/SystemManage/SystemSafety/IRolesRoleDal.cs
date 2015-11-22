/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IRolesRoleDal.cs
 * 檔功能描述： 群組與角色數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.3
 * 
 */

using System.Data;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemSafety
{
    /// <summary>
    /// 功能管理數據操作接口
    /// </summary>
    [RefClass("SystemManage.SystemSafety.RolesRoleDal")]
    public interface IRolesRoleDal
    {
        /// <summary>
        /// 查询所有群組與角色
        /// </summary>
        /// <returns>DataTable</returns>
        DataTable GetAllRolesRole();

        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的功能Model</param>
        /// <returns>是否成功</returns>
        bool AddRolesRole(RolesRoleModel model, SynclogModel logmodel);

        /// <summary>
        /// 根據條件查询群組與角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        DataTable GetRolesRole(RolesRoleModel model);

        /// <summary>
        /// 根據條件刪除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int DeleteRolesRole(RolesRoleModel model, SynclogModel logmodel);

        /// <summary>
        /// 查詢群組設定的角色
        /// </summary>
        /// <param name="rolescode"></param>
        /// <returns></returns>
        DataTable GetRoleIsExist(string rolescode);

        /// <summary>
        /// 查詢群組沒有設定的角色
        /// </summary>
        /// <param name="rolecode"></param>
        /// <returns></returns>
        DataTable GetRoleNotExist(string rolecode);

        /// <summary>
        /// 為群組設定角色
        /// </summary>
        /// <param name="RolesCode"></param>
        /// <param name="createuser"></param>
        /// <param name="Rolecode"></param>
        /// <returns></returns>
        int SaveRolesRole(string RolesCode, string createuser, string RolesList, SynclogModel logmodel);
    }
}
