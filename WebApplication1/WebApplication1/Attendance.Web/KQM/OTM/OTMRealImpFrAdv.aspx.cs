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
 * 檔案名： OTMRealImpFrAdv.aspx.cs
 * 檔功能描述： 有效加班--預報轉入
 * 
 * 版本：1.0
 * 創建標識： 昝望 2011.12.23
 * 
 */

namespace GDSBG.MiABU.Attendance.Web.KQM.OTM
{
    public partial class OTMRealImpFrAdv : BasePage
    {
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        RealapplyBll bll = new RealapplyBll();
        static DataTable dt_global = new DataTable();
        static SynclogModel logmodel = new SynclogModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            SetCalendar(txtOTDateFrom, txtOTDateTo);
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
                ClientMessage.Add("InportRealApplyConfirm", Message.InportRealApplyConfirm);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
            if (!IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = "SYS04";
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;
                DataBind(dt_global);
                SetSelector(ImageDepCode, txtDepCode, txtDepName, "DepCode", Request.QueryString["ModuleCode"].ToString());
            }
        }


        #region 綁定數據
        /// <summary>
        /// 綁定數據
        /// </summary>

        private void DataBind()
        {
            this.ddlImportType.Items.Add(new ListItem(Message.ImportTypeInfo, "1"));
            this.ddlIsProject.Items.Insert(0, new ListItem("", ""));
            this.ddlIsProject.Items.Insert(1, new ListItem("Y", "Y"));
            this.ddlIsProject.Items.Insert(2, new ListItem("N", "N"));
            this.txtOTDateFrom.Text = DateTime.Now.AddDays(-1.0).ToShortDateString();
            this.txtOTDateTo.Text = DateTime.Now.ToShortDateString();
            dt_global = bll.SelectAdvanceapply(txtDepCode.Text.Trim(), ddlIsProject.SelectedValue, txtEmployeeNo.Text.Trim(), txtName.Text.Trim(), txtOTDateFrom.Text.Trim(), txtOTDateTo.Text.Trim(), base.SqlDep);
            this.UltraWebGrid.DataSource = dt_global;
            this.UltraWebGrid.DataBind();
        }


        private void DataBind(DataTable dt)
        {
            this.ddlImportType.Items.Add(new ListItem(Message.ImportTypeInfo, "1"));
            this.ddlIsProject.Items.Insert(0, new ListItem("", ""));
            this.ddlIsProject.Items.Insert(1, new ListItem("Y", "Y"));
            this.ddlIsProject.Items.Insert(2, new ListItem("N", "N"));
            this.txtOTDateFrom.Text = DateTime.Now.AddDays(-1.0).ToShortDateString();
            this.txtOTDateTo.Text = DateTime.Now.ToShortDateString();
            this.UltraWebGrid.DataSource = dt;
            this.UltraWebGrid.DataBind();
        }

        #endregion
        #region
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            dt_global = bll.SelectAdvanceapply(txtDepCode.Text.Trim(), ddlIsProject.SelectedValue, txtEmployeeNo.Text.Trim(), txtName.Text.Trim(), txtOTDateFrom.Text.Trim(), txtOTDateTo.Text.Trim(),base.SqlDep);
            DataBind(dt_global);

        }
        #endregion

        #region
        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.txtEmployeeNo.Text = "";
            this.txtName.Text = "";
            this.txtDepCode.Text = "";
            this.txtDepName.Text = "";
            this.txtOTDateFrom.Text = DateTime.Now.AddDays(-2.0).ToShortDateString();
            this.txtOTDateTo.Text = DateTime.Now.AddDays(-1.0).ToShortDateString();
        }
        #endregion

        #region
        protected void btnExport_Click(object sender, EventArgs e)
        {
            RealapplyModel model = new RealapplyModel();
            List<RealapplyModel> list = bll.GetList(dt_global);
            string[] header = { ControlText.gvHeadDepName, ControlText.gvHeadWorkNo, ControlText.gvHeadLocalName, ControlText.gvHeadOTDate, ControlText.gvHeadOverTimeType, ControlText.gvHeadWeek, ControlText.gvHeadOTType, ControlText.gvHeadBeginTime, ControlText.gvHeadEndTime, ControlText.gvHeadAdvanceHours, ControlText.gvHeadWorkDesc, ControlText.gvHeadImportRemark, ControlText.gvHeadStatusName, ControlText.gvHeadBillNo };
            string[] properties = { "DepName", "EmployeeNo", "Name", "OTDate", "OverTimeType", "Week", "OTType", "BeginTime", "EndTime", "AdvanceHours", "WorkDesc", "ImportRemark", "StatusName", "BillNo" };
            string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
            NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
            PageHelper.ReturnHTTPStream(filePath, true);

        }
        #endregion

        #region
        protected void btnInportRealApply_Click(object sender, EventArgs e)
        {
            int intDeleteOk = 0;
            int intDeleteError = 0;
            logmodel.ProcessFlag = "update";
            TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGrid.Bands[0].Columns[0];
            for (int i = 0; i < this.UltraWebGrid.Rows.Count; i++)
            {
                CellItem GridItem = (CellItem)tcol.CellItems[i];
                CheckBox chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                if (chkIsHaveRight.Checked)
                {
                    int num = bll.UpdateAdvanceapply(this.UltraWebGrid.Rows[i].Cells.FromKey("ID").Text.Trim(), CurrentUserInfo.Personcode,logmodel);
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
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.SuccssedCount + ":" + intDeleteOk + ";" + Message.FailedCounts + ":" + intDeleteError + "');", true);
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
                ctrlCode.ClientID, ctrlName.ClientID, flag,moduleCode));
        }

        #endregion
    }
}
