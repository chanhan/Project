using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.Hr.PCM;
using GDSBG.MiABU.Attendance.Model.Hr.PCM;
using Resources;

/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： PCMKaoQinQryForm.aspx.cs
 * 檔功能描述： 考勤查詢
 * 
 * 版本：1.0
 * 創建標識： 昝望 2011.12.12
 * 
 */

namespace GDSBG.MiABU.Attendance.Web.Hr.PCM
{
    public partial class PCMKaoQinQryForm : BasePage
    {
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        int totalCount;
        static DataTable dt_global = new DataTable();
        KaoQinDataBll bll = new KaoQinDataBll();

        protected void Page_Load(object sender, EventArgs e)
        {
            SetCalendar(txtKQDateFrom, txtKQDateTo);
            if (!IsPostBack)
            {
                this.txtKQDateFrom.Text = DateTime.Now.AddDays(-4.0).ToShortDateString();
                this.txtKQDateTo.Text = DateTime.Now.ToShortDateString();
                ddlExceptionType.DataSource = bll.GetExceptionType();
                ddlExceptionType.DataTextField = "DataValue";
                ddlExceptionType.DataValueField = "DataCode";
                ddlExceptionType.DataBind();
                ddlShiftNo.DataSource = bll.GetKqmWorkShiftType();
                ddlShiftNo.DataTextField = "DataValue";
                ddlShiftNo.DataValueField= "DataCode";
                ddlShiftNo.DataBind();
                ddlShiftNo.Items.Insert(0, new ListItem("", ""));
                ddlStatus.DataSource = bll.GetKqmKaoQinStatus();
                ddlStatus.DataTextField = "DataValue";
                ddlStatus.DataValueField = "DataCode";
                ddlStatus.DataBind();
                ddlStatus.Items.Insert(0, new ListItem("", ""));
                pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();

            }
        }

        #region 查詢
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            pager.CurrentPageIndex = 1;
            string KQDateFrom = "";
            string KQDateTo = "";
            try
            {
                KQDateFrom = Convert.ToDateTime(this.txtKQDateFrom.Text).ToString("yyyy/MM/dd");
                KQDateFrom = Convert.ToDateTime(this.txtKQDateTo.Text).ToString("yyyy/MM/dd");
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.ErrDateFormat2 + "');", true);
                return;
            }
            string temVal = "";
            string strTemVal = ddlExceptionType.SelectedValue;
            if (strTemVal != "")
            {
                string temStr = this.ddlExceptionType.SelectedValuesToString(",");
                for (int i = 0; i < temStr.Split(',').Length; i++)
                {
                    temVal = temVal + temStr.Split(',')[i].Trim() + "§";
                }
            }

            DataTable dt = bll.GetKaoQinData(txtKQDateFrom.Text.Trim(), txtKQDateTo.Text.Trim(), temVal, ddlStatus.SelectedValue, ddlShiftNo.SelectedValue, CurrentUserInfo.Personcode, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            pager.RecordCount = totalCount;
            dt_global = bll.GetKaoQinData(txtKQDateFrom.Text.Trim(), txtKQDateTo.Text.Trim(), temVal, ddlStatus.SelectedValue, ddlShiftNo.SelectedValue, CurrentUserInfo.Personcode); ;
            DataBind(dt);
        }

        #endregion


        #region 數據綁定
        private void DataBind(DataTable dt)
        {
            UltraWebGridKaoQinQuery.DataSource = dt;
            UltraWebGridKaoQinQuery.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
        }
        #endregion

        #region 重置
        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.txtKQDateFrom.Text = DateTime.Now.AddDays(-4.0).ToShortDateString();
            this.txtKQDateTo.Text = DateTime.Now.ToShortDateString();
            this.ddlStatus.ClearSelection();
            this.ddlShiftNo.ClearSelection();
            this.ddlExceptionType.ClearSelection();
            DataTable dt = new DataTable();
            DataBind(dt);
        }
        #endregion

        #region 導出報表
        protected void btnExport_Click(object sender, EventArgs e)
        {
            KaoQinDataModel model = new KaoQinDataModel();
            List<KaoQinDataModel> list = bll.GetList(dt_global);
            string[] header = { ControlText.gvHeadDepName, ControlText.gvHeadWorkNo, ControlText.gvHeadLocalName, ControlText.gvHeadKQDate, ControlText.gvHeadStatusName, ControlText.gvHeadShiftDesc, ControlText.gvHeadOnDutyTime, ControlText.gvHeadOffDutyTime, ControlText.gvHeadOTOnDutyTime, ControlText.gvHeadOTOffDutyTime, ControlText.gvHeadAbsentQty, ControlText.gvHeadExceptionName, ControlText.gvHeadReasonName, ControlText.gvHeadReasonRemark};
            string[] properties = { "DepName", "WorkNo", "LocalName", "KQDate", "StatusName", "ShiftDesc", "OnDutyTime", "OffDutyTime", "OtOnDutyTime", "OtOffDutyTime", "AbsentQty", "ExceptionName", "ReasonName", "ReasonRemark" };
            string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
            NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
            PageHelper.ReturnHTTPStream(filePath, true);

        }
        #endregion

        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            string temVal = "";
            string strTemVal = ddlExceptionType.SelectedValue;
            if (strTemVal != "")
            {
                string temStr = this.ddlExceptionType.SelectedValuesToString(",");
                for (int i = 0; i < temStr.Split(',').Length; i++)
                {
                    temVal = temVal + temStr.Split(',')[i].Trim() + "§";
                }
            }

            DataTable dt = bll.GetKaoQinData(txtKQDateFrom.Text.Trim(), txtKQDateTo.Text.Trim(), temVal, ddlStatus.SelectedValue, ddlShiftNo.SelectedValue, CurrentUserInfo.Personcode, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            pager.RecordCount = totalCount;
            dt_global = bll.GetKaoQinData(txtKQDateFrom.Text.Trim(), txtKQDateTo.Text.Trim(), temVal, ddlStatus.SelectedValue, ddlShiftNo.SelectedValue, CurrentUserInfo.Personcode); ;
            DataBind(dt);
        }
        #endregion
    }
}
