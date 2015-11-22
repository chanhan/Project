using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.Hr.KQM.Query;
using GDSBG.MiABU.Attendance.Common;
using Microsoft.Reporting.WebForms;
using Resources;

/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMMakeupReportForm.aspx.cs
 * 檔功能描述： 出勤日報表
 * 
 * 版本：1.0
 * 創建標識： 昝望 2011.12.21
 * 
 */

namespace GDSBG.MiABU.Attendance.Web.Hr.KQM.Query
{
    public partial class KQMMakeupReportForm : BasePage
    {
        KQMAbsentMonthBll bll = new KQMAbsentMonthBll();

        protected void Page_Load(object sender, EventArgs e)
        {
            PageHelper.ButtonControls(base.FuncList, pnlShowPanel.Controls, base.FuncListModule);
            if (!IsPostBack)
            {
                try
                {
                    this.ImageBatchWorkNo.Attributes.Add("OnClick", "javascript:ShowBatchWorkNo()");
                    SetCalendar(txtStartDate, txtEndDate);
                    this.txtStartDate.Text = DateTime.Now.ToString("YYYY/MM/DD");
                    this.txtEndDate.Text = DateTime.Now.ToString("YYYY/MM/DD");
                    this.txtStartDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy/MM") + "/01";
                    this.txtEndDate.Text = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM") + "/01").ToString("yyyy/MM/dd");
                    this.txtDepName.BorderStyle = BorderStyle.None;
                    this.ddlReasonType.DataSource = bll.GetExceptReason();
                    this.ddlReasonType.DataTextField = "REASONNAME";
                    this.ddlReasonType.DataValueField = "REASONNO";
                    this.ddlReasonType.DataBind();
                    SetSelector(ImageDepCode, txtDepCode, txtDepName, "DepCode");
                }
                catch (Exception ex2)
                {
                    
                }
            }
        }

        #region 查詢
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                if ((CheckData(this.txtStartDate.Text, ControlText.gvHeadBeginTime) && CheckData(this.txtEndDate.Text, ControlText.gvHeadEndTime)) && bll.CheckDateMonths(this.txtStartDate.Text.Trim(), this.txtEndDate.Text.Trim()))
                {
                    string personcode = CurrentUserInfo.Personcode;
                    string companyid = CurrentUserInfo.CompanyId;
                    string modulecode = Request.QueryString["ModuleCode"];
                    string[] temVal = null;
                    string ddlStr = "";
                    string condition = "";
                    if (this.ddlReasonType.SelectedValue.Length > 0)
                    {
                        temVal = this.ddlReasonType.SelectedValuesToString(",").Split(new char[] { ',' });
                        for (int iLoop = 0; iLoop < temVal.Length; iLoop++)
                        {
                            ddlStr = ddlStr + "'" + temVal[iLoop] + "',";
                        }
                        ddlStr = ddlStr.Substring(0, ddlStr.Length - 1);
                        condition = condition + " and  a.reasontype in (" + ddlStr + ") ";
                    }
                    if (this.txtWorkNo.Text.Trim().Length != 0)
                    {
                        condition = condition + " and a.WorkNo like '" + this.txtWorkNo.Text.Trim().ToUpper() + "%' ";
                    }
                    if (this.txtBatchEmployeeNo.Text.Trim().Length != 0)
                    {
                        string empNoList = this.txtBatchEmployeeNo.Text.Trim();
                        List<string> workNoList = new List<string>();
                        foreach (string workno in empNoList.Split(new char[] { Convert.ToChar('\r') }))
                        {
                            workNoList.Add(workno);
                        }
                        foreach (string str in workNoList)
                        {
                            if (str.Length > 0)
                            {
                                ddlStr = ddlStr + " a.WorkNo='" + str.Trim().ToUpper().Replace("\n", "") + "' or";
                            }
                        }
                        ddlStr = ddlStr.Substring(0, ddlStr.Length - 2);
                        condition = condition + " and (" + ddlStr + ")";
                    }
                    ddlStr = "";
                    List<ReportParameter> param = new List<ReportParameter> {
                    new ReportParameter("personcode", personcode),
                    new ReportParameter("companyid", companyid),
                    new ReportParameter("modulecode", modulecode),
                    new ReportParameter("depcode", this.txtDepCode.Text.Trim()),
                    new ReportParameter("StartDate", this.txtStartDate.Text.Trim()),
                    new ReportParameter("EndDate", this.txtEndDate.Text.Trim()),
                    new ReportParameter("condition", condition)
                };
                    this.ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                    this.ReportViewer1.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServer"]);
                    this.ReportViewer1.ServerReport.ReportPath = getReportDir() + "/ReportKQMMakeup";
                    this.ReportViewer1.ServerReport.ReportServerCredentials = new CustomReportCredentials();
                    this.ReportViewer1.ServerReport.SetParameters(param);
                }
            }
            catch (ReportSecurityException ex)
            {
                base.Response.Write(ex.Message);
            }
            catch (Exception ex2)
            {
                base.Response.Write(ex2.Message);
            }

        }
        #endregion


        #region 私有函數

        protected void ReportViewer1_Back(object sender, BackEventArgs e)
        {
            this.div_1.Attributes.Add("style", "display:true");
        }

        protected void ReportViewer1_Drillthrough(object sender, DrillthroughEventArgs e)
        {
            this.div_1.Attributes.Add("style", "display:none");
        }

        protected static string getReportDir()
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

        /// <summary>
        /// 日期是否為空
        /// </summary>
        /// <param name="txtValue"></param>
        /// <param name="ReValue"></param>
        /// <returns></returns>
        protected bool CheckData(string txtValue, string ReValue)
        {
            if (string.IsNullOrEmpty(txtValue))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + ReValue + Message.NotNullOrEmpty + "');", true);
                return false;
            }
            return true;
        }


        #endregion


        #region 設置Selector
        /// <summary>
        /// 設置Selector
        /// </summary>
        /// <param name="ctrlTrigger">控件ID--按鈕</param>
        /// <param name="ctrlCode">控件ID--文本框1</param>
        /// <param name="ctrlName">控件ID--文本框2</param>
        /// <param name="flag">Selector區分標誌</param>
        public static void SetSelector(WebControl ctrlTrigger, Control ctrlCode, Control ctrlName, string flag)
        {
            if (ctrlCode is TextBox) { (ctrlCode as TextBox).Attributes.Add("readonly", "readonly"); }
            if (ctrlName is TextBox) { (ctrlName as TextBox).Attributes.Add("readonly", "readonly"); }
            ctrlTrigger.Attributes.Add("onclick", string.Format("return setSelector('{0}','{1}','{2}')",
                ctrlCode.ClientID, ctrlName.ClientID, flag));
        }

        #endregion
    }
}
