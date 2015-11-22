/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IBUcalendarDal.cs
 * 檔功能描述： BU行事歷操作接口
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.12.20
 * 
 */

using System;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;


namespace GDSBG.MiABU.Attendance.IDAL.KQM.BasicData
{
    /// <summary>
    ///  BU行事歷操作接口
    /// </summary>
    [RefClass("KQM.BasicData.BUCalendarDal")]
    public interface  IBUcalendarDal
    {
        /// <summary>
        /// 根據主鍵查詢Model
        /// <param name="model">異常原因model</param>
        /// <param name="pageIndex">當前頁</param>
        /// <param name="pageSize"></param></param>
        /// <param name="totalCount">總頁數</param>
        /// <returns></returns>
        DataTable GetBUCalendarList(BUCalendarModel model, string SQLDep,string depCode, int pageIndex, int pageSize, out int totalCount);
        /// <summary>
        /// 刪除一條BU行事歷
        /// </summary>
        /// <param name="functionId">要刪除的一條BU行事歷model</param>
        /// <returns>是否執行成功</returns>
        int DeleteBUCalendarByKey(BUCalendarModel model, SynclogModel logmodel);
        /// <summary>
        /// 查詢BG行事歷中某天是不是節假日
        /// </summary>
        /// <param name="model">BU行事歷的model</param>
        /// <param name="CompanyId">公司ID</param>
        /// <returns></returns>
        DataTable GetBGCalendarByKey(BUCalendarModel model, string CompanyId);

        /// <summary>
        /// 插入BU行事歷
        /// </summary>
        /// <param name="functionId">要插入的BU行事歷model</param>
        /// <returns>插入是否成功</returns>
        int InsertBUCalendarByKey(BUCalendarModel model, SynclogModel logmodel);
        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        int UpdateBUCalendarByKey(BUCalendarModel model, BUCalendarModel newmodel, SynclogModel logmodel);

        /// <summary>
        /// 根據主鍵查詢
        /// </summary>
        /// <param name="BUCode">單位代碼</param>
        /// <param name="workDay">日期</param>
        /// <returns>該行事歷的行數</returns>
        DataTable GetBUCalendarNum(BUCalendarModel model);
        /// <summary>
        /// 導入
        /// </summary>
        /// <param name="personcode"></param>
        /// <param name="successnum"></param>
        /// <param name="errornum"></param>
        /// <returns></returns>
        DataTable ImpoertExcel(string personcode, string companyId, out int successnum, out int errornum, SynclogModel logmodel);
        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        List<BUCalendarModel> GetList(DataTable dt);
        /// <summary>
        /// 獲得選擇組織的層級
        /// </summary>
        /// <param name="depCode"></param>
        /// <returns></returns>
        string GetValue(string depCode);
    }
}
