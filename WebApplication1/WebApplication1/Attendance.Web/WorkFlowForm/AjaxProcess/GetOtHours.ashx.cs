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
    public class GetOtHours : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string result = string.Empty;
            string WorkNo = context.Request.QueryString["WorkNo"];
            string OTDate = context.Request.QueryString["OTDate"];
            string BeginTime = context.Request.QueryString["BeginTime"];
            string EndTime = context.Request.QueryString["EndTime"];
            string OTType = context.Request.QueryString["OTType"];

            result = GetOTHours(WorkNo, OTDate, BeginTime, EndTime, OTType);

            context.Response.Write(result);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public string GetOTHours(string WorkNo, string OTDate, string BeginTime, string EndTime, string OTType)
        {
            double hours = 0;
            string LHZBIsDisplayG2G3 = "";
            if (OTType.Length == 0)
            {
                OTType = FindOTType(OTDate, WorkNo);
                #region 龍華總部周邊HR。G2，G3名稱更改為G2(休息日上班)，G3(法定假日上班)
                try
                {
                    LHZBIsDisplayG2G3 = System.Configuration.ConfigurationManager.AppSettings["LHZBIsDisplayG2G3"];
                }
                catch
                {
                    LHZBIsDisplayG2G3 = "N";
                }
                if (LHZBIsDisplayG2G3 == "Y")
                {
                    OTType = OTType.Substring(0, 2);
                }
                #endregion

            }
            OverTimeBll bll = new OverTimeBll();
            if (BeginTime != EndTime)
            {
                hours = bll.GetOtHours(WorkNo.ToUpper(), OTDate, BeginTime, EndTime, OTType);
            }
            return hours.ToString();
        }

        public string FindOTType(string OTDate, string WorkNo)
        {
            OverTimeBll bll = new OverTimeBll();
            string LHZBIsDisplayG2G3 = "";
            string OtStatus = "";
            OtStatus = bll.GetOTType(WorkNo, OTDate);
            #region 龍華總部周邊HR。G2，G3名稱更改為G2(休息日上班)，G3(法定假日上班)
            try
            {
                LHZBIsDisplayG2G3 = System.Configuration.ConfigurationManager.AppSettings["LHZBIsDisplayG2G3"];
            }
            catch
            {
                LHZBIsDisplayG2G3 = "N";
            }

            if (LHZBIsDisplayG2G3 == "Y")
            {
                if (OtStatus == "G2")
                {
                    return OtStatus + "(休息日上班)";
                }
                else if (OtStatus == "G3")
                {
                    return OtStatus + "(法定假日上班)";
                }
                else
                {
                    return OtStatus;
                }
            }
            else
            {
                return OtStatus;
            }
            #endregion


        }
    }
}
