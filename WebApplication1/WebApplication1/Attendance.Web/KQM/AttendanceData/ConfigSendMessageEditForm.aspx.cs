/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： ConfigSendMessageEditForm.cs
 * 檔功能描述： 發送短信內容UI層
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
    public partial class ConfigSendMessageEditForm : BasePage
    {
        DataTable tempDataTable = new DataTable();
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = new DataTable();
        protected UltraWebTree UltraWebTreeDep;
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        ConfigSendMessageFormBll bll = new ConfigSendMessageFormBll();
        static SynclogModel logmodel = new SynclogModel();
        protected void btnSave_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "insert";
            Hashtable depts = new Hashtable();
            int i = 0;
            foreach (Node node in this.UltraWebTreeDep.CheckedNodes)
            {
                i++;
                depts.Add(i, node.Tag);
            }
            int flag = bll.SaveSRMData(CurrentUserInfo.Personcode, "KQMSYS20101", CurrentUserInfo.CompanyId, depts,logmodel);
            this.CreateTreeDep();
            if (flag > 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.UpdateSuccess + "')", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.UpdateFailed + "')", true);
            }
        }
        private void CreateTreeDep()
        {
            this.UltraWebTreeDep.Nodes.Clear();
            //string moduleCode=Request.QueryString["modulecode"].ToString();
            this.tempDataTable = this.GetPersonDeptDataByModule(CurrentUserInfo.Personcode, "KQMSYS20101", CurrentUserInfo.CompanyId);
            SortedList allTreeNodes = new SortedList();
            foreach (DataRow row in this.tempDataTable.Rows)
            {
                allTreeNodes.Add(Convert.ToString(row["depcode"]), this.CreateNode(Convert.ToString(row["depcode"]), row["depname"].ToString(), Convert.ToString(row["authorized"]).Equals("Y"), Convert.ToDecimal(this.tempDataTable.Compute("count(depcode)", "parentdepcode='" + row["depcode"] + "'")) == 0M));
                if ((row["parentdepcode"].ToString().Trim().Length > 0) && (allTreeNodes.IndexOfKey(row["parentdepcode"]) >= 0))
                {
                    ((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(row["parentdepcode"]))).Nodes.Add((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(row["depcode"])));
                }
            }
            if (this.tempDataTable.Rows.Count > 0)
            {
                this.UltraWebTreeDep.Nodes.Add((Node)allTreeNodes.GetByIndex(allTreeNodes.IndexOfKey(Convert.ToString(this.tempDataTable.Rows[0]["depcode"]))));
            }
            foreach (Node node in this.UltraWebTreeDep.Nodes)
            {
                node.Expand(false);
            }
        }
        public DataTable GetPersonDeptDataByModule(string personCode, string AmoduleCode, string AcompanyID)
        {
            return bll.GetPersonDeptDataByModule(personCode, AmoduleCode, AcompanyID);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
                ClientMessage.Add("ToLaterThanFrom", Message.ToLaterThanFrom);
                ClientMessage.Add("WrongDate", Message.WrongDate);
                ClientMessage.Add("TextBoxNotNull", Message.TextBoxNotNull);
                ClientMessage.Add("DeleteConfirm", Message.ConfirmBatchConfirm);
            }
            string flag = "";
            if (Request.QueryString["Flag"] != null)
            {
                flag = Request.QueryString["Flag"].ToString();
                if (flag == "1")
                {
                    btnSave.Enabled = true;
                }
                else
                {
                    btnSave.Enabled = false;
                }
            }
            else
                btnSave.Enabled = false;


            if (!base.IsPostBack)
            {
                this.CreateTreeDep();
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"];
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;
            }
        }
    }
}
