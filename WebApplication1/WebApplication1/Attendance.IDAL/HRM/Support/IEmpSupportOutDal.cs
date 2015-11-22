/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IEmpSupportOutDal.cs
 * 檔功能描述： 外部支援數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2012.01.04
 * 
 */

using System.Collections.Generic;
using System.Data;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.HRM.Support;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.IDAL.HRM.Support
{
    [RefClass("HRM.Support.EmpSupportOutDal")]
    public interface IEmpSupportOutDal
    {
        /// <summary>
        /// 根據條件分頁查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="LevelCondition"></param>
        /// <param name="ManagerCondition"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        DataTable GetEmpSupportOutPageInfo(EmpSupportOutModel model, string sql, string SupportDept, string LevelCondition, string ManagerCondition, int pageIndex, int pageSize, out int totalCount);

        /// <summary>
        /// 導出查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sql"></param>
        /// <param name="SupportDept"></param>
        /// <param name="LevelCondition"></param>
        /// <param name="ManagerCondition"></param>
        /// <returns></returns>
        DataTable GetEmpSupportOutForExport(EmpSupportOutModel model, string sql, string SupportDept, string LevelCondition, string ManagerCondition);

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        List<EmpSupportOutModel> GetList(DataTable dt);

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int DeleteEmpSupportOut(EmpSupportOutModel model, SynclogModel logmodel);

        /// <summary>
        /// 根據主鍵查詢
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        EmpSupportOutModel GetEmpSupportOutInfo(EmpSupportOutModel model);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddEmpSupportOut(EmpSupportOutModel model, SynclogModel logmodel);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateEmpSupportOut(EmpSupportOutModel model, SynclogModel logmodel);

        /// <summary>
        /// 獲得支援中員工
        /// </summary>
        /// <param name="workno"></param>
        /// <returns></returns>
        DataTable GetEmpSupportOut(string workno);

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
    }
}
