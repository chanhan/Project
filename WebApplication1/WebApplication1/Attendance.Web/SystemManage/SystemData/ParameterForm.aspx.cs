/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ParameterForm.cs
 * 檔功能描述： 系統參數
 * 
 * 版本：1.0
 * 創建標識： 何西 2011.12.06
 * 
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI;
using GDSBG.MiABU.Attendance.BLL.SystemManage.SystemData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemData;
using Resources;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.Web.SystemManage.SystemData
{
    public partial class ParameterForm : BasePage
    {
        ParameterBll parameterBll = new ParameterBll();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        static SynclogModel logmodel = new SynclogModel();
        protected void Page_Load(object sender, EventArgs e)
        {
           PageHelper.ButtonControls(FuncList, pnlShowPanel.Controls, base.FuncListModule);
            if (!IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;

                pager.CurrentPageIndex = 1;
                ParameterBind();
            }
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("AddFailed", Message.AddFailed);
                ClientMessage.Add("AddSuccess", Message.AddSuccess);
                ClientMessage.Add("DeleteConfirm", Message.DeleteConfirm);
                ClientMessage.Add("DeleteFailed", Message.DeleteFailed);
                ClientMessage.Add("DeleteSuccess", Message.DeleteSuccess);
                ClientMessage.Add("DeleteNotNull", Message.DeleteNotNull);
                ClientMessage.Add("UpdateFailed", Message.UpdateFailed);
                ClientMessage.Add("UpdateSuccess", Message.UpdateSuccess);
                ClientMessage.Add("TextBoxNotNull", Message.TextBoxNotNull);
                ClientMessage.Add("SaveConfim", Message.SaveConfim);
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
        }


        #region 數據綁定
        /// <summary>
        /// 數據綁定
        /// </summary>
        private void ParameterBind()
        {
            int totalCount;
            ParameterModel model = new ParameterModel();
            string actionFlag = this.hidOperate.Value.ToString().Trim();
            if (actionFlag == "Condition")
            {
                 model = PageHelper.GetModel<ParameterModel>(pnlContent.Controls);
            }
            DataTable dt = parameterBll.GetParameter(model, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            pager.RecordCount = totalCount;
            UltraWebGridParameter.DataSource = dt.DefaultView;
            UltraWebGridParameter.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
        }
        #endregion

        #region 存儲
        /// <summary>
        /// 存儲
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ParameterModel model = PageHelper.GetModel<ParameterModel>(pnlContent.Controls);
            string alert = "";
            if (hidOperate.Value == "Add")
            {
                model.ParaName = txtParaName.Text;
                model.CreateUser = CurrentUserInfo.Personcode;
                model.CreateDate = DateTime.Now;
                if (!string.IsNullOrEmpty(model.ParaName))
                {
                    ParameterModel addModel = parameterBll.GetParameterByKey(model.ParaName);//檢驗新增數據的主鍵是否已存在
                    if (addModel == null)
                    {
                        logmodel.ProcessFlag = "insert";
                        if (parameterBll.AddNew(model, logmodel))
                        {
                            alert = "alert('" + Message.AddSuccess + "')";
                        }
                        else
                        {
                            alert = "alert('" + Message.AddFailed + "')";
                        }
                    }
                    else
                    {
                        alert = "alert('" + Message.DataExist + "')";
                    }
                }
                else
                {
                    alert = "alert('" + Message.TextBoxNotNull + "')";
                }
                hidOperate.Value = null;
            }
            else if (hidOperate.Value == "Modify")
            {
                model.UpdateUser = CurrentUserInfo.Personcode;
                model.UpdateDate = DateTime.Now;
                logmodel.ProcessFlag = "update";
                bool updateFlag = parameterBll.UpdateByKey(model, false, logmodel);
                if (updateFlag == true)
                {
                    alert = "alert('" + Message.UpdateSuccess + "')";
                }
                else
                {
                    alert = "alert('" + Message.UpdateFailed + "')";
                }
                hidOperate.Value = null;
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", alert, true);
            PageHelper.CleanControlsValue(pnlContent.Controls);
            model = new ParameterModel();
            ParameterBind();
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            pager.CurrentPageIndex = 1;
            string actionFlag = this.hidOperate.Value.ToString().Trim();
            ParameterModel model = new ParameterModel();
            if (actionFlag == "Condition")
            {
                model = PageHelper.GetModel<ParameterModel>(pnlContent.Controls);
                ParameterBind();
                this.hidOperate.Value = null;

            }
            else
            {
                
                ParameterBind();
            }

        }
        #endregion

        #region 分頁
        /// <summary>
        /// 分頁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            ParameterBind();
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
            string alert = "";
            string flag = this.hidOperate.Value.ToString().Trim();
            ParameterModel model = new ParameterModel();
            if (flag == "Delete")
            {
                bool deFlag = false;
                model.ParaName = txtParaName.Text.ToString().Trim();
                logmodel.ProcessFlag = "delete";
                deFlag = parameterBll.DeleteByKey(model, logmodel);
                if (deFlag == true)
                {
                    alert = "alert('" + Message.DeleteSuccess + "')";
                }
                else
                {
                    alert = "alert('" + Message.DeleteFailed + "')";
                }
                this.hidOperate.Value = "";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show", alert, true);
            PageHelper.CleanControlsValue(pnlContent.Controls);
            ParameterBind();
        }
        #endregion
    }
}
