/*
 * Copyright (C) 2011 GDSBG MiABU 版權所有。
 * 
 * 檔案名： PaperDal.cs
 * 檔功能描述： 問卷調查數據操作類
 * 
 * 版本：1.0
 * 創建標識： 陳函 2012.01.07
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

namespace GDSBG.MiABU.Attendance.OracleDAL.SystemManage.SystemData
{
    public class PaperDal : DALBase<PaperModel>, IPaperDal
    {
        /// <summary>
        /// 根據主鍵獲取問卷數據
        /// </summary>
        /// <param name="model">問卷Model</param>
        /// <returns>問卷Model</returns>
        public PaperModel GetPaperByKey(PaperModel model)
        {
            return DalHelper.SelectByKey(model);
        }

        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的功能Model</param>
        /// <returns>是否成功</returns>
        public bool AddPaper(PaperModel model)
        {
            return DalHelper.Insert(model) != -1;
        }

        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        public bool UpdatePaperByKey(PaperModel model)
        {
            return DalHelper.UpdateByKey(model) != -1;
        }

        /// <summary>
        /// 根據主鍵刪除問卷
        /// </summary>
        /// <param name="paperSeq">問卷ID</param>
        /// <returns>是否成功</returns>
        public bool DeletePaperByKey(string paperSeq)
        {
            string cmdText = "gds_att_delete_paper_pro";
            return DalHelper.ExecuteNonQuery(cmdText, CommandType.StoredProcedure, new OracleParameter("a_paperid", paperSeq)) != -1;
        }

        /// <summary>
        /// 根據實體條件獲取問卷數據
        /// </summary>
        /// <param name="model">問卷Model</param>
        /// <param name="orderProperties">排序方式</param>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">每頁記錄數</param>
        /// <param name="totalCount">總記錄數</param>
        /// <returns>問卷Model集</returns>
        public DataTable GetOrderPaper(PaperModel model, int pageIndex, int pageSize, out int totalCount)
        {
            return DalHelper.Select(model, true, "PAPER_DATE DESC", pageIndex, pageSize, out totalCount);
        }

        /// <summary>
        /// 根據實體條件獲取問卷數據
        /// </summary>
        /// <param name="model">問卷Model</param>
        /// <returns>問卷Model集</returns>
        public DataTable GetActivePaper(PaperModel model)
        {
            return DalHelper.Select(model, "PAPER_DATE DESC");
        }

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
        public DataTable GetPaperList(string paperTitle, DateTime dateStart, DateTime dateEnd, int pageIndex, int pageSize, out int totalCount)
        {

            string cmdTxt = @"SELECT PAPER_SEQ, PAPER_TITLE, PAPER_DATE, PAPER_REMARKS, ACTIVE_FLAG,CREATE_USER, CREATE_DATE, UPDATE_USER, UPDATE_DATE, PAPER_COUNT
                                FROM GDS_ATT_INFO_PAPERS_V WHERE UPPER(PAPER_TITLE) LIKE '%'||UPPER(:papertitle)||'%' AND PAPER_DATE >=:startdate
                                AND PAPER_DATE < :enddate ORDER BY PAPER_DATE DESC";
            return DalHelper.ExecutePagerQuery(cmdTxt, pageIndex, pageSize, out totalCount, new OracleParameter(":papertitle", paperTitle),
                new OracleParameter(":startdate", dateStart), new OracleParameter(":enddate", dateEnd == DateTime.MaxValue ? dateEnd : dateEnd.AddDays(1)));
        }

        /// <summary>
        /// 獲得最新問卷
        /// </summary>
        /// <param name="topCount">條數</param>
        /// <returns></returns>
        public DataTable GetTopPaperList(int topCount)
        {
            return DalHelper.Select(new PaperModel() { ActiveFlag = "Y" }, "PAPER_DATE DESC", 1, topCount);
        }

        /// <summary>
        /// 檢查問卷標題是否已存在
        /// </summary>
        /// <param name="paperTitle">問卷標題</param>
        /// <returns>是否存在</returns>
        public bool CheckPaperTitleExist(string paperTitle)
        {
            string cmdTxt = "SELECT COUNT(1) FROM GDS_ATT_INFO_PAPERS A WHERE A.PAPER_TITLE = :a_paperTitle";
            return Convert.ToInt32(DalHelper.ExecuteScalar(cmdTxt, new OracleParameter(":a_paperTitle", paperTitle))) > 0;
        }
    }
}
