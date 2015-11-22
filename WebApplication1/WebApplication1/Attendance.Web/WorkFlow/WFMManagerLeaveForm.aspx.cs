/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： WFMManagerLeaveForm.cs
 * 檔功能描述： 簽核代理查詢
 * 
 * 版本：1.0
 * 創建標識： 何偉 2012.1.10
 * 
 */
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using GDSBG.MiABU.Attendance.BLL.WorkFlow;
using System.Collections;
using Infragistics.WebUI.UltraWebNavigator;
using System.Drawing;
using GDSBG.MiABU.Attendance.BLL.HRM.EmployeeData;
using System.Globalization;
using System.Resources;
using Infragistics.WebUI.WebSchedule;
using System.Web.SessionState;
using Infragistics.WebUI.UltraWebGrid;
using System.Web.Profile;
using System.Web.UI.HtmlControls;
using Resources;
using System.Web.Script.Serialization;

namespace GDSBG.MiABU.Attendance.Web.WorkFlow
{
    public partial class WFMManagerLeaveForm : BasePage
    {

        WFMManagerLeaveData wfld = new WFMManagerLeaveData();
        int totalCount = 0;
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
                ClientMessage.Add("DeleteConfirm", Message.DeleteFormConfirm);
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
                
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
            if (!IsPostBack)
            {
                Internationalization();
                this.ImageDepCode.Attributes.Add("onclick", "javascript:GetTreeDataValue(\"textBoxDepCode\",'" + moduleCode + "','textBoxDepName')");
                ModuleCode.Value = moduleCode;
            }
        }
        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonQuery_Click(object sender, EventArgs e)
        {
            pager.CurrentPageIndex = 1;
            dataBind(true);
        }
        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            this.textBoxDepCode.Text = "";
            this.textBoxDepName.Text = "";
            this.textBoxLocalName.Text = "";
            this.textBoxDeputyName.Text = "";
            this.textBoxStartDate.Text = "";
            this.textBoxEndDate.Text = "";
        }
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            int intDeleteOk = 0;
            int intDeleteError = 0;
            string alert = "";
            TemplatedColumn col = (TemplatedColumn) this.UltraWebGridWFM_ManagerLeave.Bands[0].Columns[0];
            for (int i = 0; i < UltraWebGridWFM_ManagerLeave.Rows.Count; i++)
            {
                CellItem GridItem = (CellItem)col.CellItems[i];
                CheckBox chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                if (chkIsHaveRight.Checked)
                {
                    string id = UltraWebGridWFM_ManagerLeave.Rows[i].Cells.FromKey("ID").Text.Trim();
                    if (wfld.DeleteData(id))
                    {
                        intDeleteOk++;
                    }
                    else
                    {
                        intDeleteError++;
                    }
                }
            }
            if (intDeleteOk + intDeleteError > 0)
            {
                alert = "alert('" + Message.DeleteSuccess + intDeleteOk + Message.DeleteFailed + intDeleteError + "')";
            }
            else
            {
                alert = "alert('" + Message.DeleteFailed + "')";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "deleteData", alert, true);
            dataBind(false);

        }
        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            dataBind(false);
        }
        #endregion
        //綁定數據
        public void dataBind(bool WindowOpen)
        {
            string condition = "";
            string sqlDep = base.sqlDep;
            string roleCode = (Request.Form["ModuleCode"] == null) ? "" : Request.Form["ModuleCode"].ToString();
            string appuser =CurrentUserInfo.Personcode;
            bool bPrivileged = base.AccessPermissionRequired;
            //if (roleCode.IndexOf("WFM") >= 0)
            //{
            //    condition = condition + " AND a.WorkNo='" + appuser + "'";
            //}
            //else if (base.bPrivileged)
            //{
            //    condition = condition + " AND exists (SELECT 1 FROM (" + base.sqlDep + ") e where e.DepCode=b.DCode)";
            //}
            if (WindowOpen)
            {
                if (this.textBoxDepName.Text.Trim().Length != 0)
                {
                    condition = condition + " AND b.DCode in (SELECT depcode FROM gds_sc_department START WITH depname LIKE '" + this.textBoxDepName.Text.Trim() + "%' CONNECT BY PRIOR depcode = parentdepcode)";
                }
                if (this.textBoxDeputyName.Text.Trim().Length != 0)
                {
                    condition = condition + " AND exists (Select LocalName From gds_att_employee f Where f.WorkNo=a.DeputyWorkNo and f.LocalName like '" + this.textBoxDeputyName.Text.Trim() + "')";
                }
                if (this.textBoxLocalName.Text.Trim().Length != 0)
                {
                    condition = condition + " AND b.LocalName like '" + this.textBoxLocalName.Text.Trim() + "%'";
                }
                if ((this.textBoxStartDate.Text.Trim().Length != 0) && (this.textBoxEndDate.Text.Trim().Length != 0))
                {
                    string StartDate = "";
                    string EndDate = "";
                    try
                    {
                        StartDate = DateTime.Parse(this.textBoxStartDate.Text.Trim()).ToString("yyyy/MM/dd");
                        EndDate = DateTime.Parse(this.textBoxEndDate.Text.Trim()).ToString("yyyy/MM/dd");
                    }
                    catch (Exception)
                    {
                       
                    }
                    condition = condition + " AND ((a.StartDate <= to_date('" + StartDate + "','yyyy/mm/dd') AND a.EndDate >= to_date('" + StartDate + "','yyyy/mm/dd')) or (a.StartDate <= to_date('" + EndDate + "','yyyy/mm/dd') AND a.EndDate >= to_date('" + EndDate + "','yyyy/mm/dd')) or (a.StartDate >= to_date('" + StartDate + "','yyyy/mm/dd') AND a.EndDate <= to_date('" + EndDate + "','yyyy/mm/dd')))";
                }
                if ((this.textBoxStartDate.Text.Trim().Length > 0) && (this.textBoxStartDate.Text.Trim().Length == 0))
                {
                    condition = condition + " AND a.StartDate<=to_date('" + DateTime.Parse(this.textBoxStartDate.Text.Trim()).ToString("yyyy/MM/dd") + "','yyyy/MM/dd')";
                }
                if ((this.textBoxEndDate.Text.Trim().Length > 0) && (this.textBoxStartDate.Text.Trim().Length == 0))
                {
                    condition = condition + " AND a.EndDate>=to_date('" + DateTime.Parse(this.textBoxEndDate.Text.Trim()).ToString("yyyy/MM/dd") + "','yyyy/MM/dd')";
                }
                this.ViewState.Add("condition", condition);
            }
            else
            {
                condition = Convert.ToString(this.ViewState["condition"]);
            }
            DataTable dt = wfld.GetDataByCondition(condition,pager.CurrentPageIndex,pager.PageSize,out totalCount);
            pager.RecordCount = totalCount;
            UltraWebGridWFM_ManagerLeave.DataSource = dt;
            UltraWebGridWFM_ManagerLeave.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
            
        }
        //數據初始化
        private void Internationalization()
        {
            UltraWebGridWFM_ManagerLeave.Bands[0].Columns.FromKey("StartDate").Format = "yyyy-MM-dd";
            UltraWebGridWFM_ManagerLeave.Bands[0].Columns.FromKey("EndDate").Format = "yyyy-MM-dd";
            UltraWebGridWFM_ManagerLeave.Bands[0].Columns.FromKey("ModifyDate").Format = "yyyy-MM-dd HH:mm:ss";
        }
        protected void textBoxLocalName_TextChanged(object sender, EventArgs e)
        {
            if(!wfld.CheckEmpName(textBoxLocalName.Text.Trim()))
            {
                 string alert = "alert('"+Message.NameNotExist+"')";
                 ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", alert, true);
                 textBoxLocalName.Text = string.Empty;
            }
        }
        protected void textBoxDeputyName_TextChanged(object sender, EventArgs e)
        {
            if (!wfld.CheckEmpName(textBoxDeputyName.Text.Trim()))
            {
                string alert = "alert('" + Message.NameIsNotExist + "')";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", alert, true);
                textBoxDeputyName.Text = string.Empty;
            }
        }

    }
}
