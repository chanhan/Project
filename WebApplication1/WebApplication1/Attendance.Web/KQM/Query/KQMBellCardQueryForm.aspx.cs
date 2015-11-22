/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMBellCaedQueryForm
 * 檔功能描述： 刷卡原始數據UI
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.26
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.KQM.Query;
using GDSBG.MiABU.Attendance.Model.KQM.Query;
using Resources;

namespace GDSBG.MiABU.Attendance.Web.KQM.Query
{
    public partial class KQMBellCaedQueryForm : BasePage
    {
        BellCardDataModel model = new BellCardDataModel();
        BellCardQueryBll bellCardQueryBll = new BellCardQueryBll();
        DataTable dt = new DataTable();
        static DataTable dt_global = new DataTable();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        #region 頁面加載
        protected void Page_Load(object sender, EventArgs e)
        {
            PageHelper.ButtonControls(FuncList, pnlShowPanel.Controls, base.FuncListModule);
            SetCalendar(txtFromDate, txtToDate);
            if (!base.IsPostBack)
            {
                this.txtFromDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
                this.txtToDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
                this.dt.Clear();
                dt = bellCardQueryBll.GetBellNo();
                this.ddlBellNo.DataSource = dt.DefaultView;
                this.ddlBellNo.DataTextField = "BellNo";
                this.ddlBellNo.DataValueField = "BellNo";
                this.ddlBellNo.DataBind();
                this.ddlBellNo.Items.Insert(0, new ListItem("", ""));
                this.ddlBellNo.Items.Insert(1, new ListItem(Message.SIGNMAKEUP, "SIGNMAKEUP"));
                this.ddlBellNo.Items.Insert(2, new ListItem(Message.SIGNINOUT, "SIGNINOUT"));

            }
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("WrongDate", Message.WrongDate);
                ClientMessage.Add("CardDateNotNull", Message.CardDateNotNull);
                ClientMessage.Add("WorkNoNotNull", Message.WorkNoNotNull);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
        }
        #endregion

        #region 導出
        protected void btnExport_Click(object sender, EventArgs e)
        {
            model = PageHelper.GetModel<BellCardDataModel>(pnlContent.Controls);
            string fromdate = this.txtFromDate.Text.Trim();
            string todate = this.txtToDate.Text.Trim();
            string sql = base.SqlDep;
            if (model != null)
            {
                if (this.chkHisFlag.Checked)
                {
                    dt = bellCardQueryBll.GetBellCardDataForExport(model, sql, true, fromdate, todate);
                }
                else
                {
                    dt = bellCardQueryBll.GetBellCardDataForExport(model, sql, false, fromdate, todate);
                }
            }
            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "nodataexport", "alert('" + Message.NoDataExport + "');", true);
            }
            else
            {
                List<BellCardDataModel> list = bellCardQueryBll.GetList(dt);
                string[] header = { ControlText.gvWorkNo, ControlText.gvLocalName, ControlText.gvDName, ControlText.gvCardTime, ControlText.gvCardTimeMM, ControlText.gvCardNo, ControlText.gvBellNo, ControlText.gvAddRess };
                string[] properties = { "WorkNo", "LocalName", "DepName", "CardTime", "CardTimeMM", "CardNo", "BellNo", "AddRess" };
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
            this.txtFromDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.txtToDate.Text = DateTime.Now.AddDays(1.0).ToString("yyyy/MM/dd");
            this.ddlBellNo.ClearSelection();
            this.txtWorkNo.Text = "";
            this.txtLocalName.Text = "";
        }
        #endregion

        #region 查詢方法
        private void Query()
        {
            int totalCount = 0;
            model = PageHelper.GetModel<BellCardDataModel>(pnlContent.Controls);
            string fromdate = this.txtFromDate.Text.Trim();
            string todate = this.txtToDate.Text.Trim();
            string sql = base.SqlDep;
            if (model != null)
            {
                if (this.chkHisFlag.Checked)
                {
                    dt_global = bellCardQueryBll.GetBellCardData(model, sql, true, fromdate, todate, pager.CurrentPageIndex, pager.PageSize, out totalCount);
                }
                else
                {
                    dt_global = bellCardQueryBll.GetBellCardData(model, sql, false, fromdate, todate, pager.CurrentPageIndex, pager.PageSize, out totalCount);
                }
                pager.RecordCount = totalCount;
                this.UltraWebGridBellCardQuery.DataSource = dt_global.DefaultView;
                this.UltraWebGridBellCardQuery.DataBind();
                pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
            }

            //string condition = "";
            //string strTableName = "KQM_BELLCARDDATA";
            //if (this.ProcessFlag.Value.ToLower().Equals("condition") || WindowOpen)
            //{
            //    if (!(base.CheckData(this.textBoxFromDate.Text.ToString(), "bfw.kqm_bellcard.carddate") && base.CheckData(this.textBoxToDate.Text, "bfw.kqm_bellcard.carddate")) || ((((this.ddlBellNo.SelectedValue.Length == 0) && (this.textBoxWorkNo.Text.Trim().Length == 0)) && (this.textBoxName.Text.Trim().Length == 0)) && !base.CheckData(this.textBoxWorkNo.Text.ToString(), "common.employeeno")))
            //    {
            //        return;
            //    }
            //    if ((this.ddlBellNo.SelectedValue.Length == 0) && base.bPrivileged)
            //    {
            //        condition = condition + " AND exists (SELECT 1 FROM (" + base.sqlDep + ") e where e.DepCode=b.DCode)";
            //    }
            //    if (this.textBoxWorkNo.Text.Trim().Length != 0)
            //    {
            //        condition = condition + " AND b.WorkNo = '" + this.textBoxWorkNo.Text.ToUpper().Trim() + "'";
            //    }
            //    if (this.textBoxName.Text.Trim().Length != 0)
            //    {
            //        condition = condition + " AND b.LocalName like '" + this.textBoxName.Text.Trim() + "%'";
            //    }
            //    if (((this.textBoxWorkNo.Text.Trim().Length == 0) && (this.textBoxName.Text.Trim().Length == 0)) && !base.CheckDateMonths(this.textBoxFromDate.Text.Trim(), this.textBoxToDate.Text.Trim()))
            //    {
            //        return;
            //    }
            //    if (this.ddlBellNo.SelectedValue != "")
            //    {
            //        condition = condition + " and a.BellNo ='" + this.ddlBellNo.SelectedValue + "'";
            //    }
            //    condition = (condition + " AND a.CardTime >= to_date('" + DateTime.Parse(this.textBoxFromDate.Text.Trim()).ToString("yyyy/MM/dd") + " 00:00','yyyy/MM/dd hh24:mi') ") + " AND a.CardTime <= to_date('" + DateTime.Parse(this.textBoxToDate.Text.Trim()).ToString("yyyy/MM/dd") + " 23:59','yyyy/MM//dd hh24:mi') ";
            //    if (this.CheckBoxHisFlag.Checked)
            //    {
            //        strTableName = "KQM_BELLCARDDATA_HIS";
            //    }
            //    this.ViewState.Add("condition", condition);
            //    this.ViewState.Add("TableName", strTableName);
            //}
            //else
            //{
            //    condition = Convert.ToString(this.ViewState["condition"]);
            //    strTableName = Convert.ToString(this.ViewState["TableName"]);
            //}
            //base.SetForwardPage(forwarderType, ((WebNumericEdit)this.PageNavigator.FindControl("WebNumericEditCurrentpage")).Value.ToString());
            //this.dataSet = ((ServiceLocator)this.Session["serviceLocator"]).GetKQMBellCardQuery().GetDataByCondition(strTableName, condition, Convert.ToInt32(this.Session["PageSize"]), ref this.forwarderPage, ref this.totalPage, ref this.totalRecodrs);
            //this.SetPageInfor(base.forwarderPage, base.totalPage, base.totalRecodrs);
            //this.DataUIBind();
            //base.WriteMessage(0, base.GetResouseValue("common.message.trans.complete"));
        }
        #endregion

    }
}
