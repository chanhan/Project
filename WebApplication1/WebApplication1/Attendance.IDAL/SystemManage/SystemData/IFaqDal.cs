using System;
using System.Collections.Generic;
using System.Text;
using GDSBG.MiABU.Attendance.Common.Attributes;
using GDSBG.MiABU.ESS.Model.SystemManage.Interaction;
using System.Data;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemData
{
    /// <summary>
    /// 功能管理數據操作接口
    /// </summary>
    [RefClass("SystemManage.SystemData.FaqDal")]
    public interface IFaqDal
    {
        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的功能Model</param>
        /// <returns>是否成功</returns>
        bool AddFaq(FaqModel model, SynclogModel logmodel);

        /// <summary>
        /// 根據主鍵獲取常見問題數據
        /// </summary>
        /// <param name="model">常見問題Model</param>
        /// <returns>常見問題Model</returns>
        FaqModel GetFaqByKey(FaqModel model);

        /// <summary>
        /// 更新功能
        /// </summary>
        /// <param name="model">常見問題Model</param>
        /// <returns>是否成功</returns>
        bool UpdateFaq(FaqModel model, SynclogModel logmodel);

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
        DataTable GetFaqList(string FaqTitle, string faqType, DateTime dateStart, DateTime dateEnd, bool onlyFamiliar, int pageIndex, int pageSize, out int totalCount);

        ///// <summary>
        /// 按提問日期取得前幾條常見問題
        /// </summary>
        /// <param name="topCount">條數</param>
        /// <returns></returns>
        DataTable GetTopFaqList(int topCount);

        DataTable GetFaqType();

    }
}
