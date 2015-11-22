/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： BUCalendar.cs
 * 檔功能描述： BU行事歷UI層
 * 
 * 版本：1.0
 * 創建標識： 高子焱 2012.01.09
 * 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using GDSBG.MiABU.Attendance.BLL.KQM.BasicData;
using GDSBG.MiABU.Attendance.Model.KQM.BasicData;
using Resources;
using GDSBG.MiABU.Attendance.Model.SystemManage.SystemSafety;

namespace GDSBG.MiABU.Attendance.Web.KQM.BasicData
{
    public partial class BUCalendar : BasePage
    {
        BUCalendarModel model = new BUCalendarModel();
        static DataTable dt = new DataTable();
        BUCalendarBll bll = new BUCalendarBll();
        public string strFriday;
        public string strMonday;
        public string strSaturday;
        public string strSunday;
        public string strThursday;
        public string strTuesday;
        public string strWednesday;
        static SynclogModel logmodel = new SynclogModel();
        #region 上傳
        protected void btnImportSave_Click(object sender, EventArgs e)
        {
            int succesnum = 0;
            int errornum = 0;
            int flag = ImportModuleExcel();
            if (flag == 1)
            {
                logmodel.ProcessFlag = "insert";
                dt = bll.ImpoertExcel(CurrentUserInfo.Personcode, CurrentUserInfo.CompanyId, out succesnum, out errornum, logmodel);
                if (succesnum == 0 && errornum == 0)
                {
                    lblupload.Text = Message.ImportInfoIsWrong;
                    return;
                }

                lblupload.Text = Message.NumberOfSuccessed + succesnum + Message.NumberOfFailed + errornum;
                dt = changeError(dt);
                this.UltraWebGridImport.DataSource = dt;
                this.UltraWebGridImport.DataBind();

            }
            else if (flag == 0)
            {
                lblupload.Text = Message.FailToSaveData;
            }
            else if (flag == -1)
            {
                lblupload.Text = Message.DataFormatError;
            }
            else if (flag == -3)
            {
                lblupload.Text = Message.IsNotExcel;
            }
            btnImport.Text = Message.btnBack;
            ImportFlag.Value = "2";
        }
        #endregion
        #region  頁面加載
        protected void Page_Load(object sender, EventArgs e)
        {
            JavaScriptSerializer JsSerializer = new JavaScriptSerializer();
            Dictionary<string, string> ClientMessage = null;
            DataTable dtSelectAll = new DataTable();
            this.strMonday = Resources.ControlText.monday;
            this.strTuesday = Resources.ControlText.tuesday;
            this.strWednesday = Resources.ControlText.wednesday;
            this.strThursday = Resources.ControlText.thursday;
            this.strFriday = Resources.ControlText.friday;
            this.strSaturday = Resources.ControlText.saturday;
            this.strSunday = Resources.ControlText.sunday;
            if (!this.PanelImport.Visible)
            {
                this.btnImport.Text = Message.btnImport;
            }
            else
            {
                this.btnImport.Text = Message.btnBack;
            }
            PageHelper.ButtonControls(base.FuncList, pnlShowPanel.Controls, base.FuncListModule);
            SetCalendar(txtWorkDay);
            if (!IsPostBack)
            {
                btnCancel.Attributes.Add("disabled", "true");
                btnSave.Attributes.Add("disabled", "true");
                logmodel.ProcessOwner = CurrentUserInfo.Personcode;
                logmodel.TransactionType = Request.QueryString["ModuleCode"];
                logmodel.LevelNo = "2";
                logmodel.FromHost = Request.UserHostAddress;
                this.txtWorkDay.Attributes.Add("onpropertychange", "javascript:WeekDate();");
                pager.CurrentPageIndex = 1;
                SetSelector(imgDPcode, txtBUCode, txtDPname, Request.QueryString["ModuleCode"].ToString());
                this.ddlHoliDayFlag.Items.Insert(0, new ListItem("", ""));
                this.ddlHoliDayFlag.Items.Insert(1, new ListItem("Y", "Y"));
                this.ddlHoliDayFlag.Items.Insert(2, new ListItem("N", "N"));
                this.ddlWorkFlag.Items.Insert(0, new ListItem("", ""));
                this.ddlWorkFlag.Items.Insert(1, new ListItem("Y", "Y"));
                this.ddlWorkFlag.Items.Insert(2, new ListItem("N", "N"));
                this.textBoxsReset("", true);
                DataUIBind();
            }
            if (ClientMessage == null)
            {
                ClientMessage = new Dictionary<string, string>();
                ClientMessage.Add("NotOnlyOne", Message.NotOnlyOne);
                ClientMessage.Add("AtLastOneChoose", Message.AtLastOneChoose);
                ClientMessage.Add("TextBoxNotNull", Message.TextBoxNotNull);
                ClientMessage.Add("DeleteConfirm", Message.DeleteConfirm);
                ClientMessage.Add("SaveConfirm", Message.SaveConfirm);
                ClientMessage.Add("WrongDate", Message.WrongDate);
                ClientMessage.Add("btnImport", Message.btnImport);
                ClientMessage.Add("btnBack", Message.btnBack);
                ClientMessage.Add("MustSameWithBU", Message.MustSameWithBU);
            }
            string clientmsg = JsSerializer.Serialize(ClientMessage);
            Page.ClientScript.RegisterClientScriptBlock(GetType(), "ClientMessage", "var Message=" + clientmsg + ";", true);
        }
        #endregion

        #region webGrid綁定數據
        private void DataUIBind()
        {
            ImportFlag.Value = "1";
            int totalCount;
            string SQLDep = base.SqlDep;
            string depCode = model.BUCode;
            model.BUCode = null;
            model.OrgName = null;
            model.HolidayFlag = ddlHoliDayFlag.SelectedValue;
            DataTable dtSelect = bll.GetBUCalendarList(model, SqlDep,depCode, pager.CurrentPageIndex, pager.PageSize, out totalCount);
            pager.RecordCount = totalCount;
            this.UltraWebGridBuCalendar.DataSource = dtSelect;
            this.UltraWebGridBuCalendar.DataBind();
            pager.TextAfterPageIndexBox = "/" + pager.PageCount.ToString();
        }
        #endregion
        #region 分頁
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            if (this.ProcessFlag.Value == "Condition")
            {
                model = PageHelper.GetModel<BUCalendarModel>(pnlContent.Controls);
            }
            else
            {
                model = new BUCalendarModel();
            }
            DataUIBind();
        }
        #endregion
        #region 導入按鈕事件
        protected void btnImport_Click(object sender, EventArgs e)
        {
            this.UltraWebGridImport.Rows.Clear();
            if (!this.PanelImport.Visible)
            {
                this.PanelImport.Visible = true;
                this.PanelData.Visible = false;
                this.btnCondition.Enabled = false;
                this.btnQuery.Enabled = false;
                this.btnDelete.Enabled = false;
                this.btnAdd.Enabled = false;
                this.btnModify.Enabled = false;
                this.btnImport.Text = Message.btnBack;
                this.lblupload.Text = "";
            }
            else
            {
                this.PanelImport.Visible = false;
                this.PanelData.Visible = true;
                this.btnCondition.Enabled = true;
                this.btnQuery.Enabled = true;
                this.btnDelete.Enabled = true;
                this.btnAdd.Enabled = true;
                this.btnModify.Enabled = true;
                this.btnImport.Text = Message.btnImport;
                this.lblupload.Text = "";
            }
        }
        #endregion
        #region 放大鏡綁定
        /// <summary>
        /// 設置Selector
        /// </summary>
        /// <param name="ctrlTrigger">控件ID--按鈕</param>
        /// <param name="ctrlCode">控件ID--文本框1</param>
        /// <param name="ctrlName">控件ID--文本框2</param>
        public void SetSelector(WebControl ctrlTrigger, Control ctrlCode, Control ctrlName, string moduleCode)
        {
            if (ctrlCode is TextBox) { (ctrlCode as TextBox).Attributes.Add("readonly", "readonly"); }
            if (ctrlName is TextBox) { (ctrlName as TextBox).Attributes.Add("readonly", "readonly"); }
            ctrlTrigger.Attributes.Add("onclick", string.Format("return setSelector('{0}','{1}','{2}')",
                ctrlCode.ClientID, ctrlName.ClientID, moduleCode));
        }
        #endregion

        #region 查詢
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            if (this.hidOperate.Value == "Condition")
            {
                ProcessFlag.Value = "Condition";
                model = PageHelper.GetModel<BUCalendarModel>(pnlContent.Controls);
            }
            else
            {
                model = new BUCalendarModel();
            }
            pager.CurrentPageIndex = 1;
            DataUIBind();
            hidOperate.Value = "";
        }
        #endregion

        #region 刪除事件
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string alert = "";
            string actionFlag = this.hidOperate.Value.ToString().Trim();
            //刪除
            if (actionFlag == "Delete")
            {
                logmodel.ProcessFlag = "delete";
                model.BUCode = txtBUCode.Text.Trim().ToString();
                model.WorkDay = Convert.ToDateTime(txtWorkDay.Text.Trim().ToString());
                if (bll.DeleteBUCalendarByKey(model, logmodel) > 0)
                {
                    alert = "alert('" + Message.DeleteSuccess + "')";
                }
                else
                {
                    alert = "alert('" + Message.DeleteFailed + "')";
                }
                this.hidOperate.Value = "";
            }
            Page.ClientScript.RegisterClientScriptBlock(GetType(), "show", alert, true);

            pager.CurrentPageIndex = 1;
            PageHelper.CleanControlsValue(pnlContent.Controls);
            model = new BUCalendarModel();
            DataUIBind();
        }
        #endregion
        #region 保留狀態
        private void ButtonsReset(string buttonText)
        {
            btnCondition.Attributes.Remove("disabled");
            btnQuery.Attributes.Remove("disabled");
            btnAdd.Attributes.Remove("disabled");
            btnModify.Attributes.Remove("disabled");
            btnDelete.Attributes.Remove("disabled");
            btnCancel.Attributes.Remove("disabled");
            btnSave.Attributes.Remove("disabled");
            switch (buttonText)
            {
                case "Condition":
                    btnCondition.Attributes.Add("disabled", "true");
                    btnAdd.Attributes.Add("disabled", "true");
                    btnModify.Attributes.Add("disabled", "true");
                    btnDelete.Attributes.Add("disabled", "true");
                    btnSave.Attributes.Add("disabled", "true");
                    break;

                case "Query":
                case "Delete":
                case "Cancel":
                case "Save":
                    btnCancel.Attributes.Add("disabled", "true");
                    btnSave.Attributes.Add("disabled", "true");
                    break;

                case "Add":
                case "Modify":
                    btnCondition.Attributes.Add("disabled", "true");
                    btnQuery.Attributes.Add("disabled", "true");
                    btnAdd.Attributes.Add("disabled", "true");
                    btnModify.Attributes.Add("disabled", "true");
                    btnDelete.Attributes.Add("disabled", "true");
                    break;
            }
        }
        private void textBoxsReset(string buttonText, bool read)
        {
            this.ddlWorkFlag.Attributes.Add("disabled", "true");
            this.ddlHoliDayFlag.Attributes.Add("disabled","true");
            txtWorkDay.Attributes.Add("readonly","true");
            txtWeekNo.Attributes.Add("readonly", "true");
            txtWeekDay.Attributes.Add("readonly", "true");
            txtRemark.Attributes.Add("readonly", "true");
            txtWorkDay.Attributes.Add("readonly", "true");
            txtBUCode.Attributes.Add("readonly", "true");
            imgDPcode.Attributes.Add("class", "img_hidden");
            this.ddlWorkFlag.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.ddlHoliDayFlag.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtWorkDay.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtWeekNo.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtWeekDay.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtRemark.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtBUCode.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            this.txtDPname.BorderStyle = read ? BorderStyle.None : BorderStyle.NotSet;
            if (buttonText == "Add" || buttonText == "Modify")
            {
                this.ddlWorkFlag.BorderStyle = read ? BorderStyle.None : BorderStyle.Solid;
                this.ddlHoliDayFlag.BorderStyle = read ? BorderStyle.None : BorderStyle.Solid;
                this.txtWorkDay.BorderStyle = read ? BorderStyle.None : BorderStyle.Solid;
                this.txtWeekNo.BorderStyle = read ? BorderStyle.None : BorderStyle.Solid;
                this.txtWeekDay.BorderStyle = read ? BorderStyle.None : BorderStyle.Solid;
                this.txtRemark.BorderStyle = read ? BorderStyle.None : BorderStyle.Solid;
                this.ddlWorkFlag.Attributes.Remove("disabled");
                this.ddlHoliDayFlag.Attributes.Remove("disabled");
                txtWorkDay.Attributes.Remove("readonly");
                txtWeekNo.Attributes.Remove("readonly");
                txtWeekDay.Attributes.Remove("readonly");
                txtRemark.Attributes.Remove("readonly");
                txtWorkDay.Attributes.Remove("readonly");
                txtBUCode.Attributes.Remove("readonly");
                imgDPcode.Attributes.Add("class","img_show");
            }
        }
        #endregion
        #region Ajax事件
        protected override void AjaxProcess()
        {
            bool blPro = false;
            int result = 0;
            string workDay = Request.Form["WorkDay"];
            string buCode = Request.Form["BUCodeO"];
            string activeFlag = Request.Form["ActiveFlag"];
            string workDayAjax=Request.Form["WorkDayAjax"];
            string buCodeAjax=Request.Form["BUCodeAjax"];
            if (activeFlag == "Add" || activeFlag == "Modify" && Request.Form["WorkDay"] != null)
            {
                string CompanyId = CurrentUserInfo.CompanyId;
                model.WorkDay = Convert.ToDateTime(Request.Form["WorkDay"]);
                DataTable dtBGCalendar = bll.GetBGCalendarByKey(model, CompanyId);
                //數據不全暫時這樣寫
                if (dtBGCalendar != null)
                {
                    if (dtBGCalendar.Rows.Count > 0)
                    {
                        if (dtBGCalendar.Rows[0]["holidayflag"].ToString() != Request.Form["HolidayFlag"])
                        {
                            result = 2;
                        }
                        else
                        {
                            result = 1;
                        }
                    }
                    else
                    {
                        result = 1;
                    }
                }
                else
                {
                    result = 1;
                }

                blPro = true;
            }
            if ((activeFlag == "Add") && workDay != null && buCode != null)
            {
                model.WorkDay = Convert.ToDateTime(Request.Form["WorkDay"]);
                model.BUCode = Request.Form["BUCodeO"];
                DataTable dtExist = bll.GetBUCalendarNum(model);
                if (dtExist.Rows.Count > 0)
                {
                    result = 3;
                }
                else
                {
                    result = 1;
                }
            }
            if ((activeFlag == "Modify") && workDay != null && buCode != null)
            {
                if (workDayAjax == workDay && buCodeAjax == buCode)
                {
                    result = 1;
                }
                else
                {
                    model.WorkDay = Convert.ToDateTime(Request.Form["WorkDay"]);
                    model.BUCode = Request.Form["BUCodeO"];
                    DataTable dtExist = bll.GetBUCalendarNum(model);
                    if (dtExist.Rows.Count > 0)
                    {
                        result = 3;
                    }
                    else
                    {
                        result = 4;
                    }
                }
            }
            if (blPro)
            {
                Response.Clear();
                Response.Write(result.ToString());
                Response.End();
            }
        }
        #endregion


        #region 保存事件
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string activeFlag = hidOperate.Value.ToString().Trim();
            if (txtBUCode.Text.Length > 0)
            {
                string LevelCode = bll.GetValue(txtBUCode.Text.ToString());
                int DepLevel = 10;
                if (CurrentUserInfo.DepLevel.ToString().Length > 0)
                {
                    DepLevel = Convert.ToInt32(CurrentUserInfo.DepLevel.ToString());
                }
                switch (LevelCode)
                {
                    case "0":
                        if (DepLevel <= 0)
                        {
                            break;

                        }
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "show", "alert('" + Message.NoAuthority + "')", true);
                        ButtonsReset(hidOperate.Value);
                        textBoxsReset(hidOperate.Value, false);
                        return;

                    case "1":
                        if (DepLevel <= 1)
                        {
                            break;
                        }
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "show", "alert('" + Message.NoAuthority + "')", true);
                        ButtonsReset(hidOperate.Value);
                        textBoxsReset(hidOperate.Value, false);
                        return;

                    case "2":
                        if (DepLevel <= 2)
                        {
                            break;
                        }
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "show", "alert('" + Message.NoAuthority + "')", true);
                        ButtonsReset(hidOperate.Value);
                        textBoxsReset(hidOperate.Value, false);
                        return;

                    case "3":
                        if ((DepLevel <= 3) || (DepLevel == 7))
                        {
                            break;
                        }
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "show", "alert('" + Message.NoAuthority + "')", true);
                        ButtonsReset(hidOperate.Value);
                        textBoxsReset(hidOperate.Value, false);
                        return;

                    case "4":
                        if ((DepLevel <= 3) || (DepLevel == 7))
                        {
                            break;
                        }
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "show", "alert('" + Message.NoAuthority + "')", true);
                        ButtonsReset(hidOperate.Value);
                        textBoxsReset(hidOperate.Value, false);
                        return;

                    case "5":
                        if ((DepLevel <= 5) || (DepLevel == 7))
                        {
                            break;
                        }
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "show", "alert('" + Message.NoAuthority + "')", true);
                        ButtonsReset(hidOperate.Value);
                        textBoxsReset(hidOperate.Value, false);
                        return;

                    case "6":
                        if ((DepLevel <= 6) || (DepLevel == 7))
                        {
                            break;
                        }
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "show", "alert('" + Message.NoAuthority + "')", true);
                        ButtonsReset(hidOperate.Value);
                        textBoxsReset(hidOperate.Value, false);
                        return;

                    case "7":
                        if (DepLevel <= 7)
                        {
                            break;
                        }
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "show", "alert('" + Message.NoAuthority + "')", true);
                        ButtonsReset(hidOperate.Value);
                        textBoxsReset(hidOperate.Value, false);
                        return;

                    case "8":
                        if (DepLevel <= 8)
                        {
                            break;
                        }
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "show", "alert('" + Message.NoAuthority + "')", true);
                        ButtonsReset(hidOperate.Value);
                        textBoxsReset(hidOperate.Value, false);
                        return;
                }
            }
            if (activeFlag == "Add")
            {
                logmodel.ProcessFlag = "insert";
                model = PageHelper.GetModel<BUCalendarModel>(pnlContent.Controls);
                model.ModifyDate = DateTime.Now;
                model.Modifier = CurrentUserInfo.Personcode;
                model.HolidayFlag = ddlHoliDayFlag.SelectedValue;
                int flag = bll.InsertBUCalendarByKey(model, logmodel);
                if (flag > 0)
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "show", "alert('" + Message.AddSuccess + "')", true);
                    ButtonsReset(hidOperate.Value);
                    textBoxsReset("", false);
                    hidOperate.Value = "";
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "show", "alert('" + Message.AddFailed + "')", true);
                    ButtonsReset(hidOperate.Value);
                    textBoxsReset("", false);
                }
            }
            if (activeFlag == "Modify")
            {
                logmodel.ProcessFlag = "update";
                BUCalendarModel newmodel = new BUCalendarModel();
                newmodel = PageHelper.GetModel<BUCalendarModel>(pnlContent.Controls);
                newmodel.ModifyDate = DateTime.Now;
                newmodel.Modifier = CurrentUserInfo.Personcode;
                newmodel.HolidayFlag = ddlHoliDayFlag.SelectedValue;
                model.WorkDay = Convert.ToDateTime(hidWorkDay.Value);
                model.BUCode = hidBUCode.Value;
                int flag = bll.UpdateBUCalendarByKey(model, newmodel, logmodel);
                if (flag > 0)
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "show", "alert('" + Message.UpdateSuccess + "')", true);
                    hidOperate.Value = "";

                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "show", "alert('" + Message.UpdateFailed + "')", true);
                }
            }

            pager.CurrentPageIndex = 1;
            model = new BUCalendarModel();
            DataUIBind();
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
            string tableName = "GDS_ATT_BUCALENDAR_TEMP";
            string[] columnProperties = { "DEPNAME", "COSTCODE", "WORKDAY", "WORKFLAG", "HOLIDAYFLAG", "REMARK" };
            string[] columnType = { "varchar", "varchar", "varchar", "varchar", "varchar", "varchar" };
            string createUser = CurrentUserInfo.Personcode;
            deFlag = NPOIHelper.DeleteExcelSql(tableName, createUser);
            string filePath = GetImpFileName();
            if (string.IsNullOrEmpty(filePath))
                return -2;

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
                    lblupload.Text = Message.FailToUpload;
                }
            }
            else
            {
                lblupload.Text = Message.PathIsNull;
            }

            return filePath;
        }
        #endregion

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
                            case "ErrDepnameNull": errorInfo = errorInfo + Message.ErrDepnameNull + ",";
                                break;
                            case "ErrWorkDayNull": errorInfo = errorInfo + Message.ErrWorkDayNull + ";";
                                break;
                            case "ErrWorkFlagNull": errorInfo = errorInfo + Message.ErrWorkFlagNull + ";";
                                break;
                            case "ErrHolidayFlagNull": errorInfo = errorInfo + Message.ErrHolidayFlagNull + ";";
                                break;
                            case "NotOnlyOne": errorInfo = errorInfo + Message.NotOnlyOne + ";";
                                break;
                            case "ErrDepNameNotFind": errorInfo = errorInfo + Message.ErrDepNameNotFind + ";";
                                break;
                            case "ErrWorkFlag": errorInfo = errorInfo + Message.ErrWorkFlag + ";";
                                break;
                            case "ErrHolidayFlag": errorInfo = errorInfo + Message.ErrHolidayFlag + ";";
                                break;
                            case "ErrHolidayFlagNotEquals": errorInfo = errorInfo + Message.ErrHolidayFlagNotEquals + ";";
                                break;
                            case "ErrMoreDepName": errorInfo = errorInfo + Message.ErrMoreDepName + ";";
                                break;
                            case "common_message_data_errordate": errorInfo = errorInfo + Message.common_message_data_errordate + ";";
                                break;
                            case "ErrWrongDate": errorInfo = errorInfo + Message.ErrWrongDate + ";";
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

        #region 導出報表
        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (dt.Rows != null)
            {
                if (dt.Rows.Count != 0)
                {
                    model = PageHelper.GetModel<BUCalendarModel>(pnlContent.Controls);
                    List<BUCalendarModel> list = bll.GetList(dt);
                    string[] header = { ControlText.gvErrorMsg, ControlText.gvBUName, ControlText.gvCostCode, ControlText.gvWorkDay, ControlText.gvWorkFlag, ControlText.gvHolidayFlag, ControlText.gvRemark };
                    string[] properties = { "ErrorMsg", "DepName", "CostCode", "WorkDay", "WorkFlag", "HolidayFlag", "Remark" };
                    string filePath = MapPath("~/ExportFileTemp/") + DateTime.Now.Ticks + ".xls";
                    NPOIHelper.ExportExcel(list, header, properties, 5000, filePath);
                    PageHelper.ReturnHTTPStream(filePath, true);
                }
                else
                {
                    lblupload.Text = Message.DataIsNotFound;
                }
            }
        }
        #endregion
    }
}
