/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： NightAllowanceQry.cs
 * 檔功能描述： 夜宵補助統計頁面UI層
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2012.01.09
 * 
 */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.Hr.KQM.Query;
using GDSBG.MiABU.Attendance.Common;
using Microsoft.Reporting.WebForms;

namespace GDSBG.MiABU.Attendance.Web.KQM.Query
{
    public partial class NightAllowanceQry : BasePage
    {
        #region 導出頁面的跳轉（跳轉到Excel生成的頁面）
        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Hr/KQM/Query/NightAllowanceToExcel.aspx?dateTo=" + this.txtEndDate.Text + "&dateFrom=" + this.txtStartDate.Text + "&workNo=" + txtWorkNo.Text + "&depCode=" + txtDepCode.Text + "&localName=" + txtLocalName.Text+"&moduleCode=" + Request.QueryString["ModuleCode"]);
        }
        #endregion
        #region 查詢按鈕
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                if ((!string.IsNullOrEmpty(this.txtStartDate.Text) && !string.IsNullOrEmpty(this.txtEndDate.Text)) && (((this.txtWorkNo.Text.Trim().Length != 0) || (this.txtLocalName.Text.Trim().Length != 0)) || new KQMAbsentMonthBll().CheckDateMonths(this.txtStartDate.Text.Trim(), this.txtEndDate.Text.Trim())))
                {
                    string worknoList = null;
                    if (this.txtWorkNo.Text.Trim().Length != 0)
                    {
                        worknoList = this.txtWorkNo.Text.Trim();
                    }
                    else if (this.txtBatchEmployeeNo.Text.Trim().Length != 0)
                    {
                        StringBuilder sb = new StringBuilder();
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
                                sb.Append(str.Trim().ToUpper().Replace("\n", "") + ",");
                            }
                        }
                        worknoList = sb.ToString().Substring(0, sb.ToString().Length - 1);
                    }
                    else
                    {
                        worknoList = "";
                    }
                    string personcode = CurrentUserInfo.Personcode;
                    string companyid = CurrentUserInfo.CompanyId;
                    string modulecode = Request.QueryString["ModuleCode"].ToString();
                    List<ReportParameter> param = new List<ReportParameter> {
                    new ReportParameter("personcode", personcode),
                    new ReportParameter("companyid", companyid),
                    new ReportParameter("modulecode", modulecode),
                    new ReportParameter("depcode", this.txtDepCode.Text.Trim()),
                    new ReportParameter("StartDate", this.txtStartDate.Text.Trim()),
                    new ReportParameter("EndDate", this.txtEndDate.Text.Trim()),
                    new ReportParameter("workno", worknoList),
                    new ReportParameter("localname", this.txtLocalName.Text.Trim())
                };
                    this.ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                    this.ReportViewer1.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServer"]);
                    this.ReportViewer1.ServerReport.ReportPath = getReportDir() + "/ReportKQMNightAllowance";
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
        #region 頁面加載
        protected void Page_Load(object sender, EventArgs e)
        {
            PageHelper.ButtonControls(base.FuncList, pnlShowPanel.Controls, base.FuncListModule);
            SetCalendar(txtStartDate, txtEndDate);
            if (!base.IsPostBack)
            {
                //SetSelector(imgDepCode, txtDepCode, txtDepName);
                SetSelector(imgDepCode, txtDepCode, txtDepName, Request.QueryString["ModuleCode"].ToString());
                this.txtStartDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy/MM") + "/01";
                this.txtEndDate.Text = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM") + "/01").AddDays(-1.0).ToString("yyyy/MM/dd");
                this.imgBatchWorkNo.Attributes.Add("OnClick", "javascript:ShowBatchWorkNo()");
                this.txtDepName.BorderStyle = BorderStyle.None;
            }
        }
        #endregion
        #region 放大鏡綁定
        /// <summary>
        /// 設置Selector
        /// </summary>
        /// <param name="ctrlTrigger">控件ID--按鈕</param>
        /// <param name="ctrlCode">控件ID--文本框1</param>
        /// <param name="ctrlName">控件ID--文本框2</param>
        public void SetSelector(WebControl ctrlTrigger, Control ctrlCode, Control ctrlName, string moduleCode)
        {
            if (ctrlCode is TextBox) { (ctrlCode as TextBox).Attributes.Add("readonly", "readonly"); }
            if (ctrlName is TextBox) { (ctrlName as TextBox).Attributes.Add("readonly", "readonly"); }
            ctrlTrigger.Attributes.Add("onclick", string.Format("return setSelector('{0}','{1}','{2}')",
                ctrlCode.ClientID, ctrlName.ClientID, moduleCode));
        }
        #endregion
        #region  導出控件（生成報表）
        protected void ReportViewer1_Back(object sender, BackEventArgs e)
        {
        }

        protected void ReportViewer1_Drillthrough(object sender, DrillthroughEventArgs e)
        {
        }

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
        #endregion
    }
}
