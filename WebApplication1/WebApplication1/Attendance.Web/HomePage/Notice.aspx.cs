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
using System.Collections.Generic;

namespace GDSBG.MiABU.Attendance.Web.HomePage
{
    public partial class Notice : BasePage
    {
        InfoNoticesBll infoNoticesBll = new InfoNoticesBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                pager.CurrentPageIndex = 1;
                NoticeTypeBind();
                NoticeBind();
            }
            SetCalendar(txtNoticeDateEnd, txtNoticeDateStart);

        }

        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            pager.CurrentPageIndex = 1;
            NoticeBind();
        }

        #region 公告類型綁定
        /// <summary>
        /// 公告類型綁定
        /// </summary>
        private void NoticeTypeBind()
        {
            ddlNoticeTypeId.DataTextField = "NOTICE_TYPE_NAME";
            ddlNoticeTypeId.DataValueField = "NOTICE_TYPE_ID";

            DataTable dataTable = infoNoticesBll.GetNoticeType();

            ddlNoticeTypeId.DataSource = dataTable;
            ddlNoticeTypeId.DataBind();
            ddlNoticeTypeId.Items.Insert(0, new ListItem("", ""));
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
            NoticeBind();
        }
        #endregion


        /// <summary>
        /// 公告Model綁定
        /// </summary>
        private void NoticeBind()
        {
            int totalCount;
            string noticeTitle = txtNoticeTitle.Text.Trim();
            string startDate = txtNoticeDateStart.Text.Trim();
            string endDate = txtNoticeDateEnd.Text.Trim();
            string noticeType = ddlNoticeTypeId.SelectedValue;
            DataTable dt = infoNoticesBll.GetNoticeList(noticeTitle, noticeType, startDate, endDate, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            pager.RecordCount = totalCount;
            UltraWebGridInfoNotice.DataSource = dt;
            UltraWebGridInfoNotice.DataBind();
        }


        #region UltraWebGrid_DataBound
        /// <summary>
        /// UltraWebGrid_DataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UltraWebGridInfoNotice_DataBound(object sender, EventArgs e)
        {
            for (int loop = 0; loop < UltraWebGridInfoNotice.Rows.Count; loop++)
            {
                string urlStr = "<a style='text-decoration:none;' href=javascript:OpenDetail('Notice',";
                urlStr += "'" + this.UltraWebGridInfoNotice.Rows[loop].Cells.FromKey("NOTICE_ID").Text + "')>" + this.UltraWebGridInfoNotice.Rows[loop].Cells.FromKey("Notice_Title").Value + "</a>";

                this.UltraWebGridInfoNotice.Rows[loop].Cells.FromKey("Notice_Title").Value = urlStr;
            }
        }
        #endregion
    }
}
