/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： RoleBll.cs
 * 檔功能描述： 角色設定UI類
 * 
 * 版本：1.0
 * 創建標識： 陳函 2011.11.30
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Web.UI;
using GDSBG.MiABU.Attendance.BLL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using Resources;
namespace GDSBG.MiABU.Attendance.Web.SystemManage.SystemSafety
{
    public partial class RoleCode : BasePage
    {
        RoleBll roleBll = new RoleBll();
        AuthorityBll authorityBll = new AuthorityBll();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        static RoleModel model;
        Dictionary<string, string> ClientMessage = null;
        static SynclogModel logmodel = new SynclogModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            PageHelper.ButtonControls(FuncList, pnlShowPanel.Controls, base.FuncListModule);
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("RoleCodeNotNull", Message.RoleCodeNotNull);
                ClientMessage.Add("RoleExist", Message.RoleExist);
                ClientMessage.Add("RoleDeleteConfirm", Message.RoleDeleteConfirm);
                ClientMessage.Add("RoleDeleteCheck", Message.RoleDeleteCheck);
                ClientMessage.Add("ConfirmDisable", Message.ConfirmDisable);
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
            }
            if (!base.IsPostBack)
            {
                ModuleCode.Value = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;

                pager.CurrentPageIndex = 1;
                model = new RoleModel();
                DataBind();
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void DataBind()
        {
            int totalCount;
            //  RoleModel model = PageHelper.GetModel<RoleModel>(pnlContent.Controls);
            DataTable dt = roleBll.GetRole(model, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            //if (dt.Rows.Count==0)
            //{
            //    if (pager.CurrentPageIndex>1)
            //    {
            //        dt = roleBll.GetRole(model, pager.CurrentPageIndex - 1, pager.PageSize, out totalCount);                    
            //    }
            //}
            this.UltraWebGridRole.DataSource = dt;
            pager.RecordCount = totalCount;
            this.UltraWebGridRole.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
            for (int i = 0; i < this.UltraWebGridRole.Rows.Count; i++)
            {
                if ((this.UltraWebGridRole.Rows[i].Cells.FromKey("DELETED").Value != null) && Convert.ToString(this.UltraWebGridRole.Rows[i].Cells.FromKey("DELETED").Value).Equals("Y"))
                {
                    this.UltraWebGridRole.Rows[i].Style.ForeColor = Color.Red;
                }
            }
        }
        /// <summary>
        /// AJAX验证角色代码是否存在
        /// </summary>
        protected override void AjaxProcess()
        {
            if (Request.Form["rolecode"] != null)
            {
                string processFlag = Request.Form["flag"];
                string strinfo = "";
                switch (processFlag)
                {
                    case "Add":
                        {

                            strinfo = "N";
                            if (roleBll.IsExist(Request.Form["rolecode"]))
                            {
                                strinfo = "Y";
                            }
                            break;
                        }

                    case "Disable":
                        {
                            if (Request.Form["rolecode"] != null)
                            {
                                strinfo = "N";
                                if (authorityBll.GetAuthorityBykey(Request.Form["rolecode"]))
                                {
                                    strinfo = "Y";
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
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            pager.CurrentPageIndex = 1;
            model = PageHelper.GetModel<RoleModel>(pnlContent.Controls);
            DataBind();
            PageHelper.CleanControlsValue(pnlContent.Controls);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            RoleModel model = null;
            string alert = "";
            if (hidOperate.Value == "Add")
            {
                logmodel.ProcessFlag = "insert";
                model = PageHelper.GetModel<RoleModel>(pnlContent.Controls);
                if (roleBll.AddRole(model, logmodel))
                {
                    alert = "alert('" + Message.AddSuccess + "')";
                }
                else
                {
                    alert = "alert('" + Message.AddFailed + "')";
                }
            }
            else if (hidOperate.Value == "Modify")
            {
                logmodel.ProcessFlag = "update";
                model = PageHelper.GetModel<RoleModel>(pnlContent.Controls, ddlAcceptmsg, ddlDeleted);
                if (roleBll.UpdateRoleByKey(model, logmodel))
                {
                    alert = "alert('" + Message.UpdateSuccess + "')";
                }
                else
                {
                    alert = "alert('" + Message.UpdateFailed + "')";
                }
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "addOrUpDateRole", alert, true);
            PageHelper.CleanControlsValue(pnlContent.Controls);
            DataBind();
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            PageHelper.CleanControlsValue(pnlContent.Controls);
            DataBind();
            hidOperate.Value = "Cancel";

            //點擊取消之後,直接點擊"修改",應當提示選擇數據,否則頁面角色代碼欄位為空,依然可修改.--F3228823
            hidModify.Value = "N";
        }
        /// <summary>
        /// 生效
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEnable_Click(object sender, EventArgs e)
        {
            RoleModel model = PageHelper.GetModel<RoleModel>(pnlContent.Controls);
            model.Deleted = "N";
            string alert = "";
            logmodel.ProcessFlag = "update";
            if (roleBll.UpdateRoleByKey(model, logmodel))
            {
                alert = "alert('" + Message.EnableSuccess + "')";
            }
            else
            {
                alert = "alert('" + Message.EnableFailed + "')";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "enableRole", alert, true);
            PageHelper.CleanControlsValue(pnlContent.Controls);
            DataBind();
        }
        /// <summary>
        /// 失效
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDisable_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "update";
            bool b = roleBll.RoleDisable(txtRoleCode.Text.Trim(), logmodel);
            string alert = "";
            if (b)
            {
                alert = "alert('" + Message.DisableSuccess + "')";
            }
            else
            {
                alert = "alert('" + Message.DisableFailed + "')";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "disableRole", alert, true);
            PageHelper.CleanControlsValue(pnlContent.Controls);
            DataBind();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "delete";
      //      RoleModel model = PageHelper.GetModel<RoleModel>(pnlContent.Controls, txtRoleName, ddlAcceptmsg, ddlDeleted);
            string alert = "";
            if (roleBll.DeleteRole(txtRoleCode.Text.Trim(), logmodel))
            {
                alert = "alert('" + Message.DeleteSuccess + "')";
            }
            else
            {
                alert = "alert('" + Message.DisableFailed + "')";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "deleteRole", alert, true);
            PageHelper.CleanControlsValue(pnlContent.Controls);
            DataBind();
        }

        /// <summary>
        /// 分頁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            DataBind();
        }
    }
}
