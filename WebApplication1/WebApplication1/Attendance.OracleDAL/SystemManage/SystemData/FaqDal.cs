using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDSBG.MiABU.ESS.Model.SystemManage.Interaction;
using GDSBG.MiABU.Attendance.IDAL.SystemManage.SystemData;
using System.Data.OracleClient;
using System.Data;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.OracleDAL.SystemManage.SystemData
{
    public class FaqDal : DALBase<FaqModel>, IFaqDal
    {
        /// <summary>
        /// 新增功能
        /// </summary>
        /// <param name="model">要新增的功能Model</param>
        /// <returns>是否成功</returns>
        public bool AddFaq(FaqModel model, SynclogModel logmodel)
        {
            return DalHelper.Insert(model, logmodel) == 1;
        }

        /// <summary>
        /// 根據主鍵獲取常見問題數據
        /// </summary>
        /// <param name="model">常見問題Model</param>
        /// <returns>常見問題Model</returns>
        public FaqModel GetFaqByKey(FaqModel model)
        {
            return DalHelper.SelectByKey(model);
        }

        /// <summary>
        /// 更新功能
        /// </summary>
        /// <param name="model">常見問題Model</param>
        /// <returns>是否成功</returns>
        public bool UpdateFaq(FaqModel model, SynclogModel logmodel)
        {
            return DalHelper.UpdateByKey(model, true,logmodel) != -1;
        }

        /// <summary>
        /// 根據條件查詢常見問題Model集
        /// </summary>
        /// <param name="faqTitle">問題標題</param>
        /// <param name="faqType">問題類型</param>
        /// <param name="dateStart">開始時間</param>
        /// <param name="dateEnd">結束時間</param>
        /// <param name="pageIndex">頁索引</param>
        /// <param name="pageSize">頁記錄數</param>
        /// <param name="totalCount">查詢記錄總數</param>
        /// <returns>查詢結果：Model集</returns>
        public DataTable GetFaqList(string faqTitle, string faqType, DateTime dateStart, DateTime dateEnd, bool onlyFamiliar, int pageIndex, int pageSize, out int totalCount)
        {
            string cmdTxt = @"SELECT FAQ_SEQ, EMP_NO, EMP_NAME, EMP_PHONE, EMP_EMAIL, FAQ_TITLE, FAQ_DATE,FAQ_CONTENT, ANSWER_NAME,ANSWER_EMAIL, ANSWER_DATE, ANSWER_CONTENT,
                            ANSWER_FLAG, IS_FAMILIAR, FAQ_TYPE_ID, FAQ_TYPE_NAME, CREATE_USER,CREATE_DATE, UPDATE_USER, UPDATE_DATE FROM gds_att_info_faqs_v WHERE UPPER(FAQ_TITLE) LIKE '%'||UPPER(:faqTitle)||'%'
                            AND FAQ_TYPE_ID=NVL (:faqType,FAQ_TYPE_ID) AND FAQ_DATE >= :startdate AND FAQ_DATE <= :enddate";
            if (onlyFamiliar) { cmdTxt += " AND IS_FAMILIAR = 'Y'"; }
            cmdTxt += " ORDER BY FAQ_DATE DESC";
            return DalHelper.ExecutePagerQuery(cmdTxt, pageIndex, pageSize, out totalCount, new OracleParameter(":faqTitle", faqTitle),
                new OracleParameter(":faqType", faqType), new OracleParameter(":startdate", dateStart), new OracleParameter(":enddate", dateEnd == DateTime.MaxValue ? dateEnd : dateEnd.AddDays(1)));
        }

        /// <summary>
        /// 按提問日期取得前幾條常見問題
        /// </summary>
        /// <param name="topCount">條數</param>
        /// <returns></returns>
        public DataTable GetTopFaqList(int topCount)
        {
            return DalHelper.Select(new FaqModel() { IsFamiliar = "Y", AnswerFlag = "Y" }, "FAQ_DATE DESC", 1, topCount);
        }

        public DataTable GetFaqType()
        {
            string cmdText = "select  *  from gds_att_faq_types where active_flag='Y'";
            return DalHelper.ExecuteQuery(cmdText);
        }
    }
}
