/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： Synclog.cs
 * 檔功能描述： 日誌管理UI層
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2012.01.09
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using GDSBG.MiABU.Attendance.BLL.SystemManage.SystemSafety;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;
using Resources;

namespace GDSBG.MiABU.Attendance.Web.SystemManage.SystemSafety
{
    public partial class Synclog : BasePage
    {
        SynclogBll syncBll = new SynclogBll();
        SynclogModel model = new SynclogModel();
        int totalCount = 0;
        #region
        protected void Page_Load(object sender, EventArgs e)
        {
            SetCalendar(txtFromDate, txtToDate);
            JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
            Dictionary<string, string> ClientMessage = null;
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("WrongDate", Message.WrongDate);
                ClientMessage.Add("ToLaterThanFrom", Message.ToLaterThanFrom);
                pager.CurrentPageIndex = 1;
                dataBind();
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
        }
        #endregion
        #region  查詢
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            pager.CurrentPageIndex = 1;
            dataBind();
        }
        #endregion
        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            dataBind();
        }
        #endregion
        #region 綁定數據
        protected void dataBind()
        {
            model = PageHelper.GetModel<SynclogModel>(pnlContent.Controls);
            string FromDate = txtFromDate.Text.Trim();
            string ToDate = txtToDate.Text.Trim();
            string condition = "";
            if (chkException.Checked)
            {
                condition += "1";
            }
            if (chkAction.Checked && !(chkException.Checked))
            {
                condition += "2";
            }
            else if (chkException.Checked && chkAction.Checked)
            {
                condition += ",2";
            }
            if (chkError.Checked && !(chkException.Checked) && !(chkAction.Checked))
            {
                condition += "3";
            }
            else if (chkError.Checked && chkAction.Checked||chkException.Checked && chkError.Checked)
            {
                condition += ",3";
            }
            DataTable dt = syncBll.SelectByString(model, condition, FromDate, ToDate, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            pager.RecordCount = totalCount;
            UltraWebGridException.DataSource = dt;
            UltraWebGridException.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
        }
        #endregion
    }
}
