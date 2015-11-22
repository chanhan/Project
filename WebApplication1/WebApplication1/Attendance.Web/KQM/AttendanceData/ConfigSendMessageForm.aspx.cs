/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ConfigSendMessageForm.cs
 * 檔功能描述： 發送短信UI層
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2012.01.09
 * 
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI;
using GDSBG.MiABU.Attendance.BLL.KQM.AttendanceData;
using Infragistics.WebUI.UltraWebNavigator;
using Resources;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.Web.KQM.AttendanceData
{
    public partial class ConfigSendMessageForm : BasePage
    {
        DataTable tempDataTable = new DataTable();
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = new DataTable();
        protected UltraWebTree UltraWebTreeDeplevel;
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        ConfigSendMessageFormBll bll = new ConfigSendMessageFormBll();
        static SynclogModel logmodel = new SynclogModel();
        #region 頁面加載
        protected void Page_Load(object sender, EventArgs e)
        {
            PageHelper.ButtonControls(base.FuncList, pnlShowPanel.Controls, base.FuncListModule);
            if (!base.IsPostBack)
            {
                this.CreateTree();
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"];
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;
            }
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
                ClientMessage.Add("ToLaterThanFrom", Message.ToLaterThanFrom);
                ClientMessage.Add("WrongDate", Message.WrongDate);
                ClientMessage.Add("TextBoxNotNull", Message.TextBoxNotNull);
                ClientMessage.Add("DeleteConfirm", Message.ConfirmBatchConfirm);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
        }
        #endregion
        protected void btnSave_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "insert";
            Hashtable ModuleCodes = new Hashtable();
            int i = 0;
            foreach (Node node in this.UltraWebTreeModule.CheckedNodes)
            {
                i++;
                ModuleCodes.Add(i, node.Tag);
            }
            int flag = bll.UpdateSendMsgByKey(CurrentUserInfo.Personcode, ModuleCodes,logmodel);
            this.CreateTree();
            if (flag > 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.UpdateSuccess + "')", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.UpdateFailed + "')", true);
            }
        }

        private void CreateTree()
        {
            this.UltraWebTreeModule.Nodes.Clear();
            dataTable1 = bll.GetSendMsgDataByKey(CurrentUserInfo.Personcode, CurrentUserInfo.RoleCode);
            foreach (DataRow row in dataTable1.Rows)
            {
                this.UltraWebTreeModule.Nodes.Add(base.CreateNode(Convert.ToString(row["ModuleCode"]), row["RemindName"].ToString(), true, true));
            }
            dataTable2 = bll.GetSendMsgDataNotByKey(CurrentUserInfo.Personcode, CurrentUserInfo.RoleCode);
            foreach (DataRow row in dataTable2.Rows)
            {
                this.UltraWebTreeModule.Nodes.Add(base.CreateNode(Convert.ToString(row["ModuleCode"]), row["RemindName"].ToString(), false, true));
            }
            this.UltraWebTreeModule.ExpandAll();
        }
    }
}
