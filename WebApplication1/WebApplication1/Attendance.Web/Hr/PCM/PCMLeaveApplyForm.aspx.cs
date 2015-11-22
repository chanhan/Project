/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： PCMLeaveApplyForm.aspx.cs
 * 檔功能描述： 個人中心請假申請
 * 
 * 版本：1.0
 * 創建標識： 陳函 2012.03.10
 * 
 */

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using GDSBG.MiABU.Attendance.BLL.Hr.PCM;
using GDSBG.MiABU.Attendance.BLL.Hr.KQM.KaoQinData;
using System.IO;
using System.Drawing;
using Infragistics.WebUI.UltraWebGrid;
using GDSBG.MiABU.Attendance.Model.KQM.KaoQinData;
using System.Collections.Generic;
using Resources;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.Web.Hr.PCM
{
    public partial class PCMLeaveApplyForm : BasePage
    {
        PCMLeaveApplyBll PCMLeaveApply = new PCMLeaveApplyBll();
        KQMLeaveApplyForm_ZBLHBll leaveApply = new KQMLeaveApplyForm_ZBLHBll();
        KQMLeaveApplyExportBll leaveApplyExport = new KQMLeaveApplyExportBll();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        static SynclogModel logmodel = new SynclogModel();
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("ConfirmDelete", Message.ConfirmDelete);
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
            if (!string.IsNullOrEmpty(Request.QueryString["fileName"]))
            {
                PageHelper.ReturnHTTPStream(MapPath("~/Testify/" + Request.QueryString["fileName"]), false);
            }
            SetCalendar(txtStartDate, txtEndDate);
            if (!IsPostBack)
            {
                PageHelper.ButtonControls(FuncList, pnlShowPanel.Controls, base.FuncListModule);
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;

                hidModuleCode.Value = Request.QueryString["ModuleCode"].ToString();
                if (leaveApply.isLeaveNoAudit().Equals("Y"))
                {
                    //txtStartDate.Text = DateTime.Now.AddDays((double)(-DateTime.Now.Day + 1)).ToString("yyyy/mm/dd");
                    txtStartDate.Text = DateTime.Now.AddDays(-DateTime.Now.Day + 1).ToString("yyyy/MM/dd");
                    txtEndDate.Text = DateTime.Now.AddDays(2).ToString("yyyy/MM/dd");
                    hidStartDate.Value = txtStartDate.Text;
                    hidEndDate.Value = txtEndDate.Text;
                }
                else
                {
                    txtStartDate.Text = DateTime.Now.AddDays(-3).ToString("yyyy/MM/dd");
                    txtEndDate.Text = DateTime.Now.AddDays(3).ToString("yyyy/MM/dd");
                    hidStartDate.Value = txtStartDate.Text;
                    hidEndDate.Value = txtEndDate.Text;
                }
                initDropDownList();
                Query();
            }

            this.EnableButton();
        }
        protected void UltraWebGridLeaveApply_DataBound(object sender, EventArgs e)
        {
            string sStatus = "";
            string sID = "";
            string ThisLVTotal = "";
            string TestifyFile = "";
            string UploadFile = "";
            string LVWorkDays = "";
            for (int i = 0; i < UltraWebGridLeaveApply.Rows.Count; i++)
            {
                //    UltraWebGridLeaveApply.Rows[i].Cells[29].Value = "<a style='text-decoration:none;' href=javascript:openProgress('" + UltraWebGridLeaveApply.Rows[i].Cells[29].Value + "')>" + UltraWebGridLeaveApply.Rows[i].Cells[29].Value + "</a>";
                UltraWebGridLeaveApply.Rows[i].Cells[29].Value = "<a  href=javascript:openProgress('" + UltraWebGridLeaveApply.Rows[i].Cells[29].Value + "')>" + Message.WatchProgress + "</a>";

                sStatus = UltraWebGridLeaveApply.Rows[i].Cells.FromKey("Status").Text.Trim();
                switch (sStatus)
                {
                    case "0":
                        UltraWebGridLeaveApply.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Green;
                        break;
                    case "1":
                        UltraWebGridLeaveApply.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Blue;
                        break;
                    case "3":
                        UltraWebGridLeaveApply.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Maroon;
                        break;
                    case "4":
                        UltraWebGridLeaveApply.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Red;
                        break;
                    default:
                        UltraWebGridLeaveApply.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Black;
                        break;
                }
                //       sID = UltraWebGridLeaveApply.Rows[i].Cells.FromKey("ID").Text.Trim();
                //       ThisLVTotal = leaveApply.getThisLVTotal(sID);
                //       UltraWebGridLeaveApply.Rows[i].Cells.FromKey("ThisLVTotal").Text = ThisLVTotal;
                //       UltraWebGridLeaveApply.Rows[i].Cells.FromKey("LVTotalDays").Text = Convert.ToString(Convert.ToDouble(ThisLVTotal) / 8);
                //       LVWorkDays = leaveApply.getTLVWorkDays(sID);
                //       UltraWebGridLeaveApply.Rows[i].Cells.FromKey("LVWorkDays").Text = Convert.ToString(Convert.ToDouble(LVWorkDays) / 8);

                if (this.UltraWebGridLeaveApply.Rows[i].Cells.FromKey("TestifyFile").Value != null)
                {
                    TestifyFile = UltraWebGridLeaveApply.Rows[i].Cells.FromKey("TestifyFile").Value.ToString();
                    if (!TestifyFile.Equals("Y"))
                    {
                        //  string filePath = Server.MapPath("~/Testify/") + TestifyFile;
                        UltraWebGridLeaveApply.Rows[i].Cells.FromKey("TestifyFile").Value = "<a style='text-decoration:none;' href=javascript:down('" + TestifyFile + "') >" + TestifyFile + "</a>";
                    }
                }
                if (this.UltraWebGridLeaveApply.Rows[i].Cells.FromKey("UploadFile").Value != null)
                {
                    UploadFile = UltraWebGridLeaveApply.Rows[i].Cells.FromKey("UploadFile").Value.ToString();
                    //  UltraWebGridLeaveApply.Rows[i].Cells.FromKey("UploadFile").Value = "<a href=\"..\\Excel\\Testify\\" + UploadFile + "\" target=\"_blank\">" + UploadFile + "</a>";
                    UltraWebGridLeaveApply.Rows[i].Cells.FromKey("UploadFile").Value = "<a style='text-decoration:none;' href=javascript:down('" + UploadFile + "') >" + UploadFile + "</a>";
                }
            }
        }
        private void EnableButton()
        {
            bool bValue = this.UltraWebGridLeaveApply.Rows.Count > 0;
            btnDelete.Enabled = bValue;
        }
        private void initDropDownList()
        {
            DataTable dtLeaveType = leaveApply.getLeaveType();
            ddlLVTypeCode.DataSource = dtLeaveType;
            ddlLVTypeCode.DataTextField = "LVTypeName";
            ddlLVTypeCode.DataValueField = "LVTypeCode";
            ddlLVTypeCode.DataBind();
            ddlLVTypeCode.Items.Insert(0, new ListItem("", ""));

            DataTable dtStatus = leaveApply.getStatus();
            ddlStatus.DataSource = dtStatus;
            ddlStatus.DataTextField = "DataValue";
            ddlStatus.DataValueField = "DataCode";
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("", ""));

            DataTable dtApplyType = leaveApply.getApplyType();
            ddlApplyType.DataSource = dtApplyType;
            ddlApplyType.DataTextField = "DataValue";
            ddlApplyType.DataValueField = "DataCode";
            ddlApplyType.DataBind();
            ddlApplyType.Items.Insert(0, new ListItem("", ""));
        }

        private void Query()
        {

            int totalCount;
            string billNo = txtBillNo.Text.Trim();
            string LVTypeCode = ddlLVTypeCode.SelectedValue.Trim();
            string status = ddlStatus.SelectedValue.Trim();
            string startDate = txtStartDate.Text.Trim();
            string endDate = txtEndDate.Text.Trim();
            string applyType = ddlApplyType.SelectedValue.Trim();
            if (startDate.Length > 0 && endDate.Length > 0)
            {
                if (!Regex.IsMatch(startDate, @"^((\d{2}(([02468][048])|([13579][26]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|([1-2][0-9])))))|(\d{2}(([02468][1235679])|([13579][01345789]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|(1[0-9])|(2[0-8]))))))$"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "addOrUpDateAttType1", "alert('" + Message.StartDateFormateNotRight + "')", true);
                    return;
                }
                if (!Regex.IsMatch(endDate, @"^((\d{2}(([02468][048])|([13579][26]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|([1-2][0-9])))))|(\d{2}(([02468][1235679])|([13579][01345789]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|(1[0-9])|(2[0-8]))))))$"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "addOrUpDateAttType2", "alert('" + Message.EndDateFormateNotRight + "')", true);
                    return;
                }
            }
            DataTable dt = PCMLeaveApply.getApplyData(CurrentUserInfo.Personcode, billNo, LVTypeCode, status, startDate, endDate, applyType, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            UltraWebGridLeaveApply.DataSource = dt;
            pager.RecordCount = totalCount;
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
            UltraWebGridLeaveApply.DataBind();
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtBillNo.Text = "";
            ddlLVTypeCode.SelectedIndex = -1;
            ddlStatus.SelectedIndex = -1;
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            ddlApplyType.SelectedIndex = -1;
            txtStartDate.Text = hidStartDate.Value;
            txtEndDate.Text = hidEndDate.Value;

        }
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
            this.ProcessFlag.Value = "";
        }
        private void WriteMessage(string alert)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "addOrUpDateLeaveApply" + alert, "alert('" + alert + "')", true);
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int intDeleteOk = 0;
                int intDeleteError = 0;
                string ProxyStatus = "";
                string sysKqoQinDays = leaveApply.getKqoQinDays();
                string strModifyDate = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy/MM") + "/" + sysKqoQinDays).ToString("yyyy/MM/dd");
                TemplatedColumn tcol = (TemplatedColumn)UltraWebGridLeaveApply.Bands[0].Columns[0];
                for (int i = 0; i < UltraWebGridLeaveApply.Rows.Count; i++)
                {
                    CellItem GridItem = (CellItem)tcol.CellItems[i];
                    CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                    if (chkIsHaveRight.Checked)
                    {
                        if (DateTime.Parse(UltraWebGridLeaveApply.Rows[i].Cells.FromKey("StartDate").Text.Trim()).ToString("yyyy/MM/dd").CompareTo(System.DateTime.Now.ToString("yyyy/MM") + "/01") == -1 &&
                            strModifyDate.CompareTo(System.DateTime.Now.ToString("yyyy/MM/dd")) <= 0)
                        {
                            WriteMessage(UltraWebGridLeaveApply.Rows[i].Cells.FromKey("WorkNo").Text.Trim() + Message.OverDate + strModifyDate);
                            return;
                        }
                        ProxyStatus = UltraWebGridLeaveApply.Rows[i].Cells.FromKey("ProxyStatus").Value == null ? "" : UltraWebGridLeaveApply.Rows[i].Cells.FromKey("ProxyStatus").Text;
                        if (UltraWebGridLeaveApply.Rows[i].Cells.FromKey("Status").Text.Trim() != "0" && UltraWebGridLeaveApply.Rows[i].Cells.FromKey("Status").Text != "3")
                        {
                            WriteMessage(Message.DeleteApplyovertimeEnd);
                            return;
                        }
                        else
                        {
                            if (ProxyStatus.Equals("1"))
                            {
                                WriteMessage(Message.DeleteApplyovertimeEnd);
                                return;
                            }
                        }
                    }
                }
                for (int i = 0; i < UltraWebGridLeaveApply.Rows.Count; i++)
                {
                    CellItem GridItem = (CellItem)tcol.CellItems[i];
                    CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                    if (chkIsHaveRight.Checked)
                    {
                        DataTable tempDataTable = new DataTable();
                        tempDataTable = leaveApply.getLeaveApplyData(UltraWebGridLeaveApply.Rows[i].Cells.FromKey("ID").Text.Trim());
                        if (tempDataTable.Rows.Count > 0)
                        {
                            leaveApply.DeleteData(tempDataTable,logmodel);
                            if (UltraWebGridLeaveApply.Rows[i].Cells.FromKey("LVTypeCode").Text.Equals("U"))
                            {
                                leaveApply.CountCanAdjlasthy(UltraWebGridLeaveApply.Rows[i].Cells.FromKey("WORKNO").Text);
                            }
                            intDeleteOk += 1;
                        }
                        else
                        {
                            intDeleteError += 1;
                        }
                    }
                }
                if (intDeleteOk + intDeleteError > 0)
                {
                    string alert = Message.SuccssedCount + "：" + intDeleteOk + ";" + Message.FailedCounts + "：" + intDeleteError;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "deleteLeaveApply", "alert('" + alert + "')", true);
                }
                else
                {
                    WriteMessage(Message.AtLastOneChoose);
                    return;
                }
                Query();
                this.ProcessFlag.Value = "";
            }
            catch (System.Exception ex)
            {
                WriteMessage(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            string billNo = txtBillNo.Text.Trim();
            string LVTypeCode = ddlLVTypeCode.SelectedValue.Trim();
            string status = ddlStatus.SelectedValue.Trim();
            string startDate = txtStartDate.Text.Trim();
            string endDate = txtEndDate.Text.Trim();
            string applyType = ddlApplyType.SelectedValue.Trim();
            List<LeaveApplyViewModel> list = leaveApplyExport.getApplyList(CurrentUserInfo.Personcode, billNo, LVTypeCode, status, startDate, endDate, applyType);
            string[] header = { ControlText.gvWorkNo, ControlText.gvLocalName, 
                                  ControlText.gvSexName, ControlText.gvgvbuName, 
                                  ControlText.gvgvDepName, ControlText.gvLVTypeName, 
                                   ControlText.gvStartDate, ControlText.gvStartTime,
                                   ControlText.gvgvEndDate, ControlText.gvEndTime, 
                                   ControlText.gvgvLVTotal, ControlText.gvgvThisLVTotal, 
                                   ControlText.gvgvLVTotalDays, ControlText.gvLVWorkDays, 
                                   ControlText.gvReason,ControlText.gvProxyWorkNo,
                                   ControlText.gvProxyName,ControlText.gvTestifyFile,ControlText.gvUploadFile,ControlText.gvgvApplyTypeName,
                                   ControlText.gvStatusName,ControlText.gvIsLastYear,ControlText.gvBillNo,ControlText.gvgvModifier,
                                   ControlText.gvgvModifyDate};
            string[] properties = { "WorkNo", "LocalName", "SexName","BuName", "DepName", "LVTypeName", "StartDate",
                                      "StartTime", "EndDate", "EndTime", "LVTotal","ThisLvTotal","LvTotalDays","LvWorkDays","Reason","ProxyWorkno","Proxy","TestifyFile","UpLoadFile","ApplyTypeName","StatusName","IsLastYear","BillNo","Update_User","Update_DateStr"};
            string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
            NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
            PageHelper.ReturnHTTPStream(filePath, true);
        }
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            Query();
        }


    }
}
