/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OTMMonthTotalForm.cs
 * 檔功能描述： 月加班匯總頁面
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2012.02.26
 * 
 */
using System;
using System.Data;
using System.Web.UI;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Resources;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.KQM.BasicData;
using UNLV.IAP.WebControls;
using Infragistics.WebUI.UltraWebGrid;
using System.Drawing;
using GDSBG.MiABU.Attendance.Model.KQM.Query.OTM;
using GDSBG.MiABU.Attendance.BLL.KQM.Query.OTM;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.SS.Util;
using GDSBG.MiABU.Attendance.BLL.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.Web.KQM.OTM
{
    public partial class OTMMonthTotalForm : BasePage
    {
        DataTable tempDataTable = new DataTable();
        TypeDataBll bllTypeData = new TypeDataBll();
        DataTable tempDt = new DataTable();
        static DataTable dt = new DataTable();
        OTMTotalQryModel model = new OTMTotalQryModel();
        OTMTotalQryBll bllOTMQry = new OTMTotalQryBll();
        SynclogModel logmodel = new SynclogModel();
        PersonBll bllPerson = new PersonBll();
        DataSet dataSet = new DataSet();
        DataTable importDataTable = new DataTable();
        #region 放大鏡綁定
        /// <summary>
        /// 設置Selector
        /// </summary>
        /// <param name="ctrlTrigger">控件ID--按鈕</param>
        /// <param name="ctrlCode">控件ID--文本框1</param>
        /// <param name="ctrlName">控件ID--文本框2</param>
        public void SetSelector(WebControl ctrlTrigger, Control ctrlCode, Control ctrlName, string moduleCode)
        {
            if (ctrlCode is TextBox) { (ctrlCode as TextBox).Attributes.Add("readonly", "readonly"); }
            if (ctrlName is TextBox) { (ctrlName as TextBox).Attributes.Add("readonly", "readonly"); }
            ctrlTrigger.Attributes.Add("onclick", string.Format("return setSelector('{0}','{1}','{2}')",
                ctrlCode.ClientID, ctrlName.ClientID, moduleCode));
        }
        #endregion
        #region  頁面加載
        protected void Page_Load(object sender, EventArgs e)
        {
            JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
            Dictionary<string, string> ClientMessage = null;
            HiddenModuleCode.Value = Request.QueryString["ModuleCode"];
            if (!base.IsPostBack)
            {
                SetSelector(imgDepCode, txtDepCode, txtDepName, Request.QueryString["ModuleCode"].ToString());
                this.txtYearMonth.Text = DateTime.Now.ToString("yyyy/MM");
                txtApproveDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
                this.ddlIsSupporter.Items.Insert(0, new ListItem("", ""));
                this.ddlIsSupporter.Items.Insert(1, new ListItem("Y", "Y"));
                this.ddlIsSupporter.Items.Insert(2, new ListItem("N", "N"));
                this.BindDropDownList(this.ddlEmpStatus, "EmpState");
                this.BindDropDownList(this.ddlOTTypeCode, "OverTimeType");
                this.BindDropDownList(this.ddlApproveFlag, "ApproveFlag");
                this.imgBatchWorkNo.Attributes.Add("OnClick", "javascript:ShowBatchWorkNo()");
            }
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
                ClientMessage.Add("DeleteFailed", Message.DeleteFailed);
                ClientMessage.Add("DeleteSuccess", Message.DeleteSuccess);
                ClientMessage.Add("TextBoxNotNull", Message.TextBoxNotNull);
                ClientMessage.Add("DeleteConfirm", Message.DeleteConfirm);
                ClientMessage.Add("btnImport", Message.btnImport);
                ClientMessage.Add("btnBack", Message.btnBack);
                ClientMessage.Add("UnAudit", Message.UnAudit);
                ClientMessage.Add("UnSendAudit", Message.UnSendAudit);
                ClientMessage.Add("NoSelect", Message.NoSelect);
                ClientMessage.Add("ConfirmReturn", Message.ConfirmReturn);
                ClientMessage.Add("UnCancelAudit", Message.UnCancelAudit);
                ClientMessage.Add("OnlyNoAudit", Message.OnlyNoAudit);
                ClientMessage.Add("OnlyNoCanModify", Message.OnlyNoCanModify);
                ClientMessage.Add("ErrReturnTimeWrong", Message.ErrReturnTimeWrong);
                ClientMessage.Add("WrongDate", Message.WrongDate);
                ClientMessage.Add("DepCodeNotNull", Message.DepCodeNotNull);
                ClientMessage.Add("billno_copy_success", Message.billno_copy_success);
                ClientMessage.Add("CanOnlyChooseOne", Message.CanOnlyChooseOne);

            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
        }
        #endregion

        #region 下拉菜單綁定
        protected void BindDropDownList(DropDownCheckList dlist, string DataTypeValue)
        {
            tempDt = bllTypeData.GetdllDateTypeList(DataTypeValue);
            dlist.DataSource = tempDt.DefaultView;
            dlist.DataTextField = "DataValue";
            dlist.DataValueField = "DataCode";
            dlist.DataBind();
            tempDt.Clear();
        }
        #endregion
        #region  WebGrid綁定初始化
        protected void UltraWebGridOTMMonthTotal_InitializeLayout(object sender, LayoutEventArgs e)
        {
            int i;
            DateTime YearMonth = Convert.ToDateTime(this.txtYearMonth.Text + "/01");
            if (this.HiddenYearMonth.Value.Length == 0)
            {
                this.HiddenYearMonth.Value = this.txtYearMonth.Text.Replace("/", "");
                foreach (UltraGridColumn c in e.Layout.Bands[0].Columns)
                {
                    c.Header.RowLayoutColumnInfo.OriginY = 1;
                }
                ColumnHeader ch = new ColumnHeader(true)
                {
                    Caption = Resources.ControlText.gvApplyHours
                };
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch.RowLayoutColumnInfo.OriginX = 8;
                ch.RowLayoutColumnInfo.SpanX = 3;
                ch.Style.HorizontalAlign = HorizontalAlign.Center;
                e.Layout.Bands[0].HeaderLayout.Add(ch);
                ColumnHeader ch2 = new ColumnHeader(true)
                {
                    Caption = Resources.ControlText.gvRelSalaryHours
                };
                ch2.RowLayoutColumnInfo.OriginY = 0;
                ch2.RowLayoutColumnInfo.OriginX = 11;
                ch2.RowLayoutColumnInfo.SpanX = 3;
                ch2.Style.HorizontalAlign = HorizontalAlign.Center;
                e.Layout.Bands[0].HeaderLayout.Add(ch2);
                ColumnHeader ch3 = new ColumnHeader(true)
                {
                    Caption = Resources.ControlText.gvSpecApplyHours
                };
                ch3.RowLayoutColumnInfo.OriginY = 0;
                ch3.RowLayoutColumnInfo.OriginX = 14;
                ch3.RowLayoutColumnInfo.SpanX = 3;
                ch3.Style.HorizontalAlign = HorizontalAlign.Center;
                e.Layout.Bands[0].HeaderLayout.Add(ch3);
                ColumnHeader ch4 = new ColumnHeader(true)
                {
                    Caption = Resources.ControlText.gvSpecrelSalary
                };
                ch4.RowLayoutColumnInfo.OriginY = 0;
                ch4.RowLayoutColumnInfo.OriginX = 17;
                ch4.RowLayoutColumnInfo.SpanX = 3;
                ch4.Style.HorizontalAlign = HorizontalAlign.Center;
                e.Layout.Bands[0].HeaderLayout.Add(ch4);
                for (i = 1; i < 0x20; i++)
                {
                    ColumnHeader chr = new ColumnHeader(true)
                    {
                        Caption = i.ToString()
                    };
                    chr.RowLayoutColumnInfo.OriginY = 0;
                    chr.RowLayoutColumnInfo.OriginX = i + 0x1D;
                    chr.RowLayoutColumnInfo.SpanX = 1;
                    chr.Style.HorizontalAlign = HorizontalAlign.Center;
                    e.Layout.Bands[0].HeaderLayout.Add(chr);
                }
                ch = e.Layout.Bands[0].Columns.FromKey("CheckBoxAll").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch = e.Layout.Bands[0].Columns.FromKey("WorkNo").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch = e.Layout.Bands[0].Columns.FromKey("BillNo").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch = e.Layout.Bands[0].Columns.FromKey("LocalName").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch = e.Layout.Bands[0].Columns.FromKey("BuName").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch = e.Layout.Bands[0].Columns.FromKey("DName").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch = e.Layout.Bands[0].Columns.FromKey("OverTimeType").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch = e.Layout.Bands[0].Columns.FromKey("G2Remain").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch = e.Layout.Bands[0].Columns.FromKey("MAdjust1").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch = e.Layout.Bands[0].Columns.FromKey("MRelAdjust").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch = e.Layout.Bands[0].Columns.FromKey("ApproveFlagName").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch = e.Layout.Bands[0].Columns.FromKey("AdvanceAdjust").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch = e.Layout.Bands[0].Columns.FromKey("RestAdjust").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch = e.Layout.Bands[0].Columns.FromKey("auditer").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch = e.Layout.Bands[0].Columns.FromKey("auditdate").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch = e.Layout.Bands[0].Columns.FromKey("auditidea").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch = e.Layout.Bands[0].Columns.FromKey("jindutu").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
            }
            else
            {
                this.HiddenYearMonth.Value = this.txtYearMonth.Text.Replace("/", "");
            }
            string FromKeyName = "";
            for (i = 1; i < 0x20; i++)
            {
                FromKeyName = "Day" + i.ToString();
                e.Layout.Bands[0].Columns.FromKey(FromKeyName).Header.Caption = this.GetWeek(YearMonth.AddDays((double)(i - 1)));
                if (this.isHoliday(i, YearMonth))
                {
                    e.Layout.Rows.Band.Columns[e.Layout.Bands[0].Columns.FromKey(FromKeyName).Index].CellStyle.BackColor = Color.DarkKhaki;
                }
                else
                {
                    e.Layout.Rows.Band.Columns[e.Layout.Bands[0].Columns.FromKey(FromKeyName).Index].CellStyle.BackColor = Color.White;
                }
            }
        }
        #endregion
        #region  判斷是否節假日
        public bool isHoliday(int day, DateTime date)
        {
            bool bValue = false;
            switch (Convert.ToInt32(date.AddDays((double)(day - 1)).DayOfWeek))
            {
                case 0:
                case 6:
                    bValue = true;
                    break;
            }
            return bValue;
        }
        #endregion
        #region 判斷星期幾
        private string GetWeek(DateTime SelectYearMonth)
        {
            string getWeek = SelectYearMonth.DayOfWeek.ToString();
            switch (getWeek)
            {
                case "Sunday":
                    return "日";

                case "Monday":
                    return "一";

                case "Tuesday":
                    return "二";

                case "Wednesday":
                    return "三";

                case "Thursday":
                    return "四";

                case "Friday":
                    return "五";

                case "Saturday":
                    return "六";
            }
            return "";
        }
        #endregion
        #region WebGrid綁定
        protected void UltraWebGridOTMMonthTotal_DataBound(object sender, EventArgs e)
        {
            decimal Normal = 0M;
            decimal Spec = 0M;
            for (int i = 0; i < this.UltraWebGridOTMMonthTotal.Rows.Count; i++)
            {
                int k;
                if (this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("ApproveFlag").Text.Trim() == "0")
                {
                    this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("ApproveFlagName").Style.ForeColor = Color.Green;
                }
                if (this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("ApproveFlag").Text.Trim() == "1")
                {
                    this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("ApproveFlagName").Style.ForeColor = Color.Blue;
                }
                if (this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("ApproveFlag").Text.Trim() == "2")
                {
                    this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("ApproveFlagName").Style.ForeColor = Color.Black;
                }
                if (this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("ApproveFlag").Text.Trim() == "3")
                {
                    this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("ApproveFlagName").Style.ForeColor = Color.Maroon;
                }
                if (this.QueryFlag.Value == "Spec")
                {
                    k = 1;
                    while (k < 0x20)
                    {
                        Spec = Convert.ToDecimal((Convert.ToString(this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("SpecDay" + k.ToString()).Text) == "") ? "0" : Convert.ToString(this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("SpecDay" + k.ToString()).Text));
                        //this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("Day" + k.ToString()).Text = this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("SpecDay" + k.ToString()).Text;
                        this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("Day" + k.ToString()).Text = Convert.ToString((decimal)(Spec));
                        k++;
                    }
                }
                if (this.QueryFlag.Value == "All")
                {
                    for (k = 1; k < 0x20; k++)
                    {
                        Normal = Convert.ToDecimal((Convert.ToString(this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("Day" + k.ToString()).Text) == "") ? "0" : Convert.ToString(this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("Day" + k.ToString()).Text));
                        Spec = Convert.ToDecimal((Convert.ToString(this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("SpecDay" + k.ToString()).Text) == "") ? "0" : Convert.ToString(this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("SpecDay" + k.ToString()).Text));
                        this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("Day" + k.ToString()).Text = Convert.ToString((decimal)(Normal + Spec));
                    }
                }
                else
                {
                    for (k = 1; k < 0x20; k++)
                    {
                        Normal = Convert.ToDecimal((Convert.ToString(this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("Day" + k.ToString()).Text) == "") ? "0" : Convert.ToString(this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("Day" + k.ToString()).Text));
                        this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("Day" + k.ToString()).Text = Convert.ToString((decimal)(Normal));
                        //this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("Day" + k.ToString()).Text =(Convert.ToString(this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("Day" + k.ToString()).Text) == "") ? "0" : Convert.ToString(this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("Day" + k.ToString()).Text);
                    }
                }
            }
        }
        #endregion
        #region 查询
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            this.QueryFlag.Value = "All";
            model = PageHelper.GetModel<OTMTotalQryModel>(pnlContent.Controls);
            pager.CurrentPageIndex = 1;
            DataUIBind();
            this.txtBatchEmployeeNo.Text = "";
        }
        #endregion
        #region 綁定數據
        private void DataUIBind()
        {
            string BatchEmployeeNo = "";
            string overTimeType = "";
            string approveFlag = "";
            string empStatus = "";
            int totalCount;
            string SQLDep = base.SqlDep;
            if (this.txtYearMonth.Text.Length == 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.YearMonthNotNull + "')", true);
            }
            else
            {
                string YearMonth = this.txtYearMonth.Text.Replace("/", "");
                string ddlStr = "";
                string str = txtBatchEmployeeNo.Text.Trim();
                if (str != "")
                {
                    for (int i = 0; i < str.Split('\n').Length; i++)
                    {
                        BatchEmployeeNo = BatchEmployeeNo + str.Split('\n')[i].Trim() + "§";
                    }
                }
                string strTemVal = this.ddlOTTypeCode.SelectedValue;
                if (strTemVal != "")
                {
                    string temStr = this.ddlOTTypeCode.SelectedValuesToString(",");
                    for (int i = 0; i < temStr.Split(',').Length; i++)
                    {
                        overTimeType = overTimeType + temStr.Split(',')[i].Trim() + "§";
                    }
                }
                string strVal = this.ddlApproveFlag.SelectedValue;
                if (strVal != "")
                {
                    string temStr = this.ddlApproveFlag.SelectedValuesToString(",");
                    for (int i = 0; i < temStr.Split(',').Length; i++)
                    {
                        approveFlag = approveFlag + temStr.Split(',')[i].Trim() + "§";
                    }
                }
                ddlStr = this.ddlEmpStatus.SelectedValue;
                if (ddlStr != "")
                {
                    string temVal = this.ddlEmpStatus.SelectedValuesToString(",");
                    for (int i = 0; i < temVal.Split(',').Length; i++)
                    {
                        empStatus = empStatus + temVal.Split(',')[i].Trim() + "§";
                    }
                }
                model.Flag = ddlIsSupporter.SelectedValue;
                string depCode = model.DepCode;
                string flag = model.Flag;
                model.Flag = null;
                model.YearMonth = YearMonth;
                model.DepCode = null;
                model.DName = null;
                DataTable dtSel = bllOTMQry.GetOTMQryList(model, flag, depCode, SQLDep, BatchEmployeeNo, overTimeType, approveFlag, empStatus, pager.CurrentPageIndex, pager.PageSize, out totalCount);
                dt = bllOTMQry.GetOTMQryList(model, flag, depCode, SQLDep, BatchEmployeeNo, overTimeType, approveFlag, empStatus);
                this.UltraWebGridOTMMonthTotal.DataSource = dtSel;
                this.UltraWebGridOTMMonthTotal.DataBind();
                pager.RecordCount = totalCount;
                pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
            }
        }
        #endregion
        #region  正常加班查詢
        protected void btnNormalQuery_Click(object sender, EventArgs e)
        {
            this.QueryFlag.Value = "Normal";
            this.OTMmsg.Text = "(" + Resources.ControlText.btnNormalQuery + ")";
            model = PageHelper.GetModel<OTMTotalQryModel>(pnlContent.Controls);
            pager.CurrentPageIndex = 1;
            DataUIBind();
            this.txtBatchEmployeeNo.Text = "";
            this.OTMmsg.Text = "";
        }
        #endregion
        #region  專案加班查詢 （查詢條件一樣 只是顯示格式不同）
        protected void btnSpecQuery_Click(object sender, EventArgs e)
        {
            this.QueryFlag.Value = "Spec";
            this.OTMmsg.Text = "(" + Resources.ControlText.btnSpecQuery + ")";
            model = PageHelper.GetModel<OTMTotalQryModel>(pnlContent.Controls);
            pager.CurrentPageIndex = 1;
            DataUIBind();
            this.txtBatchEmployeeNo.Text = "";
            this.OTMmsg.Text = "";
        }
        #endregion

        #region  重置
        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.txtDepCode.Text = "";
            this.txtDepName.Text = "";
            this.txtWorkNo.Text = "";
            this.txtLocalName.Text = "";
            this.ddlOTTypeCode.ClearSelection();
            this.ddlApproveFlag.ClearSelection();
            this.txtYearMonth.Text = DateTime.Now.ToString("yyyy/MM");
            this.ddlIsSupporter.ClearSelection();
        }
        #endregion
        #region 导入按钮页面的交换
        protected void btnImport_Click(object sender, EventArgs e)
        {
            this.UltraWebGridImport.Rows.Clear();
            if (!this.PanelImport.Visible)
            {
                this.PanelImport.Visible = true;
                this.PanelData.Visible = false;
                this.btnQuery.Enabled = false;
                this.btnReset.Enabled = false;
                this.btnModify.Enabled = false;
                this.btnAudit.Enabled = false;
                this.btnAuditAll.Enabled = false;
                this.btnCancelAudit.Enabled = false;
                this.btnOrgRecal.Enabled = false;
                this.btnRecalculate.Enabled = false;
                this.btnNormalQuery.Enabled = false;
                this.btnSpecQuery.Enabled = false;
                this.btnBatchCancelAudit.Enabled = false;
                this.btnImport.Text = Message.btnBack;
                this.lbluploadMsg.Text = "";
            }
            else
            {
                this.PanelImport.Visible = false;
                this.PanelData.Visible = true;
                this.btnQuery.Enabled = true;
                this.btnReset.Enabled = true;
                this.btnModify.Enabled = true;
                this.btnAudit.Enabled = true;
                this.btnAuditAll.Enabled = true;
                this.btnCancelAudit.Enabled = true;
                this.btnOrgRecal.Enabled = true;
                this.btnRecalculate.Enabled = true;
                this.btnNormalQuery.Enabled = true;
                this.btnSpecQuery.Enabled = true;
                this.btnBatchCancelAudit.Enabled = true;
                this.btnImport.Text = Message.btnImport;
            }
        }
        #endregion
        #region 核准的保存按鈕
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.UltraWebGridOTMMonthTotal.Rows.Count != 0)
            {
                int i;
                int intDeleteOk = 0;
                int intDeleteError = 0;
                logmodel.ProcessFlag = "update";
                TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGridOTMMonthTotal.Bands[0].Columns[0];
                for (i = 0; i < this.UltraWebGridOTMMonthTotal.Rows.Count; i++)
                {
                    if (((tcol.CellItems[i] as CellItem).FindControl("CheckBoxCell") as CheckBox).Checked)
                    {
                        string workNo = this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("WorkNo").Text.Trim();
                        string yearMonth = this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("YearMonth").Text.Trim();
                        string status = "2";
                        //model.ApproveDate = Convert.ToDateTime(txtApproveDate.Text.ToString());
                        model.Approver = txtApprover.Text.ToString();
                        int flag = bllOTMQry.UpdateMonthTal(workNo, yearMonth, status, model, logmodel);
                        if (flag == 1)
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
                    string alertText = Message.AuditSucccess + "：" + intDeleteOk + "," + Message.AuditFaile + "：" + intDeleteError;
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + alertText + "')", true);
                }
                model = new OTMTotalQryModel();
                DataUIBind();
                this.ProcessFlag.Value = "";
            }
        }
        #endregion
        #region   取消核准
        protected void btnCancelAudit_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "update";
            if (this.UltraWebGridOTMMonthTotal.Rows.Count != 0)
            {
                int i;
                int intDeleteOk = 0;
                int intDeleteError = 0;
                CellItem GridItem;
                CheckBox chkIsHaveRight;
                TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGridOTMMonthTotal.Bands[0].Columns[0];
                for (i = 0; i < this.UltraWebGridOTMMonthTotal.Rows.Count; i++)
                {
                    GridItem = (CellItem)tcol.CellItems[i];
                    chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                    if (chkIsHaveRight.Checked)
                    {
                        string workNo = this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("WorkNo").Text.Trim();
                        string yearMonth = this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("YearMonth").Text.Trim();
                        string status = "0";
                        model.Approver = "";
                        model.ApproveDate = null;
                        int flag = bllOTMQry.UpdateMonthTal(workNo, yearMonth, status, model, logmodel);
                        if (flag == 1)
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
                    string alertText = Message.CancelAuditSucccess + "：" + intDeleteOk + "," + Message.CancelAuditFaile + "：" + intDeleteError;
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + alertText + "')", true);
                }
                model = new OTMTotalQryModel();
                DataUIBind();
                this.ProcessFlag.Value = "";
            }
        }
        #endregion

        #region 導出功能
        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (this.UltraWebGridOTMMonthTotal.Rows.Count == 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert(" + Message.NoDataExport + ")", true);
            }
            else
            {
                int i;
                if (this.QueryFlag.Value == "All")
                {
                    this.tempDataTable = dt;
                }
                this.UltraWebGridOTMMonthTotal.Bands[0].Columns[0].Hidden = true;
                DateTime YearMonth = Convert.ToDateTime(this.txtYearMonth.Text + "/01");
                int DaysInMonth = DateTime.DaysInMonth(YearMonth.Year, YearMonth.Month);
                switch (DaysInMonth)
                {
                    case 0x1c:
                        this.UltraWebGridOTMMonthTotal.Bands[0].Columns.FromKey("Day29").Hidden = true;
                        this.UltraWebGridOTMMonthTotal.Bands[0].Columns.FromKey("Day30").Hidden = true;
                        this.UltraWebGridOTMMonthTotal.Bands[0].Columns.FromKey("Day31").Hidden = true;
                        break;

                    case 0x1d:
                        this.UltraWebGridOTMMonthTotal.Bands[0].Columns.FromKey("Day29").Hidden = false;
                        this.UltraWebGridOTMMonthTotal.Bands[0].Columns.FromKey("Day30").Hidden = true;
                        this.UltraWebGridOTMMonthTotal.Bands[0].Columns.FromKey("Day31").Hidden = true;
                        break;

                    case 30:
                        this.UltraWebGridOTMMonthTotal.Bands[0].Columns.FromKey("Day29").Hidden = false;
                        this.UltraWebGridOTMMonthTotal.Bands[0].Columns.FromKey("Day30").Hidden = false;
                        this.UltraWebGridOTMMonthTotal.Bands[0].Columns.FromKey("Day31").Hidden = true;
                        break;

                    case 0x1f:
                        this.UltraWebGridOTMMonthTotal.Bands[0].Columns.FromKey("Day29").Hidden = false;
                        this.UltraWebGridOTMMonthTotal.Bands[0].Columns.FromKey("Day30").Hidden = false;
                        this.UltraWebGridOTMMonthTotal.Bands[0].Columns.FromKey("Day31").Hidden = false;
                        break;
                }
                string tmpFilePath = MapPath("~/ExcelModel/OTMMonthTotal.xls");
                string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                string yearMonth = txtYearMonth.Text.Trim().ToString();
                HSSFWorkbook hssfworkbook;
                using (Stream fileStream = new FileStream(tmpFilePath, FileMode.Open))
                {
                    hssfworkbook = new HSSFWorkbook(fileStream);
                }
                Sheet insertSheet = hssfworkbook.GetSheet("Sheet1");

                int days = System.Threading.Thread.CurrentThread.CurrentUICulture.Calendar.GetDaysInMonth(YearMonth.Year, YearMonth.Month);

                insertSheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, 22 + days));
                insertSheet = GetHead(insertSheet, 1, 23, yearMonth);
                insertSheet = LoadData(insertSheet, 3);
                Row headRow = insertSheet.CreateRow(0);
                for (int d = 0; d < 23 + days; d++)
                {
                    headRow.CreateCell(d);
                }
                string cellValue = string.Concat(new object[] { this.txtDepName.Text.Replace("→", ""), YearMonth.Month, "月加班匯總表", this.OTMmsg.Text });
                insertSheet.GetRow(0).GetCell(0).CellStyle.Alignment = HorizontalAlignment.CENTER;
                insertSheet.GetRow(0).GetCell(0).SetCellValue(cellValue);
                OutExport(hssfworkbook, filePath);
                PageHelper.ReturnHTTPStream(filePath, true);
            }
        }
        /// <summary>
        /// 加載表頭
        /// </summary>
        private Sheet GetHead(Sheet tmpSheet, int startRowHeader, int col, string yearMonth)
        {
            DateTime date = Convert.ToDateTime(yearMonth + "/01");
            DateTime weekDate = Convert.ToDateTime(yearMonth + "/01");
            int days = System.Threading.Thread.CurrentThread.CurrentUICulture.Calendar.GetDaysInMonth(date.Year, date.Month);
            Row headRow = tmpSheet.CreateRow(0);

            for (int d = 1; d <= days; d++)
            {
                tmpSheet.GetRow(startRowHeader).CreateCell(col + d - 1).SetCellValue(d.ToString());
                weekDate = Convert.ToDateTime(yearMonth + "/" + d.ToString());
                DateTime str = Convert.ToDateTime(yearMonth + "/" + d.ToString());
                // string weekDay = GetWeekstr(yearMonth + "/" + d.ToString());
                string weekDay = GetWeek(str);
                tmpSheet.GetRow(startRowHeader + 1).CreateCell(col + d - 1).SetCellValue(weekDay.ToString());
            }
            return tmpSheet;
        }
        /// <summary>
        /// 加載數據
        /// </summary>
        private Sheet LoadData(Sheet tmpSheet, int startRow)
        {
            UltraWebGrid uwgExport = new UltraWebGrid();
            if (this.PanelData.Visible == true)
            {
                uwgExport = this.UltraWebGridOTMMonthTotal;
            }
            if (this.PanelImport.Visible == true)
            {
                uwgExport = this.UltraWebGridImport;
            }
            int nR = uwgExport.Rows.Count;
            //判斷數據控件中是否有數據
            if (nR > 0)
            {
                int nC = uwgExport.Columns.Count;
                //逐行加載數據
                for (int i = 0; i < nR; i++)
                {
                    Row row1 = tmpSheet.CreateRow(0);
                    Row row = tmpSheet.CreateRow(startRow + i);
                    //每行的每列加載數據
                    for (int j = 1; j < 24; j++)
                    {
                        // if (uwgExport.Columns[j].hi)
                        if (uwgExport.Rows[i].Cells[j].Value != null)
                        {
                            row.CreateCell(j - 1).SetCellValue(uwgExport.Rows[i].Cells[j].Value.ToString());
                        }
                        else
                        {
                            row.CreateCell(j - 1).SetCellValue("");
                        }
                    }
                    DateTime YearMonth = Convert.ToDateTime(this.txtYearMonth.Text + "/01");
                    int DaysInMonth = DateTime.DaysInMonth(YearMonth.Year, YearMonth.Month);
                    for (int tmp = 1; tmp < (DaysInMonth + 1); tmp++)
                    {
                        row.CreateCell(22 + tmp).SetCellValue(uwgExport.Rows[i].Cells.FromKey("Day" + tmp.ToString()).Value.ToString());
                    }
                }
            }
            return tmpSheet;
        }
        private void OutExport(HSSFWorkbook workBook, string filePath)
        {
            using (FileStream file = new FileStream(filePath, FileMode.Create))
            {
                workBook.Write(file);
                file.Flush();
                file.Close();
                workBook = null;
            }
        }
        #endregion

        #region  批量取消核准
        protected void btnBatchCancelAudit_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "update";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string status = "0";
                string workNo = dt.Rows[i]["WorkNo"].ToString();
                string yearMonth = dt.Rows[i]["YearMonth"].ToString();
                model.Approver = "";
                model.ApproveDate = null;
                int flag = bllOTMQry.UpdateMonthTal(workNo, yearMonth, status, model, logmodel);
            }
            model = new OTMTotalQryModel();
            DataUIBind();
            this.ProcessFlag.Value = "";
        }
        #endregion
        #region    核准全部
        protected void btnAuditAll_Click(object sender, EventArgs e)
        {
            string YearMonth = this.txtYearMonth.Text.Replace("/", "");
            string YearMonthNow = DateTime.Now.ToString("yyyyMM");
            logmodel.ProcessFlag = "update";
            if (YearMonth.Equals(YearMonthNow))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.NoDataAudit + "')", true);
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string status = "2";
                    string sqlDep = base.SqlDep;
                    model.Approver = CurrentUserInfo.Personcode;
                    string workNo = dt.Rows[i]["WorkNo"].ToString();
                    string yearMonth = dt.Rows[i]["YearMonth"].ToString();
                    int flag = bllOTMQry.UpdateMonthTal(workNo, yearMonth, status, model, logmodel);
                }
                model = new OTMTotalQryModel();
                DataUIBind();
                this.ProcessFlag.Value = "";
            }
        }
        #endregion
        #region  人員計算
        protected void btnRecalculate_Click(object sender, EventArgs e)
        {
            try
            {
                int i;
                CellItem GridItem;
                CheckBox chkIsHaveRight;
                int intConfirmOk = 0;
                int intConfirmError = 0;
                TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGridOTMMonthTotal.Bands[0].Columns[0];
                for (i = 0; i < this.UltraWebGridOTMMonthTotal.Rows.Count; i++)
                {
                    GridItem = (CellItem)tcol.CellItems[i];
                    chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                    if (chkIsHaveRight.Checked && (this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("ApproveFlag").Text.Trim() != "0"))
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert(''" + Message.UnAudit + "'')", true);
                        return;
                    }
                }
                for (i = 0; i < this.UltraWebGridOTMMonthTotal.Rows.Count; i++)
                {
                    GridItem = (CellItem)tcol.CellItems[i];
                    chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                    if (chkIsHaveRight.Checked)
                    {
                        model.WorkNo = this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("WorkNo").Text.Trim();
                        model.YearMonth = this.HiddenYearMonth.Value;
                        this.tempDataTable = bllOTMQry.GetOTMQryList(model);
                        if (this.tempDataTable.Rows.Count > 0)
                        {
                            bllOTMQry.CountCanAdjlasthy(this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("WorkNo").Text.Trim(), this.HiddenYearMonth.Value);
                            intConfirmOk++;
                        }
                        else
                        {
                            intConfirmError++;
                        }
                    }
                }
                if ((intConfirmOk + intConfirmError) > 0)
                {
                    string alertText = string.Concat(new object[] { Message.SuccessCount, "：", intConfirmOk, ";", Message.FaileCount, intConfirmError });
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + alertText + "')", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.AtLastOneChoose + "')", true);
                    return;
                }
                DataUIBind();
                this.ProcessFlag.Value = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region   組織計算
        protected void btnOrgRecal_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtDepCode.Text.Trim()))
                {
                    //base.WriteMessage(1, base.GetResouseValue("common.message.select.dept"));
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.DepCodeNotNull + "')", true);
                }
                else
                {
                    if (string.IsNullOrEmpty(this.HiddenYearMonth.Value))
                    {
                        this.HiddenYearMonth.Value = this.txtYearMonth.Text.Replace("/", "");
                    }
                    bllOTMQry.CountCanAdjlasthy("", this.HiddenYearMonth.Value, this.txtDepCode.Text.Trim());
                    this.ProcessFlag.Value = "Condition";
                    DataUIBind();
                    this.ProcessFlag.Value = "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region 上傳
        protected void btnImportSave_Click(object sender, EventArgs e)
        {
            try
            {
                btnImport.Text = Message.btnBack;
                string strFileName = this.FileUpload.FileName;
                string strFileSize = this.FileUpload.PostedFile.ContentLength.ToString();
                string strFileExt = strFileName.Substring(strFileName.LastIndexOf(".") + 1);
                string strTime = string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now);
                string strFilePath = Server.MapPath(@"\") + "ExportFileTemp" + "\\" + strTime + ".xls";
                // string strFilePath = base.Server.MapPath(@"..\") + @"Excel\UpLoad\" + strTime + ".xls";
                if (strFileExt == "xls")
                {
                    this.FileUpload.SaveAs(strFilePath);
                }
                else
                {
                    lblupload.Text = Message.IsNotExcel;
                    return;
                }
                this.dataSet.Clear();
                this.dataSet.Tables.Add("KQM_Import");
                this.dataSet.Tables["KQM_Import"].Columns.Add(new DataColumn("ErrorMsg", typeof(string)));
                this.dataSet.Tables["KQM_Import"].Columns.Add(new DataColumn("WorkNo", typeof(string)));
                this.dataSet.Tables["KQM_Import"].Columns.Add(new DataColumn("YearMonth", typeof(string)));
                this.dataSet.Tables["KQM_Import"].Columns.Add(new DataColumn("G2RelSalary", typeof(string)));
                this.dataSet.Tables["KQM_Import"].Columns.Add(new DataColumn("SpecG2RelSalary", typeof(string)));
                string WorkNo = "";
                string YearMonth = "";
                string G2RelSalary = "";
                string SpecG2RelSalary = "";
                string errorMsg = "";
                double G2Apply = 0.0;
                double SpecG2Apply = 0.0;
                int index = 0;
                int errorCount = 0;
                DataView dv = bllOTMQry.ExceltoDataView(strFilePath);
                int inttotal = dv.Table.Rows.Count;
                this.ProcessFlag.Value = "Modify";
                for (int i = 0; i < inttotal; i++)
                {
                    WorkNo = dv.Table.Rows[i][0].ToString().Trim().ToUpper();
                    YearMonth = dv.Table.Rows[i][1].ToString().Trim();
                    G2RelSalary = dv.Table.Rows[i][2].ToString().Trim();
                    SpecG2RelSalary = dv.Table.Rows[i][3].ToString().Trim();
                    G2RelSalary = (G2RelSalary.Length > 0) ? G2RelSalary : "-1";
                    SpecG2RelSalary = (SpecG2RelSalary.Length > 0) ? SpecG2RelSalary : "-1";
                    this.importDataTable = bllPerson.GetEmployeeInfo(WorkNo, SqlDep);
                    if (this.importDataTable.Rows.Count == 0)
                    {
                        if (errorMsg.Length > 0)
                        {
                            errorMsg = errorMsg + ",";
                        }
                        errorMsg = errorMsg + Message.WorkNoIsWrong;
                    }//工號有誤(工號不存在或已離職或不在權限內)
                    if (YearMonth.Length != 6)
                    {
                        if (errorMsg.Length > 0)
                        {
                            errorMsg = errorMsg + ",";
                        }
                        errorMsg = errorMsg + Message.ErrYearMonth;
                    }//年份格式不正確
                    else
                    {
                        try
                        {
                            Convert.ToDateTime(YearMonth.Substring(0, 4) + "/" + YearMonth.Substring(4, 2) + "/01").ToString("yyyy/MM/dd");
                        }
                        catch (Exception)
                        {
                            if (errorMsg.Length > 0)
                            {
                                errorMsg = errorMsg + ",";
                            }
                            errorMsg = errorMsg + Message.ErrYearMonth;
                        }
                    }//年份格式不正確
                    try
                    {
                        if (!IsDouble1(Convert.ToString(Convert.ToDecimal(G2RelSalary))))
                        {
                            if (errorMsg.Length > 0)
                            {
                                errorMsg = errorMsg + ",";
                            }
                            errorMsg = errorMsg + Message.bfw_otm_monthtotal_checkrelsalary;
                        }
                        if (!IsDouble1(Convert.ToString(Convert.ToDecimal(SpecG2RelSalary))))
                        {
                            if (errorMsg.Length > 0)
                            {
                                errorMsg = errorMsg + ",";
                            }
                            errorMsg = errorMsg + Message.bfw_otm_monthtotal_checkrelsalary;
                        }
                    }//以上數據必須為數字且精確到小數點后一位
                    catch (Exception)
                    {
                        if (errorMsg.Length > 0)
                        {
                            errorMsg = errorMsg + ",";
                        }
                        errorMsg = errorMsg + Message.bfw_otm_monthtotal_checkrelsalary;
                    }
                    if (errorMsg.Length == 0)
                    {
                        model.WorkNo = WorkNo;
                        model.YearMonth = YearMonth;
                        model.ApproveFlag = "0";
                        this.tempDataTable = bllOTMQry.GetOTMQryList(model);
                        if (this.tempDataTable.Rows.Count > 0)
                        {
                            G2Apply = Convert.ToDouble(this.tempDataTable.Rows[0]["G2Apply"]);
                            SpecG2Apply = Convert.ToDouble(this.tempDataTable.Rows[0]["SpecG2Apply"]);
                            if (Convert.ToDouble(G2RelSalary) > G2Apply)
                            {
                                if (errorMsg.Length > 0)
                                {
                                    errorMsg = errorMsg + ",";
                                }
                                errorMsg = errorMsg + Message.CantMoreThanApply;
                            }//實發加班時數不能大於申報加班時數
                            if (Convert.ToDouble(SpecG2RelSalary) > SpecG2Apply)
                            {
                                if (errorMsg.Length > 0)
                                {
                                    errorMsg = errorMsg + ",";
                                }
                                errorMsg = errorMsg + Message.CantMoreThanApply;
                            }//實發加班時數不能大於申報加班時數
                            if (errorMsg.Length == 0)
                            {
                                index++;
                                DataRow row = this.tempDataTable.Rows[0];
                                row.BeginEdit();
                                row["WORKNO"] = WorkNo;
                                row["YearMonth"] = YearMonth;
                                if (!G2RelSalary.Equals("-1"))
                                {
                                    row["G2RelSalary"] = G2RelSalary;
                                }
                                if (!SpecG2RelSalary.Equals("-1"))
                                {
                                    row["SpecG2Salary"] = SpecG2RelSalary;
                                }
                                row.EndEdit();
                                this.tempDataTable.AcceptChanges();
                                if (!(bllOTMQry.SaveData(this.ProcessFlag.Value, CurrentUserInfo.Personcode, this.tempDataTable)))
                                {
                                    errorCount++;
                                    this.dataSet.Tables["KQM_Import"].Rows.Add(new string[] { Message.CantMoreThanControl, WorkNo, YearMonth, G2RelSalary, SpecG2RelSalary });
                                }
                                if (inttotal < 100)
                                {
                                    bllOTMQry.CountCanAdjlasthy(WorkNo, YearMonth);
                                }
                            }
                            else
                            {
                                errorCount++;
                                this.dataSet.Tables["KQM_Import"].Rows.Add(new string[] { errorMsg, WorkNo, YearMonth, G2RelSalary, SpecG2RelSalary });
                            }
                        }
                    }
                    else
                    {
                        errorCount++;
                        this.dataSet.Tables["KQM_Import"].Rows.Add(new string[] { errorMsg, WorkNo, YearMonth, G2RelSalary });
                    }
                    errorMsg = "";
                }
                if (File.Exists(strFilePath))
                {
                    File.Delete(strFilePath);
                }
                this.lbluploadMsg.Text = string.Concat(new object[] { Message.UpdateSuccess, "：", index, "  ;", Message.UpdateFailed, "：", errorCount, " ." });
                this.UltraWebGridImport.DataSource = this.dataSet.Tables["KQM_Import"].DefaultView;
                this.UltraWebGridImport.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region 送簽
        protected void btnSendAudit_Click(object sender, EventArgs e)
        {
            int intSendOK = 0;
            int intSendBillNo = 0;
            int intSendError = 0;
            string OrgCode = "", BillNo = "", AuditOrgCode = "", BillNoType = "KQM";
            string alertText;
            string BillTypeCode = "KQMMonthTotal";
            TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGridOTMMonthTotal.Bands[0].Columns[0];
            int countRow = UltraWebGridOTMMonthTotal.Rows.Count;
            for (int i = 0; i < countRow; i++)
            {
                CellItem GridItem = (CellItem)tcol.CellItems[i];
                CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                if (chkIsHaveRight.Checked)
                {
                    BillNo = "";
                    OrgCode = String.IsNullOrEmpty(this.HiddenOrgCode.Value) ? UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("depcode").Text.Trim() : this.HiddenOrgCode.Value.Trim();

                    AuditOrgCode = bllOTMQry.GetWorkFlowOrgCode(OrgCode, BillTypeCode);

                    if (AuditOrgCode.Length > 0)
                    {
                        string WorkNo = UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("WorkNo").Text.Trim();
                        string YearMonth = UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("YearMonth").Text.Trim();
                        string queryFlag = QueryFlag.Value;
                        dt = bllOTMQry.GetDtByID(WorkNo, YearMonth);
                        if (dt.Rows.Count > 0)
                        {
                            BillNo = dt.Rows[0]["BILLNO"].ToString();
                            if (!string.IsNullOrEmpty(BillNo))
                            {
                                //更新OTM_AdvanceApply表
                                bllOTMQry.SaveAuditData("Modify", WorkNo, YearMonth, BillNo, AuditOrgCode, BillTypeCode, CurrentUserInfo.Personcode, queryFlag, logmodel);
                                intSendOK += 1;
                            }
                            else
                            {
                                //生產單號，更新OTM_AdvanceApply表
                                BillNo = bllOTMQry.SaveAuditData("Add", WorkNo, YearMonth, BillNoType, AuditOrgCode, BillTypeCode, CurrentUserInfo.Personcode, queryFlag, logmodel);
                                intSendBillNo += 1;
                                intSendOK += 1;
                            }
                        }
                    }
                    else
                    {
                        //沒有定義簽核流程
                        intSendError += 1;
                    }
                }
            }
            if (intSendOK + intSendError > 0)
            {
                if (intSendError > 0)
                {
                    alertText = Message.SuccCount + intSendOK + ";" + Message.common_message_billcount + "：" + intSendBillNo + ";" + Message.FaileCount + intSendError + "(沒有定義簽核流程)";
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + alertText + "')", true);
                }
                else
                {
                    alertText = Message.SuccCount + intSendOK + ";" + Message.common_message_billcount + "：" + intSendBillNo;
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + alertText + "')", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.AtLastOneChoose + "')", true);
                return;
            }
            DataUIBind();
            this.ProcessFlag.Value = "";
            this.HiddenOrgCode.Value = "";
        }
        #endregion

        #region  組織送簽
        protected void btnOrgAudit_Click(object sender, EventArgs e)
        {
            try
            {
                int intSendOK = 0;
                int intSendBillNo = 0;
                int intSendError = 0;
                string OrgCode = "", alertText, AuditOrgCode = "", BillNoType = "KQM";
                TemplatedColumn tcol = (TemplatedColumn)UltraWebGridOTMMonthTotal.Bands[0].Columns[0];
                string BillTypeCode = "KQMMonthTotal";
                Dictionary<string, List<OTMTotalQryModel>> dicy = new Dictionary<string, List<OTMTotalQryModel>>();
                int countRow = UltraWebGridOTMMonthTotal.Rows.Count;
                for (int i = 0; i < countRow; i++)
                {
                    CellItem GridItem = (CellItem)tcol.CellItems[i];
                    CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                    if (chkIsHaveRight.Checked)
                    {
                        OrgCode = UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("depcode").Text.Trim();
                        AuditOrgCode = bllOTMQry.GetWorkFlowOrgCode(OrgCode, BillTypeCode);
                        string key = AuditOrgCode;
                        List<OTMTotalQryModel> list = new List<OTMTotalQryModel>();
                        if (!dicy.ContainsKey(key) && AuditOrgCode.Length > 0)
                        {
                            dicy.Add(key, list);
                        }
                        else if (AuditOrgCode.Length == 0)
                        {
                            intSendError += 1;

                        }
                        AuditOrgCode = "";
                    }
                }
                for (int i = 0; i < countRow; i++)
                {
                    CellItem GridItem = (CellItem)tcol.CellItems[i];
                    CheckBox chkIsHaveRight = (CheckBox)(GridItem.FindControl("CheckBoxCell"));
                    if (chkIsHaveRight.Checked)
                    {
                        OrgCode = UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("depcode").Text.Trim();
                        AuditOrgCode = bllOTMQry.GetWorkFlowOrgCode(OrgCode, BillTypeCode);
                        string key = AuditOrgCode;
                        if (dicy[key] != null)
                        {
                            model.WorkNo = UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("WorkNo").Text.Trim();
                            model.YearMonth = UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("YearMonth").Text.Trim();
                            dicy[key].Add(model);
                        }
                    }
                }
                //生產單號，更新OTM_AdvanceApply表
                int count = bllOTMQry.SaveOrgAuditData("Add", dicy, BillNoType, BillTypeCode, CurrentUserInfo.Personcode, logmodel);
                intSendBillNo = count;
                intSendOK += 1;
                if (intSendOK + intSendError > 0)
                {
                    if (intSendError > 0)
                    {
                        alertText = Message.SuccCount + intSendOK + ";" + Message.common_message_billcount + "：" + intSendBillNo + ";" + Message.FaileCount + intSendError + "(沒有定義簽核流程)";
                        Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + alertText + "')", true);
                    }
                    else
                    {
                        alertText = Message.SuccCount + intSendOK + ";" + Message.common_message_billcount + "：" + intSendBillNo;
                        Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + alertText + "')", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.AtLastOneChoose + "')", true);
                    return;
                }
                DataUIBind();
                this.ProcessFlag.Value = "";
                this.HiddenOrgCode.Value = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            model = PageHelper.GetModel<OTMTotalQryModel>(pnlContent.Controls);
            DataUIBind();
            this.txtBatchEmployeeNo.Text = "";
        }
        #endregion
    }
}
