/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： RolesRoleSet.cs
 * 檔功能描述： 群組與角色設定UI
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.3
 * 
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI;
using GDSBG.MiABU.Attendance.BLL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using Infragistics.WebUI.UltraWebNavigator;
using Resources;

namespace GDSBG.MiABU.Attendance.Web.SystemManage.SystemSafety
{
    public partial class RolesRoleSet : BasePage
    {
        RolesRoleBll rolesRoleBll = new RolesRoleBll();
        RolesBll rolesBll = new RolesBll();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        static SynclogModel logmodel = new SynclogModel();

        #region 頁面加載
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("EnableAndSave", Message.EnableAndSave);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);

            if (!IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;

                LoadMenu();
            }
        }
        #endregion

        #region 角色樹綁定
        private void LoadMenu()
        {
            string rolescode = base.Request.QueryString["RolesCode"];
            if (!string.IsNullOrEmpty(rolescode))
            {
                RolesModel rolesModel = new RolesModel();
                rolesModel = rolesBll.GetRolesByKey(rolescode);
                this.DisableInfo.Value = rolesModel.Deleted;
            }
            else
            {
                rolescode = "";
            }
                DataTable dt_1 = rolesRoleBll.GetRoleIsExist(rolescode);
                DataTable dt_2 = rolesRoleBll.GetRoleNotExist(rolescode);
                this.UltraWebTreeRoleCode.Nodes.Clear();
                if (dt_1.Rows.Count > 0)
                {
                    foreach (DataRow row in dt_1.Rows)
                    {
                        this.UltraWebTreeRoleCode.Nodes.Add(base.CreateNode(Convert.ToString(row["rolecode"]), row["rolename"].ToString(), true, true));
                    }
                }
                if (dt_2.Rows.Count > 0)
                {
                    foreach (DataRow row in dt_2.Rows)
                    {
                        this.UltraWebTreeRoleCode.Nodes.Add(base.CreateNode(Convert.ToString(row["rolecode"]), row["RoleName"].ToString(), false, true));
                    }
                }
                this.UltraWebTreeRoleCode.ExpandAll();

        }
        #endregion

        #region 存儲
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            string rolescode = base.Request.QueryString["RolesCode"];
            string alert = "";
            if (!string.IsNullOrEmpty(rolescode))
            {
                string RolesList = "";
                string createuser = base.CurrentUserInfo.Personcode;
                foreach (Node node in this.UltraWebTreeRoleCode.CheckedNodes)
                {
                    RolesList = RolesList + node.Tag.ToString() + "§";
                }
                if (rolesRoleBll.SaveRolesRole(rolescode, createuser, RolesList,logmodel))
                {
                    alert = "alert('" + Message.SaveSuccess + "');";
                }
                else
                {
                    alert = "alert('" + Message.SaveFailed + "');";
                }
            }
            else
            {
                alert = "alert('" + Message.NoRolesSelected + "');";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteAndAddRoles", alert, true);
            LoadMenu();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "reload", "window.parent.location.href=window.parent.location.href;", true);
        }
        #endregion
    }
}
