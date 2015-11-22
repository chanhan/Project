using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using GDSBG.MiABU.Attendance.BLL.WorkFlow;
using System.Data;
using System.Configuration;
using System.Web.SessionState;
using Resources;

namespace GDSBG.MiABU.Attendance.Web.WorkFlowForm.AjaxProcess
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

    public class AbnormalAttendanceHandle : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string WorkNo = context.Request.QueryString["WorkNo"];
            string KQDate = context.Request.QueryString["KQDate"];
            string ShiftNo = context.Request.QueryString["ShiftNo"];
            string sResult = "";

            Bll_AbnormalAttendanceHandle bll_abnormal = new Bll_AbnormalAttendanceHandle();
            DataTable dt_Card = new DataTable();
            
            dt_Card = bll_abnormal.GetBellCardData(WorkNo, KQDate, ShiftNo);
            sResult = "<table cellspacing=0 cellpadding=0 width=100%>";
            sResult += "<tr>";
            sResult += "<td class=td_label align=center height=25 width=20>No</td>";
            sResult += "<td class=td_label height=25 width=60>" + Message.WorkNo + "</td>";
            sResult += "<td class=td_label height=25 width=70>" + Message.LocalName + "</td>";
            sResult += "<td class=td_label height=25 width=140>" + Message.CardTime + "</td>";
            sResult += "<td class=td_label height=25 width=90>" + Message.BellNo + "</td>";
            sResult += "<td class=td_label height=25 width=140>" + Message.ReadTime + "</td>";
            sResult += "<td class=td_label height=25 width=80>" + Message.CardNo + "</td>";
            sResult += "</tr>";

            if (dt_Card != null && dt_Card.Rows.Count > 0)
            {
                for (int i = 0; i <dt_Card.Rows.Count; i++)
                {
                    string temp_WorkNo = dt_Card.Rows[i]["WORKNO"].ToString();
                    string temp_LocalName = dt_Card.Rows[i]["LOCALNAME"].ToString();
                    string temp_CardTime = dt_Card.Rows[i]["CARDTIME"].ToString();
                    string temp_BellNo = dt_Card.Rows[i]["BELLNO"].ToString();
                    string temp_ReadTime = dt_Card.Rows[i]["READTIME"].ToString();
                    string temp_CardNo = dt_Card.Rows[i]["CARDNO"].ToString();

                    sResult += "<tr>";
                    sResult += "<td class=td_label align=center height=25>" + (i+1).ToString() + "</td>";
                    sResult += "<td class=td_label height=25>" + temp_WorkNo + "</td>";
                    sResult += "<td class=td_label height=25>" + temp_LocalName + "</td>";
                    sResult += "<td class=td_label height=25>" + temp_CardTime + "</td>";
                    sResult += "<td class=td_label height=25>" + temp_BellNo + "</td>";
                    sResult += "<td class=td_label height=25>" + temp_ReadTime + "</td>";
                    sResult += "<td class=td_label height=25>" + temp_CardNo + "</td>";
                    sResult += "</tr>";
                }

            }
            sResult += "</table>";
            context.Response.Write(sResult);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
