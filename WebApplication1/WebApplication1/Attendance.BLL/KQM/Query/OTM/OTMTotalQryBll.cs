/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OTMTotalQryBll.cs
 * 檔功能描述： 加班匯總業務邏輯類
 * 
 * 版本：1.0
 * 創建標識：高子焱 2011.12.30
 * 
 */

using System.Collections.Generic;
using System.Data;
using System.Collections;
using System;
using GDSBG.MiABU.Attendance.IDAL.KQM.Query.OTM;
using GDSBG.MiABU.Attendance.Model.KQM.Query.OTM;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;


namespace GDSBG.MiABU.Attendance.BLL.KQM.Query.OTM
{
    public class OTMTotalQryBll : BLLBase<IOTMTotalQryDal>
    {
        /// <summary>
        /// 加班匯總查詢
        /// </summary>
        /// <param name="model">加班匯總model</param>
        /// <param name="BatchEmployeeNo">批量查詢工號</param>
        /// <param name="overTimeType">加班類別</param>
        /// <param name="approveFlag">簽核狀態</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetOTMQryList(OTMTotalQryModel model,string depCode,string SQLDep, string BatchEmployeeNo, string overTimeType, string approveFlag, int pageIndex, int pageSize, out int totalCount)
        {
            return DAL.GetOTMQryList(model,depCode,SQLDep, BatchEmployeeNo, overTimeType, approveFlag,pageIndex,pageSize, out totalCount);
        }
        /// <summary>
        /// 月加班匯總查詢
        /// </summary>
        /// <param name="model">月加班匯總查詢model</param>
        /// <param name="BatchEmployeeNo">批量查詢工號</param>
        /// <param name="overTimeType">加班類別</param>
        /// <param name="approveFlag">簽核狀態</param>
        /// <param name="empStatus">人员类别</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetOTMQryList(OTMTotalQryModel model, string flag, string depCode, string SQLDep, string BatchEmployeeNo, string overTimeType, string approveFlag, string empStatus, int pageIndex, int pageSize, out int totalCount)
        {
            return DAL.GetOTMQryList(model,flag, depCode, SQLDep, BatchEmployeeNo, overTimeType, approveFlag, empStatus, pageIndex, pageSize, out totalCount);
        }
        /// <summary>
        /// 加班匯總查詢(導出)
        /// </summary>
        /// <param name="model">月加班匯總查詢model</param>
        /// <param name="BatchEmployeeNo">批量查詢工號</param>
        /// <param name="overTimeType">加班類別</param>
        /// <param name="approveFlag">簽核狀態</param>
        /// <returns></returns>
        public DataTable GetOTMQryList(OTMTotalQryModel model, string depCode, string SQLDep, string BatchEmployeeNo, string overTimeType, string approveFlag)
        {
            return DAL.GetOTMQryList(model, depCode, SQLDep, BatchEmployeeNo, overTimeType, approveFlag);
        }
        /// <summary>
        /// 月加班匯總查詢(導出)
        /// </summary>
        /// <param name="model">月加班匯總查詢model</param>
        /// <param name="BatchEmployeeNo">批量查詢工號</param>
        /// <param name="overTimeType">加班類別</param>
        /// <param name="approveFlag">簽核狀態</param>
        /// <param name="empStatus">人员类别</param>
        /// <returns></returns>
        public DataTable GetOTMQryList(OTMTotalQryModel model, string flag, string depCode, string SQLDep, string BatchEmployeeNo, string overTimeType, string approveFlag, string empStatus)
        {
            return DAL.GetOTMQryList(model,flag, depCode, SQLDep, BatchEmployeeNo, overTimeType, approveFlag, empStatus);
        }
        /// <summary>
        /// 返回ModelLIst  導出
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<OTMTotalQryModel> GetList(DataTable dt)
        {
            return DAL.GetList(dt);
        }
        /// <summary>
        /// 查詢單人信息（修改頁面）
        /// </summary>
        /// <param name="model">月加班匯總查詢model</param>
        /// <returns></returns>
        public DataTable GetOTMQryList(OTMTotalQryModel model,string SQLDep)
        {
            return DAL.GetOTMQryList(model, SQLDep);
        }
        /// <summary>
        /// 查詢單人信息（修改頁面保存按鈕）
        /// </summary>
        /// <param name="model">月加班匯總查詢model</param>
        /// <returns></returns>
        public DataTable GetOTMQryList(OTMTotalQryModel model)
        {
            return DAL.GetOTMQryList(model);
        }
        /// <summary>
        /// 驗證修改部份的信息 
        /// </summary>
        /// <param name="processFlag">標識位</param>
        /// <param name="dt">需要修改的datatable</param>
        /// <returns></returns>
        public bool SaveData(string processFlag,string personCode,DataTable dt)
        {
            return DAL.SaveData(processFlag,personCode,dt);
        }
        /// <summary>
        /// 更改對應的信息（gds_att_monthotal表等）
        /// </summary>
        /// <param name="workNo">工號  主鍵1</param>
        /// <param name="yearMonth">年月  主鍵2</param>
        public void CountCanAdjlasthy(string workNo,string yearMonth)
        {
             DAL.CountCanAdjlasthy(workNo, yearMonth);
        }
        /// <summary>
        /// 組織計算 
        /// </summary>
        /// <param name="sWorkNo">工號</param>
        /// <param name="YearMonth">年月</param>
        /// <param name="sDCode">部門代碼</param>
        public void CountCanAdjlasthy(string sWorkNo, string YearMonth, string sDCode)
        {
            DAL.CountCanAdjlasthy(sWorkNo, YearMonth, sDCode);
        }
        /// <summary>
        /// 查詢固定信息（是否是專案加班）
        /// </summary>
        /// <returns></returns>
        public string GetParemeterValue()
        {
            return DAL.GetParemeterValue();
        }
        /// <summary>
        /// 核准的保存按鈕
        /// </summary>
        /// <param name="workNo">工號</param>
        /// <param name="yearMonth">年月</param>
        /// <param name="Status">狀態</param>
        /// <param name="model">加班匯總查詢model</param>
        /// <param name="logmodel">管理日誌的model</param>
        /// <returns></returns>
        public int UpdateMonthTal(string workNo, string yearMonth, string status, OTMTotalQryModel model, SynclogModel logmodel)
        {
            return DAL.UpdateMonthTal(workNo, yearMonth, status,model, logmodel);
        }
        /// <summary>
        /// 導入
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="successnum"></param>
        /// <param name="errornum"></param>
        /// <returns></returns>
        public DataTable ImpoertExcel(string personcode,string sqlDep, out int successnum, out int errornum, SynclogModel logmodel)
        {
            return DAL.ImpoertExcel(personcode, sqlDep,out successnum,out errornum, logmodel);
        }
        /// <summary>
        /// 獲取數據源 ---導入
        /// </summary>
        /// <param name="strFilePath"></param>
        /// <returns></returns>
        public DataView ExceltoDataView(string strFilePath)
        {
            return DAL.ExceltoDataView(strFilePath);
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
        /// 送簽的流程   發起表單
        /// </summary>
        /// <param name="processFlag">標識位（是新發起的表單還是簽核中）</param>
        /// <param name="ID">數據的ID</param>
        /// <param name="billNoType">單據類型</param>
        /// <param name="auditOrgCode">要簽核單位</param>
        /// <param name="billTypeCode">單據類型代碼</param>
        /// <param name="applyMan">申請人</param>
        /// <returns></returns>
        public string SaveAuditData(string processFlag, string WorkNo, string YearMonth, string billNoType, string auditOrgCode, string billTypeCode, string applyMan, string QueryFlag, SynclogModel logmodel)
        {
            return DAL.SaveAuditData(processFlag, WorkNo, YearMonth, billNoType, auditOrgCode, billTypeCode,applyMan,QueryFlag,logmodel);
        }
        /// <summary>
        /// 根據數據的ID查詢單號
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetDtByID(string WorkNo,string  YearMonth)
        {
            return DAL.GetDtByID(WorkNo,YearMonth);
        }
        /// <summary>
        /// 組織送簽
        /// </summary>
        /// <param name="processFlag">標識位</param>
        /// <param name="diry"></param>
        /// <param name="BillNoType">單據類型</param>
        /// <param name="BillTypeCode">單據類型代碼</param>
        /// <param name="Person">簽核人</param>
        /// <returns></returns>
        public int SaveOrgAuditData(string processFlag, Dictionary<string, List<OTMTotalQryModel>> diry, string BillNoType, string BillTypeCode, string Person, SynclogModel logmodel)
        {
            return DAL.SaveOrgAuditData(processFlag, diry, BillNoType, BillTypeCode, Person,logmodel);
        }
    }
}
