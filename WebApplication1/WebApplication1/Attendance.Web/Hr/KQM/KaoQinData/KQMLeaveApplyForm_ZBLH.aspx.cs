/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMLeaveApplyForm_ZBLH.aspx.cs
 * 檔功能描述： 請假申請UI類
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
using GDSBG.MiABU.Attendance.BLL.Hr.KQM.KaoQinData;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using Resources;
using GDSBG.MiABU.Attendance.BLL.HRM.EmployeeData;
using System.Drawing;
using Infragistics.WebUI.UltraWebGrid;
using GDSBG.MiABU.Attendance.Model.KQM.KaoQinData;
using System.IO;
using System.Text.RegularExpressions;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.Web.Hr.KQM.KaoQinData
{
    public partial class KQMLeaveApplyForm_ZBLH : BasePage
    {
        string strModuleCode;
        bool Privileged = true;//組織權限
        HrmEmpOtherMoveBll hrmEmpOtherMoveBll = new HrmEmpOtherMoveBll();
        KQMLeaveApplyForm_ZBLHBll leaveApply = new KQMLeaveApplyForm_ZBLHBll();
        KQMLeaveApplyExportBll leaveApplyExport = new KQMLeaveApplyExportBll();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        static SynclogModel logmodel = new SynclogModel();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(Request.QueryString["fileName"]))
            {
                PageHelper.ReturnHTTPStream(MapPath("~/Testify/" + Request.QueryString["fileName"]), false);
            }
            SetCalendar(txtStartDate, txtEndDate, txtApplyStartDate, txtApplyEndDate, txtApproveDate);
            strModuleCode = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("StartDateEndDateNotNull", Message.StartDateEndDateNotNull);
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
                ClientMessage.Add("NoCloseBillNoCancel", Message.NoCloseBillNoCancel);
                ClientMessage.Add("SelectCancelBill", Message.SelectCancelBill);
                ClientMessage.Add("ConfirmCancle", Message.ConfirmCancle);
                ClientMessage.Add("NoApprovalNoCanel", Message.NoApprovalNoCanel);
                ClientMessage.Add("NoSelect", Message.NoSelect);
                ClientMessage.Add("UnAudit", Message.UnAudit);
                ClientMessage.Add("AuditUnaudit", Message.AuditUnaudit);
                ClientMessage.Add("SelectBill", Message.SelectBill);
                ClientMessage.Add("NoApprovalBillNoVerify", Message.NoApprovalBillNoVerify);
                ClientMessage.Add("NoApprovalOrRefuseNoUpdate", Message.NoApprovalOrRefuseNoUpdate);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
            IsHavePrivileged();
            hidPrivileged.Value = Privileged == true ? "true" : "false";
            if (!IsPostBack)
            {
                PageHelper.ButtonControls(FuncList, pnlShowPanel.Controls, base.FuncListModule);
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;

                strModuleCode = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                hidModuleCode.Value = strModuleCode;
                ImageDepCode.Attributes.Add("onclick", "javascript:GetTreeDataValue(\"txtDepCode\",'" + strModuleCode + "','txtDepName')");
                initDropDownList();
                if (leaveApply.isLeaveNoAudit().Equals("Y"))
                {
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
                EnableButton();
            }
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

            ddlIsLastYear.Items.Insert(0, new ListItem("", ""));
            ddlIsLastYear.Items.Insert(1, new ListItem("N", "N"));
            ddlIsLastYear.Items.Insert(2, new ListItem("Y", "Y"));
        }

        #region  是否有組織權限
        /// <summary>
        /// 是否有組織權限
        /// </summary>
        private void IsHavePrivileged()
        {

            if (CurrentUserInfo.Personcode.Equals("internal") || CurrentUserInfo.RoleCode.Equals("Person"))
            {
                Privileged = false;
            }
            else
            {
                DataTable dt = hrmEmpOtherMoveBll.GetDataByCondition(strModuleCode);
                if ((dt != null) && (dt.Rows.Count > 0))
                {
                    Privileged = false;
                }
            }
        }
        #endregion

        private void EnableButton()
        {
            bool bValue = UltraWebGridLeaveApply.Rows.Count > 0 ? true : false;
            btnApproved.Enabled = bValue;
            btnCancelApproved.Enabled = bValue;
            btnDelete.Enabled = bValue;
            btnCancelLeave.Enabled = bValue;
            btnCancelCancelLeave.Enabled = bValue;
            btnSendAudit.Enabled = bValue;
         //   btnOrgAudit.Enabled = bValue;
        }
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            if (txtWorkNo.Text.Trim().Length == 0 && txtLocalName.Text.Trim().Length == 0 && txtBillNo.Text.Trim().Length == 0)
            {
                if (this.txtStartDate.Text.Trim().Length == 0 || txtEndDate.Text.Trim().Length == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "StartDateEndDateNotNull", "alert('" + Message.StartDateEndDateNotNull + "')", true);
                    return;
                }
                else
                {
                    if (!Regex.IsMatch(this.txtStartDate.Text.Trim(), @"^((\d{2}(([02468][048])|([13579][26]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|([1-2][0-9])))))|(\d{2}(([02468][1235679])|([13579][01345789]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|(1[0-9])|(2[0-8]))))))$"))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "addOrUpDateAttType1", "alert('" + Message.StartDateFormateNotRight + "')", true);
                        return;
                    }
                    if (!Regex.IsMatch(this.txtEndDate.Text.Trim(), @"^((\d{2}(([02468][048])|([13579][26]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|([1-2][0-9])))))|(\d{2}(([02468][1235679])|([13579][01345789]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|(1[0-9])|(2[0-8]))))))$"))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "addOrUpDateAttType2", "alert('" + Message.EndDateFormateNotRight + "')", true);
                        return;
                    }
                    //if (!CheckDateMonths(txtStartDate.Text.Trim(), txtEndDate.Text.Trim()))
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "addOrUpDateAttType3", "alert('" + Message.DateThreeMonths + "')", true);
                    //    return;
                    //}
                }
            }
            DataBind();
            EnableButton();
        }
        protected void btnExport_Click(object sender, System.EventArgs e)
        {
            string depName = txtDepName.Text.Trim();
            string billNo = txtBillNo.Text.Trim();
            string workNo = txtWorkNo.Text.Trim();
            string localName = txtLocalName.Text.Trim();
            string LVTypeCode = ddlLVTypeCode.SelectedValue.Trim();
            string status = ddlStatus.SelectedValue.Trim();
            string testify = ddlTestify.SelectedValue.Trim();
            string startDate = txtStartDate.Text.Trim();
            string endDate = txtEndDate.Text.Trim();
            string applyStartDate = txtApplyStartDate.Text.Trim();
            string applyEndDate = txtApplyEndDate.Text.Trim();
            string applyType = ddlApplyType.SelectedValue.Trim();
            bool flag = chkFlag.Checked;
            string IsLastYear = ddlIsLastYear.SelectedValue.Trim();
            List<LeaveApplyViewModel> list = leaveApplyExport.getApplyData(Privileged, SqlDep, depName, billNo, workNo, localName, LVTypeCode, status, testify, startDate, endDate, applyStartDate, applyEndDate, applyType, flag, IsLastYear);

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
        private bool CheckData(string txtValue, string ReValue)
        {
            if (string.IsNullOrEmpty(txtValue))
            {
                WriteMessage(ReValue);
                return false;
            }
            return true;
        }
        /// <summary>
        /// 審核 表單狀態 0（未核准）——》2（已核准）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, System.EventArgs e)
        {
            string id = "";
            string approver = "";
            string approvedate = "";
            if (this.UltraWebGridLeaveApply.Rows.Count == 0)
            {
                return;
            }
            if (!CheckData(txtApprover.Text, Message.AuthorizerNotNull) || !CheckData(txtApproveDate.Text, Message.AuthorizerDateNotNull))
            {
                return;
            }
            try
            {
                TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGridLeaveApply.Bands[0].Columns[0];
                for (int i = 0; i < this.UltraWebGridLeaveApply.Rows.Count; i++)
                {
                    if (((tcol.CellItems[i] as CellItem).FindControl("CheckBoxCell") as CheckBox).Checked)
                    {
                        if (!this.UltraWebGridLeaveApply.Rows[i].Cells.FromKey("Status").Text.Trim().Equals("0"))
                        {
                            WriteMessage(Message.CommonMessageAuditUnaudit);
                            return;
                        }
                        else
                        {
                            id = this.UltraWebGridLeaveApply.Rows[i].Cells.FromKey("ID").Text.Trim();
                            approver = txtApprover.Text.Trim();
                            approvedate = txtApproveDate.Text.Trim();
                            leaveApply.changeStatusByID(id, approver, approvedate, "2", logmodel);
                            DataBind();

                            this.ProcessFlag.Value = "";
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                WriteMessage(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        /// <summary>
        /// 取消核准  表單狀態 2——》0 ，代理人狀態變為1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelApproved_Click(object sender, EventArgs e)
        {
            string id = "";
            if (this.UltraWebGridLeaveApply.Rows.Count == 0)
            {
                return;
            }
            try
            {
                string sysKqoQinDays = leaveApply.getKqoQinDays();
                string strModifyDate = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy/MM") + "/" + sysKqoQinDays).ToString("yyyy/MM/dd");

                TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGridLeaveApply.Bands[0].Columns[0];

                for (int i = 0; i < this.UltraWebGridLeaveApply.Rows.Count; i++)
                {
                    if (((tcol.CellItems[i] as CellItem).FindControl("CheckBoxCell") as CheckBox).Checked)
                    {
                        if (!this.UltraWebGridLeaveApply.Rows[i].Cells.FromKey("Status").Text.Trim().Equals("2"))
                        {
                            WriteMessage(Message.NoApprovalBillNoVerify);
                            return;
                        }
                        else if (DateTime.Parse(UltraWebGridLeaveApply.Rows[i].Cells.FromKey("StartDate").Text.Trim()).ToString("yyyy/MM/dd").CompareTo(System.DateTime.Now.ToString("yyyy/MM") + "/01") == -1 &&
                                strModifyDate.CompareTo(System.DateTime.Now.ToString("yyyy/MM/dd")) <= 0)
                        {
                            WriteMessage(UltraWebGridLeaveApply.Rows[i].Cells.FromKey("WorkNo").Text.Trim() + Message.OvertopOperateDate + strModifyDate);
                            return;
                        }
                        else
                        {

                            id = this.UltraWebGridLeaveApply.Rows[i].Cells.FromKey("ID").Text.Trim();
                            string proxyStatus = UltraWebGridLeaveApply.Rows[i].Cells.FromKey("ProxyStatus").Value == null ? "" : UltraWebGridLeaveApply.Rows[i].Cells.FromKey("ProxyStatus").Text;
                            leaveApply.changeStatusByID(id, "0", proxyStatus,logmodel);
                            DataBind();
                            this.ProcessFlag.Value = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteMessage(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        protected void btnSendAudit_Click(object sender, EventArgs e)
        {
            if (this.UltraWebGridLeaveApply.Rows.Count == 0)
            {

                return;
            }
            try
            {
                int intSendOK = 0;
                int intSendBillNo = 0;
                int intSendError = 0;
                string OrgCode = "",  AuditOrgCode = "", BillTypeCode = "D002", ProxyNotes = "", BillNoType = "KQL";
                string LevelCode = "", ManagerCode = "", LVTypeCode = "";
                double LVTotal = 0;
                string ProxyStatus = "";
                TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGridLeaveApply.Bands[0].Columns[0];
                for (int i = 0; i < UltraWebGridLeaveApply.Rows.Count; i++)
                {
                    CellItem GridItem = (CellItem)tcol.CellItems[i];
                    CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                    if (chkIsHaveRight.Checked)
                    {
                        ProxyStatus = UltraWebGridLeaveApply.Rows[i].Cells.FromKey("ProxyStatus").Value == null ? "" : UltraWebGridLeaveApply.Rows[i].Cells.FromKey("ProxyStatus").Text;
                        if (UltraWebGridLeaveApply.Rows[i].Cells.FromKey("Status").Text == "0")
                        {
                            if (ProxyStatus == "0" || ProxyStatus == "2" || ProxyStatus == "")
                            { }
                            else
                            {
                                WriteMessage(Message.NoApprovalNoAudit);
                                return;
                            }
                        }
                        else
                        {
                            WriteMessage(Message.NoApprovalNoAudit);
                            return;
                        }
                    }
                }
                for (int i = 0; i < this.UltraWebGridLeaveApply.Rows.Count; i++)
                {
                    if (((tcol.CellItems[i] as CellItem).FindControl("CheckBoxCell") as CheckBox).Checked)
                    {
                        if (UltraWebGridLeaveApply.Rows[i].Cells.FromKey("LevelCode").Value != null)
                        {
                            LevelCode = UltraWebGridLeaveApply.Rows[i].Cells.FromKey("LevelCode").Text.Trim();
                        }
                        else
                        {
                            LevelCode = "";
                        }
                        if (UltraWebGridLeaveApply.Rows[i].Cells.FromKey("ManagerCode").Value != null)
                        {
                            ManagerCode = UltraWebGridLeaveApply.Rows[i].Cells.FromKey("ManagerCode").Text.Trim();
                        }
                        else
                        {
                            ManagerCode = "";
                        }
                        ProxyStatus = UltraWebGridLeaveApply.Rows[i].Cells.FromKey("ProxyStatus").Value == null ? "" : UltraWebGridLeaveApply.Rows[i].Cells.FromKey("ProxyStatus").Text;
                        LVTypeCode = UltraWebGridLeaveApply.Rows[i].Cells.FromKey("LVTypeCode").Text.Trim();
                        LVTotal = Convert.ToDouble(UltraWebGridLeaveApply.Rows[i].Cells.FromKey("LVTotal").Text.Trim());
                        ProxyNotes = UltraWebGridLeaveApply.Rows[i].Cells.FromKey("ProxyNotes").Value == null ? "" : UltraWebGridLeaveApply.Rows[i].Cells.FromKey("ProxyNotes").Text;
                        OrgCode = String.IsNullOrEmpty(hidOrgCode.Value) ? UltraWebGridLeaveApply.Rows[i].Cells.FromKey("dCode").Text.Trim() : hidOrgCode.Value.Trim();
                        string empNo = UltraWebGridLeaveApply.Rows[i].Cells.FromKey("WORKNO").Value == null ? "" : UltraWebGridLeaveApply.Rows[i].Cells.FromKey("WORKNO").Text;
                        string startDate = Convert.ToDateTime(UltraWebGridLeaveApply.Rows[i].Cells.FromKey("StartDate").Text).ToString("yyyy/MM/dd") + " " + Convert.ToDateTime(UltraWebGridLeaveApply.Rows[i].Cells.FromKey("StartTime").Text).ToString("HH:mm");
                        string endDate = Convert.ToDateTime(UltraWebGridLeaveApply.Rows[i].Cells.FromKey("EndDate").Text).ToString("yyyy/MM/dd") + " " + Convert.ToDateTime(UltraWebGridLeaveApply.Rows[i].Cells.FromKey("EndTime").Text).ToString("HH:mm");
                        string lvtypeCode = UltraWebGridLeaveApply.Rows[i].Cells.FromKey("LVTypeCode").Value == null ? "" : UltraWebGridLeaveApply.Rows[i].Cells.FromKey("LVTypeCode").Text;
                        AuditOrgCode = leaveApply.getWorkFlowOrgCode(OrgCode, BillTypeCode, lvtypeCode, startDate, endDate, empNo, lvtypeCode);
                        if (AuditOrgCode.Length > 0)
                        {


                            string billNo = UltraWebGridLeaveApply.Rows[i].Cells.FromKey("BillNo").Value == null ? "" : UltraWebGridLeaveApply.Rows[i].Cells.FromKey("BillNo").Text;
                            string reason = UltraWebGridLeaveApply.Rows[i].Cells.FromKey("Reason").Value == null ? "" : UltraWebGridLeaveApply.Rows[i].Cells.FromKey("Reason").Text;
                            string sFlow_LevelRemark = Message.flow_levelremark;
                            bool bResult = leaveApply.SaveAuditData(sFlow_LevelRemark, UltraWebGridLeaveApply.Rows[i].Cells.FromKey("ID").Text.Trim(), BillNoType, AuditOrgCode, BillTypeCode, CurrentUserInfo.Personcode, reason, empNo, startDate, endDate, lvtypeCode, logmodel);
                            if (bResult)
                            {
                                intSendBillNo += 1;
                                intSendOK += 1;
                            }
                            else
                            {
                                intSendError++;
                            }
                         

                        }
                        else
                        {
                            //沒有定義簽核流程
                            intSendError++;
                        }
                    }
                }
                if (intSendOK + intSendError > 0)
                {
                    if (intSendError > 0)
                    {
                        WriteMessage(Message.SuccssedCount + "：" + intSendOK + ";" + Message.ProduceBillConut + "：" + intSendBillNo + ";" + Message.FailedCounts + "：" + intSendError + "(" + Message.NoDefineFlow + ")");
                    }
                    else
                    {
                        WriteMessage(Message.SuccssedCount + "：" + intSendOK + ";" + Message.ProduceBillConut + "：" + intSendBillNo);
                    }
                }
                else
                {
                    WriteMessage(Message.AtLastOneChoose);
                    return;
                }
                DataBind();
                this.ProcessFlag.Value = "";
            }
            catch (Exception ex)
            {
                WriteMessage(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        protected void btnOrgAudit_Click(object sender, System.EventArgs e)
        {
            if (this.UltraWebGridLeaveApply.Rows.Count == 0)
            {
                return;
            }
            int intSendOK = 0;
            int intSendBillNo = 0;
            int intSendError = 0;
            string OrgCode = "",  AuditOrgCode = "", BillTypeCode = "D002",  BillNoType = "KQL";
            string ProxyStatus = "";
            TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGridLeaveApply.Bands[0].Columns[0];
            for (int i = 0; i < UltraWebGridLeaveApply.Rows.Count; i++)
            {
                CellItem GridItem = (CellItem)tcol.CellItems[i];
                CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                if (chkIsHaveRight.Checked)
                {
                    ProxyStatus = UltraWebGridLeaveApply.Rows[i].Cells.FromKey("ProxyStatus").Value == null ? "" : UltraWebGridLeaveApply.Rows[i].Cells.FromKey("ProxyStatus").Text;
                    if (UltraWebGridLeaveApply.Rows[i].Cells.FromKey("Status").Text == "0")
                    {
                        if (ProxyStatus == "0" || ProxyStatus == "2" || ProxyStatus == "")
                        { }
                        else
                        {
                            WriteMessage(Message.NoApprovalNoAudit);
                            return;
                        }
                    }
                    else
                    {
                        WriteMessage(Message.NoApprovalNoAudit);
                        return;
                    }
                }
            }

            Dictionary<string, List<string>> dicy = new Dictionary<string, List<string>>();
            for (int i = 0; i < this.UltraWebGridLeaveApply.Rows.Count; i++)
            {
                CellItem GridItem = (CellItem)tcol.CellItems[i];
                CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                if (chkIsHaveRight.Checked)
                {
                    OrgCode = String.IsNullOrEmpty(hidOrgCode.Value) ? UltraWebGridLeaveApply.Rows[i].Cells.FromKey("dCode").Text.Trim() : hidOrgCode.Value.Trim();

                    string lvType = UltraWebGridLeaveApply.Rows[i].Cells.FromKey("LVTypeCode").Value == null ? "" : UltraWebGridLeaveApply.Rows[i].Cells.FromKey("LVTypeCode").Text;
                    string startDate = Convert.ToDateTime(UltraWebGridLeaveApply.Rows[i].Cells.FromKey("StartDate").Text).ToString("yyyy/MM/dd") + " " + Convert.ToDateTime(UltraWebGridLeaveApply.Rows[i].Cells.FromKey("StartTime").Text).ToString("HH:mm");
                    string endDate = Convert.ToDateTime(UltraWebGridLeaveApply.Rows[i].Cells.FromKey("EndDate").Text).ToString("yyyy/MM/dd") + " " + Convert.ToDateTime(UltraWebGridLeaveApply.Rows[i].Cells.FromKey("EndTime").Text).ToString("HH:mm");
                    string empNo = UltraWebGridLeaveApply.Rows[i].Cells.FromKey("WORKNO").Value == null ? "" : UltraWebGridLeaveApply.Rows[i].Cells.FromKey("WORKNO").Text;
                    string lvTotalDays = UltraWebGridLeaveApply.Rows[i].Cells.FromKey("LVTotalDays").Value == null ? "" : UltraWebGridLeaveApply.Rows[i].Cells.FromKey("LVTotalDays").Text;
                    AuditOrgCode = leaveApply.getWorkFlowOrgCode(OrgCode, BillTypeCode, lvType, startDate, endDate, empNo, lvTotalDays);
                    if (AuditOrgCode.Length > 0)
                    {
                        string keyAdd = leaveApply.getAuditType(startDate, endDate, empNo, lvTotalDays);
                        string key = AuditOrgCode + "^" + lvType + "^" + keyAdd;
                        List<string> list = new List<string>();
                        if (!dicy.ContainsKey(key))
                        {
                            dicy.Add(key, list);
                        }
                    }

                    else if (AuditOrgCode.Length == 0)
                    {
                        intSendError += 1;
                    }
                    AuditOrgCode = "";
                }
            }

            for (int i = 0; i < this.UltraWebGridLeaveApply.Rows.Count; i++)
            {
                CellItem GridItem = (CellItem)tcol.CellItems[i];
                CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                if (chkIsHaveRight.Checked)
                {
                    OrgCode = String.IsNullOrEmpty(hidOrgCode.Value) ? UltraWebGridLeaveApply.Rows[i].Cells.FromKey("dCode").Text.Trim() : hidOrgCode.Value.Trim();
                    string lvType = UltraWebGridLeaveApply.Rows[i].Cells.FromKey("LVTypeCode").Value == null ? "" : UltraWebGridLeaveApply.Rows[i].Cells.FromKey("LVTypeCode").Text;
                    string startDate = Convert.ToDateTime(UltraWebGridLeaveApply.Rows[i].Cells.FromKey("StartDate").Text).ToString("yyyy/MM/dd") + " " + Convert.ToDateTime(UltraWebGridLeaveApply.Rows[i].Cells.FromKey("StartTime").Text).ToString("HH:mm");
                    string endDate = Convert.ToDateTime(UltraWebGridLeaveApply.Rows[i].Cells.FromKey("EndDate").Text).ToString("yyyy/MM/dd") + " " + Convert.ToDateTime(UltraWebGridLeaveApply.Rows[i].Cells.FromKey("EndTime").Text).ToString("HH:mm");
                    string empNo = UltraWebGridLeaveApply.Rows[i].Cells.FromKey("WORKNO").Value == null ? "" : UltraWebGridLeaveApply.Rows[i].Cells.FromKey("WORKNO").Text;
                    string lvTotalDays = UltraWebGridLeaveApply.Rows[i].Cells.FromKey("LVTotalDays").Value == null ? "" : UltraWebGridLeaveApply.Rows[i].Cells.FromKey("LVTotalDays").Text;
                    AuditOrgCode = leaveApply.getWorkFlowOrgCode(OrgCode, BillTypeCode, lvType, startDate, endDate, empNo, lvTotalDays);
                    string reason = UltraWebGridLeaveApply.Rows[i].Cells.FromKey("Reason").Value == null ? "" : UltraWebGridLeaveApply.Rows[i].Cells.FromKey("Reason").Text;
              
                    if (AuditOrgCode.Length > 0)
                    {
                        string keyAdd = leaveApply.getAuditType(startDate, endDate, empNo, lvTotalDays);
                        string key = AuditOrgCode + "^" + lvType + "^" + keyAdd;
                        if (dicy[key] != null)
                        {
                            dicy[key].Add(UltraWebGridLeaveApply.Rows[i].Cells.FromKey("ID").Text.Trim());
                        }
                    }
                }
            }
            string Flow_LevelRemark=Message.flow_levelremark;
            int count = leaveApply.SaveOrgAuditData(Flow_LevelRemark, "Add", dicy, BillNoType, BillTypeCode, CurrentUserInfo.Personcode);
            intSendBillNo = count;
            if (intSendBillNo > 0)
            {
                intSendOK += 1;
            }
            if (intSendOK + intSendError > 0)
            {
                if (intSendError > 0)
                {
                    WriteMessage(Message.SuccssedCount + "：" + intSendOK + ";" + Message.ProduceBillConut + "：" + intSendBillNo + ";" + Message.FailedCounts + "：" + intSendError + "(" + Message.NoDefineFlow + ")");
                }
                else
                {
                    WriteMessage(Message.SuccssedCount + "：" + intSendOK + ";" + Message.ProduceBillConut + "：" + intSendBillNo);
                }
            }
            else
            {
                WriteMessage(Message.AtLastOneChoose);
                return;
            }
            DataBind();
            this.ProcessFlag.Value = "";
        }

        /// <summary>
        /// 取消銷假   已結案變成已核准 4——》2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelCancelLeave_Click(object sender, System.EventArgs e)
        {
            if (this.UltraWebGridLeaveApply.Rows.Count == 0)
            {
                return;
            }

            int iCount = 0;
            string id = "";
            try
            {

                string sysKqoQinDays = leaveApply.getKqoQinDays();
                string strModifyDate = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy/MM") + "/" + sysKqoQinDays).ToString("yyyy/MM/dd");

                TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGridLeaveApply.Bands[0].Columns[0];
                for (int i = 0; i < this.UltraWebGridLeaveApply.Rows.Count; i++)
                {
                    if (((tcol.CellItems[i] as CellItem).FindControl("CheckBoxCell") as CheckBox).Checked)
                    {
                        if (!this.UltraWebGridLeaveApply.Rows[i].Cells.FromKey("Status").Text.Trim().Equals("4"))
                        {
                            WriteMessage(Message.NoCloseBillNoCancel);
                            return;
                        }
                        else if (DateTime.Parse(UltraWebGridLeaveApply.Rows[i].Cells.FromKey("StartDate").Text.Trim()).ToString("yyyy/MM/dd").CompareTo(System.DateTime.Now.ToString("yyyy/MM") + "/01") == -1 &&
                                strModifyDate.CompareTo(System.DateTime.Now.ToString("yyyy/MM/dd")) <= 0)
                        {
                            WriteMessage(UltraWebGridLeaveApply.Rows[i].Cells.FromKey("WorkNo").Text.Trim() + Message.OvertopOperateDate + strModifyDate);
                            return;
                        }
                        else
                        {
                            id = this.UltraWebGridLeaveApply.Rows[i].Cells.FromKey("ID").Text.Trim();
                           string proxyStatus = this.UltraWebGridLeaveApply.Rows[i].Cells.FromKey("ProxyStatus").Text.Trim();
                            leaveApply.changeStatusByID(id, "2",proxyStatus, logmodel);
                        }
                        iCount++;
                    }
                }
                if (iCount == 0)
                {
                    WriteMessage(Message.SelectCancelBill);
                    return;
                }
                else
                {
                    DataBind();
                    this.ProcessFlag.Value = "";
                }
            }
            catch (Exception ex)
            {
                WriteMessage(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        protected void btnDelete_Click(object sender, System.EventArgs e)
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
                            WriteMessage(Message.bfw_kqm_PCMAdvanceApplyForm_noDelect);
                            return;
                        }
                        else
                        {
                            if (ProxyStatus.Equals("1"))
                            {
                                WriteMessage(Message.bfw_kqm_PCMAdvanceApplyForm_noDelect);
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
                            leaveApply.DeleteData(tempDataTable, logmodel);
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
                    WriteMessage(Message.common_message_data_select);
                    return;
                }
                DataBind();
                this.ProcessFlag.Value = "";
            }
            catch (System.Exception ex)
            {
                WriteMessage(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }

        private void WriteMessage(string alert)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "addOrUpDateLeaveApply" + alert, "alert('" + alert + "')", true);
        }

        //private bool CheckDateMonths(string startDate, string endDate)
        //{
        //    return leaveApply.CheckDateMonths(startDate, endDate);
        //}

        private void DataBind()
        {
            int totalCount;
            string depName = txtDepName.Text.Trim();
            string billNo = txtBillNo.Text.Trim();
            string workNo = txtWorkNo.Text.Trim();
            string localName = txtLocalName.Text.Trim();
            string LVTypeCode = ddlLVTypeCode.SelectedValue.Trim();
            string status = ddlStatus.SelectedValue.Trim();
            string testify = ddlTestify.SelectedValue.Trim();
            string startDate = txtStartDate.Text.Trim();
            string endDate = txtEndDate.Text.Trim();
            string applyStartDate = txtApplyStartDate.Text.Trim();
            string applyEndDate = txtApplyEndDate.Text.Trim();
            string applyType = ddlApplyType.SelectedValue.Trim();
            bool flag = chkFlag.Checked;
            string IsLastYear = ddlIsLastYear.SelectedValue.Trim();
            DataTable dt = leaveApply.getApplyData(Privileged, SqlDep, depName, billNo, workNo, localName, LVTypeCode, status, testify, startDate, endDate, applyStartDate, applyEndDate, applyType, flag, IsLastYear, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            UltraWebGridLeaveApply.DataSource = dt;
            pager.RecordCount = totalCount;
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
            UltraWebGridLeaveApply.DataBind();
        }
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            DataBind();
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
                UltraWebGridLeaveApply.Rows[i].Cells[30].Value = "<a  href=javascript:openProgress('" + UltraWebGridLeaveApply.Rows[i].Cells[30].Value + "')>" + Message.WatchProgress + "</a>";

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
                string proxyStatus = UltraWebGridLeaveApply.Rows[i].Cells.FromKey("ProxyStatus").Value == null ? "" : UltraWebGridLeaveApply.Rows[i].Cells.FromKey("ProxyStatus").Text;
                switch (proxyStatus)
                {
                    case "0":
                        UltraWebGridLeaveApply.Rows[i].Cells.FromKey("proxystatusname").Style.ForeColor = Color.Green;
                        break;
                    case "1":
                        UltraWebGridLeaveApply.Rows[i].Cells.FromKey("proxystatusname").Style.ForeColor = Color.Blue;
                        break;
                    case "3":
                        UltraWebGridLeaveApply.Rows[i].Cells.FromKey("proxystatusname").Style.ForeColor = Color.Maroon;
                        break;
                    case "4":
                        UltraWebGridLeaveApply.Rows[i].Cells.FromKey("proxystatusname").Style.ForeColor = Color.Red;
                        break;
                    default:
                        UltraWebGridLeaveApply.Rows[i].Cells.FromKey("proxystatusname").Style.ForeColor = Color.Black;
                        break;
                }
        
                if (this.UltraWebGridLeaveApply.Rows[i].Cells.FromKey("TestifyFile").Value != null)
                {
                    TestifyFile = UltraWebGridLeaveApply.Rows[i].Cells.FromKey("TestifyFile").Value.ToString();
                    if (!TestifyFile.Equals("Y"))
                    {
                        UltraWebGridLeaveApply.Rows[i].Cells.FromKey("TestifyFile").Value = "<a  href=javascript:down('" + TestifyFile + "') >" + TestifyFile + "</a>";
                    }
                }
                if (this.UltraWebGridLeaveApply.Rows[i].Cells.FromKey("UploadFile").Value != null)
                {
                    UploadFile = UltraWebGridLeaveApply.Rows[i].Cells.FromKey("UploadFile").Value.ToString();
                    UltraWebGridLeaveApply.Rows[i].Cells.FromKey("UploadFile").Value = "<a  href=javascript:down('" + UploadFile + "') >" + UploadFile + "</a>";
                }
            }
        }
    }
}
