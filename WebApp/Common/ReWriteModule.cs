using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    public class ReWriteModule:IHttpModule
    {

        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
        }

        private void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpContext context = application.Context;
            string url = context.Request.Url.PathAndQuery;
            if (url.IndexOf("?") <= 0) return;
              NameValueCollection query = context.Request.QueryString;
            string pageName = query["u"];
            string paremeter = query["p"];
            List<string> pa = new List<string>();
            if (!string.IsNullOrEmpty(paremeter))
            {
                paremeter.Split('-').ToList().ForEach(p=>pa.Add(string.Join("=",p.Split('|'))));
            }
            foreach (var item in query.AllKeys)
            {
                
            }
            string newUrl = "~/" + pageName + ".aspx?" + string.Join("&", pa);
            context.RewritePath(newUrl);
        }
    }
}
