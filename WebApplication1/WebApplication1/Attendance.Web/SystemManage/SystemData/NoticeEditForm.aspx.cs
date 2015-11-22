/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： NoticeEditForm.cs
 * 檔功能描述： 新增系統公告UI類
 * 
 * 版本：1.0
 * 創建標識： 陳函 2012.01.03
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.SystemManage.SystemData;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemData;
using Resources;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.Web.SystemManage.SystemData
{
    public partial class NoticeEditForm : BasePage
    {
        InfoNoticesBll infoNoticesBll = new InfoNoticesBll();
        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        static SynclogModel logmodel = new SynclogModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            hidNoticeId.Value = Request.QueryString["NoticeId"]==null?"":Request.QueryString["NoticeId"].ToString();
            hidOperate.Value = Request.QueryString["hidOperate"] == null ? "" : Request.QueryString["hidOperate"].ToString();
            if (!string.IsNullOrEmpty(Request.QueryString["path"]))
            {
                PageHelper.ReturnHTTPStream(MapPath("~/AnnexFiles/" + Request.QueryString["path"]), false);
            }
            SetCalendar(txtNoticeDate);
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("NoticeDateMustBeToday", Message.NoticeDateMustBeToday);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
            if (!base.IsPostBack)
            {
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["NoticeManagerModuleCode"] == null ? "" : Request.QueryString["NoticeManagerModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;

                NoticeTypeBind();
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

            string fileName = GetImpFileName();
            InfoNoticesModel model = PageHelper.GetModel<InfoNoticesModel>(pnlNoticeInfo.Controls);
            model.NoticeContent = txtNoticeContent.Text;
            model.AnnexFilePath += fileName;
            if (hidOperate.Value == "Add")
            {
                logmodel.ProcessFlag = "insert";

                model.BrowseTimes = 0;
                model.CreateDate = DateTime.Now;
                model.CreateUser = CurrentUserInfo.Personcode;
                lblMessage.Text = infoNoticesBll.AddNotice(model, logmodel) ? Message.SaveSuccess : Message.SaveFailed;
            }
            else if (hidOperate.Value == "Modify")
            {
                logmodel.ProcessFlag = "update";

                lblMessage.Text = infoNoticesBll.UpdateNoticeByKey(model, logmodel) ? Message.UpdateSuccess : Message.UpdateFailed;
            }
            PageHelper.CleanControlsValue(pnlNoticeInfo.Controls);
            chkActiveFlag.Checked = true;
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "showNotice", "window.location.href='NoticeManager.aspx?ModuleCode=" + Request.QueryString["NoticeManagerModuleCode"].ToString() + "'", true);
        //    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "showNotice", "window.parent.document.all.btnQuery.click();", true);
         
        }
        #endregion


        #region 獲取上傳文件名,并將文件保存到服務器
        /// <summary>
        /// 獲取上傳文件名,并將文件保存到服務器
        /// </summary>
        /// <returns></returns>
        private string GetImpFileName()
        {
            StringBuilder allFileName = new StringBuilder();
            HttpFileCollection files = Request.Files;
            for (int iFile = 0; iFile < files.Count; iFile++)
            {
                HttpPostedFile postedFile = files[iFile];
                string fileName, fileExtension;
                fileName = Path.GetFileName(postedFile.FileName);
                if (fileName != "")
                {
                    fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + fileName;
                    fileExtension = Path.GetExtension(fileName);
                    postedFile.SaveAs(HttpContext.Current.Request.MapPath("~/AnnexFiles/") + fileName);
                    allFileName.Append(fileName + "|");
                }
            }
            return allFileName.ToString();
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

        #region Ajax請求獲得功能Model Json
        /// <summary>
        /// Ajax請求獲得功能Model Json
        /// </summary>
        protected override void AjaxProcess()
        {
            if (!string.IsNullOrEmpty(Request.Form["NoticeId"]) && !string.IsNullOrEmpty(Request.Form["Operate"]))
            {
                if (Request.Form["Operate"].ToString() == "Modify")
                {
                    InfoNoticesModel infoNoticesModel = infoNoticesBll.GetNoticeByKey(Request.Form["NoticeId"]);
                    string noticeJson = null;
                    noticeJson = JsSerializer.Serialize(infoNoticesModel);
                    Response.Clear();
                    Response.Write(noticeJson);
                    Response.End();
                }
            }
        }
        #endregion
    }
}
