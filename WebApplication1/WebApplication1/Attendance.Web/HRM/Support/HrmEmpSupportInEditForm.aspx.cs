/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： HrmEmpSupportInEditForm
 * 檔功能描述： 內部支援編輯UI
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2012.01.06
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.HRM.Support;
using GDSBG.MiABU.Attendance.BLL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.HRM.Support;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using Resources;

namespace GDSBG.MiABU.Attendance.Web.HRM.Support
{
    public partial class HrmEmpSupportInEditForm : BasePage
    {
        EmpSupportInModel model = new EmpSupportInModel();
        static SynclogModel logmodel = new SynclogModel();
        TypeDataBll typeDataBll = new TypeDataBll();
        EmpSupportInBll empSupportInBll = new EmpSupportInBll();
        EmployeeBll employeeBll = new EmployeeBll();
        DataTable dt = new DataTable();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;

        #region 頁面加載
        protected void Page_Load(object sender, EventArgs e)
        {
            SetCalendar(txtStartDate, txtPrepEndDate, txtEndDate);
            if (!base.IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;

                this.ddlDataBind();
                string EmployeeNo = (base.Request.QueryString["EmployeeNo"] == null) ? "" : base.Request.QueryString["EmployeeNo"].ToString();
                string SupportOrder = (base.Request.QueryString["SupportOrder"] == null) ? "" : base.Request.QueryString["SupportOrder"].ToString();
                string ProcessFlag = (base.Request.QueryString["ProcessFlag"] == null) ? "" : base.Request.QueryString["ProcessFlag"].ToString();
                this.HiddenSupportOrder.Value = SupportOrder;
                string module_Code = base.Request.QueryString["ModuleCode"].ToString();
                this.imgDepCode.Attributes.Add("onclick", "javascript:GetTreeDataValue(\"txtSupportDeptName\",'','txtSupportDept')");
                this.ProcessFlag.Value = ProcessFlag;
                if (ProcessFlag == "Add")
                {
                    this.Add();
                }
                else if ((EmployeeNo.Length > 0) && (SupportOrder.Length > 0))
                {
                    this.Modify(EmployeeNo, SupportOrder);
                }
                else
                {
                    base.Response.Write("<script type='text/javascript'>alert(\"" + Message.NoItemSelected + "\");window.parent.document.all.topTable.style.display='';window.parent.document.all.divEdit.style.display='none';window.parent.document.all.PanelData.style.display='';window.parent.document.all.div_2.style.display=''</script>");
                } 
            }
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("WorkNoIsWrong", Message.WorkNoIsWrong);
                ClientMessage.Add("WorkNo", Message.WorkNo);
                ClientMessage.Add("SupportDeptNameNotNull", Message.SupportDeptNameNotNull);
                ClientMessage.Add("StateNotNull", Message.StateNotNull);
                ClientMessage.Add("IsKaoQinNotNull", Message.IsKaoQinNotNull);
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
            this.ProcessFlag.Value = "Add";
            this.TextBoxsReset("Add", false);

        }
        #endregion

        #region 存儲
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string alert = "";
            model = PageHelper.GetModel<EmpSupportInModel>(pnlContent.Controls);
            string WorkNo = model.WorkNo;
            if (empSupportInBll.GetEmpStatus(WorkNo).Rows[0][0].ToString().Equals("2"))
            {
                base.Response.Write("<script type='text/javascript'>alert(\"" + Message.WorkNoIsWrong + "\");</script>");
                return;
            }
            if (this.ProcessFlag.Value == "Add")
            {
                logmodel.ProcessFlag = "insert";
                if (empSupportInBll.GetEmpSupportIn(WorkNo).Rows.Count>0)
                {
                    base.Response.Write("<script type='text/javascript'>alert(\"" + Message.MoreThanOneSupport + "\");</script>");
                    return;
                }
                model.SupportOrder = empSupportInBll.GetEmpsupportOrder(WorkNo).Rows[0][0].ToString();
                bool flag = empSupportInBll.AddEmpSupportIn(model,logmodel);
                if (flag == true)
                {
                    alert = "alert('" + Message.AddSuccess + "')";
                    this.ddlState.SelectedIndex = 0;
                    this.txtWorkNo.Text = "";
                    this.txtSupportDept.Text = "";
                    this.txtSupportDeptName.Text = "";
                    this.txtStartDate.Text = "";
                    this.txtPrepEndDate.Text = "";
                    this.txtEndDate.Text = "";
                    this.txtRemark.Text = "";
                    this.HiddenSupportOrder.Value = "";
                }
                else
                {
                    alert = "alert('" + Message.AddFailed + "')";
                    this.TextBoxsReset("Add", false);
                }
            }
            if (this.ProcessFlag.Value == "Modify")
            {
                logmodel.ProcessFlag = "update";
                string tmpSupportOrder = this.HiddenSupportOrder.Value.Trim();
                if (empSupportInBll.GetEmpSupportByWorkNoAndOrder(WorkNo, tmpSupportOrder).Rows.Count > 0)
                {
                    base.Response.Write("<script type='text/javascript'>alert(\"" + Message.DuplicateState + "\");</script>");
                    return;
                }
                model.SupportOrder = tmpSupportOrder;
                bool flag = empSupportInBll.UpdateEmpSupportIn(model,logmodel);
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
            EmpSupportInModel newmodel = empSupportInBll.GetEmpSupportInInfo(model);
            if (newmodel != null)
            {
                PageHelper.BindControls<EmpSupportInModel>(newmodel, pnlContent.Controls);
                this.HiddenSupportOrder.Value = (SeqNo == null) ? "" : SeqNo;
                this.HiddenState.Value = newmodel.State;
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
            this.txtLocalName.BorderStyle = BorderStyle.None;
            this.txtSex.BorderStyle = BorderStyle.None;
            this.txtLevelName.BorderStyle = BorderStyle.None;
            this.txtManagerName.BorderStyle = BorderStyle.None;
            this.txtTechnicalName.BorderStyle = BorderStyle.None;
            this.txtSupportDeptName.BorderStyle = BorderStyle.None;
            if (buttonText.Equals("Add") || buttonText.Equals("Modify"))
            {
                this.txtWorkNo.ReadOnly = false;
                if (buttonText.Equals("Add"))
                {
                    this.ddlDataBind();
                    this.txtSupportDept.Text = "";
                    this.txtSupportDeptName.Text = "";
                    this.txtStartDate.Text = "";
                    this.txtPrepEndDate.Text = "";
                    this.txtEndDate.Text = "";
                    this.txtRemark.Text = "";
                    this.HiddenSupportOrder.Value = "";
                }
                else
                {
                    this.txtWorkNo.BorderStyle = BorderStyle.None;
                }
            }
            else
            {
                this.txtWorkNo.BorderStyle = BorderStyle.NotSet;
            }
        }
        #endregion

        #region ajax
        protected override void AjaxProcess()
        {
            string noticeJson = "";
            if (!string.IsNullOrEmpty(Request.Form["WorkNo"]))
            {
                string WorkNo = Request.Form["WorkNo"].ToString();
                dt=empSupportInBll.GetEmp(WorkNo);
                List<EmployeeModel> list = employeeBll.GetList(dt);
                if (list.Count >0)
                {
                    noticeJson = JsSerializer.Serialize(list[0]);
                }
                Response.Clear();
                Response.Write(noticeJson);
                Response.End();
            }
        }
        #endregion
    }
}
