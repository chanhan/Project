/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： BUCalendarBll.cs
 * 檔功能描述： BU行事歷業務邏輯類
 * 
 * 版本：1.0
 * 創建標識：高子焱 2011.12.20
 * 
 */

using System.Collections.Generic;
using GDSBG.MiABU.Attendance.IDAL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using System.Data;
using System.Collections;
using System;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.BLL.KQM.BasicData
{
    public class BUCalendarBll : BLLBase<IBUcalendarDal>
    {
        /// <summary>
        /// 根據主鍵查詢Model
        /// <param name="model">異常原因model</param>
        /// <param name="orderType">排序</param>
        /// <param name="pageIndex">當前頁</param>
        /// <param name="pageSize"></param></param>
        /// <param name="totalCount">總頁數</param>
        /// <returns></returns>
        public DataTable GetBUCalendarList(BUCalendarModel model,string SQLDep,string depCode, int pageIndex, int pageSize, out int totalCount)
        {
            return DAL.GetBUCalendarList(model,SQLDep,depCode, pageIndex, pageSize, out totalCount);
        }
        /// <summary>
        /// 刪除一條BU行事歷
        /// </summary>
        /// <param name="functionId">要刪除的一條BU行事歷model</param>
        /// <returns>是否執行成功</returns>
        public int DeleteBUCalendarByKey(BUCalendarModel model, SynclogModel logmodel)
        {
            return DAL.DeleteBUCalendarByKey(model,logmodel);
        }
        /// <summary>
        /// 查詢BG行事歷中某天是不是節假日
        /// </summary>
        /// <param name="model">BU行事歷的model</param>
        /// <param name="CompanyId">公司ID</param>
        /// <returns></returns>
        public DataTable GetBGCalendarByKey(BUCalendarModel model, string CompanyId)
        {
            return DAL.GetBGCalendarByKey(model, CompanyId);
        }
        /// <summary>
        /// 插入BU行事歷
        /// </summary>
        /// <param name="functionId">要插入的BU行事歷model</param>
        /// <returns>插入是否成功</returns>
        public int InsertBUCalendarByKey(BUCalendarModel model, SynclogModel logmodel)
        {
            return DAL.InsertBUCalendarByKey(model,logmodel);
        }
        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        public int UpdateBUCalendarByKey(BUCalendarModel model, BUCalendarModel newmodel, SynclogModel logmodel)
        {

            return DAL.UpdateBUCalendarByKey(model, newmodel,logmodel);
        }
        /// <summary>
        /// 根據主鍵查詢
        /// </summary>
        /// <param name="BUCode">單位代碼</param>
        /// <param name="workDay">日期</param>
        /// <returns>該行事歷的行數</returns>
        public DataTable GetBUCalendarNum(BUCalendarModel model)
        {
            return DAL.GetBUCalendarNum(model);
        }
        /// <summary>
        /// 導入
        /// </summary>
        /// <param name="personcode">登錄者工號</param>
        /// <param name="companyId">登錄者事業群代碼</param>
        /// <param name="successnum">成功插入的條數</param>
        /// <param name="errornum">錯誤條數</param>
        /// <returns></returns>
        public DataTable ImpoertExcel(string personcode, string companyId, out int successnum, out int errornum, SynclogModel logmodel)
        {
            return DAL.ImpoertExcel(personcode,companyId, out successnum, out errornum,logmodel);
        }
        /// <summary>
        /// 將datatable轉換為List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<BUCalendarModel> GetList(DataTable dt)
        {
            return DAL.GetList(dt);
        }
        /// <summary>
        /// 獲得選擇組織的層級
        /// </summary>
        /// <param name="depCode"></param>
        /// <returns></returns>
        public string GetValue(string depCode)
        {
            return DAL.GetValue(depCode);
        }
    }

}
