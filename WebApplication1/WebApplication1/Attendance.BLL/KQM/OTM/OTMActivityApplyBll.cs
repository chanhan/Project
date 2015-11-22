

/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OTMActivityApplyBll.cs
 * 檔功能描述：免卡人員加班導入業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.8
 * 
 */
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.IDAL.KQM.OTM;
using GDSBG.MiABU.Attendance.Model.KQM.OTM;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using System.Data;

namespace GDSBG.MiABU.Attendance.BLL.KQM.OTM
{
    public class OTMActivityApplyBll : BLLBase<IOTMActivityApplyDal>
    {
        /// <summary>
        /// 獲取審核轉檯(未審核,已審核)
        /// </summary>
        /// <returns></returns>
        public DataTable GetOTStatus()
        {
            return DAL.GetOTStatus();
        }

        /// <summary>
        /// 分頁查詢免卡人員加班導入資料
        /// </summary>
        /// <param name="model"></param>
        /// <param name="?"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetActivityApplyList(ActivityModel model, string sqlDep, string depCode, string workNoStrings, string dateFrom, string dateTo, int pageIndex, int pageSize, out int totalCount)
        {
            return DAL.GetActivityApplyList(model, sqlDep, depCode, workNoStrings, dateFrom, dateTo, pageIndex, pageSize, out totalCount);
        }

        public DataTable getActivityApplyList(string depName, string sqlDep, string depCode, string workNoStrings, string dateFrom, string dateTo, string condition, int pageIndex, int pageSize, out int totalCount)
        {
            return DAL.getActivityApplyList(depName,  sqlDep,  depCode,  workNoStrings,  dateFrom,  dateTo,  condition,  pageIndex,  pageSize, out  totalCount);
        }
        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的免卡人員加班導入Model</param>
        /// <returns>是否成功</returns>
        public bool AddActivityApply(ActivityModel model,SynclogModel logmodel)
        {
            return DAL.AddActivityApply(model,logmodel );
        }

        /// <summary>
        /// 獲取員工資料
        /// </summary>
        /// <returns></returns>
        public DataTable GetEmp(string empNo, string sqlDep)
        {
            return DAL.GetEmp(empNo, sqlDep);
        }

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<ActivityModel> GetList(DataTable dt)
        {
            return DAL.GetList(dt);
        }

        /// <summary>
        /// 刪除免卡人員加班信息
        /// </summary>
        /// <param name="model"> 要刪除的model</param>
        /// <returns></returns>
        public int DeleteActivity(ActivityModel model,SynclogModel logmodel)
        {
            return DAL.DeleteActivity(model,logmodel);
        }


        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        public bool UpdateActivityByKey(ActivityModel model,SynclogModel logmodel)
        {
            return DAL.UpdateActivityByKey(model,logmodel);
        }


        /// <summary>
        /// 獲取員工某月加班時數
        /// </summary>
        /// <returns></returns>
        public DataTable GetMonthAllOverTime(string empNo, string yearMonth)
        {
            return DAL.GetMonthAllOverTime(empNo, yearMonth);
        }

        /// <summary>
        /// 獲取加班類型
        /// </summary>
        /// <param name="sWorkNo"></param>
        /// <param name="sDate"></param>
        /// <returns></returns>
        public string GetOTType(string workNo, string date)
        {
            return DAL.GetOTType(workNo, date);
        }

        /// <summary>
        /// 獲取OTMFlag
        /// </summary>
        /// <param name="WorkNo"></param>
        /// <param name="OtDate"></param>
        /// <returns></returns>
        public string GetOTMFlag(string WorkNo, string OtDate)
        {

            return DAL.GetOTMFlag(WorkNo, OtDate);

        }

        /// <summary>
        /// 獲取員工信息
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <returns></returns>
        public DataTable GetVData(string EmployeeNo,string sqlDep)
        {
            return DAL.GetVData(EmployeeNo, sqlDep);
        }

        /// <summary>
        /// 獲取 LHZBData
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public DataTable GetLHZBData(string EmployeeNo, string ID, string otDate, string flag)
        {
            return DAL.GetLHZBData(EmployeeNo, ID, otDate, flag);
        }

        /// <summary>
        /// 刪除數據
        /// </summary>
        /// <param name="dataTable"></param>
        public void LHZBDeleteData(DataTable dataTable,SynclogModel logmodel)
        {
            DAL.LHZBDeleteData(dataTable,logmodel);
        }

        /// <summary>
        /// 核准
        /// </summary>
        /// <param name="dataTable"></param>
        public void LHZBAudit(DataTable dataTable,SynclogModel logmodel)
        {
            DAL.LHZBAudit(dataTable,logmodel);
        }

        /// <summary>
        /// 獲取ParaValue
        /// </summary>
        /// <returns></returns>
        public string GetValue(string flag, ActivityModel moveShiftModel)
        {
            return DAL.GetValue(flag, moveShiftModel);
        }

        /// <summary>
        /// 取消核准
        /// </summary>
        /// <param name="dataTable"></param>
        public void LHZBCancelAudit(DataTable dataTable,SynclogModel logmodel)
        {
            DAL.LHZBCancelAudit(dataTable,logmodel);
        }

        /// <summary>
        /// 根據SQL查詢信息
        /// </summary>
        /// <param name="strShiftNo"></param>
        /// <returns></returns>
        public DataTable GetDataTableBySQL(string flag, ActivityModel moveShiftModel)
        {
            return DAL.GetDataTableBySQL(flag, moveShiftModel);
        }

        /// <summary>
        /// 獲取ShiftNo
        /// </summary>
        /// <param name="sWorkNo"></param>
        /// <param name="sDate"></param>
        /// <returns></returns>
        public string GetShiftNo(string sWorkNo, string sDate)
        {
            return DAL.GetShiftNo(sWorkNo, sDate);
        }


        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="OtDate"></param>
        /// <param name="WorkNo"></param>
        /// <returns></returns>
        public string GetByIndex(string OtDate, string WorkNo)
        {
            return DAL.GetByIndex(OtDate, WorkNo);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="processFlag"></param>
        /// <param name="dataTable"></param>
        public void LHZBSaveData(string processFlag, DataTable dataTable, string loginID,SynclogModel logmodel)
        {
            DAL.LHZBSaveData(processFlag, dataTable, loginID,logmodel);

        }


        /// <summary>
        /// 查詢免卡人員加班導入資料(不分頁)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="?"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetActivityApplyList(ActivityModel model, string sqlDep, string depCode, string workNoStrings, string dateFrom, string dateTo)
        {
            return DAL.GetActivityApplyList(model, sqlDep, depCode, workNoStrings, dateFrom, dateTo);
        }


        /// <summary>
        /// 送簽
        /// </summary>
        /// <param name="processFlag"></param>
        /// <param name="ID"></param>
        /// <param name="BillNoType"></param>
        /// <param name="AuditOrgCode"></param>
        /// <returns></returns>
        public string SaveAuditData(string processFlag, string ID, string BillNoType, string AuditOrgCode)
        {
            return DAL.SaveAuditData(processFlag, ID, BillNoType, AuditOrgCode);
        }


        public DataTable getAuditBillInfoByBillNo(string billno, string status)
        {
            return DAL.getAuditBillInfoByBillNo(billno, status);
        }

        /// <summary>
        /// 組織送簽
        /// </summary>
        /// <param name="processFlag"></param>
        /// <param name="diry"></param>
        /// <param name="BillNoType"></param>
        /// <param name="BillTypeCode"></param>
        /// <param name="Person"></param>
        /// <returns></returns>
        public int SaveOrgAuditData(string processFlag, Dictionary<string, List<string>> diry, string BillNoType, string BillTypeCode, string Person, SynclogModel logmodel)
        {
            return DAL.SaveOrgAuditData(processFlag, diry, BillNoType, BillTypeCode, Person,  logmodel);
        }
        public bool SaveAuditData(string WorkNo, string BillNoType, string BillTypeCode, string ApplyMan, string AuditOrgCode, string Flow_LevelRemark, SynclogModel logmodel)
        {
            return DAL.SaveAuditData( WorkNo,  BillNoType,  BillTypeCode,  ApplyMan,  AuditOrgCode,  Flow_LevelRemark,  logmodel);
        }
    }
}
 