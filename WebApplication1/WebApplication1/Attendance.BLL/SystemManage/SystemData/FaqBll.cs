/*
 * Copyright (C) 2011 GDSBG MiABU 版權所有。
 * 
 * 檔案名： FaqBll.cs
 * 檔功能描述： 常見問題業務邏輯類
 * 
 * 版本：1.0
 * 創建標識： 陳函 2012.01.07
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDSBG.MiABU.ESS.Model.SystemManage.Interaction;
using System.Data;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.BLL.SystemManage.SystemData
{
    public class FaqBll : BLLBase<IFaqDal>
    {
        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的功能Model</param>
        /// <returns>是否成功</returns>
        public bool AddFaq(FaqModel model, SynclogModel logmodel)
        {
            return DAL.AddFaq(model, logmodel);
        }

        /// <summary>
        /// 根據主鍵獲取常見問題數據
        /// </summary>
        /// <param name="model">常見問題Model</param>
        /// <returns>常見問題Model</returns>
        public FaqModel GetFaqByKey(string faqSeq)
        {
            FaqModel model = new FaqModel();
            model.FaqSeq = Convert.ToInt32(faqSeq);
            return DAL.GetFaqByKey(model);
        }

        /// <summary>
        /// 更新功能
        /// </summary>
        /// <param name="model">常見問題Model</param>
        /// <returns>是否成功</returns>
        public bool UpdateFaq(FaqModel model, SynclogModel logmodel)
        {
            return DAL.UpdateFaq(model, logmodel);
        }

        /// <summary>
        /// 根據條件查詢常見問題Model集
        /// </summary>
        /// <param name="FaqTitle">問題標題</param>
        /// <param name="faqType">問題類型</param>
        /// <param name="dateStart">開始時間</param>
        /// <param name="dateEnd">結束時間</param>
        /// <param name="pageIndex">頁索引</param>
        /// <param name="pageSize">頁記錄數</param>
        /// <param name="totalCount">查詢記錄總數</param>
        /// <returns>查詢結果：Model集</returns>
        public DataTable GetFaqList(string FaqTitle, string faqType, string dateStart, string dateEnd, bool onlyFamiliar, int pageIndex, int pageSize, out int totalCount)
        {
            DateTime startDate = DateTime.MinValue, endDate = DateTime.MaxValue;
            if (!string.IsNullOrEmpty(dateStart))
            {
                startDate = Convert.ToDateTime(dateStart);
            }
            if (!string.IsNullOrEmpty(dateEnd))
            {
                endDate = Convert.ToDateTime(dateEnd);
            }
            return DAL.GetFaqList(FaqTitle, faqType, startDate, endDate, onlyFamiliar, pageIndex, pageSize, out totalCount);
        }

        /// <summary>
        /// 按提問日期取得前幾條常見問題
        /// </summary>
        /// <param name="topCount">條數</param>
        /// <returns></returns>
        public DataTable GetTopFaqList(int topCount)
        {
            return DAL.GetTopFaqList(topCount);
        }


        public DataTable GetFaqType()
        {
            return DAL.GetFaqType();
        }

    }
}
