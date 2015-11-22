/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ReportKaoQinSumForm.aspx.cs
 * 檔功能描述：總部周邊出勤日報表
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.9
 * 
 */
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Resources;
using System.Web.Script.Serialization;
using System.Configuration;
using System.Drawing;
using Microsoft.Reporting.WebForms;
using System.Text;
using GDSBG.MiABU.Attendance.Common;
using GDSBG.MiABU.Attendance.Web;
using GDSBG.MiABU.Attendance.BLL.Hr.KQM.Query;

namespace GDSBG.MiABU.Attendance.Web.Hr.KQM.Query
{
    public partial class ReportKaoQinSumForm : BasePage
    {
        ReportKaoQinSumBll kqSumBll = new ReportKaoQinSumBll();
        #region 頁面載入
        /// <summary>
        /// 頁面載入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtToDate.Text = DateTime.Now.ToString("yyyy/MM/dd");

                this.txtDepName.BorderStyle = BorderStyle.None;
            }
            SetCalendar(txtToDate);

            SetSelector(imgDepCode, txtDepCode, txtDepName, "dept");
        }
        #endregion


        /// <summary>
        /// 查詢按鈕事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {

            
            DataTable tempTable = new DataTable();
            tempTable = kqSumBll.GetDataBySqlText(this.txtToDate.Text.Trim());
            string personcode = CurrentUserInfo.Personcode;
            string companyid = CurrentUserInfo.CompanyId;
            string modulecode = base.Request["ModuleCode"].ToString();
            List<ReportParameter> param = new List<ReportParameter>();

            param.Add(new ReportParameter("KQDate", this.txtToDate.Text.Trim()));

            param.Add(new ReportParameter("sumall", tempTable.Rows[0][0].ToString()));
            param.Add(new ReportParameter("facton", tempTable.Rows[0][1].ToString()));
            param.Add(new ReportParameter("absentqty", tempTable.Rows[0][2].ToString()));

            ReportViewer1.ProcessingMode = ProcessingMode.Remote;
            ReportViewer1.ServerReport.ReportServerUrl = new Uri(System.Configuration.ConfigurationManager.AppSettings["ReportServer"]);

            ReportViewer1.ServerReport.ReportPath = getReportDir() + "/ReportKaoQinSumDay";

            ReportViewer1.ServerReport.ReportServerCredentials = new CustomReportCredentials();
            ReportViewer1.ServerReport.SetParameters(param);

        }

        /// <summary>
        /// 獲取星期
        /// </summary>
        /// <param name="dateString"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        protected string GetWeekDay(string dateString, int i)
        {
            DateTime dt = Convert.ToDateTime(dateString);
            dt = dt.AddDays(i - (int)dt.DayOfWeek);
            return dt.ToString("MM/dd");
        }

        //鑽取到其它報表時，把表頭查詢條件隱藏
        protected void ReportViewer1_Drillthrough(object sender, DrillthroughEventArgs e)
        {
            this.div_1.Attributes.Add("style", "display:none");
        }
        //返回父報表時，把表頭查詢條件顯示
        protected void ReportViewer1_Back(object sender, BackEventArgs e)
        {
            this.div_1.Attributes.Add("style", "display:true");
        }

        #region 設置放大鏡頁面,用於輔助選擇
        /// <summary>
        /// 設置Selector
        /// </summary>
        /// <param name="ctrlTrigger">控件ID--按鈕</param>
        /// <param name="ctrlCode">控件ID--文本框1</param>
        /// <param name="ctrlName">控件ID--文本框2</param>
        /// <param name="flag">Selector區分標誌</param>
        public void SetSelector(WebControl ctrlTrigger, Control ctrlCode, Control ctrlName, string flag)
        {
            if (ctrlCode is TextBox) { (ctrlCode as TextBox).Attributes.Add("readonly", "readonly"); }
            if (ctrlName is TextBox) { (ctrlName as TextBox).Attributes.Add("readonly", "readonly"); }
            ctrlTrigger.Attributes.Add("onclick", string.Format("return setSelector('{0}','{1}','{2}')",
                ctrlCode.ClientID, ctrlName.ClientID, flag));
        }
        #endregion

        /// <summary>
        /// 獲取路徑
        /// </summary>
        /// <returns></returns>
        public static string getReportDir()
        {
            try
            {
                string ReportDir = EncryptClass.Instance.Decrypt(ConfigurationManager.AppSettings["ReportDir"]);
                if (string.IsNullOrEmpty(ReportDir))
                {
                    ReportDir = "eHRMMReport";
                }
                return ("/" + ReportDir);
            }
            catch (Exception)
            {
                return "/eHRMMReport";
            }
        }
    }
}
