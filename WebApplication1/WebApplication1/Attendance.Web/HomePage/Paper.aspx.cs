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
using Infragistics.WebUI.UltraWebGrid;
using GDSBG.MiABU.Attendance.BLL.SystemManage.SystemData;

namespace GDSBG.MiABU.Attendance.Web.HomePage
{
    public partial class Paper : BasePage
    {

        DataTable paperDt = new DataTable();
        PaperBll paperBll = new PaperBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            SetCalendar(txtPaperDateEnd, txtPaperDateStart);
            if (!IsPostBack)
            {
                txtPaperDateEnd.Text = DateTime.Now.ToString("yyyy/MM/dd");
                pager.CurrentPageIndex = 1;
                PaperBind();
            }
        }

        /// <summary>
        /// 分頁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            PaperBind();
        }

        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            pager.CurrentPageIndex = 1;
           PaperBind();
        }
        /// <summary>
        /// 問卷調查數據綁定
        /// </summary>
        private void PaperBind()
        {
            int totalCount;
            paperDt = paperBll.GetPaperList(txtPaperTitle.Text, txtPaperDateStart.Text, txtPaperDateEnd.Text, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            pager.RecordCount = totalCount;
            UltraWebGridPaper.DataSource = paperDt;
            UltraWebGridPaper.DataBind();
        }

        #region UltraWebGrid_DataBound
        /// <summary>
        /// UltraWebGrid_DataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UltraWebGridPaper_DataBound(object sender, EventArgs e)
        {
            if (UltraWebGridPaper.Rows.Count > 0)
            {
                for (int loop = 0; loop < UltraWebGridPaper.Rows.Count; loop++)
                {
                    string urlStr = "<a style='text-decoration:none;' href=javascript:OpenDetail('Paper',";
                    urlStr += "'" + this.UltraWebGridPaper.Rows[loop].Cells.FromKey("Paper_Seq").Text + "')>" + this.UltraWebGridPaper.Rows[loop].Cells.FromKey("Paper_Title").Value + "</a>";

                    this.UltraWebGridPaper.Rows[loop].Cells.FromKey("Paper_Title").Value = urlStr;
                    Infragistics.WebUI.UltraWebGrid.TemplatedColumn tcol = (TemplatedColumn)UltraWebGridPaper.Bands[0].Columns[2];
                    CellItem GridItem = (CellItem)tcol.CellItems[loop];
                    Image image = (Image)(GridItem.FindControl("imgActiveFlag"));
                    if (paperDt.Rows[loop]["Active_Flag"].ToString() == "Y")
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
