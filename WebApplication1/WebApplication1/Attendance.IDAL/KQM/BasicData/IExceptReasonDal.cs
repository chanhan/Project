/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IExceptReasonDal.cs
 * 檔功能描述： 異常原因數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2011.12.15
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
    ///  異常原因數據操作接口
    /// </summary>
    [RefClass("KQM.BasicData.ExceptReasonDal")]
    public interface IExceptReasonDal
    {
        /// <summary>
        /// 根據主鍵查詢Model
        /// <param name="model">班別定義model</param>
        /// <param name="orderType">排序</param>
        /// <param name="pageIndex">當前頁</param>
        /// <param name="pageSize"></param></param>
        /// <param name="totalCount">總頁數</param>
        /// <returns></returns>
        DataTable GetExceptList(ExceptReasonModel model, string orderType, int pageIndex, int pageSize, out int totalCount);
        /// <summary>
        /// 刪除一條異常原因記錄
        /// </summary>
        /// <param name="functionId">要刪除的異常原因的model</param>
        /// <returns>是否執行成功</returns>
        int DeleteExceptByKey(ExceptReasonModel model, SynclogModel logmodel);
        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        bool UpdateExceptByKey(ExceptReasonModel model, SynclogModel logmodel);
        /// <summary>
        /// 根據主鍵查詢Model
        /// <param name="model">異常原因model</param>
        /// <returns></returns>
        DataTable GetExceptByKey(ExceptReasonModel model);
        /// <summary>
        /// 插入一條異常原因記錄
        /// </summary>
        /// <param name="functionId">要插入的異常原因的model</param>
        /// <returns>插入是否成功</returns>
        int InsertExceptByKey(ExceptReasonModel model, SynclogModel logmodel);
        ///// <summary>
        ///// 更新一條異常原因記錄
        ///// </summary>
        ///// <param name="functionId">要更新的異常原因的model</param>
        ///// <returns>更新是否成功</returns>
        //int UpdateExceptByKey(ExceptReasonModel model);
    }
}
