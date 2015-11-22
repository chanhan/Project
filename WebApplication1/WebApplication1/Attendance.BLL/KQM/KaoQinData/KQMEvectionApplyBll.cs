/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMEvectionApplyBll.cs
 * 檔功能描述：外出申請業務邏輯類
 * 
 * 版本：1.0
 * 創建標識：高子焱 2012.02.16
 * 
 */
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.IDAL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using System.Data;
using System.Collections;
using System;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.IDAL.KQM.KaoQinData;
using GDSBG.MiABU.Attendance.Model.KQM.KaoQinData;

namespace GDSBG.MiABU.Attendance.BLL.KQM.KaoQinData
{
    public class KQMEvectionApplyBll : BLLBase<IKQMEvectionApplyDal>
    {
        /// <summary>
        /// 根據傳入的model查詢相應的數據
        /// </summary>
        /// <param name="model">外出申請的model</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetEvectionList(KQMEvectionApplyModel model, string depCode, string sqlDep, int pageIndex, int pageSize, out int totalCount)
        {
            return DAL.GetEvectionList(model,depCode,sqlDep, pageIndex, pageSize, out totalCount);
        }
        /// <summary>
        /// 根據傳入的model查詢相應的數據
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataTable GetEvectionList(KQMEvectionApplyModel model, string depCode, string sqlDep)
        {
            return DAL.GetEvectionList(model, depCode,  sqlDep);
        }
        /// <summary>
        /// 根據傳入的model查詢相應的數據
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataTable GetEvectionList(KQMEvectionApplyModel model)
        {
            return DAL.GetEvectionList(model);
        }
        /// <summary>
        /// 插入一筆外出申請記錄
        /// </summary>
        /// <param name="functionId"></param>
        /// <returns>插入是否成功</returns>
        public int InsertEvectionByKey(KQMEvectionApplyModel model, SynclogModel logmodel)
        {
            return DAL.InsertEvectionByKey(model, logmodel);
        }
        /// <summary>
        /// 更新一筆外出申請記錄
        /// </summary>
        /// <param name="functionId"></param>
        /// <returns>更新是否成功</returns>
        public int UpdateEvctionByKey(KQMEvectionApplyModel model, SynclogModel logmodel)
        {
            return DAL.UpdateEvctionByKey(model, logmodel);
        }
        /// <summary>
        /// 更新一筆外出申請記錄
        /// </summary>
        /// <param name="functionId"></param>
        /// <returns>更新是否成功</returns>
        public int UpdateEvction(string ID,string status, KQMEvectionApplyModel model,SynclogModel logmodel)
        {
            return DAL.UpdateEvction(ID,status,model,logmodel);
        }
        /// <summary>
        /// 更新一筆外出申請記錄
        /// </summary>
        /// <param name="functionId"></param>
        /// <returns>更新是否成功</returns>
        public int UpdateEvction(string ID, string status, SynclogModel logmodel)
        {
            return DAL.UpdateEvction(ID, status, logmodel);
        }
        /// <summary>
        /// 根據主鍵刪除選中的數據
        /// </summary>
        /// <param name="model"></param>
        /// <param name="logmodel"></param>
        /// <returns></returns>
        public int DeleteEevctionByKey(KQMEvectionApplyModel model, SynclogModel logmodel)
        {
            return DAL.DeleteEevctionByKey(model, logmodel);
        }
        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<KQMEvectionApplyModel> GetList(DataTable dt)
        {
            return DAL.GetList(dt);
        }
        /// <summary>
        /// 導入
        /// </summary>
        /// <param name="personcode">登錄者工號</param>
        /// <param name="companyId">登錄者事業群代碼</param>
        /// <param name="successnum">成功插入的條數</param>
        /// <param name="errornum">錯誤條數</param>
        /// <returns></returns>
        public DataTable ImpoertExcel(string personcode, out int successnum, out int errornum, SynclogModel logmodel)
        {
            return DAL.ImpoertExcel(personcode, out successnum, out errornum, logmodel);
        }
        /// <summary>
        /// 根據單據類型和組織ID查詢可以簽核的最近的部門ID
        /// </summary>
        /// <param name="OrgCode">組織</param>
        /// <param name="BillTypeCode"></param>
        /// <returns></returns>
        public string GetWorkFlowOrgCode(string OrgCode, string BillTypeCode)
        {
            return DAL.GetWorkFlowOrgCode(OrgCode, BillTypeCode);
        }
        /// <summary>
        /// 根據數據的ID查詢單號
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetDtByID(string id)
        {
            return DAL.GetDtByID(id);
        }
        /// <summary>
        /// 送簽的流程   發起表單
        /// </summary>
        /// <param name="processFlag">標識位（是新發起的表單還是簽核中）</param>
        /// <param name="ID">數據的ID</param>
        /// <param name="billNoType">單據類型</param>
        /// <param name="auditOrgCode">要簽核單位</param>
        /// <param name="billTypeCode">單據類型代碼</param>
        /// <param name="ApplyMan">申請人</param>
        /// <returns></returns>
        public string SaveAuditData(string ID, string WorkNo, string billNoType, string billTypeCode, string ApplyMan, string auditOrgCode, string Flow_LevelRemark, SynclogModel logmodel)
        {
            return DAL.SaveAuditData(ID, WorkNo, billNoType, billTypeCode, ApplyMan,auditOrgCode, Flow_LevelRemark, logmodel);
        }
        /// <summary>
        /// 組織送簽
        /// </summary>
        /// <param name="processFlag">標識位</param>
        /// <param name="diry"></param>
        /// <param name="BillNoType">單據類型</param>
        /// <param name="BillTypeCode">單據類型代碼</param>
        /// <param name="Person">申請人</param>
        /// <returns></returns>
        public int SaveOrgAuditData(string processFlag, Dictionary<string, List<string>> diry, string BillNoType, string BillTypeCode, string sFlow_LevelRemark ,string Person, SynclogModel logmodel)
        {
            return DAL.SaveOrgAuditData(processFlag, diry, BillNoType, BillTypeCode,sFlow_LevelRemark, Person,logmodel);
        }
    }
}
