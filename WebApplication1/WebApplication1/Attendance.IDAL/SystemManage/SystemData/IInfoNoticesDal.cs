/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： IInfoNoticesDal.cs
 * 檔功能描述： 公告信息數據接口類
 * 
 * 版本：1.0
 * 創建標識： 陳函 2012.01.03
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemData;
using GDSBG.MiABU.Attendance.Common.Attributes;
using System.Data;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemData
{
    /// <summary>
    /// 公告信息操作接口
    /// </summary>
    [RefClass("SystemManage.SystemData.InfoNoticesDal")]
  public  interface IInfoNoticesDal
    {
        /// <summary>
        /// 查詢公告信息
        /// </summary>
        /// <param name="model">公告信息實體</param>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">每頁顯示的記錄數</param>
        /// <param name="totalCount">總記錄數</param>
        /// <returns>公告信息DataTable</returns>
        DataTable GetOrderNotice(InfoNoticesModel model, int pageIndex, int pageSize, out int totalCount);
        /// <summary>
        /// 查詢公告類型
        /// </summary>
        /// <returns>公告類型DataTable</returns>
        DataTable GetNoticeType();
        /// <summary>
        /// 添加公告信息
        /// </summary>
        /// <param name="model">公告信息實體</param>
        /// <returns>添加是否成功</returns>
        bool AddNotice(InfoNoticesModel model,SynclogModel logmodel);
        /// <summary>
        /// 按ID查詢公告信息
        /// </summary>
        /// <param name="NoticeId">公告信息ID</param>
        /// <returns>公告信息實體</returns>
        InfoNoticesModel GetNoticeByKey(string NoticeId);
        /// <summary>
        /// 更新公告信息
        /// </summary>
        /// <param name="model">公告信息實體</param>
        /// <returns>更新是否成功</returns>
        bool UpdateNoticeByKey(InfoNoticesModel model, SynclogModel logmodel);
        /// <summary>
        /// 刪除公告信息
        /// </summary>
        /// <param name="model">公告信息實體</param>
        /// <returns>刪除是否成功</returns>
        bool DeleteNotice(InfoNoticesModel model, SynclogModel logmodel);

        DataTable GetTopNoticeList(int topCount);

        bool AddBrowseTimes(string noticeId);

        DataTable GetNoticeList(string noticeTitle, string noticeType, DateTime startDate, DateTime endDate, int pageIndex, int pageSize, out int totalCount);
    }
}
