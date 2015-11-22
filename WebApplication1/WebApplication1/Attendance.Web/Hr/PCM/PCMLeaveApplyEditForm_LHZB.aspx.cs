/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： PCMLeaveApplyEditForm_LHZB.aspx.cs
 * 檔功能描述： 個人中心請假申請新增修改
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
using GDSBG.MiABU.Attendance.BLL.Hr.PCM;
using System.Text;
using Resources;
using GDSBG.MiABU.Attendance.Model.Hr.PCM;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.Web.Hr.PCM
{
    public partial class PCMLeaveApplyEditForm_LHZB : BasePage
    {
        KQMLeaveApplyForm_ZBLHBll leaveApply = new KQMLeaveApplyForm_ZBLHBll();
        PCMLeaveApplyBll pCMLeaveApply = new PCMLeaveApplyBll();
        string dateFormat = "yyyy/MM/dd";
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        static SynclogModel logmodel = new SynclogModel();
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (ClientMessage == null)
                {
                    ClientMessage = new Dictionary<string, string>();
                    ClientMessage.Add("ConsortIn", Message.ConsortIn);
                    ClientMessage.Add("ConsortWorkNo", Message.ConsortWorkNo);
                    ClientMessage.Add("ConsortName", Message.ConsortName);
                    ClientMessage.Add("ConsortCode", Message.ConsortCode);
                    ClientMessage.Add("ConsortBg", Message.ConsortBg);
                    ClientMessage.Add("BillRefuseAudit", Message.BillRefuseAudit);
                    ClientMessage.Add("InputWorkNoFirst", Message.InputWorkNoFirst);
                    ClientMessage.Add("otm_nowshiftno", Message.otm_nowshiftno);
                    ClientMessage.Add("otm_exception_errorshiftno_1", Message.otm_exception_errorshiftno_1);
                    ClientMessage.Add("SelectBill", Message.SelectBill);
                    ClientMessage.Add("NoApprovalBillNoVerify", Message.NoApprovalBillNoVerify);
                    ClientMessage.Add("SaveingAndAudit", Message.SaveingAndAudit);
                    ClientMessage.Add("EmpBasicInfoNotExist", Message.EmpBasicInfoNotExist);


                }
                string clientmsg = JsSerializer.Serialize(ClientMessage);
                Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
                SetCalendar(txtStartDate, txtEndDate);
                if (!this.IsPostBack)
                {
                    logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                    logmodel.TransactionType = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                    logmodel.LevelNo = "2";
                    logmodel.FromHost = Request.UserHostAddress;

                    hidMarryFlag.Value = leaveApply.getMarryFlag() == "Y" ? "Y" : "N";
                    DataTable tempDataSet = new DataTable();
                    #region

                    DataTable empData = pCMLeaveApply.getEmployeeDataByCondition(CurrentUserInfo.Personcode);
                    string strSex = "";
                    string marrystate = "";
                    foreach (DataRow row in empData.Rows)
                    {
                        strSex = row["Sex"].ToString();
                        marrystate = row["marrystate"].ToString();
                    }
                    //添加參數管控
                    string flag = leaveApply.getMarryFlag();
                    //if (flag == "Y")
                    //{
                    //    if (!string.IsNullOrEmpty(marrystate))
                    //    {
                    //        if (marrystate == "Y")
                    //        {
                    //            marrystate_condition = " and lvtypecode <> 'J' ";
                    //        }
                    //    }
                    //}
                    #endregion
                    if (strSex.Equals("1"))
                    {
                        tempDataSet = pCMLeaveApply.getKQMLeaveTypeData(marrystate, "0");
                    }
                    else
                    {
                        tempDataSet = pCMLeaveApply.getKQMLeaveTypeData(marrystate, "1");
                    }
                    this.ddlLVTypeCode.DataSource = tempDataSet.DefaultView;
                    this.ddlLVTypeCode.DataTextField = "LVTypeName";
                    this.ddlLVTypeCode.DataValueField = "LVTypeCode";
                    this.ddlLVTypeCode.DataBind();
                    this.ddlLVTypeCode.Items.Insert(0, new ListItem("", ""));
                    tempDataSet.Clear();
                    tempDataSet = leaveApply.getApplyType();
                    this.ddlApplyType.DataSource = tempDataSet.DefaultView;
                    this.ddlApplyType.DataTextField = "DataValue";
                    this.ddlApplyType.DataValueField = "DataCode";
                    this.ddlApplyType.DataBind();
                    this.ddlApplyType.Items.Insert(0, new ListItem("", ""));
                    tempDataSet.Clear();
                    tempDataSet = leaveApply.getLevelCode();
                    ddlLevelCode.DataSource = tempDataSet.DefaultView;
                    ddlLevelCode.DataTextField = "LevelName";
                    ddlLevelCode.DataValueField = "LevelCode";
                    ddlLevelCode.DataBind();
                    ddlLevelCode.Items.Insert(0, new ListItem("", ""));


                    txtStartDate.Attributes.Add("onpropertychange", "javascript:GetLVTotal();");
                    txtStartDate.Attributes.Add("onafterpaste", "javascript:GetLVTotal();");

                    txtProxyWorkNo.Attributes.Add("onblur", "GetDeputy();");

                    txtEndDate.Attributes.Add("onpropertychange", "javascript:GetLVTotal();");
                    txtEndDate.Attributes.Add("onafterpaste", "javascript:GetLVTotal();");

                    this.ddlLVTypeCode.Attributes.Add("onchange", "onChangeLVTypeCode(this.options[this.selectedIndex].value)");
                    ddlConsortInFoxonn.Attributes.Add("onchange", "ConsortIsMandatory(this.options[this.selectedIndex].value)");


                    string BillNo = this.Request.QueryString["BillNo"] == null ? "" : this.Request.QueryString["BillNo"].ToString();
                    string ProcessFlag = this.Request.QueryString["ProcessFlag"] == null ? "" : this.Request.QueryString["ProcessFlag"].ToString();
                    this.ImageWorkNo.Attributes.Add("OnClick", "javascript:GetEmpDataValue('txtProxyWorkNo','txtProxy','txtProxyNotes')");
                    this.hidBillNo.Value = BillNo;
                    this.ProcessFlag.Value = ProcessFlag;

                    //是否保存之後直接核准
                    hidLeaveNoAudit.Value = leaveApply.isLeaveNoAudit();
                    if (ProcessFlag == "Add")
                    {
                        //選擇申請單需要通知的窗口

                        tempDataSet = pCMLeaveApply.GetDataSetBySQL(CurrentUserInfo.Personcode);
                        this.ddlWindow.DataSource = tempDataSet;
                        this.ddlWindow.DataTextField = "localname";
                        this.ddlWindow.DataValueField = "personcode";
                        this.ddlWindow.DataBind();
                        this.ddlWindow.Items.Insert(0, new ListItem("", ""));


                        Add();
                        EmpQuery(CurrentUserInfo.Personcode);
                    }
                    else
                    {
                        this.divSystemMSG.Visible = false;
                        if (BillNo.Length > 0)
                        {
                            Modify(BillNo);
                        }
                        else
                        {
                            Response.Write("<script type='text/javascript'>alert('" + Message.AtLastOneChoose + "');window.parent.document.all.topTable.style.display='';window.parent.document.all.divEdit.style.display='none';</script>");
                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                WriteMessage(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }
        private void EmpQuery(string employeeNo)
        {

            DataTable tempDataTable = pCMLeaveApply.getVDataByCondition(employeeNo);
            if (tempDataTable.Rows.Count > 0)
            {
                leaveApply.CountCanAdjlasthy(employeeNo);
                foreach (System.Data.DataRow newRow in tempDataTable.Rows)
                {
                    txtEmployeeNo.Text = newRow["WORKNO"].ToString();
                    txtName.Text = newRow["LOCALNAME"].ToString();
                    txtDPcode.Text = newRow["DName"].ToString();
                    txtSex.Text = newRow["SEX"].ToString();
                    txtJoinDate.Text = newRow["JoinDate"].ToString().Trim() == "" ? "" : string.Format("{0:" + dateFormat + "}", newRow["JoinDate"]);
                    txtManager.Text = newRow["managername"].ToString();
                    hidManagerCode.Value = newRow["ManagerCode"].ToString();
                    txtLevelCode.Text = newRow["levelname"].ToString();
                    hidLevelCode.Value = newRow["LevelCode"].ToString();
                    hidDCode.Value = newRow["DCode"].ToString();
                    txtComeYears.Text = newRow["comeyears"].ToString();
                    if (this.ProcessFlag.Value == "Add")
                    {
                        txtStartDate.Text = "";
                        txtStartTime.Text = "";
                        txtEndDate.Text = "";
                        txtEndTime.Text = "";
                    }
                    GetEmpLeave(employeeNo.ToUpper());
                }
            }
            else
            {
                txtEmployeeNo.Text = "";
                txtName.Text = "";
                txtDPcode.Text = "";
                txtSex.Text = "";

                txtStartDate.Text = "";
                txtStartTime.Text = "";
                txtEndDate.Text = "";
                txtEndTime.Text = "";
                txtReason.Text = "";
                txtProxy.Text = "";
                txtLVTotal.Text = "";
                txtRemark.Text = "";
                hidBillNo.Value = "";
                ddlLVTypeCode.SelectedIndex = -1;
                ddlApplyType.SelectedIndex = -1;
                hidLVTypeCode.Value = "";
                hidApplyType.Value = "";
                txtBillNo.Text = "";

                WriteMessage(Message.EmpBasicInfoNotExist);
            }
        }

        private void WriteMessage(string alert)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "addOrUpDateLeaveApply" + alert, "alert('" + alert + "')", true);
        }
        private void GetEmpLeave(string empNo)
        {
            string strSex = leaveApply.getSexCode(empNo);
            StringBuilder sbHeader = new StringBuilder();
            StringBuilder sbAble = new StringBuilder();
            StringBuilder sbAlready = new StringBuilder();
            string sql = "";
            string sTime = "";
            string strYears = leaveApply.getInWorkYears(empNo);
            double dYears = 0.0;
            double dJLimitdays = Convert.ToDouble(leaveApply.getLimitdays());
            if (!string.IsNullOrEmpty(strYears))
            {
                dYears = Convert.ToDouble(strYears);
                if (dYears > 1.0)
                {
                    dYears = 1.0;
                }
            }
            string strAges = leaveApply.getAges(empNo);
            string strJLimitdays = leaveApply.getJLimitdays();
            string SpecLimitDays = leaveApply.getSpecLimitDays(empNo);
            if (!string.IsNullOrEmpty(SpecLimitDays))
            {
                strJLimitdays = SpecLimitDays;
            }
            if (!string.IsNullOrEmpty(strAges))
            {
                if (strSex.Equals("1") && (Convert.ToDouble(strAges) >= 25.0))
                {
                    dJLimitdays = Convert.ToDouble(strJLimitdays);
                }
                else if (strSex.Equals("0") && (Convert.ToDouble(strAges) >= 23.0))
                {
                    dJLimitdays = Convert.ToDouble(strJLimitdays);
                }
            }
            int LeaveTypeCount = 0;
            DataTable tempDt = new DataTable();
            if (strSex.Equals("1"))
            {
                tempDt = leaveApply.getLeaveTypeCount("0");
            }
            else
            {
                tempDt = leaveApply.getLeaveTypeCount("1");
            }
            LeaveTypeCount = tempDt.Rows.Count;
            string intWidth = Convert.ToString(Math.Round((double)((100 / LeaveTypeCount) - 0.5)));
            sbHeader.Append("<table class='inner_table' cellspacing='0' cellpadding='0' width='100%'>");
            sbHeader.Append(string.Concat(new object[] { " <tr><td class='td_label' style='width:", intWidth, "%'><font style='FONT-WEIGHT: bold; FONT-SIZE: 12pt; COLOR: navy'>", DateTime.Now.Year, "</font></td>" }));
            sbAble.Append(" <tr><td class='td_label' style='width:" + intWidth + "%'><font style='FONT-WEIGHT: bold; FONT-SIZE: 12pt; COLOR: navy'>" + Message.ableRest + "</font></td>");
            sbAlready.Append(" <tr><td class='td_label' style='width:" + intWidth + "%'><font style='FONT-WEIGHT: bold; FONT-SIZE: 12pt; COLOR: navy'>" + Message.alreadyRest + "</font></td>");
            DataTable dt = new DataTable();
            if (strSex.Equals("1"))
            {

                dt = leaveApply.getDataByCondition("0");
            }
            else
            {
                dt = leaveApply.getDataByCondition("1");
            }
            this.ddlLVTypeCode.DataSource = dt;
            this.ddlLVTypeCode.DataTextField = "LVTypeName";
            this.ddlLVTypeCode.DataValueField = "LVTypeCode";
            this.ddlLVTypeCode.DataBind();
            this.ddlLVTypeCode.Items.Insert(0, new ListItem("", ""));
            double LVTotal = Convert.ToDouble(leaveApply.getLVTotal(empNo).Rows[0][0].ToString());
            if ((tempDt != null) && (tempDt.Rows.Count > 0))
            {
                for (int i = 0; i < tempDt.Rows.Count; i++)
                {
                    SpecLimitDays = leaveApply.getLimitDays(empNo, tempDt.Rows[i]["LVTypeCode"].ToString());
                    if (tempDt.Rows[i]["LVTypeCode"].ToString().Equals("Y"))
                    {
                        string[] temVal = leaveApply.getYearLeaveDays(empNo, DateTime.Now.Year.ToString(), DateTime.Now.ToString("yyyy-MM-dd")).Split(new char[] { '|' });
                        sbAble.Append("<td class='td_label' style='width:" + intWidth + "%'><font style='FONT-WEIGHT: bold; FONT-SIZE: 12pt; COLOR: navy'>" + Convert.ToString(Math.Ceiling((double)((Convert.ToDouble(temVal[0].ToString()) * 8.0) * dYears))) + "</font></td>");
                        sbHeader.Append("<td class='td_label' style='width:" + intWidth + "%'><font style='FONT-WEIGHT: bold; FONT-SIZE: 12pt; COLOR: navy'>" + tempDt.Rows[i]["LVTypeName"].ToString() + "(H)</font></td>");
                        sbAlready.Append(" <td class='td_label' style='width:" + intWidth + "%'><font style='FONT-WEIGHT: bold; FONT-SIZE: 12pt; COLOR: navy'>" + Convert.ToString(Math.Ceiling((double)((Convert.ToDouble(temVal[2].ToString()) * 8.0) * dYears))) + "</font></td>");
                    }
                    else
                    {
                        sbHeader.Append("<td class='td_label' style='width:" + intWidth + "%'><font style='FONT-WEIGHT: bold; FONT-SIZE: 12pt; COLOR: navy'>" + tempDt.Rows[i]["LVTypeName"].ToString() + "(H)</font></td>");
                        if (!tempDt.Rows[i]["LimitDays"].ToString().Equals("") && !tempDt.Rows[i]["LVTypeCode"].ToString().Equals("U"))
                        {
                            if (Convert.ToString(tempDt.Rows[i]["LVTypeCode"]).Equals("I") || Convert.ToString(tempDt.Rows[i]["LVTypeCode"]).Equals("T"))
                            {
                                if (!string.IsNullOrEmpty(SpecLimitDays))
                                {
                                    sbAble.Append("<td class='td_label' style='width:" + intWidth + "%'><font style='FONT-WEIGHT: bold; FONT-SIZE: 12pt; COLOR: navy'>" + Convert.ToString(Math.Ceiling((double)((Convert.ToDouble(SpecLimitDays) * 8.0) * dYears))) + "</font></td>");
                                }
                                else
                                {
                                    sbAble.Append("<td class='td_label' style='width:" + intWidth + "%'><font style='FONT-WEIGHT: bold; FONT-SIZE: 12pt; COLOR: navy'>" + Convert.ToString(Math.Ceiling((double)((Convert.ToDouble(tempDt.Rows[i]["LimitDays"].ToString()) * 8.0) * dYears))) + "</font></td>");
                                }
                            }
                            else if (tempDt.Rows[i]["LVTypeCode"].ToString().Equals("J"))
                            {
                                sbAble.Append("<td class='td_label' style='width:" + intWidth + "%'><font style='FONT-WEIGHT: bold; FONT-SIZE: 12pt; COLOR: navy'>" + Convert.ToString((double)(dJLimitdays * 8.0)) + "</font></td>");
                            }
                            else if (!string.IsNullOrEmpty(SpecLimitDays))
                            {
                                sbAble.Append("<td class='td_label' style='width:" + intWidth + "%'><font style='FONT-WEIGHT: bold; FONT-SIZE: 12pt; COLOR: navy'>" + Convert.ToString(Math.Ceiling((double)((Convert.ToDouble(SpecLimitDays) * 8.0) * dYears))) + "</font></td>");
                            }
                            else
                            {
                                sbAble.Append("<td class='td_label' style='width:" + intWidth + "%'><font style='FONT-WEIGHT: bold; FONT-SIZE: 12pt; COLOR: navy'>" + Convert.ToString(Math.Ceiling((double)(Convert.ToDouble(tempDt.Rows[i]["LimitDays"].ToString()) * 8.0))) + "</font></td>");
                            }
                        }
                        else if (tempDt.Rows[i]["LVTypeCode"].ToString().Equals("U"))
                        {
                            sTime = leaveApply.getTime(empNo, LVTotal);
                            sTime = sTime.Equals("") ? "-" : sTime;
                            sbAble.Append("<td class='td_label' style='width:" + intWidth + "%'><font style='FONT-WEIGHT: bold; FONT-SIZE: 12pt; COLOR: navy'>" + sTime + "</font></td>");
                        }
                        else
                        {
                            sbAble.Append("<td class='td_label' style='width:" + intWidth + "%'><font style='FONT-WEIGHT: bold; FONT-SIZE: 12pt; COLOR: navy'>-</font></td>");
                        }
                        if (tempDt.Rows[i]["LVTypeCode"].ToString().Equals("U"))
                        {
                            sTime = leaveApply.getSumlvTotal(LVTotal, empNo, tempDt.Rows[i]["LVTypeCode"].ToString(), true);
                        }
                        else
                        {
                            sTime = leaveApply.getSumlvTotal(LVTotal, empNo, tempDt.Rows[i]["LVTypeCode"].ToString(), false);
                        }
                        sTime = sTime.Equals("") ? "-" : sTime;
                        sbAlready.Append(" <td class='td_label' style='width:" + intWidth + "%'><font style='FONT-WEIGHT: bold; FONT-SIZE: 12pt; COLOR: navy'>" + sTime + "</font></td>");
                    }
                }
                sbAlready.Append("</tr></table>");
                sbAble.Append("</tr>");
                sbHeader.Append("</tr>");
                divEmpLeave.InnerHtml = sbHeader.ToString() + sbAble.ToString() + sbAlready.ToString();
            }
        }
        protected void Modify(string BillNo)
        {
            this.DataUIBind(BillNo);
            this.ProcessFlag.Value = "Modify";
            TextBoxsReset("Modify", false);
        }

        private void DataUIBind(string billNo)
        {

            DataTable saveDataTable = leaveApply.getLeaveApply(billNo);
            if (saveDataTable.Rows.Count > 0)
            {
                EmpQuery(saveDataTable.Rows[0]["WorkNo"].ToString());
                txtStartDate.Text = saveDataTable.Rows[0]["StartDate"].ToString().Trim() == null ? "" : string.Format("{0:" + dateFormat + "}", Convert.ToDateTime(saveDataTable.Rows[0]["StartDate"].ToString().Trim()));
                txtEndDate.Text = saveDataTable.Rows[0]["EndDate"].ToString().Trim() == null ? "" : string.Format("{0:" + dateFormat + "}", Convert.ToDateTime(saveDataTable.Rows[0]["EndDate"].ToString().Trim()));
                txtStartTime.Value = saveDataTable.Rows[0]["StartTime"].ToString().Trim() == null ? DateTime.Parse("0001/01/01 00:00") : Convert.ToDateTime(saveDataTable.Rows[0]["StartTime"].ToString().Trim());
                txtEndTime.Value = saveDataTable.Rows[0]["EndTime"].ToString().Trim() == null ? DateTime.Parse("0001/01/01 00:00") : Convert.ToDateTime(saveDataTable.Rows[0]["EndTime"].ToString().Trim());

                string LVTotal = saveDataTable.Rows[0]["LVTotal"].ToString().Trim() == null ? "" : saveDataTable.Rows[0]["LVTotal"].ToString().Trim();
                txtLVTotal.Text = LVTotal;
                if (LVTotal.Length > 0)
                {
                    txtLVTotalDays.Text = Convert.ToString(Convert.ToDouble(LVTotal) / 8);
                }
                txtReason.Text = saveDataTable.Rows[0]["Reason"].ToString().Trim() == null ? "" : saveDataTable.Rows[0]["Reason"].ToString().Trim();
                txtProxy.Text = saveDataTable.Rows[0]["Proxy"].ToString().Trim() == null ? "" : saveDataTable.Rows[0]["Proxy"].ToString().Trim();
                txtProxyWorkNo.Text = saveDataTable.Rows[0]["ProxyWorkNo"].ToString().Trim() == null ? "" : saveDataTable.Rows[0]["ProxyWorkNo"].ToString().Trim();
                txtProxyNotes.Text = saveDataTable.Rows[0]["ProxyNotes"].ToString().Trim() == null ? "" : saveDataTable.Rows[0]["ProxyNotes"].ToString().Trim();
                hidProxyFlag.Value = saveDataTable.Rows[0]["ProxyFlag"].ToString().Trim() == null ? "" : saveDataTable.Rows[0]["ProxyFlag"].ToString().Trim();
                txtRemark.Text = saveDataTable.Rows[0]["Remark"].ToString().Trim() == null ? "" : saveDataTable.Rows[0]["Remark"].ToString().Trim();
                ddlLVTypeCode.SelectedIndex = this.ddlLVTypeCode.Items.IndexOf(this.ddlLVTypeCode.Items.FindByValue(saveDataTable.Rows[0]["LVTypeCode"].ToString()));
                txtEmergencyContactPerson.Text = saveDataTable.Rows[0]["emergencycontactperson"].ToString();
                txtEmergencyTelephone.Text = saveDataTable.Rows[0]["emergencytelephone"].ToString();
                //    txtConsortAllowance.Text = saveDataTable.Rows[0]["WifeLivePay"].ToString();
                txtConsortDepCode.Text = saveDataTable.Rows[0]["WifeBG"].ToString();
                txtConsortName.Text = saveDataTable.Rows[0]["WifeName"].ToString();
                txtConsortWorkNo.Text = saveDataTable.Rows[0]["WifeWorkNo"].ToString();
                ddlConsortInFoxonn.SelectedIndex = this.ddlConsortInFoxonn.Items.IndexOf(this.ddlConsortInFoxonn.Items.FindByValue(saveDataTable.Rows[0]["WifeISFoxconn"].ToString()));
                ddlLevelCode.SelectedIndex = this.ddlLevelCode.Items.IndexOf(this.ddlLevelCode.Items.FindByValue(saveDataTable.Rows[0]["WifeLevelName"].ToString()));
                this.ddlApplyType.SelectedValue = saveDataTable.Rows[0]["ApplyType"].ToString().Trim() == null ? "" : saveDataTable.Rows[0]["ApplyType"].ToString().Trim();
                hidLVTypeCode.Value = saveDataTable.Rows[0]["LVTypeCode"].ToString().Trim() == null ? "" : saveDataTable.Rows[0]["LVTypeCode"].ToString().Trim();
                hidApplyType.Value = saveDataTable.Rows[0]["ApplyType"].ToString().Trim() == null ? "" : saveDataTable.Rows[0]["ApplyType"].ToString().Trim();
                txtBillNo.Text = saveDataTable.Rows[0]["BillNo"].ToString().Trim() == null ? "" : saveDataTable.Rows[0]["BillNo"].ToString().Trim();
                hidStatus.Value = saveDataTable.Rows[0]["Status"].ToString();
                string ProxyStatus = Convert.ToString(saveDataTable.Rows[0]["ProxyStatus"]);
                switch (saveDataTable.Rows[0]["Status"].ToString())
                {
                    //申請狀態
                    case "1":
                    case "2":
                    case "4":
                        this.btnSave.Enabled = false;
                        break;
                    default:
                        if (ProxyStatus.Equals("1"))
                        {
                            this.btnSave.Enabled = false;
                        }
                        break;
                }
            }
        }
        protected void Add()
        {

            if (this.hidSave.Value != "Save")
            {
                txtEmployeeNo.Text = "";
            }
            this.ProcessFlag.Value = "Add";
            this.TextBoxsReset("Add", false);

        }
        protected void SetTextBoxState(Control ctrl, bool bReadOnly)
        {
            if (ctrl is TextBox)
            {
                (ctrl as TextBox).BorderStyle = bReadOnly ? BorderStyle.None : BorderStyle.NotSet;
                (ctrl as TextBox).ReadOnly = bReadOnly;
            }
            else if (ctrl is HtmlInputText)
            {
                (ctrl as HtmlInputText).Disabled = !bReadOnly;
            }
            else if (ctrl is DropDownList)
            {
                (ctrl as DropDownList).Enabled = bReadOnly;
            }
        }
        private void TextBoxsReset(string buttonText, bool read)
        {
            txtEmployeeNo.BorderStyle = BorderStyle.None;
            txtStartDate.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            txtStartTime.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            txtEndDate.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            txtEndTime.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            txtReason.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            txtRemark.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            txtReason.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            txtProxy.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            txtRemark.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            txtProxyNotes.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            txtProxy.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
            this.SetTextBoxState(txtJoinDate, true);
            this.SetTextBoxState(txtManager, true);
            this.SetTextBoxState(txtLevelCode, true);
            this.SetTextBoxState(txtDPcode, true);
            this.SetTextBoxState(txtSex, true);
            txtComeYears.BorderStyle = BorderStyle.None;
            txtLVTotal.BorderStyle = BorderStyle.None;

            if (buttonText.ToLower() == "add")
            {
                txtBillNo.Text = Message.SystemAuto;
                txtStartDate.Text = "";
                txtStartTime.Text = "";
                txtEndDate.Text = "";
                txtEndTime.Text = "";
                txtReason.Text = "";
                txtProxy.Text = "";
                txtLVTotal.Text = "";
                txtRemark.Text = "";
                txtManager.Text = "";
                txtJoinDate.Text = "";
                txtLevelCode.Text = "";
                txtSex.Text = "";
                txtDPcode.Text = "";
                txtComeYears.Text = "";
                hidBillNo.Value = "";
                ddlLVTypeCode.SelectedIndex = -1;
                ddlApplyType.SelectedIndex = -1;
                hidLVTypeCode.Value = "";
                hidApplyType.Value = "";
            }
        }
        /// <summary>
        /// 判斷輸入值是否為空
        /// </summary>
        /// <param name="txtValue"></param>
        /// <param name="ReValue"></param>
        /// <returns></returns>
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
        /// 判斷請假時數是否正確（總時數不能小於最小時間且總時數除以最小時間餘數為0）
        /// </summary>
        /// <param name="LVType"></param>
        /// <param name="sTotal"></param>
        /// <returns></returns>
        public bool CheckLvDays(string LVType, string sTotal)
        {
            if (sTotal.Length == 0)
            {
                return false;
            }
            try
            {
                double MinHours = 0.5;
                double StandardHours = 0.5;
                double dTotal = Convert.ToDouble(sTotal);
                if (LVType.Equals("Y"))
                {
                    dTotal *= 8.0;
                }
                DataTable tempDataTable = leaveApply.getHours(LVType);
                if (tempDataTable.Rows.Count > 0)
                {
                    MinHours = Convert.ToDouble(tempDataTable.Rows[0]["MinHours"]);
                    StandardHours = Convert.ToDouble(tempDataTable.Rows[0]["StandardHours"]);
                }
                if (dTotal < MinHours)
                {
                    return false;
                }
                //if ((dTotal % StandardHours) != 0.0)
                //{
                //    return false;
                //}
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 檢查Notes格式
        /// </summary>
        /// <param name="txtValue"></param>
        /// <returns></returns>
        protected bool CheckNotesData(string txtValue)
        {
            bool bValue = true;
            string pattern = @"(^[A-Z0-9-.\s?]+)/[A-Z]{2,8}/(FOXCONN|CMINL@CMINL|CMINL)$";
            bValue = Regex.IsMatch(txtValue.ToUpper(), pattern);
            if (!bValue)
            {
                pattern = @"^([A-Z0-9]+[-|\.]?)+[A-Z0-9]@([A-Z0-9]+(-[A-Z0-9]+)?\.)+[A-Z]{2,}$";
                bValue = Regex.IsMatch(txtValue.ToUpper(), pattern);
            }
            if (!bValue)
            {
                WriteMessage(Message.NoteFormateNotRight);
            }
            return bValue;
        }
        /// <summary>
        /// 判斷請假時數是否超過管控上限
        /// </summary>
        /// <param name="ProcessFlag">新增修改標誌</param>
        /// <param name="workNo">工號</param>
        /// <param name="lvType">假別</param>
        /// <param name="strDate">請假日期</param>
        /// <param name="sTotal">總時數</param>
        /// <param name="BillNo">ID</param>
        /// <param name="ImportType"></param>
        /// <returns>請假時數是否超過管控上限</returns>
        private bool CheckLvtotal(string ProcessFlag, string workNo, string lvType, string strDate, string sTotal, string BillNo, string ImportType)
        {
            return leaveApply.CheckLvtotal(ProcessFlag, workNo, lvType, strDate, sTotal, BillNo, ImportType);
        }
        private bool CheckLeaveOverTime(string empNo, string startDate, string startTime, string endDate, string endTime)
        {
            return leaveApply.CheckLeaveOverTime(empNo, startDate, startTime, endDate, endTime);
        }
        private static string MoveSpecailChar(string str)
        {
            str = str.Trim().Replace("'", "");
            return str;
        }
        private double CheckLvtotal(string processFlag, string empNo, string startDate, string BillNo)
        {
            return leaveApply.CheckLvtotal(processFlag, empNo, startDate, BillNo);
        }
        private bool CheckLeaveOverTime(string WorkNo, string StartDate, string StartTime, string EndDate, string EndTime, string BillNo)
        {
            return leaveApply.CheckLeaveOverTime(WorkNo, StartDate, StartTime, EndDate, EndTime, BillNo);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (((((CheckData(txtEmployeeNo.Text, Message.EmpNoNotNull) && CheckData(this.ddlLVTypeCode.SelectedValue, Message.LeaveTypeNotNull)) && (CheckData(txtStartDate.Text, Message.StartDateNotNull) && CheckData(txtStartTime.Text, Message.ErrStartTimeNull))) && ((CheckData(txtEndDate.Text, Message.EndDateNotNull) && CheckData(txtEndTime.Text, Message.ErrEndDateNull)) && (CheckData(this.ddlApplyType.Text, Message.ErrApplyTypeNull) && CheckData(txtReason.Text, Message.ErrReasonNull)))) && (CheckData(txtProxyWorkNo.Text, Message.ProxyWorkNoNotNull) && CheckData(txtProxy.Text, Message.ProxyWorkNameNotNull))) && CheckData(txtLVTotal.Text, Message.DayNotNull))
                {
                    if (pCMLeaveApply.iSAllowPCM(ddlLVTypeCode.SelectedValue).Equals("N"))//是否允許個人申請
                    {
                        WriteMessage(Message.PersonLimitType);
                    }
                    else if (txtProxyWorkNo.Text.Trim().Equals(txtEmployeeNo.Text.Trim()))//代理人工號與申請人工號不能相同
                    {
                        WriteMessage(Message.ProxyApplyNoSame);
                    }
                    else if (this.ddlLVTypeCode.SelectedValue.Equals("x") && ((Convert.ToInt16(txtLVTotal.Value) / 8) <= 15))//請假類別爲大於15天事假時，判斷請假總時間
                    {
                        WriteMessage(Message.LeaveDayCountNotRight);
                    }
                    else
                    {
                        if (this.ddlLVTypeCode.SelectedValue.Equals("J")) //請婚假
                        {
                            if (txtEmployeeNo.Text.Trim().ToUpper().Equals(txtConsortWorkNo.Text.Trim().ToUpper()))//判斷申請人工號與配偶工號是否相同
                            {
                                WriteMessage(Message.ApplyConsortNotSame);
                                return;
                            }
                            if (!CheckData(ddlConsortInFoxonn.SelectedValue, Message.ConsortInNotNull) || (ddlConsortInFoxonn.SelectedValue.Equals("Y") && !(((CheckData(txtConsortDepCode.Text, Message.ConsortBgNotNull) && CheckData(txtConsortName.Text, Message.ConsortNameNotNull)) && (CheckData(txtConsortWorkNo.Text, Message.ConsortWorkNoNotNull))) && CheckData(ddlLevelCode.SelectedValue, Message.LevelNotNull))))
                            {
                                //配偶是否在集團為空或配偶在集團但配偶工號、配偶姓名 、資位、配偶所在事業群至少有一個為空
                                return;
                            }
                        }
                        //if (this.ddlLVTypeCode.SelectedValue.Equals("Y"))//不能申請年休假
                        //{
                        //    WriteMessage(Message.LeaveApplyError);
                        //}
                        else if ((txtProxyNotes.Text.Trim().Length <= 0) || CheckNotesData(txtProxyNotes.Text.Trim()))//代理人沒有Notes或Notes存在且格式正確
                        {
                            if (!CheckLvDays(this.ddlLVTypeCode.SelectedValue, txtLVTotal.Value.ToString()))
                            {
                                WriteMessage(Message.LeaveDayCountNotRight);
                            }
                            else
                            {
                                try
                                {
                                    if ((txtStartTime.Text.Length != 5) || (txtEndTime.Text.Length != 5))//開始時間或結束時間格式不正確
                                    {
                                        WriteMessage(Message.ErrStartTimeOrEndTime);
                                        return;
                                    }
                                    string nStartDate = string.Format(txtStartDate.Text + " " + txtStartTime.Text, "yyyy/MM/dd HH:mi");
                                    TimeSpan tsLVTotal = (TimeSpan)(Convert.ToDateTime(string.Format(txtEndDate.Text + " " + txtEndTime.Text, "yyyy/MM/dd HH:mi")) - Convert.ToDateTime(nStartDate));
                                    if (tsLVTotal.TotalHours <= 0.0)//結束日期小於開始日期
                                    {
                                        WriteMessage(Message.EndLaterThanStart);
                                        return;
                                    }
                                }
                                catch (Exception)
                                {
                                    WriteMessage(Message.TimeFormateErr);
                                    return;
                                }
                                if (CheckLvtotal(this.ProcessFlag.Value, txtEmployeeNo.Text.Trim(), this.ddlLVTypeCode.SelectedValue, txtStartDate.Text, txtLVTotal.Value.ToString(), hidBillNo.Value.Trim(), ""))
                                {
                                    if (this.ddlLVTypeCode.SelectedValue.Equals("U"))
                                    {
                                        WriteMessage(Message.TotalLeaveDayOver);
                                    }
                                    else
                                    {
                                        WriteMessage(Message.LeaveApplyTotalError);
                                    }
                                }
                                else
                                {
                                    DataRow row;
                                    string strStatus = "0";
                                    string BillNo = "";
                                    string strID = "";
                                    if (hidLeaveNoAudit.Value.Equals("Y"))
                                    {
                                        strStatus = "2";
                                    }
                                    else if (!txtEndDate.Text.Equals(txtStartDate.Text))//如果開始日期與結束日期不是同一天，判斷班別是否一致
                                    {
                                        string startShiftNo = "";
                                        string endShiftNo = "";
                                        startShiftNo = leaveApply.getLVTotal(txtEmployeeNo.Text.Trim(), txtStartDate.Text);
                                        endShiftNo = leaveApply.getLVTotal(txtEmployeeNo.Text.Trim(), txtEndDate.Text);
                                        if (!(endShiftNo.StartsWith("C") || !startShiftNo.StartsWith("C")))//結束班別不是夜班且開始班別是夜班
                                        {
                                            endShiftNo = leaveApply.getLVTotal(txtEmployeeNo.Text.Trim(), Convert.ToDateTime(txtEndDate.Text).AddDays(-1.0).ToString("yyyy/MM/dd"));
                                        }
                                        if (!(startShiftNo.Equals(endShiftNo) && (startShiftNo.Length != 0)))//開始班別為空或開始班別與結束班別不一致
                                        {
                                            WriteMessage(Message.ShiftNoNotSame);
                                            return;
                                        }
                                    }
                                    double MrelAdjust = 0.0;
                                    if (this.ddlLVTypeCode.SelectedValue.Equals("U"))//調休假獲取當月可調時數
                                    {
                                        MrelAdjust = CheckLvtotal(this.ProcessFlag.Value, txtEmployeeNo.Text.Trim(), txtStartDate.Text, hidBillNo.Value.Trim());
                                    }
                                    DataTable tempDataTable = pCMLeaveApply.getDataById(hidBillNo.Value.Trim());
                                    if (this.ProcessFlag.Value.Equals("Add"))
                                    {
                                        if (!CheckLeaveOverTime(txtEmployeeNo.Text, txtStartDate.Text, txtStartTime.Text, txtEndDate.Text, txtEndTime.Text))
                                        {
                                            WriteMessage(Message.LeaveDaySame);
                                            return;
                                        }
                                        string restchangehours = leaveApply.getRestChangeHours();
                                        if ((restchangehours != "N") && (this.ddlLVTypeCode.SelectedValue == "U"))//允許調休且請調休假
                                        {
                                            int lvHours = Convert.ToInt32(txtLVTotal.Value);
                                            int maxrestchangehours = Convert.ToInt32(restchangehours);
                                            if (lvHours > maxrestchangehours)//總時數超過調休時數
                                            {
                                                WriteMessage(Message.OverMostHours + ":" + restchangehours);
                                                return;
                                            }
                                        }
                                        row = tempDataTable.NewRow();
                                        row.BeginEdit();
                                        row["WorkNo"] = txtEmployeeNo.Text.ToUpper().Trim();
                                        row["LVTypeCode"] = this.ddlLVTypeCode.SelectedValue;
                                        row["StartDate"] = txtStartDate.Text;
                                        row["StartShiftNo"] = leaveApply.getLVTotal(txtEmployeeNo.Text.ToUpper().Trim(), txtStartDate.Text);
                                        row["StartTime"] = txtStartTime.Text;
                                        row["EndDate"] = txtEndDate.Text;
                                        row["EndShiftNo"] = leaveApply.getLVTotal(txtEmployeeNo.Text.ToUpper().Trim(), txtEndDate.Text);
                                        row["EndTime"] = txtEndTime.Text;
                                        row["LVTotal"] = txtLVTotal.Value.ToString();
                                        row["Reason"] = MoveSpecailChar(txtReason.Text);
                                        row["Proxy"] = MoveSpecailChar(txtProxy.Text);
                                        row["ProxyWorkNo"] = txtProxyWorkNo.Text.ToUpper().Trim();
                                        row["ProxyNotes"] = MoveSpecailChar(txtProxyNotes.Text).Replace(",", "");
                                        row["ProxyFlag"] = hidProxyFlag.Value;
                                        row["Remark"] = MoveSpecailChar(txtRemark.Text);
                                        row["ApplyType"] = this.ddlApplyType.SelectedValue;
                                        row["Status"] = strStatus;
                                        row["UPDATE_USER"] = CurrentUserInfo.Personcode.ToUpper();
                                        row["emergencycontactperson"] = txtEmergencyContactPerson.Text;
                                        row["emergencytelephone"] = txtEmergencyTelephone.Text;
                                        row["ProxyStatus"]="1";//個人中心請假申請代理人簽核狀態為簽核中
                                        if (this.ddlLVTypeCode.SelectedValue.Equals("J"))
                                        {
                                            row["WifeISFoxconn"] = ddlConsortInFoxonn.SelectedValue;
                                            row["WifeBG"] = txtConsortDepCode.Text;
                                            row["WifeWorkNo"] = txtConsortWorkNo.Text;
                                            row["WifeName"] = txtConsortName.Text;
                                            row["WifeLevelName"] = ddlLevelCode.SelectedValue;
                                        }
                                        row.EndEdit();
                                        tempDataTable.Rows.Add(row);
                                        tempDataTable.AcceptChanges();
                                        strID = leaveApply.SaveData(this.ProcessFlag.Value, tempDataTable, logmodel);
                                        leaveApply.getLeaveDetail(strID);
                                        if (this.ddlWindow.SelectedValue.Length > 0)
                                        {
                                            string remindContent = txtDPcode.Text + "：" + txtEmployeeNo.Text + txtName.Text + Message.HaveApplyed + this.ddlLVTypeCode.SelectedItem.Text;
                                            pCMLeaveApply.ExcuteSQL(this.ddlWindow.SelectedValue, remindContent, logmodel);
                                        }
                                    }
                                    else if (this.ProcessFlag.Value.Equals("Modify"))
                                    {
                                        if (tempDataTable.Rows.Count == 0) //無數據
                                        {
                                            WriteMessage(Message.AtLastOneChoose);
                                            return;
                                        }
                                        if ((hidStatus.Value != "0") && (hidStatus.Value != "3")) //只有未核准和拒簽的才能修改
                                        {
                                            WriteMessage(Message.NoApprovalOrRefuseNoUpdate);
                                            return;
                                        }
                                        //請假日期是否重複
                                        if (!CheckLeaveOverTime(txtEmployeeNo.Text, txtStartDate.Text, txtStartTime.Text, txtEndDate.Text, txtEndTime.Text, hidBillNo.Value))
                                        {
                                            WriteMessage(Message.LeaveDaySame);
                                            return;
                                        }
                                        row = tempDataTable.Rows[0];
                                        row.BeginEdit();
                                        row["ID"] = hidBillNo.Value;
                                        row["WorkNo"] = txtEmployeeNo.Text.ToUpper().Trim();
                                        row["LVTypeCode"] = ddlLVTypeCode.SelectedValue;
                                        row["StartDate"] = txtStartDate.Text;
                                        row["StartShiftNo"] = leaveApply.getLVTotal(txtEmployeeNo.Text.ToUpper().Trim(), txtStartDate.Text);
                                        row["StartTime"] = txtStartTime.Text;
                                        row["EndDate"] = txtEndDate.Text;
                                        row["EndShiftNo"] = leaveApply.getLVTotal(txtEmployeeNo.Text.ToUpper().Trim(), txtEndDate.Text);
                                        row["EndTime"] = txtEndTime.Text;
                                        row["LVTotal"] = txtLVTotal.Value.ToString();
                                        row["Reason"] = MoveSpecailChar(txtReason.Text);
                                        row["Proxy"] = MoveSpecailChar(txtProxy.Text);
                                        row["ProxyWorkNo"] = txtProxyWorkNo.Text.ToUpper().Trim();
                                        row["ProxyNotes"] = MoveSpecailChar(txtProxyNotes.Text).Replace(",", "");
                                        row["ProxyFlag"] = hidProxyFlag.Value;
                                        row["Remark"] = MoveSpecailChar(txtRemark.Text);
                                        row["ApplyType"] = this.ddlApplyType.SelectedValue;
                                        row["UPDATE_USER"] = CurrentUserInfo.Personcode.ToUpper();
                                        row["emergencycontactperson"] = txtEmergencyContactPerson.Text;
                                        row["emergencytelephone"] = txtEmergencyTelephone.Text;
                                        if (this.ddlLVTypeCode.SelectedValue.Equals("J"))
                                        {
                                            row["WifeISFoxconn"] = ddlConsortInFoxonn.SelectedValue;
                                            row["WifeBG"] = txtConsortDepCode.Text;
                                            row["WifeWorkNo"] = txtConsortWorkNo.Text;
                                            row["WifeName"] = txtConsortName.Text;
                                            row["WifeLevelName"] = ddlLevelCode.SelectedValue;
                                        }
                                        if ((this.hidStatus.Value == "3") || (this.hidStatus.Value == "0"))
                                        {
                                            row["Status"] = strStatus;
                                            if (Convert.ToString(row["ProxyStatus"]).Equals("3"))
                                            {
                                                row["ProxyStatus"] = strStatus;
                                            }
                                        }
                                       // if ((this.hidStatus.Value == "3") || (this.hidStatus.Value == "0"))
                                       // {
                                       //     row["Status"] = "0";
                                       //  //   if (Convert.ToString(row["ProxyStatus"]).Equals("3"))
                                       ////     {
                                       //         row["ProxyStatus"] = "0";
                                       //   //  }
                                       // }
                                        row.EndEdit();
                                        tempDataTable.AcceptChanges();
                                        leaveApply.SaveData(this.ProcessFlag.Value, tempDataTable, logmodel);
                                        strID = hidBillNo.Value;
                                        leaveApply.getLeaveDetail(strID);
                                    }
                                    if (this.ddlLVTypeCode.SelectedValue.Equals("U"))
                                    {
                                        leaveApply.CountCanAdjlasthy(txtEmployeeNo.Text.ToUpper().Trim());
                                        if (MrelAdjust < Convert.ToDouble(txtLVTotal.Value.ToString()))
                                        {
                                            Response.Write(string.Concat(new object[] { "<script type='text/javascript'>alert('", Message.ApplySuccessClickAudit, ";", Message.Tip, MrelAdjust, "');window.parent.document.all.btnQuery.click();</script>" }));
                                        }
                                        else
                                        {
                                            Response.Write("<script type='text/javascript'>alert('" + Message.ApplySuccessClickAudit + "');window.parent.document.all.btnQuery.click();</script>");
                                        }
                                    }
                                    //else if (this.ddlLVTypeCode.SelectedValue.Equals("U"))
                                    //{
                                       
                                    //}
                                    else
                                    {
                                        Response.Write("<script type='text/javascript'>alert('" + Message.ApplySuccessClickAudit + "');window.parent.document.all.btnQuery.click();</script>");
                                    }
                                    //if (hidSendNotes.Value.Equals("Y") && strStatus.Equals("0"))
                                    //{
                                    //    if (this.ProcessFlag.Value.Equals("Add"))
                                    //    {
                                    //        DataTable dt = new DataTable();
                                    //        dt = pCMLeaveApply.getVDataByWorkNo(txtEmployeeNo.Text.ToUpper().Trim());
                                    //        if (tempDataTable.Rows.Count > 0)
                                    //        {
                                    //            foreach (DataRow newRow in tempDataTable.Rows)
                                    //            {
                                    //                hidManagerCode.Value = newRow["ManagerCode"].ToString();
                                    //                hidLevelCode.Value = newRow["LevelCode"].ToString();

                                    //            }
                                    //        }
                                    //    }
                                    //    string LVTypeCode = this.ddlLVTypeCode.SelectedValue;
                                    //    string ManagerCode = this.hidManagerCode.Value;
                                    //    string LevelCode = this.hidLevelCode.Value;
                                    //    double LVTotal = Convert.ToDouble(txtLVTotal.Value.ToString());
                                    //    string ProxyNotes = txtProxyNotes.Text;
                                    //    string BillTypeCode = "D002";
                                    //    string OrgCode = hidDCode.Value;
                                    //    string appValue = "";
                                    //    string capValue = "";
                                    //    string cmpType = "";
                                    //    double minDays = 0.0;
                                    //    double maxDays = 0.0;
                                    //    string[] temVal = null;
                                    //    bool isFlag = false;
                                    //    DataTable tempDataTableNew = null;
                                    //    if (leaveApply.getBillTypeCode(LVTypeCode).Trim() == "")
                                    //    {
                                    //        tempDataTableNew = pCMLeaveApply.getApptypetoBillConfig(LVTypeCode);
                                    //        if (tempDataTableNew.Rows.Count > 0)
                                    //        {
                                    //            foreach (DataRow newRow in tempDataTableNew.Rows)
                                    //            {
                                    //                appValue = newRow["AppValue"].ToString();
                                    //                cmpType = newRow["CmpType"].ToString();
                                    //                capValue = newRow["CapValue"].ToString();
                                    //                minDays = Convert.ToDouble(newRow["MinDays"].ToString().Trim());
                                    //                maxDays = Convert.ToDouble(newRow["MaxDays"].ToString().Trim());
                                    //                if (cmpType.Trim() == "hrmmgr")
                                    //                {
                                    //                    if (((LVTotal >= (minDays * 8.0)) && (LVTotal <= (maxDays * 8.0))) && (capValue.Trim() != ""))
                                    //                    {
                                    //                        temVal = capValue.Trim().Replace("，", ",").Split(new char[] { ',' });
                                    //                        Array.Sort<string>(temVal);
                                    //                        if (Array.BinarySearch<string>(temVal, ManagerCode) > 0)
                                    //                        {
                                    //                            //   BillTypeCode = newRow["billType"].ToString().Trim();
                                    //                            isFlag = true;
                                    //                        }
                                    //                    }
                                    //                }
                                    //                else if (cmpType.Trim() == "hrmlevel")
                                    //                {
                                    //                    if (((LVTotal >= (minDays * 8.0)) && (LVTotal <= (maxDays * 8.0))) && (capValue.Trim() != ""))
                                    //                    {
                                    //                        temVal = capValue.Trim().Replace("，", ",").Split(new char[] { ',' });
                                    //                        Array.Sort<string>(temVal);
                                    //                        if (Array.BinarySearch<string>(temVal, LevelCode) > 0)
                                    //                        {
                                    //                            //   BillTypeCode = newRow["billType"].ToString().Trim();
                                    //                            isFlag = true;
                                    //                        }
                                    //                    }
                                    //                }
                                    //                else if ((cmpType.Trim() == "") && ((LVTotal >= (minDays * 8.0)) && (LVTotal <= (maxDays * 8.0))))
                                    //                {
                                    //                    // BillTypeCode = newRow["billType"].ToString().Trim();
                                    //                    isFlag = true;
                                    //                }
                                    //                if (isFlag)
                                    //                {
                                    //                    break;
                                    //                }
                                    //            }
                                    //        }
                                    //    }
                                    //    string startDate = txtStartDate.Text + " " + txtStartTime.Text;
                                    //    string endDate = txtEndDate.Text + " " + txtEndTime.Text;
                                    //    string lvTypeCode = ddlLVTypeCode.SelectedValue;
                                    //    string AuditOrgCode = leaveApply.getWorkFlowOrgCode(OrgCode, BillTypeCode, lvTypeCode, startDate, endDate, txtEmployeeNo.Text);
                                    //    if (AuditOrgCode.Length > 0)
                                    //    {
                                    //        if (ProxyNotes.Length < 5)
                                    //        {
                                    //            string sFlow_LevelRemark = Message.flow_levelremark;
                                    //            leaveApply.SaveAuditData(sFlow_LevelRemark, strID, "KQL", AuditOrgCode, BillTypeCode, CurrentUserInfo.Personcode, "", txtEmployeeNo.Text, startDate, endDate, lvTypeCode, logmodel);
                                    //        }
                                    //        else
                                    //        {
                                    //            leaveApply.changeProxyStatus(strID, CurrentUserInfo.Personcode, logmodel);
                                    //        }
                                    //    }
                                    //    else
                                    //    {
                                    //        Response.Write("<script type='text/javascript'>alert('" + Message.SaveSuccessNoAudit + "');window.parent.document.all.btnQuery.click();</script>");
                                    //        return;
                                    //    }
                                    //}
                                    //if (hidSendNotes.Value.Equals("Y"))
                                    //{
                                    //    if (this.ddlLVTypeCode.SelectedValue.Equals("U"))
                                    //    {
                                    //        if (MrelAdjust < Convert.ToDouble(txtLVTotal.Value.ToString()))
                                    //        {
                                    //            Response.Write(string.Concat(new object[] { "<script type='text/javascript'>alert('", Message.SaveSuccessAndAudit, ";", Message.Tip, MrelAdjust, "');window.parent.document.all.btnQuery.click();</script>" }));
                                    //        }
                                    //        else
                                    //        {
                                    //            Response.Write("<script type='text/javascript'>alert('" + Message.SaveSuccessAndAudit + "');window.parent.document.all.btnQuery.click();</script>");
                                    //        }
                                    //    }
                                    //    else
                                    //    {
                                    //        Response.Write("<script type='text/javascript'>alert('" + Message.SaveSuccessAndAudit + "');window.parent.document.all.btnQuery.click();</script>");
                                    //    }
                                    //}
                                  
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteMessage((ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
            }
        }


        /// <summary>
        /// AJAX验证角色代码是否存在
        /// </summary>
        protected override void AjaxProcess()
        {
            if (Request.Form["workno"] != null)
            {
                string processFlag = Request.Form["flag"];
                string strinfo = "";
                switch (processFlag)
                {
                    case "LVTypeCode":
                        {

                            strinfo = "N";
                            if (leaveApply.checkRefuseApply(Request.Form["workno"], Request.Form["typecode"]) == "Y")
                            {
                                strinfo = "Y";
                            }
                            break;
                        }

                    case "LVTotal":
                        {
                            if (Request.Form["workno"] != null)
                            {
                                strinfo = leaveApply.getLVTotal(Request.Form["workno"], Request.Form["startDate"], Request.Form["endDate"], Request.Form["typecode"]);
                            }
                            break;
                        }
                    case "ShiftDeac":
                        {
                            if (Request.Form["workno"] != null)
                            {
                                strinfo = leaveApply.getLVTotal(Request.Form["workno"], Request.Form["startDate"]);
                            }
                            break;
                        }
                    case "proxy":
                        {
                            if (Request.Form["workno"] != null)
                            {
                                List<GetEmpViewModel> list = GetEmpList(Request.Form["workno"].ToString());
                                if (list != null && list.Count == 1)
                                {
                                    strinfo = JsSerializer.Serialize(list[0]);
                                }
                                else
                                {
                                    strinfo = JsSerializer.Serialize(new GetEmpViewModel());
                                }
                            }
                            break;
                        }
                }
                Response.Clear();
                Response.Write(strinfo);
                Response.End();
            }

        }
        /// <summary>
        /// 獲取代理人的基本信息
        /// </summary>
        /// <param name="proxyWorkNo">代理人工號</param>
        /// <returns>代理人基本信息</returns>
        private List<GetEmpViewModel> GetEmpList(string proxyWorkNo)
        {
            List<GetEmpViewModel> list = new List<GetEmpViewModel>();
            GetEmpViewBll getEmpViewBll = new GetEmpViewBll();
            list = getEmpViewBll.GetEmpList(proxyWorkNo);
            return list;
        }
    }
}
