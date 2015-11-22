using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.KQM.KaoQinData;
using GDSBG.MiABU.Attendance.Model.KQM.KaoQinData;
using Resources;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;


/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMEmployeeShiftEditForm.aspx.cs
 * 檔功能描述： 例外排班
 * 
 * 版本：1.0
 * 創建標識： 昝望 2011.12.27
 * 
 */


namespace GDSBG.MiABU.Attendance.Web.KQM.KaoQinData
{
    public partial class KQMEmployeeShiftEditForm : BasePage
    {

        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        EmployeeShiftBll bll = new EmployeeShiftBll();
        string employeeNo = "";
        static SynclogModel logmodel = new SynclogModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            employeeNo = Request.QueryString["EmployeeNo"];
            HiddenEmployeeNo.Value = employeeNo;
            HiddenShift.Value = employeeNo;
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("DeleteConfirm", Message.DeleteConfirm);
                ClientMessage.Add("SaveConfim", Message.SaveConfim);
                ClientMessage.Add("DataExist", Message.DataExist);
                ClientMessage.Add("ShiftNoNotNull", Message.ShiftNoNotNull);
                ClientMessage.Add("StartDateNotNull", Message.StartDateNotNull);
                ClientMessage.Add("EndDateNotNull", Message.EndDateNotNull);
                ClientMessage.Add("EndLaterThanStart", Message.EndLaterThanStart);
                ClientMessage.Add("DepCodeNotNull", Message.DepCodeNotNull);
                ClientMessage.Add("LanguageNotNull", Message.LanguageNotNull);
                ClientMessage.Add("IfAdminNotNull", Message.IfAdminNotNull);
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
                ClientMessage.Add("EmpNotExist", Message.EmpNotExist);
                string clientmsg = JsSerializer.Serialize(ClientMessage);
                Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
            }
            SetCalendar(txtStartDate, txtEndDate);
            if (!IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;
                txtChineseName.Attributes.Add("disabled", "true");
                txtSYC.Attributes.Add("disabled", "true");
                txtSYB.Attributes.Add("disabled", "true");
                btnAdd.Attributes.Add("disabled", "true");
                btnModify.Attributes.Add("disabled", "true");
                btnCancel.Attributes.Add("disabled", "true");
                btnSave.Attributes.Add("disabled", "true");
                btnDelete.Attributes.Add("disabled", "true");
                ddlShiftNo.Attributes.Add("disabled", "true");
                if (employeeNo != "")
                {
                    btnAdd.Attributes.Remove("disabled");
                    btnModify.Attributes.Remove("disabled");
                    btnDelete.Attributes.Remove("disabled");
                    DataTable dt = bll.GetEmployeeInfo(employeeNo);
                    if (dt != null && dt.Rows.Count != 0)
                    {
                        this.txtEmployeeNo.Text = dt.Rows[0]["WORKNO"].ToString();
                        this.txtChineseName.Text = dt.Rows[0]["LOCALNAME"].ToString();
                        this.txtSYC.Text = dt.Rows[0]["Syc"].ToString();
                        this.txtSYB.Text = dt.Rows[0]["DEPNAME"].ToString();
                        HiddenChineseName.Value = dt.Rows[0]["LOCALNAME"].ToString();
                        this.HiddenSYC.Value = dt.Rows[0]["Syc"].ToString();
                        this.HiddenSYB.Value = dt.Rows[0]["DEPNAME"].ToString();
                        DataTable dt_shift = bll.GetShift(dt.Rows[0]["DEPcode"].ToString());
                        //DataTable dt_shift = bll.GetShift("B00002");
                        ddlShiftNo.DataSource = dt_shift;
                        this.ddlShiftNo.DataTextField = "ShiftDetail";
                        this.ddlShiftNo.DataValueField = "ShiftNo";
                        this.ddlShiftNo.DataBind();
                        this.ddlShiftNo.Items.Insert(0, new ListItem("", ""));
                        this.UltraWebGrid.DataSource = bll.GetEmployeeShifInfo(txtEmployeeNo.Text.Trim());
                        this.UltraWebGrid.DataBind();
                        txtEmployeeNo.Attributes.Add("disabled", "true");
                    }
                }
            }
        }

        #region  數據綁定
        private void DataBind()
        {
            ddlShiftNo.Attributes.Add("disabled", "true");
            txtChineseName.Attributes.Add("disabled", "true");
            txtSYC.Attributes.Add("disabled", "true");
            txtSYB.Attributes.Add("disabled", "true");
            btnQuery.Attributes.Remove("disabled");
            btnAdd.Attributes.Remove("disabled");
            btnModify.Attributes.Remove("disabled");
            btnDelete.Attributes.Remove("disabled");
            btnSave.Attributes.Add("disabled", "true");
            btnCancel.Attributes.Add("disabled", "true");
            if (HiddenShift.Value != "")
            {
                this.txtStartDate.Text = "";
                this.txtEndDate.Text = "";
                this.ddlShiftNo.SelectedValue = "";
                txtEmployeeNo.Text = HiddenEmployeeNo.Value;
                this.txtChineseName.Text = HiddenChineseName.Value;
                this.txtSYC.Text = this.HiddenSYC.Value;
                this.txtSYB.Text = this.HiddenSYB.Value;
                this.UltraWebGrid.DataSource = bll.GetEmployeeShifInfo(txtEmployeeNo.Text.Trim());
                this.UltraWebGrid.DataBind();
            }
            else
            {
                this.txtEmployeeNo.Text = "";
                this.txtChineseName.Text = "";
                this.txtSYC.Text = "";
                this.txtSYB.Text = "";
                this.txtStartDate.Text = "";
                this.txtEndDate.Text = "";
                this.ddlShiftNo.SelectedValue = "";
                this.UltraWebGrid.DataSource = bll.GetEmployeeShifInfo(txtEmployeeNo.Text.Trim());
                this.UltraWebGrid.DataBind();
            }
        }
        #endregion

        #region 查詢
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            if (txtEmployeeNo.Text.Trim() != "")
            {
                ddlShiftNo.Attributes.Add("disabled", "true");
                txtChineseName.Attributes.Add("disabled", "true");
                txtSYC.Attributes.Add("disabled", "true");
                txtSYB.Attributes.Add("disabled", "true");
                btnQuery.Attributes.Remove("disabled");
                btnAdd.Attributes.Remove("disabled");
                btnModify.Attributes.Remove("disabled");
                btnDelete.Attributes.Remove("disabled");
                btnSave.Attributes.Add("disabled", "true");
                btnCancel.Attributes.Add("disabled", "true");
                DataTable dt = bll.GetEmployeeInfo(txtEmployeeNo.Text.Trim());
                if (dt != null && dt.Rows.Count != 0)
                {
                    this.txtEmployeeNo.Text = dt.Rows[0]["WORKNO"].ToString();
                    this.txtChineseName.Text = dt.Rows[0]["LOCALNAME"].ToString();
                    this.txtSYC.Text = dt.Rows[0]["Syc"].ToString();
                    this.txtSYB.Text = dt.Rows[0]["DEPNAME"].ToString();
                    HiddenChineseName.Value = dt.Rows[0]["LOCALNAME"].ToString();
                    this.HiddenSYC.Value = dt.Rows[0]["Syc"].ToString();
                    this.HiddenSYB.Value = dt.Rows[0]["DEPNAME"].ToString();
                    DataTable dt_shift = bll.GetShift(dt.Rows[0]["DEPcode"].ToString());
                    //DataTable dt_shift = bll.GetShift("B00002");
                    ddlShiftNo.DataSource = dt_shift;
                    this.ddlShiftNo.DataTextField = "ShiftDetail";
                    this.ddlShiftNo.DataValueField = "ShiftNo";
                    this.ddlShiftNo.DataBind();
                    this.ddlShiftNo.Items.Insert(0, new ListItem("", ""));
                    this.UltraWebGrid.DataSource = bll.GetEmployeeShifInfo(txtEmployeeNo.Text.Trim());
                    this.UltraWebGrid.DataBind();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.EmpBasicInfoNotExist + "');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.ErrorWorkNoNull + "');", true);
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            EmployeeShiftModel model = new EmployeeShiftModel();
            if (txtEmployeeNo.Text.ToString() != "")
            {
                model.WorkNo = txtEmployeeNo.Text.ToString();
            }
            else
            {
                model.WorkNo = HiddenEmployeeNo.Value;
            }

            model.Shift = ddlShiftNo.SelectedValue;
            model.UpdateUser = CurrentUserInfo.Personcode;
            model.ID = HiddenID.Value;
            try
            {
                model.StartDate = Convert.ToDateTime(this.txtStartDate.Text).ToString("yyyy/MM/dd");
                model.EndDate = Convert.ToDateTime(this.txtEndDate.Text).ToString("yyyy/MM/dd");
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.ErrDateFormat + "');", true);
                txtChineseName.Text = HiddenChineseName.Value;
                txtSYC.Text = HiddenSYC.Value;
                txtSYB.Text = HiddenSYB.Value;
                return;
            }
            DataTable dt = bll.GetEmployeeShifInfo(model.WorkNo);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["ID"].ToString() != HiddenID.Value)
                {
                    if (!(Convert.ToDateTime(this.txtStartDate.Text) > Convert.ToDateTime(dt.Rows[i]["enddate"].ToString()) || Convert.ToDateTime(this.txtEndDate.Text) < Convert.ToDateTime(dt.Rows[i]["startdate"].ToString())))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.ShiftNotOnly + "');", true);
                        return;
                    }
                }
            }
            bool succeed;
            if (hidOperate.Value == "modify")
            {
                logmodel.ProcessFlag = "update";
                succeed = bll.UpdateEmployeeShiftByKey(model, logmodel);
                if (succeed)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.UpdateSuccess + "');", true);
                    DataBind();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.UpdateFailed + "');", true);

                }
            }
            if (hidOperate.Value == "add")
            {
                logmodel.ProcessFlag = "insert";
                succeed = bll.AddEmployeeShift(model, logmodel);
                if (succeed)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.AddSuccess + "');", true);
                    DataBind();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.AddFailed + "');", true);
                }

            }


        }
        #endregion

        #region 刪除
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "delete";
            EmployeeShiftModel model = new EmployeeShiftModel();
            model.ID = HiddenID.Value;
            if (model.ID != "")
            {
                bool flag = bll.DeleteEmployeeShiftByKey(model.ID, logmodel);
                if (flag)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.DeleteSuccess + "');", true);
                    DataBind();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.DeleteFailed + "');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.AtLastOneChoose + "');", true);
            }
        }
        #endregion

        #region 取消
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.hidOperate.Value.Equals("add"))
            {
                if (this.UltraWebGrid.Rows.Count > 0)
                {
                    if (this.UltraWebGrid.DisplayLayout.SelectedRows.Count == 0)
                    {
                        this.UltraWebGrid.Rows[0].Selected = true;
                    }
                    this.txtStartDate.Text = (this.UltraWebGrid.Rows[this.UltraWebGrid.DisplayLayout.SelectedRows[0].Index].Cells.FromKey("StartDate").Text == null) ? "" : string.Format("{0:}", Convert.ToDateTime(this.UltraWebGrid.Rows[this.UltraWebGrid.DisplayLayout.SelectedRows[0].Index].Cells.FromKey("StartDate").Text.Trim()));
                    this.txtEndDate.Text = (this.UltraWebGrid.Rows[this.UltraWebGrid.DisplayLayout.SelectedRows[0].Index].Cells.FromKey("EndDate").Text == null) ? "" : string.Format("{0:}", Convert.ToDateTime(this.UltraWebGrid.Rows[this.UltraWebGrid.DisplayLayout.SelectedRows[0].Index].Cells.FromKey("EndDate").Text.Trim()));
                    this.HiddenShiftNo.Value = (this.UltraWebGrid.Rows[this.UltraWebGrid.DisplayLayout.SelectedRows[0].Index].Cells.FromKey("ShiftNo").Text == null) ? "" : this.UltraWebGrid.Rows[this.UltraWebGrid.DisplayLayout.SelectedRows[0].Index].Cells.FromKey("ShiftNo").Text.Trim();
                    this.ddlShiftNo.SelectedIndex = this.ddlShiftNo.Items.IndexOf(this.ddlShiftNo.Items.FindByValue((this.UltraWebGrid.Rows[this.UltraWebGrid.DisplayLayout.SelectedRows[0].Index].Cells.FromKey("ShiftNo").Text == null) ? "" : this.UltraWebGrid.Rows[this.UltraWebGrid.DisplayLayout.SelectedRows[0].Index].Cells.FromKey("ShiftNo").Text.Trim()));
                    this.HiddenID.Value = (this.UltraWebGrid.Rows[this.UltraWebGrid.DisplayLayout.SelectedRows[0].Index].Cells.FromKey("ID").Text == null) ? "" : this.UltraWebGrid.Rows[this.UltraWebGrid.DisplayLayout.SelectedRows[0].Index].Cells.FromKey("ID").Text.Trim();
                }
                else
                {
                    this.ddlShiftNo.SelectedValue = "";
                    this.txtStartDate.Text = "";
                    this.txtEndDate.Text = "";
                    this.HiddenShiftNo.Value = "";
                    this.HiddenID.Value = "";
                }
            }
            this.hidOperate.Value = "";
            txtEmployeeNo.Text = HiddenEmployeeNo.Value;
            this.txtChineseName.Text = HiddenChineseName.Value;
            this.txtSYC.Text = this.HiddenSYC.Value;
            this.txtSYB.Text = this.HiddenSYB.Value;
            this.TextBoxsReset("Cancel", true);
            btnQuery.Attributes.Remove("disabled");
            btnAdd.Attributes.Remove("disabled");
            btnModify.Attributes.Remove("disabled");
            btnDelete.Attributes.Remove("disabled");
            btnSave.Attributes.Add("disabled", "true");
            btnCancel.Attributes.Add("disabled", "true");
            ddlShiftNo.Attributes.Add("disabled", "true");
            SetCalendar(txtStartDate, txtEndDate);
        }
        #endregion

        #region 私有函數
        private void TextBoxsReset(string buttonText, bool read)
        {
            this.txtStartDate.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtEndDate.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            if ((buttonText.ToLower() == "add") || (buttonText.ToLower() == "modify"))
            {
                this.txtEmployeeNo.BorderStyle = BorderStyle.None;
                if (buttonText.ToLower() == "add")
                {
                    this.txtStartDate.Text = "";
                    this.txtEndDate.Text = "";
                    this.ddlShiftNo.SelectedValue = "";
                    this.HiddenShiftNo.Value = "";
                    this.HiddenID.Value = "";
                }
            }
            else
            {
                this.txtEmployeeNo.BorderStyle = BorderStyle.NotSet;
            }
        }
        #endregion

        #region 查詢工號
        protected override void AjaxProcess()
        {
            string noticeJson = null;
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(Request.Form["Empno"]))
            {
                string empno = Request.Form["Empno"];
                EmployeeShiftModel model = new EmployeeShiftModel();
                dt = bll.GetEmployeeInfo(empno);
                if (dt.Rows.Count != 0)
                {
                    model.LocalName = dt.Rows[0]["LocalName"].ToString();
                    model.Syc = dt.Rows[0]["Syc"].ToString();
                    model.DepName = dt.Rows[0]["DEPNAME"].ToString();
                    if (Request.Form["Flag"].ToString() == "add")
                    {
                        DataTable dt_shift = bll.GetShift(dt.Rows[0]["DEPcode"].ToString());
                        ddlShiftNo.DataSource = dt_shift;
                        this.ddlShiftNo.DataTextField = "shiftdetail";
                        this.ddlShiftNo.DataValueField = "shiftno";
                        this.ddlShiftNo.DataBind();
                        this.ddlShiftNo.Items.Insert(0, new ListItem("", ""));
                    }
                }
                if (model != null)
                {
                    noticeJson = JsSerializer.Serialize(model);
                }
                Response.Clear();
                Response.Write(noticeJson);
                Response.End();
            }
        }
        #endregion

        protected void txtEmployeeNo_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(this.txtEmployeeNo.Text))
            {
                string empno = this.txtEmployeeNo.Text.ToString();
                HiddenEmployeeNo.Value = empno;
                EmployeeShiftModel model = new EmployeeShiftModel();
                dt = bll.GetEmployeeInfo(empno);
                if (dt.Rows.Count != 0)
                {
                    this.txtChineseName.Text = dt.Rows[0]["LocalName"].ToString();
                    this.txtSYC.Text = dt.Rows[0]["Syc"].ToString();
                    this.txtSYB.Text = dt.Rows[0]["DEPNAME"].ToString();
                    if (hidOperate.Value == "add")
                    {
                        DataTable dt_shift = bll.GetShift(dt.Rows[0]["DEPcode"].ToString());
                        ddlShiftNo.DataSource = dt_shift;
                        this.ddlShiftNo.DataTextField = "shiftdetail";
                        this.ddlShiftNo.DataValueField = "shiftno";
                        this.ddlShiftNo.DataBind();
                        this.ddlShiftNo.Items.Insert(0, new ListItem("", ""));
                        txtEmployeeNo.Attributes.Remove("disabled");
                        ddlShiftNo.Attributes.Remove("disabled");
                        txtStartDate.Attributes.Remove("readonly");
                        txtEndDate.Attributes.Remove("readonly");
                        this.txtStartDate.CssClass = "input_textBox_haveborder";
                        this.txtEndDate.CssClass = "input_textBox_haveborder";
                        btnQuery.Attributes.Add("disabled", "true");
                        btnAdd.Attributes.Add("disabled", "true");
                        btnModify.Attributes.Add("disabled", "true");
                        btnDelete.Attributes.Add("disabled", "true");
                        btnSave.Attributes.Remove("disabled");
                        btnCancel.Attributes.Remove("disabled");
                    }
                }



            }
        }

    }
}
