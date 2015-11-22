using System;
//using ASP;
using Resources;
using System.Globalization;
using System.Resources;
using System.Web.Profile;
using System.Web.UI;
using Infragistics.WebUI.WebSchedule;

namespace GDSBG.MiABU.Attendance.Web.ControlLib
{
    public partial class BaseControl : System.Web.UI.UserControl
    {
        protected void CheckSession()
        {
            //try
            //{
            //    if ((base.Session["appUser"] == null) || (base.Session["appUser"].ToString().Trim() == ""))
            //    {
            //        string sAppPath;
            //        if (base.Request.ApplicationPath.Equals("/"))
            //        {
            //            sAppPath = string.Empty;
            //        }
            //        else
            //        {
            //            sAppPath = base.Request.ApplicationPath;
            //        }
            //        string location = sAppPath + "/LogoutForm.aspx";
            //        base.Response.Write("<SCRIPT LANGUAGE='JavaScript'>self.top.location.href=' " + location + "';</SCRIPT>");
            //        base.Response.End();
            //    }
            //}
            //catch (Exception)
            //{               
            //}
        }

        //protected string GetResouseValue(string key)
        //{
        //    try
        //    {
        //        string resourceValue = "";
        //        CultureInfo ci = CultureInfo.CurrentCulture;
        //        resourceValue = new ResourceManager(typeof(Resource)).GetString(key, ci);
        //        if (string.IsNullOrEmpty(resourceValue))
        //        {
        //            resourceValue = "Undefine";
        //        }
        //        return resourceValue;
        //    }
        //    catch (Exception)
        //    {
        //        return "Undefine";
        //    }
        //}

        //protected void Page_Init(object sender, EventArgs e)
        //{
        //}

        //protected void Page_Load(object sender, EventArgs e)
        //{

        //}
        //protected global_asax ApplicationInstance
        //{
        //    get
        //    {
        //        return (global_asax)this.Context.ApplicationInstance;
        //    }
        //}

        //protected DefaultProfile Profile
        //{
        //    get
        //    {
        //        return (DefaultProfile)this.Context.Profile;
        //    }
        //}
    }
}