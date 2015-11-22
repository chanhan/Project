/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IDepartmentDal.cs
 * 檔功能描述：組織資料數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2012.02.03
 * 
 */

using System.Collections.Generic;
using System.Data;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.HRM;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.IDAL.HRM
{
    [RefClass("HRM.DepartmentDal")]
    public interface IDepartmentDal
    {
        /// <summary>
        /// 獲得部門排序Id
        /// </summary>
        /// <returns></returns>
        DataTable GetOrderId();

        /// <summary>
        /// 根據條件查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sql"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        DataTable GetDeaprtmentPageInfo(DepartmentModel model, string levelcode, string sql, int pageIndex, int pageSize, out int totalCount);
    
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
        DataTable GetDeaprtmentForExport(DepartmentModel model, string levelcode, string sql);

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        List<DepartmentModel> GetList(DataTable dt);

        /// <summary>
        /// 查詢組織下的員工
        /// </summary>
        /// <param name="depcode"></param>
        /// <returns></returns>
        DataTable GetWorkNoByDept(string depcode);

        /// <summary>
        ///查詢子組織
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        DataTable GetDeptByParent(string companyid, string depcode);

        /// <summary>
        /// 查詢用戶層級信息
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="levelcode"></param>
        /// <returns></returns>
        DataTable GetUserDepLevel(string personcode, string levelcode);

         /// <summary>
        /// 失效
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        bool Disable(string companyid, string depcode, SynclogModel logmodel);

        /// <summary>
        /// 查詢組織信息
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        DataTable GetDept(string companyid, string depcode);

        /// <summary>
        /// 生效
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        bool Enable(string companyid, string depcode, SynclogModel logmodel);

        /// <summary>
        /// 獲得組織及其子組織信息
        /// </summary>
        /// <param name="depcode"></param>
        /// <returns></returns>
        DataTable GetDeptByParentDept(string depcode);

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Delete(DepartmentModel model, SynclogModel logmodel);

        /// <summary>
        /// 檢查費用代碼
        /// </summary>
        /// <param name="CostCode"></param>
        /// <param name="AccountEntity"></param>
        /// <param name="DepCode"></param>
        /// <returns></returns>
        DataTable GetCostCode(string CostCode, string AccountEntity, string DepCode);

        /// <summary>
        /// 查詢參數值
        /// </summary>
        /// <returns></returns>
        DataTable GetParaValue();

        /// <summary>
        /// 查詢組織的排序Id
        /// </summary>
        /// <param name="depcode"></param>
        /// <returns></returns>
        DataTable GetOrderIdByDepCode(string depcode);

        /// <summary>
        /// 查詢層級代碼的排序Id
        /// </summary>
        /// <param name="levelcode"></param>
        /// <returns></returns>
        DataTable GetOrderIdBylevelcode(string levelcode);

        /// <summary>
        /// 根據部門代碼和層級代碼檢驗信息是否存在
        /// </summary>
        /// <param name="depcode"></param>
        /// <param name="levelcode"></param>
        /// <returns></returns>
        DataTable CheckDepCodeAndLevelCode(string depcode, string levelcode);

        /// <summary>
        /// 查詢最大部門代碼
        /// </summary>
        /// <param name="levelcode"></param>
        /// <param name="parentcode"></param>
        /// <param name="outhead"></param>
        /// <returns></returns>
        string GetMaxCode(string levelcode, string parentcode, out string outhead);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddDepartment(DepartmentModel model, SynclogModel logmodel);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateDepartment(DepartmentModel model, SynclogModel logmodel);

        /// <summary>
        /// 查詢用戶權限部門資料
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="companyid"></param>
        /// <param name="modulecode"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        DataTable GetDepartmentByUser(string personcode, string companyid, string modulecode, string depcode);
        
        /// <summary>
        /// 查詢已所要添加部門作為父組織的組織數
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="depcode"></param>
        /// <param name="parentdepcode"></param>
        /// <returns></returns>
        DataTable GetCount(string companyid, string depcode, string parentdepcode);

        /// <summary>
        /// 查詢法人資料
        /// </summary>
        /// <returns></returns>
        DataTable GetCorporationInfo();

        /// <summary>
        /// 查詢區域資料
        /// </summary>
        /// <returns></returns>
        DataTable GetAreaInfo();

        /// <summary>
        /// 查詢組織層級資料
        /// </summary>
        /// <param name="personcode"></param>
        /// <returns></returns>
        DataTable GetDepLevelInfo(string personcode);

        /// <summary>
        /// 查詢組織信息(用於組織圖)
        /// </summary>
        /// <param name="depcode"></param>
        /// <returns></returns>
        DataTable GetDepartmentInfo(string depcode);

        /// <summary>
        /// 查詢組織在職員工人數
        /// </summary>
        /// <param name="depcode"></param>
        /// <returns></returns>
        DataTable GetDepEmpCount(string depcode);

        /// <summary>
        /// 查詢是否是事業群
        /// </summary>
        /// <param name="depcode"></param>
        /// <returns></returns>
        DataTable CheckDepCode(string depcode);

        /// <summary>
        /// 查詢組織信息(用於組織圖A)
        /// </summary>
        /// <param name="depcode"></param>
        /// <returns></returns>
        DataTable GetDepartmentAInfo(string depcode);

        /// <summary>
        /// 根據levletype查詢組織員工數
        /// </summary>
        /// <param name="depcode"></param>
        /// <returns></returns>
        DataTable GetEmpCountByLevelType(string depcode);
    }
}
