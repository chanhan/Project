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
using Resources;
using GDSBG.MiABU.ESS.Model.SystemManage.Interaction;
using Infragistics.WebUI.UltraWebGrid;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.Web.HomePage
{
    public partial class Faq : BasePage
    {
        FaqBll faqBll = new FaqBll();
        DataTable dataTable = new DataTable();
        static SynclogModel logmodel = new SynclogModel();
        protected void Page_Load(object sender, EventArgs e)
        {

            SetCalendar(txtSelFaqDateEnd, txtSelFaqDateStart);
            if (!IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;

                pager.CurrentPageIndex = 1;
                FaqTypeBind();
                InitialValue();
                FaqBind();
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
            logmodel.ProcessFlag = "insert";
            FaqModel model = PageHelper.GetModel<FaqModel>(pnlAddNew.Controls);
            model.CreateDate = DateTime.Now;
            model.CreateUser = CurrentUserInfo.CreateUser;
            lblMessage.Text = faqBll.AddFaq(model, logmodel) ? Message.SaveSuccess : Message.SaveFailed;
            PageHelper.CleanControlsValue(pnlAddNew.Controls);
            FaqBind();
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
        #region 分頁
        /// <summary>
        /// 分頁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            FaqTypeBind();
        }
        #endregion

        #region 設置默認值
        /// <summary>
        /// 設置默認值
        /// </summary>
        private void InitialValue()
        {
            txtFaqDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            txtSelFaqDateEnd.Text = DateTime.Now.ToString("yyyy/MM/dd");
            txtEmpPhone.Text = CurrentUserInfo.Tel;
            txtEmpNo.Text = CurrentUserInfo.Personcode;
            txtEmpName.Text = CurrentUserInfo.Cname;
            txtEmpEmail.Text = CurrentUserInfo.Mail;
        }
        #endregion

        #region 常見問題類型綁定
        /// <summary>
        /// 公告類型綁定
        /// </summary>
        private void FaqTypeBind()
        {
            ddlFaqTypeId.DataTextField = "FAQ_TYPE_NAME";
            ddlFaqTypeId.DataValueField = "FAQ_TYPE_ID";
            ddlSelFaqType.DataTextField = "FAQ_TYPE_NAME";
            ddlSelFaqType.DataValueField = "FAQ_TYPE_ID";


            DataTable dt = faqBll.GetFaqType();

            ddlFaqTypeId.DataSource = dt;
            ddlFaqTypeId.DataBind();
            ddlFaqTypeId.Items.Insert(0, new ListItem("", ""));
            ddlSelFaqType.DataSource = dt;
            ddlSelFaqType.DataBind();
            ddlSelFaqType.Items.Insert(0, new ListItem("", ""));
        }
        #endregion

        #region 數據綁定
        /// <summary>
        /// 數據綁定
        /// </summary>
        private void FaqBind()
        {
            int totalCount = 0;

            string faqTitle = txtSelFaqTitle.Text;
            string faqTypeId = "";
            string startDate = txtSelFaqDateStart.Text;
            string endDate = txtSelFaqDateEnd.Text;
            if (!string.IsNullOrEmpty(ddlSelFaqType.SelectedValue))
            {
                faqTypeId = ddlSelFaqType.SelectedValue;
            }
            dataTable = faqBll.GetFaqList(faqTitle, faqTypeId, startDate, endDate, chkOnlyFamiliar.Checked, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            pager.RecordCount = totalCount;
            UltraWebGridProblem.DataSource = dataTable;
            UltraWebGridProblem.DataBind();
        }
        #endregion


        #region UltraWebGrid_DataBound
        /// <summary>
        /// UltraWebGrid_DataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UltraWebGridProblem_DataBound(object sender, EventArgs e)
        {
            if (dataTable.Rows.Count > 0)
            {
                Infragistics.WebUI.UltraWebGrid.TemplatedColumn tcol = (TemplatedColumn)UltraWebGridProblem.Bands[0].Columns[2];
                for (int loop = 0; loop < UltraWebGridProblem.Rows.Count; loop++)
                {

                    string urlStr = "<a style='text-decoration:none;' href=javascript:OpenDetail('Faq',";
                    urlStr += "'" + this.UltraWebGridProblem.Rows[loop].Cells.FromKey("FAQ_SEQ").Text + "')>" + this.UltraWebGridProblem.Rows[loop].Cells.FromKey("FAQ_TITLE").Value + "</a>";

                    this.UltraWebGridProblem.Rows[loop].Cells.FromKey("FAQ_TITLE").Value = urlStr;

                    CellItem GridItem = (CellItem)tcol.CellItems[loop];
                    Image image = (Image)(GridItem.FindControl("imgActiveFlag"));
                    if (dataTable.Rows[loop]["answer_flag"].ToString() == "Y")
                    {
                        image.ImageUrl = "../CSS/Images_new/gou.gif";
                    }
                    else
                    {
                        image.ImageUrl = "../CSS/Images_new/cha.gif";
                    }
                }

            }




        }
        #endregion
    }
}
