/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ICompanyAssignDal.cs
 * 檔功能描述： 關聯公司設定數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.7
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
    [RefClass("SystemManage.SystemSafety.CompanyAssignDal")]
    public interface ICompanyAssignDal
    {
        /// <summary>
        /// 獲得用戶所關聯的公司代碼
        /// </summary>
        /// <param name="personcode"></param>
        /// <returns></returns>
        DataTable GetPersonCompany(string personcode);

        /// <summary>
        /// 獲得所有用戶沒有關聯的公司代碼
        /// </summary>
        /// <param name="personcode"></param>
        /// <returns></returns>
        DataTable GetOtherAllCompany(string personcode);

        /// <summary>
        /// 保存用戶公司關聯信息
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="companyList"></param>
        /// <param name="p_user"></param>
        /// <returns></returns>
        int SavePersonCompany(string personcode, string companyList, string p_user, SynclogModel logmodel);


        /// <summary>
        /// 獲得用戶所有公司代碼
        /// </summary>
        /// <param name="personcode"></param>
        /// <returns></returns>
        DataTable GetAllCompany(string personcode);
    }
}
