/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OTMDetailQryForm.aspx.cs
 * 檔功能描述： 加班預報查詢
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

namespace GDSBG.MiABU.Attendance.Web.KQM.Query.OTM
{
    public partial class OTMDetailQryForm : BasePage
    {
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        static DataTable advanceApplyTable = new DataTable();
        static DataTable ddlTable = new DataTable();
        OTMDetailQryBll detailQryBll = new OTMDetailQryBll();
        OTMAdvanceApplyQryModel advanceApplyQryModel = new OTMAdvanceApplyQryModel();
        string dCode = "";
        string batchEmployeeNo = "";
        string dateFrom = "";
        string dateTo = "";
        string hoursCondition = "";
        string hours = "";
        string condition = "";

        #region 查詢
        /// <summary>
        /// 查詢按鈕事件
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
            advanceApplyTable = detailQryBll.GetOTMAdvanceQryList(advanceApplyQryModel, base.SqlDep.ToString(), dCode, batchEmployeeNo, dateFrom, dateTo, hoursCondition, hours, pager.CurrentPageIndex, pager.PageSize, out  totalCount);
            this.DataUIBind();
            pager.RecordCount = totalCount;
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
        }


        #endregion

        #region 頁面綁定數據
        /// <summary>
        /// WebGrid綁定數據
        /// </summary>
        private void DataUIBind()
        {
            this.UltraWebGrid.DataSource = advanceApplyTable.DefaultView;
            this.UltraWebGrid.DataBind();
        }


        /// <summary>
        /// WebGrid的DataBound方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UltraWebGrid_DataBound(object sender, EventArgs e)
        {
            string oTDate = "";
            string LHZBIsDisplayG2G3 = "";
            string tempOTType = "";
            string tempStatus = "";
            try
            {
                LHZBIsDisplayG2G3 = ConfigurationManager.AppSettings["LHZBIsDisplayG2G3"];
            }
            catch
            {
                LHZBIsDisplayG2G3 = "N";
            }
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
                    string CSS4S0002 = this.UltraWebGrid.Rows[i].Cells.FromKey("OTMSGFlag").Text;
                    if (CSS4S0002 != null)
                    {
                        if (!(CSS4S0002 == "1"))
                        {
                            if (CSS4S0002 == "2")
                            {
                                goto Label_0546;
                            }
                            if (CSS4S0002 == "3")
                            {
                                goto Label_056C;
                            }
                            if (CSS4S0002 == "4")
                            {
                                goto Label_0592;
                            }
                            if (CSS4S0002 == "5")
                            {
                                goto Label_05B8;
                            }
                            if (CSS4S0002 == "6")
                            {
                                goto Label_05DE;
                            }
                        }
                        else
                        {
                            this.UltraWebGrid.Rows[i].Style.ForeColor = Color.Green;
                        }
                    }
                }
                goto Label_0605;
            Label_0546:
                this.UltraWebGrid.Rows[i].Style.ForeColor = Color.Blue;
                goto Label_0605;
            Label_056C:
                this.UltraWebGrid.Rows[i].Style.ForeColor = Color.SaddleBrown;
                goto Label_0605;
            Label_0592:
                this.UltraWebGrid.Rows[i].Style.ForeColor = Color.Maroon;
                goto Label_0605;
            Label_05B8:
                this.UltraWebGrid.Rows[i].Style.ForeColor = Color.Tomato;
                goto Label_0605;
            Label_05DE:
                this.UltraWebGrid.Rows[i].Style.ForeColor = Color.Red;
            Label_0605:
                string workNo = this.UltraWebGrid.Rows[i].Cells.FromKey("WorkNo").Text.Trim();
                oTDate = Convert.ToDateTime(this.UltraWebGrid.Rows[i].Cells.FromKey("OTDate").Text).ToString("yyyy/MM/dd");
                DataTable tempDataTable = detailQryBll.GetDataTableBySQL(workNo, oTDate);
                if (tempDataTable.Rows.Count > 0)
                {
                    this.UltraWebGrid.Rows[i].Cells.FromKey("G1Total").Value = Convert.ToDouble(this.UltraWebGrid.Rows[i].Cells.FromKey("G1Total").Value) + Convert.ToDouble(tempDataTable.Rows[0]["G1Total"]);
                    this.UltraWebGrid.Rows[i].Cells.FromKey("G2Total").Value = Convert.ToDouble(this.UltraWebGrid.Rows[i].Cells.FromKey("G2Total").Value) + Convert.ToDouble(tempDataTable.Rows[0]["G2Total"]);
                    this.UltraWebGrid.Rows[i].Cells.FromKey("G3Total").Value = Convert.ToDouble(this.UltraWebGrid.Rows[i].Cells.FromKey("G3Total").Value) + Convert.ToDouble(tempDataTable.Rows[0]["G3Total"]);
                }
            }
        }

        /// <summary>
        /// 下拉列表綁定數據
        /// </summary>
        protected void ddlDataBind()
        {
            ddlTable = new DataTable();
            ddlTable = detailQryBll.GetDataByCondition("condition1");
            this.ddlOTStatus.DataSource = ddlTable.DefaultView;
            this.ddlOTStatus.DataTextField = "DataValue";
            this.ddlOTStatus.DataValueField = "DataCode";
            this.ddlOTStatus.DataBind();
            this.ddlOTStatus.Items.Insert(0, new ListItem("", ""));
            this.ddlOTStatus.SelectedValue = "";
            ddlTable.Clear();
            ddlTable = detailQryBll.GetDataByCondition("condition2");
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
                this.imgBatchWorkNo.Attributes.Add("OnClick", "javascript:ShowBatchWorkNo()");
                this.txtOTDateFrom.Text = DateTime.Now.AddDays((double)(-DateTime.Now.Day + 1)).ToString("yyyy/MM/dd");
                this.txtOTDateTo.Text = DateTime.Now.AddDays(2.0).ToString("yyyy/MM/dd");
                this.ModuleCode.Value = base.Request["ModuleCode"].ToString();
                this.ddlDataBind();
            }
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);

            SetSelector(imgDepCode, txtDepCode, txtDepName, "dept", base.Request["ModuleCode"].ToString());
            SetCalendar(txtOTDateFrom, txtOTDateTo);
        }


        #endregion

        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            this.Query(true, "Goto");
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
        public void SetSelector(WebControl ctrlTrigger, Control ctrlCode, Control ctrlName, string flag,string moduleCode)
        {
            if (ctrlCode is TextBox) { (ctrlCode as TextBox).Attributes.Add("readonly", "readonly"); }
            if (ctrlName is TextBox) { (ctrlName as TextBox).Attributes.Add("readonly", "readonly"); }
            ctrlTrigger.Attributes.Add("onclick", string.Format("return setSelector('{0}','{1}','{2}','{3}')",
                ctrlCode.ClientID, ctrlName.ClientID, flag,moduleCode));
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
                tempTable = detailQryBll.GetOTMAdvanceQryList(advanceApplyQryModel, base.SqlDep.ToString(), dCode, batchEmployeeNo, dateFrom, dateTo, hoursCondition, hours);
                List<OTMAdvanceApplyQryModel> list = detailQryBll.GetList(tempTable);
                string[] header = { ControlText.gvOTMAdvanceWorkNo, ControlText.gvOTMAdvanceLocalName, ControlText.gvOTMAdvanceBuName, ControlText.gvOTMAdvanceDepName, ControlText.gvOTMAdvanceOTDate, ControlText.gvOTMAdvanceOverTimeType, ControlText.gvOTMAdvanceWeek, ControlText.gvOTMAdvanceOTType, ControlText.gvOTMAdvanceBeginTime, ControlText.gvOTMAdvanceEndTime,
                                        ControlText.gvOTMAdvanceHours, ControlText.gvOTMAdvanceStatusName, ControlText.gvOTMAdvanceImportFlag, ControlText.gvOTMAdvanceG1Total, ControlText.gvOTMAdvanceG2Total, ControlText.gvOTMAdvanceG3Total, ControlText.gvOTMAdvanceWorkDesc, ControlText.gvOTMAdvanceRemark, ControlText.gvOTMAdvanceBillNo, 
                                        ControlText.gvOTMAdvanceApproverName, ControlText.gvOTMAdvanceApproveDate,ControlText.gvOTMAdvanceApRemark,ControlText.gvOTMAdvanceModifier,ControlText.gvOTMAdvanceModifyDate};
                string[] properties = { "WorkNo", "LocalName", "BuName", "DepName", "OTDate", "OverTimeType", "Week", "OTType", "BeginTime", "EndTime", "Hours", "StatusName", "ImportFlag", "G1Total", "G2Total", "G3Total", "WorkDesc", "Remark","BillNo"
                                           ,"ApproverName"	,"ApproveDate"	,"ApRemark"	,"Modifier"	,"ModifyDate"};
                string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
                PageHelper.ReturnHTTPStream(filePath, true);
            }
        }
        #endregion

        #region 獲取頁面值
        public void GetPageValue()
        {
            dCode = this.txtDepName.Text.Trim();
            batchEmployeeNo = (this.txtEmployeeNo.Text.Trim() != "") ? "" : this.txtBatchEmployeeNo.Text.Trim();
            dateFrom = DateTime.Parse(this.txtOTDateFrom.Text.Trim()).ToString("yyyy/MM/dd");
            dateTo = DateTime.Parse(this.txtOTDateTo.Text.Trim()).ToString("yyyy/MM/dd");
            hoursCondition = this.ddlHoursCondition.SelectedValue;
            hours = this.txtHours.Text.Trim();
            advanceApplyQryModel.WorkNo = this.txtEmployeeNo.Text.Trim();
            advanceApplyQryModel.BillNo = this.txtBillNo.Text.Trim();
            advanceApplyQryModel.LocalName = this.txtName.Text.Trim();
            advanceApplyQryModel.OverTimeType = this.ddlPersonType.SelectedValue;
            advanceApplyQryModel.OTType = this.ddlOTType.SelectedValue;
            advanceApplyQryModel.Status = this.ddlOTStatus.SelectedValue;
            advanceApplyQryModel.IsProject = this.ddlIsProject.SelectedValue;
        }
        #endregion
    }
}
