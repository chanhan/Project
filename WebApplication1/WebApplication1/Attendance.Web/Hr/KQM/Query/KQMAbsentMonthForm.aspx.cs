/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OTMDetailQryForm.aspx.cs
 * 檔功能描述：缺勤統計報表
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.9
 * 
 */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.Common;
using Microsoft.Reporting.WebForms;
using GDSBG.MiABU.Attendance.BLL.Hr.KQM.Query;
using Resources;

namespace GDSBG.MiABU.Attendance.Web.Hr.KQM.Query
{
    public partial class KQMAbsentMonthForm : BasePage
    {
        KQMAbsentMonthBll bll = new KQMAbsentMonthBll();

        /// <summary>
        /// 查詢按鈕事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                string personcode = CurrentUserInfo.Personcode;
                string companyid = CurrentUserInfo.CompanyId;
                string modulecode = base.Request["ModuleCode"].ToString();
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

                int flag = bll.AbsentMonth_val(personcode, modulecode, companyid, this.txtDepCode.Text.Trim(), this.txtStartDate.Text.Trim(), this.txtEndDate.Text.Trim(), worknoList);
                if (flag == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.clear + "');", true);
                    return;
                }
                List<ReportParameter> param = new List<ReportParameter> {
                    new ReportParameter("personcode", personcode),
                    new ReportParameter("companyid", companyid),
                    new ReportParameter("modulecode", modulecode),
                    new ReportParameter("depcode", this.txtDepCode.Text.Trim()),
                    new ReportParameter("StartDate", this.txtStartDate.Text.Trim()),
                    new ReportParameter("EndDate", this.txtEndDate.Text.Trim()),
                    new ReportParameter("workno", worknoList)
                };
                this.ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                this.ReportViewer1.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServer"]);
                this.ReportViewer1.ServerReport.ReportPath = getReportDir() + "/ReportKQMAbsentMonth";
                this.ReportViewer1.ServerReport.ReportServerCredentials = new CustomReportCredentials();
                this.ReportViewer1.ServerReport.SetParameters(param);

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


        /// <summary>
        /// 頁面載入方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                
                this.imgBatchWorkNo.Attributes.Add("OnClick", "javascript:ShowBatchWorkNo()");
                this.txtStartDate.Text = DateTime.Now.ToString("yyyy/MM") + "/01";
                this.txtEndDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
                this.txtDepName.BorderStyle = BorderStyle.None;
            }

            SetSelector(imgDepCode, txtDepCode, txtDepName, "dept", Request.QueryString["ModuleCode"].ToString());
            SetCalendar(txtStartDate, txtEndDate);
        }

        /// <summary>
        /// 返回父報表時，把表頭查詢條件顯示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ReportViewer1_Back(object sender, BackEventArgs e)
        {
            this.div_1.Attributes.Add("style", "display:true");
        }
        /// <summary>
        /// 鑽取到其它報表時，把表頭查詢條件隱藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ReportViewer1_Drillthrough(object sender, DrillthroughEventArgs e)
        {
            this.div_1.Attributes.Add("style", "display:none");
        }

        #region 設置放大鏡頁面,用於輔助選擇
        /// <summary>
        /// 設置Selector
        /// </summary>
        /// <param name="ctrlTrigger">控件ID--按鈕</param>
        /// <param name="ctrlCode">控件ID--文本框1</param>
        /// <param name="ctrlName">控件ID--文本框2</param>
        /// <param name="flag">Selector區分標誌</param>
        public void SetSelector(WebControl ctrlTrigger, Control ctrlCode, Control ctrlName, string flag, string moduleCode)
        {
            if (ctrlCode is TextBox) { (ctrlCode as TextBox).Attributes.Add("readonly", "readonly"); }
            if (ctrlName is TextBox) { (ctrlName as TextBox).Attributes.Add("readonly", "readonly"); }
            ctrlTrigger.Attributes.Add("onclick", string.Format("return setSelector('{0}','{1}','{2}','{3}')",
                ctrlCode.ClientID, ctrlName.ClientID, flag, moduleCode));
        }
        #endregion


        /// <summary>
        /// 獲取報表路徑
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
