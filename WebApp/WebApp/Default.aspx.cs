using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Thread t1 = new Thread(new ThreadStart(start));
            Thread t2 = new Thread(new ParameterizedThreadStart(start2));
        }
        private void start()
        {

        }
        private void start2(object i)
        {

        }
    }
}