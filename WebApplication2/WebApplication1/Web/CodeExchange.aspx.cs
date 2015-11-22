using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.BLL;

namespace Maticsoft.Web
{
    public partial class CodeExchange : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ShowByPhoneNumber();
        }
        private void ShowByPhoneNumber()
        {
            string phonenumber = phone.Text.Trim();
            DataTable dt = new ReciveMsgBLL().getCodeExchangeByPhoneNumber(phonenumber);
            if (dt != null&&dt.Rows.Count>0)
            {
                gvCode.DataSource = dt;
                gvCode.DataBind();
                labphone.Text = "";
            }
            else
            {
                labphone.Text = "手机号不存在";
            }


        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            ShowByCode();
        }
        private void ShowByCode()
        {
            string codestr = code.Text.Trim();
            DataTable dt = new ReciveMsgBLL().getCodeExchangeByCode(codestr);
            if (dt != null&&dt.Rows.Count>0)
            {
                gvCode.DataSource = dt;
                gvCode.DataBind();
                labcode.Text = "";
            }
            else
            {
                labcode.Text = "兑换码不存在";
            }


        }
    }
}