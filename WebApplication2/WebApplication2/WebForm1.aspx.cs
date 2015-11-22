using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
   
    public partial class WebForm1 : System.Web.UI.Page
    {
        SendMail sendMail = new SendMail("799082195@qq.com", "chen_han2008@sina.com", "测试内容", "测试标题", "1988a01b02c");
        protected void Page_Load(object sender, EventArgs e)
        {
            sendMail.Attachments("D:\\cat.png,D:\\Rose.html");
            bool b=  sendMail.Send();
        }
    }
}