
/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IKqmRulesDal.cs
 * 檔功能描述： 缺勤規則數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.8
 * 
 */

using System;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.KQM.OTM;
using System.Data;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.IDAL.KQM.OTM
{
    /// <summary>
    /// 缺勤規則數據操作接口
    /// </summary>
    [RefClass("KQM.OTM.OTMActivityApplyDal")]
    public interface IOTMActivityApplyDal
    {

        /// <summary>
        /// 獲取審核轉檯(未審核,已審核)
        /// </summary>
        /// <returns></returns>
        DataTable GetOTStatus();

        /// <summary>
        /// 分頁查詢免卡人員加班導入資料
        /// </summary>
        /// <param name="model"></param>
        /// <param name="?"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        DataTable GetActivityApplyList(ActivityModel model,string sqlDep, string depCode, string workNoStrings, string dateFrom, string dateTo, int pageIndex, int pageSize, out int totalCount);

        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的免卡人員加班導入Model</param>
        /// <returns>是否成功</returns>
        bool AddActivityApply(ActivityModel model,SynclogModel logmodel);

        /// <summary>
        /// 獲取員工資料
        /// </summary>
        /// <returns></returns>
        DataTable GetEmp(string empNo, string sqlDep);

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt">徐轉化的Datatable</param>
        /// <returns></returns>
        List<ActivityModel> GetList(DataTable dt);

        /// <summary>
        /// 刪除免卡人員加班信息
        /// </summary>
        /// <param name="model"> 要刪除的model</param>
        /// <returns></returns>
        int DeleteActivity(ActivityModel model,SynclogModel logmodel);

        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        bool UpdateActivityByKey(ActivityModel model,SynclogModel logmodel);

        /// <summary>
        /// 獲取員工某月加班時數
        /// </summary>
        /// <returns></returns>
        DataTable GetMonthAllOverTime(string empNo, string yearMonth);

        /// <summary>
        /// 獲取加班類型
        /// </summary>
        /// <param name="sWorkNo"></param>
        /// <param name="sDate"></param>
        /// <returns></returns>
        string GetOTType(string workNo, string date);


        /// <summary>
        /// 獲取OTMFlag
        /// </summary>
        /// <param name="WorkNo"></param>
        /// <param name="OtDate"></param>
        /// <returns></returns>
        string GetOTMFlag(string WorkNo, string OtDate);

        /// <summary>
        /// 獲取員工信息
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <returns></returns>
        DataTable GetVData(string EmployeeNo, string sqlDep);


        /// <summary>
        /// 獲取 LHZBData
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        DataTable GetLHZBData(string EmployeeNo, string ID, string otDate, string flag);


        /// <summary>
        /// 刪除數據
        /// </summary>
        /// <param name="dataTable"></param>
        void LHZBDeleteData(DataTable dataTable,SynclogModel logmodel);

        /// <summary>
        /// 核准
        /// </summary>
        /// <param name="dataTable"></param>
        void LHZBAudit(DataTable dataTable,SynclogModel logmodel);


        /// <summary>
        /// 獲取ParaValue
        /// </summary>
        /// <returns></returns>
        string GetValue(string flag, ActivityModel moveShiftModel);

        /// <summary>
        /// 取消核准
        /// </summary>
        /// <param name="dataTable"></param>
        void LHZBCancelAudit(DataTable dataTable,SynclogModel logmodel);

        /// <summary>
        /// 根據SQL查詢信息
        /// </summary>
        /// <param name="strShiftNo"></param>
        /// <returns></returns>
        DataTable GetDataTableBySQL(string flag, ActivityModel model);

        /// <summary>
        /// 獲取ShiftNo
        /// </summary>
        /// <param name="sWorkNo"></param>
        /// <param name="sDate"></param>
        /// <returns></returns>
        string GetShiftNo(string sWorkNo, string sDate);


        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="OtDate"></param>
        /// <param name="WorkNo"></param>
        /// <returns></returns>
        string GetByIndex(string OtDate, string WorkNo);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="processFlag"></param>
        /// <param name="dataTable"></param>
        void LHZBSaveData(string processFlag, DataTable dataTable, string loginID,SynclogModel logmodel);


        /// <summary>
        /// 查詢免卡人員加班導入資料(不分頁)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="?"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        DataTable GetActivityApplyList(ActivityModel model, string sqlDep, string depCode, string workNoStrings, string dateFrom, string dateTo);


        string SaveAuditData(string processFlag, string ID, string BillNoType, string AuditOrgCode);

        DataTable getAuditBillInfoByBillNo(string billno, string status);

        /// <summary>
        /// 組織送簽
        /// </summary>
        /// <param name="processFlag"></param>
        /// <param name="diry"></param>
        /// <param name="BillNoType"></param>
        /// <param name="BillTypeCode"></param>
        /// <param name="Person"></param>
        /// <returns></returns>
        int SaveOrgAuditData(string processFlag, Dictionary<string, List<string>> diry, string BillNoType, string BillTypeCode, string Person, SynclogModel logmodel);
        bool SaveAuditData(string WorkNo, string BillNoType, string BillTypeCode, string ApplyMan, string AuditOrgCode, string Flow_LevelRemark, SynclogModel logmodel);

        DataTable getActivityApplyList(string depName, string sqlDep, string depCode, string workNoStrings, string dateFrom, string dateTo, string condition, int pageIndex, int pageSize, out int totalCount);
    
    }

}
