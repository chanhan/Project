/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ITWCadreDal.cs
 * 檔功能描述： 駐派幹部資料數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.16
 * 
 */

using System;
using System.Collections.Generic;
using System.Data;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.HRM;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.IDAL.HRM
{
    [RefClass("HRM.TWCadreDal")]
    public interface ITWCadreDal
    {
        /// <summary>
        /// 獲得所有資位
        /// </summary>
        /// <returns></returns>
        DataTable GetLevel();

        /// <summary>
        /// 獲得所有管理職
        /// </summary>
        /// <returns></returns>
        DataTable GetManager();

        /// <summary>
        /// 獲得所有在職狀態
        /// </summary>
        /// <returns></returns>
        DataTable GetEmpStatus();

        /// <summary>
        /// 獲得性別資料
        /// </summary>
        /// <returns></returns>
        DataTable GetSex();

        /// <summary>
        /// 根據條件查詢數據
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sql"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        DataTable GetTWCadrePageInfo(TWCadreModel model, string sql, string LevelCondition, string ManagerCondition, string StatusCondition, Nullable<DateTime> JoinDateFrom, Nullable<DateTime> JoinDateTo, Nullable<DateTime> LeaveDateFrom, Nullable<DateTime> LeaveDateTo, int pageIndex, int pageSize, out int totalCount);

        /// <summary>
        /// 導出查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sql"></param>
        /// <param name="LevelCondition"></param>
        /// <param name="ManagerCondition"></param>
        /// <param name="StatusCondition"></param>
        /// <param name="JoinDateFrom"></param>
        /// <param name="JoinDateTo"></param>
        /// <param name="LeaveDateFrom"></param>
        /// <param name="LeaveDateTo"></param>
        /// <returns></returns>
        DataTable GetTWCadreForExport(TWCadreModel model, string sql, string LevelCondition, string ManagerCondition, string StatusCondition, Nullable<DateTime> JoinDateFrom, Nullable<DateTime> JoinDateTo, Nullable<DateTime> LeaveDateFrom, Nullable<DateTime> LeaveDateTo);

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        List<TWCadreModel> GetList(DataTable dt);

        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的功能Model</param>
        /// <returns>是否成功</returns>
        bool AddTWCdare(TWCadreModel model, SynclogModel logmodel);
 
        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        bool UpdateTWCdareByKey(TWCadreModel model, SynclogModel logmodel);

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="workno">工號</param>
        /// <returns></returns>
        int DeleteTWCadre(string workno, SynclogModel logmodel);

        /// <summary>
        /// 查詢資料是否存在
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        DataTable GetTWCafreByKey(TWCadreModel model);

        /// <summary>
        /// 獲得派駐幹部資料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        TWCadreModel GetTWCadreInfoByKey(TWCadreModel model);
        
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
