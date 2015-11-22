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
using System.Text;
using Resources;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.BLL.Hr.PCM;
using GDSBG.MiABU.Attendance.Model.Hr.PCM;

namespace GDSBG.MiABU.Attendance.Web.Hr.KQM.KaoQinData
{
    public partial class KQMLeaveApplyEditForm : BasePage
    {
        KQMLeaveApplyForm_ZBLHBll leaveApply = new KQMLeaveApplyForm_ZBLHBll();
        bool IsPrivileged;
        string moduleCode = "";
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;

        static SynclogModel logmodel = new SynclogModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            SetCalendar(txtStartDate, txtEndDate);
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
                ClientMessage.Add("EmpBasicInfoNotExist", Message.EmpBasicInfoNotExist);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
            if (!IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;
                txtProxyWorkNo.Attributes.Add("onblur", "GetDeputy();");

                moduleCode = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                hidMarryFlag.Value = leaveApply.getMarryFlag() == "Y" ? "Y" : "N";
                IsPrivileged = Request.QueryString["privileged"].ToString() == "true" ? true : false;
                initDropDownList();
                string BillNo = (Request.QueryString["BillNo"] == null) ? "" : base.Request.QueryString["BillNo"].ToString();
                string ProcessFlag = (Request.QueryString["ProcessFlag"] == null) ? "" : base.Request.QueryString["ProcessFlag"].ToString();
                this.hidBillNo.Value = BillNo;
                this.ProcessFlag.Value = ProcessFlag;
                if (ProcessFlag == "Add")
                {
                    this.Add();
                }
                else if (BillNo.Length > 0)
                {
                    Modify(BillNo);
                }
                else
                {
                    Response.Write("<script type='text/javascript'>alert(\"" + Message.AtLastOneChoose + "\");window.parent.document.all.topTable.style.display='';window.parent.document.all.divEdit.style.display='none';</script>");
                }
                txtStartDate.Attributes.Add("onpropertychange", "javascript:GetLVTotal();");
                txtStartDate.Attributes.Add("onafterpaste", "javascript:GetLVTotal();");
                txtEndDate.Attributes.Add("onpropertychange", "javascript:GetLVTotal();");
                txtEndDate.Attributes.Add("onafterpaste", "javascript:GetLVTotal();");
                ddlLVTypeCode.Attributes.Add("onchange", "onChangeLVTypeCode(this.options[this.selectedIndex].value)");
                ddlConsortInFoxonn.Attributes.Add("onchange", "ConsortIsMandatory(this.options[this.selectedIndex].value)");

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
        protected void Add()
        {
            this.ProcessFlag.Value = "Add";
            if (this.hidSave.Value != "Save")
            {
                this.txtEmployeeNo.Text = "";
            }
            TextBoxsReset("Add", false);
        }
        protected void Modify(string BillNo)
        {
            this.ProcessFlag.Value = "Modify";
            this.DataUIBind(BillNo);
            this.TextBoxsReset("Modify", false);
        }
        private void DataUIBind(string BillNo)
        {
            string ProxyStatus;
            DataTable tempDataTable = leaveApply.getDataByBillNo(BillNo);
            if (tempDataTable.Rows.Count > 0)
            {
                EmpQuery(tempDataTable.Rows[0]["WorkNo"].ToString());
                txtStartDate.Text = (tempDataTable.Rows[0]["StartDate"].ToString().Trim() == null) ? "" : string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(tempDataTable.Rows[0]["StartDate"].ToString().Trim()));
                txtEndDate.Text = (tempDataTable.Rows[0]["EndDate"].ToString().Trim() == null) ? "" : string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(tempDataTable.Rows[0]["EndDate"].ToString().Trim()));
                txtStartTime.Value = (tempDataTable.Rows[0]["StartTime"].ToString().Trim() == null) ? DateTime.Parse("0001/01/01 00:00") : Convert.ToDateTime(tempDataTable.Rows[0]["StartTime"].ToString().Trim());
                txtEndTime.Value = (tempDataTable.Rows[0]["EndTime"].ToString().Trim() == null) ? DateTime.Parse("0001/01/01 00:00") : Convert.ToDateTime(tempDataTable.Rows[0]["EndTime"].ToString().Trim());
                txtEmergencyContactPerson.Text = (tempDataTable.Rows[0]["EMERGENCYCONTACTPERSON"].ToString().Trim() == null) ? "" : tempDataTable.Rows[0]["EMERGENCYCONTACTPERSON"].ToString().Trim();

                txtEmergencyTelephone.Text = (tempDataTable.Rows[0]["EMERGENCYTELEPHONE"].ToString().Trim() == null) ? "" : tempDataTable.Rows[0]["EMERGENCYTELEPHONE"].ToString().Trim();

                string LVTotal = (tempDataTable.Rows[0]["LVTotal"].ToString().Trim() == null) ? "" : tempDataTable.Rows[0]["LVTotal"].ToString().Trim();
                txtLVTotal.Text = LVTotal;
                if (LVTotal.Length > 0)
                {
                    txtLVTotalDays.Text = Convert.ToString((double)(Convert.ToDouble(LVTotal) / 8.0));
                }
                txtReason.Text = (tempDataTable.Rows[0]["Reason"].ToString().Trim() == null) ? "" : tempDataTable.Rows[0]["Reason"].ToString().Trim();
                txtProxyWorkNo.Text = (tempDataTable.Rows[0]["proxyworkno"].ToString().Trim() == null) ? "" : tempDataTable.Rows[0]["proxyworkno"].ToString().Trim();
                txtProxyName.Text = (tempDataTable.Rows[0]["Proxy"].ToString().Trim() == null) ? "" : tempDataTable.Rows[0]["Proxy"].ToString().Trim();
                hidProxyName.Value = txtProxyName.Text;
                txtProxyNotes.Text = (tempDataTable.Rows[0]["proxynotes"].ToString().Trim() == null) ? "" : tempDataTable.Rows[0]["proxynotes"].ToString().Trim();
                hidProxyNotes.Value = txtProxyNotes.Text;
                txtRemark.Text = (tempDataTable.Rows[0]["Remark"].ToString().Trim() == null) ? "" : tempDataTable.Rows[0]["Remark"].ToString().Trim();
                txtConsortDepCode.Text = tempDataTable.Rows[0]["WifeBG"].ToString();
                txtConsortName.Text = tempDataTable.Rows[0]["WifeName"].ToString();
                txtConsortWorkNo.Text = tempDataTable.Rows[0]["WifeWorkNo"].ToString();
                ddlConsortInFoxonn.SelectedIndex = ddlConsortInFoxonn.Items.IndexOf(ddlConsortInFoxonn.Items.FindByValue(tempDataTable.Rows[0]["WifeISFoxconn"].ToString()));
                ddlLevelCode.SelectedIndex = ddlLevelCode.Items.IndexOf(ddlLevelCode.Items.FindByValue(tempDataTable.Rows[0]["WifeLevelName"].ToString()));
                ddlLVTypeCode.SelectedIndex = ddlLVTypeCode.Items.IndexOf(ddlLVTypeCode.Items.FindByValue(tempDataTable.Rows[0]["LVTypeCode"].ToString()));
                ddlApplyType.SelectedIndex = ddlApplyType.Items.IndexOf(ddlApplyType.Items.FindByValue(tempDataTable.Rows[0]["ApplyType"].ToString()));
                hidLVTypeCode.Value = (tempDataTable.Rows[0]["LVTypeCode"].ToString().Trim() == null) ? "" : tempDataTable.Rows[0]["LVTypeCode"].ToString().Trim();
                hidApplyType.Value = (tempDataTable.Rows[0]["ApplyType"].ToString().Trim() == null) ? "" : tempDataTable.Rows[0]["ApplyType"].ToString().Trim();
                txtBillNo.Text = (tempDataTable.Rows[0]["BillNo"].ToString().Trim() == null) ? "" : tempDataTable.Rows[0]["BillNo"].ToString().Trim();
                hidStatus.Value = tempDataTable.Rows[0]["Status"].ToString();
                ProxyStatus = Convert.ToString(tempDataTable.Rows[0]["ProxyStatus"]);
                string status = tempDataTable.Rows[0]["Status"].ToString();
                if (status == null)
                {
                    goto Label_088A;
                }
                if (!(status == "1") && !(status == "2"))
                {
                    if (status == "4")
                    {
                        if (Convert.ToString(CurrentUserInfo.DepLevel).Equals("0"))
                        {
                            this.btnSave.Enabled = true;
                        }
                        else
                        {
                            this.btnSave.Enabled = false;
                        }
                        return;
                    }
                    goto Label_088A;
                }
                this.btnSave.Enabled = false;
            }
            return;
        Label_088A:
            if (ProxyStatus.Equals("1"))
            {
                btnSave.Enabled = false;
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
            txtProxyWorkNo.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            // txtProxyName.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            //txtProxyNotes.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            txtRemark.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            txtReason.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            //    txtProxyWorkNo.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            txtRemark.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            SetTextBoxState(txtJoinDate, true);
            SetTextBoxState(txtManager, true);
            //  SetTextBoxState(txtProxyName, true);
            //  SetTextBoxState(txtProxyNotes, true);
            SetTextBoxState(txtLevelCode, true);
            SetTextBoxState(txtDPcode, true);
            SetTextBoxState(txtSex, true);
            this.txtComeYears.BorderStyle = BorderStyle.None;
            txtLVTotal.BorderStyle = BorderStyle.None;
            txtLVTotalDays.BorderStyle = BorderStyle.None;
            if (buttonText.ToLower() == "add")
            {
                txtBillNo.Text = Message.SystemAuto;
                txtEmployeeNo.BorderStyle = BorderStyle.NotSet;
                txtStartDate.Text = "";
                txtStartTime.Text = "";
                txtEndDate.Text = "";
                txtEndTime.Text = "";
                txtReason.Text = "";
                txtProxyWorkNo.Text = "";
                txtProxyName.Text = "";
                txtProxyNotes.Text = "";
                txtEmergencyContactPerson.Text = "";
                txtEmergencyTelephone.Text = "";
                txtLVTotal.Text = "";
                txtLVTotalDays.Text = "";
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

        private bool CheckData(string txtValue, string ReValue)
        {
            if (string.IsNullOrEmpty(txtValue))
            {
                WriteMessage(ReValue);
                return false;
            }
            return true;
        }

        private void WriteMessage(string alert)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "addOrUpDateLeaveApply" + alert, "alert('" + alert + "')", true);
        }

        private void SetTextBoxState(Control ctrl, bool bReadOnly)
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
        private void initDropDownList()
        {
            DataTable dtLeaveType = leaveApply.getLeaveType();
            ddlLVTypeCode.DataSource = dtLeaveType;
            ddlLVTypeCode.DataTextField = "LVTypeName";
            ddlLVTypeCode.DataValueField = "LVTypeCode";
            ddlLVTypeCode.DataBind();
            ddlLVTypeCode.Items.Insert(0, new ListItem("", ""));


            DataTable dtLevelCode = leaveApply.getLevelCode();
            ddlLevelCode.DataSource = dtLevelCode;
            ddlLevelCode.DataTextField = "LevelName";
            ddlLevelCode.DataValueField = "LevelCode";
            ddlLevelCode.DataBind();
            ddlLevelCode.Items.Insert(0, new ListItem("", ""));

            DataTable dtApplyType = leaveApply.getApplyType();
            ddlApplyType.DataSource = dtApplyType;
            ddlApplyType.DataTextField = "DataValue";
            ddlApplyType.DataValueField = "DataCode";
            ddlApplyType.DataBind();
            ddlApplyType.Items.Insert(0, new ListItem("", ""));
        }

        protected void txtEmployeeNo_TextChanged(object sender, EventArgs e)
        {

            if (this.txtEmployeeNo.Text.Trim().Length > 0)
            {
                this.EmpQuery(this.txtEmployeeNo.Text.Trim());
            }

        }

        private void EmpQuery(string employeeNo)
        {
            DataTable dtEmpInfo = leaveApply.getEmpInfo(IsPrivileged, SqlDep, employeeNo.ToUpper());
            string marrystate = null;
            if (dtEmpInfo.Rows.Count > 0)
            {
                leaveApply.CountCanAdjlasthy(employeeNo.ToUpper());
                foreach (DataRow newRow in dtEmpInfo.Rows)
                {
                    this.txtEmployeeNo.Text = newRow["WORKNO"].ToString();
                    this.txtName.Text = newRow["LOCALNAME"].ToString();
                    this.txtDPcode.Text = newRow["DName"].ToString();
                    this.txtSex.Text = newRow["SEX"].ToString();
                    this.txtJoinDate.Text = (newRow["JoinDate"].ToString().Trim() == "") ? "" : string.Format("{0:yyyy/MM/dd}", newRow["JoinDate"]);
                    this.txtManager.Text = newRow["managername"].ToString();
                    this.txtLevelCode.Text = newRow["levelname"].ToString();
                    this.txtComeYears.Text = newRow["comeyears"].ToString();
                    marrystate = newRow["marrystate"].ToString();
                    if (this.ProcessFlag.Value == "Add")
                    {
                        this.txtStartDate.Text = "";
                        this.txtStartTime.Text = "";
                        this.txtEndDate.Text = "";
                        this.txtEndTime.Text = "";
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
                //    txtProxyWorkNo.Text = "";
                //   txtProxyName.Text = "";
                //      txtProxyNotes.Text = "";
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
            bool marryFlag = false;
            bool noYearHolidayFlag = false;
            if ((leaveApply.getMarryFlag() == "Y") && (marrystate == "Y"))
            {
                marryFlag = true;

            }
            if (leaveApply.getNoYearHoliday() == "Y" && !string.IsNullOrEmpty(leaveApply.getWorkNo(employeeNo)))
            {
                noYearHolidayFlag = true;
            }
            ddlLVTypeCode.Items.Clear();
            DataTable dt = leaveApply.getLVType(marryFlag, noYearHolidayFlag);
            this.ddlLVTypeCode.DataSource = dt;
            this.ddlLVTypeCode.DataTextField = "LVTypeName";
            this.ddlLVTypeCode.DataValueField = "LVTypeCode";
            this.ddlLVTypeCode.DataBind();
            this.ddlLVTypeCode.Items.Insert(0, new ListItem("", ""));
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
            for (int i = 0; i < ddlLVTypeCode.Items.Count; i++)
            {
                ddlLVTypeCode.Items[i].Selected = false;
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
                if ((dTotal % StandardHours) != 0.0)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private bool CheckLvtotal(string ProcessFlag, string workNo, string lvType, string strDate, string sTotal, string BillNo, string ImportType)
        {
            return leaveApply.CheckLvtotal(ProcessFlag, workNo, lvType, strDate, sTotal, BillNo, ImportType);
        }
        private static string MoveSpecailChar(string str)
        {
            str = str.Trim().Replace("'", "");
            return str;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strStatus = "0";
            try
            {
                if ((((CheckData(txtEmployeeNo.Text, Message.ErrorWorkNoNull) && (CheckData(ddlLVTypeCode.SelectedValue, Message.LeaveTypeNotNull)) && (CheckData(txtStartDate.Text, Message.StartDateNotNull) && CheckData(txtStartTime.Text, Message.ErrStartDateNull))) && ((CheckData(txtEndDate.Text, Message.EndDateNotNull) && CheckData(txtEndTime.Text, Message.ErrEndDateNull)) && (CheckData(ddlApplyType.Text, Message.ErrApplyTypeNull) && CheckData(txtReason.Text, Message.ErrReasonNull)))) && CheckData(txtLVTotal.Text, Message.DayNotNull)))
                {
                    if (txtProxyWorkNo.Text.Trim().Length > 0)
                    {
                        if (!leaveApply.IsProxyExists(txtProxyWorkNo.Text.Trim()))
                        {
                            WriteMessage(Message.ProxyNotExists);
                            return;
                        }
                    }

                    string strAllowDepLevel = leaveApply.getAllowDepLevel(ddlLVTypeCode.SelectedValue);
                    if (!string.IsNullOrEmpty(strAllowDepLevel) && !strAllowDepLevel.Equals(Convert.ToString(CurrentUserInfo.DepLevel)))
                    {
                        WriteMessage(Message.NoPrivileged + ddlLVTypeCode.SelectedItem.Text);
                        return;
                    }
                    else
                    {
                        string empLeaveDate = leaveApply.getLeaveDate(txtEmployeeNo.Text.Trim());
                        if (!string.IsNullOrEmpty(empLeaveDate) && (string.Compare(Convert.ToDateTime(txtEndDate.Text).ToString("yyyy-MM-dd"), empLeaveDate) > 0))
                        {
                            WriteMessage(Message.LeaveDateAndOutDate);
                            return;
                        }
                        else if (ddlLVTypeCode.SelectedValue.Equals("x") && ((Convert.ToInt16(txtLVTotal.Value) / 8) <= 15))
                        {
                            WriteMessage(Message.LeaveDayCountNotRight);
                            return;
                        }
                        else
                        {
                            if (this.ddlLVTypeCode.SelectedValue.Equals("J"))
                            {
                                if (txtEmployeeNo.Text.Trim().ToUpper().Equals(txtConsortWorkNo.Text.Trim().ToUpper()))
                                {
                                    WriteMessage(Message.ConsortEmpNoErr);
                                    return;
                                }
                                if (CheckData(ddlConsortInFoxonn.SelectedValue, Message.ConsortInNotNull) || (ddlConsortInFoxonn.SelectedValue.Equals("Y") && !(((CheckData(txtConsortDepCode.Text, Message.ConsortBgNotNull) && CheckData(txtConsortName.Text, Message.ConsortNameNotNull)) && (CheckData(txtConsortWorkNo.Text, Message.ConsortWorkNoNotNull))) && CheckData(ddlLevelCode.SelectedValue, Message.ConsortLevelCodeNotNull))))
                                {
                                    return;
                                }
                            }
                            else
                            {
                                if (!CheckLvDays(this.ddlLVTypeCode.SelectedValue, txtLVTotal.Value.ToString()))
                                {
                                    WriteMessage(Message.LeaveDayCountNotRight);
                                }
                                else
                                {
                                    string ImportType = "";
                                    if (this.ProcessFlag.Value.Equals("Modify") && this.ddlLVTypeCode.SelectedValue.Equals("U"))
                                    {
                                        if (!Convert.ToString(CurrentUserInfo.RoleCode).Equals("internal"))
                                        {
                                            if (leaveApply.getAuthorizedFunctionList(CurrentUserInfo.RoleCode, moduleCode).IndexOfValue("ImportU") >= 0)
                                            {
                                                ImportType = "U";
                                            }
                                        }
                                        else
                                        {
                                            ImportType = "U";
                                        }
                                    }
                                    if (CheckLvtotal(this.ProcessFlag.Value, txtEmployeeNo.Text.Trim(), ddlLVTypeCode.SelectedValue, txtStartDate.Text, txtLVTotal.Value.ToString(), hidBillNo.Value.Trim(), ImportType))
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
                                        try
                                        {
                                            if ((txtStartTime.Text.Length != 5) || (txtEndTime.Text.Length != 5))
                                            {
                                                WriteMessage(Message.EndDateLaterThanStartDate);
                                                return;
                                            }
                                            string nStartDate = string.Format(txtStartDate.Text + " " + txtStartTime.Text, "yyyy/MM/dd HH:mi");
                                            TimeSpan tsLVTotal = (TimeSpan)(Convert.ToDateTime(string.Format(txtEndDate.Text + " " + txtEndTime.Text, "yyyy/MM/dd HH:mi")) - Convert.ToDateTime(nStartDate));
                                            if (tsLVTotal.TotalHours <= 0.0)
                                            {
                                                WriteMessage(Message.EndDateLaterThanStartDate);
                                                return;
                                            }
                                        }
                                        catch (Exception)
                                        {
                                            WriteMessage(Message.TimeFormateErr);
                                            return;
                                        }
                                        if (leaveApply.getParaValue().Equals("Y"))
                                        {
                                            strStatus = "2";
                                        }
                                        else if (!txtEndDate.Text.Equals(txtStartDate.Text))
                                        {
                                            string startShiftNo = "";
                                            string endShiftNo = "";
                                            startShiftNo = leaveApply.getLVTotal(txtEmployeeNo.Text.Trim(), txtStartDate.Text);
                                            endShiftNo = leaveApply.getLVTotal(txtEmployeeNo.Text.Trim(), txtEndDate.Text);
                                            if (!(endShiftNo.StartsWith("C") || !startShiftNo.StartsWith("C")))
                                            {
                                                endShiftNo = leaveApply.getLVTotal(txtEmployeeNo.Text.Trim(), Convert.ToDateTime(this.txtEndDate.Text).AddDays(-1.0).ToString("yyyy/MM/dd"));
                                            }
                                            if ((((startShiftNo.StartsWith("A") && endShiftNo.StartsWith("C")) || (startShiftNo.StartsWith("C") && endShiftNo.StartsWith("A"))) || ((startShiftNo.StartsWith("B") && endShiftNo.StartsWith("C")) || (startShiftNo.StartsWith("C") && endShiftNo.StartsWith("B")))) || (startShiftNo.Length == 0))
                                            {
                                                WriteMessage(Message.ShiftNoNotSame);
                                                return;
                                            }
                                        }
                                        double MrelAdjust = 0.0;
                                        if (this.ddlLVTypeCode.SelectedValue.Equals("U"))
                                        {
                                            MrelAdjust = CheckLvtotal(this.ProcessFlag.Value, txtEmployeeNo.Text.Trim(), txtStartDate.Text, hidBillNo.Value.Trim());
                                        }
                                        string LeaveID = "";
                                        DataTable tempDataTable = leaveApply.getDataByBillNo(hidBillNo.Value.Trim());
                                        if (this.ProcessFlag.Value.Equals("Add"))
                                        {
                                            if (!CheckLeaveOverTime(txtEmployeeNo.Text, txtStartDate.Text, txtStartTime.Text, txtEndDate.Text, txtEndTime.Text))
                                            {
                                                WriteMessage(Message.LeaveDaySame);
                                                return;
                                            }
                                            string restChangeHours = leaveApply.getRestChangeHours();
                                            if ((restChangeHours != "N") && (this.ddlLVTypeCode.SelectedValue == "U"))
                                            {
                                                int lvHours = Convert.ToInt32(txtLVTotal.Value);
                                                int maxRestChangeHours = Convert.ToInt32(restChangeHours);
                                                if (lvHours > maxRestChangeHours)
                                                {
                                                    WriteMessage(Message.OverMostHours + ":" + restChangeHours);
                                                    return;
                                                }
                                            }
                                            row = tempDataTable.NewRow();
                                            row.BeginEdit();
                                            row["WorkNo"] = txtEmployeeNo.Text.ToUpper().Trim();
                                            row["LVTypeCode"] = this.ddlLVTypeCode.SelectedValue;
                                            row["StartDate"] = txtStartDate.Text;
                                            row["StartShiftNo"] = leaveApply.getLVTotal(txtEmployeeNo.Text.Trim(), txtStartDate.Text);
                                            row["StartTime"] = txtStartTime.Text;
                                            row["EndDate"] = txtEndDate.Text;
                                            row["EndShiftNo"] = leaveApply.getLVTotal(txtEmployeeNo.Text.Trim(), txtEndDate.Text);
                                            row["EndTime"] = txtEndTime.Text;
                                            row["LVTotal"] = txtLVTotal.Value.ToString();
                                            row["Reason"] = txtReason.Text.Trim();
                                            row["proxyworkno"] = txtProxyWorkNo.Text.Trim();
                                            //      row["proxy"] = MoveSpecailChar(txtProxyName.Text);
                                            row["proxy"] = hidProxyName.Value;
                                            //row["proxynotes"] = MoveSpecailChar(txtProxyNotes.Text).Replace(",", "");
                                            row["proxynotes"] = hidProxyNotes.Value;
                                            row["proxystatus"] = txtProxyWorkNo.Text.Trim().Length > 0 ? "1" : "";//沒有代理人代理人狀態設為“”
                                            row["Remark"] = txtRemark.Text.Trim();
                                            row["ApplyType"] = this.ddlApplyType.SelectedValue;
                                            row["Status"] = strStatus;
                                            row["UPDATE_USER"] = CurrentUserInfo.Cname.ToUpper();
                                            row["EMERGENCYCONTACTPERSON"] = txtEmergencyContactPerson.Text.Trim();
                                            row["EMERGENCYTELEPHONE"] = txtEmergencyTelephone.Text.Trim();

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
                                            LeaveID = leaveApply.SaveData(this.ProcessFlag.Value, tempDataTable, logmodel);
                                            leaveApply.getLeaveDetail(LeaveID);
                                            this.hidSave.Value = "Save";
                                        }
                                        else if (ProcessFlag.Value.Equals("Modify"))
                                        {
                                            if (tempDataTable.Rows.Count == 0)
                                            {
                                                WriteMessage(Message.AtLastOneChoose);
                                                return;
                                            }
                                            if (!CheckLeaveOverTime(txtEmployeeNo.Text, txtStartDate.Text, txtStartTime.Text, txtEndDate.Text, txtEndTime.Text, hidBillNo.Value))
                                            {
                                                WriteMessage(Message.LeaveDaySame);
                                                return;
                                            }
                                            row = tempDataTable.Rows[0];
                                            row.BeginEdit();
                                            row["ID"] = this.hidBillNo.Value;
                                            row["WorkNo"] = txtEmployeeNo.Text.ToUpper().Trim();
                                            row["LVTypeCode"] = ddlLVTypeCode.SelectedValue;
                                            row["StartDate"] = txtStartDate.Text;
                                            row["StartShiftNo"] = leaveApply.getLVTotal(txtEmployeeNo.Text.Trim(), txtStartDate.Text);
                                            row["StartTime"] = txtStartTime.Text;
                                            row["EndDate"] = txtEndDate.Text;
                                            row["EndShiftNo"] = leaveApply.getLVTotal(txtEmployeeNo.Text.Trim(), txtEndDate.Text);
                                            row["EndTime"] = txtEndTime.Text;
                                            row["LVTotal"] = txtLVTotal.Value.ToString();
                                            row["Reason"] = txtReason.Text.Trim();
                                            row["proxyworkno"] = txtProxyWorkNo.Text.Trim();
                                            row["proxy"] = hidProxyName.Value;
                                            //row["proxy"] = txtProxyName.Text.Trim();
                                         //   row["proxynotes"] = MoveSpecailChar(txtProxyNotes.Text).Replace(",", "");
                                            row["proxynotes"] = hidProxyNotes.Value;
                                            row["proxystatus"] = txtProxyWorkNo.Text.Trim().Length > 0 ? "1" : "";//沒有代理人代理人狀態設為“”
                                            row["Remark"] = txtRemark.Text.Trim();
                                            row["ApplyType"] = ddlApplyType.SelectedValue;
                                            row["UPDATE_USER"] = CurrentUserInfo.Cname.ToUpper();
                                            row["EMERGENCYCONTACTPERSON"] = txtEmergencyContactPerson.Text.Trim();
                                            row["EMERGENCYTELEPHONE"] = txtEmergencyTelephone.Text.Trim();
                                            if (ddlLVTypeCode.SelectedValue.Equals("J"))
                                            {
                                                row["WifeISFoxconn"] = ddlConsortInFoxonn.SelectedValue;
                                                row["WifeBG"] = txtConsortDepCode.Text;
                                                row["WifeWorkNo"] = txtConsortWorkNo.Text;
                                                row["WifeName"] = txtConsortName.Text;
                                                row["WifeLevelName"] = ddlLevelCode.SelectedValue;
                                            }
                                            if ((hidStatus.Value == "3") || (hidStatus.Value == "0"))
                                            {
                                                row["Status"] = strStatus;
                                                if (Convert.ToString(row["ProxyStatus"]).Equals("3"))
                                                {
                                                    row["ProxyStatus"] = strStatus;
                                                }
                                            }
                                            row.EndEdit();
                                            tempDataTable.AcceptChanges();
                                            leaveApply.SaveData(ProcessFlag.Value, tempDataTable, logmodel);
                                            leaveApply.getLeaveDetail(hidBillNo.Value);
                                        }
                                        if (this.ddlLVTypeCode.SelectedValue.Equals("U"))
                                        {
                                            leaveApply.CountCanAdjlasthy(txtEmployeeNo.Text.ToUpper().Trim());
                                        }
                                        if (this.ProcessFlag.Value == "Add")
                                        {
                                            if (ddlLVTypeCode.SelectedValue.Equals("U"))
                                            {
                                                if (MrelAdjust < Convert.ToDouble(txtLVTotal.Value.ToString()))
                                                {
                                                    WriteMessage(Message.Tip + MrelAdjust);
                                                }
                                                else
                                                {
                                                    WriteMessage(Message.kqm_addsessn);
                                                }
                                            }
                                            else
                                            {
                                                WriteMessage(Message.kqm_addsessn);
                                            }
                                            this.Add();
                                            if (txtEmployeeNo.Text.Trim().Length > 0)
                                            {
                                                EmpQuery(txtEmployeeNo.Text.Trim());
                                            }
                                        }
                                        else if (ddlLVTypeCode.SelectedValue.Equals("U"))
                                        {
                                            if (MrelAdjust < Convert.ToDouble(txtLVTotal.Value.ToString()))
                                            {
                                                Response.Write(string.Concat(new object[] { "<script type='text/javascript'>alert('", Message.Tip, MrelAdjust, "');window.parent.document.all.btnQuery.click();</script>" }));
                                            }
                                            else
                                            {
                                                Response.Write("<script type='text/javascript'>window.parent.document.all.btnQuery.click();</script>");
                                            }
                                        }
                                        else
                                        {
                                            Response.Write("<script type='text/javascript'>window.parent.document.all.btnQuery.click();</script>");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteMessage(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }

        private bool CheckLeaveOverTime(string empNo, string startDate, string startTime, string endDate, string endTime)
        {
            return leaveApply.CheckLeaveOverTime(empNo, startDate, startTime, endDate, endTime);
        }

        private double CheckLvtotal(string processFlag, string empNo, string startDate, string BillNo)
        {
            return leaveApply.CheckLvtotal(processFlag, empNo, startDate, BillNo);
        }

        private bool CheckLeaveOverTime(string WorkNo, string StartDate, string StartTime, string EndDate, string EndTime, string BillNo)
        {
            return leaveApply.CheckLeaveOverTime(WorkNo, StartDate, StartTime, EndDate, EndTime, BillNo);
        }
    }
}
