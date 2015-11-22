/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： InfoNoticesDal.cs
 * 檔功能描述： 公告信息數據操作類
 * 
 * 版本：1.0
 * 創建標識： 陳函 2012.01.03
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemData;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemData;
using System.Data;
using System.Data.OracleClient;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.OracleDAL.SystemManage.SystemData
{
    public class InfoNoticesDal : DALBase<InfoNoticesModel>, IInfoNoticesDal
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
            DataTable dataTable = DalHelper.Select(model, "notice_date desc", pageIndex, pageSize, out totalCount);
            return dataTable;
        }

        /// <summary>
        /// 查詢公告類型
        /// </summary>
        /// <returns>公告類型DataTable</returns>
        public DataTable GetNoticeType()
        {
            string cmdText = "select * from gds_att_notice_types where active_flag='Y'";
            return DalHelper.ExecuteQuery(cmdText);
        }
        /// <summary>
        /// 添加公告信息
        /// </summary>
        /// <param name="model">公告信息實體</param>
        /// <returns>添加是否成功</returns>
        public bool AddNotice(InfoNoticesModel model, SynclogModel logmodel)
        {
            return DalHelper.Insert(model, logmodel) == 1;
        }
        /// <summary>
        /// 按ID查詢公告信息
        /// </summary>
        /// <param name="NoticeId">公告信息ID</param>
        /// <returns>公告信息實體</returns>
        public InfoNoticesModel GetNoticeByKey(string NoticeId)
        {
            InfoNoticesModel infoNoticesModel = new InfoNoticesModel();
            infoNoticesModel.NoticeId = Convert.ToInt32(NoticeId);
            return DalHelper.SelectByKey(infoNoticesModel);
        }
        /// <summary>
        /// 更新公告信息
        /// </summary>
        /// <param name="model">公告信息實體</param>
        /// <returns>更新是否成功</returns>
        public bool UpdateNoticeByKey(InfoNoticesModel model, SynclogModel logmodel)
        {
            return DalHelper.UpdateByKey(model, logmodel) == 1;
        }
        /// <summary>
        /// 刪除公告信息
        /// </summary>
        /// <param name="model">公告信息實體</param>
        /// <returns>刪除是否成功</returns>
        public bool DeleteNotice(InfoNoticesModel model, SynclogModel logmodel)
        {
            return DalHelper.Delete(model, logmodel) == 1;
        }

        public DataTable GetTopNoticeList(int topCount)
        {
            return DalHelper.Select(new InfoNoticesModel() { ActiveFlag = "Y" }, "NOTICE_DATE DESC", 1, topCount);
        }

        public bool AddBrowseTimes(string noticeId)
        {
            string cmdTxt = @"UPDATE GDS_ATT_INFO_NOTICES SET BROWSE_TIMES=BROWSE_TIMES+1 WHERE NOTICE_ID= :noticeId";
            return DalHelper.ExecuteNonQuery(cmdTxt, new OracleParameter(":noticeId", noticeId)) == 1;
        }
        /// <summary>
        /// 根據條件查詢公告Model
        /// </summary>
        /// <param name="noticeTitle">公告標題</param>
        /// <param name="noticeType">公告類型</param>
        /// <param name="dateStart">開始日期</param>
        /// <param name="dateEnd">結束日期</param>
        /// <param name="pageIndex">頁索引</param>
        /// <param name="pageSize">頁記錄數</param>
        /// <param name="totalCount">總記錄數</param>
        /// <returns>公告Model集</returns>
        public DataTable GetNoticeList(string noticeTitle, string noticeType, DateTime dateStart, DateTime dateEnd, int pageIndex, int pageSize, out int totalCount)
        {

            string cmdTxt = @"SELECT NOTICE_ID, NOTICE_TITLE, NOTICE_TYPE_ID, NOTICE_TYPE_NAME,NOTICE_DATE, NOTICE_AUTHOR, AUTHOR_TEL, BROWSE_TIMES, NOTICE_DEPT,
                             ANNEX_FILE_PATH, NOTICE_CONTENT, ACTIVE_FLAG FROM gds_att_info_notices_v WHERE UPPER(NOTICE_TITLE) LIKE '%'||UPPER(:noticetitle)||'%'
                             AND NOTICE_TYPE_ID=NVL(:noticeType,NOTICE_TYPE_ID) AND NOTICE_DATE >= :startdate AND NOTICE_DATE < :enddate ORDER BY NOTICE_DATE DESC";
            return DalHelper.ExecutePagerQuery(cmdTxt, pageIndex, pageSize, out totalCount, new OracleParameter(":noticetitle", noticeTitle),
                new OracleParameter(":noticeType", noticeType), new OracleParameter(":startdate", dateStart), new OracleParameter(":enddate", dateEnd == DateTime.MaxValue ? dateEnd : dateEnd.AddDays(1)));
        }
    }
}
