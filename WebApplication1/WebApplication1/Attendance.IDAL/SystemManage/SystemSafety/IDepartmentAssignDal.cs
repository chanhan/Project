/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IDepartmentAssignDal.cs
 * 檔功能描述： 關聯組織設定數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.8
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
    [RefClass("SystemManage.SystemSafety.DepartmentAssignDal")]
    public interface IDepartmentAssignDal
    {
        /// <summary>
        /// 獲得用戶的所有功能模組
        /// </summary>
        /// <param name="personcode"></param>
        /// <returns></returns>
        DataTable GetPersonModule(string rolescode);

        /// <summary>
        /// 獲得用戶的公司代碼
        /// </summary>
        /// <param name="personcode"></param>
        /// <returns></returns>
        DataTable GetPersonCompany(string personcode);

        /// <summary>
        /// 獲得用戶的組織層級
        /// </summary>
        /// <param name="personcode"></param>
        /// <returns></returns>
        DataTable GetPersonDeplevel(string personcode);
        
        /// <summary>
        /// 獲得所有的組織層級
        /// </summary>
        /// <returns></returns>
        DataTable GetAllLevelCode();

        /// <summary>
        /// 根據公司和模組查詢權限範圍內的部門
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="personCode"></param>
        /// <param name="ModuleCode"></param>
        /// <param name="CompanyId"></param>
        /// <param name="DepLevel"></param>
        /// <returns></returns>
        DataTable GetPersonDeptDataByModule(string Appuser, string modulecode, string personCode, string ModuleCode, string CompanyId, string DepLevel);

        /// <summary>
        /// 保存用戶組織關聯信息
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="moduleCode"></param>
        /// <param name="companyId"></param>
        /// <param name="depts"></param>
        /// <param name="p_user"></param>
        /// <returns></returns>
        int SavePersonDeptData(string personcode, string rolecode, string modulecode, string companyId, string depts, string p_user, SynclogModel logmodel);
    }
}
