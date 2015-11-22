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
using System.Web.Script.Serialization;
using System.Collections.Generic;
using GDSBG.MiABU.Attendance.BLL.KQM.KaoQinData;
using Resources;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： KQMOrgShiftEditForm.aspx.cs
 * 檔功能描述： 組織排班
 * 
 * 版本：1.0
 * 創建標識： 昝望 2011.12.27
 * 
 */


namespace GDSBG.MiABU.Attendance.Web.KQM.KaoQinData
{
    public partial class KQMOrgShiftEditForm : BasePage
    {
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        EmployeeShiftBll bll = new EmployeeShiftBll();
        static string Orgcode = "";
        static SynclogModel logmodel = new SynclogModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            //PageHelper.ButtonControls(base.FuncList, pnlShowPanel.Controls, base.FuncListModule);
            Orgcode = Request.QueryString["OrgCode"];
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("DeleteConfirm", Message.DeleteConfirm);
                ClientMessage.Add("SaveConfim", Message.SaveConfim);
                ClientMessage.Add("DataExist", Message.DataExist);
                ClientMessage.Add("EndDateNotNull", Message.EndDateNotNull);
                ClientMessage.Add("StartDateNotNull", Message.StartDateNotNull);
                ClientMessage.Add("EndLaterThanStart", Message.EndLaterThanStart);
                ClientMessage.Add("RolesCodeNotNull", Message.RolesCodeNotNull);
                ClientMessage.Add("DepCodeNotNull", Message.DepCodeNotNull);
                ClientMessage.Add("LanguageNotNull", Message.LanguageNotNull);
                ClientMessage.Add("ShiftNoNotNull", Message.ShiftNoNotNull);
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
                string clientmsg = JsSerializer.Serialize(ClientMessage);
                Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
            }
            this.txtOrgCode.Text = Orgcode;
            SetCalendar(txtStartDate, txtEndDate);
            if (!IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress; 
                txtOrgCode.Attributes.Add("disabled", "true");
                txtStartDate.Attributes.Add("readonly", "true");
                txtEndDate.Attributes.Add("readonly", "true");
                btnAdd.Attributes.Add("disabled", "true");
                btnModify.Attributes.Add("disabled", "true");
                btnCancel.Attributes.Add("disabled", "true");
                btnSave.Attributes.Add("disabled", "true");
                btnDelete.Attributes.Add("disabled", "true");
                ddlShiftNo.Attributes.Add("disabled", "true");
                if (Orgcode != "" && Orgcode != null)
                {
                    btnAdd.Attributes.Remove("disabled");
                    btnModify.Attributes.Remove("disabled");
                    btnDelete.Attributes.Remove("disabled");
                    DataTable dt = bll.SelectOrgShift(Orgcode);
                    DataTable dt_shift = bll.GetShift(Orgcode);
                    //DataTable dt_shift = bll.GetShift("B00002");
                    ddlShiftNo.DataSource = dt_shift;
                    this.ddlShiftNo.DataTextField = "ShiftDetail";
                    this.ddlShiftNo.DataValueField = "ShiftNo";
                    this.ddlShiftNo.DataBind();
                    this.ddlShiftNo.Items.Insert(0, new ListItem("", ""));
                    this.txtOrgCode.Text = Orgcode;
                    if (dt != null && dt.Rows.Count != 0)
                    {                 
                        this.txtStartDate.Text = Convert.ToDateTime(dt.Rows[0]["StartDate"].ToString()).ToString("yyyy/MM/dd");
                        this.txtEndDate.Text = Convert.ToDateTime(dt.Rows[0]["EndDate"].ToString()).ToString("yyyy/MM/dd");
                        this.UltraWebGrid.DataSource = bll.SelectOrgShift(Orgcode);
                        this.UltraWebGrid.DataBind();
                    }

                }
            }
        }

        #region  數據綁定
        private void DataBind()
        {
            this.txtOrgCode.Text = Orgcode;
            this.UltraWebGrid.DataSource = bll.SelectOrgShift(Orgcode);
            this.UltraWebGrid.DataBind();
        }

        private void DataBind(DataTable dt)
        {
            this.UltraWebGrid.DataSource = dt;
            this.UltraWebGrid.DataBind();
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
            string StartDate;
            string EndDate;
            try
            {
                 StartDate = Convert.ToDateTime(this.txtStartDate.Text).ToString("yyyy/MM/dd");
                 EndDate = Convert.ToDateTime(this.txtEndDate.Text).ToString("yyyy/MM/dd");
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.ErrDateFormat + "');", true);
                return;
            }
            DataTable dt = bll.SelectOrgShift(txtOrgCode.Text.Trim());
            if (hidOperate.Value == "add")
            {
                HiddenID.Value = "";
            }
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
                succeed = bll.UpdateOrgShift(HiddenID.Value,ddlShiftNo.SelectedValue,StartDate,EndDate,CurrentUserInfo.Personcode,logmodel);
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
                succeed = bll.AddOrgShift(Orgcode, ddlShiftNo.SelectedValue, StartDate, EndDate, CurrentUserInfo.Personcode, logmodel);
                if (succeed)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.AddSuccess + "');", true);
                    DataBind();
                    this.txtStartDate.Text = "";
                    this.txtEndDate.Text = "";
                    this.ddlShiftNo.SelectedValue = "";
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
            if (HiddenID.Value != "")
            {
                logmodel.ProcessFlag = "delete";
                bool flag = bll.DeleteOrgShift(HiddenID.Value,logmodel);
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
            if (this.ProcessFlag.Value.Equals("Add"))
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
            this.ProcessFlag.Value = "";
            this.TextBoxsReset("Cancel", true);
            this.ButtonsReset("Cancel");
            SetCalendar(txtStartDate, txtEndDate);
        }
        #endregion

        #region 私有函數

        private void ButtonsReset(string buttonText)
        {
            this.btnAdd.Enabled = true;
            this.btnModify.Enabled = true;
            this.btnDelete.Enabled = true;
            this.btnCancel.Enabled = true;
            this.btnSave.Enabled = true;
            switch (buttonText)
            {
                case "Condition":
                    this.btnAdd.Enabled = false;
                    this.btnModify.Enabled = false;
                    this.btnDelete.Enabled = false;
                    this.btnCancel.Enabled = false;
                    this.btnSave.Enabled = false;
                    break;

                case "Query":
                case "Delete":
                case "Cancel":
                case "Save":
                    this.btnSave.Enabled = false;
                    this.btnCancel.Enabled = false;
                    break;

                case "Add":
                case "Modify":
                    this.btnAdd.Enabled = false;
                    this.btnModify.Enabled = false;
                    this.btnDelete.Enabled = false;
                    break;

                case "Exist":
                    this.btnAdd.Enabled = false;
                    this.btnModify.Enabled = false;
                    this.btnCancel.Enabled = false;
                    this.btnSave.Enabled = false;
                    break;
            }
        }



        private void TextBoxsReset(string buttonText, bool read)
        {
            this.txtStartDate.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtEndDate.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            if (((buttonText.ToLower() == "add") || (buttonText.ToLower() == "modify")) && (buttonText.ToLower() == "add"))
            {
                this.txtStartDate.Text = "";
                this.txtEndDate.Text = "";
                this.ddlShiftNo.SelectedValue = "";
                this.HiddenShiftNo.Value = "";
                this.HiddenID.Value = "";
            }
        }

        #endregion
    }
}
