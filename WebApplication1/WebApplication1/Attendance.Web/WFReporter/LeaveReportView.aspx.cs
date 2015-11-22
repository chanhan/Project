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


namespace GDSBG.MiABU.Attendance.Web.WFReporter
{
    public partial class LeaveReportView : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        #region 查詢
        protected void btnQuery_Click(object sender, EventArgs e)
        {
     
        }

        #endregion


        #region 數據綁定
        private void DataBind(DataTable dt)
        {
            //UltraWebGridKaoQinQuery.DataSource = dt;
            //UltraWebGridKaoQinQuery.DataBind();
            //pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
        }
        #endregion

        #region 重置
        protected void btnReset_Click(object sender, EventArgs e)
        {

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
            //string temVal = "";
            //string strTemVal = ddlExceptionType.SelectedValue;
            //if (strTemVal != "")
            //{
            //    string temStr = this.ddlExceptionType.SelectedValuesToString(",");
            //    for (int i = 0; i < temStr.Split(',').Length; i++)
            //    {
            //        temVal = temVal + temStr.Split(',')[i].Trim() + "§";
            //    }
            //}

            //DataTable dt = bll.GetKaoQinData(txtKQDateFrom.Text.Trim(), txtKQDateTo.Text.Trim(), temVal, ddlStatus.SelectedValue, ddlShiftNo.SelectedValue, CurrentUserInfo.Personcode, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            //pager.RecordCount = totalCount;
            //dt_global = bll.GetKaoQinData(txtKQDateFrom.Text.Trim(), txtKQDateTo.Text.Trim(), temVal, ddlStatus.SelectedValue, ddlShiftNo.SelectedValue, CurrentUserInfo.Personcode); ;
            //DataBind(dt);
        }
        #endregion
    }
}
