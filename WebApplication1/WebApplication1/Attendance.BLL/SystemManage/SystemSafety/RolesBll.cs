/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： RolesBll.cs
 * 檔功能描述： 群組設定業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.1
 * 
 */

using System.Data;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.BLL.SystemManage.SystemSafety
{
    public class RolesBll : BLLBase<IRolesDal>
    {
        /// <summary>
        /// 根據主鍵查詢Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Model</returns>
        public RolesModel GetRolesByKey(string rolesCode)
        {
            RolesModel model = new RolesModel();
            model.RolesCode = rolesCode;
            return DAL.GetRolesByKey(model);
        }

        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的功能Model</param>
        /// <returns>是否成功</returns>
        public bool AddRoles(RolesModel model, SynclogModel logmodel)
        {
            return DAL.AddRoles(model,logmodel);
        }

        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        public bool UpdateRolesByKey(RolesModel model, SynclogModel logmodel)
        {
            return DAL.UpdateRolesByKey(model,logmodel);
        }

        /// <summary>
        /// 根據條件查询群組
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataTable GetRoles(RolesModel model)
        {
            return DAL.GetRoles(model);
        }

        /// <summary>
        /// 根據條件刪除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int DeleteRoles(RolesModel model, SynclogModel logmodel)
        {
            return DAL.DeleteRoles(model,logmodel);
        }

        /// <summary>
        /// 根據條件查詢數據
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetRolesPageInfo(RolesModel model, int pageIndex, int pageSize, out int totalCount)
        {
            return DAL.GetRolesPageInfo(model, pageIndex, pageSize, out totalCount);
        }
    }
}
