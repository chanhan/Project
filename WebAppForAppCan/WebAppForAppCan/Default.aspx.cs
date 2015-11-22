using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppForAppCan
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod == "GET")
            {
                string reqid = Request.QueryString["reqId"].ToString();
                string userid = Request.QueryString["userId"].ToString();
                Response.Write(reqid + "   " + userid);
            }
            else if (Request.HttpMethod == "POST")
            {
                string reqid = Request.Form["reqId"].ToString();
                string userid = Request.Form["userId"].ToString();
                Response.Write(reqid + "   " + userid);
            }
            Response.End();
        }
    }
}