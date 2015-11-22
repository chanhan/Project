/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IRoleDal.cs
 * 檔功能描述： 角色數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 何西 2011.11.30
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
    [RefClass("SystemManage.SystemSafety.RoleDal")]
    public interface IRoleDal
    {
        /// <summary>
        /// 根據主鍵查詢Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Model</returns>
         RoleModel GetRoleByKey(RoleModel model);

         /// <summary>
         /// 根据条件查询数据
         /// </summary>
         /// <param name="roleName"></param>
         /// <returns></returns>
      //   DataTable GetRole(RoleModel model);
         DataTable GetRole(RoleModel model, bool b, string orderByString, int currentPageIndex, int pageSize, out int totalCount);
        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的功能Model</param>
        /// <returns>是否成功</returns>
         bool AddRole(RoleModel model, SynclogModel logmodel);
       
        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
         bool UpdateRoleByKey(RoleModel model, SynclogModel logmodel);


        /// <summary>
        /// 根据model删除角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns>删除的行数</returns>
         int DeleteRole(string roleCode, SynclogModel logmodel);

        /// <summary>
        /// 根據角色代碼查詢功能模塊
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        DataTable GetUserModuleListByRoleCode(string roleCode);


        /// <summary>
        /// 失效角色
        /// </summary>
        /// <param name="roleCode">角色代碼</param>
        /// <returns>失效是否成功</returns>
        int RoleDisable(string roleCode, SynclogModel logmodel);
    }
}
