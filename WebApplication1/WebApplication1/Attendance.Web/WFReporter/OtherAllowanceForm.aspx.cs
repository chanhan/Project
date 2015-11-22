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
using GDSBG.MiABU.Attendance.BLL.Hr.KQM.KaoQinData;

namespace GDSBG.MiABU.Attendance.Web.WFReporter
{
    public partial class OtherAllowanceForm :BasePage
    {
        KQMLeaveApplyForm_ZBLHBll leaveApply = new KQMLeaveApplyForm_ZBLHBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            //SetCalendar(txtStartDate, txtEndDate);
            if (!IsPostBack)
            {
                InitDropDownList();
            }
        }
        private void InitDropDownList()
        {
            DataTable dtStatus = leaveApply.getStatus();
            ddlStatus.DataSource = dtStatus;
            ddlStatus.DataTextField = "DataValue";
            ddlStatus.DataValueField = "DataCode";
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("", ""));
        }
        protected void btnQuery_Click(object sender, EventArgs e)
        {

        }
        protected void btnExport_Click(object sender, EventArgs e)
        {

        }
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            //DataBind();
        }
    }
}
