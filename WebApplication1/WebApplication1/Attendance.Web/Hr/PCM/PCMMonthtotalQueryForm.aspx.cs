using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.Hr.PCM;
using Infragistics.WebUI.UltraWebGrid;
using Resources;

/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： PCMMonthtotalQueryForm.aspx.cs
 * 檔功能描述： 月加班匯總查詢
 * 
 * 版本：1.0
 * 創建標識： 昝望 2011.12.12
 * 
 */

namespace GDSBG.MiABU.Attendance.Web.Hr.PCM
{
    public partial class PCMMonthtotalQueryForm : BasePage
    {
        int totalCount;
        static DataTable dt_global = new DataTable();
        MonthtotalBll bll = new MonthtotalBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            PageHelper.ButtonControls(base.FuncList, pnlShowPanel.Controls, base.FuncListModule);
            if (!IsPostBack)
            {
                this.txtYearMonth.Text = DateTime.Now.ToString("yyyy/MM");
                pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
            }
        }

        #region 查詢
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            pager.CurrentPageIndex = 1;
            string YearMonth = this.txtYearMonth.Text.Replace("/", "");
            if (YearMonth != "")
            {
                DataTable dt = bll.GetMonthtotal(CurrentUserInfo.Personcode, YearMonth, pager.CurrentPageIndex, pager.PageSize, out totalCount);
                pager.RecordCount = totalCount;
                dt_global = bll.GetMonthtotal(CurrentUserInfo.Personcode, YearMonth); 
                DataBind(dt);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.YearMonthNotNull + "');", true);
            }
        }

        #endregion

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
                    Caption = ControlText.gvApplyHours
                };
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch.RowLayoutColumnInfo.OriginX = 5;
                ch.RowLayoutColumnInfo.SpanX = 3;
                ch.Style.HorizontalAlign = HorizontalAlign.Center;
                e.Layout.Bands[0].HeaderLayout.Add(ch);
                ColumnHeader ch1 = new ColumnHeader(true)
                {
                    Caption = ControlText.gvHeadPMLT
                };
                ch1.RowLayoutColumnInfo.OriginY = 0;
                ch1.RowLayoutColumnInfo.OriginX = 8;
                ch1.RowLayoutColumnInfo.SpanX = 4;
                ch1.Style.HorizontalAlign = HorizontalAlign.Center;
                e.Layout.Bands[0].HeaderLayout.Add(ch1);
                ColumnHeader ch2 = new ColumnHeader(true)
                {
                    Caption = ControlText.gvRelSalaryHours
                };
                ch2.RowLayoutColumnInfo.OriginY = 0;
                ch2.RowLayoutColumnInfo.OriginX = 12;
                ch2.RowLayoutColumnInfo.SpanX = 3;
                ch2.Style.HorizontalAlign = HorizontalAlign.Center;
                e.Layout.Bands[0].HeaderLayout.Add(ch2);
                ColumnHeader ch3 = new ColumnHeader(true)
                {
                    Caption = ControlText.gvHeadRemain
                };
                ch3.RowLayoutColumnInfo.OriginY = 0;
                ch3.RowLayoutColumnInfo.OriginX = 15;
                ch3.RowLayoutColumnInfo.SpanX = 3;
                ch3.Style.HorizontalAlign = HorizontalAlign.Center;
                e.Layout.Bands[0].HeaderLayout.Add(ch3);
                ColumnHeader ch4 = new ColumnHeader(true)
                {
                    Caption = ControlText.gvSpecApplyHours
                };
                ch4.RowLayoutColumnInfo.OriginY = 0;
                ch4.RowLayoutColumnInfo.OriginX = 0x12;
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
                    chr.RowLayoutColumnInfo.OriginX = i + 0x19;
                    chr.RowLayoutColumnInfo.SpanX = 1;
                    chr.Style.HorizontalAlign = HorizontalAlign.Center;
                    e.Layout.Bands[0].HeaderLayout.Add(chr);
                }
                ch = e.Layout.Bands[0].Columns.FromKey("WorkNo").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch = e.Layout.Bands[0].Columns.FromKey("LocalName").Header;
                ch.RowLayoutColumnInfo.SpanY = 2;
                ch.RowLayoutColumnInfo.OriginY = 0;
                ch = e.Layout.Bands[0].Columns.FromKey("DepName").Header;
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

        private string GetWeek(DateTime SelectYearMonth)
        {
            string getWeek = SelectYearMonth.DayOfWeek.ToString();
            switch (getWeek)
            {
                case "Sunday":
                    return ControlText.gvHeadSunday;

                case "Monday":
                    return ControlText.gvHeadMonday;

                case "Tuesday":
                    return ControlText.gvHeadTuesday;

                case "Wednesday":
                    return ControlText.gvHeadWednesday;

                case "Thursday":
                    return ControlText.gvHeadThursday;

                case "Friday":
                    return ControlText.gvHeadFriday;

                case "Saturday":
                    return ControlText.gvHeadSaturday;
            }
            return "";
        }

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

        #region 數據綁定
        private void DataBind(DataTable dt)
        {
            UltraWebGridOTMMonthTotal.DataSource = dt;
            UltraWebGridOTMMonthTotal.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
        }
        #endregion

        #region 重置
        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.txtYearMonth.Text = DateTime.Now.ToString("yyyy/MM");
        }
        #endregion

        #region 導出報表
        protected void btnExport_Click(object sender, EventArgs e)
        {
            //KaoQinDataModel model = new KaoQinDataModel();
            //List<KaoQinDataModel> list = bll.GetList(dt_global);
            //string[] header = { ControlText.gvHeadDepName, ControlText.gvHeadWorkNo, ControlText.gvHeadLocalName, ControlText.gvHeadKQDate, ControlText.gvHeadStatusName, ControlText.gvHeadShiftDesc, ControlText.gvHeadOnDutyTime, ControlText.gvHeadOffDutyTime, ControlText.gvHeadOTOnDutyTime, ControlText.gvHeadOTOffDutyTime, ControlText.gvHeadAbsentQty, ControlText.gvHeadExceptionName, ControlText.gvHeadReasonName, ControlText.gvHeadReasonRemark };
            //string[] properties = { "DepName", "WorkNo", "LocalName", "KQDate", "StatusName", "ShiftDesc", "OnDutyTime", "OffDutyTime", "OtOnDutyTime", "OtOffDutyTime", "AbsentQty", "ExceptionName", "ReasonName", "ReasonRemark" };
            //string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
            //NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
            //PageHelper.ReturnHTTPStream(filePath, true);

        }
        #endregion

        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            string YearMonth = this.txtYearMonth.Text.Replace("/", "");
            if (YearMonth != "")
            {
                DataTable dt = bll.GetMonthtotal(CurrentUserInfo.Personcode, YearMonth, pager.CurrentPageIndex, pager.PageSize, out totalCount);
                pager.RecordCount = totalCount;
                dt_global = bll.GetMonthtotal(CurrentUserInfo.Personcode, YearMonth); 
                DataBind(dt);
            }
        }
        #endregion
    }
}
