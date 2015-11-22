/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： HrmDepartmentEditForm
 * 檔功能描述： 組織擴建編輯UI
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2012.01.02
 * 
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.HRM;
using GDSBG.MiABU.Attendance.Model.HRM;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using Resources;

namespace GDSBG.MiABU.Attendance.Web.HRM
{
    public partial class HrmDepartmentEditForm : BasePage
    {
        DepartmentModel model = new DepartmentModel();
        static SynclogModel logmodel = new SynclogModel();
        HrmDepartmentBll bllHrmDepartment = new HrmDepartmentBll();
        DataTable dt = new DataTable();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        #region 頁面加載
        protected void Page_Load(object sender, EventArgs e)
        {
            PageHelper.ButtonControls(FuncList, pnlShowPanel.Controls, base.FuncListModule);
            if (!base.Page.IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;

                this.TextBoxsReset("", true);
                this.ButtonsReset("Condition");
                string DepCode = (base.Request.QueryString["DepCode"] == null) ? "" : base.Request.QueryString["DepCode"].ToString();
                if (DepCode.ToString().Length != 0)
                {
                    this.Query(DepCode);
                }
            }
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("DepNameNotNull", Message.DepNameNotNull);
                ClientMessage.Add("OrderIdNotNull", Message.OrderIdNotNull);
                ClientMessage.Add("ConfirmBatchConfirm", Message.ConfirmBatchConfirm);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
        }
        #endregion

        #region 新增
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string orderid = this.HiddenNextLevelCode.Value.ToString();
            dt = bllHrmDepartment.GetHead(orderid);
            if (dt.Rows.Count == 0)
            {
                base.Response.Write("<script type='text/javascript'>alert(\"" + Message.operatedepartmentext + "\");</script>");
                return;
            }
            string head = dt.Rows[0][0].ToString();
            string levelname = dt.Rows[0][1].ToString();
            if (!head.Equals("-"))
            {
                string alert = "alert('" + Message.operatedepartmentext + "')";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "operatedepartmen", alert, true);
            }
            else
            {
                this.HiddenMaxDepCode.Value = this.HiddenDepCode.Value.Substring(0, 6);
                this.ProcessFlag.Value = "Add";
                this.TextBoxsReset("Add", false);
                this.ButtonsReset("Add");
                this.txtLevelName.Text = levelname;
            }
        }
        #endregion

        #region 取消
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.Query(this.HiddenDepCode.Value);
        }
        #endregion

        #region 組織異動
        protected void btnChange_Click(object sender, EventArgs e)
        {
            if (this.div_change.Visible)
            {
                this.div_change.Visible = false;
            }
            else
            {
                this.div_change.Visible = true;
            }
        }
        #endregion

        #region 組織異動存儲
        protected void btnChangedSave_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "update";
            string alert = "";
            dt.Clear();
            string depcode = this.HiddenDepCode.Value.ToString();
            dt = bllHrmDepartment.GetWorkNoByDepCode(1, depcode);
            if (this.dt.Rows.Count > 0)
            {
                alert = "alert('" + Message.extdepartmentdelete + "'+'" + this.dt.Rows[0]["WorkNo"].ToString() + "')";
            }
            else
            {
                string newdepname = this.txtNewDepName.Text.Trim();
                string sql = base.SqlDep;
                dt.Clear();
                dt = bllHrmDepartment.GetDept(newdepname,sql);
                if (this.dt.Rows.Count == 0)
                {
                    alert = "alert('" + Message.DepartmentNotExist + "')";
                }
                else if (!this.HiddenParentDepCode.Value.Equals(this.txtNewDepName.Text))
                {
                    bool flag = bllHrmDepartment.ChangeSave(newdepname, depcode,logmodel);
                    if (flag == true)
                    {
                        base.Response.Write("<script type='text/javascript'>window.parent.document.all.HiddenDelete.click();</script>");
                        this.TextBoxsReset("", true);
                        this.ButtonsReset("Condition");
                        this.HiddenDepCode.Value = "";
                        this.HiddenLevelCode.Value = "";
                        this.HiddenNextLevelCode.Value = "";
                        this.txtDepName.Text = "";
                        this.txtOrderID.Text = "";
                        this.txtLevelName.Text = "";
                        this.ProcessFlag.Value = "";
                        this.div_change.Visible = false;
                        base.Response.Write("<script type='text/javascript'>alert(\"" + Message.DataSaveSuccess + "\");window.parent.document.all.HiddenReload.click();</script>");
                        return;
                    }
                    else
                    {
                        alert = "alert('" + Message.DataSaveFailed + "')";
                    }
                }
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ChangeSave", alert, true);
        }
        #endregion

        #region 刪除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "delete";
            string alert = "";
            string HiddenHead = this.HiddenHead.Value;
            string depcode = this.HiddenDepCode.Value;
            string companyid = CurrentUserInfo.CompanyId.ToString();
            if ((HiddenHead == null) || (HiddenHead != "-"))
            {
                alert = "alert('" + Message.operatedepartmentext + "')";
            }
            else
            {
                dt.Clear();

                dt = bllHrmDepartment.GetDeptByParentdepcode(false, depcode);
                if (this.dt.Rows.Count > 1)
                {
                    alert = "alert('" + Message.operatedepartmentdelete + "')";
                }
                else
                {
                    dt.Clear();
                    dt = bllHrmDepartment.GetWorkNoByDepCode(0, depcode);
                    if (this.dt.Rows.Count > 0)
                    {
                        alert = "alert('" + Message.extdepartmentdelete + "'+'" + this.dt.Rows[0]["WorkNo"].ToString() + "')";
                    }
                    else
                    {
                        dt.Clear();
                        dt = bllHrmDepartment.GetDepartmentDetailData("condition", companyid, depcode);
                        if (this.dt.Rows.Count > 0)
                        {
                            int num = bllHrmDepartment.DeleteDepartment(companyid, depcode,logmodel);
                            if (num == 0)
                            {
                                alert = "alert('" + Message.CannotDelete + "')";
                            }
                            if (num == 1)
                            {
                                base.Response.Write("<script type='text/javascript'>window.parent.document.all.HiddenDelete.click();</script>");
                                this.TextBoxsReset("", true);
                                this.ButtonsReset("Condition");
                                this.HiddenDepCode.Value = "";
                                this.HiddenLevelCode.Value = "";
                                this.HiddenNextLevelCode.Value = "";
                                this.txtDepName.Text = "";
                                this.txtOrderID.Text = "";
                                this.txtLevelName.Text = "";
                                this.ProcessFlag.Value = "";
                                alert = "alert('" + Message.DeleteSuccess + "')";
                            }
                            if (num == 2)
                            {
                                alert = "alert('" + Message.DeleteFailed + "')";
                            }
                        }
                        else
                        {
                            alert = "alert('" + Message.NoItemSelected + "')";
                        }
                    }
                }
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "delete", alert, true);
        }
        #endregion

        #region 失效
        protected void btnDisable_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "update";
            string alert = "";
            string HiddenHead = this.HiddenHead.Value;
            string depcode = this.HiddenDepCode.Value;
            string companyid = CurrentUserInfo.CompanyId.ToString();
            if ((HiddenHead == null) || (HiddenHead != "-"))
            {
                alert = "alert('" + Message.operatedepartmentext + "')";
            }
            else
            {
                dt.Clear();
                dt = bllHrmDepartment.GetDeptByParentdepcode(true, depcode);
                if (this.dt.Rows.Count > 1)
                {
                    alert = "alert('" + Message.operatedepartmentdelete + "')";
                }
                else
                {
                    dt.Clear();
                    dt = bllHrmDepartment.GetWorkNoByDepCode(2, depcode);
                    if (this.dt.Rows.Count > 0)
                    {
                        alert = "alert('" + Message.extdepartmentdelete + "'+'" + this.dt.Rows[0]["WorkNo"].ToString() + "')";
                    }
                    else
                    {
                        dt.Clear();
                        dt = bllHrmDepartment.GetDepartmentDetailData("condition", companyid, depcode);
                        if (this.dt.Rows.Count > 0)
                        {
                            int num = bllHrmDepartment.DisableOrEnable(companyid, depcode, "Y",logmodel);
                            if (num == 0)
                            {
                                alert = "alert('" + Message.extdepartmentdelete + "')";
                            }
                            if (num == 1)
                            {
                                alert = "alert('" + Message.DisableSuccess + "')";
                                base.Response.Write("<script type='text/javascript'>window.parent.document.all.HiddenEnabled.value='false';window.parent.document.all.HiddenEnabled.click();</script>");
                            }
                            if (num == 2)
                            {
                                alert = "alert('" + Message.DisableFailed + "')";
                            }
                        }
                        else
                        {
                            alert = "alert('" + Message.NoItemSelected + "')";
                        }
                    }
                }
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "disable", alert, true);
        }
        #endregion

        #region 生效
        protected void btnEnable_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "update";
            string alert = "";
            string HiddenHead = this.HiddenHead.Value;
            if ((HiddenHead == null) || (HiddenHead != "-"))
            {
                alert = "alert('" + Message.operatedepartmentext + "')";

            }
            else
            {
                string condition = "condition";
                string companyid = CurrentUserInfo.CompanyId.ToString();
                string depcode = this.HiddenDepCode.Value.ToString();
                dt = bllHrmDepartment.GetDepartmentDetailData(condition, companyid, depcode);
                if (this.dt.Rows.Count > 0)
                {
                    int num = bllHrmDepartment.DisableOrEnable(companyid, depcode, "N",logmodel);
                    if (num == 1)
                    {
                        alert = "alert('" + Message.EnableSuccess + "')";
                        base.Response.Write("<script type='text/javascript'>window.parent.document.all.HiddenEnabled.value='true';window.parent.document.all.HiddenEnabled.click();</script>");
                    }
                    else
                    {
                        alert = "alert('" + Message.EnableFailed + "')";
                    }
                }
                else
                {
                    alert = "alert('" + Message.NoItemSelected + "')";
                }
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "enable", alert, true);
        }
        #endregion

        #region 修改
        protected void btnModify_Click(object sender, EventArgs e)
        {
            string hiddenHead = this.HiddenHead.Value;
            if ((hiddenHead == null) || (hiddenHead != "-"))
            {
                string alert = "alert('" + Message.operatedepartmentext + "')";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "operatedepartmen", alert, true);
            }
            else
            {
                this.ProcessFlag.Value = "Modify";
                this.TextBoxsReset("Modify", false);
                this.ButtonsReset("Modify");
            }
        }
        #endregion

        #region 存儲
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtDepName.Text) && !string.IsNullOrEmpty(this.txtOrderID.Text))
            {
                dt.Clear();
                string condition = "";
                string companyid = CurrentUserInfo.CompanyId;
                string depcode = this.HiddenDepCode.Value;
                string hiddenlevelcode = this.HiddenNextLevelCode.Value;
                string alert = "";
                dt = bllHrmDepartment.GetDepartmentDetailData(condition, companyid, depcode);
                if (this.ProcessFlag.Value.Equals("Add"))
                {
                    if (this.dt.Rows.Count > 0)
                    {
                        alert = "alert('" + Message.NotOnlyOne + "')";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "save", alert, true);
                        return;
                    }
                    string maxdepcode = this.HiddenMaxDepCode.Value;
                    string DepCode = bllHrmDepartment.GetMaxDepCode(maxdepcode);
                    if (DepCode == "")
                    {
                        alert = "alert('" + Message.DepCodeAutoError + "')";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "save", alert, true);
                        return;
                    }
                    string LevelCode = bllHrmDepartment.GetHead(hiddenlevelcode).Rows[0]["levelcode"].ToString();
                    model = PageHelper.GetModel<DepartmentModel>(pnlContent.Controls);
                    model.OrderId = this.txtOrderID.Text.Trim();
                    model.CompanyId = companyid;
                    model.DepCode = DepCode;
                    model.LevelCode = LevelCode;
                    model.ParentDepCode = depcode;
                    model.CreateDate = System.DateTime.Now;
                    model.CreateUser = base.CurrentUserInfo.Personcode;
                    logmodel.ProcessFlag = "insert";
                    int num = bllHrmDepartment.SaveDepartment("Add", model,logmodel);
                    if (num == 1)
                    {
                        base.Response.Write("<script type='text/javascript'>window.parent.document.all.HiddenAdd.value='" + this.txtDepName.Text.Trim() + "';window.parent.document.all.HiddenAddDepCode.value='" + DepCode + "';window.parent.document.all.HiddenAdd.click();</script>");
                        this.TextBoxsReset("", true);
                        this.ButtonsReset("Condition");
                        this.HiddenDepCode.Value = "";
                        this.HiddenLevelCode.Value = "";
                        this.HiddenNextLevelCode.Value = "";
                        this.txtDepName.Text = "";
                        this.txtOrderID.Text = "";
                        this.txtLevelName.Text = "";
                        this.ProcessFlag.Value = "";
                        alert = "alert('" + Message.SaveSuccess + "')";
                    }
                    else
                    {
                        alert = "alert('" + Message.SaveFailed + "')";
                    }
                }
                else if (this.ProcessFlag.Value.Equals("Modify"))
                {
                    dt.Clear();
                    dt = bllHrmDepartment.GetDepartmentDetailData("condition", companyid, depcode);
                    if (this.dt.Rows.Count == 0)
                    {
                        alert = "alert('" + Message.NoItemSelected + "')";
                        return;
                    }
                    model = PageHelper.GetModel<DepartmentModel>(pnlContent.Controls);
                    model.DepCode = this.HiddenDepCode.Value;
                    model.CompanyId = dt.Rows[0]["companyid"].ToString();
                    model.OrderId = this.txtOrderID.Text.Trim();
                    model.UpdateDate = System.DateTime.Now;
                    model.UpdateUser = base.CurrentUserInfo.Personcode;
                    logmodel.ProcessFlag = "update";
                    int num = bllHrmDepartment.SaveDepartment("Modify", model,logmodel);
                    if (num == 1)
                    {
                        base.Response.Write("<script type='text/javascript'>window.parent.document.all.HiddenChange.value='" + this.txtDepName.Text.Trim() + "';window.parent.document.all.HiddenChange.click();</script>");
                        this.Query(this.HiddenDepCode.Value);
                        alert = "alert('" + Message.UpdateSuccess + "')";
                    }
                    else
                    {
                        alert = "alert('" + Message.UpdateFailed + "')";
                    }
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "operatedepartmen", alert, true);
            }
        }
        #endregion

        #region button屬性
        private void ButtonsReset(string buttonText)
        {
            this.btnAdd.Enabled = true;
            this.btnModify.Enabled = true;
            this.btnDelete.Enabled = true;
            this.btnCancel.Enabled = true;
            this.btnSave.Enabled = true;
            this.btnEnable.Enabled = true;
            this.btnDisable.Enabled = true;
            switch (buttonText)
            {
                case "Condition":
                    this.btnAdd.Enabled = false;
                    this.btnModify.Enabled = false;
                    this.btnDelete.Enabled = false;
                    this.btnSave.Enabled = false;
                    this.btnCancel.Enabled = false;
                    this.btnEnable.Enabled = false;
                    this.btnDisable.Enabled = false;
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
                    this.btnEnable.Enabled = false;
                    this.btnDisable.Enabled = false;
                    break;
            }
        }
        #endregion

        #region 查詢方法
        private void Query(string DepCode)
        {
            string companyid = CurrentUserInfo.CompanyId.ToString();
            dt.Clear();
            dt = bllHrmDepartment.GetDepartment(companyid, DepCode);
            if (this.dt.Rows.Count > 0)
            {
                this.txtDepName.Text = this.dt.Rows[0]["depname"].ToString();
                this.txtOrderID.Text = this.dt.Rows[0]["orderid"].ToString();
                this.HiddenLevelCode.Value = this.dt.Rows[0]["DepLevel"].ToString();
                this.txtLevelName.Text = this.dt.Rows[0]["LevelName"].ToString();
                this.HiddenParentDepCode.Value = this.dt.Rows[0]["ParentDepCode"].ToString();
                this.HiddenHead.Value = this.dt.Rows[0]["Head"].ToString();
                if (!this.dt.Rows[0]["levelcode"].ToString().Equals("4"))
                {
                    this.btnChange.Enabled = false;
                }
                else
                {
                    this.btnChange.Enabled = true;
                }
                this.HiddenDepCode.Value = DepCode;
                try
                {
                    this.HiddenNextLevelCode.Value = Convert.ToString((int)(Convert.ToInt32(this.dt.Rows[0]["DepLevel"].ToString()) + 1));
                }
                catch (Exception)
                {
                    this.HiddenNextLevelCode.Value = "";
                }
            }
            else
            {
                string alert = "alert('" + Message.NoItemSelected + "')";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "query", alert, true);
                return;
            }
            this.ButtonsReset("Query");
            this.TextBoxsReset("Query", true);
            this.ProcessFlag.Value = "";
        }
        #endregion

        #region 重置
        private void TextBoxsReset(string buttonText, bool read)
        {
            this.txtDepName.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtOrderID.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtLevelName.BorderStyle = BorderStyle.None;
            if (((buttonText.ToLower() == "add") || (buttonText.ToLower() == "modify")) && (buttonText.ToLower() == "add"))
            {
                this.txtDepName.Text = "";
                this.txtOrderID.Text = "";
                this.txtLevelName.Text = "";
            }
        }
        #endregion
    }
}
