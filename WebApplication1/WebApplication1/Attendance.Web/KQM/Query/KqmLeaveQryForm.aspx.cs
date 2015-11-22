/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KqmLeaveQryForm.cs
 * 檔功能描述： 請假明細查詢UI層
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2012.01.09
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.KQM.BasicData;
using GDSBG.MiABU.Attendance.BLL.KQM.Query;
using GDSBG.MiABU.Attendance.Model.KQM.Query;
using Resources;

namespace GDSBG.MiABU.Attendance.Web.KQM.Query
{
    public partial class KqmLeaveQryForm : BasePage
    {
        TypeDataBll bllTypeData = new TypeDataBll();
        KqmLeaveQryFormBll bllLeaveQry = new KqmLeaveQryFormBll();
        DataTable tempDT = new DataTable();
        KqmLeaveQryFormModel model = new KqmLeaveQryFormModel();
        static DataTable dt = new DataTable();

        #region 查詢按鈕
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            model = PageHelper.GetModel<KqmLeaveQryFormModel>(pnlContent.Controls);
            model.StatusCode = ddlStatus.SelectedValue;
            pager.CurrentPageIndex = 1;
            DataUIBind();
            this.txtBatchEmployeeNo.Text = "";
        }
        #endregion
        #region 重置按鈕
        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.txtDepCode.Text = "";
            this.txtDepName.Text = "";
            this.txtWorkNo.Text = "";
            this.txtKQDateFrom.Text = "";
            this.txtKQDateTo.Text = "";
            this.txtLocalName.Text = "";
            this.txtBatchEmployeeNo.Text = "";
            this.ddlStatus.ClearSelection();
            this.ddlLeaveType.ClearSelection();
            this.txtKQDateFrom.Text = DateTime.Today.AddDays((double)(-DateTime.Now.Day + 1)).ToString("yyyy-MM-dd");
            this.txtKQDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        #endregion
        #region 請假類別綁定
        private void ddlLeaveTypeDataBind()
        {
            this.tempDT.Clear();
            this.tempDT = bllLeaveQry.GetLeaveType();
            this.ddlLeaveType.DataSource = tempDT;
            this.ddlLeaveType.DataTextField = "LVTypeName";
            this.ddlLeaveType.DataValueField = "LVTypeCode";
            this.ddlLeaveType.DataBind();
        }
        #endregion
        #region 簽核狀態綁定
        private void ddlStatusDataBind()
        {
            this.tempDT.Clear();
            this.tempDT = bllTypeData.GetDataTypeList("'BillAuditState'");
            this.ddlStatus.DataSource = tempDT;
            this.ddlStatus.DataTextField = "DataValue";
            this.ddlStatus.DataValueField = "DataCode";
            this.ddlStatus.DataBind();
            this.ddlStatus.Items.Insert(0, new ListItem("", ""));
        }
        #endregion
        #region 在職狀態綁定
        private void ddlEmpStatusBind()
        {
            this.tempDT.Clear();
            this.tempDT = bllTypeData.GetDataTypeList("'EmpState'");
            this.ddlEmpStatus.DataSource = tempDT;
            this.ddlEmpStatus.DataTextField = "DataValue";
            this.ddlEmpStatus.DataValueField = "DataCode";
            this.ddlEmpStatus.DataBind();
        }
        #endregion
        #region  獲得請假的時數
        public string GetDeductLeaveTotal(string sID, string sStartDate, string sEndDate, string lvTotal)
        {
            if ((this.txtKQDateFrom.Text.Trim().Length > 0) || (this.txtKQDateTo.Text.Trim().Length > 0))
            {
                string strEndDate = sEndDate;
                string LeaveDate = bllLeaveQry.GetLeaveDate(sID);
                string FlagEndDate = bllLeaveQry.GetFlagEndDate(LeaveDate, sEndDate);
                if (this.txtKQDateFrom.Text.Trim().Length > 0)
                {
                    sStartDate = Convert.ToDateTime(this.txtKQDateFrom.Text.Trim()).ToString("yyyy/MM/dd");
                }
                if (this.txtKQDateTo.Text.Trim().Length > 0)
                {
                    sEndDate = Convert.ToDateTime(this.txtKQDateTo.Text.Trim()).ToString("yyyy/MM/dd");
                }
                if (!string.IsNullOrEmpty(LeaveDate) && (Convert.ToDouble(FlagEndDate) > 0.0))
                {
                    strEndDate = LeaveDate;
                }
                lvTotal = bllLeaveQry.GetLeaveDetail(sID, sStartDate, strEndDate);
            }
            return lvTotal;
        }
        #endregion
        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            model = PageHelper.GetModel<KqmLeaveQryFormModel>(pnlContent.Controls);
            model.StatusCode = ddlStatus.SelectedValue;
            DataUIBind();
            this.txtBatchEmployeeNo.Text = "";
        }
        #endregion
        #region 頁面加載
        protected void Page_Load(object sender, EventArgs e)
        {
            PageHelper.ButtonControls(base.FuncList, pnlShowPanel.Controls, base.FuncListModule);
            SetCalendar(txtKQDateFrom, txtKQDateTo);
            if (!base.IsPostBack)
            {
                SetSelector(imgDepCode, txtDepCode, txtDepName, Request.QueryString["ModuleCode"].ToString());
                this.imgBatchWorkNo.Attributes.Add("OnClick", "javascript:ShowBatchWorkNo()");
                this.txtKQDateFrom.Text = DateTime.Today.AddDays((double)(-DateTime.Now.Day + 1)).ToString("yyyy/MM/dd");
                this.txtKQDateTo.Text = DateTime.Now.ToString("yyyy/MM/dd");
                this.ddlLeaveTypeDataBind();
                this.ddlStatusDataBind();
                this.ddlEmpStatusBind();
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
        #region 數據綁定
        private void DataUIBind()
        {
            int totalCount;
            string empStatus = "";
            string BatchEmployeeNo = "";
            string startDate = "";
            string endDate = "";
            string SQLDep = base.SqlDep;
            string str = txtBatchEmployeeNo.Text.Trim();
            if (str != "")
            {
                for (int i = 0; i < str.Split('\n').Length; i++)
                {
                    BatchEmployeeNo = BatchEmployeeNo + str.Split('\n')[i].Trim() + "§";
                }
            }
            string leaveType = "";
            string strTemVal = this.ddlLeaveType.SelectedValue;
            if (strTemVal != "")
            {
                string temStr = this.ddlLeaveType.SelectedValuesToString(",");
                for (int i = 0; i < temStr.Split(',').Length; i++)
                {
                    leaveType = leaveType + temStr.Split(',')[i].Trim() + "§";
                }
            }
            if ((this.txtKQDateFrom.Text.Trim().Length != 0) && (this.txtKQDateTo.Text.Trim().Length != 0))
            {
                startDate = DateTime.Parse(this.txtKQDateFrom.Text.Trim()).ToString("yyyy/MM/dd");
                endDate = DateTime.Parse(this.txtKQDateTo.Text.Trim()).AddDays(1.0).ToString("yyyy/MM/dd");
            }
            empStatus = "";
            if (this.ddlEmpStatus.SelectedValue != "")
            {
                string temStr = this.ddlEmpStatus.SelectedValuesToString(",");
                for (int i = 0; i < temStr.Split(',').Length; i++)
                {
                    empStatus = empStatus + temStr.Split(',')[i].Trim() + "§";
                }
            }
            string depCode = model.DepCode;
            model.DepName = null;
            model.DepCode = null;
            DataTable dtAll = bllLeaveQry.GetLeaveDataList(model, depCode, SQLDep, BatchEmployeeNo, leaveType, startDate, empStatus, endDate, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            dt = bllLeaveQry.GetLeaveDataList(model, depCode, SQLDep, BatchEmployeeNo, leaveType, startDate, empStatus, endDate);
            this.UltraWebGridLeaveQry.DataSource = dtAll;
            this.UltraWebGridLeaveQry.DataBind();
            pager.RecordCount = totalCount;
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
        }
        #endregion

        #region 行綁定事件
        protected void UltraWebGridLeaveQry_DataBound(object sender, EventArgs e)
        {
            for (int i = 0; i < this.UltraWebGridLeaveQry.Rows.Count; i++)
            {
                string sbdate = Convert.ToDateTime(this.UltraWebGridLeaveQry.Rows[i].Cells.FromKey("StartDate").Text.Trim()).AddDays(-1.0).ToString("yyyy/MM/dd");
                string sedate = Convert.ToDateTime(this.UltraWebGridLeaveQry.Rows[i].Cells.FromKey("EndDate").Text.Trim()).ToString("yyyy/MM/dd");
                string sID = this.UltraWebGridLeaveQry.Rows[i].Cells.FromKey("ID").Text.Trim();
                string lvtotal = this.UltraWebGridLeaveQry.Rows[i].Cells.FromKey("LVTotal").Text.Trim();
                lvtotal = this.GetDeductLeaveTotal(sID, sbdate, sedate, lvtotal);
                this.UltraWebGridLeaveQry.Rows[i].Cells.FromKey("ThisLVTotal").Text = lvtotal;
                this.UltraWebGridLeaveQry.Rows[i].Cells.FromKey("LVTotalDays").Text = Convert.ToString((double)(Convert.ToDouble(lvtotal) / 8.0));
            }
        }
        #endregion

        #region 導出
        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count != 0)
            {
                model = PageHelper.GetModel<KqmLeaveQryFormModel>(pnlContent.Controls);
                List<KqmLeaveQryFormModel> list = bllLeaveQry.GetModelList(dt);
                foreach (KqmLeaveQryFormModel modelList in list)
                {
                    modelList.ThisLVTotal = Convert.ToDecimal(GetDeductLeaveTotal(model.ID, model.StartDate, model.EndDate, model.LVTotal.ToString()));
                    modelList.LVTotalDays = Convert.ToDecimal(Convert.ToDouble(model.ThisLVTotal) / 8.0);
                }
                string[] header = { ControlText.gvDepName, ControlText.gvWorkNo, ControlText.gvLocalName, ControlText.gvLeaveType, ControlText.gvSTime, ControlText.gvETime, ControlText.gvLVTotal, ControlText.gvThisLVTotal, ControlText.gvLVTotalDays, ControlText.gvProxy, ControlText.gvReason, ControlText.gvApprover, ControlText.gvStatusName };
                string[] properties = { "DepName", "WorkNo", "LocalName", "LeaveType", "STime", "ETime", "LVTotal", "ThisLVTotal", "LVTotalDays", "Proxy", "Reason", "Approver", "StatusName" };
                string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
                PageHelper.ReturnHTTPStream(filePath, true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.NoDataExport + "')", true);
            }
        }
        #endregion
    }
}
