/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： AuthorityBll.cs
 * 檔功能描述： 角色功能模組角色授權業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2011.12.7
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Data;
using System.Collections;
namespace GDSBG.MiABU.Attendance.BLL.SystemManage.SystemSafety
{
    public class AuthorityBll : BLLBase<IAuthorityDal>
    {
        /// <summary>
        /// 根據角色代碼查詢該角色是否有系統功能權限
        /// </summary>
        /// <param name="rolecode">角色代碼</param>
        /// <returns>角色是否有系統功能權限</returns>
        public bool GetAuthorityBykey(string rolecode)
        {
            AuthorityModel authorityModel = new AuthorityModel();
            authorityModel.RoleCode = rolecode;
            return DAL.GetAuthorityBykey(authorityModel).Rows.Count > 0;
        }
        /// <summary>
        /// 查詢角色已授權的功能模組
        /// </summary>
        /// <param name="authorityModel">角色功能模組實體類</param>
        /// <returns>角色已授權的功能模組</returns>
        public DataTable GetAuthority(AuthorityModel authorityModel)
        {
            return DAL.GetModule(authorityModel);
        }
        /// <summary>
        /// 保存角色模組資料
        /// </summary>
        /// <param name="moduleList">功能模組</param>
        /// <param name="roleCode">角色代碼</param>
        /// <param name="workNo">工號</param>
        /// <param name="logmodel">日誌管理實體類</param>
        /// <returns>保存角色模組資料是否成功</returns>
        public bool SaveRoleModule(string moduleList, string roleCode, string workNo, SynclogModel logmodel, string selectModuleCode)
        {
            return DAL.SaveRoleModule(moduleList, roleCode, workNo, logmodel, selectModuleCode);
        }
    }
}
