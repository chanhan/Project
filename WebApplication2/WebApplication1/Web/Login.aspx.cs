using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Maticsoft.Common;

namespace Maticsoft.Web
{
    public partial class Login : System.Web.UI.Page
    {
        string UserStr = ConfigurationManager.AppSettings["UserName"].ToString();
        string PwdStr = ConfigurationManager.AppSettings["Password"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string user = username.Text.Trim();
            string pwd = password.Text.Trim();
            List<string> userlist = UserStr.Split('/').ToList();
            List<string> pwdlist = PwdStr.Split('/').ToList();
            if (userlist.Contains(user))
            {
                int index = userlist.IndexOf(user);
                if (pwdlist[index] == pwd)
                {
                    Session["User"] = user;
                    Response.Redirect("EasyLid.aspx");
                }
                else
                {
                    MessageBox.Show(this,"用户名或密码错误");
                }
            }
            else
            {
                MessageBox.Show(this,"用户名或密码错误");
            }
            
        }
    }
}