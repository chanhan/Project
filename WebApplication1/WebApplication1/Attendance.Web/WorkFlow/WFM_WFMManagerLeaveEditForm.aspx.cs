/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： WFM_WFMManagerLeaveEditForm.cs
 * 檔功能描述： 簽核代理新增、修改
 * 
 * 版本：1.0
 * 創建標識： 何偉 2012.1.10
 * 
 */
using System;
using System.Web;
using System.Web.UI;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using Resources;
using System.Data;
using GDSBG.MiABU.Attendance.BLL.WorkFlow;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Infragistics.WebUI.WebSchedule;

namespace GDSBG.MiABU.Attendance.Web.WorkFlow
{
    public partial class WFM_WFMManagerLeaveEditForm :BasePage
    {
        WFMManagerLeaveData wfld = new WFMManagerLeaveData();
        protected void Page_Load(object sender, EventArgs e)
        {
            string moduleCode = "WFMSYS004";
            SetCalendar(textBoxStartDate, textBoxEndDate);//日曆
            JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
            Dictionary<string, string> ClientMessage = null;
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("WrongDate", Message.WrongDate);
                ClientMessage.Add("ToLaterThanFrom", Message.ToLaterThanFrom);
                ClientMessage.Add("ErrWorknoNull", Message.ErrWorknoNull);
                ClientMessage.Add("LocalNameNotNull", Message.LocalNameNotNull);
                ClientMessage.Add("ErrDeputyNoNotNll", Message.ErrDeputyNoNotNll);
                ClientMessage.Add("ErrDeputyNameNll", Message.ErrDeputyNameNll);
                ClientMessage.Add("DepCodeNotNull", Message.DepCodeNotNull);
                ClientMessage.Add("ErrNotesNull", Message.ErrNotesNull);
                ClientMessage.Add("ErrDeputyReason", Message.ErrDeputyReason);
                ClientMessage.Add("DeputyDateNotNll", Message.DeputyDateNotNll);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
            if (!IsPostBack)
            {
                DeputyTypeBind();
               
                string ID = (Request.QueryString["ID"] == null) ? "" : Request.QueryString["ID"].ToString();
                string ProcessFlag = (Request.QueryString["ProcessFlag"] == null) ? "" : Request.QueryString["ProcessFlag"].ToString();
                this.HiddenID.Value = ID;
                this.ProcessFlag.Value = ProcessFlag;
                if (ProcessFlag.ToLower() == "add")
                {
                    GetRecord();
                }
                else
                {
                    tr_log.Style["display"] = "none";
                    if (ID.Length > 0)
                    { 
                        GetData(ID);
                    }
                }
                SetReadOnly(ProcessFlag.ToLower());
                this.ImageDepCode.Attributes.Add("onclick", "javascript:GetTreeDataValue(\"textBoxDCode\",'" + moduleCode + "','textBoxDName')");
               
            }
        }
        //綁定代理原因
        public void DeputyTypeBind()
        {  
            string condition=" WHERE DataType='WFMLeaveType' ORDER BY OrderId";
            DataTable dt = wfld.GetDeputType(condition);
            ddlLeaveType.DataTextField = "DataValue";
            ddlLeaveType.DataValueField = "DataCode";
            ddlLeaveType.DataSource = dt;
            ddlLeaveType.DataBind();
            ddlLeaveType.Items.Insert(0, new ListItem("", ""));
        }
        //保存數據
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DataTable dt = CreateTableTemp();
            //string inputCode = "D00226,D00159019";//textBoxDCode.Text
            string[] dCode = textBoxDCode.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string deputyflag = "N";
            if (HiddenDeputyNotes.Value != textBoxDeputyNotes.Text.Trim())
            {
                deputyflag = "Y";
            }
                string workno = textBoxEmployeeNo.Text;
                string startdate = textBoxStartDate.Text;
                string enddate = textBoxEndDate.Text;
                string deputyworkno = textBoxDeputyWorkNo.Text;
                string deputynotes = textBoxDeputyNotes.Text;
                string leavetype = ddlLeaveType.SelectedValue;
                string remark=textBoxRemark.Text;
                string id = HiddenID.Value;
            for (int i = 0; i < dCode.Length; i++)
            {
                string deptcode = dCode[i];
                dt.Rows.Add(workno, startdate, enddate, deputyworkno, deputynotes, leavetype, remark, deptcode, deputyflag, id);
            }
            string alert = "";
            if (wfld.SaveData(ProcessFlag.Value, dt, CurrentUserInfo.Personcode))
            {
                alert = "alert('" + Message.SaveSuccess + "')";
                alert += ";window.parent.document.all.btnQuery.click();";
            }
            else
            {
                alert = "alert('" + Message.SaveFailed + "')";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", alert, true);
        }
        //只讀
        public void SetReadOnly(string flag)
        {
            textBoxLocalName.ReadOnly = true;
            textBoxDeputyName.ReadOnly = true;
            if (flag == "add")
            {
                textBoxEmployeeNo.ReadOnly = false;
            }
            else
            {
                textBoxEmployeeNo.ReadOnly = true;
            }
        }
        //獲取數據
        public void GetData(string ID)
        {
            string condition = "and a.ID='" + ID + "' ";
            //if ((this.HiddenroleCode.Value.IndexOf("WFM") <= 0) && this.bPrivileged.Equals("True"))
            //{
            //    condition = condition + " AND exists (SELECT 1 FROM (" + base.sqlDep + ") e where e.DepCode=b.DCode)";
            //}
            DataTable dt = wfld.GetDataByCondition(condition);
            if (dt != null && dt.Rows.Count > 0)
            {
                this.textBoxEmployeeNo.Text = dt.Rows[0]["WorkNo"].ToString();
                this.textBoxLocalName.Text = dt.Rows[0]["LocalName"].ToString();
                this.ddlLeaveType.SelectedValue = dt.Rows[0]["LeaveType"].ToString();
                this.textBoxStartDate.Text = Convert.ToDateTime(dt.Rows[0]["StartDate"].ToString()).ToString("yyyy/MM/dd");
                this.textBoxEndDate.Text = Convert.ToDateTime(dt.Rows[0]["EndDate"].ToString()).ToString("yyyy/MM/dd");
                this.textBoxDeputyWorkNo.Text = dt.Rows[0]["DeputyWorkNo"].ToString();
                this.textBoxDeputyName.Text = dt.Rows[0]["DeputyName"].ToString();
                this.textBoxDeputyNotes.Text = dt.Rows[0]["DeputyNotes"].ToString();
                this.textBoxRemark.Text = dt.Rows[0]["Remark"].ToString();
                this.textBoxDName.Text = dt.Rows[0]["dept_name"].ToString();//單位名字
                this.textBoxDCode.Text = dt.Rows[0]["deptcode"].ToString();//單位代碼
                HiddenDeputyNotes.Value = dt.Rows[0]["DeputyNotes"].ToString();//隱藏Notes 用來對比是否改變
            }
        }
        protected void textBoxEmployeeNo_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = wfld.GetEmpInfo(textBoxEmployeeNo.Text.Trim().ToUpper());
            if (dt != null && dt.Rows.Count > 0)
            {
                textBoxEmployeeNo.Text = textBoxEmployeeNo.Text.Trim().ToUpper();
                textBoxLocalName.Text = dt.Rows[0]["localname"].ToString();
            }
            else
            {
                textBoxEmployeeNo.Text = "";
                textBoxLocalName.Text = "";
                string alert = "alert('" + Message.ErrWorkNoNotEXIST + "')";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", alert, true);              
            }
        }
        protected void textBoxDeputyWorkNo_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = wfld.GetEmpInfo(textBoxDeputyWorkNo.Text.Trim().ToUpper());
            if (dt != null && dt.Rows.Count > 0)
            {
                textBoxDeputyWorkNo.Text = textBoxDeputyWorkNo.Text.Trim().ToUpper();
                textBoxDeputyName.Text = dt.Rows[0]["localname"].ToString();
                textBoxDeputyNotes.Text = dt.Rows[0]["notes"].ToString();
            }
            else
            {
                textBoxDeputyWorkNo.Text = "";
                textBoxDeputyName.Text = "";
                textBoxDeputyNotes.Text = "";                
                string alert = "alert('" + Message.ErrWorkNoNotEXIST + "')";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", alert, true);
            }
        }
        //獲取以前代理記錄
        public void GetRecord()
        {
            DataTable dt = wfld.GetDeputyRecord(CurrentUserInfo.Personcode);
            ddlDeputyLog.DataValueField = "ID";
            ddlDeputyLog.DataTextField = "text";
            ddlDeputyLog.DataSource = dt;
            ddlDeputyLog.DataBind();
            ddlDeputyLog.Items.Insert(0, new ListItem("", ""));
            foreach (ListItem item in this.ddlDeputyLog.Items)
            {
                item.Attributes.Add("title", item.Text);
            }
        }
        protected void ddlDeputyLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDeputyLog.SelectedValue != "")
            {
                GetData(ddlDeputyLog.SelectedValue);
            }
            else
            {
                this.textBoxEmployeeNo.Text = "";
                this.textBoxLocalName.Text = "";
                this.ddlLeaveType.SelectedValue = "";
                this.textBoxStartDate.Text = "";
                this.textBoxEndDate.Text = "";
                this.textBoxDeputyWorkNo.Text = "";
                this.textBoxDeputyName.Text ="";
                this.textBoxDeputyNotes.Text = "";
                this.textBoxRemark.Text = "";
                this.textBoxDName.Text = "";//單位名字
                this.textBoxDCode.Text = "";//單位代碼
            }
            foreach (ListItem item in this.ddlDeputyLog.Items)
            {
                item.Attributes.Add("title", item.Text);
            }
        }
        //創建一個空表
        public DataTable CreateTableTemp()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("WorkNo");
            dt.Columns.Add("StartDate");
            dt.Columns.Add("EndDate");
            dt.Columns.Add("DeputyWorkNo");
            dt.Columns.Add("DeputyNotes");
            dt.Columns.Add("LeaveType");
            dt.Columns.Add("Remark");
            dt.Columns.Add("DeptCode");
            dt.Columns.Add("DeputyFlag");//Notess是否修改  Y是  N否
            dt.Columns.Add("ID");
            return dt;
        }
    }
}
