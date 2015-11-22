/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： HrmTWCadreForm
 * 檔功能描述： 駐派幹部資料新增編輯UI
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.16
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.HRM;
using GDSBG.MiABU.Attendance.BLL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.HRM;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using Resources;

namespace GDSBG.MiABU.Attendance.Web.HRM
{
    public partial class HrmTWCadreEditForm : BasePage
    {
        TypeDataBll bllTypeData = new TypeDataBll();
        TWCadreBll tWCadreBll = new TWCadreBll();
        EmployeeBll employeeBll = new EmployeeBll();
        TWCadreModel model = new TWCadreModel();
        static SynclogModel logmodel = new SynclogModel();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;

        #region 頁面加載
        protected void Page_Load(object sender, EventArgs e)
        {
            SetCalendar(txtJoinDate, txtLeaveDate);
            if (!base.IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;

                DropDownListBind();
                string processFlag = base.Request.QueryString["ProcessFlag"];
                this.ProcessFlag.Value = processFlag;
                this.ModuleCode.Value = base.Request.QueryString["ModuleCode"];
                string WorkNo = base.Request.QueryString["WorkNo"];
                if (processFlag == "Modify")
                {
                    this.lblWorkNo.Attributes.Add("readonly", "true");
                    model.WorkNo = WorkNo;
                    TWCadreModel newmodel = tWCadreBll.GetTWCadreInfoByKey(model);
                    PageHelper.BindControls<TWCadreModel>(newmodel, pnlContent.Controls);
                }

            }
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("WrongDate", Message.WrongDate);
                ClientMessage.Add("IsKaoQinNotNull", Message.IsKaoQinNotNull);
                ClientMessage.Add("DepNameNotNull", Message.DepNameNotNull);
                ClientMessage.Add("LocalNameNotNull", Message.LocalNameNotNull);
                ClientMessage.Add("WorkNoNotNull", Message.WorkNoNotNull);
                ClientMessage.Add("NotOnlyOne", Message.NotOnlyOne);
                ClientMessage.Add("EmpNotExist", Message.EmpNotExist);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
        }
        #endregion

        #region 下拉菜單數據綁定
        protected void DropDownListBind()
        {
            DataTable dt = tWCadreBll.GetLevel();
            this.ddlLevelCode.DataSource = dt.DefaultView;
            this.ddlLevelCode.DataTextField = "LevelName";
            this.ddlLevelCode.DataValueField = "LevelCode";
            this.ddlLevelCode.DataBind();
            this.ddlLevelCode.Items.Insert(0, new ListItem("", ""));
            dt.Clear();
            dt = tWCadreBll.GetManager();
            this.ddlManagerCode.DataSource = dt.DefaultView;
            this.ddlManagerCode.DataTextField = "ManagerName";
            this.ddlManagerCode.DataValueField = "ManagerCode";
            this.ddlManagerCode.DataBind();
            this.ddlManagerCode.Items.Insert(0, new ListItem("", ""));
            dt.Clear();
            dt = tWCadreBll.GetEmpStatus();
            //dt = bllTypeData.GetDataTypeList("EmpState");
            this.ddlStatus.DataSource = dt.DefaultView;
            this.ddlStatus.DataTextField = "StatusName";//"datavalue";
            this.ddlStatus.DataValueField = "StatusCode";//"datacode";
            this.ddlStatus.DataBind();
            dt.Clear();
        }
        #endregion

        #region ajax驗證
        /// <summary>
        /// ajax驗證
        /// </summary>
        protected override void AjaxProcess()
        {
            if (!string.IsNullOrEmpty(Request.Form["WorkNo"]))
            {
                int msg = 0;
                model.WorkNo = Request.Form["WorkNo"];
                DataTable dt = tWCadreBll.GetTWCafreByKey(model);
                if (dt.Rows.Count > 0)
                {
                    msg = 1;
                }
                Response.Clear();
                Response.Write(msg.ToString());
                Response.End();
            }
        }
        #endregion

        #region 儲存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string alert = "";
            model = PageHelper.GetModel<TWCadreModel>(pnlContent.Controls);
            if (this.ProcessFlag.Value == "Add")
            {
                DataTable dt = tWCadreBll.GetTWCafreByKey(model);
                if (dt.Rows.Count > 0)
                {
                    alert = "alert('" + Message.NotOnlyOne + "')";
                }
                logmodel.ProcessFlag = "insert";
                model.CreateUser = base.CurrentUserInfo.Personcode;
                model.CreateDate = System.DateTime.Now;
                bool flag = tWCadreBll.AddTWCdare(model, logmodel);
                if (flag == true)
                {
                    alert = "alert('" + Message.AddSuccess + "')";
                }
                else
                {
                    alert = "alert('" + Message.AddFailed + "')";
                }
                PageHelper.CleanControlsValue(pnlContent.Controls);
            }
            if (this.ProcessFlag.Value == "Modify")
            {
                logmodel.ProcessFlag = "update";
                model.UpdateDate = System.DateTime.Now;
                model.UpdateUser = base.CurrentUserInfo.Personcode;
                bool flag = tWCadreBll.UpdateTWCdareByKey(model, logmodel);
                if (flag == true)
                {
                    alert = "alert('" + Message.UpdateSuccess + "')";
                }
                else
                {
                    alert = "alert('" + Message.UpdateFailed + "')";
                }
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "save", alert, true);
        }
        #endregion
    }
}
