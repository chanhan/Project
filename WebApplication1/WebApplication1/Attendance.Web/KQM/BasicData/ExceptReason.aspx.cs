/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ExceptReason.cs
 * 檔功能描述： 異常原因UI層
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2012.01.09
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Web.UI;
using GDSBG.MiABU.Attendance.BLL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Common;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using Resources;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.Web.KQM.BasicData
{
    public partial class ExceptReason : BasePage
    {
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        ExceptReasonModel model = new ExceptReasonModel();
        Dictionary<string, string> ClientMessage = null;
        ExceptReasonBll bll = new ExceptReasonBll();
        static DataTable dt = new DataTable();
        static SynclogModel logmodel = new SynclogModel();
        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            if (this.ProcessFlag.Value == "Condition")
            {
                model = PageHelper.GetModel<ExceptReasonModel>(pnlContent.Controls);
            }
            else
            {
                model = new ExceptReasonModel();
            }
            DataUIBind();
        }
        #endregion

        #region 查詢
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            if (this.hidOperate.Value == "Condition")
            {
                ProcessFlag.Value = "Condition";
                model = PageHelper.GetModel<ExceptReasonModel>(pnlContent.Controls);
            }
            else
            {
                model = new ExceptReasonModel();
            }
            pager.CurrentPageIndex = 1;
            DataUIBind();
            hidOperate.Value = "";
        }
        #endregion
        #region 頁面加載
        protected void Page_Load(object sender, EventArgs e)
        {
            PageHelper.ButtonControls(base.FuncList, pnlShowPanel.Controls, base.FuncListModule);
            if (!IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"];
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;
                pager.CurrentPageIndex = 1;
                DataUIBind();
            }
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("CantChangToLac", Message.CantChangToLac);
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
                ClientMessage.Add("TextBoxNotNull", Message.TextBoxNotNull);
                ClientMessage.Add("DeleteConfirm", Message.DeleteConfirm);
                ClientMessage.Add("SaveConfirm", Message.SaveConfirm);
                ClientMessage.Add("ShiftIsUsedNoDelete", Message.ShiftIsUsedNoDelete);
                ClientMessage.Add("EnableConfirm", Message.EnableConfirm);
                ClientMessage.Add("DisableConfirm", Message.DisableConfirm);
                ClientMessage.Add("IsSalaryNoNull", Message.IsSalaryNoNull);
                ClientMessage.Add("ExceptionNameNoNull", Message.ExceptionNameNoNull);
                ClientMessage.Add("ExceptionCodeNoNull", Message.ExceptionCodeNoNull);
                ClientMessage.Add("OnlyYOrN", Message.OnlyYOrN);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
        }
        #endregion
        #region  查詢代碼
        private void DataUIBind()
        {
            string activeFlag = hidOperate.Value.ToString().Trim();
            string orderType = "";
            int totalCount;
            //if (activeFlag == "Query" || activeFlag == "" || string.IsNullOrEmpty(activeFlag))
            //{
            //    orderType = "1";
            //}
            //else if (activeFlag == "Edit")
            //{
            //    orderType = "2";
            //}
            dt = bll.GetExceptList(model, orderType, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            pager.RecordCount = totalCount;
            this.UltraWebGridDegree.DataSource = dt;
            this.UltraWebGridDegree.DataBind();
            for (int i = 0; i < this.UltraWebGridDegree.Rows.Count; i++)
            {
                if ((this.UltraWebGridDegree.Rows[i].Cells.FromKey("EFFECTFLAG").Value != null) && Convert.ToString(this.UltraWebGridDegree.Rows[i].Cells.FromKey("EFFECTFLAG").Value).Equals("N"))
                {
                    this.UltraWebGridDegree.Rows[i].Style.ForeColor = Color.Red;
                }
            }
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
        }
        #endregion
        #region 刪除事件
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string alert = "";
            string actionFlag = this.hidOperate.Value.ToString().Trim();
            //刪除
            if (actionFlag == "Delete")
            {

                model.ReasonNo = txtReasonNo.Text.ToString().Trim();
                if (bll.DeleteExceptByKey(model, logmodel) > 0)
                {
                    alert = "alert('" + Message.DeleteSuccess + "')";
                }
                else
                {
                    alert = "alert('" + Message.DeleteFailed + "')";
                }
                this.hidOperate.Value = "";
            }
            Page.ClientScript.RegisterStartupScript(GetType(), "show", alert, true);
            pager.CurrentPageIndex = 1;
            PageHelper.CleanControlsValue(pnlContent.Controls);
            model = new ExceptReasonModel();
            DataUIBind();
        }
        #endregion
        #region 生效
        protected void btnEnable_Click(object sender, EventArgs e)
        {
            string alert = "";
            string actionFlag = this.hidOperate.Value.ToString().Trim();
            //生效
            if (actionFlag == "Enable")
            {
                logmodel.ProcessFlag = "update";
                model.UpdateDate = System.DateTime.Now;
                model.UpdateUser = CurrentUserInfo.Personcode;
                model.ReasonNo = txtReasonNo.Text.ToString().Trim();
                model.EffectFlag = "Y";
                if (bll.UpdateExceptByKey(model,logmodel))
                {
                    alert = "alert('" + Message.EnableSuccess + "')";
                }
                else
                {
                    alert = "alert('" + Message.EnableFailed + "')";
                }
                this.hidOperate.Value = "";
            }
            Page.ClientScript.RegisterStartupScript(GetType(), "show", alert, true);
            pager.CurrentPageIndex = 1;
            PageHelper.CleanControlsValue(pnlContent.Controls);
            model = new ExceptReasonModel();
            DataUIBind();
        }
        #endregion
        #region 失效
        protected void btnDisable_Click(object sender, EventArgs e)
        {
            string alert = "";
            string actionFlag = this.hidOperate.Value.ToString().Trim();
            //失效
            if (actionFlag == "Disable")
            {
                logmodel.ProcessFlag = "update";
                model.UpdateDate = System.DateTime.Now;
                model.UpdateUser = CurrentUserInfo.Personcode;
                model.ReasonNo = txtReasonNo.Text.ToString().Trim();
                model.EffectFlag = "N";
                if (bll.UpdateExceptByKey(model,logmodel))
                {
                    alert = "alert('" + Message.DisableSuccess + "')";
                }
                else
                {
                    alert = "alert('" + Message.DisableFailed + "')";
                }
            }
            Page.ClientScript.RegisterStartupScript(GetType(), "show", alert, true);
            pager.CurrentPageIndex = 1;
            PageHelper.CleanControlsValue(pnlContent.Controls);
            model = new ExceptReasonModel();
            DataUIBind();
            this.hidOperate.Value = "";
        }
        #endregion
        #region 保存事件
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string activeFlag = hidOperate.Value.ToString().Trim();
            hidOperate.Value = "Edit";
            if (activeFlag == "Add")
            {
                logmodel.ProcessFlag = "insert";
                model = PageHelper.GetModel<ExceptReasonModel>(pnlContent.Controls);
                model.UpdateDate = DateTime.Now;
                model.UpdateUser = CurrentUserInfo.Personcode;
                int flag = bll.InsertExceptByKey(model, logmodel);
                if (flag > 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.AddSuccess + "')", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.AddFailed + "')", true);
                }
            }
            if (activeFlag == "Modify")
            {
                logmodel.ProcessFlag = "update";
                model = PageHelper.GetModel<ExceptReasonModel>(pnlContent.Controls);
                model.UpdateDate = DateTime.Now;
                model.UpdateUser = CurrentUserInfo.Personcode;
                bool flag = bll.UpdateExceptByKey(model,logmodel);
                if (flag)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.UpdateSuccess + "')", true);

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.UpdateFailed + "')", true);
                }
            }
            pager.CurrentPageIndex = 1;
            model = new ExceptReasonModel();
            DataUIBind();
            hidOperate.Value = "";
        }
        #endregion
        #region Ajax事件
        protected override void AjaxProcess()
        {
            bool blPro = false;
            int result = 0;
            if (Request.Form["ActiveFlag"] == "Modify")
            {
                 result = 1;
                 blPro = true;
            }
            if (Request.Form["ActiveFlag"] == "Add" && !string.IsNullOrEmpty(Request.Form["ReasonNo"]))
            {
                model.ReasonNo = Request.Form["ReasonNo"].ToString();
                DataTable dtExcept = bll.GetExceptByKey(model);
                if (dtExcept.Rows.Count > 0 || dtExcept == null)
                {
                    result = 0;
                }
                else
                {
                    result = 1;
                }

                blPro = true;
            }
            if (blPro)
            {
                Response.Clear();
                Response.Write(result.ToString());
                Response.End();
            }
        }
        #endregion
    }
}
