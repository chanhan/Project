using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using GDSBG.MiABU.Attendance.BLL.WorkFlow;
using System.Web.SessionState;

namespace GDSBG.MiABU.Attendance.Web.WorkFlowForm.AjaxProcess
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetOTType : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string OTDate = context.Request.QueryString["OTDate"];
            string WorkNo = context.Request.QueryString["WorkNo"];
            string result = FindOTType(OTDate, WorkNo);
            context.Response.Write(result);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public string FindOTType(string OTDate, string WorkNo)
        {
            OverTimeBll bll = new OverTimeBll();
            return bll.GetOTType(WorkNo, OTDate);
        }
    }
}
