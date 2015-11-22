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
using Infragistics.WebUI.UltraWebGrid;

namespace GDSBG.MiABU.Attendance.Web
{
    public partial class Index : BasePage
    {
        InfoNoticesBll infoNoticesBll = new InfoNoticesBll();
        FaqBll faqBll = new FaqBll();
        PaperBll paperBll = new PaperBll();
        ServiceBll serviceBll = new ServiceBll();
        DataTable paperDt = new DataTable();
        DataTable serviceDt = new DataTable();
        DataTable formDt = new DataTable();
        FormManageBll formManageBll = new FormManageBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["path"]))
            {
                string filePath = Server.MapPath("~/FormFiles/") + Request.QueryString["path"].ToString();
                PageHelper.ReturnHTTPStream(filePath, false);
            }
            if (!base.IsPostBack)
            {
                DataTable noticeDt = infoNoticesBll.GetTopNoticeList(5);
                UltraWebGridInfoNotice.DataSource = noticeDt;
                UltraWebGridInfoNotice.DataBind();

                DataTable problemDt = faqBll.GetTopFaqList(5);
                UltraWebGridProblem.DataSource = problemDt;
                UltraWebGridProblem.DataBind();

                formDt = formManageBll.GetTopPaperList(5);
                UltraWebGridInfoForm.DataSource = formDt;
                UltraWebGridInfoForm.DataBind();

                serviceDt = serviceBll.GetTopServiceList(5);
                UltraWebGridServiceHotline.DataSource = serviceDt;
                UltraWebGridServiceHotline.DataBind();
            }

        }



        #region UltraWebGrid_DataBound
        /// <summary>
        /// UltraWebGrid_DataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UltraWebGridInfoNotice_DataBound(object sender, EventArgs e)
        {
            if (UltraWebGridInfoNotice.Rows.Count > 0)
            {
                for (int loop = 0; loop < UltraWebGridInfoNotice.Rows.Count; loop++)
                {
                    string urlStr = "<a style='text-decoration:none;' href=javascript:OpenDetail('Notice',";
                    urlStr += "'" + this.UltraWebGridInfoNotice.Rows[loop].Cells.FromKey("NOTICE_ID").Text + "')>" + this.UltraWebGridInfoNotice.Rows[loop].Cells.FromKey("Notice_Title").Value + "</a>";

                    this.UltraWebGridInfoNotice.Rows[loop].Cells.FromKey("Notice_Title").Value = urlStr;
                }
            }

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
            if (UltraWebGridProblem.Rows.Count > 0)
            {
                for (int loop = 0; loop < UltraWebGridProblem.Rows.Count; loop++)
                {
                    string urlStr = "<a style='text-decoration:none;' href=javascript:OpenDetail('Faq',";
                    urlStr += "'" + this.UltraWebGridProblem.Rows[loop].Cells.FromKey("FAQ_SEQ").Text + "')>" + this.UltraWebGridProblem.Rows[loop].Cells.FromKey("FAQ_TITLE").Value + "</a>";

                    this.UltraWebGridProblem.Rows[loop].Cells.FromKey("FAQ_TITLE").Value = urlStr;

                }
            }

        }
        #endregion

        #region UltraWebGrid_DataBound
        /// <summary>
        /// UltraWebGrid_DataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UltraWebGridInfoForm_DataBound(object sender, EventArgs e)
        {

            for (int loop = 0; loop < UltraWebGridInfoForm.Rows.Count; loop++)
            {
                //     this.UltraWebGridInfoForm.Rows[loop].Cells.FromKey("FORM_SEQ").Value = "<span onclick='down(" + this.UltraWebGridInfoForm.Rows[loop].Cells.FromKey("Form_Path").Value + ")'><input type='image'  src='../../CSS/Images_new/DownLoad2.gif' style='border-width:0px;'  /></span>";
                       this.UltraWebGridInfoForm.Rows[loop].Cells.FromKey("Form_Path").Value = "<a style='text-decoration:none;' href=javascript:down('" + this.UltraWebGridInfoForm.Rows[loop].Cells.FromKey("Form_Path").Value + "')>" + this.UltraWebGridInfoForm.Rows[loop].Cells.FromKey("Form_Path").Value + "</a>";
          //      this.UltraWebGridInfoForm.Rows[loop].Cells.FromKey("Form_Path").Value = "<input type='image'  src='../../CSS/Images_new/DownLoad2.gif' onclick='return down(" + this.UltraWebGridInfoForm.Rows[loop].Cells.FromKey("Form_Path").Value + ");' style='border-width:0px;' />";
             //          this.UltraWebGridInfoForm.Rows[loop].Cells.FromKey("Form_Path").Value = "<a style='text-decoration:none;' href=javascript:down('" + this.UltraWebGridInfoForm.Rows[loop].Cells.FromKey("Form_Path").Value + "')>Down</a>";
             
            }
        }
        #endregion

        protected void UltraWebGridInfoForm_ItemCommand(object sender, UltraWebGridCommandEventArgs e)
        {
            if (e.InnerCommandEventArgs.CommandName.ToString() == "DownFrom")
            {
                string filePath = Server.MapPath("~/FormFiles/") + e.InnerCommandEventArgs.CommandArgument.ToString();
                PageHelper.ReturnHTTPStream(filePath, false);
            }
        }


        //protected void linkbtn_Command(object sender, CommandEventArgs e)
        //{
        //    if (e.CommandName == "DownFrom")
        //    {
        //        string filePath = Server.MapPath("~/FormFiles/") + e.CommandArgument.ToString();
        //        PageHelper.ReturnHTTPStream(filePath, false);
        //    }
        //}


    }
}
