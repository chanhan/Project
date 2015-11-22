/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IAuthorityDal.cs
 * 檔功能描述： 角色功能模組接口類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2011.12.7
 * 
 */
using System.Data;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemSafety
{
    [RefClass("SystemManage.SystemSafety.AuthorityDal")]
    public interface IAuthorityDal
    {
        /// <summary>
        /// 根據角色代碼查詢該角色是否有系統功能權限
        /// </summary>
        /// <param name="authorityModel">角色功能模組實體類</param>
        /// <returns>角色是否有系統功能權限</returns>
        DataTable GetAuthorityBykey(AuthorityModel authorityModel);
        /// <summary>
        /// 查詢角色已授權的功能模組
        /// </summary>
        /// <param name="authorityModel">角色功能模組實體類</param>
        /// <returns>角色已授權的功能模組</returns>
        DataTable GetModule(AuthorityModel authorityModel);
        /// <summary>
        /// 保存角色模組資料
        /// </summary>
        /// <param name="moduleList">功能模組</param>
        /// <param name="roleCode">角色代碼</param>
        /// <param name="workNo">工號</param>
        /// <param name="logmodel">日誌管理實體類</param>
        /// <returns>保存角色模組資料是否成功</returns>
        bool SaveRoleModule(string moduleList, string roleCode, string workNo, SynclogModel logmodel, string selectModuleCode);
    }
}
