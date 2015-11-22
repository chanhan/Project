using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Resources;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.Hr.KQM.Query;
using GDSBG.MiABU.Attendance.Common;
using Infragistics.WebUI.WebSchedule;
using Microsoft.Reporting.WebForms;
using Resources;

/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： RolesSelector.aspx.cs
 * 檔功能描述：未刷補卡
 * 
 * 版本：1.0
 * 創建標識： 昝望 2011.12.21
 * 
 */


namespace GDSBG.MiABU.Attendance.Web.Hr.KQM.Query
{
    public partial class ReportKQMKaoQinDayForm : BasePage
    {
        KQMAbsentMonthBll bll = new KQMAbsentMonthBll();

        protected void Page_Load(object sender, EventArgs e)
        {
            PageHelper.ButtonControls(base.FuncList, pnlShowPanel.Controls, base.FuncListModule);
            ModuleCode.Value = Request.QueryString["ModuleCode"].ToString();
            if (!IsPostBack)
            {
                try
                {
                    SetSelector(ImageDepCode, txtDepCode, txtDepName, "DepCode");
                    SetCalendar(txtToDate);
                    this.txtToDate.Text = DateTime.Now.ToString("YYYY/MM/DD");
                    this.txtToDate.Text = DateTime.Now.ToShortDateString();
                    this.txtDepName.BorderStyle = BorderStyle.None;
                    this.ddlArea.DataSource = bll.GetAreaCode();
                    this.ddlArea.DataTextField = "singlename";
                    this.ddlArea.DataValueField = "AreaCode";
                    this.ddlArea.DataBind();
                    DataTable dt = bll.GetParavalue();
                    if (dt.Rows[0][0].ToString().Trim().Equals("LH") && CurrentUserInfo.DepLevel.Equals("0"))
                    {
                        this.ddlArea.Enabled = true;
                    }
                    else
                    {
                        this.ddlArea.Enabled = false;
                    }
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
                if (CheckData(this.txtToDate.Text, ControlText.gvOTMExceptionOTDate))
                {
                    return;
                }
                string personcode = CurrentUserInfo.Personcode;
                string companyid = CurrentUserInfo.CompanyId;
                string modulecode = Request.QueryString["ModuleCode"];
                int flag = bll.KaoQinDay_val(this.txtToDate.Text.Trim(), personcode, modulecode, companyid, this.txtDepCode.Text.Trim());
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
                new ReportParameter("KQDate", this.txtToDate.Text.Trim()),
                new ReportParameter("Title", ""),
                new ReportParameter("DepName", "單位"),
                new ReportParameter("MonthStart", "月初人數"),
                new ReportParameter("Yesterday", "昨日人數"),
                new ReportParameter("NewIn", "當日新進"),
                new ReportParameter("Leave", "當日離職"),
                new ReportParameter("MoveIn", "當日調入"),
                new ReportParameter("MoveOut", "當日調出"),
                new ReportParameter("ShoudOn", "應到人數"),
                new ReportParameter("FactOn", "實到人數"),
                new ReportParameter("ClassA", "白班"),
                new ReportParameter("ClassB", "中班"),
                new ReportParameter("ClassC", "晚班"),
                new ReportParameter("Total", "合計"),
                new ReportParameter("KQPercent", "出勤率"),
                new ReportParameter("Vaction", "休假"),
                new ReportParameter("Absent", "缺勤人數"),
                new ReportParameter("AbsentDesc", "缺勤說明"),
                new ReportParameter("LeaveI", "事假"),
                new ReportParameter("LeaveT", "病假"),
                new ReportParameter("LeaveC", "礦工"),
                new ReportParameter("LeaveAB", "遲到早退"),
                new ReportParameter("LeaveR", "工傷"),
                new ReportParameter("LeaveU", "調休"),
                new ReportParameter("StayWork", "留職"),
                new ReportParameter("LeaveS", "產假"),
                new ReportParameter("LeaveJ", "婚假"),
                new ReportParameter("LeaveK", "喪假"),
                new ReportParameter("LeaveY", "年休假"),
                new ReportParameter("LeaveX", "出差"),
                new ReportParameter("LeaveL", "看護假"),
                new ReportParameter("LeaveV", "節育假"),
                new ReportParameter("LeaveW", "探親假"),
                new ReportParameter("LeaveZ", "醫療期病假"),
                new ReportParameter("LeaveRK", "停工假"),
                new ReportParameter("LeaveO", "特殊有薪假"),
                new ReportParameter("Other", "其他")
            };
                this.ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                this.ReportViewer1.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServer"]);
                if (!this.ddlArea.Enabled)
                {
                    goto Label_0680;
                }
                string area = this.ddlArea.SelectedValue;
                if (area == null)
                {
                    goto Label_0655;
                }
                if (!(area == "CS"))
                {
                    if (area == "YK")
                    {
                        goto Label_05E6;
                    }
                    if (area == "YT")
                    {
                        goto Label_060B;
                    }
                    if (area == "LH")
                    {
                        goto Label_0630;
                    }
                    goto Label_0655;
                }
                this.ReportViewer1.ServerReport.ReportPath = getReportDir() + "/ReportKQMKaoQinDayCS";
                goto Label_06A2;
            Label_05E6:
                this.ReportViewer1.ServerReport.ReportPath = getReportDir() + "/ReportKQMKaoQinDayYK";
                goto Label_06A2;
            Label_060B:
                this.ReportViewer1.ServerReport.ReportPath = getReportDir() + "/ReportKQMKaoQinDayYT";
                goto Label_06A2;
            Label_0630:
                this.ReportViewer1.ServerReport.ReportPath = getReportDir() + "/ReportKQMKaoQinDay";
                goto Label_06A2;
            Label_0655:
                this.ReportViewer1.ServerReport.ReportPath = getReportDir() + "/ReportKQMKaoQinDay";
                goto Label_06A2;
            Label_0680:
                this.ReportViewer1.ServerReport.ReportPath = getReportDir() + "/ReportKQMKaoQinDay";
            Label_06A2:
                this.ReportViewer1.ServerReport.ReportServerCredentials = new CustomReportCredentials();
                this.ReportViewer1.ServerReport.SetParameters(param);
            }
            catch (ReportSecurityException ex)
            {

            }
            catch (Exception ex2)
            {

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

        protected string GetResouseValue(string key)
        {
            try
            {
                string resourceValue = "";
                CultureInfo ci = CultureInfo.CurrentCulture;
                resourceValue = new ResourceManager(typeof(Resource)).GetString(key, ci);
                if (string.IsNullOrEmpty(resourceValue))
                {
                    resourceValue = "Undefine";
                }
                return resourceValue.Replace("'", "''");
            }
            catch (Exception)
            {
                return "Undefine";
            }
        }

        /// <summary>
        /// 日期是否為空
        /// </summary>
        /// <param name="txtValue"></param>
        /// <param name="ReValue"></param>
        /// <returns></returns>
        public bool CheckData(string txtValue, string ReValue)
        {
            if (string.IsNullOrEmpty(txtValue))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + ReValue + Message.NotNullOrEmpty + "');", true);
                return true;
            }
            return false;
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
