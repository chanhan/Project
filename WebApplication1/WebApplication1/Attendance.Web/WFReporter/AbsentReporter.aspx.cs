/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： AbsentReporter.aspx.cs
 * 檔功能描述：缺勤統計表UI
 * 
 * 版本：1.0
 * 創建標識： 顧陳偉 2012.3.15
 * 
 * 
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.WFReporter;
using GDSBG.MiABU.Attendance.Model.WFReporter;
using Resources;

namespace GDSBG.MiABU.Attendance.Web.WFReporter
{
    public partial class AbsentReporter : BasePage
    {
        AbsentReportBll bllAbsentReport = new AbsentReportBll();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;

        #region 頁面加載
        protected void Page_Load(object sender, EventArgs e)
        {
            SetCalendar(txtKQDateFrom, txtKQDateTo);
            if (!base.IsPostBack)
            {
                this.ModuleCode.Value = base.Request.QueryString["ModuleCode"].ToString();
                this.ddlAbsentType.Items.Add(new ListItem("遲到早退", "ab"));
                this.ddlAbsentType.Items.Add(new ListItem("曠工", "c"));
                this.ddlAbsentType.Items.Add(new ListItem("事假", "i"));
                this.ddlAbsentType.Items.Add(new ListItem("病假", "t"));
                this.ddlAbsentType.Items.Add(new ListItem("婚假", "j"));
                this.ddlAbsentType.Items.Add(new ListItem("產假", "s"));
                this.ddlAbsentType.Items.Add(new ListItem("喪假", "k"));
                this.ddlAbsentType.Items.Add(new ListItem("節育假", "v"));
                this.ddlAbsentType.Items.Add(new ListItem("年休假", "y"));
                this.ddlAbsentType.Items.Add(new ListItem("公休假", "r"));
                this.ddlAbsentType.Items.Add(new ListItem("因私事假", "x"));
                this.ddlAbsentType.Items.Add(new ListItem("醫療期", "z"));
                this.ddlAbsentType.Items.Add(new ListItem("未刷補卡次數", "numcount"));
                this.ddlAbsentType.Items.Insert(0, new ListItem("", ""));
            }
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("WrongDate", Message.WrongDate);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
        }
        #endregion

        #region 查询
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            pager.CurrentPageIndex = 1;
            WebGridDataBind();
        }
        #endregion

        #region 导出
        protected void btnExport_Click(object sender, EventArgs e)
        {
            string depcode = this.txtDepCode.Text;
            string workno = this.txtWorkNo.Text;
            string localname = this.txtLocalname.Text;
            string fromDate = this.txtKQDateFrom.Text.Trim();
            string toDate = this.txtKQDateTo.Text.Trim();
            string yearmonth = this.txtYearMonth.Text.ToString().Trim().Replace("/", "");
            string absenttype = this.ddlAbsentType.SelectedValue.ToString();
            string status = this.txtStatus.Text;
            string sql = base.SqlDep;
            DataTable dt = bllAbsentReport.GetAbsentInfoForExport(sql, depcode, workno, localname, fromDate, toDate, yearmonth, absenttype, status);
            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "nodataexport", "alert('" + Message.NoDataExport + "');", true);
            }
            else
            {
                List<AbsentReporterModel> list = bllAbsentReport.GetList(dt);
                string[] header = { ControlText.gvBuOTMQryName, ControlText.gvHeadDepName, ControlText.gvHeadWorkNo, ControlText.gvHeadLocalName, ControlText.gvLateEarly, ControlText.gvAbsent, ControlText.gvShortAffairLeave, ControlText.gvSickLeave, ControlText.gvMarriageLeave, ControlText.gvMaternityLeave, ControlText.gvCompassionateLeave, ControlText.gvBirthControlLeave, ControlText.gvLeaveApplyYear, ControlText.gvSabbaticalLeave, ControlText.gvLongAffairLeave, ControlText.gvTreatmentLeave, ControlText.gvMarkUpCount, ControlText.gvAbsentRemark };
                string[] properties = { "BGName", "DepName", "WorkNo", "LocalName", "AB", "C", "I", "T", "J", "S", "K", "V", "Y", "R", "X", "Z", "Numcount", "Remark" };
                string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
                PageHelper.ReturnHTTPStream(filePath, true);
            }
        }
        #endregion

        #region 數據綁定
        protected void WebGridDataBind()
        {
            int totalCount = 0;
            string depcode = this.txtDepCode.Text;
            string workno = this.txtWorkNo.Text;
            string localname = this.txtLocalname.Text;
            string fromDate = this.txtKQDateFrom.Text.Trim();
            string toDate = this.txtKQDateTo.Text.Trim();
            string yearmonth = this.txtYearMonth.Text.ToString().Trim().Replace("/", "");
            string absenttype = this.ddlAbsentType.SelectedValue.ToString();
            string status = this.txtStatus.Text;
            string sql = base.SqlDep;
            DataTable dt = bllAbsentReport.GetAbsentInfo(sql, depcode, workno, localname, fromDate, toDate, yearmonth, absenttype, status, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            this.UltraWebGridAbsentTotal.DataSource = dt.DefaultView;
            this.UltraWebGridAbsentTotal.DataBind();
            pager.RecordCount = totalCount;
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
        }
        #endregion

        #region WebGrid綁定
        protected void UltraWebGridAbsentTotal_DataBound(object sender, EventArgs e)
        {
            //decimal Normal = 0M;
            //decimal Spec = 0M;
            //for (int i = 0; i < this.UltraWebGridAbsentTotal.Rows.Count; i++)
            //{
            //    int k;
            //    if (this.UltraWebGridAbsentTotal.Rows[i].Cells.FromKey("ApproveFlag").Text.Trim() == "0")
            //    {
            //        this.UltraWebGridAbsentTotal.Rows[i].Cells.FromKey("ApproveFlagName").Style.ForeColor = Color.Green;
            //    }
            //    if (this.UltraWebGridAbsentTotal.Rows[i].Cells.FromKey("ApproveFlag").Text.Trim() == "1")
            //    {
            //        this.UltraWebGridAbsentTotal.Rows[i].Cells.FromKey("ApproveFlagName").Style.ForeColor = Color.Blue;
            //    }
            //    if (this.UltraWebGridAbsentTotal.Rows[i].Cells.FromKey("ApproveFlag").Text.Trim() == "2")
            //    {
            //        this.UltraWebGridAbsentTotal.Rows[i].Cells.FromKey("ApproveFlagName").Style.ForeColor = Color.Black;
            //    }
            //    if (this.UltraWebGridAbsentTotal.Rows[i].Cells.FromKey("ApproveFlag").Text.Trim() == "3")
            //    {
            //        this.UltraWebGridAbsentTotal.Rows[i].Cells.FromKey("ApproveFlagName").Style.ForeColor = Color.Maroon;
            //    }
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
    }
}
