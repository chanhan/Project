/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： OTMTotalQry.cs
 * 檔功能描述： 加班匯總查詢UI層
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2012.01.09
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.KQM.BasicData;
using GDSBG.MiABU.Attendance.BLL.KQM.Query.OTM;
using GDSBG.MiABU.Attendance.Model.KQM.Query.OTM;
using Infragistics.WebUI.UltraWebGrid;
using Resources;
using System.Web.Script.Serialization;

namespace GDSBG.MiABU.Attendance.Web.KQM.Query.OTM
{
    public partial class OTMTotalQry : BasePage
    {
        TypeDataBll bllTypeData = new TypeDataBll();
        DataTable tempDataTable = new DataTable();
        static DataTable dt = new DataTable();
        OTMTotalQryModel model = new OTMTotalQryModel();
        OTMTotalQryBll bllOTMQry = new OTMTotalQryBll();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        #region 導出報表
        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (this.UltraWebGridOTMMonthTotal.Rows.Count == 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.NoDataExport + "')", true);
            }
            else
            {
                string tmpFilePath = MapPath("~/ExcelModel/OTMTotalQryTmp.xls");
                List<OTMTotalQryModel> list = bllOTMQry.GetList(dt);
                string[] header = { ControlText.gvBuOTMQryName, ControlText.gvDName, ControlText.gvWorkNo, ControlText.gvLocalName, ControlText.gvOverTimeType, ControlText.gvG1Apply, ControlText.gvG2Apply, ControlText.gvG3Apply, ControlText.gvG1RelSalary, ControlText.gvG2RelSalary,
                                        ControlText.gvG3RelSalary, ControlText.gvSpecG1Apply, ControlText.gvSpecG2Apply, ControlText.gvSpecG3Apply, ControlText.gvSpecG1Salary, ControlText.gvSpecG2Salary, ControlText.gvSpecG3Salary, ControlText.gvG2Remain, ControlText.gvMAdjust1, 
                                        ControlText.gvMRelAdjust, ControlText.gvApproveFlagName,"1","2","3","4","5"	,"6","7","8","9","10","11",	"12","13","14",	"15","16","17","18","19","20","21","22","23","24","25","26"	,"27","28","29","30","31"};
                string[] properties = { "BuName", "DName", "WorkNo", "LocalName", "OverTimeType", "G1Apply", "G2Apply", "G3Apply", "G1RelSalary", "G2RelSalary", "G3RelSalary", "SpecG1Apply", "SpecG2Apply", "SpecG3Apply", "SpecG1Salary","SpecG2Salary","SpecG3Salary","G2Remain", "MAdjust1", "MRelAdjust", "ApproveFlagName","Day1"
                                           ,"Day2"	,"Day3"	,"Day4"	,"Day5"	,"Day6"	,"Day7"	,"Day8","Day9"	,"Day10","Day11","Day12","Day13","Day14","Day15","Day16","Day17","Day18","Day19","Day20","Day21","Day22","Day23","Day24","Day25"
                                           ,"Day26"	,"Day27","Day28","Day29","Day30","Day31"};
                string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                string yearMonth = txtYearMonth.Text.Trim().ToString();
                NPOIHelper.ExportExcel(list, properties, 5000, 3, tmpFilePath, filePath, yearMonth);
                PageHelper.ReturnHTTPStream(filePath, true);
            }
        }
        #endregion
        #region  查詢按鈕  獲得model值
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            model = PageHelper.GetModel<OTMTotalQryModel>(pnlContent.Controls);
            pager.CurrentPageIndex = 1;
            DataUIBind();
            this.txtBatchEmployeeNo.Text = "";
        }
        #endregion
        #region  重置按鈕
        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.txtDepCode.Text = "";
            this.txtDepName.Text = "";
            this.txtWorkNo.Text = "";
            this.txtLocalName.Text = "";
            this.txtBatchEmployeeNo.Text = "";
            this.ddlOTTypeCode.ClearSelection();
            this.ddlApproveFlag.ClearSelection();
            this.txtYearMonth.Text = DateTime.Now.ToString("yyyy/MM");
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

        #region 頁面加載
        protected void Page_Load(object sender, EventArgs e)
        {
            //————按鈕事件設置 start—————————
            PageHelper.ButtonControls(base.FuncList, pnlShowControls.Controls, base.FuncListModule);
            //————————end—————————————
            if (!base.IsPostBack)
            {
                SetSelector(imgDepCode, txtDepCode, txtDepName, Request.QueryString["ModuleCode"].ToString());
                this.imgBatchWorkNo.Attributes.Add("OnClick", "javascript:ShowBatchWorkNo()");
                this.txtYearMonth.Text = DateTime.Now.ToString("yyyy/MM");
                ddlApproveFlagBind();
                ddlOTTypeCodeBind();
            }
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("TextBoxNotNull", Message.TextBoxNotNull);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);

        }
        #endregion
        #region 簽核狀態綁定
        private void ddlApproveFlagBind()
        {
            this.tempDataTable.Clear();
            this.tempDataTable = bllTypeData.GetDataTypeList("'ApproveFlag'");
            this.ddlApproveFlag.DataSource = tempDataTable;
            this.ddlApproveFlag.DataTextField = "DataValue";
            this.ddlApproveFlag.DataValueField = "DataCode";
            this.ddlApproveFlag.DataBind();
        }
        #endregion
        #region 加班類別綁定
        private void ddlOTTypeCodeBind()
        {
            this.tempDataTable.Clear();
            this.tempDataTable = bllTypeData.GetDataTypeList("'OverTimeType'");
            this.ddlOTTypeCode.DataSource = tempDataTable;
            this.ddlOTTypeCode.DataTextField = "DataValue";
            this.ddlOTTypeCode.DataValueField = "DataCode";
            this.ddlOTTypeCode.DataBind();
        }
        #endregion
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

        #region UltraWebGridOTMMonthTotal_DataBound
        protected void UltraWebGridOTMMonthTotal_DataBound(object sender, EventArgs e)
        {
            for (int i = 0; i < this.UltraWebGridOTMMonthTotal.Rows.Count; i++)
            {
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
            }
        }
        #endregion
        #region 獲得webgrid的表頭
        protected void UltraWebGridOTMMonthTotal_InitializeLayout(object sender, LayoutEventArgs e)
        {
            int i;
            DateTime YearMonth = Convert.ToDateTime(this.txtYearMonth.Text + "/01");
            if (this.hidYearMonth.Value.Length == 0)
            {
                this.hidYearMonth.Value = this.txtYearMonth.Text.Replace("/", "");
                foreach (UltraGridColumn c in e.Layout.Bands[0].Columns)
                {
                    c.Header.RowLayoutColumnInfo.OriginY = 1;
                }
                ColumnHeader ch = new ColumnHeader(true)
                {
                    Caption = Resources.ControlText.gvApplyHours
                };
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch.RowLayoutColumnInfo.OriginX = 5;
                ch.RowLayoutColumnInfo.SpanX = 3;
                ch.Style.HorizontalAlign = HorizontalAlign.Center;
                e.Layout.Bands[0].HeaderLayout.Add(ch);
                ColumnHeader ch2 = new ColumnHeader(true)
                {
                    Caption = Resources.ControlText.gvRelSalaryHours
                };
                ch2.RowLayoutColumnInfo.OriginY = 0;
                ch2.RowLayoutColumnInfo.OriginX = 8;
                ch2.RowLayoutColumnInfo.SpanX = 3;
                ch2.Style.HorizontalAlign = HorizontalAlign.Center;
                e.Layout.Bands[0].HeaderLayout.Add(ch2);
                ColumnHeader ch3 = new ColumnHeader(true)
                {
                    Caption = Resources.ControlText.gvSpecApplyHours
                };
                ch3.RowLayoutColumnInfo.OriginY = 0;
                ch3.RowLayoutColumnInfo.OriginX = 11;
                ch3.RowLayoutColumnInfo.SpanX = 3;
                ch3.Style.HorizontalAlign = HorizontalAlign.Center;
                e.Layout.Bands[0].HeaderLayout.Add(ch3);
                ColumnHeader ch4 = new ColumnHeader(true)
                {
                    Caption = Resources.ControlText.gvSpecrelSalary
                };
                ch4.RowLayoutColumnInfo.OriginY = 0;
                ch4.RowLayoutColumnInfo.OriginX = 14;
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
                    chr.RowLayoutColumnInfo.OriginX = i + 0x15;
                    chr.RowLayoutColumnInfo.SpanX = 1;
                    chr.Style.HorizontalAlign = HorizontalAlign.Center;
                    e.Layout.Bands[0].HeaderLayout.Add(chr);
                }
                ch = e.Layout.Bands[0].Columns.FromKey("BuName").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch = e.Layout.Bands[0].Columns.FromKey("WorkNo").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch = e.Layout.Bands[0].Columns.FromKey("LocalName").Header;
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
            }
            else
            {
                this.hidYearMonth.Value = this.txtYearMonth.Text.Replace("/", "");
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
        #region 綁定數據
        private void DataUIBind()
        {
            string BatchEmployeeNo = "";
            string overTimeType = "";
            string approveFlag = "";
            int totalCount;
            string SQLDep = base.SqlDep;
            if (this.txtYearMonth.Text.Length == 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert(''" + lblYearMonth + "'+'" + Message.TextBoxNotNull + "'')", true);
            }
            string YearMonth = this.txtYearMonth.Text.Replace("/", "");
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
            string depCode = model.DepCode;
            model.YearMonth = YearMonth;
            model.DepCode = null;
            model.DName = null;
            DataTable dtSel = bllOTMQry.GetOTMQryList(model, depCode, SQLDep, BatchEmployeeNo, overTimeType, approveFlag, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            dt = bllOTMQry.GetOTMQryList(model, depCode, SQLDep, BatchEmployeeNo, overTimeType, approveFlag);
            this.UltraWebGridOTMMonthTotal.DataSource = dtSel;
            this.UltraWebGridOTMMonthTotal.DataBind();
            pager.RecordCount = totalCount;
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
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
