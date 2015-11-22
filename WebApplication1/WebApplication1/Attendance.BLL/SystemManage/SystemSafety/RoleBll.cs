/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： RoleBll.cs
 * 檔功能描述： 角色管理業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 何西 2011.11.30
 * 
 */

using System.Collections.Generic;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Data;


namespace GDSBG.MiABU.Attendance.BLL.SystemManage.SystemSafety
{
    public class RoleBll : BLLBase<IRoleDal>
    {
        /// <summary>
        /// 根據主鍵查詢Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Model</returns>
        public RoleModel GetRoleByKey(string roleCode)
        {
            RoleModel model = new RoleModel();
            model.RoleCode = roleCode;
            return DAL.GetRoleByKey(model);
        }


        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public DataTable GetRole(RoleModel model, int currentPageIndex, int pageSize, out int totalCount)
        {
            bool b = model.RoleCode != null ? false : true;
            string orderByString = "create_date desc";
            return DAL.GetRole(model, b, orderByString,currentPageIndex,pageSize,out totalCount);
        }

        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的功能Model</param>
        /// <returns>是否成功</returns>
        public bool AddRole(RoleModel model, SynclogModel logmodel)
        {
            return DAL.AddRole(model, logmodel);
        }

        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        public bool UpdateRoleByKey(RoleModel model, SynclogModel logmodel)
        {
            return DAL.UpdateRoleByKey(model, logmodel);
        }
        /// <summary>
        /// 判断角色代码是否存在
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public bool IsExist(string roleCode)
        {
            bool blExist = false;
            RoleModel model = GetRoleByKey(roleCode);
            if (model != null)
            {
                blExist = true;
            }
            return blExist;
        }

        /// <summary>
        /// 根据model删除角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns>删除是否成功</returns>
        public bool DeleteRole(string  roleCode, SynclogModel logmodel)
        {
            return DAL.DeleteRole(roleCode, logmodel) == 1;
        }

        /// <summary>
        /// 根據角色代碼查詢功能模塊
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public DataTable GetUserModuleListByRoleCode(string roleCode)
        {
            return DAL.GetUserModuleListByRoleCode(roleCode);
        }

        public bool RoleDisable(string roleCode, SynclogModel logmodel)
        {
            return DAL.RoleDisable(roleCode, logmodel) == 1;
        }

    }
}
