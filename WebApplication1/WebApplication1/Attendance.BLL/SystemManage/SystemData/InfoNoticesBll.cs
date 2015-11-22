/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： InfoNoticesBll.cs
 * 檔功能描述： 公告信息業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 陳函 2012.01.03
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemData;
using System.Data;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.BLL.SystemManage.SystemData
{
    public class InfoNoticesBll : BLLBase<IInfoNoticesDal>
    {
        /// <summary>
        /// 查詢公告信息
        /// </summary>
        /// <param name="model">公告信息實體</param>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">每頁顯示的記錄數</param>
        /// <param name="totalCount">總記錄數</param>
        /// <returns>公告信息DataTable</returns>
        public DataTable GetOrderNotice(InfoNoticesModel model, int pageIndex, int pageSize, out int totalCount)
        {
            return DAL.GetOrderNotice(model, pageIndex, pageSize, out totalCount);
        }
        /// <summary>
        /// 查詢公告類型
        /// </summary>
        /// <returns>公告類型DataTable</returns>
        public DataTable GetNoticeType()
        {
            return DAL.GetNoticeType();
        }
        /// <summary>
        /// 添加公告信息
        /// </summary>
        /// <param name="model">公告信息實體</param>
        /// <returns>添加是否成功</returns>
        public bool AddNotice(InfoNoticesModel model, SynclogModel logmodel)
        {
            return DAL.AddNotice(model, logmodel);
        }
        /// <summary>
        /// 按ID查詢公告信息
        /// </summary>
        /// <param name="NoticeId">公告信息ID</param>
        /// <returns>公告信息實體</returns>
        public InfoNoticesModel GetNoticeByKey(string NoticeId)
        {
            return DAL.GetNoticeByKey(NoticeId);
        }
        /// <summary>
        /// 更新公告信息
        /// </summary>
        /// <param name="model">公告信息實體</param>
        /// <returns>更新是否成功</returns>
        public bool UpdateNoticeByKey(InfoNoticesModel model, SynclogModel logmodel)
        {
            return DAL.UpdateNoticeByKey(model, logmodel);
        }
        /// <summary>
        /// 刪除公告信息
        /// </summary>
        /// <param name="model">公告信息實體</param>
        /// <returns>刪除是否成功</returns>
        public bool DeleteNotice(InfoNoticesModel model, SynclogModel logmodel)
        {
            return DAL.DeleteNotice(model, logmodel);
        }


        public DataTable  GetTopNoticeList(int topCount)
        {
            return DAL.GetTopNoticeList(topCount);
        }

        public bool AddBrowseTimes(string noticeId)
        {
            return DAL.AddBrowseTimes(noticeId);
        }



        public DataTable GetNoticeList(string noticeTitle, string noticeType, string dateStart, string dateEnd, int pageIndex, int pageSize, out int totalCount)
        {     DateTime startDate = DateTime.MinValue, endDate = DateTime.MaxValue;
            if (!string.IsNullOrEmpty(dateStart))
            {
                startDate = Convert.ToDateTime(dateStart);
            }
            if (!string.IsNullOrEmpty(dateEnd))
            {
                endDate = Convert.ToDateTime(dateEnd);
            }
            return DAL.GetNoticeList(noticeTitle, noticeType, startDate, endDate, pageIndex, pageSize, out totalCount);
        }
    }
}
