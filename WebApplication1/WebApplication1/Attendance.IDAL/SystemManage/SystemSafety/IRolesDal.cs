/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IRolesDal.cs
 * 檔功能描述： 群組數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.1
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
    [RefClass("SystemManage.SystemSafety.RolesDal")]
    public interface IRolesDal
    {
        /// <summary>
        /// 根據主鍵查詢Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Model</returns>
        RolesModel GetRolesByKey(RolesModel model);

        /// <summary>
        /// 根据主键查询DataTable
        /// </summary>
        /// <param name="model"></param>
        /// <returns>DataTable</returns>
        DataTable GetRolesDataTableByKey(RolesModel model);

        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的功能Model</param>
        /// <returns>是否成功</returns>
        bool AddRoles(RolesModel model, SynclogModel logmodel);


        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        bool UpdateRolesByKey(RolesModel model, SynclogModel logmodel);
        
        /// <summary>
        /// 根據條件查询群組
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        DataTable GetRoles(RolesModel model);

        /// <summary>
        /// 根據條件刪除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int DeleteRoles(RolesModel model, SynclogModel logmodel);

        /// <summary>
        /// 根據條件查詢數據
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        DataTable GetRolesPageInfo(RolesModel model, int pageIndex, int pageSize, out int totalCount);
    }
}
