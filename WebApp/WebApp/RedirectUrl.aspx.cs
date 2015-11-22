using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{
    public partial class RedirectUrl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = Request.QueryString["value1"];
            Label2.Text = Request.QueryString["value2"];
            Label3.Text = Request.Form["postValue1"];
        }
    }
}