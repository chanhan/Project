/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OTMRealQryForm.aspx.cs
 * 檔功能描述： 加班實報查詢
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.12.9
 * 
 */
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.Model.KQM.Query.OTM;
using GDSBG.MiABU.Attendance.BLL.KQM.Query.OTM;
using System.Collections.Generic;
using Resources;
using System.Web.Script.Serialization;
using System.Configuration;
using System.Drawing;
using Infragistics.WebUI.UltraWebGrid;
using GDSBG.MiABU.Attendance.Model.KQM.Query;


namespace GDSBG.MiABU.Attendance.Web.KQM.Query.OTM
{
    public partial class OTMRealQryForm : BasePage
    {

        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        static DataTable realApplyTable = new DataTable();
        static DataTable ddlTable = new DataTable();
        OTMRealQryBll realQryBll = new OTMRealQryBll();
        OTMRealApplyQryModel realApplyQryModel = new OTMRealApplyQryModel();
        string dCode = "";
        string batchEmployeeNo = "";
        string dateFrom = "";
        string dateTo = "";
        string hoursCondition = "";
        string hours = "";
        string condition = "";

        #region 頁面按鈕事件
        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {

            if ((this.txtOTDateFrom.Text.Trim() == "") || (this.txtOTDateTo.Text.Trim() == ""))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.ErrWorkDayNull + "');", true);
            }
            else
            {
                pager.CurrentPageIndex = 1;
                this.Query(true, "Goto");
                this.ProcessFlag.Value = "";
            }

        }

        #endregion


        #region 頁面綁定數據
        /// <summary>
        /// WebGrid綁定數據
        /// </summary>
        private void DataUIBind()
        {
            this.UltraWebGrid.DataSource = realApplyTable.DefaultView;
            this.UltraWebGrid.DataBind();
        }

        /// <summary>
        /// 下拉列表綁定數據
        /// </summary>
        protected void ddlDataBind()
        {

            ddlTable = realQryBll.GetDataByCondition("condition1");
            this.ddlOTStatus.DataSource = ddlTable.DefaultView;
            this.ddlOTStatus.DataTextField = "DataValue";
            this.ddlOTStatus.DataValueField = "DataCode";
            this.ddlOTStatus.DataBind();
            this.ddlOTStatus.Items.Insert(0, new ListItem("", ""));
            this.ddlOTStatus.SelectedValue = "";
            ddlTable.Clear();
            ddlTable = realQryBll.GetDataByCondition("condition2");
            this.ddlPersonType.DataSource = ddlTable.DefaultView;
            this.ddlPersonType.DataTextField = "DataValue";
            this.ddlPersonType.DataValueField = "DataCode";
            this.ddlPersonType.DataBind();
            this.ddlPersonType.Items.Insert(0, new ListItem("", ""));
        }

        #endregion

        #region 頁面加載

        /// <summary>
        /// 頁面加載
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            PageHelper.ButtonControls(base.FuncList, pnlShowPanel.Controls, base.FuncListModule);
            if (!IsPostBack)
            {
                this.txtOTDateFrom.Text = DateTime.Now.AddDays((double)(-DateTime.Now.Day + 1)).ToString("yyyy/MM/dd");
                this.txtOTDateTo.Text = DateTime.Now.AddDays(2.0).ToString("yyyy/MM/dd");
                this.ModuleCode.Value = base.Request["ModuleCode"].ToString();
                this.ddlDataBind();
            }
            SetSelector(imgDepCode, txtDepCode, txtDepName, "dept", base.Request["ModuleCode"].ToString());
            SetCalendar(txtOTDateFrom, txtOTDateTo);

            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
                ClientMessage.Add("ToLaterThanFrom", Message.ToLaterThanFrom);
                ClientMessage.Add("WrongDate", Message.WrongDate);
                ClientMessage.Add("TextBoxNotNull", Message.TextBoxNotNull);
                ClientMessage.Add("DeleteConfirm", Message.DeleteConfirm);
                ClientMessage.Add("btnImport", Message.btnImport);
                ClientMessage.Add("btnBack", Message.btnBack);
                ClientMessage.Add("WorkNo", Message.WorkNo);
                ClientMessage.Add("No", Message.No);
                ClientMessage.Add("LocalName", Message.LocalName);
                ClientMessage.Add("CardNo", Message.CardNo);
                ClientMessage.Add("CardTime", Message.CardTime);
                ClientMessage.Add("BellNo", Message.BellNo);
                ClientMessage.Add("ReadTime", Message.ReadTime);
                ClientMessage.Add("KQDateNotNull", Message.KQDateNotNull);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);


        }

        #endregion

        #region 獲取頁面值
        public void GetPageValue()
        {
            dCode = this.txtDepName.Text.Trim();
            batchEmployeeNo = "";
            dateFrom = DateTime.Parse(this.txtOTDateFrom.Text.Trim()).ToString("yyyy/MM/dd");
            dateTo = DateTime.Parse(this.txtOTDateTo.Text.Trim()).ToString("yyyy/MM/dd");
            hoursCondition = this.ddlHoursCondition.SelectedValue;
            hours = this.txtHours.Text.Trim();

            realApplyQryModel.BillNo = this.txtBillNo.Text.Trim();
            realApplyQryModel.WorkNo = this.txtEmployeeNo.Text.Trim();
            realApplyQryModel.LocalName = this.txtName.Text.Trim();
            realApplyQryModel.OverTimeType = this.ddlPersonType.SelectedValue;
            realApplyQryModel.OTType = this.ddlOTType.SelectedValue;
            realApplyQryModel.Status = this.ddlOTStatus.SelectedValue;
            realApplyQryModel.IsProject = this.ddlIsProject.SelectedValue;
        }
        #endregion

        #region 查詢
        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="WindowOpen"></param>
        /// <param name="forwarderType"></param>
        private void Query(bool WindowOpen, string forwarderType)
        {

            if (this.ProcessFlag.Value.ToLower().Equals("condition") || WindowOpen)
            {
                GetPageValue();
                this.ViewState.Add("condition", condition);
            }
            else
            {
                condition = Convert.ToString(this.ViewState["condition"]);
            }
            int totalCount = 0;
            
            realApplyTable = realQryBll.GetOTMRealQryList(realApplyQryModel,base.SqlDep.ToString(), dCode, batchEmployeeNo, dateFrom, dateTo, hoursCondition, hours, pager.CurrentPageIndex, pager.PageSize, out  totalCount);
            this.DataUIBind();
            pager.RecordCount = totalCount;
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
        }

        #endregion

        #region Ajaxs事件
        protected override void AjaxProcess()
        {
            string noticeJson = null;

            if (!string.IsNullOrEmpty(Request.Form["WorkNo"]))
            {
                string WorkNo = Request.Form["WorkNo"].ToString();
                string KQDate = Request.Form["KQDate"].ToString();
                string ShiftNo = Request.Form["ShiftNo"].ToString();
                DataTable temp = new DataTable();
                temp = realQryBll.GetBellCardData(WorkNo, KQDate, ShiftNo);
                List<BellCardDataModel> list = realQryBll.GetBellCardList(temp);
                if (list != null)
                {
                    noticeJson = JsSerializer.Serialize(list);
                }
                Response.Clear();
                Response.Write(noticeJson);
                Response.End();
            }
        }
        #endregion

        #region WebGrid行綁定事件
        /// <summary>
        /// WebGrid行綁定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UltraWebGrid_DataBound(object sender, EventArgs e)
        {
            string tempOTType = "";
            string tempStatus = "";
            string LHZBIsDisplayG2G3 = "";
            try
            {
                LHZBIsDisplayG2G3 = ConfigurationManager.AppSettings["LHZBIsDisplayG2G3"];
            }
            catch
            {
                LHZBIsDisplayG2G3 = "N";
            }
            this.UltraWebGrid.DisplayLayout.Bands[0].Columns.FromKey("OTDate").Type = ColumnType.HyperLink;
            string oTDate = "";
            for (int i = 0; i < this.UltraWebGrid.Rows.Count; i++)
            {
                if (LHZBIsDisplayG2G3 == "Y")
                {
                    tempOTType = this.UltraWebGrid.Rows[i].Cells.FromKey("OTType").Text == null ? "" : this.UltraWebGrid.Rows[i].Cells.FromKey("OTType").Text.Trim();
                    if (tempOTType == "G2")
                    {
                        this.UltraWebGrid.Rows[i].Cells.FromKey("OTType").Value = string.Concat(new object[] { this.UltraWebGrid.Rows[i].Cells.FromKey("OTType").Value, "(", base.GetResouseValue("otm.g2.remark"), ")" });
                    }
                    else if (tempOTType == "G3")
                    {
                        this.UltraWebGrid.Rows[i].Cells.FromKey("OTType").Value = string.Concat(new object[] { this.UltraWebGrid.Rows[i].Cells.FromKey("OTType").Value, "(", base.GetResouseValue("otm.g3.remark"), ")" });
                    }
                }

                if (this.UltraWebGrid.Rows[i].Cells.FromKey("ShiftDesc").Value != null)
                {
                    this.UltraWebGrid.Rows[i].Cells.FromKey("OTDate").TargetURL = "javascript:ShowDetail('" + this.UltraWebGrid.Rows[i].Cells.FromKey("WorkNo").Text + "','" + Convert.ToDateTime(this.UltraWebGrid.Rows[i].Cells.FromKey("OTDate").Text).ToString("yyyy/MM/dd") + "','" + this.UltraWebGrid.Rows[i].Cells.FromKey("ShiftDesc").Text + "')";
                }

                else
                {
                    this.UltraWebGrid.Rows[i].Cells.FromKey("OTDate").TargetURL = "javascript:ShowDetail('" + this.UltraWebGrid.Rows[i].Cells.FromKey("WorkNo").Text + "','" + Convert.ToDateTime(this.UltraWebGrid.Rows[i].Cells.FromKey("OTDate").Text).ToString("yyyy/MM/dd") + "','A')";
                }
                tempStatus = this.UltraWebGrid.Rows[i].Cells.FromKey("Status").Text == null ? "" : this.UltraWebGrid.Rows[i].Cells.FromKey("Status").Text.Trim();
                if (tempStatus == "0")
                {
                    this.UltraWebGrid.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Green;
                }
                if (tempStatus == "1")
                {
                    this.UltraWebGrid.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Blue;
                }
                if (tempStatus == "2")
                {
                    this.UltraWebGrid.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Black;
                }
                if (tempStatus == "3")
                {
                    this.UltraWebGrid.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Maroon;
                }
                if ((this.UltraWebGrid.Rows[i].Cells.FromKey("IsProject").Value != null) && (this.UltraWebGrid.Rows[i].Cells.FromKey("IsProject").Text.Trim() == "Y"))
                {
                    this.UltraWebGrid.Rows[i].Style.ForeColor = Color.Red;
                }
                if ((this.UltraWebGrid.Rows[i].Cells.FromKey("OTMSGFlag").Value != null) && (this.UltraWebGrid.Rows[i].Cells.FromKey("WorkDesc").Value != null))
                {
                    string CSS4S0004 = this.UltraWebGrid.Rows[i].Cells.FromKey("OTMSGFlag").Text;
                    if (CSS4S0004 != null)
                    {
                        if (!(CSS4S0004 == "1"))
                        {
                            if (CSS4S0004 == "2")
                            {
                                goto Label_0758;
                            }
                            if (CSS4S0004 == "3")
                            {
                                goto Label_078D;
                            }
                            if (CSS4S0004 == "4")
                            {
                                goto Label_07C2;
                            }
                            if (CSS4S0004 == "5")
                            {
                                goto Label_07F7;
                            }
                            if (CSS4S0004 == "6")
                            {
                                goto Label_082C;
                            }
                        }
                        else
                        {
                            this.UltraWebGrid.Rows[i].Cells.FromKey("WorkDesc").Style.ForeColor = Color.Green;
                        }
                    }
                }
                goto Label_0862;
            Label_0758:
                this.UltraWebGrid.Rows[i].Cells.FromKey("WorkDesc").Style.ForeColor = Color.Blue;
                goto Label_0862;
            Label_078D:
                this.UltraWebGrid.Rows[i].Cells.FromKey("WorkDesc").Style.ForeColor = Color.SaddleBrown;
                goto Label_0862;
            Label_07C2:
                this.UltraWebGrid.Rows[i].Cells.FromKey("WorkDesc").Style.ForeColor = Color.Maroon;
                goto Label_0862;
            Label_07F7:
                this.UltraWebGrid.Rows[i].Cells.FromKey("WorkDesc").Style.ForeColor = Color.Tomato;
                goto Label_0862;
            Label_082C:
                this.UltraWebGrid.Rows[i].Cells.FromKey("WorkDesc").Style.ForeColor = Color.Red;
            Label_0862:
                if (((this.UltraWebGrid.Rows[i].Cells.FromKey("ConfirmHours").Value != null) && (this.UltraWebGrid.Rows[i].Cells.FromKey("AdvanceHours").Value != null)) && !this.UltraWebGrid.Rows[i].Cells.FromKey("ConfirmHours").Text.Equals(this.UltraWebGrid.Rows[i].Cells.FromKey("AdvanceHours").Text))
                {
                    if (this.UltraWebGrid.Rows[i].Cells.FromKey("ConfirmHours").Text.Equals("0.0"))
                    {
                        this.UltraWebGrid.Rows[i].Cells.FromKey("ConfirmHours").Style.ForeColor = Color.Red;
                        this.UltraWebGrid.Rows[i].Cells.FromKey("WorkNo").Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        this.UltraWebGrid.Rows[i].Cells.FromKey("ConfirmHours").Style.ForeColor = Color.Orange;
                        this.UltraWebGrid.Rows[i].Cells.FromKey("WorkNo").Style.ForeColor = Color.Orange;
                    }
                }
                oTDate = Convert.ToDateTime(this.UltraWebGrid.Rows[i].Cells.FromKey("OTDate").Text).ToString("yyyy/MM/dd");
                string workNo = this.UltraWebGrid.Rows[i].Cells.FromKey("WorkNo").Text.Trim();
                DataTable tempDataTable = realQryBll.GetDataTableBySQL(workNo, oTDate);
                if (tempDataTable.Rows.Count > 0)
                {
                    this.UltraWebGrid.Rows[i].Cells.FromKey("G1Total").Value = tempDataTable.Rows[0]["G1Total"].ToString();
                    this.UltraWebGrid.Rows[i].Cells.FromKey("G2Total").Value = tempDataTable.Rows[0]["G2Total"].ToString();
                    this.UltraWebGrid.Rows[i].Cells.FromKey("G3Total").Value = tempDataTable.Rows[0]["G3Total"].ToString();
                }
            }
        }
        #endregion

        #region 設置放大鏡頁面,用於輔助選擇
        /// <summary>
        /// 設置Selector
        /// </summary>
        /// <param name="ctrlTrigger">控件ID--按鈕</param>
        /// <param name="ctrlCode">控件ID--文本框1</param>
        /// <param name="ctrlName">控件ID--文本框2</param>
        /// <param name="flag">Selector區分標誌</param>
        public void SetSelector(WebControl ctrlTrigger, Control ctrlCode, Control ctrlName, string flag, string moduleCode)
        {
            if (ctrlCode is TextBox) { (ctrlCode as TextBox).Attributes.Add("readonly", "readonly"); }
            if (ctrlName is TextBox) { (ctrlName as TextBox).Attributes.Add("readonly", "readonly"); }
            ctrlTrigger.Attributes.Add("onclick", string.Format("return setSelector('{0}','{1}','{2}','{3}')",
                ctrlCode.ClientID, ctrlName.ClientID, flag, moduleCode));
        }
        #endregion

        #region 導出
        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (this.UltraWebGrid.Rows.Count == 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.NoDataExport + "')", true);
            }
            else
            {
                GetPageValue();
                DataTable tempTable = new DataTable();
                tempTable = realQryBll.GetOTMRealQryList(realApplyQryModel,base.SqlDep.ToString(), dCode, batchEmployeeNo, dateFrom, dateTo, hoursCondition, hours);
                List<OTMRealApplyQryModel> list = realQryBll.GetList(tempTable);
                string[] header = { ControlText.gvOTMRealWorkNo, ControlText.gvOTMRealLocalName, ControlText.gvOTMRealBuName, ControlText.gvOTMRealDepName, ControlText.gvOTMRealOTDate, ControlText.gvOTMRealOTType, ControlText.gvOTMRealWeek, ControlText.gvOTMRealOverTimeType, ControlText.gvOTMRealShiftDesc, ControlText.gvOTMRealAdvanceTime,
                                        ControlText.gvOTMRealAdvanceHours, ControlText.gvOTMRealOverTimeSpan, ControlText.gvOTMRealRealHours, ControlText.gvOTMRealConfirmHours, ControlText.gvOTMRealConfirmRemark, ControlText.gvOTMRealWorkDesc, ControlText.gvOTMRealIsProject, ControlText.gvOTMRealStatusName, ControlText.gvOTMRealG1Total, 
                                        ControlText.gvOTMRealG2Total, ControlText.gvOTMRealG3Total,ControlText.gvOTMRealRemark,ControlText.gvOTMRealBillNo,ControlText.gvOTMRealApproverName,
                                  ControlText.gvOTMRealApproveDate,ControlText.gvOTMRealApRemark,ControlText.gvOTMRealModifier,ControlText.gvOTMRealModifyDate};
                string[] properties = { "WorkNo", "LocalName", "BuName", "DepName", "OTDate", "OTType", "Week", "OverTimeType", "ShiftDesc", "AdvanceTime", "AdvanceHours", "OverTimeSpan", 
                                          "RealHours","ConfirmHours", "ConfirmRemark","WorkDesc","IsProject","StatusName","G1Total", "G2Total", "G3Total", "ApproveDate", "ApRemark","Modifier","ModifyDate"};
                string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
                PageHelper.ReturnHTTPStream(filePath, true);
            }
        }
        #endregion

        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            this.Query(true, "Goto");
        }
        #endregion

    }
}
