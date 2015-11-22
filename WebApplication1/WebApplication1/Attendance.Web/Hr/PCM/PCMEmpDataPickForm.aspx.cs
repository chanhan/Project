using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using GDSBG.MiABU.Attendance.BLL.Hr.PCM;

namespace GDSBG.MiABU.Attendance.Web.Hr.PCM
{
    public partial class PCMEmpDataPickForm : BasePage
    {
        PCMLeaveApplyBll pCMLeaveApply = new PCMLeaveApplyBll();

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnQuery_Click(object sender, EventArgs e)
        {
           DataTable dt= pCMLeaveApply.getAuditData(txtWorkNo.Text.Trim(), txtLocalName.Text.Trim());
            this.UltraWebGridAudit.DataSource = dt;
            this.UltraWebGridAudit.DataBind();
        }
    }
}
