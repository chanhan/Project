/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： INetSignInSetDal.cs
 * 檔功能描述： 網上簽到名單設定數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.12
 * 
 */

using System;
using System.Collections.Generic;
using System.Data;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.IDAL.KQM.BasicData
{
    /// <summary>
    /// 網上簽到名單設定數據操作接口
    /// </summary>
    [RefClass("KQM.BasicData.NetSignInSetDal")]
    public interface INetSignInSetDal
    {
        /// <summary>
        /// 根據條件查詢數據
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        DataTable GetSignEmpPageInfo(NetSignInSetModel model,string sql, int pageIndex, int pageSize, out int totalCount);

        /// <summary>
        /// 導出查詢
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        DataTable GetSignEmpForExport(NetSignInSetModel model, string sql);

        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的功能Model</param>
        /// <returns>是否成功</returns>
        bool AddNetSignInEmp(NetSignInSetModel model, SynclogModel logmodel);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="oldmodel"></param>
        /// <param name="model"></param>
        /// <param name="logmodel"></param>
        /// <returns></returns>
        bool UpdateNetSignInEmpByKey(NetSignInSetModel oldmodel, NetSignInSetModel model, SynclogModel logmodel);

        /// <summary>
        /// 根據主鍵查詢model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        NetSignInSetModel GetNetSignInEmpByKey(NetSignInSetModel model);

        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="workno"></param>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        DataTable GetNetSignInEmp(string workno, Nullable<DateTime> startdate, Nullable<DateTime> enddate);
        
        /// <summary>
        /// 修改查詢
        /// </summary>
        /// <param name="workno"></param>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        DataTable GetNetSignInEmpForModify(string workno, Nullable<DateTime> startdate, Nullable<DateTime> enddate, Nullable<DateTime> oldstartdate, Nullable<DateTime> oldenddate);

        /// <summary>
        /// 根據工號是否存在
        /// </summary>
        /// <param name="workno"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        DataTable GetWorkNoInfoByUser(string workno, string sql);

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="workno">工號</param>
        /// <returns></returns>
        int DeleteNetSignInEmployee(string workno, string startenddate, SynclogModel logmodel);

        /// <summary>
        /// 獲得當前用戶在臨時表中創建的所有數據
        /// </summary>
        /// <param name="createuser"></param>
        /// <returns></returns>
        DataTable GetAllTempNetSignInEmp(string createuser);

        /// <summary>
        /// 導入
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="successnum"></param>
        /// <param name="errornum"></param>
        /// <returns></returns>
        DataTable ImportExcel(string personcode, string modulecode, out int successnum, out int errornum, SynclogModel logmodel);

        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        List<NetSignInSetModel> GetList(DataTable dt);
    }
}
