/*
 * Copyright (C) 2011 GDSBG MiABU 版權所有。
 * 
 * 檔案名： PaperBll.cs
 * 檔功能描述： 問卷業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 陳函 2012.01.07
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemData;
using System.Data;

namespace GDSBG.MiABU.Attendance.BLL.SystemManage.SystemData
{
    public class PaperBll : BLLBase<IPaperDal>
    {
        /// <summary>
        /// 根據主鍵獲取問卷數據
        /// </summary>
        /// <param name="model">問卷Model</param>
        /// <returns>問卷Model</returns>
        public PaperModel GetPaperByKey(string paperSeq)
        {
            PaperModel model = new PaperModel();
            model.PaperSeq = Convert.ToInt32(paperSeq);
            return DAL.GetPaperByKey(model);
        }

        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的功能Model</param>
        /// <returns>是否成功</returns>
        public bool AddPaper(PaperModel model)
        {
            return DAL.AddPaper(model);
        }

        /// <summary>
        /// 根據主鍵修改功能
        /// </summary>
        /// <param name="model">要修改的功能Model</param>
        /// <returns>是否成功</returns>
        public bool UpdatePaperByKey(PaperModel model)
        {
            return DAL.UpdatePaperByKey(model);
        }

        /// <summary>
        /// 根據主鍵刪除問卷
        /// </summary>
        /// <param name="paperSeq">問卷ID</param>
        /// <returns>是否成功</returns>
        public bool DeletePaperByKey(string paperSeq)
        {
            return DAL.DeletePaperByKey(paperSeq);
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
        public DataTable  GetOrderPaper(PaperModel model, int pageIndex, int pageSize, out int totalCount)
        {
            return DAL.GetOrderPaper(model, pageIndex, pageSize, out totalCount);
        }

        /// <summary>
        /// 根據實體條件獲取問卷數據
        /// </summary>
        /// <param name="model">問卷Model</param>
        /// <returns>問卷Model集</returns>
        public DataTable GetActivePaper()
        {
            PaperModel model = new PaperModel();
            model.ActiveFlag = "Y";
            return DAL.GetActivePaper(model);
        }

        /// <summary>
        /// 分頁查詢問卷
        /// </summary>
        /// <param name="paperTitle"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetPaperList(string paperTitle, string startDate, string endDate, int pageIndex, int pageSize, out int totalCount)
        {
            DateTime dateStarte = DateTime.MinValue;
            DateTime dateEnd = DateTime.MaxValue;
            if (!string.IsNullOrEmpty(startDate))
            {
                dateStarte = Convert.ToDateTime(startDate);
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                dateEnd = Convert.ToDateTime(endDate);
            }
            return DAL.GetPaperList(paperTitle, dateStarte, dateEnd, pageIndex, pageSize, out totalCount);
        }

        /// <summary>
        /// 獲得最新問卷
        /// </summary>
        /// <param name="topCount">條數</param>
        /// <returns></returns>
        public DataTable GetTopPaperList(int topCount)
        {
            return DAL.GetTopPaperList(topCount);
        }

        /// <summary>
        /// 檢查問卷標題是否已存在
        /// </summary>
        /// <param name="paperTitle">問卷標題</param>
        /// <returns>是否存在</returns>
        public bool CheckPaperTitleExist(string paperTitle)
        {
            return DAL.CheckPaperTitleExist(paperTitle);
        }
    }
}
