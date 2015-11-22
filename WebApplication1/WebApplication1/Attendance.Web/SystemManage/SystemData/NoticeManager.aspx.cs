/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： NoticeManager.cs
 * 檔功能描述： 系統公告發佈UI類
 * 
 * 版本：1.0
 * 創建標識： 陳函 2012.01.03
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.SystemManage.SystemData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemData;
using Infragistics.WebUI.UltraWebGrid;
using Resources;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.Web.SystemManage.SystemData
{
    public partial class NoticeManager : BasePage
    {
        InfoNoticesBll infoNoticesBll = new InfoNoticesBll();
        DataTable dt = new DataTable();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        static SynclogModel logmodel = new SynclogModel();

        protected void Page_Load(object sender, EventArgs e)
        {
          PageHelper.ButtonControls(FuncList, pnlShowPanel.Controls, base.FuncListModule);
            SetCalendar(txtNoticeDate);
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
                ClientMessage.Add("NoticeDateMustBeToday", Message.NoticeDateMustBeToday);
                ClientMessage.Add("DeleteNoticeConfirm", Message.DeleteNoticeConfirm);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
            if (!base.IsPostBack)
            {
                ModuleCode.Value = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"] == null ? "" : Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;

                pager.CurrentPageIndex = 1;
                DataBind();
                NoticeTypeBind();
            }

        }
        #region 分頁
        /// <summary>
        /// 分頁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            DataBind();

        }
        #endregion


        #region Ajax請求獲得功能Model Json
        /// <summary>
        /// Ajax請求獲得功能Model Json
        /// </summary>
        protected override void AjaxProcess()
        {
            if (!string.IsNullOrEmpty(Request.Form["NoticeId"]))
            {
                InfoNoticesModel infoNoticesModel = infoNoticesBll.GetNoticeByKey(Request.Form["NoticeId"]);
                string noticeJson = null;
                noticeJson = JsSerializer.Serialize(infoNoticesModel);
                Response.Clear();
                Response.Write(noticeJson);
                Response.End();
            }
        }
        #endregion



        #region 公告信息綁定
        /// <summary>
        /// 公告信息綁定
        /// </summary>
        private void DataBind()
        {
            int totalCount;
            InfoNoticesModel model = PageHelper.GetModel<InfoNoticesModel>(pnlNoticeInfo.Controls, txtNoticeContent, hidNoticeId, hidBrowseTimes, hidAnnexFilePath);
            model.AnnexFilePath = null;
            if (model.NoticeDate != null)
            {
                model.NoticeDate = Convert.ToDateTime(Convert.ToDateTime(model.NoticeDate.ToString()).ToString("yyyy/MM/dd"));
            }
            dt = infoNoticesBll.GetOrderNotice(model, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            this.UltraWebGridInfoNotice.DataSource = dt;
            pager.RecordCount = totalCount;
            this.UltraWebGridInfoNotice.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
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
            pager.CurrentPageIndex = 1;
            DataBind();
            PageHelper.CleanControlsValue(pnlNoticeInfo.Controls);
            chkActiveFlag.Checked = true;
            ProcessFlag.Value = "N";
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            
            logmodel.ProcessFlag = "delete";
            InfoNoticesModel model = new InfoNoticesModel();
            model.NoticeId = Convert.ToInt32(hidNoticeId.Value);
          
            string alert  = infoNoticesBll.DeleteNotice(model, logmodel) ? Message.DeleteSuccess : Message.DeleteFailed;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "disableRole", "alert('"+alert+"')", true);
            PageHelper.CleanControlsValue(pnlNoticeInfo.Controls);
            chkActiveFlag.Checked = true;
            DataBind();
            ProcessFlag.Value = "N";
        }

        #endregion
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

        #region UltraWebGrid_DataBound
        /// <summary>
        /// UltraWebGrid_DataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UltraWebGridInfoNotice_DataBound(object sender, EventArgs e)
        {
            Infragistics.WebUI.UltraWebGrid.TemplatedColumn tcol = (TemplatedColumn)UltraWebGridInfoNotice.Bands[0].Columns[3];
            for (int loop = 0; loop < UltraWebGridInfoNotice.Rows.Count; loop++)
            {
                CellItem GridItem = (CellItem)tcol.CellItems[loop];
                Image image = (Image)(GridItem.FindControl("imgActiveFlag"));
                if (dt.Rows[loop]["Active_Flag"].ToString() == "Y")
                {
                    image.ImageUrl = "../../CSS/Images_new/gou.gif";
                }
                else
                {
                    image.ImageUrl = "../../CSS/Images_new/cha.gif";
                }
                //string urlStr = "<a style='text-decoration:none;' href=javascript:OpenDetail('Notice',";
                //urlStr += "'" + this.UltraWebGridInfoNotice.Rows[loop].Cells.FromKey("NOTICE_ID").Text + "')>" + this.UltraWebGridInfoNotice.Rows[loop].Cells.FromKey("Notice_Title").Value + "</a>";

                //this.UltraWebGridInfoNotice.Rows[loop].Cells.FromKey("Notice_Title").Value = urlStr;
            }
        }
        #endregion
    }
}
