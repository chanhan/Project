/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： CompanyAssignBll.cs
 * 檔功能描述： 關聯公司設定業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.7
 * 
 */

using System.Data;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.BLL.SystemManage.SystemSafety
{
    public class CompanyAssignBll : BLLBase<ICompanyAssignDal>
    {
        /// <summary>
        /// 獲得用戶所關聯的公司代碼
        /// </summary>
        /// <param name="personcode"></param>
        /// <returns></returns>
        public DataTable GetPersonCompany(string personcode)
        {
            return DAL.GetPersonCompany(personcode);
        }

        /// <summary>
        /// 獲得所有用戶沒有關聯的公司代碼
        /// </summary>
        /// <param name="personcode"></param>
        /// <returns></returns>
        public DataTable GetOtherAllCompany(string personcode)
        {
            return DAL.GetOtherAllCompany(personcode);
        }

        /// <summary>
        /// 保存用戶公司關聯信息
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="companyList"></param>
        /// <param name="p_user"></param>
        /// <returns></returns>
        public bool SavePersonCompany(string personcode, string companyList, string p_user, SynclogModel logmodel)
        {
            if (DAL.SavePersonCompany(personcode, companyList, p_user,logmodel) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 獲得用戶所有公司代碼
        /// </summary>
        /// <param name="personcode"></param>
        /// <returns></returns>
        public DataTable GetAllCompany(string personcode)
        {
            return DAL.GetAllCompany(personcode);
        }
    }
}
