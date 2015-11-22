/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMLeaveApplyYearQryForm
 * 檔功能描述： 年休假統計查詢UI
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.30
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.KQM.BasicData;
using GDSBG.MiABU.Attendance.BLL.KQM.Query;
using GDSBG.MiABU.Attendance.Model.KQM.Query;
using Resources;

namespace GDSBG.MiABU.Attendance.Web.KQM.Query
{
    public partial class KQMLeaveApplyYearQryForm : BasePage
    {
        KQMLeaveApplyYearQryBll bllLeaveApplyYearQry = new KQMLeaveApplyYearQryBll();
        TypeDataBll typeDataBll = new TypeDataBll();
        KQMLeaveApplyYearQryModel model = new KQMLeaveApplyYearQryModel();
        static DataTable dt_global = new DataTable();
        DataTable dt = new DataTable();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;

        #region 頁面加載
        protected void Page_Load(object sender, EventArgs e)
        {
            PageHelper.ButtonControls(FuncList, pnlShowPanel.Controls, base.FuncListModule);
            SetCalendar(txtJoinDateFrom, txtJoinDateTo, txtCountDateFrom, txtCountDateTo);
            if (!base.IsPostBack)
            {
                this.imgBatchWorkNo.Attributes.Add("OnClick", "javascript:ShowBatchWorkNo()");
                this.ModuleCode.Value = base.Request.QueryString["ModuleCode"].ToString();
                int thisyear = DateTime.Now.Year;
                for (int i = -1; i < 3; i++)
                {
                    this.ddlLeaveyear.Items.Insert(i + 1, new ListItem(Convert.ToString(thisyear - i), Convert.ToString(thisyear - i)));
                }
                this.ddlLeaveyear.SelectedIndex = 1;
                ddlStatusDataBind();
            }
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("WrongDate", Message.WrongDate);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
        }
        #endregion

        #region 導出
        protected void btnExport_Click(object sender, EventArgs e)
        {
            model = PageHelper.GetModel<KQMLeaveApplyYearQryModel>(pnlContent.Controls);
            model.LeaveYear = Convert.ToInt32(this.ddlLeaveyear.SelectedValue.ToString());
            string BatchEmployeeNo = "";
            string str = this.txtBatchEmployeeNo.Text.Trim();
            if (str != "")
            {
                for (int i = 0; i < str.Split('\n').Length; i++)
                {
                    BatchEmployeeNo = BatchEmployeeNo + str.Split('\n')[i].Trim() + "§";
                }
            }
            string joindatefrom = this.txtJoinDateFrom.Text.Trim();
            string joindateto = this.txtJoinDateTo.Text.Trim();
            string countdatefrom = this.txtCountDateFrom.Text.Trim();
            string countdateto = this.txtCountDateTo.Text.Trim();
            string Status = "";
            if (!string.IsNullOrEmpty(this.ddlStatus.SelectedValue.ToString()))
            {
                string[] temVal = ddlStatus.SelectedValuesToString(",").Split(',');
                for (int iLoop = 0; iLoop < temVal.Length; iLoop++)
                {
                    Status += "'" + temVal[iLoop] + "',";
                }
                Status = Status.Substring(0, Status.Length - 1);
            }
            string sql = SqlDep;
            dt = bllLeaveApplyYearQry.GetLeaveApplyYearForExport(model, sql, BatchEmployeeNo, joindatefrom, joindateto, countdatefrom, countdateto, Status);
            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "nodataexport", "alert('" + Message.NoDataExport + "');", true);
            }
            else
            {
                List<KQMLeaveApplyYearQryModel> list = bllLeaveApplyYearQry.GetList(dt);
                string[] header = { ControlText.gvDepName, ControlText.gvWorkNo, ControlText.gvLocalName, ControlText.gvSex, ControlText.gvKQMJoinDate, ControlText.gvEnableStartDate, ControlText.gvLeaveYear, ControlText.gvStartYears, ControlText.gvEndYears, ControlText.gvOutWorkYears, ControlText.gvOutFoxconnYears, ControlText.gvStandardDays, ControlText.gvAlreadDays, ControlText.gvKQMLeaveDays, ControlText.gvNextYearDays, ControlText.gvLeaveRecDays, ControlText.gvCountDays, ControlText.gvStatus, ControlText.gvUpdateDate };
                string[] properties = { "DepName", "WorkNo", "LocalName", "SexName", "JoinDate", "EnableStartDate", "LeaveYear", "StartYears", "EndYears", "OutWorkYears", "OutFoxconnYears", "StandardDays", "AlreadDays", "LeaveDays", "NextYearDays", "LeaveRecDays", "CountDays", "StatusName", "UpdateDate" };
                string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
                PageHelper.ReturnHTTPStream(filePath, true);
            }
        }
        #endregion

        #region 查詢
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            pager.CurrentPageIndex = 1;
            Query();
        }
        #endregion

        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            Query();
        }
        #endregion

        #region 重置
        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.txtDepCode.Text = "";
            this.txtDepName.Text = "";
            this.txtWorkNo.Text = "";
            this.txtJoinDateFrom.Text = DateTime.Today.AddDays(-DateTime.Now.Day + 1).ToString("yyyy/MM/dd");
            this.txtJoinDateTo.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.txtLocalName.Text = "";
            this.ddlLeaveyear.SelectedIndex = 1;
            this.ddlStatus.SelectedIndex = -1;
        }
        #endregion

        #region 數據處理
        protected void UltraWebGridLeaveQry_DataBound(object sender, EventArgs e)
        {
            string WorkNo = "", CountDateFrom = "", CountDateTo = "";
            CountDateFrom = this.txtCountDateFrom.Text;
            CountDateTo = this.txtCountDateTo.Text;
            string countdays = "";
            if (CountDateFrom.Length >= 8 && CountDateTo.Length >= 8)
            {
                CountDateFrom = DateTime.Parse(CountDateFrom).ToString("yyyy/MM/dd");
                CountDateTo = DateTime.Parse(CountDateTo).ToString("yyyy/MM/dd");
                for (int i = 0; i < UltraWebGridLeaveQry.Rows.Count; i++)
                {
                    WorkNo = UltraWebGridLeaveQry.Rows[i].Cells.FromKey("WorkNo").Value == null ? "" : UltraWebGridLeaveQry.Rows[i].Cells.FromKey("WorkNo").Text;
                    dt.Clear();
                    dt = bllLeaveApplyYearQry.GetCountDays(WorkNo, CountDateFrom, CountDateTo);
                    countdays = dt.Rows[0][0].ToString();
                    DataRow row = dt_global.Rows[i];
                    row.BeginEdit();
                    row["CountDays"] = countdays;
                    row.EndEdit();
                    UltraWebGridLeaveQry.Rows[i].Cells.FromKey("CountDays").Value = countdays;
                }
            }
        }
        #endregion

        #region 在職狀態下拉菜單綁定
        private void ddlStatusDataBind()
        {
            dt.Clear();
            dt = typeDataBll.GetStatusList();
            this.ddlStatus.DataSource = dt.DefaultView;
            this.ddlStatus.DataTextField = "DataValue";
            this.ddlStatus.DataValueField = "DataCode";
            this.ddlStatus.DataBind();
        }
        #endregion

        #region 查詢方法
        private void Query()
        {
            int totalCount = 0;
            model = PageHelper.GetModel<KQMLeaveApplyYearQryModel>(pnlContent.Controls);
            model.LeaveYear=Convert.ToInt32(this.ddlLeaveyear.SelectedValue.ToString());
            string BatchEmployeeNo = "";
            string str = this.txtBatchEmployeeNo.Text.Trim();
            if (str != "")
            {
                for (int i = 0; i < str.Split('\n').Length; i++)
                {
                    BatchEmployeeNo = BatchEmployeeNo + str.Split('\n')[i].Trim() + "§";
                }
            }
            string joindatefrom = this.txtJoinDateFrom.Text.Trim();
            string joindateto = this.txtJoinDateTo.Text.Trim();
            string countdatefrom = this.txtCountDateFrom.Text.Trim();
            string countdateto = this.txtCountDateTo.Text.Trim();
            string Status = "";
            if (!string.IsNullOrEmpty(this.ddlStatus.SelectedValue.ToString()))
            {
                string[] temVal = ddlStatus.SelectedValuesToString(",").Split(',');
                for (int iLoop = 0; iLoop < temVal.Length; iLoop++)
                {
                    Status += "'" + temVal[iLoop] + "',";
                }
                Status = Status.Substring(0, Status.Length - 1);
            }
            string sql = SqlDep;
            dt_global = bllLeaveApplyYearQry.GetLeaveApplyYear(model,sql, BatchEmployeeNo, joindatefrom, joindateto, countdatefrom, countdateto, Status, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            this.UltraWebGridLeaveQry.DataSource = dt_global.DefaultView;
            this.UltraWebGridLeaveQry.DataBind();
            pager.RecordCount = totalCount;
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
        }
        #endregion

    }
}
