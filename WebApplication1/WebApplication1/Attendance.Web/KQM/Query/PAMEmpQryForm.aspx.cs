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
using GDSBG.MiABU.Attendance.BLL.KQM.Query;

namespace GDSBG.MiABU.Attendance.Web.KQM.Query
{
    public partial class PAMEmpQryForm : BasePage
    {
        KQMSelfServiceBll kqmSelfServiceBll = new KQMSelfServiceBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string workNo = Request.QueryString["WorkNo"].ToString();
                string year = Request.QueryString["Year"].ToString();

                DataTable dataTableKaoQinData = kqmSelfServiceBll.getKaoQinData(workNo, year);
                WebGridKaoQin.DataSource = dataTableKaoQinData;
                WebGridKaoQin.DataBind();

                DataTable dataTableJiangChengData = kqmSelfServiceBll.getdataTableJiangChengData(workNo, year);
                WebGridJiangCheng.DataSource = dataTableJiangChengData;
                WebGridJiangCheng.DataBind();


                DataTable dataTableIEData = kqmSelfServiceBll.getdataTableIEData(workNo, year);
                WebGridIE.DataSource = dataTableIEData;
                WebGridIE.DataBind();

                 DataTable dataTableStudyData = kqmSelfServiceBll.getdataTableStudyData(workNo, year);
                 WebGridStudy.DataSource = dataTableStudyData;
                 WebGridStudy.DataBind();
                


            }
        }
    }
}
