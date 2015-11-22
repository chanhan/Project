/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： RelatedOrgBll.cs
 * 檔功能描述： 關聯組織設定設定業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.8
 * 
 */

using System.Data;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.BLL.SystemManage.SystemSafety
{
    public class DepartmentAssignBll : BLLBase<IDepartmentAssignDal>
    {
        /// <summary>
        /// 獲得用戶的所有功能模組
        /// </summary>
        /// <param name="personcode"></param>
        /// <returns></returns>
        public DataTable GetPersonModule(string personcode)
        {
            return DAL.GetPersonModule(personcode);
        }

        /// <summary>
        /// 獲得用戶的公司代碼
        /// </summary>
        /// <param name="personcode"></param>
        /// <returns></returns>
        public DataTable GetPersonCompany(string rolescode)
        {
            return DAL.GetPersonCompany(rolescode);
        }

        /// <summary>
        /// 獲得用戶的組織層級
        /// </summary>
        /// <param name="personcode"></param>
        /// <returns></returns>
        public DataTable GetPersonDeplevel(string personcode)
        {
            return DAL.GetPersonDeplevel(personcode);
        }

        /// <summary>
        /// 獲得所有的組織層級
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllLevelCode()
        {
            return DAL.GetAllLevelCode();
        }

        /// <summary>
        /// 根據公司和模組查詢權限範圍內的部門
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="personCode"></param>
        /// <param name="ModuleCode"></param>
        /// <param name="CompanyId"></param>
        /// <param name="DepLevel"></param>
        /// <returns></returns>
        public DataTable GetPersonDeptDataByModule(string Appuser, string modulecode, string personCode, string ModuleCode, string CompanyId, string DepLevel)
        {
            return DAL.GetPersonDeptDataByModule(Appuser,modulecode, personCode, ModuleCode, CompanyId, DepLevel);
        }

        /// <summary>
        /// 保存用戶組織關聯信息
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="moduleCode"></param>
        /// <param name="companyId"></param>
        /// <param name="depts"></param>
        /// <param name="p_user"></param>
        /// <returns></returns>
        public bool SavePersonDeptData(string personcode, string rolecode, string modulecode, string companyId, string depts, string p_user, SynclogModel logmodel)
        {
            if (DAL.SavePersonDeptData(personcode, rolecode,modulecode, companyId, depts, p_user,logmodel) == 1)
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
