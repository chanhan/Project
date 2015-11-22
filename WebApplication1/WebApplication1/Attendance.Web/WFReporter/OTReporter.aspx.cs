
/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： Module.aspx.cs
 * 檔功能描述： 系統模組
 * 
 * 版本：1.0
 * 創建標識： 張明強 2011.11.28
 * 
 */

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Infragistics.WebUI.UltraWebGrid;
using System.Drawing;
using GDSBG.MiABU.Attendance.BLL.WFReporter;



namespace GDSBG.MiABU.Attendance.Web.WFReporter
{
    public partial class OTReporter : BasePage
    {
        OTReporterBll oTReporterBll = new OTReporterBll();
        static DataTable oTReporterTable = new DataTable();
        string yearMonth = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtYearMonth.Text = DateTime.Now.ToString("yyyy/MM");
                pager.CurrentPageIndex = 1;
                yearMonth = this.txtYearMonth.Text.ToString().Trim().Replace("/","");
                WebGridDataBind();
            }
        }

        #region 查询
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            pager.CurrentPageIndex = 1;
            yearMonth = this.txtYearMonth.Text.ToString().Trim().Replace("/", "");
            WebGridDataBind();
        }
        #endregion

        #region 導出報表
        protected void btnExport_Click(object sender, EventArgs e)
        {
            //if (this.UltraWebGridOTMMonthTotal.Rows.Count == 0)
            //{
            //    Page.ClientScript.RegisterStartupScript(GetType(), "show", "alert('" + Message.NoDataExport + "')", true);
            //}
            //else
            //{
            //    string tmpFilePath = MapPath("E:\\ExcelModelTEST.xls");
            //    List<OTMTotalQryModel> list = bllOTMQry.GetList(dt);
            //    string[] header = { ControlText.gvBuOTMQryName, ControlText.gvDName, ControlText.gvWorkNo, ControlText.gvLocalName, ControlText.gvOverTimeType,"1","2","3","4","5","6","7","8","9","10","11",	"12","13","14",	"15","16","17","18","19","20","21","22","23","24","25","26"	,"27","28","29","30","31",ControlText.gvG1Apply, ControlText.gvG2Apply, ControlText.gvG3Apply, ControlText.gvG1RelSalary, ControlText.gvG2RelSalary,
            //                            ControlText.gvG3RelSalary};
            //    string[] properties = { "BuName", "DName", "WorkNo", "LocalName", "OverTimeType","Day1" ,"Day2"	,"Day3"	,"Day4"	,"Day5"	,"Day6"	,"Day7"	,"Day8","Day9"	,"Day10","Day11","Day12","Day13","Day14","Day15","Day16","Day17","Day18","Day19","Day20","Day21","Day22","Day23","Day24","Day25"
            //                               ,"Day26"	,"Day27","Day28","Day29","Day30","Day31", "G1Apply", "G2Apply", "G3Apply", "G1RelSalary", "G2RelSalary", "G3RelSalary"};
            //    string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
            //    string yearMonth = txtYearMonth.Text.Trim().ToString();
            //    NPOIHelper.ExportExcel(list, properties, 5000, 3, tmpFilePath, filePath, yearMonth);
            //    PageHelper.ReturnHTTPStream(filePath, true);
            //}
        }
        #endregion

        protected void WebGridDataBind()
        {
            int totalCount = 0;
            oTReporterTable = oTReporterBll.GetOTMQryList(base.SqlDep.ToString(), yearMonth, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            this.UltraWebGridOTMMonthTotal.DataSource = oTReporterTable.DefaultView;
            this.UltraWebGridOTMMonthTotal.DataBind();
            pager.RecordCount = totalCount;
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
        }

        #region WebGrid綁定
        protected void UltraWebGridOTMMonthTotal_DataBound(object sender, EventArgs e)
        {
            //decimal Normal = 0M;
            //decimal Spec = 0M;
            //for (int i = 0; i < this.UltraWebGridOTMMonthTotal.Rows.Count; i++)
            //{
                int k;
                //if (this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("ApproveFlag").Text.Trim() == "0")
                //{
                //    this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("ApproveFlagName").Style.ForeColor = Color.Green;
                //}
                //if (this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("ApproveFlag").Text.Trim() == "1")
                //{
                //    this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("ApproveFlagName").Style.ForeColor = Color.Blue;
                //}
                //if (this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("ApproveFlag").Text.Trim() == "2")
                //{
                //    this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("ApproveFlagName").Style.ForeColor = Color.Black;
                //}
                //if (this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("ApproveFlag").Text.Trim() == "3")
                //{
                //    this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("ApproveFlagName").Style.ForeColor = Color.Maroon;
                //}
                //if (this.QueryFlag.Value == "Spec")
                //{
                //    k = 1;
                //    while (k < 0x20)
                //    {
                //        this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("Day" + k.ToString()).Text = this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("SpecDay" + k.ToString()).Text;
                //        k++;
                //    }
                //}
                //if (this.QueryFlag.Value == "All")
                //{
                //    for (k = 1; k < 0x20; k++)
                //    {
                //        Normal = Convert.ToDecimal((Convert.ToString(this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("Day" + k.ToString()).Text) == "") ? "0" : Convert.ToString(this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("Day" + k.ToString()).Text));
                //        Spec = Convert.ToDecimal((Convert.ToString(this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("SpecDay" + k.ToString()).Text) == "") ? "0" : Convert.ToString(this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("SpecDay" + k.ToString()).Text));
                //        this.UltraWebGridOTMMonthTotal.Rows[i].Cells.FromKey("Day" + k.ToString()).Text = Convert.ToString((decimal)(Normal + Spec));
                //    }
                //}
            //}
        }
        #endregion


        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            WebGridDataBind();
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

                for (i = 1; i < 0x20; i++)
                {
                    ColumnHeader chr = new ColumnHeader(true)
                    {
                        Caption = i.ToString()
                    };
                    chr.RowLayoutColumnInfo.OriginY = 0;
                    chr.RowLayoutColumnInfo.OriginX = i + 6;
                    chr.RowLayoutColumnInfo.SpanX = 1;
                    chr.Style.HorizontalAlign = HorizontalAlign.Center;
                    e.Layout.Bands[0].HeaderLayout.Add(chr);
                }
                ColumnHeader ch = new ColumnHeader(true)
                {
                    Caption = Resources.ControlText.gvOTApplyHours
                };
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch.RowLayoutColumnInfo.OriginX = 69;
                ch.RowLayoutColumnInfo.SpanX = 3;
                ch.Style.HorizontalAlign = HorizontalAlign.Center;
                e.Layout.Bands[0].HeaderLayout.Add(ch);
                ColumnHeader ch2 = new ColumnHeader(true)
                {
                    Caption = Resources.ControlText.gvOTSalaryHours
                };
                ch2.RowLayoutColumnInfo.OriginY = 0;
                ch2.RowLayoutColumnInfo.OriginX =72;
                ch2.RowLayoutColumnInfo.SpanX = 3;
                ch2.Style.HorizontalAlign = HorizontalAlign.Center;
                e.Layout.Bands[0].HeaderLayout.Add(ch2);
               
                
                
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
                ch = e.Layout.Bands[0].Columns.FromKey("depcode").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch = e.Layout.Bands[0].Columns.FromKey("DName").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch = e.Layout.Bands[0].Columns.FromKey("OverTimeType").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
                //ch = e.Layout.Bands[0].Columns.FromKey("G2Remain").Header;
                //ch.RowLayoutColumnInfo.SpanY = 2;
                //ch.RowLayoutColumnInfo.OriginY = 0;
                //ch = e.Layout.Bands[0].Columns.FromKey("MAdjust1").Header;
                //ch.RowLayoutColumnInfo.SpanY = 2;
                //ch.RowLayoutColumnInfo.OriginY = 0;
                //ch = e.Layout.Bands[0].Columns.FromKey("MRelAdjust").Header;
                //ch.RowLayoutColumnInfo.SpanY = 2;
                //ch.RowLayoutColumnInfo.OriginY = 0;
                //ch = e.Layout.Bands[0].Columns.FromKey("ApproveFlagName").Header;
                //ch.RowLayoutColumnInfo.SpanY = 2;
                //ch.RowLayoutColumnInfo.OriginY = 0;
                //ch = e.Layout.Bands[0].Columns.FromKey("AdvanceAdjust").Header;
                //ch.RowLayoutColumnInfo.SpanY = 2;
                //ch.RowLayoutColumnInfo.OriginY = 0;
                //ch = e.Layout.Bands[0].Columns.FromKey("RestAdjust").Header;
                //ch.RowLayoutColumnInfo.SpanY = 2;
                //ch.RowLayoutColumnInfo.OriginY = 0;
                //ch = e.Layout.Bands[0].Columns.FromKey("auditer").Header;
                //ch.RowLayoutColumnInfo.SpanY = 2;
                //ch.RowLayoutColumnInfo.OriginY = 0;
                //ch = e.Layout.Bands[0].Columns.FromKey("auditdate").Header;
                //ch.RowLayoutColumnInfo.SpanY = 2;
                //ch.RowLayoutColumnInfo.OriginY = 0;
                //ch = e.Layout.Bands[0].Columns.FromKey("auditidea").Header;
                //ch.RowLayoutColumnInfo.SpanY = 2;
                //ch.RowLayoutColumnInfo.OriginY = 0;
                
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
    }
}
