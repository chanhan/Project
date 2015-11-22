/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： DepartmentBll.cs
 * 檔功能描述： 組織資料業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2012.01.02
 * 
 */

using System;
using System.Collections.Generic;
using System.Data;
using GDSBG.MiABU.Attendance.IDAL.HRM;
using GDSBG.MiABU.Attendance.Model.HRM;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.BLL.HRM
{
    public class DepartmentBll : BLLBase<IDepartmentDal>
    {
        /// <summary>
        /// 獲得部門排序Id
        /// </summary>
        /// <returns></returns>
        public DataTable GetOrderId()
        {
            return DAL.GetOrderId();
        }

        /// <summary>
        /// 根據條件查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sql"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetDeaprtmentPageInfo(DepartmentModel model, string levelcode, string sql, int pageIndex, int pageSize, out int totalCount)
        {
            return DAL.GetDeaprtmentPageInfo(model,levelcode, sql, pageIndex, pageSize, out totalCount);
        }

        /// <summary>
        /// 導出查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="levelcode"></param>
        /// <param name="sql"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetDeaprtmentForExport(DepartmentModel model, string levelcode, string sql)
        {
            return DAL.GetDeaprtmentForExport(model, levelcode, sql);
        }
        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<DepartmentModel> GetList(DataTable dt)
        {
            return DAL.GetList(dt);
        }

        /// <summary>
        /// 查詢組織下的員工
        /// </summary>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable GetWorkNoByDept(string depcode)
        {
            return DAL.GetWorkNoByDept(depcode);
        }

        /// <summary>
        ///查詢子組織
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable GetDeptByParent(string companyid, string depcode)
        {
            return DAL.GetDeptByParent(companyid, depcode);
        }

        /// <summary>
        /// 查詢用戶層級信息
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="levelcode"></param>
        /// <returns></returns>
        public DataTable GetUserDepLevel(string personcode, string levelcode)
        {
            return DAL.GetUserDepLevel(personcode, levelcode);
        }

        /// <summary>
        /// 失效
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public bool Disable(string companyid, string depcode, SynclogModel logmodel)
        {
            return DAL.Disable(companyid, depcode,logmodel);
        }

        /// <summary>
        /// 查詢組織信息
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable GetDept(string companyid, string depcode)
        {
            return DAL.GetDept(companyid, depcode);
        }

        /// <summary>
        /// 生效
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public bool Enable(string companyid, string depcode, SynclogModel logmodel)
        {
            return DAL.Enable(companyid, depcode,logmodel);
        }

        /// <summary>
        /// 獲得組織及其子組織信息
        /// </summary>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable GetDeptByParentDept(string depcode)
        {
            return DAL.GetDeptByParentDept(depcode);
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(DepartmentModel model, SynclogModel logmodel)
        {
            return DAL.Delete(model,logmodel);
        }

        /// <summary>
        /// 檢查費用代碼
        /// </summary>
        /// <param name="CostCode"></param>
        /// <param name="AccountEntity"></param>
        /// <param name="DepCode"></param>
        /// <returns></returns>
        public DataTable GetCostCode(string CostCode, string AccountEntity, string DepCode)
        {
            return DAL.GetCostCode(CostCode, AccountEntity, DepCode);
        }

        /// <summary>
        /// 查詢參數值
        /// </summary>
        /// <returns></returns>
        public DataTable GetParaValue()
        {
            return DAL.GetParaValue();
        }

        /// <summary>
        /// 查詢組織的排序Id
        /// </summary>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable GetOrderIdByDepCode(string depcode)
        {
            return DAL.GetOrderIdByDepCode(depcode);
        }

        /// <summary>
        /// 查詢層級代碼的排序Id
        /// </summary>
        /// <param name="levelcode"></param>
        /// <returns></returns>
        public DataTable GetOrderIdBylevelcode(string levelcode)
        {
            return DAL.GetOrderIdBylevelcode(levelcode);
        }

        /// <summary>
        /// 根據部門代碼和層級代碼檢驗信息是否存在
        /// </summary>
        /// <param name="depcode"></param>
        /// <param name="levelcode"></param>
        /// <returns></returns>
        public DataTable CheckDepCodeAndLevelCode(string depcode, string levelcode)
        {
            return DAL.CheckDepCodeAndLevelCode(depcode, levelcode);
        }

        /// <summary>
        /// 查詢最大部門代碼
        /// </summary>
        /// <param name="levelcode"></param>
        /// <param name="parentcode"></param>
        /// <returns></returns>
        public string GetMaxCode(string levelcode, string parentcode)
        {
            string sDepCode = "00001";
            string sHead = "";
            int iDepCode = 0;
            string depcode = DAL.GetMaxCode(levelcode, parentcode, out sHead);
            if (sHead.Equals("-"))
            {
                if (depcode.Length > 0)
                {
                    string[] arr = depcode.Split(new char[] { '-' });
                    if (arr.Length > 1)
                    {
                        iDepCode = Convert.ToInt32(arr[1]);
                    }
                    iDepCode++;
                    sDepCode = iDepCode.ToString().PadLeft(3, '0');
                }
                sDepCode = parentcode + sDepCode;
            }
            else
            {
                if (depcode.Length > 0)
                {
                    iDepCode = Convert.ToInt32(depcode.Substring(sHead.Length)) + 1;
                    sDepCode = iDepCode.ToString().PadLeft(5, '0');
                }
                sDepCode = sHead + sDepCode;
            }
            return sDepCode;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddDepartment(DepartmentModel model, SynclogModel logmodel)
        {
            return DAL.AddDepartment(model,logmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateDepartment(DepartmentModel model, SynclogModel logmodel)
        {
            return DAL.UpdateDepartment(model,logmodel);
        }

        /// <summary>
        /// 查詢用戶權限部門資料
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="companyid"></param>
        /// <param name="modulecode"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable GetDepartmentByUser(string personcode, string companyid, string modulecode, string depcode)
        {
            return DAL.GetDepartmentByUser(personcode, companyid, modulecode, depcode);
        }

        /// <summary>
        /// 查詢已所要添加部門作為父組織的組織數
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="depcode"></param>
        /// <param name="parentdepcode"></param>
        /// <returns></returns>
        public DataTable GetCount(string companyid, string depcode, string parentdepcode)
        {
            return DAL.GetCount(companyid, depcode, parentdepcode);
        }

        /// <summary>
        /// 查詢法人資料
        /// </summary>
        /// <returns></returns>
        public DataTable GetCorporationInfo()
        {
            return DAL.GetCorporationInfo();
        }

        /// <summary>
        /// 查詢區域資料
        /// </summary>
        /// <returns></returns>
        public DataTable GetAreaInfo()
        {
            return DAL.GetAreaInfo();
        }

        /// <summary>
        /// 查詢組織層級資料
        /// </summary>
        /// <param name="personcode"></param>
        /// <returns></returns>
        public DataTable GetDepLevelInfo(string personcode)
        {
            return DAL.GetDepLevelInfo(personcode);
        }

        /// <summary>
        /// 查詢組織信息(用於組織圖)
        /// </summary>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable GetDepartmentInfo(string depcode)
        {
            return DAL.GetDepartmentInfo(depcode);
        }

        /// <summary>
        /// 查詢組織在職員工人數
        /// </summary>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable GetDepEmpCount(string depcode)
        {
            return DAL.GetDepEmpCount(depcode);
        }

        /// <summary>
        /// 查詢是否是事業群
        /// </summary>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable CheckDepCode(string depcode)
        {
            return DAL.CheckDepCode(depcode);
        }

        /// <summary>
        /// 查詢組織信息(用於組織圖A)
        /// </summary>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable GetDepartmentAInfo(string depcode)
        {
            return DAL.GetDepartmentAInfo(depcode);
        }

        /// <summary>
        /// 根據levletype查詢組織員工數
        /// </summary>
        /// <param name="depcode"></param>
        /// <returns></returns>
        public DataTable GetEmpCountByLevelType(string depcode)
        {
            return DAL.GetEmpCountByLevelType(depcode);
        }
    }
}
