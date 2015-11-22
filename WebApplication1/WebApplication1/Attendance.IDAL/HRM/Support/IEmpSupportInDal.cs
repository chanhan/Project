/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IEmpSupportInDal.cs
 * 檔功能描述： 內部支援數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2012.01.06
 * 
 */

using System.Collections.Generic;
using System.Data;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.HRM.Support;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.IDAL.HRM.Support
{
    [RefClass("HRM.Support.EmpSupportInDal")]
    public interface IEmpSupportInDal
    {
        /// <summary>
        /// 根據條件分頁查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="LevelCondition"></param>
        /// <param name="ManagerCondition"></param>
        /// <param name="StartDateFrom"></param>
        /// <param name="StartDateTo"></param>
        /// <param name="PrepEndDateFrom"></param>
        /// <param name="PreEndDateTo"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        DataTable GetEmpSupportInPageInfo(EmpSupportInModel model, string SupportDept, string LevelCondition, string ManagerCondition, string StartDateFrom, string StartDateTo, string PrepEndDateFrom, string PrepEndDateTo, int pageIndex, int pageSize, out int totalCount);
        
        /// <summary>
        /// 導出查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sql"></param>
        /// <param name="SupportDept"></param>
        /// <param name="LevelCondition"></param>
        /// <param name="ManagerCondition"></param>
        /// <param name="StartDateFrom"></param>
        /// <param name="StartDateTo"></param>
        /// <param name="PrepEndDateFrom"></param>
        /// <param name="PrepEndDateTo"></param>
        /// <returns></returns>
        DataTable GetEmpSupportInForExport(EmpSupportInModel model, string SupportDept, string LevelCondition, string ManagerCondition, string StartDateFrom, string StartDateTo, string PrepEndDateFrom, string PrepEndDateTo);

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        List<EmpSupportInModel> GetList(DataTable dt);

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int DeleteEmpSupportIn(EmpSupportInModel model, SynclogModel logmodel);

        /// <summary>
        /// 根據主鍵查詢
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        EmpSupportInModel GetEmpSupportInInfo(EmpSupportInModel model);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddEmpSupportIn(EmpSupportInModel model, SynclogModel logmodel);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateEmpSupportIn(EmpSupportInModel model, SynclogModel logmodel);

        /// <summary>
        /// 獲得員工資料
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <returns></returns>
        DataTable GetEmp(string EmployeeNo);

        /// <summary>
        /// 獲得內部支援員工資料
        /// </summary>
        /// <param name="EmployeeNo"></param>
        /// <returns></returns>
        DataTable GetEmpSupportIn(string EmployeeNo);

        /// <summary>
        /// 獲得員工在職狀態
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        DataTable GetEmpStatus(string workno);

        /// <summary>
        /// 獲得員工的支援順序號
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        DataTable GetEmpsupportOrder(string workno);

        /// <summary>
        /// 由工號和支援序號查詢工號
        /// </summary>
        /// <param name="workno"></param>
        /// <param name="supportorder"></param>
        /// <returns></returns>
        DataTable GetEmpSupportByWorkNoAndOrder(string workno, string supportorder);

        /// <summary>
        /// 導入
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="successnum"></param>
        /// <param name="errornum"></param>
        /// <returns></returns>
        DataTable ImpoertExcel(string personcode, out int successnum, out int errornum, SynclogModel logmodel);

        /// <summary>
        /// 放大鏡部門查詢
        /// </summary>
        /// <param name="depname"></param>
        /// <param name="depcode"></param>
        /// <returns></returns>
        DataTable GetDataByCondition(string modulecode, string sql, string depname, string depcode);
    }
}
