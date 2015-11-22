/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OrgEmployeeBll.cs
 * 檔功能描述： 人員編組功能模組業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2011.12.16
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using GDSBG.MiABU.Attendance.IDAL.HRM;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.BLL.HRM
{
    public class OrgEmployeeBll : BLLBase<IOrgEmployeeDal>
    {
        /// <summary>
        /// 得到人員層級樹
        /// </summary>
        /// <param name="personCode">工號</param>
        /// <param name="companyId">公司ID</param>
        /// <param name="moduleCode">模組代碼</param>
        /// <returns>查詢結果DataTable</returns>
        public DataTable getOrgEmployeeTree(string personCode, string companyId, string moduleCode)
        {
            return DAL.getOrgEmployeeTree(personCode, companyId, moduleCode);
        }

        /// <summary>
        /// 根據組織代碼查詢
        /// </summary>
        /// <param name="OrgCode">組織代碼</param>
        /// <returns>查詢結果DataTable</returns>
        public DataTable GetDataByOrgCode(string OrgCode)
        {
            return DAL.GetDataByOrgCode(OrgCode);
        }
        /// <summary>
        /// 獲取處級及以下組織代碼
        /// </summary>
        /// <param name="OrgCode">組織代碼</param>
        /// <returns>處級及以下組織代碼</returns>
        public string GetDepCode(string OrgCode)
        {
            DataTable dt = DAL.GetDepCode(OrgCode);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            return "";
        }
        /// <summary>
        /// 查詢所選組織下的子階組織或所屬組級、線級、段級的所有組
        /// </summary>
        /// <param name="personCode">工號</param>
        /// <param name="companyId">公司ID</param>
        /// <param name="moduleCode">模組代碼</param>
        /// <param name="parentDep">父級組織代碼</param>
        /// <returns>所選組織下的子階組織或所屬組級、線級、段級的所有組</returns>
        public DataTable GetAuthorizedDept(string personCode, string companyId, string moduleCode, string parentDep)
        {
            return DAL.GetAuthorizedDept(personCode, companyId, moduleCode, parentDep);
        }
        /// <summary>
        /// 所選"課組"必須是所填員工目前所屬部門下的課或組
        /// </summary>
        /// <param name="moduleCode">模組代碼</param>
        /// <param name="OrgCode">組織代碼</param>
        /// <param name="personCode">登陸工號</param>
        /// <param name="companyId">公司ID</param>
        /// <returns>是否是所填員工目前所屬部門下的課或組</returns>
        public bool IsInDep(string OrgCode, string sqlDep)
        {
            //if (DAL.IsInDep(moduleCode, OrgCode, personCode, companyId) != null)
            //{
            //    return DAL.IsInDep(moduleCode, OrgCode, personCode, companyId).Rows.Count > 0;
            //}
            //else
            //{
            //    return false;
            //}
            return DAL.IsInDep(OrgCode, sqlDep).Rows.Count > 0;
        }
        /// <summary>
        /// 保存清除編組
        /// </summary>
        /// <param name="worknos">所有工號</param>
        /// <param name="OrgCode">組織代碼</param>
        /// <param name="personcode">登陸帳號</param>
        /// <returns>是否更新成功</returns>
        public bool UpdateData(string worknos, string OrgCode, string personcode,SynclogModel logmodel)
        {
            return DAL.UpdateData(worknos, OrgCode, personcode,logmodel) == 1;
        }
        /// <summary>
        /// 導入Excel
        /// </summary>
        /// <param name="createUser">創建者</param>
        /// <param name="successnum">導入成功記錄數</param>
        /// <param name="errornum">導入失敗記錄數</param>
        /// <returns>導入的信息DataTable</returns>
        public DataTable ImpoertExcel(string createUser, string moduleCode, string companyID, out int successnum, out int errornum, SynclogModel logmodel)
        {
            return DAL.ImpoertExcel(createUser, moduleCode, companyID, out  successnum, out  errornum, logmodel);
        }
    }
}
