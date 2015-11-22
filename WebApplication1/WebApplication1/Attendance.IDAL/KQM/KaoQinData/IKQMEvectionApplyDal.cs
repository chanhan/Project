/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IKQMEvectionApplyDal.cs
 * 檔功能描述： 外出申請數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2012.02.16
 * 
 */

using GDSBG.MiABU.Attendance.Common.Attributes;
using System.Data;
using GDSBG.MiABU.Attendance.Model.KQM.KaoQinData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Collections.Generic;


namespace GDSBG.MiABU.Attendance.IDAL.KQM.KaoQinData
{
    /// <summary>
    /// 外出申請數據操作接口
    /// </summary>
    [RefClass("KQM.KaoQinData.KQMEvectionApplyDal")]
    public interface IKQMEvectionApplyDal
    {
        /// <summary>
        /// 根據傳入的model查詢相應的數據
        /// </summary>
        /// <param name="model">外出申請的model</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        DataTable GetEvectionList(KQMEvectionApplyModel model,string depCode,string sqlDep ,int pageIndex, int pageSize, out int totalCount);
        /// <summary>
        /// 根據傳入的model查詢相應的數據
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        DataTable GetEvectionList(KQMEvectionApplyModel model,string depCode,string sqlDep);
        /// <summary>
        /// 根據傳入的model查詢相應的數據
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        DataTable GetEvectionList(KQMEvectionApplyModel model);
        /// <summary>
        /// 插入一筆外出申請記錄
        /// </summary>
        /// <param name="functionId"></param>
        /// <returns>插入是否成功</returns>
        int InsertEvectionByKey(KQMEvectionApplyModel model, SynclogModel logmodel);
        /// <summary>
        /// 更新一筆外出申請記錄
        /// </summary>
        /// <param name="functionId"></param>
        /// <returns>更新是否成功</returns>
        int UpdateEvctionByKey(KQMEvectionApplyModel model, SynclogModel logmodel);
        /// <summary>
        /// 更新一筆外出申請記錄
        /// </summary>
        /// <param name="functionId"></param>
        /// <returns>更新是否成功</returns>
        int UpdateEvction(string ID, string status, KQMEvectionApplyModel model, SynclogModel logmodel);

        /// <summary>
        /// 更新一筆外出申請記錄
        /// </summary>
        /// <param name="functionId"></param>
        /// <returns>更新是否成功</returns>
        int UpdateEvction(string ID, string status, SynclogModel logmodel);
        /// <summary>
        /// 根據主鍵刪除選中的數據
        /// </summary>
        /// <param name="model"></param>
        /// <param name="logmodel"></param>
        /// <returns></returns>
        int DeleteEevctionByKey(KQMEvectionApplyModel model, SynclogModel logmodel);
        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        List<KQMEvectionApplyModel> GetList(DataTable dt);

        /// <summary>
        /// 導入
        /// </summary>
        /// <param name="personcode">登錄者工號</param>
        /// <param name="companyId">登錄者事業群代碼</param>
        /// <param name="successnum">成功插入的條數</param>
        /// <param name="errornum">錯誤條數</param>
        /// <returns></returns>
        DataTable ImpoertExcel(string personcode, out int successnum, out int errornum, SynclogModel logmodel);
        /// <summary>
        /// 根據單據類型和組織ID查詢可以簽核的最近的部門ID
        /// </summary>
        /// <param name="OrgCode">組織</param>
        /// <param name="BillTypeCode"></param>
        /// <returns></returns>
        string GetWorkFlowOrgCode(string OrgCode, string BillTypeCode);
        /// <summary>
        /// 根據數據的ID查詢單號
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DataTable GetDtByID(string id);
        /// <summary>
        /// 送簽的流程   發起表單
        /// </summary>
        /// <param name="processFlag">標識位（是新發起的表單還是簽核中）</param>
        /// <param name="ID">數據的ID</param>
        /// <param name="billNoType">單據類型</param>
        /// <param name="auditOrgCode">要簽核單位</param>
        /// <param name="billTypeCode">單據類型代碼</param>
        /// <param name="workNo">工號</param>
        /// <returns></returns>
        string SaveAuditData(string ID, string workNo, string billNoType, string billTypeCode, string ApplyMan, string auditOrgCode, string Flow_LevelRemark, SynclogModel logmodel);
        /// <summary>
        /// 組織送簽
        /// </summary>
        /// <param name="processFlag">標識位</param>
        /// <param name="diry"></param>
        /// <param name="BillNoType">單據類型</param>
        /// <param name="BillTypeCode">單據類型代碼</param>
        /// <param name="Person">簽核人</param>
        /// <returns></returns>
        int SaveOrgAuditData(string processFlag, Dictionary<string, List<string>> diry, string BillNoType, string BillTypeCode,string sFlow_LevelRemark, string Person, SynclogModel logmodel);
    }
}
