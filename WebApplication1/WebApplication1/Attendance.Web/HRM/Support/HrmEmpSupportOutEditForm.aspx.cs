/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： HrmEmpSupportOutEditForm
 * 檔功能描述： 外部支援編輯UI
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2012.01.04
 * 
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.HRM;
using GDSBG.MiABU.Attendance.BLL.HRM.Support;
using GDSBG.MiABU.Attendance.BLL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.HRM.Support;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using Resources;

namespace GDSBG.MiABU.Attendance.Web.HRM.Support
{
    public partial class HrmEmpSupportOutEditForm : BasePage
    {
        EmployeeBll employeeBll = new EmployeeBll();
        TypeDataBll typeDataBll = new TypeDataBll();
        TWCadreBll twCadreBll = new TWCadreBll();
        EmpSupportOutBll empSupportOutBll = new EmpSupportOutBll();
        EmpSupportOutModel model = new EmpSupportOutModel();
        static SynclogModel logmodel = new SynclogModel();
        DataTable dt = new DataTable();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;

        #region 頁面加載
        protected void Page_Load(object sender, EventArgs e)
        {
            SetCalendar(txtJoinDate, txtStartDate, txtPrepEndDate, txtEndDate);
            if (!base.IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;

                this.ddlIsKaoQin.Items.Insert(0, new ListItem("", ""));
                this.ddlIsKaoQin.Items.Insert(1, new ListItem("Y", "Y"));
                this.ddlIsKaoQin.Items.Insert(2, new ListItem("N", "N"));
                this.ddlDataBind();
                string EmployeeNo = (base.Request.QueryString["EmployeeNo"] == null) ? "" : base.Request.QueryString["EmployeeNo"].ToString();
                string SeqNo = (base.Request.QueryString["SeqNo"] == null) ? "" : base.Request.QueryString["SeqNo"].ToString();
                string ProcessFlag = (base.Request.QueryString["ProcessFlag"] == null) ? "" : base.Request.QueryString["ProcessFlag"].ToString();
                this.HiddenSeqNo.Value = SeqNo;
                string module_Code = base.Request.QueryString["ModuleCode"].ToString();
                this.imgDepCode.Attributes.Add("onclick", "javascript:GetTreeDataValue(\"txtSupportDeptName\",'" + module_Code + "','txtSupportDept')");
                this.ProcessFlag.Value = ProcessFlag;
                if (ProcessFlag == "Add")
                {
                    this.Add();
                }
                else if ((EmployeeNo.Length > 0) && (SeqNo.Length > 0))
                {
                    this.Modify(EmployeeNo, SeqNo);
                }
                else
                {
                    base.Response.Write("<script type='text/javascript'>alert(\"" + Message.NoItemSelected + "\");window.parent.document.all.topTable.style.display='';window.parent.document.all.divEdit.style.display='none';</script>");
                }
            }
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("WorkNoNotNull", Message.WorkNoNotNull);
                ClientMessage.Add("LocalNameNotNull", Message.LocalNameNotNull);
                ClientMessage.Add("SexNotNull", Message.SexNotNull);
                ClientMessage.Add("SupportDeptNameNotNull", Message.SupportDeptNameNotNull);
                ClientMessage.Add("SubDepNameNotNull", Message.SubDepNameNotNull);
                ClientMessage.Add("LevelNotNull", Message.LevelNotNull);
                ClientMessage.Add("OverTimeTypeNotNull", Message.OverTimeTypeNotNull);
                ClientMessage.Add("StateNotNull", Message.StateNotNull);
                ClientMessage.Add("IsKaoQinNotNull", Message.IsKaoQinNotNull);
                ClientMessage.Add("JoinDateNotNull", Message.JoinDateNotNull);
                ClientMessage.Add("WrongDate", Message.WrongDate);
                ClientMessage.Add("EndDateLaterThanStartDate", Message.EndDateLaterThanStartDate);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
        }
        #endregion

        #region 新增頁面設置
        protected void Add()
        {
            if (this.HiddenSave.Value != "Save")
            {
                this.txtWorkNo.Text = "";
            }
            this.ddlState.Enabled = false;
            this.ProcessFlag.Value = "Add";
            this.TextBoxsReset("Add", false);
        }
        #endregion

        #region 存儲
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string alert = "";
            model = PageHelper.GetModel<EmpSupportOutModel>(pnlContent.Controls);
            string WorkNo = model.WorkNo;
            model.SupportOrder = this.HiddenSeqNo.Value;
            if (this.ProcessFlag.Value == "Add")
            {
                logmodel.ProcessFlag = "insert";
                if (employeeBll.GetEmp(WorkNo).Rows.Count > 0 || empSupportOutBll.GetEmpSupportOut(WorkNo).Rows.Count > 0)
                {
                    base.Response.Write("<script type='text/javascript'>alert(\"" + Message.DuplicateState + "\");</script>");
                    return;
                }
                model.SupportOrder = empSupportOutBll.GetEmpsupportOrder(WorkNo).Rows[0][0].ToString();
                bool flag = empSupportOutBll.AddEmpSupportOut(model, logmodel);
                if (flag == true)
                {
                    alert = "alert('" + Message.AddSuccess + "')";
                    this.txtWorkNo.Text = "";
                    this.txtLocalName.Text = "";
                    this.ddlSex.SelectedValue = "";
                    this.txtSupportDept.Text = "";
                    this.txtSupportDeptName.Text = "";
                    this.txtDepName.Text = "";
                    this.txtJoinDate.Text = "";
                    this.txtStartDate.Text = "";
                    this.txtPrepEndDate.Text = "";
                    this.txtEndDate.Text = "";
                    this.txtNotes.Text = "";
                    this.ddlLevelCode.SelectedValue = "";
                    this.ddlManagerCode.SelectedValue = "";
                    this.ddlOverTimeType.SelectedValue = "";
                    this.ddlIsKaoQin.SelectedValue = "";
                    this.txtRemark.Text = "";
                    this.txtCardNo.Text = "";
                }
                else
                {
                    alert = "alert('" + Message.AddFailed + "')";
                }
            }
            if (this.ProcessFlag.Value == "Modify")
            {
                logmodel.ProcessFlag = "update";
                string tmpSupportOrder = this.HiddenSeqNo.Value.Trim();
                if (empSupportOutBll.GetEmpSupportByWorkNoAndOrder(WorkNo, tmpSupportOrder).Rows.Count > 0)
                {
                    base.Response.Write("<script type='text/javascript'>alert(\"" + Message.DuplicateState + "\");</script>");
                    return;
                }

                bool flag = empSupportOutBll.UpdateEmpSupportOut(model, logmodel);
                if (flag == true)
                {
                    base.Response.Write("<script type='text/javascript'>alert('" + Message.UpdateSuccess + "');window.parent.document.all.btnQuery.click();</script>");
                }
                else
                {
                    alert = "alert('" + Message.UpdateFailed + "')";
                }
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "save", alert, true);
        }
        #endregion

        #region 下拉菜單綁定
        private void ddlDataBind()
        {
            dt.Clear();
            dt = typeDataBll.GetSupportStatusList();
            this.ddlState.DataSource = dt.DefaultView;
            this.ddlState.DataTextField = "DataValue";
            this.ddlState.DataValueField = "DataCode";
            this.ddlState.DataBind();
            this.ddlDataBindForFixed("ddlSex", "Sex");
            this.ddlDataBindForFixed("ddlOverTimeType", "OverTimeType");
            this.ddlLevelCodeDataBind();
            this.ddlManagerCodeDataBind();
        }
        #endregion

        #region 綁定方法
        private void ddlDataBindForFixed(string ControlName, string DataType)
        {
            dt.Clear();
            dt = typeDataBll.GetdllDateTypeList(DataType);
            DropDownList DDL = (DropDownList)this.FindControl(ControlName);
            DDL.DataSource = dt.DefaultView;
            DDL.DataTextField = "DataValue";
            DDL.DataValueField = "DataCode";
            DDL.DataBind();
            DDL.Items.Insert(0, new ListItem("", ""));
        }
        #endregion

        #region 資位綁定
        private void ddlLevelCodeDataBind()
        {
            dt.Clear();
            dt = twCadreBll.GetLevel();
            this.ddlLevelCode.DataSource = dt.DefaultView;
            this.ddlLevelCode.DataTextField = "LevelName";
            this.ddlLevelCode.DataValueField = "LevelCode";
            this.ddlLevelCode.DataBind();
            this.ddlLevelCode.Items.Insert(0, new ListItem("", ""));
        }
        #endregion

        #region 管理職綁定
        private void ddlManagerCodeDataBind()
        {
            dt.Clear();
            dt = twCadreBll.GetManager();
            this.ddlManagerCode.DataSource = dt.DefaultView;
            this.ddlManagerCode.DataTextField = "ManagerName";
            this.ddlManagerCode.DataValueField = "ManagerCode";
            this.ddlManagerCode.DataBind();
            this.ddlManagerCode.Items.Insert(0, new ListItem("", ""));
        }
        #endregion

        #region 修改頁面設置
        protected void Modify(string EmployeeNo, string SeqNo)
        {
            this.txtWorkNo.Text = EmployeeNo;
            this.Query(EmployeeNo, SeqNo);
            this.ProcessFlag.Value = "Modify";
            this.TextBoxsReset("Modify", false);
        }
        #endregion

        #region 修改數據綁定
        private void Query(string EmployeeNo, string SeqNo)
        {
            model.WorkNo = EmployeeNo;
            model.SupportOrder = SeqNo;

            dt.Clear();
            EmpSupportOutModel newmodel = empSupportOutBll.GetEmpSupportOutInfo(model);
            if (newmodel != null)
            {
                PageHelper.BindControls<EmpSupportOutModel>(newmodel, pnlContent.Controls);
                this.LevelCodeHid.Value = newmodel.LevelCode;
                this.ManagerCodeHid.Value = newmodel.ManagerCode;
                this.OverTimeTypeHid.Value = newmodel.OverTimeType;
                this.StateHid.Value = newmodel.State;
                this.SupportOrderHid.Value = newmodel.SupportOrder;
            }
            else
            {
                base.Response.Write("<script type='text/javascript'>alert(\"" + Message.ErrWorkNo + "\");window.parent.document.all.topTable.style.display='';window.parent.document.all.divEdit.style.display='none';</script>");
            }
        }
        #endregion

        #region 頁面文本框屬性
        private void TextBoxsReset(string buttonText, bool read)
        {
            this.txtWorkNo.BorderStyle = (buttonText == "Modify") ? BorderStyle.None : BorderStyle.NotSet;
            this.txtLocalName.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtSupportDept.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtDepName.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtStartDate.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtPrepEndDate.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtEndDate.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtRemark.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtCardNo.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            if (buttonText.Equals("Add"))
            {
                this.txtWorkNo.Text = "";
                this.txtLocalName.Text = "";
                this.ddlSex.SelectedValue = "";
                this.txtSupportDept.Text = "";
                this.txtSupportDeptName.Text = "";
                this.txtDepName.Text = "";
                this.txtStartDate.Text = "";
                this.txtPrepEndDate.Text = "";
                this.txtEndDate.Text = "";
                this.txtNotes.Text = "";
                this.ddlLevelCode.SelectedValue = "";
                this.ddlManagerCode.SelectedValue = "";
                this.ddlOverTimeType.SelectedValue = "";
                this.ddlState.SelectedValue = "";
                this.txtRemark.Text = "";
                this.txtCardNo.Text = "";
            }
        }
        #endregion

    }
}
