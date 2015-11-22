/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMReGetKaoQin.cs
 * 檔功能描述： 重新計算UI層
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2012.01.09
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.KQM.AttendanceData;
using GDSBG.MiABU.Attendance.BLL.KQM.BasicData;
using GDSBG.MiABU.Attendance.BLL.KQM.Query;
using GDSBG.MiABU.Attendance.Model.KQM.AttendanceData;
using GDSBG.MiABU.Attendance.Model.KQM.Query;
using Resources;

namespace GDSBG.MiABU.Attendance.Web.KQM.AttendanceData
{
    public partial class KQMReGetKaoQin : BasePage
    {
        KQMReGetKaoQinModel modelKaoQin = new KQMReGetKaoQinModel();
        TypeDataBll bllTypeData = new TypeDataBll();
        BellCardQueryBll bllBellData = new BellCardQueryBll();
        DataTable tempDataTable = new DataTable();
        static DataTable dt = new DataTable();
        BellCardDataModel model = new BellCardDataModel();
        KQMReGetKaoQinBll bllKaoQin = new KQMReGetKaoQinBll();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        #region 重新計算
        protected void btnCount_Click(object sender, EventArgs e)
        {
            string alertText = "";
            string alert = "";
            string sWorkNo = "";
            string sFromKQDate = "";
            string sToKQDate = "";
            if ((this.txtKQDateFrom.Text.Trim().Length == 0) || (this.txtKQDateTo.Text.Trim().Length == 0))
            {
                alert = "alert('" + Message.KQDateNotNull + "')";
                Page.ClientScript.RegisterStartupScript(GetType(), "show", alert, true);
            }
            else
            {
                string sysKqoQinDays = bllKaoQin.GetValueLastDay();
                string strModifyDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM") + "/" + sysKqoQinDays).ToString("yyyy/MM/dd");
                if (this.txtWorkNo.Text.ToString().Trim().Length > 0)
                {
                    sWorkNo = this.txtWorkNo.Text.Trim();
                    if (sWorkNo.StartsWith("f"))
                    {
                        sWorkNo = sWorkNo.ToUpper();
                    }
                    sFromKQDate = Convert.ToDateTime(this.txtKQDateFrom.Text.ToString()).ToString("yyyy/MM/dd");
                    if (!(CurrentUserInfo.RoleCode.ToString().Equals("ADMIN") || (sFromKQDate.CompareTo(DateTime.Now.AddDays(-30.0).ToString("yyyy/MM") + "/01") == -1)))
                    {
                        if ((sFromKQDate.CompareTo(DateTime.Now.ToString("yyyy/MM") + "/01") == -1) && (strModifyDate.CompareTo(DateTime.Now.ToString("yyyy/MM/dd")) <= 0))
                        {
                            alertText = Message.checkreget + ":" + strModifyDate;
                            alert = "alert('" + alertText + "')";
                            Page.ClientScript.RegisterStartupScript(GetType(), "show", alert, true);
                            return;
                        }
                        if (sFromKQDate.CompareTo(DateTime.Now.AddMonths(-1).ToString("yyyy/MM") + "/01") == -1)
                        {
                            alertText = Message.checkreget + ":" + strModifyDate;
                            alert = "alert('" + alertText + "')";
                            Page.ClientScript.RegisterStartupScript(GetType(), "show", alert, true);
                            return;
                        }
                    }
                    sToKQDate = this.txtKQDateTo.Text.ToString();
                    if (DateTime.Parse(sToKQDate) > DateTime.Today)
                    {
                        sToKQDate = DateTime.Today.ToString("yyyy/MM/dd");
                    }
                    bllKaoQin.GetKaoQinData(sWorkNo, "null", sFromKQDate, sToKQDate);
                }
                else
                {
                    if (this.UltraWebGridReGetKaoQin.Rows.Count == 0)
                    {
                        alert = "alert('" + Message.CheckQuery + "')";
                        Page.ClientScript.RegisterStartupScript(GetType(), "show", alert, true);
                        return;
                    }
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sWorkNo = dt.Rows[i]["WorkNo"].ToString();
                        sFromKQDate = Convert.ToDateTime(dt.Rows[i]["KQDate"].ToString()).ToString("yyyy/MM/dd");
                        if ((sFromKQDate.CompareTo(DateTime.Now.ToString("yyyy/MM") + "/01") == -1) && (strModifyDate.CompareTo(DateTime.Now.ToString("yyyy/MM/dd")) <= 0))
                        {
                            if (!(CurrentUserInfo.RoleCode.ToString().Equals("ADMIN") && (sFromKQDate.CompareTo(DateTime.Now.AddDays(-30.0).ToString("yyyy/MM") + "/01") != -1)))
                            {
                                alertText = Message.checkreget + ":" + strModifyDate;
                                alert = "alert('" + alertText + "')";
                                Page.ClientScript.RegisterStartupScript(GetType(), "show", alert, true);
                                return;
                            }
                        }
                        else if ((sFromKQDate.CompareTo(DateTime.Now.AddMonths(-1).ToString("yyyy/MM") + "/01") == -1) && !(this.Session["roleCode"].ToString().Equals("Admin") && (sFromKQDate.CompareTo(DateTime.Now.AddDays(-30.0).ToString("yyyy/MM") + "/01") != -1)))
                        {
                            alertText = Message.checkreget + ":" + strModifyDate;
                            alert = "alert('" + alertText + "')";
                            Page.ClientScript.RegisterStartupScript(GetType(), "show", alert, true);
                            return;
                        }
                        bllKaoQin.GetKaoQinData(sWorkNo, "null", sFromKQDate, sFromKQDate);
                    }
                }
            }
            modelKaoQin = PageHelper.GetModel<KQMReGetKaoQinModel>(pnlContent.Controls);
            modelKaoQin.DCode = txtDepCode.Text.Trim().ToString();
            pager.CurrentPageIndex = 1;
            DataUIBind();
            this.txtBatchEmployeeNo.Text = "";
        }
        #endregion
        #region 查詢按鈕
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            modelKaoQin = PageHelper.GetModel<KQMReGetKaoQinModel>(pnlContent.Controls);
            modelKaoQin.DCode = txtDepCode.Text.Trim().ToString();
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
            this.ddlStatus.ClearSelection();
            this.ddlShiftNo.ClearSelection();
            this.ddlExceptionType.ClearSelection();
            this.txtKQDateFrom.Text = DateTime.Today.AddDays(-1.0).ToString("yyyy/MM/dd");
            this.txtKQDateTo.Text = DateTime.Now.ToString("yyyy/MM/dd");
        }
        #endregion
        #region 異常原因
        private void ddlExceptionTypeDataBind()
        {
            this.tempDataTable.Clear();
            this.tempDataTable = bllTypeData.GetExceptionTypeList();
            this.ddlExceptionType.DataSource = tempDataTable;
            this.ddlExceptionType.DataTextField = "DataValue";
            this.ddlExceptionType.DataValueField = "DataCode";
            this.ddlExceptionType.DataBind();
        }
        #endregion
        #region 班別（白班和晚班）？沒有中班？
        private void ddlShiftNoDataBind()
        {
            this.tempDataTable.Clear();
            this.tempDataTable = bllTypeData.GetDataList();
            this.ddlShiftNo.DataSource = tempDataTable;
            this.ddlShiftNo.DataTextField = "DataValue";
            this.ddlShiftNo.DataValueField = "DataCode";
            this.ddlShiftNo.DataBind();
            this.ddlShiftNo.Items.Insert(0, new ListItem("", ""));
        }
        #endregion
        #region 計算的結果
        private void ddlStatusDataBind()
        {
            this.tempDataTable.Clear();
            this.tempDataTable = bllTypeData.GetKqmKaoQinStatusList();
            this.ddlStatus.DataSource = tempDataTable;
            this.ddlStatus.DataTextField = "DataValue";
            this.ddlStatus.DataValueField = "DataCode";
            this.ddlStatus.DataBind();
            this.ddlStatus.Items.Insert(0, new ListItem("", ""));
        }
        #endregion
        #region 頁面加載
        protected void Page_Load(object sender, EventArgs e)
        {
            PageHelper.ButtonControls(base.FuncList, pnlShowControls.Controls,base.FuncListModule);
            SetCalendar(txtKQDateFrom, txtKQDateTo);
            if (!base.IsPostBack)
            {
                SetSelector(imgDepCode, txtDepCode, txtDepName, Request.QueryString["ModuleCode"].ToString());
                this.imgBatchWorkNo.Attributes.Add("OnClick", "javascript:ShowBatchWorkNo()");
                this.ddlCountType.Items.Insert(0, new ListItem(Message.CounQuery0, "A"));
                this.ddlExceptionTypeDataBind();
                this.ddlShiftNoDataBind();
                this.ddlStatusDataBind();
                this.txtKQDateFrom.Text = DateTime.Today.AddDays(-1.0).ToString("yyyy/MM/dd");
                this.txtKQDateTo.Text = DateTime.Now.ToString("yyyy/MM/dd");
                pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
            }
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
                ClientMessage.Add("ToLaterThanFrom", Message.ToLaterThanFrom);
                ClientMessage.Add("WrongDate", Message.WrongDate);
                ClientMessage.Add("TextBoxNotNull", Message.TextBoxNotNull);
                ClientMessage.Add("DeleteConfirm", Message.DeleteConfirm);
                ClientMessage.Add("btnImport", Message.btnImport);
                ClientMessage.Add("btnBack", Message.btnBack);
                ClientMessage.Add("WorkNo", Message.WorkNo);
                ClientMessage.Add("No", Message.No);
                ClientMessage.Add("LocalName", Message.LocalName);
                ClientMessage.Add("CardNo", Message.CardNo);
                ClientMessage.Add("CardTime", Message.CardTime);
                ClientMessage.Add("BellNo", Message.BellNo);
                ClientMessage.Add("ReadTime", Message.ReadTime);
                ClientMessage.Add("KQDateNotNull", Message.KQDateNotNull);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
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
            int totalCount = 0;
            string BatchEmployeeNo = "";
            string SqlDep = base.SqlDep;
            string str = txtBatchEmployeeNo.Text.Trim();
            if (str != "")
            {
                for (int i = 0; i < str.Split('\n').Length; i++)
                {
                    BatchEmployeeNo = BatchEmployeeNo + str.Split('\n')[i].Trim() + "§";
                }
            }
            string temVal = "";
            string strTemVal = this.ddlExceptionType.SelectedValue;
            if (strTemVal != "")
            {
                string temStr = this.ddlExceptionType.SelectedValuesToString(",");
                for (int i = 0; i < temStr.Split(',').Length; i++)
                {
                    temVal = temVal + temStr.Split(',')[i].Trim() + "§";
                }
            }
            string shiftNo = this.ddlShiftNo.SelectedValue.ToString().Trim();
            string ststus = this.ddlStatus.SelectedValue.Trim();
            string fromDate = this.txtKQDateFrom.Text.Trim();
            string toDate = this.txtKQDateTo.Text.Trim();
            string depCode = modelKaoQin.DCode;
            modelKaoQin.DCode = null;
            modelKaoQin.DepName = null;
            DataTable dtSel = bllKaoQin.GetKaoQinDataList(modelKaoQin, BatchEmployeeNo, temVal, shiftNo, ststus, fromDate, toDate, SqlDep, depCode, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            dt = bllKaoQin.GetKaoQinDataList(modelKaoQin, BatchEmployeeNo, temVal, shiftNo, ststus, fromDate, toDate, SqlDep, depCode);
            pager.RecordCount = totalCount;
            this.UltraWebGridReGetKaoQin.DataSource = dtSel;
            this.UltraWebGridReGetKaoQin.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
            for (int i = 0; i < this.UltraWebGridReGetKaoQin.Rows.Count; i++)
            {
                if ((this.UltraWebGridReGetKaoQin.Rows[i].Cells.FromKey("Status").Value != null) && this.UltraWebGridReGetKaoQin.Rows[i].Cells.FromKey("Status").Text.Equals("1"))
                {
                    this.UltraWebGridReGetKaoQin.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Red;
                }
            }
        }
        #endregion
        #region webgrid行綁定
        protected void UltraWebGridReGetKaoQin_DataBound(object sender, EventArgs e)
        {
            string ExceptionType = "";
            string ExceptionCode = "";
            string ExceptionName = "";
            for (int i = 0; i < this.UltraWebGridReGetKaoQin.Rows.Count; i++)
            {
                this.UltraWebGridReGetKaoQin.Rows[i].Cells.FromKey("KQDate").TargetURL = "javascript:ShowDetail('" + this.UltraWebGridReGetKaoQin.Rows[i].Cells.FromKey("WorkNo").Text + "','" + Convert.ToDateTime(this.UltraWebGridReGetKaoQin.Rows[i].Cells.FromKey("KQDate").Text).ToString("yyyy/MM/dd") + "','" + this.UltraWebGridReGetKaoQin.Rows[i].Cells.FromKey("ShiftNo").Text + "')";
                ExceptionType = (this.UltraWebGridReGetKaoQin.Rows[i].Cells.FromKey("ExceptionType").Value == null) ? "" : this.UltraWebGridReGetKaoQin.Rows[i].Cells.FromKey("ExceptionType").Text;
                if (ExceptionType.Length > 0)
                {
                    for (int j = 0; j < ExceptionType.Length; j++)
                    {
                        ExceptionCode = ExceptionType.Substring(j, 1);
                        tempDataTable = bllTypeData.GetDataList(ExceptionCode);
                        if (this.tempDataTable.Rows.Count > 0)
                        {
                            ExceptionName = ExceptionName + this.tempDataTable.Rows[0]["DataValue"].ToString() + "+";
                        }
                        this.tempDataTable.Clear();
                    }
                }
                if (ExceptionName.Length > 0)
                {
                    ExceptionName = ExceptionName.Substring(0, ExceptionName.Length - 1);
                    this.UltraWebGridReGetKaoQin.Rows[i].Cells.FromKey("ExceptionName").Value = ExceptionName;
                }
                ExceptionType = "";
                ExceptionCode = "";
                ExceptionName = "";
            }
        }
        #endregion
        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            modelKaoQin = PageHelper.GetModel<KQMReGetKaoQinModel>(pnlContent.Controls);
            modelKaoQin.DCode = txtDepCode.Text.Trim().ToString();
            DataUIBind();
            this.txtBatchEmployeeNo.Text = "";
        }
        #endregion
        #region Ajaxs事件
        protected override void AjaxProcess()
        {
            int result = 0;
            string noticeJson = null;
            if (!string.IsNullOrEmpty(Request.Form["FromDate"]))
            {
                if (string.IsNullOrEmpty(Request.Form["EmployeeNo"]))
                {
                    string FromDate = Request.Form["FromDate"];
                    string ToDate = Request.Form["ToDate"];
                    DataTable flag = bllKaoQin.GetFromDate(FromDate, ToDate);
                    if (Convert.ToInt32(flag.Rows[0]["sDays"].ToString()) >= 3)
                    {
                        result = 1;
                    }
                }
                else
                {
                    result = 0;
                }
                Response.Clear();
                Response.Write(result.ToString());
                Response.End();
            }
            if (!string.IsNullOrEmpty(Request.Form["WorkNo"]))
            {
                model.WorkNo = Request.Form["WorkNo"].ToString();
                string KQDate = Request.Form["KQDate"].ToString();
                string ShiftNo = Request.Form["ShiftNo"].ToString();
                List<BellCardDataModel> list = bllBellData.GetBellDataReList(model, KQDate, ShiftNo);
                if (list != null)
                {
                    noticeJson = JsSerializer.Serialize(list);
                }
                Response.Clear();
                Response.Write(noticeJson);
                Response.End();
            }
        }
        #endregion
        #region 導出
        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count != 0)
            {
                modelKaoQin = PageHelper.GetModel<KQMReGetKaoQinModel>(pnlContent.Controls);
                List<KQMReGetKaoQinModel> list = bllKaoQin.GetModelList(dt);
                string[] header = { ControlText.gvDepName, ControlText.gvWorkNo, ControlText.gvLocalName, ControlText.gvKQDate, ControlText.gvStatusName, ControlText.gvShiftDesc, ControlText.gvOnDutyTime, ControlText.gvOffDutyTime, ControlText.gvOTOnDutyTime, ControlText.gvOTOffDutyTime, ControlText.gvAbsentQty, ControlText.gvExceptionName, ControlText.gvHeadReasonName, ControlText.gvReasonRemark };
                string[] properties = { "DepName", "WorkNo", "LocalName", "KQDate", "StatusName", "ShiftDesc", "OnDutyTime", "OffDutyTime", "OTOnDutyTime", "OTOffDutyTime", "AbsentQty", "ExceptionName", "ReasonName", "ReasonRemark" };
                string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
                PageHelper.ReturnHTTPStream(filePath, true);
            }
            else
            {
                lblupload.Text = Message.NoDataExport;
            }
        }
        #endregion
    }
}
