/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： RolesCode.cs
 * 檔功能描述： 群組設定數據UI
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2011.12.1
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
    public partial class RolesCode : BasePage
    {
        RolesBll rb = new RolesBll();
        RolesRoleBll rrb = new RolesRoleBll();
        DataTable dt = new DataTable();
        static RolesModel newmodel = new RolesModel();
        RolesModel model = new RolesModel();
        RolesRoleModel rrmodel = new RolesRoleModel();
        static SynclogModel logmodel = new SynclogModel();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;

        #region 頁面加載
        protected void Page_Load(object sender, EventArgs e)
        {
            PageHelper.ButtonControls(FuncList, pnlShowPanel.Controls, base.FuncListModule);
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("NoItemSelected", Message.NoItemSelected);
                ClientMessage.Add("RolesCodeIsExist", Message.RolesCodeIsExist);
                ClientMessage.Add("RolesDisableConfirm", Message.RolesDisableConfirm);
                ClientMessage.Add("DeleteConfirm", Message.DeleteConfirm);
                ClientMessage.Add("RolesCodeNotNull", Message.RolesCodeNotNull);
                ClientMessage.Add("AllowFlagNotNull", Message.AllowFlagNotNull);
                ClientMessage.Add("AllowFlagOnlyYorN", Message.AllowFlagOnlyYorN);
                ClientMessage.Add("AcceptMSGNotNull", Message.AcceptMSGNotNull);
                ClientMessage.Add("AcceptMSGOnlyYorN", Message.AcceptMSGOnlyYorN);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
            if (!base.IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;
                pager.CurrentPageIndex = 1;
                GridDataBind();
            }
        }
        #endregion

        #region 數據綁定
        /// <summary>
        /// 數據綁定
        /// </summary>
        private void GridDataBind()
        {
            int totalCount;

            DataTable dt = rb.GetRolesPageInfo(newmodel, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            pager.RecordCount = totalCount;
            this.UltraWebGridRoles.DataSource = dt.DefaultView;
            this.UltraWebGridRoles.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
            for (int i = 0; i < this.UltraWebGridRoles.Rows.Count; i++)
            {
                if ((this.UltraWebGridRoles.Rows[i].Cells.FromKey("Deleted").Value != null) && Convert.ToString(this.UltraWebGridRoles.Rows[i].Cells.FromKey("Deleted").Value).Equals("Y"))
                {
                    this.UltraWebGridRoles.Rows[i].Style.ForeColor = Color.Red;
                }
            }
        }
        #endregion

        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            GridDataBind();
        }
        #endregion

        #region 查詢
        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnQuery_Click(object sender, EventArgs e)
        {
            pager.CurrentPageIndex = 1;
            newmodel = PageHelper.GetModel<RolesModel>(pnlContent.Controls);
            GridDataBind();
        }
        #endregion

        #region Ajax
        /// <summary>
        /// 驗證群組是否存在
        /// </summary>
        protected override void AjaxProcess()
        {
            if (!string.IsNullOrEmpty(Request.Form["RolesCode"]))
            {
                string IsExist = "";
                model.RolesCode = Request.Form["RolesCode"];
                rrmodel.RolesCode = Request.Form["RolesCode"];
                string processFlag = Request.Form["ProcessFlag"];
                switch (processFlag)
                {
                    case "check":
                        {
                            if (rb.GetRoles(model).Rows.Count > 0)
                            {
                                IsExist = "Y";
                            }
                            else
                            {
                                IsExist = "N";
                            }
                            break;
                        }
                    case "Disable":
                        {
                            if (rrb.GetRolesRole(rrmodel).Rows.Count > 0)
                            {
                                IsExist = "Y";
                            }
                            else
                            {
                                IsExist = "N";
                            }
                            break;
                        }
                }

                Response.Clear();
                Response.Write(IsExist);
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
            rrmodel.RolesCode = this.txtRolesCode.Text;
            model.RolesCode = this.txtRolesCode.Text;
            string alert = "";
            logmodel.ProcessFlag = "delete";
            if (rrb.GetRolesRoleCount(rrmodel) > 0)
            {
                if (rrb.DeleteRolesRole(rrmodel,logmodel) != 0)
                {
                    if (rb.DeleteRoles(model,logmodel) != 0)
                    {
                        alert = "alert('" + Message.DeleteSuccess + "')";
                    }
                    else
                    {
                        alert = "alert('" + Message.DeleteFailed + "')";
                    }
                }
                else
                {
                    alert = "alert('" + Message.DeleteFailed + "')";
                }
            }
            else
            {
                if (rb.DeleteRoles(model,logmodel) != 0)
                {
                    alert = "alert('" + Message.DeleteSuccess + "')";
                }
                else
                {
                    alert = "alert('" + Message.DeleteFailed + "')";
                }
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteRoles", alert, true);
            PageHelper.CleanControlsValue(pnlContent.Controls);
            GridDataBind();
        }
        #endregion

        #region 存儲
        /// <summary>
        /// 存儲
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnSave_Click(object sender, EventArgs e)
        {
            model = PageHelper.GetModel<RolesModel>(pnlContent.Controls);
            string alert = "";
            logmodel.ProcessFlag = "insert";
            if (hidOperate.Value == "Add")
            {
                model.CreateDate = System.DateTime.Now;
                model.CreateUser = base.CurrentUserInfo.Personcode;
                if (rb.AddRoles(model,logmodel))
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
                model.UpdateDate = System.DateTime.Now;
                model.UpdateUser = base.CurrentUserInfo.Personcode;
                if (rb.UpdateRolesByKey(model,logmodel))
                {
                    alert = "alert('" + Message.UpdateSuccess + "')";
                }
                else
                {
                    alert = "alert('" + Message.UpdateFailed + "')";
                }
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "save", alert, true);
            PageHelper.CleanControlsValue(pnlContent.Controls);
            GridDataBind();
        }
        #endregion

        #region 失效
        /// <summary>
        /// 失效
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnDisable_Click(object sender, EventArgs e)
        {
            RolesModel model = PageHelper.GetModel<RolesModel>(pnlContent.Controls);
            string alert = "";
            logmodel.ProcessFlag = "update";
            if (hidOperate.Value == "Disable")
            {
                    model.Deleted = "Y";
                    model.UpdateDate = System.DateTime.Now;
                    model.CreateUser = base.CurrentUserInfo.Personcode;
                    rrmodel.RolesCode = model.RolesCode;
                    rrb.DeleteRolesRole(rrmodel,logmodel);
                if (rb.UpdateRolesByKey(model,logmodel))
                {
                    alert = "alert('" + Message.DisableSuccess + "')";
                }
                else
                {
                    alert = "alert('" + Message.DisableFailed + "')";
                }
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DisableRoles", alert, true);
            PageHelper.CleanControlsValue(pnlContent.Controls);
            GridDataBind();
        }
        #endregion

        #region 生效
        /// <summary>
        /// 生效
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnEnable_Click(object sender, EventArgs e)
        {
            RolesModel model = PageHelper.GetModel<RolesModel>(pnlContent.Controls);
            string alert = "";
            model.Deleted = "N";
            model.UpdateDate = System.DateTime.Now;
            model.CreateUser = base.CurrentUserInfo.Personcode;
            if (rb.UpdateRolesByKey(model,logmodel))
            {
                alert = "alert('" + Message.EnableSuccess + "')";
            }
            else
            {
                alert = "alert('" + Message.EnableFailed + "')";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EnableRoles", alert, true);
            PageHelper.CleanControlsValue(pnlContent.Controls);
            GridDataBind();
        }
        #endregion
    }
}
