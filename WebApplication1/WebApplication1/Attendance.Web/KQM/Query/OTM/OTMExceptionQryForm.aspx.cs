/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OTMRealQryForm.aspx.cs
 * 檔功能描述： 加班異常查詢
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
using System.Collections;


namespace GDSBG.MiABU.Attendance.Web.KQM.Query.OTM
{
    public partial class OTMExceptionQryForm : BasePage
    {


        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        static DataTable exceptionApplyTable = new DataTable();
        static DataTable ddlTable = new DataTable();
        OTMExceptionQryBll exceptionQryBll = new OTMExceptionQryBll();
        OTMExceptionApplyQryModel exceptionApplyQryModel = new OTMExceptionApplyQryModel();
        string dCode = "";
        string dateFrom = "";
        string dateTo = "";
        string hoursCondition = "";
        string hours = "";

        #region 頁面按鈕事件
        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {

            if ((this.txtStartDate.Text.Trim() == "") || (this.txtEndDate.Text.Trim() == ""))
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

        # region WebGrid綁定
        /// <summary>
        /// WebGrid綁定數據
        /// </summary>
        private void DataUIBind()
        {
            if (exceptionApplyTable != null)
            {
                this.UltraWebGridRealApply.DataSource = exceptionApplyTable.DefaultView;
                this.UltraWebGridRealApply.DataBind();
            }
        }
        /// <summary>
        /// WebGri的DataBound方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UltraWebGridRealApply_DataBound(object sender, EventArgs e)
        {
            string tempOTType = "";
            string LHZBIsDisplayG2G3 = "";
            try
            {
                LHZBIsDisplayG2G3 = ConfigurationManager.AppSettings["LHZBIsDisplayG2G3"];
            }
            catch
            {
                LHZBIsDisplayG2G3 = "N";
            }
            string Status = "";
            string WeekName = "";
            for (int i = 0; i < this.UltraWebGridRealApply.Rows.Count; i++)
            {
                if (LHZBIsDisplayG2G3 == "Y")
                {
                    tempOTType = this.UltraWebGridRealApply.Rows[i].Cells.FromKey("OTType").Text == null ? "" : this.UltraWebGridRealApply.Rows[i].Cells.FromKey("OTType").Text.Trim();
                    if (tempOTType == "G2")
                    {
                        this.UltraWebGridRealApply.Rows[i].Cells.FromKey("OTType").Value = string.Concat(new object[] { this.UltraWebGridRealApply.Rows[i].Cells.FromKey("OTType").Value, "(", Message.OtmG2Remark, ")" });
                    }
                    else if (tempOTType == "G3")
                    {
                        this.UltraWebGridRealApply.Rows[i].Cells.FromKey("OTType").Value = string.Concat(new object[] { this.UltraWebGridRealApply.Rows[i].Cells.FromKey("OTType").Value, "(", Message.OtmG3Remark, ")" });
                    }
                }
                Status = this.UltraWebGridRealApply.Rows[i].Cells.FromKey("Status")==null?"":this.UltraWebGridRealApply.Rows[i].Cells.FromKey("Status").Text.Trim();
                WeekName = this.UltraWebGridRealApply.Rows[i].Cells.FromKey("Week")==null?"":this.UltraWebGridRealApply.Rows[i].Cells.FromKey("Week").Text.Trim();
                string CSS4S0002 = Status;
                if (CSS4S0002 == null)
                {
                    goto Label_035E;
                }
                if (!(CSS4S0002 == "0"))
                {
                    if (CSS4S0002 == "1")
                    {
                        goto Label_02BF;
                    }
                    if (CSS4S0002 == "3")
                    {
                        goto Label_02F4;
                    }
                    if (CSS4S0002 == "4")
                    {
                        goto Label_0329;
                    }
                    goto Label_035E;
                }
                this.UltraWebGridRealApply.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Green;
                goto Label_0393;
            Label_02BF:
                this.UltraWebGridRealApply.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Blue;
                goto Label_0393;
            Label_02F4:
                this.UltraWebGridRealApply.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Maroon;
                goto Label_0393;
            Label_0329:
                this.UltraWebGridRealApply.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Red;
                goto Label_0393;
            Label_035E:
                this.UltraWebGridRealApply.Rows[i].Cells.FromKey("StatusName").Style.ForeColor = Color.Black;
            Label_0393:
                switch (WeekName)
                {
                    case "1":
                        this.UltraWebGridRealApply.Rows[i].Cells.FromKey("Week").Text = Message.sunday;
                        break;

                    case "2":
                        this.UltraWebGridRealApply.Rows[i].Cells.FromKey("Week").Text =Message.monday;
                        break;

                    case "3":
                        this.UltraWebGridRealApply.Rows[i].Cells.FromKey("Week").Text = Message.tuesday;
                        break;

                    case "4":
                        this.UltraWebGridRealApply.Rows[i].Cells.FromKey("Week").Text = Message.wednesday;
                        break;

                    case "5":
                        this.UltraWebGridRealApply.Rows[i].Cells.FromKey("Week").Text = Message.thursday;
                        break;

                    case "6":
                        this.UltraWebGridRealApply.Rows[i].Cells.FromKey("Week").Text =Message.friday;
                        break;

                    case "7":
                        this.UltraWebGridRealApply.Rows[i].Cells.FromKey("Week").Text = Message.saturday;
                        break;
                }
            }
        }
        #endregion


        #region 獲取加班時數
        /// <summary>
        /// 獲取加班時數
        /// </summary>
        /// <param name="WorkNo"></param>
        /// <param name="OTDate"></param>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="OTType"></param>
        /// <returns></returns>
        public string Get_OverTimeHours(string WorkNo, string OTDate, string BeginTime, string EndTime, string OTType)
        {

            double hours = 0.0;
            if (BeginTime != EndTime)
            {
                hours = GetOtHours(WorkNo.ToUpper(), OTDate, BeginTime, EndTime, OTType);
            }
            return hours.ToString();
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
               
                this.ModuleCode.Value = base.Request["ModuleCode"].ToString();

                this.txtStartDate.Text = DateTime.Now.AddDays((double)(-DateTime.Now.Day + 1)).ToString("yyyy/MM/dd");
                this.txtEndDate.Text = DateTime.Now.ToString("yyyy/MM/dd");

                ddlTable = exceptionQryBll.GetDataByCondition("condition1");
                this.ddlOTType.DataSource = ddlTable.DefaultView;
                this.ddlOTType.DataTextField = "DataValue";
                this.ddlOTType.DataValueField = "DataCode";
                this.ddlOTType.DataBind();
                this.ddlOTType.Items.Insert(0, new ListItem("", ""));
                ddlTable.Clear();
                ddlTable = exceptionQryBll.GetDataByCondition("condition2");
                this.ddlStatus.DataSource = ddlTable.DefaultView;
                this.ddlStatus.DataTextField = "DataValue";
                this.ddlStatus.DataValueField = "DataCode";
                this.ddlStatus.DataBind();
                this.ddlStatus.Items.Insert(0, new ListItem("", ""));
                this.ddlStatus.SelectedValue = "";
                ddlTable.Clear();
                ddlTable = exceptionQryBll.GetDataByCondition("condition3");
                this.ddlDiffReason.DataSource = ddlTable.DefaultView;
                this.ddlDiffReason.DataTextField = "DataValue";
                this.ddlDiffReason.DataValueField = "DataCode";
                this.ddlDiffReason.DataBind();
                this.ddlDiffReason.Items.Insert(0, new ListItem("", ""));
                ddlTable.Clear();
                ddlTable = exceptionQryBll.GetDataByCondition("condition4");
                this.ddlPersonType.DataSource = ddlTable.DefaultView;
                this.ddlPersonType.DataTextField = "DataValue";
                this.ddlPersonType.DataValueField = "DataCode";
                this.ddlPersonType.DataBind();
                this.ddlPersonType.Items.Insert(0, new ListItem("", ""));
            }
            SetSelector(imgDepCode, txtDepCode, txtDepName, "dept", base.Request["ModuleCode"].ToString());
            SetCalendar(txtStartDate, txtEndDate);

        }

        #endregion


        #region 獲取頁面值
        /// <summary>
        /// 獲取頁面值
        /// </summary>
        public void GetPageValue()
        {
            dCode = this.txtDepName.Text.Trim();
            dateFrom = DateTime.Parse(this.txtStartDate.Text.Trim()).ToString("yyyy/MM/dd");
            dateTo = DateTime.Parse(this.txtEndDate.Text.Trim()).ToString("yyyy/MM/dd");
            hoursCondition = this.ddlHoursCondition.SelectedValue;
            hours = this.txtHours.Text.Trim();

            exceptionApplyQryModel.WorkNo = this.txtWorkNo.Text.Trim().ToUpper();
            exceptionApplyQryModel.LocalName = this.txtLocalName.Text.Trim();
            exceptionApplyQryModel.OverTimeType = this.ddlPersonType.SelectedValue;
            exceptionApplyQryModel.OTType = this.ddlOTType.SelectedValue;
            exceptionApplyQryModel.Status = this.ddlStatus.SelectedValue;
            exceptionApplyQryModel.DiffReason = this.ddlDiffReason.SelectedValue.ToString();
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
            int totalCount = 0;
            string condition = "";
            if (this.ProcessFlag.Value.ToLower().Equals("condition") || WindowOpen)
            {
                GetPageValue();
                this.ViewState.Add("condition", condition);
            }
            else
            {
                condition = Convert.ToString(this.ViewState["condition"]);
            }

            exceptionApplyTable = exceptionQryBll.GetOTMExceptionQryList(exceptionApplyQryModel, base.SqlDep.ToString(), dCode, dateFrom, dateTo, hoursCondition, hours, pager.CurrentPageIndex, pager.PageSize, out  totalCount);
            this.DataUIBind();
            pager.RecordCount = totalCount;
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
        }

        #endregion

        #region 導出
        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (this.UltraWebGridRealApply.Rows.Count == 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.NoDataExport + "')", true);
            }
            else
            {
                GetPageValue();
                DataTable tempTable = new DataTable();
                tempTable = exceptionQryBll.GetOTMExceptionQryList(exceptionApplyQryModel, base.SqlDep.ToString(), dCode, dateFrom, dateTo, hoursCondition, hours);
                List<OTMExceptionApplyQryModel> list = exceptionQryBll.GetList(tempTable);
                string[] header = { ControlText.gvOTMExceptionWorkNo, ControlText.gvOTMExceptionLocalName, ControlText.gvOTMExceptionBuName, ControlText.gvOTMExceptionDName, ControlText.gvOTMExceptionOverTimeType, ControlText.gvOTMExceptionOTDate, ControlText.gvOTMExceptionWeek, ControlText.gvOTMExceptionOTType, ControlText.gvOTMExceptionKQShift, ControlText.gvOTMExceptionKQTime,
                                        ControlText.gvOTMExceptionBeginTime, ControlText.gvOTMExceptionEndTime, ControlText.gvOTMExceptionRealHours, ControlText.gvOTMExceptionWorkDesc, ControlText.gvOTMExceptionDiffReasonName, ControlText.gvOTMExceptionReMarks, ControlText.gvOTMExceptionStatusName, ControlText.gvOTMExceptionReMark, ControlText.gvOTMExceptionModifier, 
                                        ControlText.gvOTMExceptionModifyDate, ControlText.gvOTMExceptionBillNo};
                string[] properties = { "WorkNo", "LocalName", "BuName", "DName", "OverTimeType", "OTDate", "Week", "OTType", "KQShift", "KQTime",
                                          "BeginTime","EndTime", "RealHours","WorkDesc","DiffReasonName","ReMarks","StatusName", "ReMark", "Modifier", "ModifyDate", "BillNo"};
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

        #region function.cs中方法

        public double GetOtHours(string WorkNo, string OTDate, string StrBtime, string StrEtime, string OTType)
        {
            try
            {
                double OtHours = 0.0;
                double RestHours = 0.0;
                string condition = "";
                if (OTType.Length != 0)
                {
                    DateTime dtTempBeginTime;
                    DateTime dtTempEndTime;
                    string dtShiftOnTime;
                    string dtShiftOffTime;
                    string dtAMRestSTime;
                    string dtAMRestETime;
                    TimeSpan tsOTHours;
                    if (OTType.Equals("G4") && (TimeSpan.Parse(Convert.ToDateTime(StrBtime).ToString("HH:mm")) < TimeSpan.Parse("06:30")))
                    {
                        OTDate = Convert.ToDateTime(OTDate).AddDays(-1.0).ToString("yyyy/MM/dd");
                    }
                    string strShiftNo = exceptionQryBll.GetShiftNo(WorkNo.ToUpper(), OTDate);
                    if (strShiftNo.Length == 0)
                    {
                        return OtHours;
                    }
                    if ((StrBtime.Length > 8) & (StrEtime.Length > 8))
                    {
                        dtTempBeginTime = DateTime.Parse(StrBtime);
                        dtTempEndTime = DateTime.Parse(StrEtime);
                    }
                    else
                    {
                        dtTempBeginTime = DateTime.Parse(OTDate + " " + StrBtime);
                        dtTempEndTime = DateTime.Parse(OTDate + " " + StrEtime);
                        SortedList list = new SortedList();
                        list = this.ReturnOTTTime(WorkNo, OTDate, dtTempBeginTime, dtTempEndTime, strShiftNo);
                        dtTempBeginTime = Convert.ToDateTime(list.GetByIndex(list.IndexOfKey("A")));
                        dtTempEndTime = Convert.ToDateTime(list.GetByIndex(list.IndexOfKey("B")));
                    }
                    if (OTType.Equals("G4") && !(dtTempBeginTime.ToString("yyyy/MM/dd").Equals(dtTempEndTime.ToString("yyyy/MM/dd")) || Convert.ToDateTime(StrBtime).ToString("HH:mm").Equals("00:00")))
                    {
                        return OtHours;
                    }
                    string dtBtime = dtTempBeginTime.ToString("yyyy/MM/dd HH:mm");
                    string dtEtime = dtTempEndTime.ToString("yyyy/MM/dd HH:mm");

                    DataTable sdt = exceptionQryBll.GetDataTableBySQL(strShiftNo);
                    if (sdt.Rows.Count == 0)
                    {
                        return OtHours;
                    }
                    string ShiftOnTime = Convert.ToString(sdt.Rows[0]["OnDutyTime"]);
                    string ShiftOffTime = Convert.ToString(sdt.Rows[0]["OffDutyTime"]);
                    string AMRestSTime = Convert.ToString(sdt.Rows[0]["AMRestSTime"]);
                    string AMRestETime = Convert.ToString(sdt.Rows[0]["AMRestETime"]);
                    string PMRestSTime = Convert.ToString(sdt.Rows[0]["PMRestSTime"]);
                    string PMRestETime = Convert.ToString(sdt.Rows[0]["PMRestETime"]);
                    string ShiftType = Convert.ToString(sdt.Rows[0]["ShiftType"]);
                    string dtPMRestSTime = "";
                    string dtPMRestETime = "";
                    if (TimeSpan.Parse(ShiftOnTime) < TimeSpan.Parse(ShiftOffTime))
                    {
                        dtShiftOnTime = DateTime.Parse(OTDate + " " + ShiftOnTime).ToString("yyyy/MM/dd HH:mm");
                        dtShiftOffTime = DateTime.Parse(OTDate + " " + ShiftOffTime).ToString("yyyy/MM/dd HH:mm");
                        dtAMRestSTime = DateTime.Parse(OTDate + " " + AMRestSTime).ToString("yyyy/MM/dd HH:mm");
                        dtAMRestETime = DateTime.Parse(OTDate + " " + AMRestETime).ToString("yyyy/MM/dd HH:mm");
                        if ((PMRestSTime.Length > 0) && (PMRestETime.Length > 0))
                        {
                            if (TimeSpan.Parse(PMRestSTime) <= TimeSpan.Parse(PMRestETime))
                            {
                                dtPMRestSTime = DateTime.Parse(OTDate + " " + PMRestSTime).ToString("yyyy/MM/dd HH:mm");
                                dtPMRestETime = DateTime.Parse(OTDate + " " + PMRestETime).ToString("yyyy/MM/dd HH:mm");
                            }
                            else
                            {
                                dtPMRestSTime = DateTime.Parse(OTDate + " " + PMRestSTime).ToString("yyyy/MM/dd HH:mm");
                                dtPMRestETime = DateTime.Parse(OTDate + " " + PMRestETime).AddDays(1.0).ToString("yyyy/MM/dd HH:mm");
                            }
                        }
                    }
                    else
                    {
                        dtShiftOnTime = DateTime.Parse(OTDate + " " + ShiftOnTime).ToString("yyyy/MM/dd HH:mm");
                        dtShiftOffTime = DateTime.Parse(OTDate + " " + ShiftOffTime).AddDays(1.0).ToString("yyyy/MM/dd HH:mm");
                        if ((TimeSpan.Parse(AMRestSTime) <= TimeSpan.Parse(AMRestETime)) && (TimeSpan.Parse(ShiftOnTime) < TimeSpan.Parse(AMRestETime)))
                        {
                            dtAMRestSTime = DateTime.Parse(OTDate + " " + AMRestSTime).ToString("yyyy/MM/dd HH:mm");
                            dtAMRestETime = DateTime.Parse(OTDate + " " + AMRestETime).ToString("yyyy/MM/dd HH:mm");
                        }
                        else if ((TimeSpan.Parse(AMRestSTime) <= TimeSpan.Parse(AMRestETime)) && (TimeSpan.Parse(ShiftOnTime) > TimeSpan.Parse(AMRestETime)))
                        {
                            dtAMRestSTime = DateTime.Parse(OTDate + " " + AMRestSTime).AddDays(1.0).ToString("yyyy/MM/dd HH:mm");
                            dtAMRestETime = DateTime.Parse(OTDate + " " + AMRestETime).AddDays(1.0).ToString("yyyy/MM/dd HH:mm");
                        }
                        else
                        {
                            dtAMRestSTime = DateTime.Parse(OTDate + " " + AMRestSTime).ToString("yyyy/MM/dd HH:mm");
                            dtAMRestETime = DateTime.Parse(OTDate + " " + AMRestETime).AddDays(1.0).ToString("yyyy/MM/dd HH:mm");
                        }
                        if ((PMRestSTime.Length > 0) && (PMRestETime.Length > 0))
                        {
                            dtPMRestSTime = DateTime.Parse(OTDate + " " + PMRestSTime).AddDays(1.0).ToString("yyyy/MM/dd HH:mm");
                            dtPMRestETime = DateTime.Parse(OTDate + " " + PMRestETime).AddDays(1.0).ToString("yyyy/MM/dd HH:mm");
                        }
                    }
                    if (string.Compare(dtBtime, dtEtime) >= 0)
                    {
                        return OtHours;
                    }
                    if (OTType.Equals("G1"))
                    {
                        if (((string.Compare(dtBtime, dtShiftOnTime) >= 0) && (string.Compare(dtShiftOffTime, dtBtime) > 0)) || ((string.Compare(dtEtime, dtShiftOnTime) > 0) && (string.Compare(dtShiftOffTime, dtEtime) >= 0)))
                        {
                            return OtHours;
                        }
                        if (string.Compare(dtShiftOnTime, dtBtime) > 0)
                        {
                            if (string.Compare(dtEtime, dtShiftOnTime) > 0)
                            {
                                return OtHours;
                            }
                            tsOTHours = (TimeSpan)(dtTempEndTime - dtTempBeginTime);
                            OtHours = tsOTHours.TotalMinutes;
                        }
                        else if (string.Compare(dtBtime, dtShiftOffTime) >= 0)
                        {
                            if (dtPMRestSTime.Length > 0)
                            {
                                if ((string.Compare(dtBtime, dtPMRestSTime) >= 0) && (string.Compare(dtPMRestETime, dtBtime) > 0))
                                {
                                    if ((string.Compare(dtEtime, dtPMRestSTime) > 0) && (string.Compare(dtPMRestETime, dtEtime) >= 0))
                                    {
                                        return OtHours;
                                    }
                                    tsOTHours = (TimeSpan)(dtTempEndTime - dtTempBeginTime);
                                    OtHours = tsOTHours.TotalMinutes;
                                    tsOTHours = (TimeSpan)(Convert.ToDateTime(dtPMRestETime) - dtTempBeginTime);
                                    RestHours += tsOTHours.TotalMinutes;
                                }
                                else if (string.Compare(dtPMRestSTime, dtBtime) >= 0)
                                {
                                    if (string.Compare(dtPMRestSTime, dtEtime) >= 0)
                                    {
                                        tsOTHours = (TimeSpan)(dtTempEndTime - dtTempBeginTime);
                                        OtHours = tsOTHours.TotalMinutes;
                                    }
                                    else if (string.Compare(dtPMRestETime, dtEtime) >= 0)
                                    {
                                        tsOTHours = (TimeSpan)(Convert.ToDateTime(dtPMRestSTime) - dtTempBeginTime);
                                        OtHours = tsOTHours.TotalMinutes;
                                    }
                                    else
                                    {
                                        tsOTHours = (TimeSpan)(dtTempEndTime - dtTempBeginTime);
                                        OtHours = tsOTHours.TotalMinutes;
                                        tsOTHours = (TimeSpan)(Convert.ToDateTime(dtPMRestETime) - Convert.ToDateTime(dtPMRestSTime));
                                        RestHours = tsOTHours.TotalMinutes;
                                    }
                                }
                                else
                                {
                                    tsOTHours = (TimeSpan)(dtTempEndTime - dtTempBeginTime);
                                    OtHours = tsOTHours.TotalMinutes;
                                }
                            }
                            else
                            {
                                tsOTHours = (TimeSpan)(dtTempEndTime - dtTempBeginTime);
                                OtHours = tsOTHours.TotalMinutes;
                            }
                        }
                    }
                    else
                    {
                        if (string.Compare(dtShiftOnTime, dtBtime) > 0)
                        {
                            dtTempBeginTime = Convert.ToDateTime(dtShiftOnTime);
                            dtBtime = dtTempBeginTime.ToString("yyyy/MM/dd HH:mm");
                        }
                        if ((string.Compare(dtAMRestETime, dtBtime) >= 0) && (string.Compare(dtBtime, dtAMRestSTime) >= 0))
                        {
                            dtTempBeginTime = Convert.ToDateTime(dtAMRestETime);
                            dtBtime = dtTempBeginTime.ToString("yyyy/MM/dd HH:mm");
                        }
                        if ((string.Compare(dtAMRestETime, dtEtime) >= 0) && (string.Compare(dtEtime, dtAMRestSTime) >= 0))
                        {
                            dtTempEndTime = Convert.ToDateTime(dtAMRestSTime);
                            dtEtime = dtTempEndTime.ToString("yyyy/MM/dd HH:mm");
                        }
                        if ((string.Compare(dtPMRestETime, dtBtime) >= 0) && (string.Compare(dtBtime, dtPMRestSTime) >= 0))
                        {
                            dtTempBeginTime = Convert.ToDateTime(dtPMRestETime);
                            dtBtime = dtTempBeginTime.ToString("yyyy/MM/dd HH:mm");
                        }
                        if ((string.Compare(dtPMRestETime, dtEtime) >= 0) && (string.Compare(dtEtime, dtPMRestSTime) >= 0))
                        {
                            dtTempEndTime = Convert.ToDateTime(dtPMRestSTime);
                            dtEtime = dtTempEndTime.ToString("yyyy/MM/dd HH:mm");
                        }
                        if (string.Compare(dtBtime, dtEtime) >= 0)
                        {
                            return OtHours;
                        }
                        if ((string.Compare(dtAMRestSTime, dtBtime) >= 0) && (string.Compare(dtEtime, dtAMRestETime) >= 0))
                        {
                            tsOTHours = (TimeSpan)(Convert.ToDateTime(dtAMRestETime) - Convert.ToDateTime(dtAMRestSTime));
                            RestHours += tsOTHours.TotalMinutes;
                            if ((dtPMRestETime.Length > 0) && (string.Compare(dtEtime, dtPMRestETime) > 0))
                            {
                                tsOTHours = (TimeSpan)(Convert.ToDateTime(dtPMRestETime) - Convert.ToDateTime(dtPMRestSTime));
                                RestHours += tsOTHours.TotalMinutes;
                            }
                        }
                        else if ((dtPMRestETime.Length > 0) && ((string.Compare(dtPMRestSTime, dtBtime) > 0) && (string.Compare(dtEtime, dtPMRestETime) > 0)))
                        {
                            tsOTHours = (TimeSpan)(Convert.ToDateTime(dtPMRestETime) - Convert.ToDateTime(dtPMRestSTime));
                            RestHours += tsOTHours.TotalMinutes;
                        }
                        tsOTHours = (TimeSpan)(dtTempEndTime - dtTempBeginTime);
                        OtHours = tsOTHours.TotalMinutes;
                    }
                    if (exceptionQryBll.GetValue("condition5", null, null, null, null).Equals("Y"))
                    {
                        OtHours = Math.Round((double)(Math.Floor((double)((OtHours - RestHours) / 10.0)) / 6.0), 1);
                    }
                    else
                    {
                        OtHours = Math.Round((double)(((OtHours - RestHours) / 60.0) * 100.0)) / 100.0;
                        if ((OtHours % 0.5) != 0.0)
                        {
                            double ihours = Math.Floor(OtHours);
                            if (OtHours > (ihours + 0.5))
                            {
                                OtHours = ihours + 0.5;
                            }
                            else
                            {
                                OtHours = ihours;
                            }
                        }
                    }
                    if ((OtHours >= 20.0) || (OtHours < 0.0))
                    {
                        OtHours = 0.0;
                    }
                }
                return OtHours;
            }
            catch (Exception)
            {
                return 0.0;
            }
        }


        public SortedList ReturnOTTTime(string WorkNo, string OTDate, DateTime dtTempBeginTime, DateTime dtTempEndTime, string ShiftNo)
        {
            string condition = "";
            SortedList list = new SortedList();
            DateTime dtMidTime = DateTime.Parse(OTDate + " 12:00");
            DateTime dtMidTime2 = DateTime.Parse(OTDate + " 08:00");
            string strTempBeginTime = dtTempBeginTime.ToString("yyyy/MM/dd HH:mm");
            string strTempEndTime = dtTempEndTime.ToString("yyyy/MM/dd HH:mm");
            string strMidTime = dtMidTime.ToString("yyyy/MM/dd HH:mm");
            string strMidTime2 = dtMidTime2.ToString("yyyy/MM/dd HH:mm");

            try
            {

                if (Convert.ToDecimal(exceptionQryBll.GetValue("condition1", strTempBeginTime, strTempEndTime, strMidTime, strMidTime2)) < 0M)
                {
                    dtTempEndTime = DateTime.Parse(DateTime.Parse(OTDate).AddDays(1.0).ToString("yyyy/MM/dd") + " " + dtTempEndTime.ToString("HH:mm"));
                }
                else if (ShiftNo.StartsWith("C"))
                {

                    if (Convert.ToDecimal(exceptionQryBll.GetValue("condition2", strTempBeginTime, strTempEndTime, strMidTime, strMidTime2)) < 0M)
                    {
                        dtTempBeginTime = DateTime.Parse(DateTime.Parse(OTDate).AddDays(1.0).ToString("yyyy/MM/dd") + " " + dtTempBeginTime.ToString("HH:mm"));
                        dtTempEndTime = DateTime.Parse(DateTime.Parse(OTDate).AddDays(1.0).ToString("yyyy/MM/dd") + " " + dtTempEndTime.ToString("HH:mm"));
                    }
                    else
                    {

                        if (Convert.ToDecimal(exceptionQryBll.GetValue("condition3", strTempBeginTime, strTempEndTime, strMidTime, strMidTime2)) < 0M)
                        {
                            dtTempEndTime = DateTime.Parse(DateTime.Parse(OTDate).AddDays(1.0).ToString("yyyy/MM/dd") + " " + dtTempEndTime.ToString("HH:mm"));
                        }
                    }
                }
                else if (ShiftNo.StartsWith("B"))
                {

                    if (Convert.ToDecimal(exceptionQryBll.GetValue("condition4", strTempBeginTime, strTempEndTime, strMidTime, strMidTime2)) < 0M)
                    {
                        dtTempBeginTime = DateTime.Parse(DateTime.Parse(OTDate).AddDays(1.0).ToString("yyyy/MM/dd") + " " + dtTempBeginTime.ToString("HH:mm"));
                        dtTempEndTime = DateTime.Parse(DateTime.Parse(OTDate).AddDays(1.0).ToString("yyyy/MM/dd") + " " + dtTempEndTime.ToString("HH:mm"));
                    }
                }
                list.Add("A", dtTempBeginTime);
                list.Add("B", dtTempEndTime);
                return list;
            }
            catch (Exception)
            {
                list.Add("A", dtTempBeginTime);
                list.Add("B", dtTempEndTime);
                return list;
            }
        }

        #endregion
    }
}
