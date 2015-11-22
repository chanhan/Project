using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using GDSBG.MiABU.Attendance.BLL.SystemManage.SystemData;
using GDSBG.MiABU.ESS.Model.SystemManage.Interaction;

namespace GDSBG.MiABU.Attendance.Web.HomePage
{
    public partial class FaqDetail : BasePage
    {
        FaqBll faqBll = new FaqBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            FaqControlBind();
        }

        /// <summary>
        /// 綁定控件
        /// </summary>
        private void FaqControlBind()
        {

            if (!string.IsNullOrEmpty(Request.QueryString["faqSeq"]))
            {
                string faqSeq = Request.QueryString["faqSeq"];
                FaqModel model = faqBll.GetFaqByKey(faqSeq);
                litAnswerContent.Text = model.AnswerContent;
                if (model.AnswerDate != null)
                    litAnswerDate.Text = model.AnswerDate.Value.ToString("yyyy/MM/dd");
                litAnswerName.Text = model.AnswerName;
                litEmpEmail.Text = model.EmpEmail;
                litEmpName.Text = model.EmpName;
                litEmpNo.Text = model.EmpNo;
                litEmpPhone.Text = model.EmpPhone;
                litFaqContent.Text = model.FaqContent;
                litFaqDate.Text = model.FaqDate.Value.ToString("yyyy/MM/dd");
                litFaqTitle.Text = model.FaqTitle;
                litFaqTypeName.Text = model.FaqTypeName;
            }
        }
    }
}
