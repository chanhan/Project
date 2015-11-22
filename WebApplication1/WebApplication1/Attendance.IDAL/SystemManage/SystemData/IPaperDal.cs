/*
 * Copyright (C) 2011 GDSBG MiABU 版權所有。
 * 
 * 檔案名： IPaperDal.cs
 * 檔功能描述： 問卷調查數據操作接口
 * 
 * 版本：1.0
 * 創建標識： 陳函 2012.01.07
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemData;
using System.Data;

namespace GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemData
{  /// <summary>
    /// 功能管理數據操作接口
    /// </summary>
    [RefClass("SystemManage.SystemData.PaperDal")]
    public interface IPaperDal
    {
        /// <summary>
        /// 根據主鍵獲取問卷數據
        /// </summary>
        /// <param name="model">問卷Model</param>
        /// <returns>問卷Model</returns>
        PaperModel GetPaperByKey(PaperModel model);

        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的功能Model</param>
        /// <returns>是否成功</returns>
        bool AddPaper(PaperModel model);

        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        bool UpdatePaperByKey(PaperModel model);

        /// <summary>
        /// 根據主鍵刪除問卷
        /// </summary>
        /// <param name="paperSeq">問卷ID</param>
        /// <returns>是否成功</returns>
        bool DeletePaperByKey(string paperSeq);

        /// <summary>
        /// 根據實體條件獲取問卷數據
        /// </summary>
        /// <param name="model">問卷Model</param>
        /// <param name="orderProperties">排序方式</param>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">每頁記錄數</param>
        /// <param name="totalCount">總記錄數</param>
        /// <returns>問卷Model集</returns>
        DataTable GetOrderPaper(PaperModel model, int pageIndex, int pageSize, out int totalCount);

        /// <summary>
        /// 根據實體條件獲取問卷數據
        /// </summary>
        /// <param name="model">問卷Model</param>
        /// <returns>問卷Model集</returns>
        DataTable GetActivePaper(PaperModel model);

        /// <summary>
        /// 分頁查詢問卷
        /// </summary>
        /// <param name="paperTitle"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        DataTable GetPaperList(string paperTitle, DateTime dateStart, DateTime dateEnd, int pageIndex, int pageSize, out int totalCount);

        /// <summary>
        /// 獲得最新問卷
        /// </summary>
        /// <param name="topCount">條數</param>
        /// <returns></returns>
       DataTable GetTopPaperList(int topCount);

        /// <summary>
        /// 檢查問卷標題是否已存在
        /// </summary>
        /// <param name="paperTitle">問卷標題</param>
        /// <returns>是否存在</returns>
        bool CheckPaperTitleExist(string paperTitle);
    }
}
