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
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemData;
using GDSBG.MiABU.Attendance.BLL.SystemManage.SystemData;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Text;
using Resources;

namespace GDSBG.MiABU.Attendance.Web.HomePage
{
    public partial class PaperDetail : BasePage
    {
        protected override bool AccessPermissionRequired { get { return false; } }

        List<QuestionModel> questionList;
        List<AnswerModel> answerList;
        List<EmpAnswerModel> empAnswerList;
        QuestionBll questionBll = new QuestionBll();
        AnswerBll answerBll = new AnswerBll();
        EmpAnswerBll empAnswerBll = new EmpAnswerBll();
        EmpPaperBll empPaperBll = new EmpPaperBll();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage;
        int chklstIdx = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hidPaperSeq.Value = Request.QueryString["PaperSeq"];
                PaperBind();
                PaperAnswerStatus();
            }
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("SaveSuccess", Message.SaveSuccess);
                ClientMessage.Add("SaveFailed", Message.SaveFailed);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
        }


        /// <summary>
        /// 獲取題目及選項數據
        /// </summary>
        private void PaperBind()
        {
            //查詢問題集
            QuestionModel modelQuestion = new QuestionModel();
            modelQuestion.PaperSeq = Convert.ToInt32(Request.QueryString["PaperSeq"]);
            questionList = questionBll.GetOrderQuestion(modelQuestion);
            if (questionList.Count > 0)
            {
                litPaperTitle.Text = questionList[0].PaperTitle;
            }
            else
                return;
            //查詢選項集
            AnswerModel modelAnswer = new AnswerModel();
            modelAnswer.QuestionActive = "Y";
            modelAnswer.PaperSeq = Convert.ToInt32(Request.QueryString["PaperSeq"]);
            answerList = answerBll.GetOrderAnswer(modelAnswer);

            //查詢答案集
            empAnswerList = empAnswerBll.GetPaperAnswer(Convert.ToInt32(Request.QueryString["PaperSeq"]));

            //加載數據
            LoadQuestion();
        }

        /// <summary>
        /// 獲取問卷答題狀態
        /// </summary>
        private void PaperAnswerStatus()
        {
            List<EmpPaperModel> list = empPaperBll.GetPaperAnswer(Convert.ToInt32(Request.QueryString["PaperSeq"]), CurrentUserInfo.Personcode);
            if (list.Count >= 1)
            {
                if (list[0].EpStatus == "Submit")
                {
                    btnSubmit.Enabled = false;
                    btnTempSave.Enabled = false;
                   lblMessage.Text = Message.SubmitPaper;
                }
            }
        }

        /// <summary>
        /// 加載問題
        /// </summary>
        private void LoadQuestion()
        {
            int nQ = questionList.Count;

            for (int q = 0; q < nQ; q++)
            {
                TableHeaderRow trQuestion = new TableHeaderRow();
                TableHeaderCell tcQuestion = new TableHeaderCell();
                tcQuestion.HorizontalAlign = HorizontalAlign.Left;
                Literal lit = new Literal();
                lit.Text = (q + 1).ToString() + ". " + questionList[q].QuestionName;
                tcQuestion.Controls.Add(lit);
                trQuestion.Cells.Add(tcQuestion);

                TableRow trAnswer = new TableRow();
                TableCell tcAnswer = LoadAnswer(questionList[q].QuestionSeq.Value, questionList[q].IsMulti);
                trAnswer.Cells.Add(tcAnswer);
                tabContent.Rows.Add(trQuestion);
                tabContent.Rows.Add(trAnswer);
            }


        }

        /// <summary>
        /// 加載選項
        /// </summary>
        /// <returns></returns>
        private TableCell LoadAnswer(int questionSeq, string isMulti)
        {
            int nA = answerList.Count;
            TableCell tcAnswer = new TableCell();
            tcAnswer.HorizontalAlign = HorizontalAlign.Left;
            if (isMulti == "Y")
            {
                EssCheckBoxList chklst = new EssCheckBoxList();
                chklst.ID = "chklst" + chklstIdx;
                chklstIdx++;
                chklst.DataTextField = "AnswerContent";
                chklst.DataValueField = "AnswerSeq";
                chklst.DataSource = answerList.FindAll(model => { return model.QuestionSeq == questionSeq; });
                chklst.DataBind();
                foreach (ListItem item in chklst.Items)
                {
                    item.Selected = empAnswerList.Find(model => { return model.AnswerSeq.Value.ToString() == item.Value; }) != null;
                }
                tcAnswer.Controls.Add(chklst);
            }
            else
            {
                RadioButtonList rdolst = new RadioButtonList();
                rdolst.DataTextField = "AnswerContent";
                rdolst.DataValueField = "AnswerSeq";
                rdolst.DataSource = answerList.FindAll(model => { return model.QuestionSeq == questionSeq; });
                rdolst.DataBind();
                foreach (ListItem item in rdolst.Items)
                {
                    item.Selected = empAnswerList.Find(model => { return model.AnswerSeq.Value.ToString() == item.Value; }) != null;
                    if (item.Selected) { break; }
                }
                tcAnswer.Controls.Add(rdolst);
            }
            return tcAnswer;
        }


        /// <summary>
        /// 保存答題內容
        /// </summary>
        protected override void AjaxProcess()
        {
            string answerContent = "";
            string status = "";
            int nSave = -1;
            int paperId = 0;
            if (!string.IsNullOrEmpty(Request.Form["answerContent"]))
            {
                answerContent = Request.Form["answerContent"];
                status = Request.Form["answerStatus"];
                paperId = Convert.ToInt32(Request.Form["paperId"]);
                if (!string.IsNullOrEmpty(answerContent))
                {
                    answerContent = answerContent.Trim();
                    answerContent = answerContent.TrimEnd('§');
                }
                nSave = empAnswerBll.SavePaperAnswer(CurrentUserInfo.Personcode, paperId, status, answerContent, CurrentUserInfo.CreateUser);
                Response.Clear();
                Response.Write(nSave.ToString());
                Response.End();
            }
        }
        private class EssCheckBoxList : CheckBoxList
        {
            public override void RenderControl(HtmlTextWriter writer)
            {
                if (Items.Count > 0)
                {
                    StringBuilder txt = new StringBuilder("<table>");
                    for (int i = 0; i < Items.Count; i++)
                    {
                        txt.AppendFormat("<tr><td><input id=\"{0}\" type=\"checkbox\" name=\"{1}\" value=\"{2}\" {3} /><label for=\"{0}\">{4}</label></td></tr>",
                            ID + "_" + i.ToString(), ID + "$" + i.ToString(), Items[i].Value, Items[i].Selected ? "checked=\"checked\"" : "", Items[i].Text);
                    }
                    txt.Append("</table>");
                    writer.Write(txt.ToString());
                }
            }
        }
    }
}
