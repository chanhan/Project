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
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemData;

namespace GDSBG.MiABU.Attendance.Web.HomePage
{
    public partial class Form : BasePage
    {
        FormManageBll formManageBll = new FormManageBll();
        //    FormTypeBll formTypeBll = new FormTypeBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["path"]))
            {
                string filePath = Server.MapPath("~/FormFiles/") + Request.QueryString["path"].ToString();
                PageHelper.ReturnHTTPStream(filePath, false);
            }
            if (!IsPostBack)
            {
                FormBind();
                FormTypeBind();
            }
        }
        /// <summary>
        /// 綁定表單
        /// </summary>
        private void FormBind()
        {
            int totalCount = 0;
            FormModel model = new FormModel();
            model.FormName = txtFormName.Text;
            if (!string.IsNullOrEmpty(ddlTypeId.SelectedValue))
            {
                model.TypeId = Convert.ToInt32(ddlTypeId.SelectedValue);
            }
            model.ActiveFlag = "Y";
            DataTable dt = formManageBll.GetOrderForm(model, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            pager.RecordCount = totalCount;
            UltraWebGridInfoForm.DataSource = dt;
            UltraWebGridInfoForm.DataBind();
        }

        #region 表單類型綁定
        /// <summary>
        /// 表單類型綁定
        /// </summary>
        private void FormTypeBind()
        {
            ddlTypeId.DataTextField = "TYPE_NAME";
            ddlTypeId.DataValueField = "TYPE_ID";

            DataTable dataTable = formManageBll.GetFormType();

            ddlTypeId.DataSource = dataTable;
            ddlTypeId.DataBind();
            ddlTypeId.Items.Insert(0, new ListItem("", ""));
        }
        #endregion

        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            pager.CurrentPageIndex = 1;
            FormBind();
        }

        ////protected void gvForm_RowCommand(object sender, GridViewCommandEventArgs e)
        ////{
        ////    if (e.CommandName == "downLoad")
        ////    {
        ////        string filePath = Server.MapPath("~/FormFiles/") + e.CommandArgument.ToString();
        ////        PageHelper.ReturnHTTPStream(filePath, false);
        ////    }
        ////}
        /// <summary>
        /// 分頁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            FormBind();
        }

        protected void UltraWebGridInfoForm_DataBound(object sender, EventArgs e)
        {
            for (int loop = 0; loop < UltraWebGridInfoForm.Rows.Count; loop++)
            {
                this.UltraWebGridInfoForm.Rows[loop].Cells.FromKey("Form_Path").Value = "<a style='text-decoration:none;' href=javascript:down('" + this.UltraWebGridInfoForm.Rows[loop].Cells.FromKey("Form_Path").Value + "')>" + this.UltraWebGridInfoForm.Rows[loop].Cells.FromKey("Form_Path").Value + "</a>";
             //   this.UltraWebGridInfoForm.Rows[loop].Cells.FromKey("Form_Path").Value = "<input type='image'  src='../../CSS/Images_new/gridviewEdit.gif' onclick='return Down(" + this.UltraWebGridInfoForm.Rows[loop].Cells.FromKey("Form_Path").Value + ");' style='border-width:0px;' />";
            }
        }
    }
}
