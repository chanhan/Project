/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： LeaveTypeForm.cs
 * 檔功能描述： 請假類別定義功能模組UI類
 * 
 * 版本：1.0
 * 創建標識： 陈函 2011.12.13
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Web.UI;
using GDSBG.MiABU.Attendance.BLL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using Resources;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.Web.KQM.BasicData
{
    public partial class LeaveTypeForm : BasePage
    {
        LeaveTypeBll leaveTypeBll = new LeaveTypeBll();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
      static  LeaveTypeModel leaveTypeModel;
      static SynclogModel logmodel = new SynclogModel();
        protected void Page_Load(object sender, EventArgs e)
        {
           PageHelper.ButtonControls(FuncList, pnlShowPanel.Controls, base.FuncListModule);
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("LvTypeExist", Message.LvTypeExist);
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
                ClientMessage.Add("DeleteAttTypeConfirm", Message.DeleteAttTypeConfirm);
            }
            if (!base.IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;

                pager.CurrentPageIndex = 1;
                leaveTypeModel = new LeaveTypeModel();
                DataBind();
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
        }
        private void DataBind()
        {
            int totalCount;
           // LeaveTypeModel leaveTypeModel = PageHelper.GetModel<LeaveTypeModel>(inputPanel.Controls, hidEffectFlag);
            DataTable dt = leaveTypeBll.GetLeaveType(leaveTypeModel, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            UltraWebGridLeaveType.DataSource = dt.DefaultView;
            pager.RecordCount = totalCount;
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
            UltraWebGridLeaveType.DataBind();
            for (int i = 0; i < this.UltraWebGridLeaveType.Rows.Count; i++)
            {
                if ((this.UltraWebGridLeaveType.Rows[i].Cells.FromKey("EFFECTFLAG").Value != null) && Convert.ToString(this.UltraWebGridLeaveType.Rows[i].Cells.FromKey("EFFECTFLAG").Value).Equals("N"))
                {
                    this.UltraWebGridLeaveType.Rows[i].Style.ForeColor = Color.Red;
                }
            }
        }
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            DataBind();
        }
        /// <summary>
        /// AJAX验证角色代码是否存在
        /// </summary>
        protected override void AjaxProcess()
        {
            if (Request.Form["lvtypecode"] != null)
            {
                string strinfo = "N";
                if (leaveTypeBll.IsExist(Request.Form["lvtypecode"]))
                {
                    strinfo = "Y";
                }
                Response.Clear();
                Response.Write(strinfo);
                Response.End();
            }

        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
             leaveTypeModel = PageHelper.GetModel<LeaveTypeModel>(inputPanel.Controls, hidEffectFlag);
            pager.CurrentPageIndex = 1;
            DataBind();
            PageHelper.CleanControlsValue(inputPanel.Controls);
            ProcessFlag.Value = "N";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            LeaveTypeModel leaveTypeModel =null;
            string alert = "";
            if (hidOperate.Value == "Add")
            {
                logmodel.ProcessFlag = "insert";
                leaveTypeModel = PageHelper.GetModel<LeaveTypeModel>(inputPanel.Controls);
                leaveTypeModel.Modifier =CurrentUserInfo.Personcode;
                if (leaveTypeBll.AddLeaveType(leaveTypeModel, logmodel))
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
                leaveTypeModel = PageHelper.GetModel<LeaveTypeModel>(inputPanel.Controls, hidEffectFlag);
                leaveTypeModel.Modifier = CurrentUserInfo.Personcode;
                if (leaveTypeBll.UpDateLeaveType(leaveTypeModel, logmodel))
                {
                    alert = "alert('" + Message.UpdateSuccess + "')";
                }
                else
                {
                    alert = "alert('" + Message.UpdateFailed + "')";
                }
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "addAndUpDateLeaveType", alert, true);
            PageHelper.CleanControlsValue(inputPanel.Controls);
            DataBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "delete";
            string alert = "";
            if (leaveTypeBll.DeleteLeaveType(txtLvTypeCode.Text, logmodel))
            {

                alert = "alert('" + Message.DeleteSuccess + "')";
            }
            else
            {
                alert = "alert('" + Message.DeleteFailed + "')";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteLeaveType", alert, true);
            PageHelper.CleanControlsValue(inputPanel.Controls);
            DataBind();
            ProcessFlag.Value = "N";
        }

        protected void btnDisable_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "update";
            string alert = "";
            if (EnableAndDisableLeaveType(txtLvTypeCode.Text, "N", logmodel))
            {

                alert = "alert('" + Message.DisableSuccess + "')";
            }
            else
            {
                alert = "alert('" + Message.DisableFailed + "')";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DisableLeaveType", alert, true);
            PageHelper.CleanControlsValue(inputPanel.Controls);
            DataBind();
            ProcessFlag.Value = "N";
        }

        protected void btnEnable_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "update";
            string alert = "";
            if (EnableAndDisableLeaveType(txtLvTypeCode.Text, "Y", logmodel))
            {

                alert = "alert('" + Message.EnableSuccess + "')";
            }
            else
            {
                alert = "alert('" + Message.EnableFailed + "')";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EnableLeaveType", alert, true);
            PageHelper.CleanControlsValue(inputPanel.Controls);
            DataBind();
            ProcessFlag.Value = "N";
        }
        private bool EnableAndDisableLeaveType(string LvTypeCode, string EffectFlag, SynclogModel logmodel)
        {
            LeaveTypeModel leaveTypeModel = new LeaveTypeModel();
            leaveTypeModel.LvTypeCode = LvTypeCode;
            leaveTypeModel.EffectFlag = EffectFlag;
            return leaveTypeBll.EnableAndDisableLeaveType(leaveTypeModel, logmodel);
        }
    }
}
