using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.KQM.OTM;
using GDSBG.MiABU.Attendance.Model.KQM.OTM;
using Infragistics.WebUI.UltraWebGrid;
using Resources;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OTMRealApplyForm.aspx.cs
 * 檔功能描述： 有效加班
 * 
 * 版本：1.0
 * 創建標識： 昝望 2011.12.23
 * 
 */

namespace GDSBG.MiABU.Attendance.Web.KQM.OTM
{
    public partial class OTMRealApplyForm : BasePage
    {
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        RealapplyBll bll = new RealapplyBll();
        int totalCount;
        static DataTable dt_global = new DataTable();
        static SynclogModel logmodel = new SynclogModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            PageHelper.ButtonControls(base.FuncList, pnlShowPanel.Controls, base.FuncListModule);
            pager.CurrentPageIndex = 1;
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("DeleteConfirm", Message.DeleteConfirm);
                ClientMessage.Add("QueryBatchWorkNo", Message.QueryBatchWorkNo);
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
                ClientMessage.Add("CancelAudit", Message.CancelAudit);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
            SetCalendar(txtOTDateFrom, txtOTDateTo);
            ModuleCode.Value = Request.QueryString["ModuleCode"].ToString();
            if (!IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;
                ddlPersonType.DataSource = bll.GetOverTimeType();
                ddlPersonType.DataValueField = "datacode";
                ddlPersonType.DataTextField = "datavalue";
                ddlPersonType.DataBind();
                this.ddlPersonType.Items.Insert(0, new ListItem("", ""));
                this.ddlPersonType.SelectedValue = "";
                ddlOTStatus.DataSource = bll.GetOTMAdvanceApplyStatus();
                ddlOTStatus.DataValueField = "datacode";
                ddlOTStatus.DataTextField = "datavalue";
                ddlOTStatus.DataBind();
                this.ddlOTStatus.Items.Insert(0, new ListItem("", ""));
                this.ddlOTStatus.SelectedValue = "";
                this.txtOTDateFrom.Text = DateTime.Now.AddDays(-1.0).ToShortDateString();
                this.txtOTDateTo.Text = DateTime.Now.ToShortDateString();
                DataBind();
                SetSelector(ImageDepCode, txtDepCode, txtDepName, "DepCode", Request.QueryString["ModuleCode"].ToString());
            }
        }

        #region 取消簽核
        protected void btnCancelAudit_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "update";
            int intUpdateOk = 0;
            int intUpdateError = 0;
            TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGrid.Bands[0].Columns[0];
            for (int i = 0; i < this.UltraWebGrid.Rows.Count; i++)
            {
                CellItem GridItem = (CellItem)tcol.CellItems[i];
                CheckBox chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                if (chkIsHaveRight.Checked)
                {
                    RealapplyModel model = new RealapplyModel();
                    model.Id = this.UltraWebGrid.Rows[i].Cells.FromKey("ID").Text.Trim();
                    model.Status = "0";
                    int num = bll.UpdateRealapplyByKey(model,logmodel);
                    if (num > 0)
                    {
                        intUpdateOk++;
                    }
                    else
                    {
                        intUpdateError++;
                    }
                }
            }
            if ((intUpdateOk + intUpdateError) > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.UpdateSuccess + ":" + intUpdateOk + ";" + Message.UpdateFailed + ":" + intUpdateError + "');", true);
                DataBind();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.AtLastOneChoose + "');", true);
                return;
            }
        }
        #endregion

        #region 綁定數據
        /// <summary>
        /// 綁定數據
        /// </summary>

        private void DataBind()
        {
           
            RealapplyModel model = new RealapplyModel();
            dt_global = bll.GetRealapply(model, pager.CurrentPageIndex, pager.PageSize, out totalCount, ddlHoursCondition.SelectedValue, txtHours.Text.Trim(), txtOTDateFrom.Text.Trim(), txtOTDateTo.Text.Trim(), txtBatchEmployeeNo.Text.Trim(),base.SqlDep);
            pager.RecordCount = totalCount;
            this.UltraWebGrid.DataSource = dt_global;
            this.UltraWebGrid.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
        }


        private void DataBind(DataTable dt)
        {  
            this.UltraWebGrid.DataSource = dt;
            this.UltraWebGrid.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
        }

        #endregion

        #region 查詢
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            string BatchEmployeeNo="";
            string str = txtBatchEmployeeNo.Text.Trim();
            if (str != "")
            {
                for (int i = 0; i < str.Split('\n').Length; i++)
                {
                    BatchEmployeeNo = BatchEmployeeNo + str.Split('\n')[i].Trim() + "§";
                }
            }
            RealapplyModel model = PageHelper.GetModel<RealapplyModel>(pnlContent.Controls);
            model.ConfirmHours = null;
            model.OverTimeType = ddlPersonType.SelectedValue;
            DataTable dt = bll.GetRealapply(model, pager.CurrentPageIndex, pager.PageSize, out totalCount, ddlHoursCondition.SelectedValue, txtHours.Text.Trim(), txtOTDateFrom.Text.Trim(), txtOTDateTo.Text.Trim(), BatchEmployeeNo, base.SqlDep);
            pager.RecordCount = totalCount;
            dt_global = dt;
            DataBind(dt);

        }
        #endregion

        #region 重置
        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.txtDepCode.Text = "";
            this.txtDepName.Text = "";
            this.txtEmployeeNo.Text = "";
            this.txtName.Text = "";
            this.ddlOTType.SelectedValue = "";
            this.ddlOTStatus.SelectedValue = "";
            this.ddlPersonType.SelectedValue = "";
            this.ddlIsProject.SelectedValue = "";
            this.txtOTDateFrom.Text = DateTime.Now.AddDays(-1.0).ToShortDateString();
            this.txtOTDateTo.Text = DateTime.Now.ToShortDateString();
        }
        #endregion

        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
             string BatchEmployeeNo="";
            string str = txtBatchEmployeeNo.Text.Trim();
            if (str != "")
            {
                for (int i = 0; i < str.Split('\n').Length; i++)
                {
                    BatchEmployeeNo = BatchEmployeeNo + str.Split('\n')[i].Trim() + "§";
                }
            }
            RealapplyModel model = PageHelper.GetModel<RealapplyModel>(pnlContent.Controls);
            model.ConfirmHours = null;
            model.OverTimeType = ddlPersonType.SelectedValue;
            DataTable dt = bll.GetRealapply(model, pager.CurrentPageIndex, pager.PageSize, out totalCount, ddlHoursCondition.SelectedValue, txtHours.Text.Trim(), txtOTDateFrom.Text.Trim(), txtOTDateTo.Text.Trim(), BatchEmployeeNo, base.SqlDep);
            pager.RecordCount = totalCount;
            dt_global = dt;
            DataBind(dt);
        }
        #endregion

        #region 導出報表
        protected void btnExport_Click(object sender, EventArgs e)
        {
            RealapplyModel model = PageHelper.GetModel<RealapplyModel>(pnlContent.Controls);
            List<RealapplyModel> list = bll.GetList(dt_global);
            string[] header = { ControlText.gvHeadDepName, ControlText.gvHeadWorkNo, ControlText.gvHeadLocalName, ControlText.gvHeadOTDate, ControlText.gvHeadOTType, ControlText.gvHeadWeek, ControlText.gvHeadOverTimeType, ControlText.gvHeadShiftDesc, ControlText.gvHeadAdvanceTime, ControlText.gvHeadAdvanceHours, ControlText.gvHeadOverTimeSpan, ControlText.gvHeadRealHours, ControlText.gvHeadConfirmHours, ControlText.gvHeadConfirmRemark, ControlText.gvHeadWorkDesc, ControlText.gvHeadIsProject, ControlText.gvHeadISPay, ControlText.gvHeadStatusName, ControlText.gvHeadG1Total, ControlText.gvHeadG2Total, ControlText.gvHeadG3Total, ControlText.gvHeadRemark, ControlText.gvHeadBillNo, ControlText.gvHeadApproverName, ControlText.gvHeadApproveDate, ControlText.gvHeadApRemark, ControlText.gvHeadupdateuser, ControlText.gvHeadupdatedate };
            string[] properties = { "DepName", "EmployeeNo", "Name", "OTDate", "OTType", "Week", "OverTimeType", "ShiftDesc", "AdvanceTime", "AdvanceHours", "OverTimeSpan", "RealHours", "ConfirmHours", "ConfirmRemark", "WorkDesc", "IsProject", "ISPay", "StatusName", "G1Total", "G2Total", "G3Total", "Remark", "BillNo", "ApproverName", "ApproveDate", "ApRemark", "update_user", "update_date" };
            string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
            NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
            PageHelper.ReturnHTTPStream(filePath, true);

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
            logmodel.ProcessFlag = "delete";
            RealapplyModel model = PageHelper.GetModel<RealapplyModel>(pnlContent.Controls);
            int intDeleteOk = 0;
            int intDeleteError = 0;
            TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGrid.Bands[0].Columns[0];
            for (int i = 0; i < this.UltraWebGrid.Rows.Count; i++)
            {
                CellItem GridItem = (CellItem)tcol.CellItems[i];
                CheckBox chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                if (chkIsHaveRight.Checked)
                {
                    int num = bll.DeleteRealapplyByKey(this.UltraWebGrid.Rows[i].Cells.FromKey("ID").Text.Trim(),logmodel);
                    if (num > 0)
                    {
                        intDeleteOk++;
                    }
                    else
                    {
                        intDeleteError++;
                    }
                }
            }
            if ((intDeleteOk + intDeleteError) > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.DeleteSuccess + ":" + intDeleteOk + ";" + Message.DeleteFailed + ":" + intDeleteError + "');", true);
                DataBind();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.AtLastOneChoose + "');", true);
                return;
            }
        }
        #endregion

        #region 設置Selector
        /// <summary>
        /// 設置Selector
        /// </summary>
        /// <param name="ctrlTrigger">控件ID--按鈕</param>
        /// <param name="ctrlCode">控件ID--文本框1</param>
        /// <param name="ctrlName">控件ID--文本框2</param>
        /// <param name="flag">Selector區分標誌</param>
        public static void SetSelector(WebControl ctrlTrigger, Control ctrlCode, Control ctrlName, string flag, string moduleCode)
        {
            if (ctrlCode is TextBox) { (ctrlCode as TextBox).Attributes.Add("readonly", "readonly"); }
            if (ctrlName is TextBox) { (ctrlName as TextBox).Attributes.Add("readonly", "readonly"); }
            ctrlTrigger.Attributes.Add("onclick", string.Format("return setSelector('{0}','{1}','{2}','{3}')",
                ctrlCode.ClientID, ctrlName.ClientID, flag, moduleCode));
        }

        #endregion
    }
}
