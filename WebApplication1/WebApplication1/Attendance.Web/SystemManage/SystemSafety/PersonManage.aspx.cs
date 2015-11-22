using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using Resources;

/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： PersonManage.aspx.cs
 * 檔功能描述： 用戶資料
 * 
 * 版本：1.0
 * 創建標識： 昝望 2011.12.21
 * 
 */

namespace GDSBG.MiABU.Attendance.Web.SystemManage.SystemSafety
{
    public partial class PersonManage : BasePage
    {
        #region
        //protected eBFW.ControlLib.Title Title;
        protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGridPerson;
        protected Infragistics.WebUI.UltraWebTab.UltraWebTab UltraWebTab;
        //protected UserLabel UserLabelTel;
        #endregion
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        int totalCount;
        static DataTable dt_global = new DataTable();
        static SynclogModel logmodel = new SynclogModel();
        static PersonModel model_global = new PersonModel();

        #region 頁面加載
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["personcode"] = "";
            pager.CurrentPageIndex = 1;
            HiddenModuleCode.Value = Request.QueryString["ModuleCode"].ToString();
            //PageHelper.ButtonControls(base.FuncList, pnlShowPanel.Controls, base.FuncListModule);
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("DeleteConfirm", Message.DeleteConfirm);
                ClientMessage.Add("SaveConfim", Message.SaveConfim);
                ClientMessage.Add("DataExist", Message.DataExist);
                ClientMessage.Add("PersoncodeNotNull", Message.PersoncodeNotNull);
                ClientMessage.Add("CnameNotNull", Message.CnameNotNull);
                ClientMessage.Add("CompanyIdNotNull", Message.CompanyIdNotNull);
                ClientMessage.Add("RolesCodeNotNull", Message.RolesCodeNotNull);
                ClientMessage.Add("DepCodeNotNull", Message.DepCodeNotNull);
                ClientMessage.Add("LanguageNotNull", Message.LanguageNotNull);
                ClientMessage.Add("IfAdminNotNull", Message.IfAdminNotNull);
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
            if (!IsPostBack)
            {
                HiddenPersonCode.Value = CurrentUserInfo.Personcode;
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;
                PersonBll bll = new PersonBll();
                PersonModel model = new PersonModel();
                DataTable tempTable = bll.GetPerson(model, pager.CurrentPageIndex, pager.PageSize, out totalCount, base.SqlDep);
                pager.RecordCount = totalCount;
                dt_global = bll.GetPerson(model, base.SqlDep);
                pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
                DataBind(tempTable);
                SetSelector(imgCompanyId, txtCompanyId, txtCompanyName, "CompanyId", Request.QueryString["ModuleCode"].ToString());
                SetSelector(imgRolCode, txtRoleCode, txtRolesName, "RoleCode", Request.QueryString["ModuleCode"].ToString());
                SetSelector(imgDepLevel, txtDepLevel, txtLevelName, "DepLevel", Request.QueryString["ModuleCode"].ToString());
                SetSelector(imgLanguage, txtLanguage, txtLanguageName, "Language", Request.QueryString["ModuleCode"].ToString());
                SetSelector(imgAdmini, txtIfAdmin, txtAdminName, "IfAdmin", Request.QueryString["ModuleCode"].ToString());
                SetSelector(imgDePCode, txtDepCode, txtDepName, "DepCode", Request.QueryString["ModuleCode"].ToString());

                SetSelector(imgUserChangePwd, txtUserChangePwd, txtUserChangePwdName, "UserChangePwd", Request.QueryString["ModuleCode"].ToString());
                SetSelector(imgUserStartPwd, txtUserStartPwd, txtUserStartPwdName, "UserStartPwd", Request.QueryString["ModuleCode"].ToString());
                SetSelector(imegSalaryLogonEnable, txtSalaryLogonEnable, txtSalaryLogonEnableName, "SalaryLogonEnable", Request.QueryString["ModuleCode"].ToString());
                SetSelector(imgIpControlFlag, txtIpControlFlag, txtIpControlFlagName, "IpControlFlag", Request.QueryString["ModuleCode"].ToString());
                SetSelector(imgEmpType, txtEmpType, txtEmpTypeName, "EmpType", Request.QueryString["ModuleCode"].ToString());
            }
        }
        #endregion

        #region 綁定數據
        /// <summary>
        /// 綁定數據
        /// </summary>

        private void DataBind()
        {
            //txtPersoncode.ReadOnly = true;
            txtPersoncode.Attributes.Add("readonly", "true");
            //txtCname.ReadOnly = true;
            txtCname.Attributes.Add("readonly", "true");
            //txtCompanyId.ReadOnly = true;
            txtCompanyId.Attributes.Add("readonly", "true");
            //txtDepCode.ReadOnly = true;
            txtDepCode.Attributes.Add("readonly", "true");
            //txtRoleCode.ReadOnly = true;
            txtRoleCode.Attributes.Add("readonly", "true");
            //txtLanguage.ReadOnly = true;
            txtLanguage.Attributes.Add("readonly", "true");
            //txtDepLevel.ReadOnly = true;
            txtDepLevel.Attributes.Add("readonly", "true");
            //txtIfAdmin.ReadOnly = true;
            txtIfAdmin.Attributes.Add("readonly", "true");
            //txtTel.ReadOnly = true;
            txtTel.Attributes.Add("readonly", "true");
            //txtMail.ReadOnly = true;
            txtMail.Attributes.Add("readonly", "true");
            btnSave.Attributes.Add("disabled", "true");
            btnCancel.Attributes.Add("disabled", "true");
            PersonBll bll = new PersonBll();
            PersonModel model = new PersonModel();
            DataTable dt = bll.GetPerson(model, pager.CurrentPageIndex, pager.PageSize, out totalCount, base.SqlDep);
            pager.RecordCount = totalCount;
            dt_global = bll.GetPerson(model, base.SqlDep); ;
            this.UltraWebGridPerson.DataSource = dt;
            this.UltraWebGridPerson.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();

        }

        private void DataBind(DataTable dt)
        {
            //txtPersoncode.ReadOnly = true;
            txtPersoncode.Attributes.Add("readonly", "true");
            //txtCname.ReadOnly = true;
            txtCname.Attributes.Add("readonly", "true");
            //txtCompanyId.ReadOnly = true;
            txtCompanyId.Attributes.Add("readonly", "true");
            //txtDepCode.ReadOnly = true;
            txtDepCode.Attributes.Add("readonly", "true");
            //txtRoleCode.ReadOnly = true;
            txtRoleCode.Attributes.Add("readonly", "true");
            //txtLanguage.ReadOnly = true;
            txtLanguage.Attributes.Add("readonly", "true");
            //txtDepLevel.ReadOnly = true;
            txtDepLevel.Attributes.Add("readonly", "true");
            //txtIfAdmin.ReadOnly = true;
            txtIfAdmin.Attributes.Add("readonly", "true");
            //txtTel.ReadOnly = true;
            txtTel.Attributes.Add("readonly", "true");
            //txtMail.ReadOnly = true;
            txtMail.Attributes.Add("readonly", "true");
            btnSave.Attributes.Add("disabled", "true");
            btnCancel.Attributes.Add("disabled", "true");
            this.UltraWebGridPerson.DataSource = dt;
            this.UltraWebGridPerson.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();

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
            PersonModel model = GetModel();
            PersonBll bll = new PersonBll();
            bool succeed;
            if (hidOperate.Value == "modify")
            {
                logmodel.ProcessFlag = "update";
                succeed = bll.UpdatePersonByKey(model, logmodel);
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
                //model.Passwd = "Foxconn88";
                logmodel.ProcessFlag = "insert";
                succeed = bll.AddPerson(model, logmodel);
                succeed = bll.UpdatePWDByKey(model.Personcode, "Foxconn88", logmodel);
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



        #region 驗證工號是否重複
        protected override void AjaxProcess()
        {
            bool flag = false;
            string noticeJson = null;
            int num = 0;
            PersonModel model = new PersonModel();
            if (!string.IsNullOrEmpty(Request.Form["Empno"]))
            {
                PersonBll bll = new PersonBll();
                string empno = Request.Form["Empno"];

                model.Personcode = empno;
                model.Num = bll.GetCount(model);
                DataTable dt = bll.GetEmployeeInfo(model.Personcode);
                if (dt != null && dt.Rows.Count != 0)
                {
                    model.Cname = dt.Rows[0]["localname"].ToString();
                }
                flag = true;
            }
            //if (!string.IsNullOrEmpty(Request.Form["Personcode"]))
            //{
            //    string empno = Request.Form["Personcode"];
            //    Session["personcode"] = empno;
            //}
            if (model != null)
            {
                noticeJson = JsSerializer.Serialize(model);
            }
            if (flag)
            {
                Response.Clear();
                Response.Write(noticeJson);
                Response.End();
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
            PersonModel model = GetModel();
            PersonBll bll = new PersonBll();
            if (model.Personcode != null)
            {
                int flag = bll.DeletePersonByKey(model.Personcode, logmodel);
                if (flag == 1)
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

        #region 重置密碼
        /// <summary>
        /// 重置密碼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnResetPWD_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "update";
            PersonModel model = GetModel();
            PersonBll bll = new PersonBll();
            bool succeed = false;
            if (model.Personcode != null)
            {
                model.Passwd = "Foxconn88";
                //succeed = bll.UpdatePersonByKey(model);
                succeed = bll.UpdatePWDByKey(model.Personcode, "Foxconn88", logmodel);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.AtLastOneChoose + "');", true);
            }
            if (succeed)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.ResetPasswordSuccess + "');", true);
                DataBind();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.ResetPasswordFailed + "');", true);
            }

        }
        #endregion

        #region 失效
        /// <summary>
        /// 失效
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDisable_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "update";
            PersonModel model = GetModel();
            PersonBll bll = new PersonBll();
            bool succeed = false;
            if (model.Personcode != null)
            {
                model.Deleted = "Y";
                succeed = bll.UpdatePersonByKey(model, logmodel);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.AtLastOneChoose + "');", true);
            }
            if (succeed)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.DisableSuccess + "');", true);
                DataBind();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.DisableFailed + "');", true);
            }
        }

        #endregion

        #region 生效

        protected void btnEnable_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "update";
            PersonModel model = GetModel();
            PersonBll bll = new PersonBll();
            bool succeed = false;
            if (model.Personcode != null)
            {
                model.Deleted = "N";
                succeed = bll.UpdatePersonByKey(model, logmodel);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.AtLastOneChoose + "');", true);
            }
            if (succeed)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.EnableSuccess + "');", true);
                DataBind();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.EnableFailed + "');", true);
            }

        }

        #endregion


        #region UltraWebGrid的DataBound事件
        /// <summary>
        /// UltraWebGrid的DataBound事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UltraWebGrid_DataBound(object sender, EventArgs e)
        {
            for (int i = 0; i < UltraWebGridPerson.Rows.Count; i++)
            {
                if (UltraWebGridPerson.Rows[i].Cells.FromKey("DELETED").Text.Trim() == "Y")
                {
                    for (int j = 0; j < UltraWebGridPerson.Columns.Count; j++)
                    {
                        UltraWebGridPerson.Rows[i].Cells[j].Style.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }
        #endregion

        #region 查詢
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            pager.CurrentPageIndex = 1;
            if (hidOperate.Value == "condition")
            {
                model_global = GetModel();
                PersonBll bll = new PersonBll();
                DataTable dt = bll.GetPerson(model_global, pager.CurrentPageIndex, pager.PageSize, out totalCount, base.SqlDep);
                pager.RecordCount = totalCount;
                dt_global = bll.GetPerson(model_global, base.SqlDep); ;
                DataBind(dt);
            }
        }
        #endregion

        #region 導出報表
        protected void btnExport_Click(object sender, EventArgs e)
        {
            PersonModel model = PageHelper.GetModel<PersonModel>(pnlContent.Controls);
            PersonBll bll = new PersonBll();
            List<PersonModel> list = bll.GetList(dt_global);
            string[] header = { ControlText.gvHeadPersonCode, ControlText.gvHeadCname, ControlText.gvHeadCompanyName, ControlText.gvHeadDepName, ControlText.gvHeadRoleName, ControlText.gvHeadAdmin, ControlText.gvHeadLanguageName, ControlText.gvHeadLevelName, ControlText.gvHeadLoginTimes, ControlText.gvHeadLoginTime, ControlText.gvHeadTel, ControlText.gvHeadMail, ControlText.gvHeadDeleted, ControlText.gvHeadModifier, ControlText.gvHeadModifyDate };
            string[] properties = { "Personcode", "Cname", "CompanyName", "DepName", "RoleName", "IfAdmin", "LanguageName", "LevelName", "LoginTimes", "LoginTime", "Tel", "Mail", "Deleted", "UpdateUser", "UpdateDate" };
            string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
            NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
            PageHelper.ReturnHTTPStream(filePath, true);

        }
        #endregion

        #region  獲得頁面TEXTBOX的值
        private PersonModel GetModel()
        {
            PersonModel model = PageHelper.GetModel<PersonModel>(pnlContent.Controls);
            return model;
        }
        #endregion

        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            if (hidOperate.Value == "condition")
            {
                PersonBll bll = new PersonBll();
                DataTable dt = bll.GetPerson(model_global, pager.CurrentPageIndex, pager.PageSize, out totalCount, base.SqlDep);
                pager.RecordCount = totalCount;
                dt_global = bll.GetPerson(model_global, base.SqlDep); ;
                DataBind(dt);
            }
            else
            {
                PersonBll bll = new PersonBll();
                PersonModel model = new PersonModel();
                DataTable dt = bll.GetPerson(model, pager.CurrentPageIndex, pager.PageSize, out totalCount, base.SqlDep);
                pager.RecordCount = totalCount;
                dt_global = bll.GetPerson(model, base.SqlDep); ;
                DataBind(dt);
            }
        }
        #endregion

        #region 設置Selector
        /// <summary>
        /// 設置Selector
        /// </summary>
        /// <param name="ctrlTrigger">控件ID--按鈕</param>
        /// <param name="ctrlCode">控件ID--文本框1</param>
        /// <param name="ctrlName">控件ID--文本框2</param>
        /// <param name="flag">Selector區分標誌</param>
        public static void SetSelector(WebControl ctrlTrigger, Control ctrlCode, Control ctrlName, string flag, string moduleCode)
        {
            if (ctrlCode is TextBox) { (ctrlCode as TextBox).Attributes.Add("readonly", "readonly"); }
            if (ctrlName is TextBox) { (ctrlName as TextBox).Attributes.Add("readonly", "readonly"); }
            ctrlTrigger.Attributes.Add("onclick", string.Format("return setSelector('{0}','{1}','{2}','{3}')",
                ctrlCode.ClientID, ctrlName.ClientID, flag, moduleCode));
        }

        #endregion
    }
}
