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
using Infragistics.WebUI.WebDataInput;


namespace GDSBG.MiABU.Attendance.Web.ControlLib
{
    public partial class PageNavigator : System.Web.UI.UserControl
    {
        protected ImageButton ImageButtonGoto;
        protected ImageButton ImageButtonNext;
        protected ImageButton ImageButtonPrevious;
        protected Label LabelTotalpage;
        protected Label LabelTotalrecords;
        protected WebNumericEdit WebNumericEditCurrentpage;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.imgbtnPrevious.Attributes.Add("onclick", "return PreviousNavigateClick(\"" + this.ClientID + "\");");
            this.imgbtnNext.Attributes.Add("onclick", "return NextNavigateClick(\"" + this.ClientID + "\");");
            this.imgbtnGoto.Attributes.Add("onclick", "return GotoNavigateClick(\"" + this.ClientID + "\");");

            
        }

       

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