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
using System.Web.Script.Serialization;
using GDSBG.MiABU.ESS.Model.SystemManage.Interaction;
using Resources;
using Infragistics.WebUI.UltraWebGrid;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.Web.SystemManage.SystemData
{
    public partial class FaqManage : BasePage
    {
        FaqBll faqBll = new FaqBll();
        DataTable dt = new DataTable();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        static SynclogModel logmodel = new SynclogModel();
        bool control;
        protected void Page_Load(object sender, EventArgs e)
        {
            SetCalendar(txtSelFaqDateEnd, txtSelFaqDateStart);
          PageHelper.ButtonControls(FuncList, pnlShowPanel.Controls, base.FuncListModule);
          PageHelper.ButtonControls(FuncList, pnlAddPanel.Controls, base.FuncListModule);
            control = upDateControl("Modify");
            if (!IsPostBack)
            {

                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;

                txtAnswerDate.Attributes["readonly"] = "readonly";
                FaqTypeBind();
                FaqBind();
            }
        }
        /// <summary>
        /// 判斷UltraWebGrid中是否顯示編輯圖片
        /// </summary>
        /// <param name="fun">功能</param>
        /// <returns>是否可見</returns>
        private bool upDateControl(string fun)
        {
            string[] strFunListModule = FuncListModule.Split(',');
            string funcList = FuncList;
            if (strFunListModule.Contains(fun))
            {
                if (funcList != null)
                {
                    string[] strFunList = funcList.Split(',');
                    if (strFunList.Contains(fun))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return true;
            }
        }
        #region UltraWebGrid_DataBound
        /// <summary>
        /// UltraWebGrid_DataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UltraWebGridProblem_DataBound(object sender, EventArgs e)
        {
            Infragistics.WebUI.UltraWebGrid.TemplatedColumn tcol = (TemplatedColumn)UltraWebGridProblem.Bands[0].Columns[4];



            if (UltraWebGridProblem.Rows.Count > 0)
            {
                for (int loop = 0; loop < UltraWebGridProblem.Rows.Count; loop++)
                {
                    string urlStr = "<a style='text-decoration:none;' href=javascript:OpenDetail('Faq',";
                    urlStr += "'" + this.UltraWebGridProblem.Rows[loop].Cells.FromKey("FAQ_SEQ").Text + "')>" + this.UltraWebGridProblem.Rows[loop].Cells.FromKey("FAQ_TITLE").Value + "</a>";

                    this.UltraWebGridProblem.Rows[loop].Cells.FromKey("FAQ_TITLE").Value = urlStr;
                    this.UltraWebGridProblem.Rows[loop].Cells.FromKey("FAQ_SEQ").Value = "<input type='image'  src='../../CSS/Images_new/gridviewEdit.gif' onclick='return SelectFaq(" + this.UltraWebGridProblem.Rows[loop].Cells.FromKey("FAQ_SEQ").Value + ");' style='border-width:0px;' />";
                    CellItem GridItem = (CellItem)tcol.CellItems[loop];
                    Image image = (Image)(GridItem.FindControl("imgAnswerFlag"));
                    if (dt.Rows[loop]["Answer_Flag"].ToString() == "Y")
                    {
                        image.ImageUrl = "../../CSS/Images_new/gou.gif";
                    }
                    else
                    {
                        image.ImageUrl = "../../CSS/Images_new/cha.gif";
                    }

                }
                if (!control)
                {
                 UltraWebGridProblem.Columns[5].Hidden = true;
                }
            }


        }
        #endregion

        #region 查詢
        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            FaqBind();
        }
        #endregion

        /// <summary>
        /// Ajax請求獲得功能Model Json
        /// </summary>
        protected override void AjaxProcess()
        {

            if (!string.IsNullOrEmpty(Request.Form["FaqSeq"]))
            {
                FaqModel model = faqBll.GetFaqByKey(Request.Form["FaqSeq"]);
                string faqJson = null;
                if (model != null)
                {
                    model.AnswerDate = DateTime.Now.Date;
                    model.AnswerEmail = CurrentUserInfo.Mail;
                    model.AnswerName = CurrentUserInfo.Cname;
                    faqJson = JsSerializer.Serialize(model);
                }
                Response.Clear();
                Response.Write(faqJson);
                Response.End();
            }
        }

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "update";
            FaqModel model = PageHelper.GetModel<FaqModel>(pnlUpd.Controls);
            model.UpdateDate = DateTime.Now;
            model.UpdateUser = CurrentUserInfo.CreateUser;
            model.AnswerFlag = "Y";
            lblMessage.Text = faqBll.UpdateFaq(model, logmodel) ? Message.UpdateSuccess : Message.UpdateFailed;
            PageHelper.CleanControlsValue(pnlUpd.Controls);
            FaqBind();
        }
        #endregion

        #region 分頁
        /// <summary>
        /// 分頁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            FaqBind();
        }
        #endregion

        #region 數據綁定
        /// <summary>
        /// 數據綁定
        /// </summary>
        private void FaqBind()
        {
            int totalCount = 0;
            pager.CurrentPageIndex = 1;
            string faqTitle = txtSelFaqTitle.Text;
            string faqTypeId = "";
            string startDate = txtSelFaqDateStart.Text;
            string endDate = txtSelFaqDateEnd.Text;
            if (!string.IsNullOrEmpty(ddlSelFaqType.SelectedValue))
            {
                faqTypeId = ddlSelFaqType.SelectedValue;
            }

            dt = faqBll.GetFaqList(faqTitle, faqTypeId, startDate, endDate, false, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            pager.RecordCount = totalCount;
            UltraWebGridProblem.DataSource = dt;
            UltraWebGridProblem.DataBind();
        }
        #endregion

        #region 常見問題類型綁定
        /// <summary>
        /// 公告類型綁定
        /// </summary>
        private void FaqTypeBind()
        {
            ddlSelFaqType.DataTextField = "FAQ_TYPE_NAME";
            ddlSelFaqType.DataValueField = "FAQ_TYPE_ID";
            DataTable dt = faqBll.GetFaqType();
            ddlSelFaqType.DataSource = dt;
            ddlSelFaqType.DataBind();
            ddlSelFaqType.Items.Insert(0, new ListItem("", ""));
        }
        #endregion
    }
}
