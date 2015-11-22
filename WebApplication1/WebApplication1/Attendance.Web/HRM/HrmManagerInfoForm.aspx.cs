using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.HRM;
using GDSBG.MiABU.Attendance.Model.HRM;
using Infragistics.WebUI.UltraWebGrid;
using Resources;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： HrmManagerInfoForm.aspx.cs
 * 檔功能描述： 組織管理者資料
 * 
 * 版本：1.0
 * 創建標識： 昝望 2011.12.12
 * 
 */

namespace GDSBG.MiABU.Attendance.Web.HRM
{

    public partial class HrmManagerInfoForm : BasePage
    {

        JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
        Dictionary<string, string> ClientMessage = null;
        int totalCount;
        int flag;
        static DataTable dt_global = new DataTable();
        static SynclogModel logmodel = new SynclogModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            pager.CurrentPageIndex = 1;
            PageHelper.ButtonControls(base.FuncList, pnlShowPanel.Controls, base.FuncListModule);
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("DeleteConfirm", Message.DeleteConfirm);
                ClientMessage.Add("SaveConfim", Message.SaveConfim);
                ClientMessage.Add("DataExist", Message.DataExist);
                ClientMessage.Add("WorkNoNotNull", Message.WorkNoNotNull);
                ClientMessage.Add("NotesNotNull", Message.NotesNotNull);
                ClientMessage.Add("EmpNotExist", Message.EmpNotExist);
                ClientMessage.Add("RolesCodeNotNull", Message.RolesCodeNotNull);
                ClientMessage.Add("DepCodeNotNull", Message.DepCodeNotNull);
                ClientMessage.Add("LanguageNotNull", Message.LanguageNotNull);
                ClientMessage.Add("IfAdminNotNull", Message.IfAdminNotNull);
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
                ClientMessage.Add("NotOnlyOne", Message.NotOnlyOne);
                ClientMessage.Add("btnImport", Message.btnImport);
                ClientMessage.Add("btnBack", Message.btnBack);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterStartupScript(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);

            if (!IsPostBack)
            {
                this.TextBoxsReset("Cancel", true);
                //this.ButtonsReset("Cancel");
                StatusReset();
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"].ToString();
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;
                DataBind();
                SetSelector(imgDepCode, txtDepCode, txtDepName, "DepCode", Request.QueryString["ModuleCode"].ToString());
            }
        }

        #region 綁定數據
        /// <summary>
        /// 綁定數據
        /// </summary>

        private void DataBind()
        {
            ImportFlag.Value = "1";
            txtDepName.Attributes.Add("readonly", "true");
            txtWorkNo.Attributes.Add("readonly", "true");
            txtLocalName.Attributes.Add("readonly", "true");
            ddlManager.Attributes.Add("disabled", "true");
            txtNotes.Attributes.Add("readonly", "true");
            txtDeputy.Attributes.Add("readonly", "true");
            txtDeputyName.Attributes.Add("readonly", "true");
            txtDeputyNotes.Attributes.Add("readonly", "true");
            chkIsDirectlyUnder.Attributes.Add("disabled", "true");
            chkIsBGAudit.Attributes.Add("disabled", "true");
            btnSave.Attributes.Add("disabled", "true");
            btnCancel.Attributes.Add("disabled", "true");
            btnCondition.Attributes.Clear();
            btnQuery.Attributes.Clear();
            btnAdd.Attributes.Clear();
            btnDelete.Attributes.Clear();
            btnModify.Attributes.Clear();
            PanelData.Attributes.Add("class", "inner_table");
            PanelImport.Attributes.Add("class", "img_hidden");
            btnImport.Text = Message.btnImport;
            OrgmanagerModel model = new OrgmanagerModel();
            OrgmanagerBll bll = new OrgmanagerBll();
            ddlManager.DataSource = bll.GetManager();
            ddlManager.DataValueField = "MANAGERCODE";
            ddlManager.DataTextField = "MANAGERNAME";
            ddlManager.DataBind();
            this.ddlManager.Items.Insert(0, new ListItem("", ""));
            this.ddlManager.SelectedValue = "";
            DataTable dt = bll.GetOrgmanager(model, pager.CurrentPageIndex, pager.PageSize, out totalCount, base.SqlDep);
            pager.RecordCount = totalCount;
            this.UltraWebGridManagerInfo.DataSource = dt;
            this.UltraWebGridManagerInfo.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
        }

        private void DataBind(DataTable dt)
        {
            txtDepName.Attributes.Add("readonly", "true");
            txtWorkNo.Attributes.Add("readonly", "true");
            txtLocalName.Attributes.Add("readonly", "true");
            ddlManager.Attributes.Add("disabled", "true");
            txtNotes.Attributes.Add("readonly", "true");
            txtDeputy.Attributes.Add("readonly", "true");
            txtDeputyName.Attributes.Add("readonly", "true");
            txtDeputyNotes.Attributes.Add("readonly", "true");
            chkIsDirectlyUnder.Attributes.Add("disabled", "true");
            chkIsBGAudit.Attributes.Add("disabled", "true");
            btnSave.Attributes.Add("disabled", "true");
            btnCancel.Attributes.Add("disabled", "true");
            btnCondition.Attributes.Clear();
            btnQuery.Attributes.Clear();
            btnAdd.Attributes.Clear();
            btnDelete.Attributes.Clear();
            btnModify.Attributes.Clear();
            PanelData.Attributes.Add("class", "inner_table");
            PanelImport.Attributes.Add("class", "img_hidden");
            btnImport.Text = Message.btnImport;
            OrgmanagerBll bll = new OrgmanagerBll();
            this.ddlManager.SelectedValue = "";
            this.UltraWebGridManagerInfo.DataSource = dt;
            this.UltraWebGridManagerInfo.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
        }

        #endregion

        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            OrgmanagerModel model = PageHelper.GetModel<OrgmanagerModel>(pnlContent.Controls);
            OrgmanagerBll bll = new OrgmanagerBll();
            DataTable dt = bll.GetOrgmanager(model, pager.CurrentPageIndex, pager.PageSize, out totalCount, base.SqlDep);
            pager.RecordCount = totalCount;
            DataBind(dt);

        }
        #endregion

        #region 查詢
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            pager.CurrentPageIndex = 1;
            OrgmanagerModel model = PageHelper.GetModel<OrgmanagerModel>(pnlContent.Controls);
            OrgmanagerBll bll = new OrgmanagerBll();
            DataTable dt = bll.GetOrgmanager(model, pager.CurrentPageIndex, pager.PageSize, out totalCount, base.SqlDep);
            pager.RecordCount = totalCount;
            DataBind(dt);
            hidOperate.Value = "";

        }
        #endregion

        #region 驗證工號
        protected override void AjaxProcess()
        {
            string noticeJson = null;
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(Request.Form["Empno"]))
            {
                OrgmanagerBll bll = new OrgmanagerBll();
                string empno = Request.Form["Empno"];
                OrgmanagerModel model = new OrgmanagerModel();
                dt = bll.GetInfo(empno);
                if (dt.Rows.Count != 0)
                {
                    model.LocalName = dt.Rows[0]["LocalName"].ToString();
                    model.Manager = dt.Rows[0]["managercode"].ToString();
                    model.Notes = dt.Rows[0]["notes"].ToString();
                }
                if (model != null)
                {
                    noticeJson = JsSerializer.Serialize(model);
                }
                Response.Clear();
                Response.Write(noticeJson);
                Response.End();
            }
            if (!string.IsNullOrEmpty(Request.Form["Deputy"]))
            {
                OrgmanagerBll bll = new OrgmanagerBll();
                string empno = Request.Form["Deputy"];
                OrgmanagerModel model = new OrgmanagerModel();
                dt = bll.GetInfo(empno);
                if (dt.Rows.Count != 0)
                {
                    model.DeputyName = dt.Rows[0]["LocalName"].ToString();
                    model.DeputyNotes = dt.Rows[0]["notes"].ToString();
                }
                if (model != null)
                {
                    noticeJson = JsSerializer.Serialize(model);
                }
                Response.Clear();
                Response.Write(noticeJson);
                Response.End();
            }

            if (!string.IsNullOrEmpty(Request.Form["WorkNo"]))
            {
                OrgmanagerModel model = new OrgmanagerModel();
                OrgmanagerBll bll = new OrgmanagerBll();
                DataTable dt1 = new DataTable();
                if (!string.IsNullOrEmpty(Request.Form["WorkNo"]))
                {
                    model.WorkNo = Request.Form["WorkNo"];
                    model.DepCode = Request.Form["DepCode"];
                    dt1 = bll.GetOrgmanager(model);

                    if (dt1 != null)
                    {
                        noticeJson = dt1.Rows.Count.ToString();
                    }
                    Response.Clear();
                    Response.Write(noticeJson);
                    Response.End();
                }
            }

        }
        #endregion


        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            StatusReset();
            OrgmanagerModel model = PageHelper.GetModel<OrgmanagerModel>(pnlContent.Controls);
            model.ManagerName = ddlManager.SelectedItem.ToString();
            OrgmanagerBll bll = new OrgmanagerBll();
            int flag;
            DataTable dt = bll.GetOrgmanager(model, pager.CurrentPageIndex, pager.PageSize, out totalCount, base.SqlDep);
            if (dt.Rows.Count == 0)
            {
                if (hidOperate.Value == "modify")
                {
                    logmodel.ProcessFlag = "update";
                    flag = bll.UpdateOrgmanagerByKey(model,HiddenDepCode.Value,HiddenWorkNo.Value, logmodel);
                    if (flag == 1)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.UpdateSuccess + "');", true);
                        DataBind();
                    }
                    else if (flag == 2)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.OnlyOneDirectlyunder + "');", true);

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.UpdateFailed + "');", true);
                    }

                }
                if (hidOperate.Value == "add")
                {
                    logmodel.ProcessFlag = "insert";
                    flag = bll.AddOrgmanager(model, logmodel);
                    if (flag == 1)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.AddSuccess + "');", true);
                        DataBind();
                    }
                    else if (flag == 2)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.OnlyOneDirectlyunder + "');", true);

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.AddFailed + "');", true);
                    }

                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.NotOnlyOne + "');", true);
            }

        }
        #endregion

        #region 刪除
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            logmodel.ProcessFlag = "delete";
            OrgmanagerModel model = PageHelper.GetModel<OrgmanagerModel>(pnlContent.Controls);
            OrgmanagerBll bll = new OrgmanagerBll();
            int intDeleteOk = 0;
            int intDeleteError = 0;
            TemplatedColumn tcol = (TemplatedColumn)this.UltraWebGridManagerInfo.Bands[0].Columns[0];
            for (int i = 0; i < this.UltraWebGridManagerInfo.Rows.Count; i++)
            {
                CellItem GridItem = (CellItem)tcol.CellItems[i];
                CheckBox chkIsHaveRight = (CheckBox)GridItem.FindControl("CheckBoxCell");
                if (chkIsHaveRight.Checked)
                {
                    int num = bll.DeleteOrgmanagerByKey(this.UltraWebGridManagerInfo.Rows[i].Cells.FromKey("WorkNo").Text.Trim(), this.UltraWebGridManagerInfo.Rows[i].Cells.FromKey("DepCode").Text.Trim(), logmodel);
                    if (num > 0)
                    {
                        intDeleteOk++;
                    }
                    else
                    {
                        intDeleteError++;
                    }
                }
            }
            if ((intDeleteOk + intDeleteError) > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.DeleteSuccess + ":" + intDeleteOk + ";" + Message.DeleteFailed + ":" + intDeleteError + "');", true);
                DataBind();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "isnumber", "alert('" + Message.AtLastOneChoose + "');", true);
                return;
            }

        }
        #endregion

        #region 取消
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.ProcessFlag.Value = "";
            this.TextBoxsReset("Cancel", true);
            this.ButtonsReset("Cancel");
            StatusReset();
        }
        #endregion


        #region 導入按鈕文字切換
        /// <summary>
        /// 導入按鈕文字切換
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (ImportFlag.Value == "1")
            {
                PanelData.Attributes.Add("class", "img_hidden");
                PanelImport.Attributes.Add("class", "inner_table");
                btnCondition.Attributes.Add("disabled", "true");
                btnQuery.Attributes.Add("disabled", "true");
                btnAdd.Attributes.Add("disabled", "true");
                btnDelete.Attributes.Add("disabled", "true");
                btnModify.Attributes.Add("disabled", "true");
                btnCancel.Attributes.Add("disabled", "true");
                btnSave.Attributes.Add("disabled", "true");
                btnImport.Text = Message.btnBack;
                ImportFlag.Value = "2";
            }
            else
            {
                StatusReset();
            }
        }
        #endregion

        #region 導入
        protected void btnImportSave_Click(object sender, EventArgs e)
        {
            OrgmanagerBll bll = new OrgmanagerBll();
            int succesnum = 0;
            int errornum = 0;
            int flag = ImportModuleExcel();
            if (flag == 1)
            {
                logmodel.ProcessFlag = "insert";
                DataTable dt = bll.ImpoertExcel(CurrentUserInfo.Personcode, out succesnum, out errornum, logmodel);
                lblupload.Text = "上傳成功筆數：" + succesnum + ",上傳失敗筆數：" + errornum;
                dt_global = changeError(dt);
                this.UltraWebGridImport.DataSource = dt_global;
                this.UltraWebGridImport.DataBind();
            }
            else if (flag == 0)
            {
                lblupload.Text = "數據保存失敗！";
            }
            else
            {
                lblupload.Text = "Excel數據格式錯誤";
            }

            PanelData.Attributes.Add("class", "img_hidden");
            PanelImport.Attributes.Add("class", "inner_table");
            btnCondition.Attributes.Add("disabled", "true");
            btnQuery.Attributes.Add("disabled", "true");
            btnAdd.Attributes.Add("disabled", "true");
            btnDelete.Attributes.Add("disabled", "true");
            btnModify.Attributes.Add("disabled", "true");
            btnCancel.Attributes.Add("disabled", "true");
            btnSave.Attributes.Add("disabled", "true");
            btnImport.Text = Message.btnBack;

        }

        #region 轉換錯誤信息
        /// <summary>
        /// 轉換錯誤信息
        /// </summary>
        private DataTable changeError(DataTable dt)
        {
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string errorInfo = "";
                    for (int j = 0; j < dt.Rows[i]["ErrorMsg"].ToString().Split('§').Length; j++)
                    {

                        switch (dt.Rows[i]["ErrorMsg"].ToString().Split('§')[j].ToString().Trim())
                        {
                            case "ErrDepcodeNull": errorInfo = errorInfo + Message.ErrDepcodeNull;
                                break;
                            case "ErrWorknoNull": errorInfo = errorInfo + Message.ErrWorknoNull;
                                break;
                            case "ErrNotesNull": errorInfo = errorInfo + Message.ErrNotesNull;
                                break;
                            case "ErrIsdirectlyunderNull": errorInfo = errorInfo + Message.ErrIsdirectlyunderNull;
                                break;
                            case "NotOnlyOne": errorInfo = errorInfo + Message.NotOnlyOne;
                                break;
                            case "ErrIsbgauditNull": errorInfo = errorInfo + Message.ErrIsbgauditNull;
                                break;
                            case "ErrWorknoNotFind": errorInfo = errorInfo + Message.ErrWorknoNotFind;
                                break;
                            case "ErrDeputyNotFind": errorInfo = errorInfo + Message.ErrDeputyNotFind;
                                break;
                            case "ErrIsdirectlyunder": errorInfo = errorInfo + Message.ErrIsdirectlyunder;
                                break;
                            case "ErrIsbgaudit": errorInfo = errorInfo + Message.ErrIsbgaudit;
                                break;
                            case "OnlyOneDirectlyunder": errorInfo = errorInfo + Message.OnlyOneDirectlyunder;
                                break;
                            default: break;
                        }
                    }
                    dt.Rows[i]["ErrorMsg"] = errorInfo;
                }
            }
            return dt;
        }
        #endregion


        #region 將文件中的數據導入到數據庫中
        /// <summary>
        /// 將文件中的數據導入到數據庫中
        /// </summary>
        private int ImportModuleExcel()
        {
            int flag;
            bool deFlag;
            string tableName = "GDS_ATT_ORGMANAGER_temp";
            string[] columnProperties = { "DEPNAME", "DEPCODE", "WORKNO", "NOTES", "ISDIRECTLYUNDER", "ISBGAUDIT", "DEPUTY", "DEPUTYNOTES" };
            string[] columnType = { "varchar", "varchar", "varchar", "varchar", "varchar", "varchar", "varchar", "varchar" };
            string createUser = CurrentUserInfo.Personcode;
            deFlag = NPOIHelper.DeleteExcelSql(tableName, createUser);
            string filePath = GetImpFileName();
            if (string.IsNullOrEmpty(filePath))
                return 1;

            flag = NPOIHelper.ImportExcel(columnProperties, columnType, filePath, tableName, createUser);
            return flag;
        }
        #endregion

        #region 獲取上傳文件名,并將文件保存到服務器
        /// <summary>
        /// 獲取導入的文件名,并將文件保存到服務器
        /// </summary>
        /// <returns></returns>
        private string GetImpFileName()
        {
            string filePath = "";
            if (FileUpload.FileName.Trim() != "")
            {
                try
                {
                    filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetFileName(FileUpload.FileName);
                    FileUpload.SaveAs(filePath);

                }
                catch
                {
                    lblupload.Text = "Exel上傳到服務器失敗!";
                }
            }
            else
            {
                lblupload.Text = "導入路徑為空,請選擇要匯入的Excel文件！";
            }

            return filePath;
        }
        #endregion

        #endregion

        #region 導出報表
        protected void btnExport_Click(object sender, EventArgs e)
        {
            btnImport.Text = Message.btnBack;
            if (dt_global.Rows.Count != 0)
            {
                OrgmanagerModel model = PageHelper.GetModel<OrgmanagerModel>(pnlContent.Controls);
                OrgmanagerBll bll = new OrgmanagerBll();
                List<OrgmanagerModel> list = bll.GetList(dt_global);
                string[] header = { ControlText.gvHeadErrorMsg, ControlText.gvHeadDepName, ControlText.gvHeadDepCode, ControlText.gvHeadWorkNo, ControlText.gvHeadNotes, ControlText.gvHeadIsDirectlyUnder, ControlText.gvHeadIsBGAudit, ControlText.gvHeadDeputy, ControlText.gvHeadProxyNotes };
                string[] properties = { "ErrorMsg", "DepName", "DepCode", "WorkNo", "Notes", "IsDirectlyUnder", "IsBGAudit", "Deputy", "DeputyNotes" };
                string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
                PageHelper.ReturnHTTPStream(filePath, true);
            }
            else
            {
                lblupload.Text = "導出數據不存在";
            }
        }
        #endregion

        #region 設置Selector
        /// <summary>
        /// 設置Selector
        /// </summary>
        /// <param name="ctrlTrigger">控件ID--按鈕</param>
        /// <param name="ctrlCode">控件ID--文本框1</param>
        /// <param name="ctrlName">控件ID--文本框2</param>
        /// <param name="flag">Selector區分標誌</param>
        public static void SetSelector(WebControl ctrlTrigger, Control ctrlCode, Control ctrlName, string flag, string moduleCode)
        {
            if (ctrlCode is TextBox) { (ctrlCode as TextBox).Attributes.Add("readonly", "readonly"); }
            if (ctrlName is TextBox) { (ctrlName as TextBox).Attributes.Add("readonly", "readonly"); }
            ctrlTrigger.Attributes.Add("onclick", string.Format("return setSelector('{0}','{1}','{2}','{3}')",
                ctrlCode.ClientID, ctrlName.ClientID, flag, moduleCode));
        }

        #endregion

        #region 私有函數
        private void StatusReset()
        {
            PanelData.Attributes.Add("class", "inner_table");
            PanelImport.Attributes.Add("class", "img_hidden");
            btnCondition.Attributes.Remove("disabled");
            btnQuery.Attributes.Remove("disabled");
            btnAdd.Attributes.Remove("disabled");
            btnDelete.Attributes.Remove("disabled");
            btnModify.Attributes.Remove("disabled");
            btnCancel.Attributes.Remove("disabled");
            btnSave.Attributes.Remove("disabled");
            btnImport.Text = Message.btnImport;
            ImportFlag.Value = "1";
        }

        private void ButtonsReset(string buttonText)
        {
            this.btnCondition.Enabled = true;
            this.btnQuery.Enabled = true;
            this.btnAdd.Enabled = true;
            this.btnModify.Enabled = true;
            this.btnDelete.Enabled = true;
            this.btnCancel.Enabled = true;
            this.btnSave.Enabled = true;
            this.btnImport.Enabled = true;
            switch (buttonText)
            {
                case "Condition":
                    this.btnCondition.Enabled = false;
                    this.btnAdd.Enabled = false;
                    this.btnModify.Enabled = false;
                    this.btnDelete.Enabled = false;
                    this.btnSave.Enabled = false;
                    this.btnImport.Enabled = false;
                    break;

                case "Query":
                case "Delete":
                case "Cancel":
                case "Save":
                    this.btnSave.Enabled = false;
                    this.btnCancel.Enabled = false;
                    break;

                case "Add":
                case "Modify":
                    this.btnCondition.Enabled = false;
                    this.btnQuery.Enabled = false;
                    this.btnAdd.Enabled = false;
                    this.btnModify.Enabled = false;
                    this.btnDelete.Enabled = false;
                    this.btnImport.Enabled = false;
                    break;
            }
        }

        private void TextBoxsReset(string buttonText, bool read)
        {
            this.txtDepName.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtWorkNo.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtLocalName.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtNotes.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtDeputyNotes.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtDeputy.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtDeputyName.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            chkIsDirectlyUnder.Attributes.Add("disabled", "true");
            chkIsBGAudit.Attributes.Add("disabled", "true");
            if (buttonText != "Modify")
            {
                this.txtDepName.Text = "";
                this.txtWorkNo.Text = "";
                this.txtLocalName.Text = "";
                this.txtNotes.Text = "";
                this.txtDeputyNotes.Text = "";
                this.txtDeputy.Text = "";
                this.txtDeputyName.Text = "";
                this.chkIsDirectlyUnder.Checked = false;
                this.chkIsBGAudit.Checked = false;
            }
            else
            {
                this.txtDepName.BorderStyle = BorderStyle.None;
                this.txtLocalName.BorderStyle = BorderStyle.None;
                this.txtDeputyName.BorderStyle = BorderStyle.None;
                this.chkIsDirectlyUnder.Checked = this.HiddenIsDirectlyUnder.Value == "Y";
                this.chkIsBGAudit.Checked = this.HiddenIsBGAudit.Value == "Y";
            }
            if (buttonText == "Add")
            {
                this.txtLocalName.BorderStyle = BorderStyle.None;
                this.txtDeputyName.BorderStyle = BorderStyle.None;
                this.HiddenDepCode.Value = "";
                this.HiddenWorkNo.Value = "";
            }
        }



        #endregion


    }
}

